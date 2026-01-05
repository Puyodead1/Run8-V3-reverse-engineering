using LibRun8.Formats;
using LibRun8.Formats.Terrain;
using Newtonsoft.Json;
using LibRun8.Common;

var options = new JsonSerializerSettings { Formatting = Formatting.Indented };
string jsonString;

Console.WriteLine("Reading...");

//Signal signal = Signal.Read(@"E:\Run8Studios\Run8 Train Simulator V3\Content\Signals\HRS_TwoLight2_Anakin_NewSystem.sig");
//string jsonString = JsonConvert.SerializeObject(signal, options);
//File.WriteAllText("signal.json", jsonString);

//XNG signal = XNG.Read(@"E:\Run8Studios\Run8 Train Simulator V3\Content\Signals\XingGate01.xng");
//string jsonString = JsonConvert.SerializeObject(signal, options);
//File.WriteAllText("xng.json", jsonString);

//TrackDatabase trackDatabase = TrackDatabase.Read(@"E:\Run8Studios\Run8 Train Simulator V3\Content\V3Routes\BNSF_MojaveSub\TrackDatabase.r8");
//jsonString = JsonConvert.SerializeObject(trackDatabase, options);
//File.WriteAllText("TrackDatabase.json", jsonString);

// Stars4 stars4 = Stars4.Read(@"E:\Run8Studios\Run8 Train Simulator V3\Content\Misc\stars4.rn8");

// List<string> lines = new List<string>();
// for (int i = 0; i < stars4.Entries.Length; i++)
// {
//     lines.Add(string.Format("String {0} - {1}", i, stars4.Entries[i]));
// }

// File.WriteAllLines("stars4.txt", lines);

//AISignalDatabase aiSignalDatabase = AISignalDatabase.Read(@"E:\Run8Studios\Run8 Train Simulator V3\Content\V3Routes\BNSF_BakersfieldSub\AISignalDatabase.r8");
//string jsonString = JsonConvert.SerializeObject(aiSignalDatabase, options);
//File.WriteAllText("AISignalDatabase.json", jsonString);

//AITrackSpeedDatabase aiTrackSpeedDatabase = AITrackSpeedDatabase.Read(@"E:\Run8Studios\Run8 Train Simulator V3\Content\V3Routes\SP-UP_RosevilleSub\AITrackSpeedDatabase.r8");jsonString = JsonConvert.SerializeObject(aiTrackSpeedDatabase, options);
//File.WriteAllText("AITrackSpeedDatabase.json", jsonString);

//BlockDetectorDatabase blockDetectorDatabase = BlockDetectorDatabase.Read(@"E:\Run8Studios\Run8 Train Simulator V3\Content\V3Routes\BNSF_MojaveSub\BlockDetectorDatabase.r8");
//jsonString = JsonConvert.SerializeObject(blockDetectorDatabase, options);
//File.WriteAllText("BlockDetectorDatabase.json", jsonString);

//CarSpewerDatabase carSpewerDatabase = CarSpewerDatabase.Read(@"E:\Run8Studios\Run8 Train Simulator V3\Content\V3Routes\BNSF_MojaveSub\CarSpewerDatabase.r8");
//jsonString = JsonConvert.SerializeObject(carSpewerDatabase, options);
//File.WriteAllText("CarSpewerDatabase.json", jsonString);

//CommTowerDatabase commTowerDatabase = CommTowerDatabase.Read(@"E:\Run8Studios\Run8 Train Simulator V3\Content\V3Routes\BNSF_MojaveSub\CommTowerDatabase.r8");
//jsonString = JsonConvert.SerializeObject(commTowerDatabase, options);
//File.WriteAllText("CommTowerDatabase.json", jsonString);

//DefectDetectorList defectDetectorList = DefectDetectorList.Read(@"E:\Run8Studios\Run8 Train Simulator V3\Content\V3Routes\CSX_ALine\DefectDetectorList.r8");
//jsonString = JsonConvert.SerializeObject(defectDetectorList, options);
//File.WriteAllText("DefectDetectorList.json", jsonString);

// test writing defect detector list back to r8
//Console.WriteLine("Writing...");
//defectDetectorList.Write("DefectDetectorList.r8");

