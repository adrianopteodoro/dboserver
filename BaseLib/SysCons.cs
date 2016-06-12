using System;
using System.IO;
using BaseLib.Packets;

namespace BaseLib
{
    public static class SysCons
    {
        public static void SavePacket(Packet pkt)
        {
            string path = @".\packets\";
            string filename = String.Format(
                "{0}_{1}.dat",
                PacketDefinitions.getPacketName(pkt.Opcode),
                DateTime.Now.ToString(@"MM-dd-yyyy_HH-mm-ss")
            );
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                FileStream fs = new FileStream(path + filename, FileMode.OpenOrCreate);
                if (pkt.Data.Length < pkt.Lenght)
                {
                    fs.Write(pkt.Data, 0, pkt.Data.Length);
                }
                else
                {
                    fs.Write(pkt.Data, 0, pkt.Lenght);
                }
                fs.Close();
            }
            catch(Exception ex)
            {
                SysCons.LogError("Exception: {0}", ex);
            }
        }

        public static void LogInfo(string text, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("[{0}] ", DateTime.Now.ToString(@"MM/dd/yyyy HH:mm:ss"));
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("[INFO] ");
            Console.ForegroundColor = ConsoleColor.Gray;
            if (args.Length == 0)
                Console.WriteLine(text);
            else
                Console.WriteLine(text, args);
        }

        public static void LogWarn(string text, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("[{0}] ", DateTime.Now.ToString(@"MM/dd/yyyy HH:mm:ss"));
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("[WARN] ");
            Console.ForegroundColor = ConsoleColor.Gray;
            if (args.Length == 0)
                Console.WriteLine(text);
            else
                Console.WriteLine(text, args);
        }

        public static void LogError(string text, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("[{0}] ", DateTime.Now.ToString(@"MM/dd/yyyy HH:mm:ss"));
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[ERROR] ");
            Console.ForegroundColor = ConsoleColor.Gray;
            if (args.Length == 0)
                Console.WriteLine(text);
            else
                Console.WriteLine(text, args);
        }
    }
}
