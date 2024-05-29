using System.Text;

namespace R8Explorer
{
    public class FileFilterManager
    {
        public struct FileFilter
        {
            public string Description { get; set; }
            public string Extension { get; set; }
            public Run8Version[] Versions { get; set; }
        }

        // holds a list of file filters
        public static FileFilter[] FileFilters = new FileFilter[]
        {
            new FileFilter { Description = "TR4 Terrain Tile", Extension = "*.tr4", Versions = new Run8Version[]{ Run8Version.V3 } },
            new FileFilter { Description = "TR3 Terrain Tile", Extension = "*.tr3", Versions = new Run8Version[]{ Run8Version.V2 } },
            new FileFilter { Description = "TR2 Terrain Tile", Extension = "*.tr2", Versions = new Run8Version[]{ Run8Version.Any } },
            new FileFilter { Description = "TerraGen Terrain Tile", Extension = "*.ter", Versions = new Run8Version[]{ Run8Version.V2 } },
            new FileFilter { Description = "AI Signal Database", Extension = "AISignalDatabase.r8", Versions = new Run8Version[]{ Run8Version.Any } },
            new FileFilter { Description = "AI Special Locations Database", Extension = "AISpecialLocations.r8", Versions = new Run8Version[]{ Run8Version.Any } },
            new FileFilter { Description = "AI Track Speed Database", Extension = "AITrackSpeed.r8", Versions = new Run8Version[]{ Run8Version.Any } },
            // theres no way to omit these from the 3D Model filter
            //new FileFilter { Description = "Avatar", Extension = "Brian.rn8;Chris.rn8;Pablo.rn8", Versions = new Run8Version[]{ Run8Version.Any } },
            new FileFilter { Description = "Block Detector Database", Extension = "BlockDetectorDatabase.r8", Versions = new Run8Version[]{ Run8Version.Any } },
            new FileFilter { Description = "Car Spewer Database", Extension = "CarSpewerDatabase.r8", Versions = new Run8Version[]{ Run8Version.Any } },
            new FileFilter { Description = "Comm Tower Database", Extension = "CommTowerDatabase.r8", Versions = new Run8Version[]{ Run8Version.Any } },
            new FileFilter { Description = "Defect Detector Database", Extension = "DefectDetectorList.r8", Versions = new Run8Version[]{ Run8Version.Any } },
            new FileFilter { Description = "Dispatcher Block Light Database", Extension = "DispatcherBlockLightDatabase.r8", Versions = new Run8Version[]{ Run8Version.Any } },
            new FileFilter { Description = "Dispatcher Signal Controller Database", Extension = "DispatcherSignalControllerDatabase.r8", Versions = new Run8Version[]{ Run8Version.Any } },
            new FileFilter { Description = "Dispatcher Switch Icon Database", Extension = "DispatcherSwitchIconDatabase.r8", Versions = new Run8Version[]{ Run8Version.Any } },
            new FileFilter { Description = "Horn & Bell Configuration", Extension = "HornBellConfiguration.r8", Versions = new Run8Version[]{ Run8Version.Any } },
            new FileFilter { Description = "Hump Configuration", Extension = "Hump.r8", Versions = new Run8Version[]{ Run8Version.Any } },
            new FileFilter { Description = "Hump Controller List", Extension = "HumpControllerList.r8", Versions = new Run8Version[]{ Run8Version.Any } },
            new FileFilter { Description = "Industry Configuration", Extension = "*.ind", Versions = new Run8Version[]{ Run8Version.Any } },
            new FileFilter { Description = "Milepost Database", Extension = "MilepostDatabase.r8", Versions = new Run8Version[]{ Run8Version.Any } },
            new FileFilter { Description = "3D Model", Extension = "*.rn8", Versions = new Run8Version[]{ Run8Version.Any } },
            new FileFilter { Description = "Road Database", Extension = "RoadDatabase.r8", Versions = new Run8Version[]{ Run8Version.Any } },
            new FileFilter { Description = "Signal", Extension = "*.sig", Versions = new Run8Version[]{ Run8Version.Any } },
            new FileFilter { Description = "Star4 Database", Extension = "stars4.r8", Versions = new Run8Version[]{ Run8Version.Any } },
            new FileFilter { Description = "Track Database", Extension = "TrackDatabase.r8", Versions = new Run8Version[]{ Run8Version.Any } },
            new FileFilter { Description = "Traffic Database", Extension = "Traffic.r8", Versions = new Run8Version[]{ Run8Version.Any } },
            new FileFilter { Description = "Crossing Gate", Extension = "*.xng", Versions = new Run8Version[]{ Run8Version.Any } },
            new FileFilter { Description = "Key Settings", Extension = "Run8KeySettings.r8", Versions = new Run8Version[]{ Run8Version.Any } },
            new FileFilter { Description = "Settings", Extension = "Run8Settings.r8", Versions = new Run8Version[]{ Run8Version.Any } },
        };

        // Constructs the filter string for the OpenFileDialog
        public static string GetOpenFileDialogFilter()
        {
            StringBuilder sb = new StringBuilder();

            // add individual file filters
            foreach (FileFilter filter in FileFilters)
            {
                sb.Append($"{filter.Description}|{filter.Extension}|");
            }

            //// add version specific formats
            //foreach (Run8Version version in Enum.GetValues(typeof(Run8Version)))
            //{
            //    // skip the any version
            //    if (version == Run8Version.Any)
            //        continue;

            //    // get all file filters for this version
            //    List<FileFilter> versionFilters = FileFilters.Where(f => f.Versions.Contains(version) || f.Versions.Contains(Run8Version.Any)).ToList();

            //    sb.Append($"All {version} Files|");
            //    foreach (FileFilter filter in versionFilters)
            //    {
            //        sb.Append($"{filter.Extension};");
            //    }
            //    sb.Remove(sb.Length - 1, 1); // remove the last semicolon
            //    sb.Append("|");
            //}

            // add final "All Run8 Files" filter, used to display all files for any version
            sb.Append("All Run8 Files|");
            foreach (FileFilter filter in FileFilters)
            {
                sb.Append($"{filter.Extension};");
            }

            sb.Remove(sb.Length - 1, 1); // remove the last semicolon

            return sb.ToString();
        }
    }
}
