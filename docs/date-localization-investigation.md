> 過去の調査記録です。調査用コードは整理済みのため、記載の診断ビルド手順は現行版では使用できません。

# 日付のローカライズAPI調査

## シグネチャで確認できた候補

| API | 入力・返り値 | 未確認の点 |
| --- | --- | --- |
| TextTagUtility.GetMonthText(month, needRuby) | 月の数値 → string | 各言語の表記、タグの有無 |
| TextTagUtility.GetDayOfWeekText(dayOfWeek, needRuby) | 曜日 → string | 長さ、タグの有無 |
| LanguageManager.GetDateTimeText(DateTime) | Am/Pm/季節/曜日のenum → string | 日時全体の整形ではない |
| GeneralInfoManager.GetDateText(ticks) | 指定日時のticks → string | 年・曜日の有無、言語別の並び順、未展開タグの有無 |
| TextTagUtility.MakeDateConstTagString(dateTime) | 指定日時 → string | 日付指定タグを作る候補。完成した表示文字列とは限らない |
| TextTagParser.Parse(text, mainTextMesh, rubyTextMesh, ...) | タグを含む文字列 → string | UIやルビ情報を扱うため、nullを渡せると仮定しない |

TextTagParser.CommonWorkにはconstDataTimeがあり、Year/Month/Day/DateConst/TimeConstなどのタグが存在する。
TimeAndDateTextIdに実在するIDを列挙し、LanguageManager.GetLocalizeText(TimeAndDateText, id, true)で日付テンプレートを確認できる。
BokuMonoDateTimeUtilityは比較・経過時間のAPIが中心で、調査した公開シグネチャに日付整形関数はなかった。

## 判断

季節・曜日の翻訳データはゲームのAPIで置き換えられる有力候補がある。
日付の語順・年・曜日を含む完成した表示はまだ確定していない。
GetDateTextが望ましい文字列を返せばそれを採用し、タグ文字列を返す場合はUIの既存パーサーとの接続を別途検証する。
完成予定日を表示すべき箇所で現在日を表示していないかも比較する。
既存の翻訳キーは、実機確認が済むまで削除しない。

## 日付API診断版

WINDMILL_DATE_DIAGNOSTICS付きビルドのみで、WindmillEtaFormatter.Formatに渡された現在日時と終了日時について候補APIの戻り値を記録する。
ゲームの日時・言語・UIテキストは書き換えず、パーサーも追加実行しない。
日付テンプレートは言語ごとに一度、日付サンプルは最大24件。毎フレームの出力は避ける。
現在動作確認済みの数値取得式ETAを維持する。

```powershell
dotnet build WindmillETA/WindmillETA.csproj -c Release -p:SkipPluginCopy=true -p:DefineConstants=WINDMILL_DATE_DIAGNOSTICS -o WindmillETA/bin/DateDiagnostics/net6.0
```

1. ゲーム終了中に通常版DLLをpluginsの外へ退避し、DateDiagnostics内のWindmillETA.dllへ置き換える。
2. 加工個数画面で、当日中と翌日以降になる個数を選ぶ。加工開始・セーブは不要。
3. ゲーム終了後、BepInEx/LogOutput.logの[ETA-DATE]行を確認。
4. まず日本語で確認し、必要なら次に英語でも確認する。再起動でログが更新されるので日本語ログを先に保存する。

ビルド成功はAPIの参照確認であり、戻り値の形式・多言語表示の実機確認は未完了。

## 日本語の実機ログで確認した結果

- GetMonthText(1, false) → `はるの月`。
- GetDayOfWeekText(Wednesday, false) → `水`。
- MakeDateConstTagString(1年目・はる12日) → `<dateconst=0001112>`。
- GetDateTextは11日・12日・13日のいずれも `1年目 はるの月 3週`。日付の代わりに週を表示する用途なのでETAには採用しない。
- TimeAndDateTextの107000 → `<month><day>日(<dayofweek>)`。
- 104000は年・日付・時刻・AM/PMを含むが、`<dateconst=p,0><timeconst=p,0>` に渡す引数も必要。
- 107010は `<hour>:<minute>`、107020は `<ampm>`。

次の最有力候補は、MakeDateConstTagString(end) と107000の言語別テンプレートを連結し、ゲームのTextTagParserで展開する方法。
例：`<dateconst=0001112><month><day>日(<dayofweek>)`。
これは組み立て候補であり、まだその文字列をパーサーに渡した実測結果ではない。
GetDateTimeTextは季節・曜日・AM/PMの部品取得であり、任意日時を丸ごと整形するAPIではない。

テンプレートと部品を共にゲームから取得できれば、翻訳JSONのSeasons・Weekdays・日付の並び順の部分を削減できる。
日付と時刻を結合する区切りや、同日なら日付を省略する方針はMod側に残る。
日本語の季節名が現在の「はる」から公式の「はるの月」になり、表示幅が広がるためUI確認も必要。
他言語の107000に含まれるタグや語順は未検証。手作業のReplaceで日本語の3タグだけを処理する方式は採用しない。

## ネイティブ日付の試験実装

WindmillNativeDateFormatter.TryFormatを追加。107000のrawテンプレートにMakeDateConstTagString(end)を付加し、別途生成したTextDataのGetTextに、表示先のLocalizedTextMeshProを文脈として渡す。
UI自身のTextDataは使わず、残り時間表示のキャッシュを書き換えない。日付整形APIの内部副作用はラッパーからは判定できないため実機で確認する。
空文字または山括弧の残る結果は失敗として従来の翻訳日付へ戻す。言語・年月日で最大128件をキャッシュし、成功/警告ログは各言語につき一度。
当日中には新しい処理を呼ばず時刻のみ表示。個数画面と加工中一覧で同じフォーマッタを使用。
実機確認：翌日以降が指定した日付・曜日になること、原文タグが残らないこと、レイアウトに収まること、残り時間表示・個数変更に副作用がないこと。各言語の表示は未検証。
