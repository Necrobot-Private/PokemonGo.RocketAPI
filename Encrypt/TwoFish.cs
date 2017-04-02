using System;

namespace PokemonGo.RocketAPI.Encrypt
{
    public static class TwoFish
    {
        public static int BLOCK_SIZE = 16;
        private static int ROUNDS = 16;

        private static int INPUT_WHITEN = 0;
        private static int OUTPUT_WHITEN = INPUT_WHITEN + BLOCK_SIZE / 4;
        private static int ROUND_SUBKEYS = OUTPUT_WHITEN + BLOCK_SIZE / 4;

        private static int SK_STEP = 0x02020202;
        private static int SK_BUMP = 0x01010101;
        private static int SK_ROTL = 9;

        /**
         * Fixed 8x8 permutation S-boxes
         */
        private static byte[][] P = new byte[2][]
    {
            new byte[256]{
                     0xA9,  0x67,  0xB3,  0xE8,
                     0x04,  0xFD,  0xA3,  0x76,
                     0x9A,  0x92,  0x80,  0x78,
                     0xE4,  0xDD,  0xD1,  0x38,
                     0x0D,  0xC6,  0x35,  0x98,
                     0x18,  0xF7,  0xEC,  0x6C,
                     0x43,  0x75,  0x37,  0x26,
                     0xFA,  0x13,  0x94,  0x48,
                     0xF2,  0xD0,  0x8B,  0x30,
                     0x84,  0x54,  0xDF,  0x23,
                     0x19,  0x5B,  0x3D,  0x59,
                     0xF3,  0xAE,  0xA2,  0x82,
                     0x63,  0x01,  0x83,  0x2E,
                     0xD9,  0x51,  0x9B,  0x7C,
                     0xA6,  0xEB,  0xA5,  0xBE,
                     0x16,  0x0C,  0xE3,  0x61,
                     0xC0,  0x8C,  0x3A,  0xF5,
                     0x73,  0x2C,  0x25,  0x0B,
                     0xBB,  0x4E,  0x89,  0x6B,
                     0x53,  0x6A,  0xB4,  0xF1,
                     0xE1,  0xE6,  0xBD,  0x45,
                     0xE2,  0xF4,  0xB6,  0x66,
                     0xCC,  0x95,  0x03,  0x56,
                     0xD4,  0x1C,  0x1E,  0xD7,
                     0xFB,  0xC3,  0x8E,  0xB5,
                     0xE9,  0xCF,  0xBF,  0xBA,
                     0xEA,  0x77,  0x39,  0xAF,
                     0x33,  0xC9,  0x62,  0x71,
                     0x81,  0x79,  0x09,  0xAD,
                     0x24,  0xCD,  0xF9,  0xD8,
                     0xE5,  0xC5,  0xB9,  0x4D,
                     0x44,  0x08,  0x86,  0xE7,
                     0xA1,  0x1D,  0xAA,  0xED,
                     0x06,  0x70,  0xB2,  0xD2,
                     0x41,  0x7B,  0xA0,  0x11,
                     0x31,  0xC2,  0x27,  0x90,
                     0x20,  0xF6,  0x60,  0xFF,
                     0x96,  0x5C,  0xB1,  0xAB,
                     0x9E,  0x9C,  0x52,  0x1B,
                     0x5F,  0x93,  0x0A,  0xEF,
                     0x91,  0x85,  0x49,  0xEE,
                     0x2D,  0x4F,  0x8F,  0x3B,
                     0x47,  0x87,  0x6D,  0x46,
                     0xD6,  0x3E,  0x69,  0x64,
                     0x2A,  0xCE,  0xCB,  0x2F,
                     0xFC,  0x97,  0x05,  0x7A,
                     0xAC,  0x7F,  0xD5,  0x1A,
                     0x4B,  0x0E,  0xA7,  0x5A,
                     0x28,  0x14,  0x3F,  0x29,
                     0x88,  0x3C,  0x4C,  0x02,
                     0xB8,  0xDA,  0xB0,  0x17,
                     0x55,  0x1F,  0x8A,  0x7D,
                     0x57,  0xC7,  0x8D,  0x74,
                     0xB7,  0xC4,  0x9F,  0x72,
                     0x7E,  0x15,  0x22,  0x12,
                     0x58,  0x07,  0x99,  0x34,
                     0x6E,  0x50,  0xDE,  0x68,
                     0x65,  0xBC,  0xDB,  0xF8,
                     0xC8,  0xA8,  0x2B,  0x40,
                     0xDC,  0xFE,  0x32,  0xA4,
                     0xCA,  0x10,  0x21,  0xF0,
                     0xD3,  0x5D,  0x0F,  0x00,
                     0x6F,  0x9D,  0x36,  0x42,
                     0x4A,  0x5E,  0xC1,  0xE0
            },
            new byte[256] {
                     0x75,  0xF3,  0xC6,  0xF4,
                     0xDB,  0x7B,  0xFB,  0xC8,
                     0x4A,  0xD3,  0xE6,  0x6B,
                     0x45,  0x7D,  0xE8,  0x4B,
                     0xD6,  0x32,  0xD8,  0xFD,
                     0x37,  0x71,  0xF1,  0xE1,
                     0x30,  0x0F,  0xF8,  0x1B,
                     0x87,  0xFA,  0x06,  0x3F,
                     0x5E,  0xBA,  0xAE,  0x5B,
                     0x8A,  0x00,  0xBC,  0x9D,
                     0x6D,  0xC1,  0xB1,  0x0E,
                     0x80,  0x5D,  0xD2,  0xD5,
                     0xA0,  0x84,  0x07,  0x14,
                     0xB5,  0x90,  0x2C,  0xA3,
                     0xB2,  0x73,  0x4C,  0x54,
                     0x92,  0x74,  0x36,  0x51,
                     0x38,  0xB0,  0xBD,  0x5A,
                     0xFC,  0x60,  0x62,  0x96,
                     0x6C,  0x42,  0xF7,  0x10,
                     0x7C,  0x28,  0x27,  0x8C,
                     0x13,  0x95,  0x9C,  0xC7,
                     0x24,  0x46,  0x3B,  0x70,
                     0xCA,  0xE3,  0x85,  0xCB,
                     0x11,  0xD0,  0x93,  0xB8,
                     0xA6,  0x83,  0x20,  0xFF,
                     0x9F,  0x77,  0xC3,  0xCC,
                     0x03,  0x6F,  0x08,  0xBF,
                     0x40,  0xE7,  0x2B,  0xE2,
                     0x79,  0x0C,  0xAA,  0x82,
                     0x41,  0x3A,  0xEA,  0xB9,
                     0xE4,  0x9A,  0xA4,  0x97,
                     0x7E,  0xDA,  0x7A,  0x17,
                     0x66,  0x94,  0xA1,  0x1D,
                     0x3D,  0xF0,  0xDE,  0xB3,
                     0x0B,  0x72,  0xA7,  0x1C,
                     0xEF,  0xD1,  0x53,  0x3E,
                     0x8F,  0x33,  0x26,  0x5F,
                     0xEC,  0x76,  0x2A,  0x49,
                     0x81,  0x88,  0xEE,  0x21,
                     0xC4,  0x1A,  0xEB,  0xD9,
                     0xC5,  0x39,  0x99,  0xCD,
                     0xAD,  0x31,  0x8B,  0x01,
                     0x18,  0x23,  0xDD,  0x1F,
                     0x4E,  0x2D,  0xF9,  0x48,
                     0x4F,  0xF2,  0x65,  0x8E,
                     0x78,  0x5C,  0x58,  0x19,
                     0x8D,  0xE5,  0x98,  0x57,
                     0x67,  0x7F,  0x05,  0x64,
                     0xAF,  0x63,  0xB6,  0xFE,
                     0xF5,  0xB7,  0x3C,  0xA5,
                     0xCE,  0xE9,  0x68,  0x44,
                     0xE0,  0x4D,  0x43,  0x69,
                     0x29,  0x2E,  0xAC,  0x15,
                     0x59,  0xA8,  0x0A,  0x9E,
                     0x6E,  0x47,  0xDF,  0x34,
                     0x35,  0x6A,  0xCF,  0xDC,
                     0x22,  0xC9,  0xC0,  0x9B,
                     0x89,  0xD4,  0xED,  0xAB,
                     0x12,  0xA2,  0x0D,  0x52,
                     0xBB,  0x02,  0x2F,  0xA9,
                     0xD7,  0x61,  0x1E,  0xB4,
                     0x50,  0x04,  0xF6,  0xC2,
                     0x16,  0x25,  0x86,  0x56,
                     0x55,  0x09,  0xBE,  0x91
            }
    };

