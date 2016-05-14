using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using BaseLib.Packets;

namespace BaseLib.Network
{
    public class Encryption
    {
        private uint RECV_RANDOM_SEED = 0x42000EBE;
        private uint SEND_RANDOM_SEED = 0x42000ABE;
        private RandomGenerator recvKeyGenerator;
        private RandomGenerator sendKeyGenerator;

        public void setSeeds(uint seed1, uint seed2)
        {
            this.sendKeyGenerator = new RandomGenerator(seed1);
            this.recvKeyGenerator = new RandomGenerator(seed2);
        }

        public Encryption(bool Swap)
        {
            if(Swap)
            {
                this.recvKeyGenerator = new RandomGenerator(RECV_RANDOM_SEED);
                this.sendKeyGenerator = new RandomGenerator(SEND_RANDOM_SEED);
            }
            else
            {
                this.recvKeyGenerator = new RandomGenerator(SEND_RANDOM_SEED);
                this.sendKeyGenerator = new RandomGenerator(RECV_RANDOM_SEED);
            }
        }

        public int TxEncrypt(ref Packet pkt)
        {
            Encrypt(ref pkt, pkt.Data.Length, sendKeyGenerator.Generate());
            return 0;
        }
        public int RxDecrypt(ref Packet pkt)
        {
	        Decrypt(ref pkt, pkt.Data.Length, recvKeyGenerator.Generate());
	        return 0;
        }

        private int Encrypt(ref Packet pkt, int Length, uint CipherKey)
        {
            if (pkt == null)
                return 0;

            int pOff = 2, pLen = Length - pOff;
            byte[] outdata = pkt.Data;
            byte[] Plain = new byte[pLen];
            Buffer.BlockCopy(outdata, pOff, Plain, 0, pLen);

            int Off = 0;

            int Round = pLen / sizeof(uint);
            for (int i = 0; i < Round; i++)
            {
                uint RoundPlain = BitConverter.ToUInt32(Plain, Off);
                RoundPlain ^= CipherKey;
                Buffer.BlockCopy(BitConverter.GetBytes(RoundPlain), 0, Plain, Off, sizeof(uint));
                Off += sizeof(uint);
            }

            int Rest = pLen % sizeof(uint);
            for (int i = 0; i < Rest; i++)
            {
                byte RestPlain = Plain[Off];
                RestPlain ^= (byte)CipherKey;
                Plain[Off] = RestPlain;
                Off++;
            }
            Buffer.BlockCopy(Plain, 0, outdata, pOff, pLen);
            pkt.SetData(outdata);
            return Length;
        }

        private int Decrypt(ref Packet pkt, int Length, uint CipherKey)
        {
            if (pkt == null)
                return 0;

            int pOff = 2, pLen = Length - pOff;
            byte[] outdata = pkt.Data;
            byte[] Plain = new byte[pLen];
            Buffer.BlockCopy(outdata, pOff, Plain, 0, pLen);

            int Off = 0;

            int Round = pLen / sizeof(uint);
            for (int i = 0; i < Round; i++)
            {
                uint RoundPlain = BitConverter.ToUInt32(Plain, Off);
                RoundPlain ^= CipherKey;
                Buffer.BlockCopy(BitConverter.GetBytes(RoundPlain), 0, Plain, Off, sizeof(uint));
                Off += sizeof(uint);
            }

            int Rest = pLen % sizeof(uint);
            for (int i = 0; i < Rest; i++)
            {
                byte RestPlain = Plain[Off];
                RestPlain ^= (byte)CipherKey;
                Plain[Off] = RestPlain;
                Off++;
            }
            Buffer.BlockCopy(Plain, 0, outdata, pOff, pLen);
            pkt.SetData(outdata);
            return Length;
        }
    }
}
