using LibRun8.Formats;
using System.Text.Json;

var options = new JsonSerializerOptions { WriteIndented = true };

Signal signal = Signal.Read(@"C:\Run8Studios\Run8 Train Simulator V3\Content\Signals\HRS_TwoLight2_Anakin_NewSystem.sig");
string jsonString = JsonSerializer.Serialize(signal, options);
File.WriteAllText("signal.json", jsonString);

//XNG signal = XNG.Read(@"C:\Run8Studios\Run8 Train Simulator V3\Content\Signals\XingGate01.xng");
//string jsonString = JsonSerializer.Serialize(signal, options);
//File.WriteAllText("xng.json", jsonString);

//TrackDatabase trackDatabase = TrackDatabase.Read(@"C:\Run8Studios\Run8 Train Simulator V3\Content\V3Routes\SP-UP_RosevilleSub\TrackDatabase.r8");
//string jsonString = JsonSerializer.Serialize(trackDatabase, options);
//File.WriteAllText("trackdatabase.json", jsonString);

//Stars4 stars4 = Stars4.Read(@"C:\Run8Studios\Run8 Train Simulator V3\Content\Misc\stars4.rn8");
//for(int i = 0; i < stars4.entryCount; i++)
//{
//    Console.WriteLine("String {0} - {1}", i, stars4.entries[i]);
//}