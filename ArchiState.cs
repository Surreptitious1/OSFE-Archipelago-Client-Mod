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

        
        private static readonly object objlock = new object();
        private static AP _AP = null;

        AP()
        {
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
                lock (objlock)
                {
                    if (_AP == null)
                    {
                        _AP = new AP();
                    }

                    return _AP;
                }
            }
        }
    }
}