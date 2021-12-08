Team Grape Jello
Sean Kaat, David Cornell, Keely Culbertson, Ivan Leung, Jade Law

Starting scene: “Start Screen”
Main scene: “Main”

How to Play:
We recommend watching the gameplay video first to avoid any confusion. Fairly standard 3D third-person shooter controls. WASD movement/strafing, space to jump, E to interact, left click to shoot, mouse to control camera/direction. ESC will pause the game.
The goal is to collect enough of each of the 4 jello colors (bottom left of the HUD) and get to the crafting podium at the end to craft the Ultra Jello. To do so, visit the 4 areas along the path of the map and complete the challenges and kill enemies. You need 30 of each, which is entirely feasible for each color. The 4 areas are where the majority of the enemies, physics objects, interactables, etc. are located. Interact (E) with the ammo refill stations to refill ammo and health and to set your respawn point. To quit out of the game at any time, pause and go to Home > Quit. The game is NOT over until you reach the “You Win” screen.

Known Problem Areas:
- Some colliders can cause the player to “stick” (most prominently the trees, as they had to be the same physics material as the ground, as well as anything else with only a mesh collider for some reason)
- Occasionally, players have been able to clip through the acid/lava colliders (there are backup colliders though, along with a force-respawn button in the pause menu)
- Once, we have seen a player somehow lock the camera pointing downward with no way to reset it

Manifest by Teammate:
Not EVERY asset will be listed, nor every C# script, because there are just far too many. Also, most team members have seen/touched many different parts of the game, so what is listed here is just the primary contributor for each. Scenes will not be listed because we all worked on them.

Sean Kaat:
Player control, model, movement, animations, shooting, etc.
All in  Animations > Player
 	Animations > WeaponAnimator, WeaponIdle, WeaponShoot
All in 	Prefabs > Particle Effects
 	Prefabs > Player, Main Camera, Acid Jello Projectile
	Prefabs > Environment > Barrel, Box, lampost
All in 	Scripts > Events
All in 	Scripts > Audio
 	Scripts > FootIK, GroundChecker, Health, Interactable, PlayerController, Projectile, Respawner, ThirdPersonCamera, TriggerContactDamager
Most lighting, including: torch, light post, post processing, emissive acid materials, 

David Cornell:
Enemy behavior, AI, models, animations, drops, inventory, etc.
Animations > CastleFences, Ultra Jello (bobbing & rising)
All in Animations > Enemy
Animations > Misc > Arena (Platform/Opening), Bridges (Idle/Rising), CastleFences/Fence, Fake Ultra Jello (all), Real Ultra Jello (all)
All in Models > Jellos
All in Prefabs > Drops
All in Prefabs > Enemies
Prefabs > Acid Jello Projectile, Enemy Jello Projectile, Sniper Jello Projectile
All in Scripts > Enemy
Scripts > BossFightMovementDisabler, CastleFences, CastleTargets, ContactDamager, CursorLock, FallBossFightTrigger, GameQuitter, JelloDrop, JelloDropTrigger, JelloInventory, MusicPlayer, SceneLoader, SceneReloader, SniperProjectile, UltraJello, UltraJelloController

Keely Culbertson:
HUD, GUIs, menus, animations, etc.
All in Animations > UI Animations
Prefabs > GameManager, HUD Canvas
All in Scripts > HUD
All in Scripts > Start Screen
Scripts > StartScreen, UIManager, WinScreen

Ivan Leung:
Terrain creation/generation, map design, level design, etc.
Animations > Fan, Windmill
All in Models > Ivan’s Map Prefabs
All in Terrain
Scripts > LevelTorches, JelloInventory

Jade Law:
Interactions, physics objects, puzzles/challenges, objectives, etc.
Animations > CloudAnimator, CloudTurn, Coin Spin, Refill Station, Village coin, Question Mark
All in Prefabs > Environment
Prefabs > InstructionSign
Scripts > AmmoStation, CastleTarget, CraftTable, MovingPlatform, VillageCoin
