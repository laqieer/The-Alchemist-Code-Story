// Decompiled with JetBrains decompiler
// Type: MessagePack.LZ4.LZ4Codec
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Diagnostics;

#nullable disable
namespace MessagePack.LZ4
{
  public static class LZ4Codec
  {
    private const int MEMORY_USAGE = 12;
    private const int NOTCOMPRESSIBLE_DETECTIONLEVEL = 6;
    private const int MINMATCH = 4;
    private const int SKIPSTRENGTH = 6;
    private const int COPYLENGTH = 8;
    private const int LASTLITERALS = 5;
    private const int MFLIMIT = 12;
    private const int MINLENGTH = 13;
    private const int MAXD_LOG = 16;
    private const int MAXD = 65536;
    private const int MAXD_MASK = 65535;
    private const int MAX_DISTANCE = 65535;
    private const int ML_BITS = 4;
    private const int ML_MASK = 15;
    private const int RUN_BITS = 4;
    private const int RUN_MASK = 15;
    private const int STEPSIZE_64 = 8;
    private const int STEPSIZE_32 = 4;
    private const int LZ4_64KLIMIT = 65547;
    private const int HASH_LOG = 10;
    private const int HASH_TABLESIZE = 1024;
    private const int HASH_ADJUST = 22;
    private const int HASH64K_LOG = 11;
    private const int HASH64K_TABLESIZE = 2048;
    private const int HASH64K_ADJUST = 21;
    private const int HASHHC_LOG = 15;
    private const int HASHHC_TABLESIZE = 32768;
    private const int HASHHC_ADJUST = 17;
    private static readonly int[] DECODER_TABLE_32 = new int[8]
    {
      0,
      3,
      2,
      3,
      0,
      0,
      0,
      0
    };
    private static readonly int[] DECODER_TABLE_64 = new int[8]
    {
      0,
      0,
      0,
      -1,
      0,
      1,
      2,
      3
    };
    private static readonly int[] DEBRUIJN_TABLE_32 = new int[32]
    {
      0,
      0,
      3,
      0,
      3,
      1,
      3,
      0,
      3,
      2,
      2,
      1,
      3,
      2,
      0,
      1,
      3,
      3,
      1,
      2,
      2,
      2,
      2,
      0,
      3,
      1,
      2,
      0,
      1,
      0,
      1,
      1
    };
    private static readonly int[] DEBRUIJN_TABLE_64 = new int[64]
    {
      0,
      0,
      0,
      0,
      0,
      1,
      1,
      2,
      0,
      3,
      1,
      3,
      1,
      4,
      2,
      7,
      0,
      2,
      3,
      6,
      1,
      5,
      3,
      5,
      1,
      3,
      4,
      4,
      2,
      5,
      6,
      7,
      7,
      0,
      1,
      2,
      3,
      3,
      4,
      6,
      2,
      6,
      5,
      5,
      3,
      4,
      5,
      6,
      7,
      1,
      2,
      4,
      6,
      4,
      4,
      5,
      7,
      2,
      6,
      5,
      7,
      6,
      7,
      7
    };
    private const int MAX_NB_ATTEMPTS = 256;
    private const int OPTIMAL_ML = 18;
    private const int BLOCK_COPY_LIMIT = 16;

    public static int MaximumOutputLength(int inputLength)
    {
      return inputLength + inputLength / (int) byte.MaxValue + 16;
    }

    internal static void CheckArguments(
      byte[] input,
      int inputOffset,
      int inputLength,
      byte[] output,
      int outputOffset,
      int outputLength)
    {
      if (inputLength == 0)
      {
        outputLength = 0;
      }
      else
      {
        if (input == null)
          throw new ArgumentNullException(nameof (input));
        if ((uint) inputOffset > (uint) input.Length)
          throw new ArgumentOutOfRangeException(nameof (inputOffset));
        if ((uint) inputLength > (uint) (input.Length - inputOffset))
          throw new ArgumentOutOfRangeException(nameof (inputLength));
        if (output == null)
          throw new ArgumentNullException(nameof (output));
        if ((uint) outputOffset > (uint) output.Length)
          throw new ArgumentOutOfRangeException(nameof (outputOffset));
        if ((uint) outputLength > (uint) (output.Length - outputOffset))
          throw new ArgumentOutOfRangeException(nameof (outputLength));
      }
    }

    public static int Encode(
      byte[] input,
      int inputOffset,
      int inputLength,
      byte[] output,
      int outputOffset,
      int outputLength)
    {
      return IntPtr.Size == 4 ? LZ4Codec.Encode32Safe(input, inputOffset, inputLength, output, outputOffset, outputLength) : LZ4Codec.Encode64Safe(input, inputOffset, inputLength, output, outputOffset, outputLength);
    }

    public static int Decode(
      byte[] input,
      int inputOffset,
      int inputLength,
      byte[] output,
      int outputOffset,
      int outputLength)
    {
      return IntPtr.Size == 4 ? LZ4Codec.Decode32Safe(input, inputOffset, inputLength, output, outputOffset, outputLength) : LZ4Codec.Decode64Safe(input, inputOffset, inputLength, output, outputOffset, outputLength);
    }

    [Conditional("DEBUG")]
    private static void Assert(bool condition, string errorMessage)
    {
      if (!condition)
        throw new ArgumentException(errorMessage);
    }

    internal static void Poke2(byte[] buffer, int offset, ushort value)
    {
      buffer[offset] = (byte) value;
      buffer[offset + 1] = (byte) ((uint) value >> 8);
    }

    internal static ushort Peek2(byte[] buffer, int offset)
    {
      return (ushort) ((uint) buffer[offset] | (uint) buffer[offset + 1] << 8);
    }

    internal static uint Peek4(byte[] buffer, int offset)
    {
      return (uint) ((int) buffer[offset] | (int) buffer[offset + 1] << 8 | (int) buffer[offset + 2] << 16 | (int) buffer[offset + 3] << 24);
    }

    private static uint Xor4(byte[] buffer, int offset1, int offset2)
    {
      return (uint) ((int) buffer[offset1] | (int) buffer[offset1 + 1] << 8 | (int) buffer[offset1 + 2] << 16 | (int) buffer[offset1 + 3] << 24) ^ (uint) ((int) buffer[offset2] | (int) buffer[offset2 + 1] << 8 | (int) buffer[offset2 + 2] << 16 | (int) buffer[offset2 + 3] << 24);
    }

