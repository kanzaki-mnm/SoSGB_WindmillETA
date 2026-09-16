> 過去の調査記録です。調査用コードは整理済みのため、記載の診断ビルド手順は現行版では使用できません。

# Date width investigation

Historical procedure: the dialog scan hook was removed when ETA moved to a separate
plain TextMeshProUGUI row. Existing dialog results describe the old layout; the
list scan hook was also removed when the list switched to an independent TMP. Current builds do not invoke either width scan. See date-width-results.md for captured data.

Build the optional diagnostic DLL (normal builds exclude this code):

```powershell
dotnet build WindmillETA/WindmillETA.csproj -c Release -p:SkipPluginCopy=true -p:DefineConstants=WINDMILL_WIDTH_DIAGNOSTICS -o artifacts/date-width-diagnostics --no-restore
```

Replace the installed WindmillETA.dll with the diagnostic DLL, then start the game.
For each language, open a populated production list and the craft quantity dialog.
Leave each screen open until `[ETA-WIDTH] DONE` appears in BepInEx/LogOutput.log.
If a screen is closed early, measuring resumes when it becomes available again.
There must be an active craft in the list for its measurement to start.

Languages: ja, en, kr, ct, cs, ge, fr, sp. Switch language using the game's settings.
The diagnostic never changes language, game time, or time notation settings.
Save the log before restarting the game, because a new session replaces LogOutput.log.

The scan uses the actual native date formatter and the actual UI font. It enumerates
valid dates over up to one weekday-cycle of years, using the game's calendar bounds.
This covers the repeating season/day/weekday combinations of the fixed-length calendar.
Both screens are measured: list size 20 (including the arrow) and dialog size 24.
Work is spread across frames with a 3 ms target budget; one native call may exceed it.

MAX lines report the widest sample, its calendar components, TMP preferred width,
and the text rectangle's available width (local units, not screenshot pixels).
Ties keep the first example. Reports are separate for each language, surface and font.
They compare fixed time samples: 23:58, 12:58 AM, and 12:58 PM, using localized
AM/PM text. This is a date-layout comparison, NOT an exhaustive search over clock
digits. Font fallback, scaling and actual rendering still require screenshot checks.
A FAILED line means the run did not finish; do not treat it as a maximum result.

Return to a normal build after collecting the logs. The diagnostic DLL has been
compiled locally; its runtime measurements require the game and are not yet verified.
