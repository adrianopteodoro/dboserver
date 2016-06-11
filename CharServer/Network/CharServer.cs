using System;
using BaseLib;
using BaseLib.Network;
using CharServer.Configs;
using CharServer.Packets;

namespace CharServer.Network
{
    public class CharServ : Server
    {
        public CharServ()
		{
			this.OnConnect += LobbyServer_OnConnect;
            this.OnDisconnect += LobbyServer_OnDisconnect;
            this.DataReceived += LobbyServer_OnDataReceived;
		}

        private void LobbyServer_OnConnect(object sender, ClientEventArgs e)
        {
            SysCons.LogInfo("Client connected: {0}", e.Client.ToString());
            e.Client.User = new CharClient(e.Client);
            byte[] rawData = { 0x06, 0x00, 0x03, 0x00, 0x30, 0x2C, 0x67, 0x4C };
            if (e.Client.IsConnected) e.Client.Send(rawData);
        }

        private void LobbyServer_OnDisconnect(object sender, ClientEventArgs e)
        {
            CharClient client = ((CharClient) e.Client.User);
            SysCons.LogInfo("Client disconnected: {0}", e.Client.ToString());
        }

        private void LobbyServer_OnDataReceived(object sender, ClientEventArgs e, byte[] data)
        {
            PacketParser parser = new PacketParser();
            parser.CheckPacket(data, (CharClient)e.Client.User);
        }
		
		public override void Run()
        {
            Console.Title = "DBO Char Server";
            if (!this.Listen(CharConfig.Instance.BindIP, CharConfig.Instance.Port)) return;
            SysCons.LogInfo("CharServer is listening on {0}:{1}...", CharConfig.Instance.BindIP, CharConfig.Instance.Port);
        }
    }
}