    private static ulong Xor8(byte[] buffer, int offset1, int offset2)
    {
      return (ulong) ((long) buffer[offset1] | (long) buffer[offset1 + 1] << 8 | (long) buffer[offset1 + 2] << 16 | (long) buffer[offset1 + 3] << 24 | (long) buffer[offset1 + 4] << 32 | (long) buffer[offset1 + 5] << 40 | (long) buffer[offset1 + 6] << 48 | (long) buffer[offset1 + 7] << 56) ^ (ulong) ((long) buffer[offset2] | (long) buffer[offset2 + 1] << 8 | (long) buffer[offset2 + 2] << 16 | (long) buffer[offset2 + 3] << 24 | (long) buffer[offset2 + 4] << 32 | (long) buffer[offset2 + 5] << 40 | (long) buffer[offset2 + 6] << 48 | (long) buffer[offset2 + 7] << 56);
    }

    private static bool Equal2(byte[] buffer, int offset1, int offset2)
    {
      return (int) buffer[offset1] == (int) buffer[offset2] && (int) buffer[offset1 + 1] == (int) buffer[offset2 + 1];
    }

    private static bool Equal4(byte[] buffer, int offset1, int offset2)
    {
      return (int) buffer[offset1] == (int) buffer[offset2] && (int) buffer[offset1 + 1] == (int) buffer[offset2 + 1] && (int) buffer[offset1 + 2] == (int) buffer[offset2 + 2] && (int) buffer[offset1 + 3] == (int) buffer[offset2 + 3];
    }

    private static void Copy4(byte[] buf, int src, int dst)
    {
      buf[dst + 3] = buf[src + 3];
      buf[dst + 2] = buf[src + 2];
      buf[dst + 1] = buf[src + 1];
      buf[dst] = buf[src];
    }

    private static void Copy8(byte[] buf, int src, int dst)
    {
      buf[dst + 7] = buf[src + 7];
      buf[dst + 6] = buf[src + 6];
      buf[dst + 5] = buf[src + 5];
      buf[dst + 4] = buf[src + 4];
      buf[dst + 3] = buf[src + 3];
      buf[dst + 2] = buf[src + 2];
      buf[dst + 1] = buf[src + 1];
      buf[dst] = buf[src];
    }

    private static void BlockCopy(byte[] src, int src_0, byte[] dst, int dst_0, int len)
    {
      if (len >= 16)
      {
        Buffer.BlockCopy((Array) src, src_0, (Array) dst, dst_0, len);
      }
      else
      {
        while (len >= 8)
        {
          dst[dst_0] = src[src_0];
          dst[dst_0 + 1] = src[src_0 + 1];
          dst[dst_0 + 2] = src[src_0 + 2];
          dst[dst_0 + 3] = src[src_0 + 3];
          dst[dst_0 + 4] = src[src_0 + 4];
          dst[dst_0 + 5] = src[src_0 + 5];
          dst[dst_0 + 6] = src[src_0 + 6];
          dst[dst_0 + 7] = src[src_0 + 7];
          len -= 8;
          src_0 += 8;
          dst_0 += 8;
        }
        while (len >= 4)
        {
          dst[dst_0] = src[src_0];
          dst[dst_0 + 1] = src[src_0 + 1];
          dst[dst_0 + 2] = src[src_0 + 2];
          dst[dst_0 + 3] = src[src_0 + 3];
          len -= 4;
          src_0 += 4;
          dst_0 += 4;
        }
        while (len-- > 0)
          dst[dst_0++] = src[src_0++];
      }
    }

    private static int WildCopy(byte[] src, int src_0, byte[] dst, int dst_0, int dst_end)
    {
      int count = dst_end - dst_0;
      if (count >= 16)
      {
        Buffer.BlockCopy((Array) src, src_0, (Array) dst, dst_0, count);
      }
      else
      {
        while (count >= 4)
        {
          dst[dst_0] = src[src_0];
          dst[dst_0 + 1] = src[src_0 + 1];
          dst[dst_0 + 2] = src[src_0 + 2];
          dst[dst_0 + 3] = src[src_0 + 3];
          count -= 4;
          src_0 += 4;
          dst_0 += 4;
        }
        while (count-- > 0)
          dst[dst_0++] = src[src_0++];
      }
      return count;
    }

    private static int SecureCopy(byte[] buffer, int src, int dst, int dst_end)
    {
      int count1 = dst - src;
      int count2 = dst_end - dst;
      int num = count2;
      if (count1 >= 16)
      {
        if (count1 >= count2)
        {
          Buffer.BlockCopy((Array) buffer, src, (Array) buffer, dst, count2);
          return count2;
        }
        do
        {
          Buffer.BlockCopy((Array) buffer, src, (Array) buffer, dst, count1);
          src += count1;
          dst += count1;
          num -= count1;
        }
        while (num >= count1);
      }
      for (; num >= 4; num -= 4)
      {
        buffer[dst] = buffer[src];
        buffer[dst + 1] = buffer[src + 1];
        buffer[dst + 2] = buffer[src + 2];
        buffer[dst + 3] = buffer[src + 3];
        dst += 4;
        src += 4;
      }
      while (num-- > 0)
        buffer[dst++] = buffer[src++];
      return count2;
    }

    public static int Encode32Safe(
      byte[] input,
      int inputOffset,
      int inputLength,
      byte[] output,
      int outputOffset,
      int outputLength)
    {
      LZ4Codec.CheckArguments(input, inputOffset, inputLength, output, outputOffset, outputLength);
      if (outputLength == 0)
        return 0;
      return inputLength < 65547 ? LZ4Codec.LZ4_compress64kCtx_safe32(LZ4Codec.HashTablePool.GetUShortHashTablePool(), input, output, inputOffset, outputOffset, inputLength, outputLength) : LZ4Codec.LZ4_compressCtx_safe32(LZ4Codec.HashTablePool.GetIntHashTablePool(), input, output, inputOffset, outputOffset, inputLength, outputLength);
    }

    public static int Encode64Safe(
      byte[] input,
      int inputOffset,
      int inputLength,
      byte[] output,
      int outputOffset,
      int outputLength)
    {
      LZ4Codec.CheckArguments(input, inputOffset, inputLength, output, outputOffset, outputLength);
      if (outputLength == 0)
        return 0;
      return inputLength < 65547 ? LZ4Codec.LZ4_compress64kCtx_safe64(LZ4Codec.HashTablePool.GetUShortHashTablePool(), input, output, inputOffset, outputOffset, inputLength, outputLength) : LZ4Codec.LZ4_compressCtx_safe64(LZ4Codec.HashTablePool.GetIntHashTablePool(), input, output, inputOffset, outputOffset, inputLength, outputLength);
    }

