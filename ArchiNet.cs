using System;
using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.Enums;

namespace ArchipelagoClient
{
    // OSFE tries to load any ".dll" files in the mod's directory as harmony
    // patches, so we reflectively load the multiclient from a file instead
    // TODO: ^ that
    public class ArchiNet
    {
        private static readonly object initlock = new object();
        private static ArchiNet _AN = null;

        public ArchipelagoSession session = null;

        private ArchiNet()
        {
        }

        public static ArchiNet I
        {
            get
            {
                lock (initlock)
                {
                    if (_AN == null)
                    {
                        _AN = new ArchiNet();
                    }
                    return _AN;
                }
            }
        }

        public bool Connect(String url, String slot_name)
        {
            session = ArchipelagoSessionFactory.CreateSession(url);
            var res = session.TryConnectAndLogin(
                "One Step From Eden",
                slot_name,
                ItemsHandlingFlags.IncludeStartingInventory
                );
            return res.Successful;
        }
    }
}