//DispatcherLightBlockDatabase dispatcherLightBlockDatabase = DispatcherLightBlockDatabase.Read(@"E:\Run8Studios\Run8 Train Simulator V3\Content\V3Routes\BNSF_MojaveSub\DispatcherBlockLightDatabase.r8");
//jsonString = JsonConvert.SerializeObject(dispatcherLightBlockDatabase, options);
//File.WriteAllText("DispatcherLightBlockDatabase.json", jsonString);

//DispatcherSignalControllerDatabase dispatcherSignalControllerDatabase = DispatcherSignalControllerDatabase.Read(@"E:\Run8Studios\Run8 Train Simulator V3\Content\V3Routes\BNSF_MojaveSub\DispatcherSignalControllerDatabase.r8");
//jsonString = JsonConvert.SerializeObject(dispatcherSignalControllerDatabase, options);
//File.WriteAllText("DispatcherSignalControllerDatabase.json", jsonString);

//DispatcherSwitchIconDatabase dispatchSwitchIconDatabase = DispatcherSwitchIconDatabase.Read(@"E:\Run8Studios\Run8 Train Simulator V3\Content\V3Routes\BNSF_MojaveSub\DispatcherSwitchIconDatabase.r8");
//jsonString = JsonConvert.SerializeObject(dispatchSwitchIconDatabase, options);
//File.WriteAllText("DispatcherSwitchIconDatabase.json", jsonString);

//RoadDatabase roadDatabase = RoadDatabase.Read(@"E:\Run8Studios\Run8 Train Simulator V3\Content\V3Routes\BNSF_MojaveSub\RoadDatabase.r8");
//jsonString = JsonConvert.SerializeObject(roadDatabase, options);
//File.WriteAllText("RoadDatabase.json", jsonString);

//HumpControllerList humpControllerList = HumpControllerList.Read(@"E:\Run8Studios\Run8 Train Simulator V3\Content\V3Routes\SP-UP_RosevilleSub\HumpControllerList.r8");
//jsonString = JsonConvert.SerializeObject(humpControllerList, options);
//File.WriteAllText("HumpControllerList.json", jsonString);

//HumpConfigDatabase hump = HumpConfigDatabase.Read(@"E:\Run8Studios\Run8 Train Simulator V3\Content\V3Routes\Regions\SouthernCA\Hump.r8");
//jsonString = JsonConvert.SerializeObject(hump, options);
//File.WriteAllText("Hump.json", jsonString);

//Texture.Decrypt(@"E:\Run8Studios\Run8 Train Simulator V3\Content\V3RailVehicles\Body\Run8_ES44DC_CabNS.tx8");

//TerrainTile tile = Terrain.Read(@"D:\Programs\Run8Studios\V2\Content\V2Routes\Regions\SouthernCA\TerrainTiles\00005_00048.tr2");
//jsonString = JsonConvert.SerializeObject(tile, options);
//File.WriteAllText("TerrainTile.json", jsonString);

//TerrainTile tile = Terrain.Read(@"E:\Run8Studios\Run8 Train Simulator V3\Content\V3Routes\Regions\SouthernCA\TerrainTiles\-00006_00044.tr4");
//jsonString = JsonConvert.SerializeObject(tile, options);
//File.WriteAllText("TerrainTile.json", jsonString);

Model model = Model.Read(@"E:\Run8Studios\Run8 Train Simulator V3\Content\V3RailVehicles\Body\Run8_ES44_BNSF01.rn8");
//Model model = Model.Read(@"C:\Users\23562\Documents\Code\Run8-V3-reverse-engineering\misc_scripts\R8_Caboose_c509_SP01.rn8");
//foreach (ModelObject obj in model.Objects)
//{
//    obj.Vertices.Clear();
//    obj.Indices = new int[] { };
//}
//jsonString = JsonConvert.SerializeObject(model, options);
//File.WriteAllText("Model.json", jsonString);

model.WriteGLTF(@"C:\Users\23562\Documents\Code\Run8-V3-reverse-engineering\misc_scripts\Run8_ES44_BNSF01.glb");