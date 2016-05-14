/*
 * Criado por SharpDevelop.
 * Usuário: Adriano
 * Data: 30/11/2011
 * Hora: 20:14
 * 
 * Para alterar este modelo use Ferramentas | Opções | Codificação | Editar Cabeçalhos Padrão.
 */
using System;
using System.Globalization;
using System.Reflection;
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
                SysCons.WriteLine("Terminating because of unhandled exception.");
            else
                SysCons.WriteLine("Caught unhandled exception.");
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

            SysCons.WriteLine("Stopping GameServer..");
            GServ.Shutdown();
            GameServerThread.Abort();
            GServ = null;

            return true;
        }
        
        public static void Shutdown()
        {
            if (GServ != null)
            {
                SysCons.WriteLine("Shutting down GameServer..");
                GServ.Shutdown();
            }
            Environment.Exit(0);
        }
	}
}
