> 過去の調査記録です。調査用コードは整理済みのため、記載の診断ビルド手順は現行版では使用できません。

# 加工開始前の時間：数値API調査

## 確認済み（interopの型・シグネチャ）

- UIWindmillCraftCountDialog.time: intフィールド。単位・補正の有無は不明。
- SetInfoTime(int craftTime, int craftCount): 時間と個数を受け取る。
- UIWindmillCraftCountDialogData.Time: ダイアログに渡す時間。
- UICountDialog.countGetter / countText: 選択個数に関係する情報。
- WindmillCraftingMaster.GetTime(time, out day, out hour, out minute, out seconds)。
- 同GetTime(time, windLv, niceParts, multiplying, out day, out hour, out minute, out seconds)。
- GetCraftTimeTextは単一time版、補正引数付き版、day/hour/minute/seconds版がある。
- GetCraftEndTimeTicks(dateTime, time, windLv, niceParts, multiplying) はlongの終了時刻を返す。

元ファイルは decompile_Assembly-CSharp_dll/BokuMono/ 配下。
GetTimeとday/hour/minute/seconds版GetCraftTimeTextは、元ゲームではprivateだがinteropから参照可能。
呼び出しの有無・順番・内部計算式はラッパーから確定できない。CallerCount(0)も未使用の根拠にしない。

## 有力な実装候補（未確定）

1. ダイアログの更新中にゲームが呼ぶGetTimeの出力、またはGetCraftTimeTextのday/hour/minute/seconds引数を捕捉する。
2. 捕捉した日・時・分・秒を現在日時に加える。風・パーツ・個数の独自計算を避ける。
3. 捕捉APIがインライン化されている場合などは、SetInfoTimeの引数とゲーム本来の変換APIを使う方式を検討する。

確定前にtimeを「分」と決めつけたり、craftTime * craftCountを採用しない。
秒の扱いとUIの端数処理も確認する。現在の文字列方式は秒を利用していない。

## 診断版

WINDMILL_DIAGNOSTICSシンボル付きのビルドだけに観測パッチを含める。
通常ビルドでは観測パッチは存在しない。既存のETA計算と翻訳データは変更しない。
観測は引数・出力の読み取りのみ。ゲーム関数の追加呼び出し・値の変更はしない。
同じ観測点の直前と同じ内容は省略し、起動1回につき最大300件。ログに出ないことだけで未使用とは判断しない。

リポジトリルートからのビルド：

```powershell
dotnet build WindmillETA/WindmillETA.csproj -c Release -p:SkipPluginCopy=true -p:DefineConstants=WINDMILL_DIAGNOSTICS -o WindmillETA/bin/Diagnostics/net6.0
```

ゲームを終了し、plugins内の通常版WindmillETA.dllをpluginsの外へ退避して、診断版のWindmillETA.dllだけに置き換える。
他の依存DLLをコピーする必要はない。通常版と診断版を同時に配置しない。

### 最初に必要な操作

1. 日本語UIで起動。ログの `[ETA-DIAG] Diagnostic build active` と `WindmillETA patches applied!` を確認。
2. 風車の加工個数画面を開き、同じ品目で1個 → 2個 → 5個（可能な範囲） → 1個に変更。
3. 可能なら短時間の品目・日をまたぐ品目をそれぞれ表示する。
4. ゲーム終了後のBepInEx/LogOutput.logを確認。最初の調査では加工開始やセーブは不要。

この結果から、SetInfoTimeの入力と各GetTime/GetCraftTimeTextの数値・画面表示を対応付ける。
必要な場合のみ、次の調査で風やパーツの違い、加工開始後の終了時刻との一致を確認する。
観測パッチの実機適用は未確認。Harmonyエラーがあれば、それもログで確認する。
調査終了後は通常版DLLへ戻す。

## 実機ログから確認できたこと・数値取得版への変更

windLv=2、niceParts=None、time=300で以下が観測された。

| multiplying | GetTime modifiedの出力（日・時・分・秒） | OnUpdateでの表示 |
| --- | --- | --- |
| 1 | 0・3・45・3 | 3時間45分 |
| 2 | 0・7・30・7 | 7時間30分 |
| 7 | 1・2・15・26 | 1日2時間15分 |
| 10 | 1・13・30・37 | 1日13時間30分 |

field.timeは300で一定、multiplyingは選択個数と一致した。
SetInfoTime、GetTime simple、GetCraftTimeText partsの記録はなかったが、未使用と断定しない。
GetTime modifiedの補正済み日・時・分を利用する方針を採用した。風の補正式そのものは推測しない。

数値取得版ではOnUpdateのPrefixで捕捉範囲を開始し、GetTime modifiedのPostfixでその範囲内の出力だけ記録する。
timeがダイアログのtimeと一致するものを受け付け、OnUpdateのPostfixで使用する。
Finalizerで範囲を閉じ、例外時も前の値を持ち越さない。
取得できない更新ではETAを追加せず、最初の1回だけ警告する。
正規表現による時間解析と翻訳JSONのPattern要件は削除した。

注意：このログだけではGetTimeがOnUpdate内部で毎回呼ばれることまでは証明できない（ログは連続同値を省略している）。
新しい捕捉範囲内で取得できるか、Finalizerを含むHarmonyパッチの適用、個数変更後の継続表示は実機確認が必要。
通常版のRelease DLLで試し、取得できない旨の警告が出る場合は呼び出し範囲を追加調査する。