    public static int Decode32Safe(
      byte[] input,
      int inputOffset,
      int inputLength,
      byte[] output,
      int outputOffset,
      int outputLength)
    {
      LZ4Codec.CheckArguments(input, inputOffset, inputLength, output, outputOffset, outputLength);
      if (outputLength == 0)
        return 0;
      if (LZ4Codec.LZ4_uncompress_safe32(input, output, inputOffset, outputOffset, outputLength) != inputLength)
        throw new ArgumentException("LZ4 block is corrupted, or invalid length has been given.");
      return outputLength;
    }

    public static int Decode64Safe(
      byte[] input,
      int inputOffset,
      int inputLength,
      byte[] output,
      int outputOffset,
      int outputLength)
    {
      LZ4Codec.CheckArguments(input, inputOffset, inputLength, output, outputOffset, outputLength);
      if (outputLength == 0)
        return 0;
      if (LZ4Codec.LZ4_uncompress_safe64(input, output, inputOffset, outputOffset, outputLength) != inputLength)
        throw new ArgumentException("LZ4 block is corrupted, or invalid length has been given.");
      return outputLength;
    }

    private static int LZ4_compressCtx_safe32(
      int[] hash_table,
      byte[] src,
      byte[] dst,
      int src_0,
      int dst_0,
      int src_len,
      int dst_maxlen)
    {
      int[] debruijnTable32 = LZ4Codec.DEBRUIJN_TABLE_32;
      int offset1 = src_0;
      int num1 = src_0;
      int src_0_1 = offset1;
      int num2 = offset1 + src_len;
      int num3 = num2 - 12;
      int num4 = dst_0;
      int num5 = num4 + dst_maxlen;
      int num6 = num2 - 5;
      int num7 = num6 - 1;
      int num8 = num6 - 3;
      int num9 = num5 - 6;
      int num10 = num5 - 8;
      if (src_len >= 13)
      {
        hash_table[(IntPtr) (LZ4Codec.Peek4(src, offset1) * 2654435761U >> 22)] = offset1 - num1;
        int offset2 = offset1 + 1;
        uint num11 = LZ4Codec.Peek4(src, offset2) * 2654435761U >> 22;
        int index1;
        while (true)
        {
          int num12 = 67;
          int offset3 = offset2;
          int offset1_1;
          do
          {
            uint index2 = num11;
            int num13 = num12++ >> 6;
            index1 = offset3;
            offset3 = index1 + num13;
            if (offset3 <= num3)
            {
              num11 = LZ4Codec.Peek4(src, offset3) * 2654435761U >> 22;
              offset1_1 = num1 + hash_table[(IntPtr) index2];
              hash_table[(IntPtr) index2] = index1 - num1;
            }
            else
              goto label_41;
          }
          while (offset1_1 < index1 - (int) ushort.MaxValue || !LZ4Codec.Equal4(src, offset1_1, index1));
          for (; index1 > src_0_1 && offset1_1 > src_0 && (int) src[index1 - 1] == (int) src[offset1_1 - 1]; --offset1_1)
            --index1;
          int len = index1 - src_0_1;
          int num14 = num4;
          int num15 = num14 + 1;
          int index3 = num14;
          if (num15 + len + (len >> 8) <= num10)
          {
            if (len >= 15)
            {
              int num16 = len - 15;
              dst[index3] = (byte) 240;
              if (num16 > 254)
              {
                do
                {
                  dst[num15++] = byte.MaxValue;
                  num16 -= (int) byte.MaxValue;
                }
                while (num16 > 254);
                byte[] numArray = dst;
                int index4 = num15;
                int dst_0_1 = index4 + 1;
                int num17 = (int) (byte) num16;
                numArray[index4] = (byte) num17;
                LZ4Codec.BlockCopy(src, src_0_1, dst, dst_0_1, len);
                num15 = dst_0_1 + len;
                goto label_17;
              }
              else
                dst[num15++] = (byte) num16;
            }
            else
              dst[index3] = (byte) (len << 4);
            if (len > 0)
            {
              int dst_end = num15 + len;
              LZ4Codec.WildCopy(src, src_0_1, dst, num15, dst_end);
              num15 = dst_end;
            }
label_17:
            while (true)
            {
              LZ4Codec.Poke2(dst, num15, (ushort) (index1 - offset1_1));
              num4 = num15 + 2;
              index1 += 4;
              int offset1_2 = offset1_1 + 4;
              int num18 = index1;
              while (index1 < num8)
              {
                int num19 = (int) LZ4Codec.Xor4(src, offset1_2, index1);
                if (num19 == 0)
                {
                  index1 += 4;
                  offset1_2 += 4;
                }
                else
                {
                  index1 += debruijnTable32[(IntPtr) (uint) ((num19 & -num19) * 125613361 >>> 27)];
                  goto label_26;
                }
              }
              if (index1 < num7 && LZ4Codec.Equal2(src, offset1_2, index1))
              {
                index1 += 2;
                offset1_2 += 2;
              }
              if (index1 < num6 && (int) src[offset1_2] == (int) src[index1])
                ++index1;
label_26:
              int num20 = index1 - num18;
              if (num4 + (num20 >> 8) <= num9)
              {
                if (num20 >= 15)
                {
                  dst[index3] += (byte) 15;
                  int num21;
                  for (num21 = num20 - 15; num21 > 509; num21 -= 510)
                  {
                    byte[] numArray1 = dst;
                    int index5 = num4;
                    int num22 = index5 + 1;
                    numArray1[index5] = byte.MaxValue;
                    byte[] numArray2 = dst;
                    int index6 = num22;
                    num4 = index6 + 1;
                    numArray2[index6] = byte.MaxValue;
                  }
                  if (num21 > 254)
                  {
                    num21 -= (int) byte.MaxValue;
                    dst[num4++] = byte.MaxValue;
                  }
                  dst[num4++] = (byte) num21;
                }
                else
                  dst[index3] += (byte) num20;
                if (index1 <= num3)
                {
                  hash_table[(IntPtr) (LZ4Codec.Peek4(src, index1 - 2) * 2654435761U >> 22)] = index1 - 2 - num1;
                  uint index7 = LZ4Codec.Peek4(src, index1) * 2654435761U >> 22;
                  offset1_1 = num1 + hash_table[(IntPtr) index7];
                  hash_table[(IntPtr) index7] = index1 - num1;
                  if (offset1_1 > index1 - 65536 && LZ4Codec.Equal4(src, offset1_1, index1))
                  {
                    int num23 = num4;
                    num15 = num23 + 1;
                    index3 = num23;
                    dst[index3] = (byte) 0;
                  }
                  else
                    break;
                }
                else
                  goto label_37;
              }
              else
                goto label_27;
            }
            int num24 = index1;
            offset2 = num24 + 1;
            src_0_1 = num24;
            num11 = LZ4Codec.Peek4(src, offset2) * 2654435761U >> 22;
          }
          else
            break;
        }
        return 0;
label_27:
        return 0;
label_37:
        src_0_1 = index1;
      }
label_41:
      int num25 = num2 - src_0_1;
      if (num4 + num25 + 1 + (num25 + (int) byte.MaxValue - 15) / (int) byte.MaxValue > num5)
        return 0;
      int dst_0_2;
      if (num25 >= 15)
      {
        byte[] numArray3 = dst;
        int index8 = num4;
        int num26 = index8 + 1;
        numArray3[index8] = (byte) 240;
        int num27;
        for (num27 = num25 - 15; num27 > 254; num27 -= (int) byte.MaxValue)
          dst[num26++] = byte.MaxValue;
        byte[] numArray4 = dst;
        int index9 = num26;
        dst_0_2 = index9 + 1;
        int num28 = (int) (byte) num27;
        numArray4[index9] = (byte) num28;
      }
      else
      {
        byte[] numArray = dst;
        int index = num4;
        dst_0_2 = index + 1;
        int num29 = (int) (byte) (num25 << 4);
        numArray[index] = (byte) num29;
      }
      LZ4Codec.BlockCopy(src, src_0_1, dst, dst_0_2, num2 - src_0_1);
      return dst_0_2 + (num2 - src_0_1) - dst_0;
    }

