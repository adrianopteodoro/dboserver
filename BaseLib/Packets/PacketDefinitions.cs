using System;

namespace BaseLib.Packets
{
    public class PacketDefinitions
    {
        public static string getPacketName(ushort opcode) {
            if (PacketDefinitions.IsDefined(opcode))
            {
                return ((PacketOpcodes)opcode).ToString();
            }
            else {
                return String.Format("UNKNOW_PACKET_{0}", opcode);
            }
        }

        public static bool IsDefined(ushort opcode) {
            return Enum.IsDefined(typeof(PacketOpcodes), opcode);
        }

        public static void LogPacketData(Packet pkt) {
            SysCons.LogWarn("Recv Packet({0}) Len({1}) Enc({2}) Opcode({3})", PacketDefinitions.getPacketName(pkt.Opcode), pkt.Lenght, pkt.Encrypt, pkt.Opcode);
            SysCons.SavePacket(pkt);
        }
    }
}
