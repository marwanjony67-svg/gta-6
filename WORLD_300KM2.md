# BudgieGTA — 300 km² Open-World Prototype

An original Unity 6 open-world driving and exploration game for Windows PC. This project is not Grand Theft Auto VI and is not affiliated with Rockstar Games. It uses original locations, characters, missions, code and assets.

## World target
- Target playable world area: **300 km²**
- World bounds: **17,320 m × 17,320 m** (approximately 299.98 km²)
- Runtime streaming: 1 km grid cells around the player
- The streamer creates and unloads lightweight blockout ground chunks as the player travels.
- This is a procedural blockout foundation, not a finished 300 km² authored city or AAA game.

## Add the world streamer
1. Open the repository's Unity 6 project.
2. Add `Assets/BudgieGTA/World/BudgieWorldStreamer.cs` to an empty GameObject named `WorldStreamer`.
3. Assign the player Transform to **Player** in the Inspector.
4. Press Play. The script creates nearby ground chunks and unloads distant chunks.
5. Tune chunk size and view radius in the Inspector. Keep the default 1,000 m chunk size for the 300 km² target.

## Development notes
- Use additive scenes or addressable assets for authored districts, interiors and mission areas.
- Replace the placeholder ground meshes with optimized terrain/road meshes and streamed asset bundles.
- Add traffic, pedestrians, missions, save/load, audio, UI, accessibility and performance profiling in separate milestones.
- Test memory and frame rate while travelling between chunks; do not load the entire world at once.

## Controls and build
This commit adds the world-streaming foundation only. Existing player and vehicle controls remain as implemented elsewhere in the project. Create a Windows x86-64 build through Unity Build Profiles after configuring the main scene.

Only use original assets or assets you have permission to use.