    private static int LZ4_compress64kCtx_safe32(
      ushort[] hash_table,
      byte[] src,
      byte[] dst,
      int src_0,
      int dst_0,
      int src_len,
      int dst_maxlen)
    {
      int[] debruijnTable32 = LZ4Codec.DEBRUIJN_TABLE_32;
      int num1 = src_0;
      int src_0_1 = num1;
      int num2 = num1;
      int num3 = num1 + src_len;
      int num4 = num3 - 12;
      int num5 = dst_0;
      int num6 = num5 + dst_maxlen;
      int num7 = num3 - 5;
      int num8 = num7 - 1;
      int num9 = num7 - 3;
      int num10 = num6 - 6;
      int num11 = num6 - 8;
      if (src_len >= 13)
      {
        int offset1 = num1 + 1;
        uint num12 = LZ4Codec.Peek4(src, offset1) * 2654435761U >> 21;
        int index1;
        while (true)
        {
          int num13 = 67;
          int offset2 = offset1;
          int offset1_1;
          do
          {
            uint index2 = num12;
            int num14 = num13++ >> 6;
            index1 = offset2;
            offset2 = index1 + num14;
            if (offset2 <= num4)
            {
              num12 = LZ4Codec.Peek4(src, offset2) * 2654435761U >> 21;
              offset1_1 = num2 + (int) hash_table[(IntPtr) index2];
              hash_table[(IntPtr) index2] = (ushort) (index1 - num2);
            }
            else
              goto label_41;
          }
          while (!LZ4Codec.Equal4(src, offset1_1, index1));
          for (; index1 > src_0_1 && offset1_1 > src_0 && (int) src[index1 - 1] == (int) src[offset1_1 - 1]; --offset1_1)
            --index1;
          int len = index1 - src_0_1;
          int num15 = num5;
          int num16 = num15 + 1;
          int index3 = num15;
          if (num16 + len + (len >> 8) <= num11)
          {
            if (len >= 15)
            {
              int num17 = len - 15;
              dst[index3] = (byte) 240;
              if (num17 > 254)
              {
                do
                {
                  dst[num16++] = byte.MaxValue;
                  num17 -= (int) byte.MaxValue;
                }
                while (num17 > 254);
                byte[] numArray = dst;
                int index4 = num16;
                int dst_0_1 = index4 + 1;
                int num18 = (int) (byte) num17;
                numArray[index4] = (byte) num18;
                LZ4Codec.BlockCopy(src, src_0_1, dst, dst_0_1, len);
                num16 = dst_0_1 + len;
                goto label_17;
              }
              else
                dst[num16++] = (byte) num17;
            }
            else
              dst[index3] = (byte) (len << 4);
            if (len > 0)
            {
              int dst_end = num16 + len;
              LZ4Codec.WildCopy(src, src_0_1, dst, num16, dst_end);
              num16 = dst_end;
            }
label_17:
            while (true)
            {
              LZ4Codec.Poke2(dst, num16, (ushort) (index1 - offset1_1));
              num5 = num16 + 2;
              index1 += 4;
              int offset1_2 = offset1_1 + 4;
              int num19 = index1;
              while (index1 < num9)
              {
                int num20 = (int) LZ4Codec.Xor4(src, offset1_2, index1);
                if (num20 == 0)
                {
                  index1 += 4;
                  offset1_2 += 4;
                }
                else
                {
                  index1 += debruijnTable32[(IntPtr) (uint) ((num20 & -num20) * 125613361 >>> 27)];
                  goto label_26;
                }
              }
              if (index1 < num8 && LZ4Codec.Equal2(src, offset1_2, index1))
              {
                index1 += 2;
                offset1_2 += 2;
              }
              if (index1 < num7 && (int) src[offset1_2] == (int) src[index1])
                ++index1;
label_26:
              int num21 = index1 - num19;
              if (num5 + (num21 >> 8) <= num10)
              {
                if (num21 >= 15)
                {
                  dst[index3] += (byte) 15;
                  int num22;
                  for (num22 = num21 - 15; num22 > 509; num22 -= 510)
                  {
                    byte[] numArray1 = dst;
                    int index5 = num5;
                    int num23 = index5 + 1;
                    numArray1[index5] = byte.MaxValue;
                    byte[] numArray2 = dst;
                    int index6 = num23;
                    num5 = index6 + 1;
                    numArray2[index6] = byte.MaxValue;
                  }
                  if (num22 > 254)
                  {
                    num22 -= (int) byte.MaxValue;
                    dst[num5++] = byte.MaxValue;
                  }
                  dst[num5++] = (byte) num22;
                }
                else
                  dst[index3] += (byte) num21;
                if (index1 <= num4)
                {
                  hash_table[(IntPtr) (LZ4Codec.Peek4(src, index1 - 2) * 2654435761U >> 21)] = (ushort) (index1 - 2 - num2);
                  uint index7 = LZ4Codec.Peek4(src, index1) * 2654435761U >> 21;
                  offset1_1 = num2 + (int) hash_table[(IntPtr) index7];
                  hash_table[(IntPtr) index7] = (ushort) (index1 - num2);
                  if (LZ4Codec.Equal4(src, offset1_1, index1))
                  {
                    int num24 = num5;
                    num16 = num24 + 1;
                    index3 = num24;
                    dst[index3] = (byte) 0;
                  }
                  else
                    break;
                }
                else
                  goto label_37;
              }
              else
                goto label_27;
            }
            int num25 = index1;
            offset1 = num25 + 1;
            src_0_1 = num25;
            num12 = LZ4Codec.Peek4(src, offset1) * 2654435761U >> 21;
          }
          else
            break;
        }
        return 0;
label_27:
        return 0;
label_37:
        src_0_1 = index1;
      }
label_41:
      int num26 = num3 - src_0_1;
      if (num5 + num26 + 1 + (num26 - 15 + (int) byte.MaxValue) / (int) byte.MaxValue > num6)
        return 0;
      int dst_0_2;
      if (num26 >= 15)
      {
        byte[] numArray3 = dst;
        int index8 = num5;
        int num27 = index8 + 1;
        numArray3[index8] = (byte) 240;
        int num28;
        for (num28 = num26 - 15; num28 > 254; num28 -= (int) byte.MaxValue)
          dst[num27++] = byte.MaxValue;
        byte[] numArray4 = dst;
        int index9 = num27;
        dst_0_2 = index9 + 1;
        int num29 = (int) (byte) num28;
        numArray4[index9] = (byte) num29;
      }
      else
      {
        byte[] numArray = dst;
        int index = num5;
        dst_0_2 = index + 1;
        int num30 = (int) (byte) (num26 << 4);
        numArray[index] = (byte) num30;
      }
      LZ4Codec.BlockCopy(src, src_0_1, dst, dst_0_2, num3 - src_0_1);
      return dst_0_2 + (num3 - src_0_1) - dst_0;
    }

