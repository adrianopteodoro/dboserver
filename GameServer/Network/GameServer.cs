using System;
using BaseLib;
using BaseLib.Network;
using GameServer.Configs;
using GameServer.Packets;

namespace GameServer.Network
{
    /// <summary>
    /// Description of GameServer.
    /// </summary>
    public class GameServ : Server
	{
		public GameServ()
		{
			this.OnConnect += GameServer_OnConnect;
            this.OnDisconnect += GameServer_OnDisconnect;
            this.DataReceived += GameServer_OnDataReceived;
		}

        private void GameServer_OnConnect(object sender, ClientEventArgs e)
        {
            SysCons.LogInfo("Client connected: {0}", e.Client.ToString());
            e.Client.User = new GameClient(e.Client);
            byte[] rawData = { 0x06, 0x00, 0x03, 0x00, 0x30, 0x2C, 0x67, 0x4C };
            if (e.Client.IsConnected) e.Client.Send(rawData);
        }

        private void GameServer_OnDisconnect(object sender, ClientEventArgs e)
        {
            GameClient client = ((GameClient) e.Client.User);
            SysCons.LogInfo("Client disconnected: {0}", e.Client.ToString());
        }

        private void GameServer_OnDataReceived(object sender, ClientEventArgs e, byte[] data)
        {
            PacketParser parser = new PacketParser();
            parser.CheckPacket(data, (GameClient)e.Client.User);
        }
		
		public override void Run()
        {
            Console.Title = "DBO Game Server";
            if (!this.Listen(GameConfig.Instance.BindIP, GameConfig.Instance.Port)) return;
            SysCons.LogInfo("GameServer is listening on {0}:{1}...", GameConfig.Instance.BindIP, GameConfig.Instance.Port);
        }
	}
}
