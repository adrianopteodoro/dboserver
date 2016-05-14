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
using BaseLib.Network;
using BaseLib.Packets;
using BaseLib.Helpers;
using AuthServer.Network;

namespace AuthServer
{
	class Program
	{
        public static AuthServ AuthServ;
        public static Thread AuthServThread;
        
		public static void Main(string[] args)
		{
            // Watch for unhandled exceptions
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler;

            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture; // Use invariant culture - we have to set it explicitly for every thread we create.
			
			StartAuth();
			
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
		
		public static bool StartAuth()
        {
            if (AuthServ != null) return false;

            AuthServ = new AuthServ();
            AuthServThread = new Thread(AuthServ.Run) { IsBackground = true, CurrentCulture = CultureInfo.InvariantCulture };
            AuthServThread.Start();

            return true;
        }

        public static bool StopAuth()
        {
            if (AuthServ == null) return false;

            SysCons.WriteLine("Stopping AuthServer..");
            AuthServ.Shutdown();
            AuthServThread.Abort();
            AuthServ = null;

            return true;
        }
        
        public static void Shutdown()
        {
            if (AuthServ != null)
            {
                SysCons.WriteLine("Shutting down AuthServer..");
                AuthServ.Shutdown();
            }
            Environment.Exit(0);
        }
	}
}