    private static int LZ4_uncompress_safe32(
      byte[] src,
      byte[] dst,
      int src_0,
      int dst_0,
      int dst_len)
    {
      int[] decoderTable32 = LZ4Codec.DECODER_TABLE_32;
      int src_0_1 = src_0;
      int dst_0_1 = dst_0;
      int num1 = dst_0_1 + dst_len;
      int num2 = num1 - 5;
      int dst_end1 = num1 - 8;
      int num3 = num1 - 8;
      int len;
      int dst_end2;
      while (true)
      {
        byte num4 = src[src_0_1++];
        if ((len = (int) num4 >> 4) == 15)
        {
          int num5;
          while (true)
          {
            byte[] numArray = src;
            int index = src_0_1++;
            if ((num5 = (int) numArray[index]) == (int) byte.MaxValue)
              len += (int) byte.MaxValue;
            else
              break;
          }
          len += num5;
        }
        dst_end2 = dst_0_1 + len;
        if (dst_end2 <= dst_end1)
        {
          if (dst_0_1 < dst_end2)
          {
            int num6 = LZ4Codec.WildCopy(src, src_0_1, dst, dst_0_1, dst_end2);
            src_0_1 += num6;
            dst_0_1 += num6;
          }
          int offset = src_0_1 - (dst_0_1 - dst_end2);
          int dst1 = dst_end2;
          int src1 = dst_end2 - (int) LZ4Codec.Peek2(src, offset);
          src_0_1 = offset + 2;
          if (src1 >= dst_0)
          {
            int num7;
            if ((num7 = (int) num4 & 15) == 15)
            {
              while (src[src_0_1] == byte.MaxValue)
              {
                ++src_0_1;
                num7 += (int) byte.MaxValue;
              }
              num7 += (int) src[src_0_1++];
            }
            int dst2;
            int src2;
            if (dst1 - src1 < 4)
            {
              dst[dst1] = dst[src1];
              dst[dst1 + 1] = dst[src1 + 1];
              dst[dst1 + 2] = dst[src1 + 2];
              dst[dst1 + 3] = dst[src1 + 3];
              int dst3 = dst1 + 4;
              int num8 = src1 + 4;
              int src3 = num8 - decoderTable32[dst3 - num8];
              LZ4Codec.Copy4(dst, src3, dst3);
              dst2 = dst3;
              src2 = src3;
            }
            else
            {
              LZ4Codec.Copy4(dst, src1, dst1);
              dst2 = dst1 + 4;
              src2 = src1 + 4;
            }
            int dst_end3 = dst2 + num7;
            if (dst_end3 > num3)
            {
              if (dst_end3 <= num2)
              {
                if (dst2 < dst_end1)
                {
                  int num9 = LZ4Codec.SecureCopy(dst, src2, dst2, dst_end1);
                  src2 += num9;
                  dst2 += num9;
                }
                while (dst2 < dst_end3)
                  dst[dst2++] = dst[src2++];
                dst_0_1 = dst_end3;
              }
              else
                goto label_28;
            }
            else
            {
              if (dst2 < dst_end3)
                LZ4Codec.SecureCopy(dst, src2, dst2, dst_end3);
              dst_0_1 = dst_end3;
            }
          }
          else
            goto label_28;
        }
        else
          break;
      }
      if (dst_end2 == num1)
      {
        LZ4Codec.BlockCopy(src, src_0_1, dst, dst_0_1, len);
        return src_0_1 + len - src_0;
      }
label_28:
      return -(src_0_1 - src_0);
    }

