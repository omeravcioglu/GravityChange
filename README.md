# Gravity Change

**A one-screen 2D mobile arcade game made in Unity 2020.3 with URP.** Tap to flip gravity and fall onto the opposite platform, steer with an on-screen joystick, and dodge glowing geometric enemies that get more varied with every level.

## Screenshots

No screenshots yet. Add images under `docs/` (for example `docs/screenshots/`) and link them here.

---

## Gameplay

- **Flip gravity:** the hero is a glowing square between a floor and a ceiling platform. Releasing a touch on the right side of the screen flips gravity, and the hero falls to the other platform. Flipping only works while the hero is standing on a platform.
- **Steer:** a floating joystick on the left side of the screen (horizontal axis only) moves the hero between two side walls. The background drifts against the movement.
- **Dodge:** a new obstacle flies in from the left or right edge every 3 seconds at a random height. Touching one ends the run. Each level unlocks one more obstacle type, and most types change behaviour when they reach a random point on their path:

| Level | Prefab | Shape | Behaviour |
|---|---|---|---|
| 0 | `NormalEnemy` | square | Drifts straight across, spinning |
| 1 | `DeformEnemy` | polygon | Doubles in size |
| 2 | `ChuckEnemy` | square | Speeds up from 1 to 6 units/s |
| 3 | `DirectionChangerEnemy` | triangle | Turns to fly straight up or down |
| 4 | `ExploseEnemy` | heptagon | Bursts into four bullets (up, down, left, right) |
| 5 | `TurretEnemy` | hexagon with a gun | Stops, aims at the hero and fires every 3 s. Touching the turret body destroys it. |
| 6 | `Miner` | octagon with spikes | Drops a mine every 3 to 6 s. A mine explodes on contact or blows up by itself after a timer. |
| 7 | `Carusel` | square hub with a spinning bar | The bar kills; touching the hub destroys the whole obstacle |

- **Score and levels:** every landing on a platform scores 1 point. The HUD shows `points / target` and `Level: N`, where the target is (level + 1) × 20. Reaching it plays a "New level!" animation, resets the points, saves the level and unlocks the next obstacle type. Points reset on every run; the level is kept between sessions.
- **Flow:** main menu (**Start**, **Settings**, **Exit**) → "Let's Go!" → play. On the very first run, a tutorial overlay labels the two halves of the screen "Joystick" and "Jump" for a few seconds. When the hero is hit, a particle burst and a loss sound play, a Unity Ads interstitial is requested, and the lose panel offers **Retry** (clear the obstacles and play again at the same level) or **Menu** (reload the scene).
- **Settings:** three volume sliders (General, Sounds, Music) drive an AudioMixer and are saved between sessions. The music loop keeps playing across scene reloads.

**Status:** a small, self-contained game with the whole loop in place: menu, settings, first-run tutorial, endless play with level-ups, lose screen and an ad call after each loss. Android is the only configured platform, and the project contains evidence of an Android build that ran on a device (see *Android build settings* below). The project files are dated June 2022.

## Tech stack

| Area | What it uses |
|---|---|
| Engine | **Unity 2020.3.26f1** (2020 LTS), created from Unity's mobile 2D project template (`com.unity.template.mobile2d` 1.0.2) |
| Rendering | **Universal Render Pipeline** 10.8.1 with the **2D Renderer**; Bloom through a global Volume; a Sprite Unlit **Shader Graph** (`Glowing`) that multiplies the sprite by an HDR colour, used on the hero, the platforms and every obstacle |
| Physics | Unity **2D physics**: a dynamic `Rigidbody2D` hero whose gravity scale is flipped in code; kinematic trigger colliders for obstacles, bullets and mines |
| Input | **Joystick Pack** *Floating Joystick* for steering and a uGUI `EventTrigger` area for the gravity flip; legacy Input Manager (Space also flips gravity) |
| UI | Unity UI (uGUI) with legacy `Text`, Canvas Scaler at a 1920×1080 reference, Animator-driven text and panel animations |
| Ads | **Unity Ads** 3.7.5 (interstitial `video` placement) |
| Audio | `AudioMixer` with Master, Music and Sounds groups and three exposed volume parameters |
| Save data | `PlayerPrefs`: level, tutorial flag and the three volume settings |
| Build protection | **Obfuscator Free** (OPS) renames symbols in `Assembly-CSharp` during builds |
| Dev tooling | **Android Logcat** package |

> Mobile Notifications, TextMesh Pro, Timeline, 2D Animation, PSD Importer, 2D SpriteShape, 2D Tilemap and 2D Pixel Perfect are installed but not used by the scene or the scripts. Unity Analytics is switched on in the Services settings, but the code sends no custom events.

### Android build settings

| Setting | Value |
|---|---|
| Product name / version | Gravity Change, version 1.0.0 (bundle version code 1) |
| Package name | `com.Company.GravityChange` |
| Orientation | Landscape only (auto-rotates between landscape left and right) |
| Scripting backend / CPU | IL2CPP, ARMv7 |
| Minimum / target API level | 19 (Android 4.4) / highest installed |
| Graphics APIs | Vulkan, then OpenGL ES 3 |
| App icon | `Assets/Icons/Icon.png` (2134×2134) |
| Frame rate | `Application.targetFrameRate = 144`, set in `GameManager` |
| iOS and other platforms | Not set up; they keep the template's default bundle ID `com.DefaultCompany.com.unity.template.mobile2D` |