        /**
         * Define the fixed p0/p1 permutations used in keyed S-box lookup.
         * By changing the following constant definitions, the S-boxes will
         * automatically get changed in the Twofish engine.
         */
        private static int P_00 = 1;
        private static int P_01 = 0;
        private static int P_02 = 0;
        private static int P_03 = P_01 ^ 1;
        private static int P_04 = 1;

        private static int P_10 = 0;
        private static int P_11 = 0;
        private static int P_12 = 1;
        private static int P_13 = P_11 ^ 1;
        private static int P_14 = 0;

        private static int P_20 = 1;
        private static int P_21 = 1;
        private static int P_22 = 0;
        private static int P_23 = P_21 ^ 1;
        private static int P_24 = 0;

        private static int P_30 = 0;
        private static int P_31 = 1;
        private static int P_32 = 1;
        private static int P_33 = P_31 ^ 1;
        private static int P_34 = 1;

        /**
         * Primitive polynomial for GF(256)
         */
        private static int GF256_FDBK_2 = 0x169 / 2;
        private static int GF256_FDBK_4 = 0x169 / 4;

        /**
         * MDS matrix
         */
        private static int[][] MDS = new int[4][];

        private static int RS_GF_FDBK = 0x14D;

        static TwoFish()
        {
            int[] m1 = new int[2];
            int[] mxArray = new int[2];
            int[] myArray = new int[2];
            int first;
            int second = 0;

            for (int i = 0; i < MDS.Length; i++)
            {
                MDS[i] = new int[256];
            }

            for (first = 0; first < 256; first++)
            {
                second = P[0][first] & 0xFF;
                m1[0] = second;
                mxArray[0] = MxX(second) & 0xFF;
                myArray[0] = MxY(second) & 0xFF;

                second = P[1][first] & 0xFF;
                m1[1] = second;
                mxArray[1] = MxX(second) & 0xFF;
                myArray[1] = MxY(second) & 0xFF;

                MDS[0][first] = m1[P_00]
                        | mxArray[P_00] << 8
                        | myArray[P_00] << 16
                        | myArray[P_00] << 24;
                MDS[1][first] = myArray[P_10]
                        | myArray[P_10] << 8
                        | mxArray[P_10] << 16
                        | m1[P_10] << 24;
                MDS[2][first] = mxArray[P_20]
                        | myArray[P_20] << 8
                        | m1[P_20] << 16
                        | myArray[P_20] << 24;
                MDS[3][first] = mxArray[P_30]
                        | m1[P_30] << 8
                        | myArray[P_30] << 16
                        | mxArray[P_30] << 24;
            }
        }

