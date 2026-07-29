using System;
using System.Collections.Generic;

namespace ArchipelagoClient {
    /**
     * https://csharpindepth.com/articles/Singleton
     */
    public class AP
    {
        public List<String> unlocked_chars = new List<string>();

        private static readonly object objlock = new object();
        private static AP _AP = null;

        AP()
        {
            // TODO: Pull unlocked chars from server state
            unlocked_chars.Add("Saffron");
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