`Assets/OPS/Obfuscator.Free/Log/Android.txt` is the obfuscator's log from an Android build made with Unity 2020.3.5f1 (the project was later saved in 2020.3.26f1). It records 16 types, 77 methods, 108 fields and 36 properties renamed in `Assembly-CSharp`.

## What I built

All game code lives in **`Assets/Scripts/`** (`Hero/`, `Enemies/`, `Camera/` and the managers at the root): 24 C# scripts, about 1,200 lines.

| System | Key scripts |
|---|---|
| Gravity-flip hero (grounded check, ± gravity scale, joystick steering, death effect) | `HeroController` |
| Obstacle spawning with a level-based unlock of 8 enemy prefabs | `EnemySpawner`, `LevelsManager` |
| Shared enemy movement and a random "activation point" that starts each special behaviour | `EnemyMovement`, `RandomPlaceForEnemy` |
| Enemy behaviours (grow, speed up, change direction, burst into bullets, aim-and-shoot turret, mine layer) | `Deformer`, `Chuck`, `DirectionChanger`, `Exploser`, `Turret`, `Miner`, `Mine`, `BulletMovement` |
| Per-enemy coloured destruction particles and off-screen cleanup zones | `OnDestroyEnemy`, `DestroyerObjects` |
| Scoring, level-ups and level save | `PointsCounter`, `LevelsManager` |
| Game state, menus, HUD and lose screen (switched through UnityEvents in the scene) | `GameManager`, `uiManager` |
| First-run tutorial overlay | `TrainingManager` |
| Audio (5 sound effects and a music loop through an AudioMixer, saved volume sliders, music kept across scene reloads) | `AudioManager`, `DontDestroing`, `uiManager` |
| Interstitial ad after each loss | `AdManager` |
| Background drift and aspect-ratio camera scaling | `BackgroundManager`, `CameraScaling` |

Other project content: 11 prefabs (8 obstacles, bullet, mine and the death particle effect), the `Glowing` Shader Graph and material, six polygon sprites (triangle to octagon) packed into `FiguresAtlas`, the background image, the app icon, 5 UI animation clips, the `AudioMixer`, the music track `Gravity.mp3` and 5 sound effects.

### Code highlights

- **`Assets/Scripts/Hero/HeroController.cs`:** the core mechanic in about 150 lines. `changeGravity()` only works while the hero is on a platform and flips `Rigidbody2D.gravityScale` between `+speed` and `-speed`. Landing on a `Platform` raises a UnityEvent that adds a point, and the tag of whatever the hero touches decides the outcome: `Enemy` and `Mine` end the run, `EnemyKilled` (turret body, carousel hub) gets destroyed.
- **`Assets/Scripts/Enemies/RandomPlaceForEnemy.cs`:** picks a random X "activation point" for each enemy and exposes a 0-to-1 `progress` value. `Deformer`, `Chuck`, `DirectionChanger`, `Exploser` and `Turret` each wait in a coroutine for `progress == 1` and then run their behaviour, so every new enemy type is a small add-on component.
- **`Assets/Scripts/Enemies/EnemySpawner.cs`:** picks a random index below `level + 1` (capped at the list length) from an ordered prefab list, so the saved level number directly decides which obstacles can appear.
- **`Assets/Scripts/Enemies/Turret.cs`:** stops at its activation point, turns toward the hero with `Atan2` and `Quaternion.Lerp`, and fires a bullet along its facing every 3 seconds.
- **`Assets/Scenes/Main.unity`:** most of the game flow is wired in the Inspector with UnityEvents. For example, the hero's lose event stops the game, shows the lose panel, plays the death effect, stops the spawner and blocks the controls.

## Scenes (build order)

| # | Scene | Purpose |
|---|---|---|
| 0 | `Assets/Scenes/Main.unity` | The whole game: main menu, settings, tutorial, gameplay, HUD and lose screen. **Menu** reloads it. |

`Assets/Scenes/SampleScene/` only holds volume profiles (`Main.unity` uses its `Global Volume Profile`, a Bloom override). `Assets/Joystick Pack/Examples/Example Scene.unity` is the joystick pack's demo scene and is not in the build.

## Integrated third-party assets

| Asset | Used for |
|---|---|
| **Joystick Pack** (free, Unity Asset Store), `Assets/Joystick Pack/` | The *Floating Joystick* prefab, limited to the horizontal axis, steers the hero. The pack's other joystick types, documentation and example scene are also in the folder. |
| **Obfuscator Free** 3.9.9 by OPS (OrangePearSoftware / GuardingPearSoftware), `Assets/OPS/` | Renames classes, methods, fields and properties in `Assembly-CSharp` at build time. Namespaces, Unity message methods, serialized fields and strings are left alone (`Obfuscator.Free/Obfuscator_Settings.txt`). Bundles Mono.Cecil (MIT). |
| **Clickuper** font by Denis Ignatov, `Assets/Fonts/20008.ttf` | All UI text. The font file's metadata names the SIL Open Font License. |

Unity packages (URP, Shader Graph, Unity Ads, uGUI and the rest of `Packages/manifest.json`) are restored by the Package Manager and are not stored in `Assets/`.

## About this repository

This public repository is a **showcase**. It contains the documentation and the **24 source files I wrote** for this project. The complete project, including licensed third-party assets that cannot be redistributed, is kept in a private repository.

Copyright © Omer Avcioglu (McHunter Studio). **All rights reserved.** Viewing only; see LICENSE.
