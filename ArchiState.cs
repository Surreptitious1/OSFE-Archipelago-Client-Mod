using System;
using System.Collections.Generic;

namespace ArchipelagoClient {
    /**
     * https://csharpindepth.com/articles/Singleton
     */
    public class AP
    {
        // Items
        public List<String> unlocked_chars = new List<string>();
        public List<String> unlocked_spells = new List<string>();
        
        // Locations
        public List<String> boss_spares = new List<string>();
        public List<String> boss_kills = new List<string>();

        // Run controller contains all other relevant controllers
        public RunCtrl RC = null; 
        public bool in_run = false; // Refers to OSFE run, not an Archipelago run
        public ArchiNet ArchiNet = null;
        
        private static readonly object initlock = new object();
        private static AP _AP = null;
         
        private AP()
        {
            //this.ArchiNet = ArchiNet.I; // Force initialization
            // TODO: Pull unlocked chars from server state
            unlocked_chars.Add("Saffron");
            unlocked_chars.Add("Shiso");
            // TODO: Pull unlocked cards from server state
            unlocked_spells.Add("Thunder");
            unlocked_spells.Add("KineticWave");
        }

        public static AP I
        {
            get
            {
                lock (initlock)
                {
                    if (_AP == null)
                    {
                        _AP = new AP();
                    } 
                    return _AP;
                }
            }
        }

        public void StartRun(RunCtrl runCtrl)
        {
            this.RC = runCtrl;
            this.in_run = true;
        }

        public void EndRun()
        {
            this.in_run = false;
            this.RC = null;
        }
    }
}