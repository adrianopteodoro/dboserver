using System;
using System.Globalization;
using System.Threading;
using BaseLib;
using CharServer.Network;

namespace CharServer
{
    class Program
    {
        public static CharServ Serv;
        public static Thread LobbyServerThread;

        public static void Main(string[] args)
        {
            // Watch for unhandled exceptions
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler;

            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture; // Use invariant culture - we have to set it explicitly for every thread we create.

            StartServer();

            while (true)
            {
                Console.ReadKey(true);
            }
        }

        private static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.IsTerminating)
                SysCons.LogError("Terminating because of unhandled exception.");
            else
                SysCons.LogError("Caught unhandled exception.");
            Console.ReadLine();
        }

        public static bool StartServer()
        {
            if (Serv != null) return false;

            Serv = new CharServ();
            LobbyServerThread = new Thread(Serv.Run) { IsBackground = true, CurrentCulture = CultureInfo.InvariantCulture };
            LobbyServerThread.Start();

            return true;
        }

        public static bool StopServer()
        {
            if (Serv == null) return false;

            SysCons.LogInfo("Stopping CharServer..");
            Serv.Shutdown();
            LobbyServerThread.Interrupt();
            Serv = null;

            return true;
        }

        public static void Shutdown()
        {
            if (Serv != null)
            {
                SysCons.LogInfo("Shutting down CharServer..");
                Serv.Shutdown();
            }
            Environment.Exit(0);
        }
    }
}