    private static int LZ4_compressCtx_safe64(
      int[] hash_table,
      byte[] src,
      byte[] dst,
      int src_0,
      int dst_0,
      int src_len,
      int dst_maxlen)
    {
      int[] debruijnTable64 = LZ4Codec.DEBRUIJN_TABLE_64;
      int offset1 = src_0;
      int num1 = src_0;
      int src_0_1 = offset1;
      int num2 = offset1 + src_len;
      int num3 = num2 - 12;
      int num4 = dst_0;
      int num5 = num4 + dst_maxlen;
      int num6 = num2 - 5;
      int num7 = num6 - 1;
      int num8 = num6 - 3;
      int num9 = num6 - 7;
      int num10 = num5 - 6;
      int num11 = num5 - 8;
      if (src_len >= 13)
      {
        hash_table[(IntPtr) (LZ4Codec.Peek4(src, offset1) * 2654435761U >> 22)] = offset1 - num1;
        int offset2 = offset1 + 1;
        uint num12 = LZ4Codec.Peek4(src, offset2) * 2654435761U >> 22;
        int index1;
        while (true)
        {
          int num13 = 67;
          int offset3 = offset2;
          int offset1_1;
          do
          {
            uint index2 = num12;
            int num14 = num13++ >> 6;
            index1 = offset3;
            offset3 = index1 + num14;
            if (offset3 <= num3)
            {
              num12 = LZ4Codec.Peek4(src, offset3) * 2654435761U >> 22;
              offset1_1 = num1 + hash_table[(IntPtr) index2];
              hash_table[(IntPtr) index2] = index1 - num1;
            }
            else
              goto label_43;
          }
          while (offset1_1 < index1 - (int) ushort.MaxValue || !LZ4Codec.Equal4(src, offset1_1, index1));
          for (; index1 > src_0_1 && offset1_1 > src_0 && (int) src[index1 - 1] == (int) src[offset1_1 - 1]; --offset1_1)
            --index1;
          int len = index1 - src_0_1;
          int num15 = num4;
          int num16 = num15 + 1;
          int index3 = num15;
          if (num16 + len + (len >> 8) <= num11)
          {
            if (len >= 15)
            {
              int num17 = len - 15;
              dst[index3] = (byte) 240;
              if (num17 > 254)
              {
                do
                {
                  dst[num16++] = byte.MaxValue;
                  num17 -= (int) byte.MaxValue;
                }
                while (num17 > 254);
                byte[] numArray = dst;
                int index4 = num16;
                int dst_0_1 = index4 + 1;
                int num18 = (int) (byte) num17;
                numArray[index4] = (byte) num18;
                LZ4Codec.BlockCopy(src, src_0_1, dst, dst_0_1, len);
                num16 = dst_0_1 + len;
                goto label_17;
              }
              else
                dst[num16++] = (byte) num17;
            }
            else
              dst[index3] = (byte) (len << 4);
            if (len > 0)
            {
              int dst_end = num16 + len;
              LZ4Codec.WildCopy(src, src_0_1, dst, num16, dst_end);
              num16 = dst_end;
            }
label_17:
            while (true)
            {
              LZ4Codec.Poke2(dst, num16, (ushort) (index1 - offset1_1));
              num4 = num16 + 2;
              index1 += 4;
              int offset1_2 = offset1_1 + 4;
              int num19 = index1;
              while (index1 < num9)
              {
                long num20 = (long) LZ4Codec.Xor8(src, offset1_2, index1);
                if (num20 == 0L)
                {
                  index1 += 8;
                  offset1_2 += 8;
                }
                else
                {
                  index1 += debruijnTable64[checked ((ulong) (unchecked (num20 & -num20 * 151050438428048703L) >>> 58))];
                  goto label_28;
                }
              }
              if (index1 < num8 && LZ4Codec.Equal4(src, offset1_2, index1))
              {
                index1 += 4;
                offset1_2 += 4;
              }
              if (index1 < num7 && LZ4Codec.Equal2(src, offset1_2, index1))
              {
                index1 += 2;
                offset1_2 += 2;
              }
              if (index1 < num6 && (int) src[offset1_2] == (int) src[index1])
                ++index1;
label_28:
              int num21 = index1 - num19;
              if (num4 + (num21 >> 8) <= num10)
              {
                if (num21 >= 15)
                {
                  dst[index3] += (byte) 15;
                  int num22;
                  for (num22 = num21 - 15; num22 > 509; num22 -= 510)
                  {
                    byte[] numArray1 = dst;
                    int index5 = num4;
                    int num23 = index5 + 1;
                    numArray1[index5] = byte.MaxValue;
                    byte[] numArray2 = dst;
                    int index6 = num23;
                    num4 = index6 + 1;
                    numArray2[index6] = byte.MaxValue;
                  }
                  if (num22 > 254)
                  {
                    num22 -= (int) byte.MaxValue;
                    dst[num4++] = byte.MaxValue;
                  }
                  dst[num4++] = (byte) num22;
                }
                else
                  dst[index3] += (byte) num21;
                if (index1 <= num3)
                {
                  hash_table[(IntPtr) (LZ4Codec.Peek4(src, index1 - 2) * 2654435761U >> 22)] = index1 - 2 - num1;
                  uint index7 = LZ4Codec.Peek4(src, index1) * 2654435761U >> 22;
                  offset1_1 = num1 + hash_table[(IntPtr) index7];
                  hash_table[(IntPtr) index7] = index1 - num1;
                  if (offset1_1 > index1 - 65536 && LZ4Codec.Equal4(src, offset1_1, index1))
                  {
                    int num24 = num4;
                    num16 = num24 + 1;
                    index3 = num24;
                    dst[index3] = (byte) 0;
                  }
                  else
                    break;
                }
                else
                  goto label_39;
              }
              else
                goto label_29;
            }
            int num25 = index1;
            offset2 = num25 + 1;
            src_0_1 = num25;
            num12 = LZ4Codec.Peek4(src, offset2) * 2654435761U >> 22;
          }
          else
            break;
        }
        return 0;
label_29:
        return 0;
label_39:
        src_0_1 = index1;
      }
label_43:
      int num26 = num2 - src_0_1;
      if (num4 + num26 + 1 + (num26 + (int) byte.MaxValue - 15) / (int) byte.MaxValue > num5)
        return 0;
      int dst_0_2;
      if (num26 >= 15)
      {
        byte[] numArray3 = dst;
        int index8 = num4;
        int num27 = index8 + 1;
        numArray3[index8] = (byte) 240;
        int num28;
        for (num28 = num26 - 15; num28 > 254; num28 -= (int) byte.MaxValue)
          dst[num27++] = byte.MaxValue;
        byte[] numArray4 = dst;
        int index9 = num27;
        dst_0_2 = index9 + 1;
        int num29 = (int) (byte) num28;
        numArray4[index9] = (byte) num29;
      }
      else
      {
        byte[] numArray = dst;
        int index = num4;
        dst_0_2 = index + 1;
        int num30 = (int) (byte) (num26 << 4);
        numArray[index] = (byte) num30;
      }
      LZ4Codec.BlockCopy(src, src_0_1, dst, dst_0_2, num2 - src_0_1);
      return dst_0_2 + (num2 - src_0_1) - dst_0;
    }

