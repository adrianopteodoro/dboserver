using System;
using BaseLib;
using BaseLib.Network;
using CommunityServer.Packets;
using CommunityServer.Configs;

namespace CommunityServer.Network
{
    /// <summary>
    /// Description of CommunityServer.
    /// </summary>
    public class CommServ : Server
	{
		public CommServ()
		{
			this.OnConnect += CommunityServer_OnConnect;
            this.OnDisconnect += CommunityServer_OnDisconnect;
            this.DataReceived += CommunityServer_OnDataReceived;
		}

        private void CommunityServer_OnConnect(object sender, ClientEventArgs e)
        {
            SysCons.LogInfo("Client connected: {0}", e.Client.ToString());
            e.Client.User = new CommClient(e.Client);
            byte[] rawData = { 0x06, 0x00, 0x03, 0x00, 0x30, 0x2C, 0x67, 0x4C };
            if (e.Client.IsConnected) e.Client.Send(rawData);
        }

        private void CommunityServer_OnDisconnect(object sender, ClientEventArgs e)
        {
            CommClient client = ((CommClient) e.Client.User);
            SysCons.LogInfo("Client disconnected: {0}", e.Client.ToString());
        }

        private void CommunityServer_OnDataReceived(object sender, ClientEventArgs e, byte[] data)
        {
            PacketParser parser = new PacketParser();
            parser.CheckPacket(data, (CommClient)e.Client.User);
        }
		
		public override void Run()
        {
            Console.Title = "DBO Community Server";
            if (!this.Listen(CommConfig.Instance.BindIP, CommConfig.Instance.Port)) return;
            SysCons.LogInfo("CommunityServer is listening on {0}:{1}...", CommConfig.Instance.BindIP, CommConfig.Instance.Port);
        }
	}
}
