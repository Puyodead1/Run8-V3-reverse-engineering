using LibRun8.Formats;
using LibRun8.Formats.Terrain;
using Newtonsoft.Json;
using Collada141;
using LibRun8.Collada;
using LibRun8.Common;

var options = new JsonSerializerSettings { Formatting = Formatting.Indented };
string jsonString;

Console.WriteLine("Reading...");

//Signal signal = Signal.Read(@"E:\Program Files\Run8Studios\Run8 Train Simulator V3\Content\Signals\HRS_TwoLight2_Anakin_NewSystem.sig");
//string jsonString = JsonConvert.SerializeObject(signal, options);
//File.WriteAllText("signal.json", jsonString);

//XNG signal = XNG.Read(@"E:\Program Files\Run8Studios\Run8 Train Simulator V3\Content\Signals\XingGate01.xng");
//string jsonString = JsonConvert.SerializeObject(signal, options);
//File.WriteAllText("xng.json", jsonString);

//TrackDatabase trackDatabase = TrackDatabase.Read(@"E:\Program Files\Run8Studios\Run8 Train Simulator V3\Content\V3Routes\BNSF_MojaveSub\TrackDatabase.r8");
//jsonString = JsonConvert.SerializeObject(trackDatabase, options);
//File.WriteAllText("TrackDatabase.json", jsonString);

//Stars4 stars4 = Stars4.Read(@"E:\Program Files\Run8Studios\Run8 Train Simulator V3\Content\Misc\stars4.rn8");

//List<string> lines = new List<string>();
//for (int i = 0; i < stars4.Entries.Length; i++)
//{
//    lines.Add(string.Format("String {0} - {1}", i, stars4.Entries[i]));
//}

//File.WriteAllLines("stars4.txt", lines);

//AISignalDatabase aiSignalDatabase = AISignalDatabase.Read(@"E:\Program Files\Run8Studios\Run8 Train Simulator V3\Content\V3Routes\BNSF_BakersfieldSub\AISignalDatabase.r8");
//string jsonString = JsonConvert.SerializeObject(aiSignalDatabase, options);
//File.WriteAllText("AISignalDatabase.json", jsonString);

//AITrackSpeedDatabase aiTrackSpeedDatabase = AITrackSpeedDatabase.Read(@"E:\Program Files\Run8Studios\Run8 Train Simulator V3\Content\V3Routes\SP-UP_RosevilleSub\AITrackSpeedDatabase.r8");jsonString = JsonConvert.SerializeObject(aiTrackSpeedDatabase, options);
//File.WriteAllText("AITrackSpeedDatabase.json", jsonString);

//BlockDetectorDatabase blockDetectorDatabase = BlockDetectorDatabase.Read(@"E:\Program Files\Run8Studios\Run8 Train Simulator V3\Content\V3Routes\BNSF_MojaveSub\BlockDetectorDatabase.r8");
//jsonString = JsonConvert.SerializeObject(blockDetectorDatabase, options);
//File.WriteAllText("BlockDetectorDatabase.json", jsonString);

//CarSpewerDatabase carSpewerDatabase = CarSpewerDatabase.Read(@"E:\Program Files\Run8Studios\Run8 Train Simulator V3\Content\V3Routes\BNSF_MojaveSub\CarSpewerDatabase.r8");
//jsonString = JsonConvert.SerializeObject(carSpewerDatabase, options);
//File.WriteAllText("CarSpewerDatabase.json", jsonString);

//CommTowerDatabase commTowerDatabase = CommTowerDatabase.Read(@"E:\Program Files\Run8Studios\Run8 Train Simulator V3\Content\V3Routes\BNSF_MojaveSub\CommTowerDatabase.r8");
//jsonString = JsonConvert.SerializeObject(commTowerDatabase, options);
//File.WriteAllText("CommTowerDatabase.json", jsonString);

//DefectDetectorList defectDetectorList = DefectDetectorList.Read(@"E:\Program Files\Run8Studios\Run8 Train Simulator V3\Content\V3Routes\CSX_ALine\DefectDetectorList.r8");
//jsonString = JsonConvert.SerializeObject(defectDetectorList, options);
//File.WriteAllText("DefectDetectorList.json", jsonString);

// test writing defect detector list back to r8
//Console.WriteLine("Writing...");
//defectDetectorList.Write("DefectDetectorList.r8");

//DispatcherLightBlockDatabase dispatcherLightBlockDatabase = DispatcherLightBlockDatabase.Read(@"E:\Program Files\Run8Studios\Run8 Train Simulator V3\Content\V3Routes\BNSF_MojaveSub\DispatcherBlockLightDatabase.r8");
//jsonString = JsonConvert.SerializeObject(dispatcherLightBlockDatabase, options);
//File.WriteAllText("DispatcherLightBlockDatabase.json", jsonString);

//DispatcherSignalControllerDatabase dispatcherSignalControllerDatabase = DispatcherSignalControllerDatabase.Read(@"E:\Program Files\Run8Studios\Run8 Train Simulator V3\Content\V3Routes\BNSF_MojaveSub\DispatcherSignalControllerDatabase.r8");
//jsonString = JsonConvert.SerializeObject(dispatcherSignalControllerDatabase, options);
//File.WriteAllText("DispatcherSignalControllerDatabase.json", jsonString);

