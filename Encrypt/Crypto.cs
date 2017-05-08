using System;

namespace PokemonGo.RocketAPI.Encrypt
{
    // 0.59 Encryption
    public class Crypto : ICrypt
    {
        public static byte[] KEY = new byte[]
        {
             0x4F,  0xEB,  0x1C,  0xA5,  0xF6,  0x1A,  0x67,  0xCE,
             0x43,  0xF3,  0xF0,  0x0C,  0xB1,  0x23,  0x88,  0x35,
             0xE9,  0x8B,  0xE8,  0x39,  0xD8,  0x89,  0x8F,  0x5A,
             0x3B,  0x51,  0x2E,  0xA9,  0x47,  0x38,  0xC4,  0x14
        };

        public byte[] MakeIv(Rand rand)
        {
            byte[] iv = new byte[TwoFish.BLOCK_SIZE];
            for (int i = 0; i < iv.Length; i++)
            {
                iv[i] = rand.Next();
            }
            return iv;
        }

        public byte MakeIntegrityByte(Rand rand)
        {
            return 0x21;
            //return 0x21;
        }

        /**
         * Encrypts the given signature
         *
         * @param input input data
         * @param msSinceStart time since start
         * @return encrypted signature
         */
        public byte[] Encrypt(byte[] input, uint msSinceStart)
        {
            try
            {
                object[] key = TwoFish.MakeKey(KEY);

                Rand rand = new Rand(msSinceStart);
                byte[] iv = MakeIv(rand);
                int blockCount = (input.Length + 256) / 256;
                int outputSize = (blockCount * 256) + 5;
                byte[] output = new byte[outputSize];

                output[0] = (byte)(msSinceStart >> 24);
                output[1] = (byte)(msSinceStart >> 16);
                output[2] = (byte)(msSinceStart >> 8);
                output[3] = (byte)msSinceStart;

                Array.Copy(input, 0, output, 4, input.Length);
                output[outputSize - 2] = (byte)(256 - input.Length % 256);

                for (int offset = 0; offset < blockCount * 256; offset += TwoFish.BLOCK_SIZE)
                {
                    for (int i = 0; i < TwoFish.BLOCK_SIZE; i++)
                    {
                        output[4 + offset + i] ^= iv[i];
                    }

                    byte[] block = TwoFish.BlockEncrypt(output, offset + 4, key);
                    Array.Copy(block, 0, output, offset + 4, block.Length);
                    Array.Copy(output, 4 + offset, iv, 0, TwoFish.BLOCK_SIZE);
                }

                output[outputSize - 1] = MakeIntegrityByte(rand);
                return output;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}