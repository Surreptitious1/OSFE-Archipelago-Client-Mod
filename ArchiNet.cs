using System;
using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.Enums;
using Archipelago.MultiClient.Net.Helpers;
using Archipelago.MultiClient.Net.Models;
using UnityEngine;

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

        private void SessionInit(String url)
        {
            if (session == null)
            {
                session = ArchipelagoSessionFactory.CreateSession(url);
                session.Items.ItemReceived += OnItemReceived;
                session.Socket.SocketClosed += OnSocketClosed;
            } 
        }

        private void OnItemReceived(ReceivedItemsHelper helper)
        {
            var item = helper.AllItemsReceived[helper.Index - 1];
            AP.I.ReceiveItem(item);
        }

        private void OnSocketClosed(string reason)
        {
            session = null;
            Debug.Log("Socket closed: " + reason);
            // TODO: display connection failure to user (prompt to reconnect)
            //       (first pass just boot back to main menu?)
        }

        public void SendLocation(string name)
        {
            var id = session.Locations.GetLocationIdFromName(session.ConnectionInfo.Game, name);
            if (id == -1)
            {
                Debug.Log($"Failed to send location {id} ({name})");
            }

            Debug.Log($"sending {name}");
            session.Locations.CompleteLocationChecks(id);
            if (AP.I.IsGoaled())
            {
                session.SetGoalAchieved();
            }
        }

        // Fetch server-side information and initialize AP state
        private void SessionConnected()
        {
            AP.I.SetSlotData(session.DataStorage.GetSlotData());
            /* just let the server handle location id tracking and repeat send any dupes
            foreach (var location in session.Locations.AllLocationsChecked)
            {
                AP.I.RestoreStateFromLocation(session.Locations.GetLocationNameFromId(location));
            }
            */
            foreach (var item in session.Items.AllItemsReceived)
            {
                AP.I.ReceiveItem(item);
            }

            // Just in case
            if (AP.I.IsGoaled())
            {
                session.SetGoalAchieved();
            }
        }

        public bool Connect(String url, String slot_name)
        {
            SessionInit(url);
            var res = session.TryConnectAndLogin(
                "One Step From Eden",
                slot_name,
                ItemsHandlingFlags.IncludeStartingInventory
                );
            if (res.Successful)
            {
                SessionConnected();
            }
            else
            {
                session = null;
            }
            return res.Successful;
        }

        private void ReceiveItem(ItemInfo item)
        {
            
        }
    }
}
