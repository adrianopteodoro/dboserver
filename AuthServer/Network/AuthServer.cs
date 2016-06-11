using System;
using BaseLib;
using BaseLib.Network;
using AuthServer.Configs;
using AuthServer.Packets;

namespace AuthServer.Network
{
    /// <summary>
    /// Description of AuthServer.
    /// </summary>
    public class AuthServ : Server
	{
		public AuthServ()
		{
			this.OnConnect += AuthServ_OnConnect;
            this.OnDisconnect += AuthServ_OnDisconnect;
            this.DataReceived += AuthServ_OnDataReceived;
		}

        private void AuthServ_OnConnect(object sender, ClientEventArgs e)
        {
            SysCons.LogInfo("Client connected: {0}", e.Client.ToString());
            e.Client.User = new AuthClient(e.Client);
            byte[] rawData = { 0x06, 0x00, 0x03, 0x00, 0x30, 0x2C, 0x67, 0x4C };
            if (e.Client.IsConnected) e.Client.Send(rawData);
        }

        private void AuthServ_OnDisconnect(object sender, ClientEventArgs e)
        {
            AuthClient client = ((AuthClient) e.Client.User);
            SysCons.LogInfo("Client disconnected: {0}", e.Client.ToString());
        }

        private void AuthServ_OnDataReceived(object sender, ClientEventArgs e, byte[] data)
        {
            PacketParser parser = new PacketParser();
            parser.CheckPacket(data, (AuthClient)e.Client.User);
        }
		
		public override void Run()
        {
            Console.Title = "DBO Auth Server";
            if (!this.Listen(AuthConfig.Instance.BindIP, AuthConfig.Instance.Port)) return;
            SysCons.LogInfo("AuthServer is listening on {0}:{1}...", AuthConfig.Instance.BindIP, AuthConfig.Instance.Port);
        }
	}
}
