using LibRun8.Formats;

Signal signal = Signal.Read(@"C:\Run8Studios\Run8 Train Simulator V3\Content\Signals\HRS_Dwarf_3L_NewSystem.sig");
Console.WriteLine("Signal Name: {0}; Entry Count: {1}; Entries Length: {2}", signal.Name, signal.EntryCount, signal.Entries.Length);