        private static int Lfsr1(int x)
        {
            return (x >> 1) ^ ((x & 0x01) != 0 ? GF256_FDBK_2 : 0);
        }

        private static int Lfsr2(int x)
        {
            return (x >> 2) ^ ((x & 0x02) != 0 ? GF256_FDBK_2 : 0) ^ ((x & 0x01) != 0 ? GF256_FDBK_4 : 0);
        }

        private static int MxX(int x)
        {
            return x ^ Lfsr2(x);
        }

        private static int MxY(int x)
        {
            return x ^ Lfsr1(x) ^ Lfsr2(x);
        }

        /**
         * Expand a user-supplied key material into a session key.
         *
         * @param k The 64/128/192/256-bit user-key to use.
         * @return This cipher's round keys.
         * @throws InvalidKeyException If the key is invalid.
         */
        public static object[] MakeKey(byte[] k)
        {
            if (k == null)
                throw new Exception("Empty key");
            int length = k.Length;
            if (!(length == 8 || length == 16 || length == 24 || length == 32))
                throw new Exception("Incorrect key length");

            int k64Cnt = length / 8;
            int subkeyCnt = ROUND_SUBKEYS + 2 * ROUNDS;
            int[] k32e = new int[4];
            int[] k32o = new int[4];
            int[] sBoxKey = new int[4];
            int i, j, offset = 0;
            for (i = 0, j = k64Cnt - 1; i < 4 && offset < length; i++, j--)
            {
                k32e[i] = (k[offset++] & 0xFF)
                        | (k[offset++] & 0xFF) << 8
                        | (k[offset++] & 0xFF) << 16
                        | (k[offset++] & 0xFF) << 24;
                k32o[i] = (k[offset++] & 0xFF)
                        | (k[offset++] & 0xFF) << 8
                        | (k[offset++] & 0xFF) << 16
                        | (k[offset++] & 0xFF) << 24;
                sBoxKey[j] = RsMdsEncode(k32e[i], k32o[i]);
            }
            int q, A, B;
            int[] subKeys = new int[subkeyCnt];
            for (i = q = 0; i < subkeyCnt / 2; i++, q += SK_STEP)
            {
                A = F32(k64Cnt, q, k32e);
                B = F32(k64Cnt, q + SK_BUMP, k32o);
                B = B << 8 | RightUShift(B, 24);
                A += B;
                subKeys[2 * i] = A;
                A += B;
                subKeys[2 * i + 1] = A << SK_ROTL | RightUShift(A, (32 - SK_ROTL));
            }
            int k0 = sBoxKey[0];
            int k1 = sBoxKey[1];
            int k2 = sBoxKey[2];
            int k3 = sBoxKey[3];
            int b0, b1, b2, b3;
            int[] sBox = new int[4 * 256];
            for (i = 0; i < 256; i++)
            {
                b0 = b1 = b2 = b3 = i;

                int val = k64Cnt & 3;

                if (val == 1)
                {
                    sBox[2 * i] = MDS[0][(P[P_01][b0] & 0xFF) ^ _b0(k0)];
                    sBox[2 * i + 1] = MDS[1][(P[P_11][b1] & 0xFF) ^ _b1(k0)];
                    sBox[0x200 + 2 * i] = MDS[2][(P[P_21][b2] & 0xFF) ^ _b2(k0)];
                    sBox[0x200 + 2 * i + 1] = MDS[3][(P[P_31][b3] & 0xFF) ^ _b3(k0)];
                }
                switch (k64Cnt & 3)
                {
                    case 1:
                        sBox[2 * i] = MDS[0][(P[P_01][b0] & 0xFF) ^ _b0(k0)];
                        sBox[2 * i + 1] = MDS[1][(P[P_11][b1] & 0xFF) ^ _b1(k0)];
                        sBox[0x200 + 2 * i] = MDS[2][(P[P_21][b2] & 0xFF) ^ _b2(k0)];
                        sBox[0x200 + 2 * i + 1] = MDS[3][(P[P_31][b3] & 0xFF) ^ _b3(k0)];
                        break;
                    case 0:
                        b0 = (P[P_04][b0] & 0xFF) ^ _b0(k3);
                        b1 = (P[P_14][b1] & 0xFF) ^ _b1(k3);
                        b2 = (P[P_24][b2] & 0xFF) ^ _b2(k3);
                        b3 = (P[P_34][b3] & 0xFF) ^ _b3(k3);


                        b0 = (P[P_03][b0] & 0xFF) ^ _b0(k2);
                        b1 = (P[P_13][b1] & 0xFF) ^ _b1(k2);
                        b2 = (P[P_23][b2] & 0xFF) ^ _b2(k2);
                        b3 = (P[P_33][b3] & 0xFF) ^ _b3(k2);


                        sBox[2 * i] = MDS[0][(P[P_01][(P[P_02][b0] & 0xFF) ^ _b0(k1)] & 0xFF) ^ _b0(k0)];
                        sBox[2 * i + 1] = MDS[1][(P[P_11][(P[P_12][b1] & 0xFF) ^ _b1(k1)] & 0xFF) ^ _b1(k0)];
                        sBox[0x200 + 2 * i] = MDS[2][(P[P_21][(P[P_22][b2] & 0xFF) ^ _b2(k1)] & 0xFF) ^ _b2(k0)];
                        sBox[0x200 + 2 * i + 1] = MDS[3][(P[P_31][(P[P_32][b3] & 0xFF) ^ _b3(k1)] & 0xFF) ^ _b3(k0)];
                        break;
                    case 3:
                        b0 = (P[P_03][b0] & 0xFF) ^ _b0(k2);
                        b1 = (P[P_13][b1] & 0xFF) ^ _b1(k2);
                        b2 = (P[P_23][b2] & 0xFF) ^ _b2(k2);
                        b3 = (P[P_33][b3] & 0xFF) ^ _b3(k2);

                        sBox[2 * i] = MDS[0][(P[P_01][(P[P_02][b0] & 0xFF) ^ _b0(k1)] & 0xFF) ^ _b0(k0)];
                        sBox[2 * i + 1] = MDS[1][(P[P_11][(P[P_12][b1] & 0xFF) ^ _b1(k1)] & 0xFF) ^ _b1(k0)];
                        sBox[0x200 + 2 * i] = MDS[2][(P[P_21][(P[P_22][b2] & 0xFF) ^ _b2(k1)] & 0xFF) ^ _b2(k0)];
                        sBox[0x200 + 2 * i + 1] = MDS[3][(P[P_31][(P[P_32][b3] & 0xFF) ^ _b3(k1)] & 0xFF) ^ _b3(k0)];
                        break;
                    case 2:
                        sBox[2 * i] = MDS[0][(P[P_01][(P[P_02][b0] & 0xFF) ^ _b0(k1)] & 0xFF) ^ _b0(k0)];
                        sBox[2 * i + 1] = MDS[1][(P[P_11][(P[P_12][b1] & 0xFF) ^ _b1(k1)] & 0xFF) ^ _b1(k0)];
                        sBox[0x200 + 2 * i] = MDS[2][(P[P_21][(P[P_22][b2] & 0xFF) ^ _b2(k1)] & 0xFF) ^ _b2(k0)];
                        sBox[0x200 + 2 * i + 1] = MDS[3][(P[P_31][(P[P_32][b3] & 0xFF) ^ _b3(k1)] & 0xFF) ^ _b3(k0)];
                        break;
                }
            }
            return new object[] { sBox, subKeys };
        }

