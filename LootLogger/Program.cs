using PcapDotNet.Core;
using PcapDotNet.Packets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace SUNLootLogger
{

    public class Program
    {
        static void Main(string[] args)
        {
            Config config = Config.instance;
            ConsoleKeyInfo cki;
            LootLogger logger = null;
            try
            {
                logger = new LootLogger();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.StackTrace);
            }
            while (true)
            {
                cki = Console.ReadKey(true);
                if (cki.Key == ConsoleKey.S)
                {
                    Console.WriteLine("Saving All player loots to a new file !");
                    logger?.SaveLootsToFile();
                    Console.WriteLine("Loots saved !");
                }
                else if (cki.Key == ConsoleKey.U)
                {
                    logger?.UploadLoots();
                }
            }
        }
    }

    public class LootLogger
    {
        public LootLogger()
        {
            LootService = new LootService();
            EventHandler = new PacketHandler(LootService);
            PhotonPacketHandler = new PhotonPacketHandler(EventHandler);

            new Thread(delegate ()
            {
                CreateListener();
            }).Start();

            Console.WriteLine(Strings.WelcomeMessage);
        }

        private PacketHandler EventHandler { get; set; }
        private ILootService LootService { get; set; }
        internal PhotonPacketHandler PhotonPacketHandler { get; set; }

        public void SaveLootsToFile()
        {
            LootService.SaveLootsToFile();
        }

        public void UploadLoots()
        {
            LootService.UploadLoots();
        }

        private void CreateListener()
        {
            IList<LivePacketDevice> allDevices = LivePacketDevice.AllLocalMachine;
            if (allDevices.Count == 0)
            {
                Debug.WriteLine("No Network Interface Found! Please make sure WinPcap is properly installed.");
                return;
            }
            for (int i = 0; i != allDevices.Count; i++)
            {
                LivePacketDevice device = allDevices[i];
                if (device.Description != null)
                {
                    Debug.WriteLine(" (" + device.Description + ")");
                }
                else
                {
                    Debug.WriteLine(" (Unknown)");
                }
            }
            using (List<LivePacketDevice>.Enumerator enumerator = allDevices.ToList().GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    PacketDevice selectedDevice = enumerator.Current;
                    new Thread(delegate()
                    {
                        using (PacketCommunicator communicator = selectedDevice.Open(65536, PacketDeviceOpenAttributes.Promiscuous, 1000))
                        {
                            try
                            {
                                if (communicator.DataLink.Kind != DataLinkKind.Ethernet)
                                {
                                    Debug.WriteLine("This program works only on Ethernet networks.");
                                }
                                else
                                {
                                    using (BerkeleyPacketFilter filter = communicator.CreateFilter("ip and udp"))
                                    {
                                        communicator.SetFilter(filter);
                                    }
                                    Console.WriteLine("Capturing on " + selectedDevice.Description + "...");
                                    communicator.ReceivePackets(0, new HandlePacket(PhotonPacketHandler.PacketHandler));
                                }
                            } catch (NotSupportedException)
                            {

                            }
                           
                        }
                    }).Start();
                }
            }
        }
    }
}
