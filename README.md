# Run8 Train Simulator V3 Reverse Engineering

This repository contains reverse engineered documentation about various Run8 V3 file formats and file parsers. This information is intended for modding.

[/docs](/docs) - Contains file format specs

[GeneralInfo.md](/GeneralInfo.md) - Contains misc info such as routes

[/Run8](/Run8) - C# Library for working with file formats

[/blender_scripts](/blender_scripts) - Import/Export scripts for Blender 3.x

### Progress

-   [ ] AISignalDatabase.r8 (Yes, but low accuracy, will come back to it)
-   [ ] AISpecialLocations.r8
-   [ ] AITrackSpeedDatabase.r8 (Yes, but low accuracy, will come back to it)
-   [x] BlockDetectorDatabase.r8
-   [x] CarSpewerDatabase.r8
-   [x] CommTowerDatabase.r8
-   [ ] DarkSignalDatabase.r8
-   [x] DefectDetectorList.r8
-   [x] DispatcherBlockLightDatabase.r8
-   [x] DispatcherSignalControllerDatabase.r8
-   [x] DispatcherSwitchIconDatabase.r8
-   [ ] DispatchLabelConfig.r8
-   [ ] DispatchNextScreenConfig.r8
-   [x] HumpControllerList.r8
-   [x] Hump.r8
-   [ ] MilepostDatabase.r8
-   [ ] RoadDatabase.r8
-   [ ] ServiceAreaDatabase.r8
-   [ ] SignalHeadDatabase.r8
-   [x] TrackDatabase.r8
-   [ ] TunnelConfiguration.r8
-   [ ] XingDetectorList.r8
-   [ ] XingGateDatabase.r8
-   [ ] Industry Configuration (.ind)
-   [ ] Traffic.r8
-   [ ] Tile Scenery (x_y.rn8)
-   [ ] Tile Scenery Vegetation (x_y.veg)
-   [ ] Terrain Tiles 2 (x_y.tr2)
-   [ ] Terrain Tiles 3 (x_y.tr3)
-   [ ] Terrain Tiles 4 (x_y.tr4)
-   [ ] HornBellConfiguration.r8
-   [ ] 3D Model (.rn8) (Partially, implementations are very crude and barely working)
-   [ ] Texture (.tx8)
-   [ ] Avatars (Partially, implementations are very crude and barely working)
-   [x] Shaders/Effects (.tkb)
-   .xsb (XACT Sound Bank)
-   .xwb (XACT Wave Bank; `unxwb` from http://aluigi.altervista.org/papers.htm)
-   [x] Signal (.sig)
-   [ ] Run8Settings.r8
-   [x] stars4.r8
-   [x] Crossing Gate (.xng)
-   [ ] Timetable.xnb (Microsoft XNA)
-   [ ] .xnb (Microsoft XNA, can apparently store data and images)