    private static int LZ4_compress64kCtx_safe64(
      ushort[] hash_table,
      byte[] src,
      byte[] dst,
      int src_0,
      int dst_0,
      int src_len,
      int dst_maxlen)
    {
      int[] debruijnTable64 = LZ4Codec.DEBRUIJN_TABLE_64;
      int num1 = src_0;
      int src_0_1 = num1;
      int num2 = num1;
      int num3 = num1 + src_len;
      int num4 = num3 - 12;
      int num5 = dst_0;
      int num6 = num5 + dst_maxlen;
      int num7 = num3 - 5;
      int num8 = num7 - 1;
      int num9 = num7 - 3;
      int num10 = num7 - 7;
      int num11 = num6 - 6;
      int num12 = num6 - 8;
      if (src_len >= 13)
      {
        int offset1 = num1 + 1;
        uint num13 = LZ4Codec.Peek4(src, offset1) * 2654435761U >> 21;
        int index1;
        while (true)
        {
          int num14 = 67;
          int offset2 = offset1;
          int offset1_1;
          do
          {
            uint index2 = num13;
            int num15 = num14++ >> 6;
            index1 = offset2;
            offset2 = index1 + num15;
            if (offset2 <= num4)
            {
              num13 = LZ4Codec.Peek4(src, offset2) * 2654435761U >> 21;
              offset1_1 = num2 + (int) hash_table[(IntPtr) index2];
              hash_table[(IntPtr) index2] = (ushort) (index1 - num2);
            }
            else
              goto label_43;
          }
          while (!LZ4Codec.Equal4(src, offset1_1, index1));
          for (; index1 > src_0_1 && offset1_1 > src_0 && (int) src[index1 - 1] == (int) src[offset1_1 - 1]; --offset1_1)
            --index1;
          int len = index1 - src_0_1;
          int num16 = num5;
          int num17 = num16 + 1;
          int index3 = num16;
          if (num17 + len + (len >> 8) <= num12)
          {
            if (len >= 15)
            {
              int num18 = len - 15;
              dst[index3] = (byte) 240;
              if (num18 > 254)
              {
                do
                {
                  dst[num17++] = byte.MaxValue;
                  num18 -= (int) byte.MaxValue;
                }
                while (num18 > 254);
                byte[] numArray = dst;
                int index4 = num17;
                int dst_0_1 = index4 + 1;
                int num19 = (int) (byte) num18;
                numArray[index4] = (byte) num19;
                LZ4Codec.BlockCopy(src, src_0_1, dst, dst_0_1, len);
                num17 = dst_0_1 + len;
                goto label_17;
              }
              else
                dst[num17++] = (byte) num18;
            }
            else
              dst[index3] = (byte) (len << 4);
            if (len > 0)
            {
              int dst_end = num17 + len;
              LZ4Codec.WildCopy(src, src_0_1, dst, num17, dst_end);
              num17 = dst_end;
            }
label_17:
            while (true)
            {
              LZ4Codec.Poke2(dst, num17, (ushort) (index1 - offset1_1));
              num5 = num17 + 2;
              index1 += 4;
              int offset1_2 = offset1_1 + 4;
              int num20 = index1;
              while (index1 < num10)
              {
                long num21 = (long) LZ4Codec.Xor8(src, offset1_2, index1);
                if (num21 == 0L)
                {
                  index1 += 8;
                  offset1_2 += 8;
                }
                else
                {
                  index1 += debruijnTable64[checked ((ulong) (unchecked (num21 & -num21 * 151050438428048703L) >>> 58))];
                  goto label_28;
                }
              }
              if (index1 < num9 && LZ4Codec.Equal4(src, offset1_2, index1))
              {
                index1 += 4;
                offset1_2 += 4;
              }
              if (index1 < num8 && LZ4Codec.Equal2(src, offset1_2, index1))
              {
                index1 += 2;
                offset1_2 += 2;
              }
              if (index1 < num7 && (int) src[offset1_2] == (int) src[index1])
                ++index1;
label_28:
              int num22 = index1 - num20;
              if (num5 + (num22 >> 8) <= num11)
              {
                if (num22 >= 15)
                {
                  dst[index3] += (byte) 15;
                  int num23;
                  for (num23 = num22 - 15; num23 > 509; num23 -= 510)
                  {
                    byte[] numArray1 = dst;
                    int index5 = num5;
                    int num24 = index5 + 1;
                    numArray1[index5] = byte.MaxValue;
                    byte[] numArray2 = dst;
                    int index6 = num24;
                    num5 = index6 + 1;
                    numArray2[index6] = byte.MaxValue;
                  }
                  if (num23 > 254)
                  {
                    num23 -= (int) byte.MaxValue;
                    dst[num5++] = byte.MaxValue;
                  }
                  dst[num5++] = (byte) num23;
                }
                else
                  dst[index3] += (byte) num22;
                if (index1 <= num4)
                {
                  hash_table[(IntPtr) (LZ4Codec.Peek4(src, index1 - 2) * 2654435761U >> 21)] = (ushort) (index1 - 2 - num2);
                  uint index7 = LZ4Codec.Peek4(src, index1) * 2654435761U >> 21;
                  offset1_1 = num2 + (int) hash_table[(IntPtr) index7];
                  hash_table[(IntPtr) index7] = (ushort) (index1 - num2);
                  if (LZ4Codec.Equal4(src, offset1_1, index1))
                  {
                    int num25 = num5;
                    num17 = num25 + 1;
                    index3 = num25;
                    dst[index3] = (byte) 0;
                  }
                  else
                    break;
                }
                else
                  goto label_39;
              }
              else
                goto label_29;
            }
            int num26 = index1;
            offset1 = num26 + 1;
            src_0_1 = num26;
            num13 = LZ4Codec.Peek4(src, offset1) * 2654435761U >> 21;
          }
          else
            break;
        }
        return 0;
label_29:
        return 0;
label_39:
        src_0_1 = index1;
      }
label_43:
      int num27 = num3 - src_0_1;
      if (num5 + num27 + 1 + (num27 - 15 + (int) byte.MaxValue) / (int) byte.MaxValue > num6)
        return 0;
      int dst_0_2;
      if (num27 >= 15)
      {
        byte[] numArray3 = dst;
        int index8 = num5;
        int num28 = index8 + 1;
        numArray3[index8] = (byte) 240;
        int num29;
        for (num29 = num27 - 15; num29 > 254; num29 -= (int) byte.MaxValue)
          dst[num28++] = byte.MaxValue;
        byte[] numArray4 = dst;
        int index9 = num28;
        dst_0_2 = index9 + 1;
        int num30 = (int) (byte) num29;
        numArray4[index9] = (byte) num30;
      }
      else
      {
        byte[] numArray = dst;
        int index = num5;
        dst_0_2 = index + 1;
        int num31 = (int) (byte) (num27 << 4);
        numArray[index] = (byte) num31;
      }
      LZ4Codec.BlockCopy(src, src_0_1, dst, dst_0_2, num3 - src_0_1);
      return dst_0_2 + (num3 - src_0_1) - dst_0;
    }

