# Missile Protocol

**Ludum Dare 59 (theme: “Signal”) — 197th of 1,610 entries (top 12%), rated 4.4/5 on itch.io.**
An asymmetric local co-op game for two players on one device, made solo in 72 hours.

▶ **Play:** [pacelin.itch.io/missile-protocol](https://pacelin.itch.io/missile-protocol)

## The idea
You are stranded in enemy territory and must reach the nearest allied port under a missile barrage.
- **The Operator** sits at the ship’s console: levers, sliders, buttons, a radar and a signal screen.
- **The Instructor** holds the manual and talks the Operator through decoding incoming signals and defusing missiles.

Neither player can win alone, so communication is the core mechanic.

## Mechanics
- **Ship control** with simulated inertia: linear and angular acceleration/deceleration, a fuel supply and hull durability.
- **Radar sweep**: a rotating beam “pings” zones and missiles in its sector relative to the ship’s heading. A satellite slider aims the lock-on cone.
- **Signal decoding mini-games** on a CRT-style screen:
  - *Wave filter*: tune amplitude and frequency with stepped sliders until the sine wave matches the target. The FMOD sound changes live with the parameters.
  - *Bit filter*: scroll through a bit string and flip the corrupted bits.
- **Missiles** are defined as ScriptableObject configs (series, mark, length, solve code) that the Instructor looks up in the manual.
- **Map zones**: repair, refuel, safe and finish zones; randomized missile spawning.
- Main menu, pause, km/miles setting, RU/EN localization.

## Engineering
- **Architecture**: VContainer `LifetimeScope` composes plain C# models (`ShipModel`, `RadarModel`, `FuelModel`, velocity models) with entry-point controllers, and views subscribe to model events.
- **State machine** for the signal screen (setup → solve filter → solve missile → result messages).
- **Async bootstrap on UniTask**: audio and localization are initialized with cancellation before the first scene loads.
- **FMOD audio layer with code generation**: a strongly-typed `AudioSystem` is generated from FMOD banks, so events are called as `AudioSystem.Game_Machines_Sine` instead of string paths.
- **Tactile machine UI**: custom slider handles, buttons and switches with press animations on DOTween.
- **Editor tooling**: a missile-config batch creator, a 2D object placer (uniform random / Poisson-disc), a localization preview window and camera utilities.

## Tech stack
Unity 6 (6000.3) · URP 17 · C# · VContainer · UniTask · DOTween · FMOD · Cinemachine 3 · Unity Localization · TextMeshPro

## Project structure
```
Assets/_Project/
├── Scripts/
│   ├── Core/        # bootstrap, audio, pause, scene loading, localization helpers
│   ├── Game/
│   │   ├── Ship/    # ship, fuel and velocity models + controller
│   │   ├── Radar/   # radar sweep, pings, target lock
│   │   ├── Filters/ # wave & bit filters, signal-screen state machine
│   │   ├── Missles/ # missile configs, views, solve panel (+ editor tools)
│   │   ├── Map/     # zones, missiles spawner, map objects
│   │   └── View/    # machine controls: sliders, buttons, switches, gauges
│   └── MainMenu/
├── Plugins/Audio/   # FMOD wrapper + code generator
└── Localization/    # RU / EN string tables
```

---
Author: Pavel Kibirev — Unity Developer · [GitHub](https://github.com/Pacelin) · [LinkedIn](https://www.linkedin.com/in/pacelin)