//DispatcherSwitchIconDatabase dispatchSwitchIconDatabase = DispatcherSwitchIconDatabase.Read(@"E:\Program Files\Run8Studios\Run8 Train Simulator V3\Content\V3Routes\BNSF_MojaveSub\DispatcherSwitchIconDatabase.r8");
//jsonString = JsonConvert.SerializeObject(dispatchSwitchIconDatabase, options);
//File.WriteAllText("DispatcherSwitchIconDatabase.json", jsonString);

//RoadDatabase roadDatabase = RoadDatabase.Read(@"E:\Program Files\Run8Studios\Run8 Train Simulator V3\Content\V3Routes\BNSF_MojaveSub\RoadDatabase.r8");
//jsonString = JsonConvert.SerializeObject(roadDatabase, options);
//File.WriteAllText("RoadDatabase.json", jsonString);

//HumpControllerList humpControllerList = HumpControllerList.Read(@"E:\Program Files\Run8Studios\Run8 Train Simulator V3\Content\V3Routes\SP-UP_RosevilleSub\HumpControllerList.r8");
//jsonString = JsonConvert.SerializeObject(humpControllerList, options);
//File.WriteAllText("HumpControllerList.json", jsonString);

//HumpConfigDatabase hump = HumpConfigDatabase.Read(@"E:\Program Files\Run8Studios\Run8 Train Simulator V3\Content\V3Routes\Regions\\NorthernCA\Hump.r8");
//jsonString = JsonConvert.SerializeObject(hump, options);
//File.WriteAllText("Hump.json", jsonString);

//Texture.Decrypt(@"E:\Program Files\Run8Studios\Run8 Train Simulator V3\Content\V3RailVehicles\Body\Run8_ES44DC_CabNS.tx8");

//TerrainTile tile = Terrain.Read(@"D:\Programs\Run8Studios\V2\Content\V2Routes\Regions\SouthernCA\TerrainTiles\00005_00048.tr2");
//jsonString = JsonConvert.SerializeObject(tile, options);
//File.WriteAllText("TerrainTile.json", jsonString);

Model model = Model.Read(@"E:\Program Files\Run8Studios\Run8 Train Simulator V3\Content\V3RailVehicles\Body\R8_MP15DC_CSX01.rn8");
foreach(ModelObject obj in model.Objects)
{
    obj.Vertices.Clear();
    obj.Indices = new int[] { };
}
jsonString = JsonConvert.SerializeObject(model, options);
File.WriteAllText("Model.json", jsonString);

//TileScenery scenery = TileScenery.Read(@"D:\Programs\Run8Studios\V2\Content\V2Routes\Regions\SouthernCA\TileScenery\00000_00037.rn8");
//jsonString = JsonConvert.SerializeObject(scenery, options);
//File.WriteAllText("TileScenery.json", jsonString);

//COLLADA model = new COLLADA();
//int[] positions = new int[] { -1, -1, -1, 1, -1, -1, 1, 1, -1, -1, 1, -1, -1, -1, 1, 1, -1, 1, 1, 1, 1, -1, 1, 1 };
//int[] normals = new int[] { -1, 0, 0, 0, 1, 0, 1, 0, 0, 0, -1, 0, 0, 0, -1, 0, 0, 1 };
//int[] indices = new int[] { 0, 1, 2, 0, 2, 3, 4, 5, 6, 4, 6, 7, 0, 1, 5, 0, 5, 4, 2, 3, 7, 2, 7, 6, 1, 2, 6, 1, 6, 5, 0, 3, 7, 0, 7, 4 };

//ColladaGeometry geoBuilder = new ColladaGeometry("Cube", "Cube");

//// create the sources
//source pos_source = ColladaUtils.CreateSource(geoBuilder.Id, ColladaArrayType.Positions, positions);
//source normals_source = ColladaUtils.CreateSource(geoBuilder.Id, ColladaArrayType.Normals, normals);

//// add the sources to the geometry
//geoBuilder.AddSources(new source[] { pos_source, normals_source });

//// add triangle input offsets
//geoBuilder.AddTriangleInput(ColladaSemantic.Vertex);
//geoBuilder.AddTriangleInput(ColladaSemantic.Normal);

//// add triangle
//geoBuilder.AddTriangles(indices);

//// build the final geometry object
//geometry geo = geoBuilder.ToGeometry();

//library_geometries lgeo = new library_geometries()
//{
//    geometry = new geometry[] { geo }
//};

//matrix node_matrix = new matrix()
//{
//    sid = "transform",
//    Values = new double[] { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1 }
//};

//instance_geometry node_geo = new instance_geometry()
//{
//    url = string.Format("#{0}", geoBuilder.Id),
//};

//node scene_node = new node()
//{
//    id = geoBuilder.Id,
//    name = geoBuilder.Name,
//    Items = new object[] {node_matrix},
//    ItemsElementName = new ItemsChoiceType2[] {ItemsChoiceType2.matrix},
//    instance_geometry = new instance_geometry[] {node_geo}
//};

//visual_scene vscene = new visual_scene()
//{
//    id = "Scene",
//    name = "Scene",
//    node = new node[] {scene_node}
//};

//library_visual_scenes lscene = new library_visual_scenes() { visual_scene = new visual_scene[] { vscene } };

//COLLADAScene scene_obj = new COLLADAScene()
//{
//    instance_visual_scene = new InstanceWithExtra()
//    {
//        url = "#Scene"
//    }
//};

//model.Items = new object[] { lgeo, lscene };
//model.scene = scene_obj;

//model.Save("test.dae");