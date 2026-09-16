# Date width measurement results

All 8 languages completed both screens (868 dates per run). No ETA-WIDTH FAILED entries.
Fixed time samples: 23:58, 12:58 AM and 12:58 PM (localized periods).
These are maximum sampled widths, not exhaustive maxima over every clock minute.
Ties retain the first example; reported dates are not necessarily unique winners.

Largest 12-hour sample: Spanish, spring, Wednesday:
12 Primavera (Miércoles) 12:58 a. m.
Dialog size 24: 490.92 local units. List size 20 including arrow: 435.75.
German summer/Thursday is a close second: dialog 490.49; list 435.40.
Largest 24-hour sample: German summer/Thursday: dialog 430.96; list 385.79.

List available width varies between 100.00 and 352.08 across runs.
Do not treat 100.00 as a confirmed final screen width; layout timing or resets
need separate investigation before changing geometry. Dialog available width
is consistently 370.00. Spanish list available width is 352.08.
The largest samples exceed those rectangles by 120.92 and 83.67 respectively.
Widths are TMP local units, not screen pixels; visually verify actual rendering.

## Preserved diagnostic output

```text
[Info   :WindmillETA] [ETA-WIDTH] MAX language=ja; surface=dialog; font=BO-SoftGoStd-DeBold SDF; size=24; clock=23:58; width=292.79; available=370.00; はるの月10日(月) 23:58; year=1, season=1, day=10, weekday=Monday
[Info   :WindmillETA] [ETA-WIDTH] MAX language=ja; surface=dialog; font=BO-SoftGoStd-DeBold SDF; size=24; clock=12:58 AM; width=344.33; available=370.00; はるの月10日(月) 12:58 AM; year=1, season=1, day=10, weekday=Monday
[Info   :WindmillETA] [ETA-WIDTH] MAX language=ja; surface=dialog; font=BO-SoftGoStd-DeBold SDF; size=24; clock=12:58 PM; width=343.44; available=370.00; はるの月10日(月) 12:58 PM; year=1, season=1, day=10, weekday=Monday
[Info   :WindmillETA] [ETA-WIDTH] DONE ja/dialog. Date combinations measured with fixed clock samples; clock digits are not exhaustive. Widths are TMP local units, not screen pixels.
[Info   :WindmillETA] [ETA-WIDTH] MAX language=ja; surface=list; font=BO-SoftGoStd-DeBold SDF; size=20; clock=23:58; width=270.65; available=352.08; → はるの月10日(月) 23:58; year=1, season=1, day=10, weekday=Monday
[Info   :WindmillETA] [ETA-WIDTH] MAX language=ja; surface=list; font=BO-SoftGoStd-DeBold SDF; size=20; clock=12:58 AM; width=313.60; available=352.08; → はるの月10日(月) 12:58 AM; year=1, season=1, day=10, weekday=Monday
[Info   :WindmillETA] [ETA-WIDTH] MAX language=ja; surface=list; font=BO-SoftGoStd-DeBold SDF; size=20; clock=12:58 PM; width=312.86; available=352.08; → はるの月10日(月) 12:58 PM; year=1, season=1, day=10, weekday=Monday
[Info   :WindmillETA] [ETA-WIDTH] DONE ja/list. Date combinations measured with fixed clock samples; clock digits are not exhaustive. Widths are TMP local units, not screen pixels.
[Info   :WindmillETA] [ETA-WIDTH] MAX language=en; surface=dialog; font=BO-SoftGoStd-DeBold SDF; size=24; clock=23:58; width=419.35; available=370.00; Summer 16 (Wednesday) 23:58; year=1, season=2, day=16, weekday=Wednesday
[Info   :WindmillETA] [ETA-WIDTH] MAX language=en; surface=dialog; font=BO-SoftGoStd-DeBold SDF; size=24; clock=12:58 AM; width=470.89; available=370.00; Summer 16 (Wednesday) 12:58 AM; year=1, season=2, day=16, weekday=Wednesday
[Info   :WindmillETA] [ETA-WIDTH] MAX language=en; surface=dialog; font=BO-SoftGoStd-DeBold SDF; size=24; clock=12:58 PM; width=470.00; available=370.00; Summer 16 (Wednesday) 12:58 PM; year=1, season=2, day=16, weekday=Wednesday
[Info   :WindmillETA] [ETA-WIDTH] DONE en/dialog. Date combinations measured with fixed clock samples; clock digits are not exhaustive. Widths are TMP local units, not screen pixels.
[Info   :WindmillETA] [ETA-WIDTH] MAX language=en; surface=list; font=BO-SoftGoStd-DeBold SDF; size=20; clock=23:58; width=376.12; available=100.00; → Summer 16 (Wednesday) 23:58; year=1, season=2, day=16, weekday=Wednesday
[Info   :WindmillETA] [ETA-WIDTH] MAX language=en; surface=list; font=BO-SoftGoStd-DeBold SDF; size=20; clock=12:58 AM; width=419.07; available=100.00; → Summer 16 (Wednesday) 12:58 AM; year=1, season=2, day=16, weekday=Wednesday
[Info   :WindmillETA] [ETA-WIDTH] MAX language=en; surface=list; font=BO-SoftGoStd-DeBold SDF; size=20; clock=12:58 PM; width=418.33; available=100.00; → Summer 16 (Wednesday) 12:58 PM; year=1, season=2, day=16, weekday=Wednesday
[Info   :WindmillETA] [ETA-WIDTH] DONE en/list. Date combinations measured with fixed clock samples; clock digits are not exhaustive. Widths are TMP local units, not screen pixels.
[Info   :WindmillETA] [ETA-WIDTH] MAX language=ct; surface=dialog; font=DFT_R9 SDF; size=24; clock=23:58; width=240.01; available=370.00; 春月10日（一） 23:58; year=1, season=1, day=10, weekday=Monday
[Info   :WindmillETA] [ETA-WIDTH] MAX language=ct; surface=dialog; font=DFT_R9 SDF; size=24; clock=12:58 AM; width=276.01; available=370.00; 春月10日（一） 12:58 AM; year=1, season=1, day=10, weekday=Monday
[Info   :WindmillETA] [ETA-WIDTH] MAX language=ct; surface=dialog; font=DFT_R9 SDF; size=24; clock=12:58 PM; width=276.01; available=370.00; 春月10日（一） 12:58 PM; year=1, season=1, day=10, weekday=Monday
[Info   :WindmillETA] [ETA-WIDTH] DONE ct/dialog. Date combinations measured with fixed clock samples; clock digits are not exhaustive. Widths are TMP local units, not screen pixels.
[Info   :WindmillETA] [ETA-WIDTH] MAX language=ct; surface=list; font=DFT_R9 SDF; size=20; clock=23:58; width=230.01; available=352.08; → 春月10日（一） 23:58; year=1, season=1, day=10, weekday=Monday
[Info   :WindmillETA] [ETA-WIDTH] MAX language=ct; surface=list; font=DFT_R9 SDF; size=20; clock=12:58 AM; width=260.01; available=352.08; → 春月10日（一） 12:58 AM; year=1, season=1, day=10, weekday=Monday
[Info   :WindmillETA] [ETA-WIDTH] MAX language=ct; surface=list; font=DFT_R9 SDF; size=20; clock=12:58 PM; width=260.01; available=352.08; → 春月10日（一） 12:58 PM; year=1, season=1, day=10, weekday=Monday
[Info   :WindmillETA] [ETA-WIDTH] DONE ct/list. Date combinations measured with fixed clock samples; clock digits are not exhaustive. Widths are TMP local units, not screen pixels.
[Info   :WindmillETA] [ETA-WIDTH] MAX language=cs; surface=dialog; font=FZY3K SDF; size=24; clock=23:58; width=220.55; available=370.00; 春月20日(周四) 23:58; year=1, season=1, day=20, weekday=Thursday
[Info   :WindmillETA] [ETA-WIDTH] MAX language=cs; surface=dialog; font=FZY3K SDF; size=24; clock=12:58 AM; width=256.79; available=370.00; 春月20日(周四) 12:58 AM; year=1, season=1, day=20, weekday=Thursday
[Info   :WindmillETA] [ETA-WIDTH] MAX language=cs; surface=dialog; font=FZY3K SDF; size=24; clock=12:58 PM; width=254.54; available=370.00; 春月20日(周四) 12:58 PM; year=1, season=1, day=20, weekday=Thursday
[Info   :WindmillETA] [ETA-WIDTH] DONE cs/dialog. Date combinations measured with fixed clock samples; clock digits are not exhaustive. Widths are TMP local units, not screen pixels.
[Info   :WindmillETA] [ETA-WIDTH] MAX language=cs; surface=list; font=FZY3K SDF; size=20; clock=23:58; width=208.80; available=100.00; → 春月20日(周四) 23:58; year=1, season=1, day=20, weekday=Thursday
[Info   :WindmillETA] [ETA-WIDTH] MAX language=cs; surface=list; font=FZY3K SDF; size=20; clock=12:58 AM; width=238.99; available=100.00; → 春月20日(周四) 12:58 AM; year=1, season=1, day=20, weekday=Thursday
[Info   :WindmillETA] [ETA-WIDTH] MAX language=cs; surface=list; font=FZY3K SDF; size=20; clock=12:58 PM; width=237.12; available=100.00; → 春月20日(周四) 12:58 PM; year=1, season=1, day=20, weekday=Thursday
[Info   :WindmillETA] [ETA-WIDTH] DONE cs/list. Date combinations measured with fixed clock samples; clock digits are not exhaustive. Widths are TMP local units, not screen pixels.
[Info   :WindmillETA] [ETA-WIDTH] MAX language=fr; surface=dialog; font=BO-SoftGoStd-DeBold SDF; size=24; clock=23:58; width=406.07; available=370.00; Dimanche 16 Printemps 23:58; year=1, season=1, day=16, weekday=Sunday
[Info   :WindmillETA] [ETA-WIDTH] MAX language=fr; surface=dialog; font=BO-SoftGoStd-DeBold SDF; size=24; clock=12:58 AM; width=457.60; available=370.00; Dimanche 16 Printemps 12:58 AM; year=1, season=1, day=16, weekday=Sunday
[Info   :WindmillETA] [ETA-WIDTH] MAX language=fr; surface=dialog; font=BO-SoftGoStd-DeBold SDF; size=24; clock=12:58 PM; width=456.72; available=370.00; Dimanche 16 Printemps 12:58 PM; year=1, season=1, day=16, weekday=Sunday
[Info   :WindmillETA] [ETA-WIDTH] DONE fr/dialog. Date combinations measured with fixed clock samples; clock digits are not exhaustive. Widths are TMP local units, not screen pixels.
[Info   :WindmillETA] [ETA-WIDTH] MAX language=fr; surface=list; font=BO-SoftGoStd-DeBold SDF; size=20; clock=23:58; width=365.05; available=100.00; → Dimanche 16 Printemps 23:58; year=1, season=1, day=16, weekday=Sunday
[Info   :WindmillETA] [ETA-WIDTH] MAX language=fr; surface=list; font=BO-SoftGoStd-DeBold SDF; size=20; clock=12:58 AM; width=407.99; available=100.00; → Dimanche 16 Printemps 12:58 AM; year=1, season=1, day=16, weekday=Sunday
[Info   :WindmillETA] [ETA-WIDTH] MAX language=fr; surface=list; font=BO-SoftGoStd-DeBold SDF; size=20; clock=12:58 PM; width=407.26; available=100.00; → Dimanche 16 Printemps 12:58 PM; year=1, season=1, day=16, weekday=Sunday
[Info   :WindmillETA] [ETA-WIDTH] DONE fr/list. Date combinations measured with fixed clock samples; clock digits are not exhaustive. Widths are TMP local units, not screen pixels.
[Info   :WindmillETA] [ETA-WIDTH] MAX language=ge; surface=dialog; font=BO-SoftGoStd-DeBold SDF; size=24; clock=23:58; width=430.96; available=370.00; 10. Sommer (Donnerstag) 23:58; year=1, season=2, day=10, weekday=Thursday
[Info   :WindmillETA] [ETA-WIDTH] MAX language=ge; surface=dialog; font=BO-SoftGoStd-DeBold SDF; size=24; clock=12:58 a.m.; width=490.49; available=370.00; 10. Sommer (Donnerstag) 12:58 a.m.; year=1, season=2, day=10, weekday=Thursday
[Info   :WindmillETA] [ETA-WIDTH] MAX language=ge; surface=dialog; font=BO-SoftGoStd-DeBold SDF; size=24; clock=12:58 p.m.; width=490.49; available=370.00; 10. Sommer (Donnerstag) 12:58 p.m.; year=1, season=2, day=10, weekday=Thursday
[Info   :WindmillETA] [ETA-WIDTH] DONE ge/dialog. Date combinations measured with fixed clock samples; clock digits are not exhaustive. Widths are TMP local units, not screen pixels.
[Info   :WindmillETA] [ETA-WIDTH] MAX language=ge; surface=list; font=BO-SoftGoStd-DeBold SDF; size=20; clock=23:58; width=385.79; available=100.00; → 10. Sommer (Donnerstag) 23:58; year=1, season=2, day=10, weekday=Thursday
[Info   :WindmillETA] [ETA-WIDTH] MAX language=ge; surface=list; font=BO-SoftGoStd-DeBold SDF; size=20; clock=12:58 a.m.; width=435.40; available=100.00; → 10. Sommer (Donnerstag) 12:58 a.m.; year=1, season=2, day=10, weekday=Thursday
[Info   :WindmillETA] [ETA-WIDTH] MAX language=ge; surface=list; font=BO-SoftGoStd-DeBold SDF; size=20; clock=12:58 p.m.; width=435.40; available=100.00; → 10. Sommer (Donnerstag) 12:58 p.m.; year=1, season=2, day=10, weekday=Thursday
[Info   :WindmillETA] [ETA-WIDTH] DONE ge/list. Date combinations measured with fixed clock samples; clock digits are not exhaustive. Widths are TMP local units, not screen pixels.
[Info   :WindmillETA] [ETA-WIDTH] MAX language=sp; surface=dialog; font=BO-SoftGoStd-DeBold SDF; size=24; clock=23:58; width=423.39; available=370.00; 12 Primavera (Miércoles) 23:58; year=1, season=1, day=12, weekday=Wednesday
[Info   :WindmillETA] [ETA-WIDTH] MAX language=sp; surface=dialog; font=BO-SoftGoStd-DeBold SDF; size=24; clock=12:58 a. m.; width=490.92; available=370.00; 12 Primavera (Miércoles) 12:58 a. m.; year=1, season=1, day=12, weekday=Wednesday
[Info   :WindmillETA] [ETA-WIDTH] MAX language=sp; surface=dialog; font=BO-SoftGoStd-DeBold SDF; size=24; clock=12:58 p. m.; width=490.92; available=370.00; 12 Primavera (Miércoles) 12:58 p. m.; year=1, season=1, day=12, weekday=Wednesday
[Info   :WindmillETA] [ETA-WIDTH] DONE sp/dialog. Date combinations measured with fixed clock samples; clock digits are not exhaustive. Widths are TMP local units, not screen pixels.
[Info   :WindmillETA] [ETA-WIDTH] MAX language=sp; surface=list; font=BO-SoftGoStd-DeBold SDF; size=20; clock=23:58; width=379.48; available=352.08; → 12 Primavera (Miércoles) 23:58; year=1, season=1, day=12, weekday=Wednesday
[Info   :WindmillETA] [ETA-WIDTH] MAX language=sp; surface=list; font=BO-SoftGoStd-DeBold SDF; size=20; clock=12:58 a. m.; width=435.75; available=352.08; → 12 Primavera (Miércoles) 12:58 a. m.; year=1, season=1, day=12, weekday=Wednesday
[Info   :WindmillETA] [ETA-WIDTH] MAX language=sp; surface=list; font=BO-SoftGoStd-DeBold SDF; size=20; clock=12:58 p. m.; width=435.75; available=352.08; → 12 Primavera (Miércoles) 12:58 p. m.; year=1, season=1, day=12, weekday=Wednesday
[Info   :WindmillETA] [ETA-WIDTH] DONE sp/list. Date combinations measured with fixed clock samples; clock digits are not exhaustive. Widths are TMP local units, not screen pixels.
[Info   :WindmillETA] [ETA-WIDTH] MAX language=kr; surface=dialog; font=NotoSansCJKkr-Bold SDF; size=24; clock=23:58; width=276.20; available=370.00; 여름철 10일(목요일) 23:58; year=1, season=2, day=10, weekday=Thursday
[Info   :WindmillETA] [ETA-WIDTH] MAX language=kr; surface=dialog; font=NotoSansCJKkr-Bold SDF; size=24; clock=12:58 AM; width=317.54; available=370.00; 여름철 10일(목요일) 12:58 AM; year=1, season=2, day=10, weekday=Thursday
[Info   :WindmillETA] [ETA-WIDTH] MAX language=kr; surface=dialog; font=NotoSansCJKkr-Bold SDF; size=24; clock=12:58 PM; width=318.17; available=370.00; 여름철 10일(목요일) 12:58 PM; year=1, season=2, day=10, weekday=Thursday
[Info   :WindmillETA] [ETA-WIDTH] DONE kr/dialog. Date combinations measured with fixed clock samples; clock digits are not exhaustive. Widths are TMP local units, not screen pixels.
[Info   :WindmillETA] [ETA-WIDTH] MAX language=kr; surface=list; font=NotoSansCJKkr-Bold SDF; size=20; clock=23:58; width=254.71; available=100.00; → 여름철 10일(목요일) 23:58; year=1, season=2, day=10, weekday=Thursday
[Info   :WindmillETA] [ETA-WIDTH] MAX language=kr; surface=list; font=NotoSansCJKkr-Bold SDF; size=20; clock=12:58 AM; width=289.16; available=100.00; → 여름철 10일(목요일) 12:58 AM; year=1, season=2, day=10, weekday=Thursday
[Info   :WindmillETA] [ETA-WIDTH] MAX language=kr; surface=list; font=NotoSansCJKkr-Bold SDF; size=20; clock=12:58 PM; width=289.68; available=100.00; → 여름철 10일(목요일) 12:58 PM; year=1, season=2, day=10, weekday=Thursday
[Info   :WindmillETA] [ETA-WIDTH] DONE kr/list. Date combinations measured with fixed clock samples; clock digits are not exhaustive. Widths are TMP local units, not screen pixels.
```
