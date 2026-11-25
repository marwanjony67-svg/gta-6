# BudgieGTA — Open-World Unity Starter

An original open-world driving and exploration game prototype for Windows PC, built with Unity 6 and C#. This is a fan-made learning project and is **not affiliated with Rockstar Games**.

> This repository does not contain Grand Theft Auto VI, Rockstar source code, GTA VI characters, story, map, models, music, logos, or other proprietary assets. Build an original city, cast, missions, and art.

## Current prototype systems
- Third-person walking controller
- Simple arcade vehicle controller
- Drive-through mission trigger
- Procedural blockout city generator
- Basic on-screen objective message

This is a playable foundation, not a finished AAA game or a complete GTA VI replacement. A full commercial-scale open-world game requires a large team, original assets, extensive testing, and substantial development time.

## Create the Unity project
1. Install Unity 6 and create a **3D (Core)** project.
2. Copy the `Assets` folder from this repository into the Unity project.
3. Open the project and wait for scripts to compile.
4. Create a Plane for the ground.
5. Create a Capsule named `Player`, add a CharacterController and attach `PlayerController.cs`.
6. Create a Cube or car model, add a Rigidbody and BoxCollider, then attach `ArcadeCarController.cs`.
7. Create an empty GameObject named `CityGenerator` and attach `CityBlockGenerator.cs`.
8. Create a trigger collider and attach `MissionZone.cs`; enable **Is Trigger**.
9. Press Play. Use WASD/arrow keys to move; use Space to brake in the car.

## Controls
- Walking: WASD / arrow keys
- Look: mouse (when a camera rig is configured)
- Vehicle: W/S accelerate/reverse, A/D steer, Space brake

The prototype uses Unity's legacy Input Manager for simple starter controls. Unity's documentation notes that new projects should prefer the Input System package; this starter keeps setup simple. See the Unity 6 docs: https://docs.unity3d.com/6000.0/Documentation/ScriptReference/CharacterController.html

## Next milestones
1. Original city map and district layout
2. Camera and interaction system
3. Vehicle enter/exit and garage
4. Mission and save systems
5. Pedestrian and traffic simulation
6. Audio, UI, accessibility, optimization
7. Windows build and QA

Only use assets you created yourself or have permission to use.
