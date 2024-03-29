﻿using LibRun8.Formats;
using LibRun8.Formats.Terrain;
using LibRun8.Utils;
using System.Text.Json;

var options = new JsonSerializerOptions { WriteIndented = true, NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowNamedFloatingPointLiterals };
string jsonString;

Console.WriteLine("Reading...");

//Signal signal = Signal.Read(@"D:\Programs\Run8Studios\Run8 Train Simulator V3\Content\Signals\HRS_TwoLight2_Anakin_NewSystem.sig");
//string jsonString = JsonSerializer.Serialize(signal, options);
//File.WriteAllText("signal.json", jsonString);

//XNG signal = XNG.Read(@"D:\Programs\Run8Studios\Run8 Train Simulator V3\Content\Signals\XingGate01.xng");
//string jsonString = JsonSerializer.Serialize(signal, options);
//File.WriteAllText("xng.json", jsonString);

//TrackDatabase trackDatabase = TrackDatabase.Read(@"D:\Programs\Run8Studios\Run8 Train Simulator V3\Content\V3Routes\BNSF_MojaveSub\TrackDatabase.r8");
//jsonString = JsonSerializer.Serialize(trackDatabase, options);
//File.WriteAllText("TrackDatabase.json", jsonString);

//Stars4 stars4 = Stars4.Read(@"D:\Programs\Run8Studios\Run8 Train Simulator V3\Content\Misc\stars4.rn8");

//List<string> lines = new List<string>();
//for (int i = 0; i < stars4.Entries.Length; i++)
//{
//    lines.Add(string.Format("String {0} - {1}", i, stars4.Entries[i]));
//}

//File.WriteAllLines("stars4.txt", lines);

//AISignalDatabase aiSignalDatabase = AISignalDatabase.Read(@"D:\Programs\Run8Studios\Run8 Train Simulator V3\Content\V3Routes\BNSF_BakersfieldSub\AISignalDatabase.r8");
//string jsonString = JsonSerializer.Serialize(aiSignalDatabase, options);
//File.WriteAllText("AISignalDatabase.json", jsonString);

//AITrackSpeedDatabase aiTrackSpeedDatabase = AITrackSpeedDatabase.Read(@"D:\Programs\Run8Studios\Run8 Train Simulator V3\Content\V3Routes\SP-UP_RosevilleSub\AITrackSpeedDatabase.r8");jsonString = JsonSerializer.Serialize(aiTrackSpeedDatabase, options);
//File.WriteAllText("AITrackSpeedDatabase.json", jsonString);

//BlockDetectorDatabase blockDetectorDatabase = BlockDetectorDatabase.Read(@"D:\Programs\Run8Studios\Run8 Train Simulator V3\Content\V3Routes\BNSF_MojaveSub\BlockDetectorDatabase.r8");
//jsonString = JsonSerializer.Serialize(blockDetectorDatabase, options);
//File.WriteAllText("BlockDetectorDatabase.json", jsonString);

//CarSpewerDatabase carSpewerDatabase = CarSpewerDatabase.Read(@"D:\Programs\Run8Studios\Run8 Train Simulator V3\Content\V3Routes\BNSF_MojaveSub\CarSpewerDatabase.r8");
//jsonString = JsonSerializer.Serialize(carSpewerDatabase, options);
//File.WriteAllText("CarSpewerDatabase.json", jsonString);

//CommTowerDatabase commTowerDatabase = CommTowerDatabase.Read(@"D:\Programs\Run8Studios\Run8 Train Simulator V3\Content\V3Routes\BNSF_MojaveSub\CommTowerDatabase.r8");
//jsonString = JsonSerializer.Serialize(commTowerDatabase, options);
//File.WriteAllText("CommTowerDatabase.json", jsonString);

//DefectDetectorList defectDetectorList = DefectDetectorList.Read(@"D:\Programs\Run8Studios\Run8 Train Simulator V3\Content\V3Routes\BNSF_MojaveSub\DefectDetectorList.r8");
//jsonString = JsonSerializer.Serialize(defectDetectorList, options);
//File.WriteAllText("DefectDetectorList.json", jsonString);

//DispatcherLightBlockDatabase dispatcherLightBlockDatabase = DispatcherLightBlockDatabase.Read(@"D:\Programs\Run8Studios\Run8 Train Simulator V3\Content\V3Routes\BNSF_MojaveSub\DispatcherBlockLightDatabase.r8");
//jsonString = JsonSerializer.Serialize(dispatcherLightBlockDatabase, options);
//File.WriteAllText("DispatcherLightBlockDatabase.json", jsonString);

//DispatcherSignalControllerDatabase dispatcherSignalControllerDatabase = DispatcherSignalControllerDatabase.Read(@"D:\Programs\Run8Studios\Run8 Train Simulator V3\Content\V3Routes\BNSF_MojaveSub\DispatcherSignalControllerDatabase.r8");
//jsonString = JsonSerializer.Serialize(dispatcherSignalControllerDatabase, options);
//File.WriteAllText("DispatcherSignalControllerDatabase.json", jsonString);

//DispatcherSwitchIconDatabase dispatchSwitchIconDatabase = DispatcherSwitchIconDatabase.Read(@"D:\Programs\Run8Studios\Run8 Train Simulator V3\Content\V3Routes\BNSF_MojaveSub\DispatcherSwitchIconDatabase.r8");
//jsonString = JsonSerializer.Serialize(dispatchSwitchIconDatabase, options);
//File.WriteAllText("DispatcherSwitchIconDatabase.json", jsonString);

//RoadDatabase roadDatabase = RoadDatabase.Read(@"D:\Programs\Run8Studios\Run8 Train Simulator V3\Content\V3Routes\BNSF_MojaveSub\RoadDatabase.r8");
//jsonString = JsonSerializer.Serialize(roadDatabase, options);
//File.WriteAllText("RoadDatabase.json", jsonString);

//HumpControllerList humpControllerList = HumpControllerList.Read(@"D:\Programs\Run8Studios\Run8 Train Simulator V3\Content\V3Routes\SP-UP_RosevilleSub\HumpControllerList.r8");
//jsonString = JsonSerializer.Serialize(humpControllerList, options);
//File.WriteAllText("HumpControllerList.json", jsonString);

//HumpConfigDatabase hump = HumpConfigDatabase.Read(@"D:\Programs\Run8Studios\Run8 Train Simulator V3\Content\V3Routes\Regions\\NorthernCA\Hump.r8");
//jsonString = JsonSerializer.Serialize(hump, options);
//File.WriteAllText("Hump.json", jsonString);

//Texture.DecryptTexture(@"D:\Programs\Run8Studios\Run8 Train Simulator V3\Content\V3RailVehicles\Body\R8_T389_ACFX.tx8");

// TODO: we need to come back to this
//TerrainTile tile = Terrain.Read(@"D:\Programs\Run8Studios\Run8 Train Simulator V3\Content\V3Routes\Regions\Pennsylvania\TerrainTiles\00000_00000.tr4");
//jsonString = JsonSerializer.Serialize(tile, options);
//File.WriteAllText("TerrainTile.json", jsonString);
//tile.WriteOBJ(@"D:\Programs\Run8Studios\V2\Content\V2Routes\Regions\SouthernCA\TerrainTiles\00249_-00019.obj");

Model model = Model.Read(@"D:\Programs\Run8Studios\Run8 Train Simulator V3\Content\V3RailVehicles\Body\R8_SD45-2_SBS01.rn8");
jsonString = JsonSerializer.Serialize(model, options);
File.WriteAllText("Model.json", jsonString);