    private static int LZ4_uncompress_safe64(
      byte[] src,
      byte[] dst,
      int src_0,
      int dst_0,
      int dst_len)
    {
      int[] decoderTable32 = LZ4Codec.DECODER_TABLE_32;
      int[] decoderTable64 = LZ4Codec.DECODER_TABLE_64;
      int src_0_1 = src_0;
      int dst_0_1 = dst_0;
      int num1 = dst_0_1 + dst_len;
      int num2 = num1 - 5;
      int dst_end1 = num1 - 8;
      int num3 = num1 - 8 - 4;
      int len;
      int dst_end2;
      while (true)
      {
        uint num4 = (uint) src[src_0_1++];
        if ((len = (int) (byte) (num4 >> 4)) == 15)
        {
          int num5;
          while (true)
          {
            byte[] numArray = src;
            int index = src_0_1++;
            if ((num5 = (int) numArray[index]) == (int) byte.MaxValue)
              len += (int) byte.MaxValue;
            else
              break;
          }
          len += num5;
        }
        dst_end2 = dst_0_1 + len;
        if (dst_end2 <= dst_end1)
        {
          if (dst_0_1 < dst_end2)
          {
            int num6 = LZ4Codec.WildCopy(src, src_0_1, dst, dst_0_1, dst_end2);
            src_0_1 += num6;
            dst_0_1 += num6;
          }
          int offset = src_0_1 - (dst_0_1 - dst_end2);
          int dst1 = dst_end2;
          int src1 = dst_end2 - (int) LZ4Codec.Peek2(src, offset);
          src_0_1 = offset + 2;
          if (src1 >= dst_0)
          {
            int num7;
            if ((num7 = (int) (byte) (num4 & 15U)) == 15)
            {
              while (src[src_0_1] == byte.MaxValue)
              {
                ++src_0_1;
                num7 += (int) byte.MaxValue;
              }
              num7 += (int) src[src_0_1++];
            }
            int dst2;
            int src2;
            if (dst1 - src1 < 8)
            {
              int num8 = decoderTable64[dst1 - src1];
              dst[dst1] = dst[src1];
              dst[dst1 + 1] = dst[src1 + 1];
              dst[dst1 + 2] = dst[src1 + 2];
              dst[dst1 + 3] = dst[src1 + 3];
              int dst3 = dst1 + 4;
              int num9 = src1 + 4;
              int src3 = num9 - decoderTable32[dst3 - num9];
              LZ4Codec.Copy4(dst, src3, dst3);
              dst2 = dst3 + 4;
              src2 = src3 - num8;
            }
            else
            {
              LZ4Codec.Copy8(dst, src1, dst1);
              dst2 = dst1 + 8;
              src2 = src1 + 8;
            }
            int dst_end3 = dst2 + num7 - 4;
            if (dst_end3 > num3)
            {
              if (dst_end3 <= num2)
              {
                if (dst2 < dst_end1)
                {
                  int num10 = LZ4Codec.SecureCopy(dst, src2, dst2, dst_end1);
                  src2 += num10;
                  dst2 += num10;
                }
                while (dst2 < dst_end3)
                  dst[dst2++] = dst[src2++];
                dst_0_1 = dst_end3;
              }
              else
                goto label_28;
            }
            else
            {
              if (dst2 < dst_end3)
                LZ4Codec.SecureCopy(dst, src2, dst2, dst_end3);
              dst_0_1 = dst_end3;
            }
          }
          else
            goto label_28;
        }
        else
          break;
      }
      if (dst_end2 == num1)
      {
        LZ4Codec.BlockCopy(src, src_0_1, dst, dst_0_1, len);
        return src_0_1 + len - src_0;
      }
label_28:
      return -(src_0_1 - src_0);
    }

    internal static class HashTablePool
    {
      [ThreadStatic]
      private static ushort[] ushortPool;
      [ThreadStatic]
      private static uint[] uintPool;
      [ThreadStatic]
      private static int[] intPool;

      public static ushort[] GetUShortHashTablePool()
      {
        if (LZ4Codec.HashTablePool.ushortPool == null)
          LZ4Codec.HashTablePool.ushortPool = new ushort[2048];
        else
          Array.Clear((Array) LZ4Codec.HashTablePool.ushortPool, 0, LZ4Codec.HashTablePool.ushortPool.Length);
        return LZ4Codec.HashTablePool.ushortPool;
      }

      public static uint[] GetUIntHashTablePool()
      {
        if (LZ4Codec.HashTablePool.uintPool == null)
          LZ4Codec.HashTablePool.uintPool = new uint[1024];
        else
          Array.Clear((Array) LZ4Codec.HashTablePool.uintPool, 0, LZ4Codec.HashTablePool.uintPool.Length);
        return LZ4Codec.HashTablePool.uintPool;
      }

      public static int[] GetIntHashTablePool()
      {
        if (LZ4Codec.HashTablePool.intPool == null)
          LZ4Codec.HashTablePool.intPool = new int[1024];
        else
          Array.Clear((Array) LZ4Codec.HashTablePool.intPool, 0, LZ4Codec.HashTablePool.intPool.Length);
        return LZ4Codec.HashTablePool.intPool;
      }
    }
  }
}