        public static int RightUShift(int val, int shift)
        {
            return (int)((uint)val >> shift);
        }

        /**
         * Encrypt exactly one block of plaintext.
         *
         * @param in The plaintext.
         * @param inOffset Index of in from which to start considering data.
         * @param sessionKey The session key to use for encryption.
         * @return The ciphertext generated from a plaintext using the session key.
         */
        public static byte[] BlockEncrypt(byte[] bArray, int inOffset, Object sessionKey)
        {
            Object[] sk = (Object[])sessionKey;
            int[] sBox = (int[])sk[0];
            int[] sKey = (int[])sk[1];

            int x0 = (bArray[inOffset++] & 0xFF)
                    | (bArray[inOffset++] & 0xFF) << 8
                    | (bArray[inOffset++] & 0xFF) << 16
                    | (bArray[inOffset++] & 0xFF) << 24;
            int x1 = (bArray[inOffset++] & 0xFF)
                    | (bArray[inOffset++] & 0xFF) << 8
                    | (bArray[inOffset++] & 0xFF) << 16
                    | (bArray[inOffset++] & 0xFF) << 24;
            int x2 = (bArray[inOffset++] & 0xFF)
                    | (bArray[inOffset++] & 0xFF) << 8
                    | (bArray[inOffset++] & 0xFF) << 16
                    | (bArray[inOffset++] & 0xFF) << 24;
            int x3 = (bArray[inOffset++] & 0xFF)
                    | (bArray[inOffset++] & 0xFF) << 8
                    | (bArray[inOffset++] & 0xFF) << 16
                    | (bArray[inOffset++] & 0xFF) << 24;

            x0 ^= sKey[INPUT_WHITEN];
            x1 ^= sKey[INPUT_WHITEN + 1];
            x2 ^= sKey[INPUT_WHITEN + 2];
            x3 ^= sKey[INPUT_WHITEN + 3];

            int t0, t1;
            int k = ROUND_SUBKEYS;
            for (int R = 0; R < ROUNDS; R += 2)
            {
                t0 = Fe32(sBox, x0, 0);
                t1 = Fe32(sBox, x1, 3);
                x2 ^= t0 + t1 + sKey[k++];
                x2 = RightUShift(x2, 1) | x2 << 31;
                x3 = x3 << 1 | RightUShift(x3, 31);
                x3 ^= t0 + 2 * t1 + sKey[k++];

                t0 = Fe32(sBox, x2, 0);
                t1 = Fe32(sBox, x3, 3);
                x0 ^= t0 + t1 + sKey[k++];
                x0 = RightUShift(x0, 1) | x0 << 31;
                x1 = x1 << 1 | RightUShift(x1, 31);
                x1 ^= t0 + 2 * t1 + sKey[k++];
            }
            x2 ^= sKey[OUTPUT_WHITEN];
            x3 ^= sKey[OUTPUT_WHITEN + 1];
            x0 ^= sKey[OUTPUT_WHITEN + 2];
            x1 ^= sKey[OUTPUT_WHITEN + 3];

            return new byte[]{
                (byte) x2, (byte) RightUShift(x2, 8), (byte) RightUShift(x2, 16), (byte) RightUShift(x2, 24),
                (byte) x3, (byte) RightUShift(x3, 8), (byte) RightUShift(x3, 16), (byte) RightUShift(x3, 24),
                (byte) x0, (byte) RightUShift(x0, 8), (byte) RightUShift(x0, 16), (byte) RightUShift(x0, 24),
                (byte) x1, (byte) RightUShift(x1, 8), (byte) RightUShift(x1, 16), (byte) RightUShift(x1, 24),
            };
        }

