using LibRun8.Formats;
using System.Text.Json;

var options = new JsonSerializerOptions { WriteIndented = true, NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowNamedFloatingPointLiterals };
string jsonString;

//Signal signal = Signal.Read(@"C:\Run8Studios\Run8 Train Simulator V3\Content\Signals\HRS_TwoLight2_Anakin_NewSystem.sig");
//string jsonString = JsonSerializer.Serialize(signal, options);
//File.WriteAllText("signal.json", jsonString);

//XNG signal = XNG.Read(@"C:\Run8Studios\Run8 Train Simulator V3\Content\Signals\XingGate01.xng");
//string jsonString = JsonSerializer.Serialize(signal, options);
//File.WriteAllText("xng.json", jsonString);

//TrackDatabase trackDatabase = TrackDatabase.Read(@"C:\Run8Studios\Run8 Train Simulator V3\Content\V3Routes\BNSF_MojaveSub\TrackDatabase.r8");
//jsonString = JsonSerializer.Serialize(trackDatabase, options);
//File.WriteAllText("TrackDatabase.json", jsonString);

//Stars4 stars4 = Stars4.Read(@"C:\Run8Studios\Run8 Train Simulator V3\Content\Misc\stars4.rn8");

//List<string> lines = new List<string>();
//for (int i = 0; i < stars4.Entries.Length; i++)
//{
//    lines.Add(string.Format("String {0} - {1}", i, stars4.Entries[i]));
//}

//File.WriteAllLines("stars4.txt", lines);

//AISignalDatabase aiSignalDatabase = AISignalDatabase.Read(@"C:\Run8Studios\Run8 Train Simulator V3\Content\V3Routes\BNSF_BakersfieldSub\AISignalDatabase.r8");
//string jsonString = JsonSerializer.Serialize(aiSignalDatabase, options);
//File.WriteAllText("AISignalDatabase.json", jsonString);

//AITrackSpeedDatabase aiTrackSpeedDatabase = AITrackSpeedDatabase.Read(@"C:\Run8Studios\Run8 Train Simulator V3\Content\V3Routes\SP-UP_RosevilleSub\AITrackSpeedDatabase.r8");jsonString = JsonSerializer.Serialize(aiTrackSpeedDatabase, options);
//File.WriteAllText("AITrackSpeedDatabase.json", jsonString);

//BlockDetectorDatabase blockDetectorDatabase = BlockDetectorDatabase.Read(@"C:\Run8Studios\Run8 Train Simulator V3\Content\V3Routes\BNSF_MojaveSub\BlockDetectorDatabase.r8");
//jsonString = JsonSerializer.Serialize(blockDetectorDatabase, options);
//File.WriteAllText("BlockDetectorDatabase.json", jsonString);

//CarSpewerDatabase carSpewerDatabase = CarSpewerDatabase.Read(@"C:\Run8Studios\Run8 Train Simulator V3\Content\V3Routes\BNSF_MojaveSub\CarSpewerDatabase.r8");
//jsonString = JsonSerializer.Serialize(carSpewerDatabase, options);
//File.WriteAllText("CarSpewerDatabase.json", jsonString);

//Console.WriteLine("Reading...");
//CommTowerDatabase commTowerDatabase = CommTowerDatabase.Read(@"C:\Run8Studios\Run8 Train Simulator V3\Content\V3Routes\BNSF_MojaveSub\CommTowerDatabase.r8");
//jsonString = JsonSerializer.Serialize(commTowerDatabase, options);
//File.WriteAllText("CommTowerDatabase.json", jsonString);

//Console.WriteLine("Reading...");
//DefectDetectorList defectDetectorList = DefectDetectorList.Read(@"C:\Run8Studios\Run8 Train Simulator V3\Content\V3Routes\BNSF_MojaveSub\DefectDetectorList.r8");
//jsonString = JsonSerializer.Serialize(defectDetectorList, options);
//File.WriteAllText("DefectDetectorList.json", jsonString);

//Console.WriteLine("Reading...");
//DispatcherLightBlockDatabase dispatcherLightBlockDatabase = DispatcherLightBlockDatabase.Read(@"C:\Run8Studios\Run8 Train Simulator V3\Content\V3Routes\BNSF_MojaveSub\DispatcherBlockLightDatabase.r8");
//jsonString = JsonSerializer.Serialize(dispatcherLightBlockDatabase, options);
//File.WriteAllText("DispatcherLightBlockDatabase.json", jsonString);

//Console.WriteLine("Reading...");
//DispatcherSignalControllerDatabase dispatcherSignalControllerDatabase = DispatcherSignalControllerDatabase.Read(@"C:\Run8Studios\Run8 Train Simulator V3\Content\V3Routes\BNSF_MojaveSub\DispatcherSignalControllerDatabase.r8");
//jsonString = JsonSerializer.Serialize(dispatcherSignalControllerDatabase, options);
//File.WriteAllText("DispatcherSignalControllerDatabase.json", jsonString);

//Console.WriteLine("Reading...");
//DispatcherSwitchIconDatabase dispatchSwitchIconDatabase = DispatcherSwitchIconDatabase.Read(@"C:\Run8Studios\Run8 Train Simulator V3\Content\V3Routes\BNSF_MojaveSub\DispatcherSwitchIconDatabase.r8");
//jsonString = JsonSerializer.Serialize(dispatchSwitchIconDatabase, options);
//File.WriteAllText("DispatcherSwitchIconDatabase.json", jsonString);

Console.WriteLine("Reading...");
RoadDatabase roadDatabase = RoadDatabase.Read(@"C:\Run8Studios\Run8 Train Simulator V3\Content\V3Routes\BNSF_MojaveSub\RoadDatabase.r8");
jsonString = JsonSerializer.Serialize(roadDatabase, options);
File.WriteAllText("RoadDatabase.json", jsonString);