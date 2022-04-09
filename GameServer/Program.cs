using System;
using System.Globalization;
using System.Threading;
using BaseLib;
using GameServer.Network;

namespace GameServer
{
    class Program
	{
        public static GameServ GServ;
        public static Thread GameServerThread;
        
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
            if (GServ != null) return false;

            GServ = new GameServ();
            GameServerThread = new Thread(GServ.Run) { IsBackground = true, CurrentCulture = CultureInfo.InvariantCulture };
            GameServerThread.Start();

            return true;
        }

        public static bool StopServer()
        {
            if (GServ == null) return false;

            SysCons.LogInfo("Stopping GameServer..");
            GServ.Shutdown();
            GameServerThread.Interrupt();
            GServ = null;

            return true;
        }
        
        public static void Shutdown()
        {
            if (GServ != null)
            {
                SysCons.LogInfo("Shutting down GameServer..");
                GServ.Shutdown();
            }
            Environment.Exit(0);
        }
	}
}