        private static int _b0(int x) { return x & 0xFF; }

        private static int _b1(int x) { return RightUShift(x, 8) & 0xFF; }

        private static int _b2(int x) { return RightUShift(x, 16) & 0xFF; }

        private static int _b3(int x) { return RightUShift(x, 24) & 0xFF; }

        /**
         * Use (12, 8) Reed-Solomon code over GF(256) to produce a key S-box
         * 32-bit entity from two key material 32-bit entities.
         *
         * @param k0 1st 32-bit entity.
         * @param k1 2nd 32-bit entity.
         * @return Remainder polynomial generated using RS code
         */
        private static int RsMdsEncode(int k0, int k1)
        {
            int r = k1;
            for (int i = 0; i < 4; i++)
            {
                r = RsRem(r);
            }
            r ^= k0;
            for (int i = 0; i < 4; i++)
            {
                r = RsRem(r);
            }
            return r;
        }

        private static int RsRem(int x)
        {
            int b = RightUShift(x, 24) & 0xFF;
            int g2 = ((b << 1) ^ ((b & 0x80) != 0 ? RS_GF_FDBK : 0)) & 0xFF;
            int g3 = RightUShift(b, 1) ^ ((b & 0x01) != 0 ? RightUShift(RS_GF_FDBK, 1) : 0) ^ g2;
            int result = (x << 8) ^ (g3 << 24) ^ (g2 << 16) ^ (g3 << 8) ^ b;
            return result;
        }

