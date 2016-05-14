using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BaseLib
{
    public static class SysCons
    {
        public static void SavePacket(byte[] data, int opcode)
        {
            string path = @".\packets\";
            string filename = String.Format("packet-{0}.dat", opcode);
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                FileStream fs = new FileStream(path + filename, FileMode.OpenOrCreate);
                fs.Write(data, 0, data.Length);
                fs.Close();
            }
            catch(Exception ex)
            {
                SysCons.WriteLine("Exception: {0}", ex);
            }
        }

        public static void WriteLine(string text, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("[{0:D02}/{1:D02}/{2:D04} - {3:D02}:{4:D02}:{5:D02}] ",
                DateTime.Now.Day,
                DateTime.Now.Month,
                DateTime.Now.Year,
                DateTime.Now.Hour,
                DateTime.Now.Minute,
                DateTime.Now.Second);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("# ");
            Console.ForegroundColor = ConsoleColor.Gray;
            if (args.Length == 0)
                Console.WriteLine(text);
            else
                Console.WriteLine(text, args);
        }

        public static void Write(string text, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("[{0:D02}/{1:D02}/{2:D04} - {3:D02}:{4:D02}:{5:D02}] ",
                DateTime.Now.Day,
                DateTime.Now.Month,
                DateTime.Now.Year,
                DateTime.Now.Hour,
                DateTime.Now.Minute,
                DateTime.Now.Second);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("# ");
            Console.ForegroundColor = ConsoleColor.Gray;
            if (args.Length == 0)
                Console.Write(text);
            else
                Console.Write(text, args);
        }
    }
}