        private static int F32(int k64Cnt, int x, int[] k32)
        {
            int b0 = _b0(x);
            int b1 = _b1(x);
            int b2 = _b2(x);
            int b3 = _b3(x);
            int k0 = k32[0];
            int k1 = k32[1];
            int k2 = k32[2];
            int k3 = k32[3];

            int result = 0;
            switch (k64Cnt & 3)
            {
                case 1:
                    result =
                            MDS[0][(P[P_01][b0] & 0xFF)
                                    ^ _b0(k0)]
                                    ^ MDS[1][(P[P_11][b1] & 0xFF)
                                    ^ _b1(k0)]
                                    ^ MDS[2][(P[P_21][b2] & 0xFF)
                                    ^ _b2(k0)]
                                    ^ MDS[3][(P[P_31][b3] & 0xFF)
                                    ^ _b3(k0)];
                    break;
                case 0:
                    b0 = (P[P_04][b0] & 0xFF) ^ _b0(k3);
                    b1 = (P[P_14][b1] & 0xFF) ^ _b1(k3);
                    b2 = (P[P_24][b2] & 0xFF) ^ _b2(k3);
                    b3 = (P[P_34][b3] & 0xFF) ^ _b3(k3);


                    b0 = (P[P_03][b0] & 0xFF) ^ _b0(k2);
                    b1 = (P[P_13][b1] & 0xFF) ^ _b1(k2);
                    b2 = (P[P_23][b2] & 0xFF) ^ _b2(k2);
                    b3 = (P[P_33][b3] & 0xFF) ^ _b3(k2);

                    result =
                            MDS[0][(P[P_01][(P[P_02][b0] & 0xFF)
                                    ^ _b0(k1)] & 0xFF)
                                    ^ _b0(k0)]
                                    ^ MDS[1][(P[P_11][(P[P_12][b1] & 0xFF)
                                    ^ _b1(k1)] & 0xFF) ^ _b1(k0)]
                                    ^ MDS[2][(P[P_21][(P[P_22][b2] & 0xFF)
                                    ^ _b2(k1)] & 0xFF)
                                    ^ _b2(k0)]
                                    ^ MDS[3][(P[P_31][(P[P_32][b3] & 0xFF)
                                    ^ _b3(k1)] & 0xFF)
                                    ^ _b3(k0)];
                    break;
                case 3:
                    b0 = (P[P_03][b0] & 0xFF) ^ _b0(k2);
                    b1 = (P[P_13][b1] & 0xFF) ^ _b1(k2);
                    b2 = (P[P_23][b2] & 0xFF) ^ _b2(k2);
                    b3 = (P[P_33][b3] & 0xFF) ^ _b3(k2);

                    result =
                            MDS[0][(P[P_01][(P[P_02][b0] & 0xFF)
                                    ^ _b0(k1)] & 0xFF)
                                    ^ _b0(k0)]
                                    ^ MDS[1][(P[P_11][(P[P_12][b1] & 0xFF)
                                    ^ _b1(k1)] & 0xFF) ^ _b1(k0)]
                                    ^ MDS[2][(P[P_21][(P[P_22][b2] & 0xFF)
                                    ^ _b2(k1)] & 0xFF)
                                    ^ _b2(k0)]
                                    ^ MDS[3][(P[P_31][(P[P_32][b3] & 0xFF)
                                    ^ _b3(k1)] & 0xFF)
                                    ^ _b3(k0)];
                    break;
                case 2:
                    result =
                            MDS[0][(P[P_01][(P[P_02][b0] & 0xFF)
                                    ^ _b0(k1)] & 0xFF)
                                    ^ _b0(k0)]
                                    ^ MDS[1][(P[P_11][(P[P_12][b1] & 0xFF)
                                    ^ _b1(k1)] & 0xFF) ^ _b1(k0)]
                                    ^ MDS[2][(P[P_21][(P[P_22][b2] & 0xFF)
                                    ^ _b2(k1)] & 0xFF)
                                    ^ _b2(k0)]
                                    ^ MDS[3][(P[P_31][(P[P_32][b3] & 0xFF)
                                    ^ _b3(k1)] & 0xFF)
                                    ^ _b3(k0)];
                    break;
            }
            return result;
        }

        private static int Fe32(int[] sBox, int x, int r)
        {
            return sBox[2 * B(x, r)]
                    ^ sBox[2 * B(x, r + 1) + 1]
                    ^ sBox[0x200 + 2 * B(x, r + 2)]
                    ^ sBox[0x200 + 2 * B(x, r + 3) + 1];
        }

        private static int B(int x, int n)
        {
            int result = 0;
            switch (n % 4)
            {
                case 0:
                    result = _b0(x);
                    break;
                case 1:
                    result = _b1(x);
                    break;
                case 2:
                    result = _b2(x);
                    break;
                case 3:
                    result = _b3(x);
                    break;
            }
            return result;
        }
    }
}