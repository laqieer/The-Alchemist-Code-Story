// Decompiled with JetBrains decompiler
// Type: MessagePack.Decoders.Ext8DateTime
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using System;

#nullable disable
namespace MessagePack.Decoders
{
  internal sealed class Ext8DateTime : IDateTimeDecoder
  {
    internal static readonly IDateTimeDecoder Instance = (IDateTimeDecoder) new Ext8DateTime();

    private Ext8DateTime()
    {
    }

    public DateTime Read(byte[] bytes, int offset, out int readSize)
    {
      byte num1 = bytes[checked (offset + 1)];
      sbyte num2 = (sbyte) bytes[offset + 2];
      if (num1 != (byte) 12 || num2 != (sbyte) -1)
        throw new InvalidOperationException(string.Format("typeCode is invalid. typeCode:{0}", (object) num2));
      uint num3 = (uint) ((int) bytes[offset + 3] << 24 | (int) bytes[offset + 4] << 16 | (int) bytes[offset + 5] << 8) | (uint) bytes[offset + 6];
      long num4 = (long) bytes[offset + 7] << 56 | (long) bytes[offset + 8] << 48 | (long) bytes[offset + 9] << 40 | (long) bytes[offset + 10] << 32 | (long) bytes[offset + 11] << 24 | (long) bytes[offset + 12] << 16 | (long) bytes[offset + 13] << 8 | (long) bytes[offset + 14];
      readSize = 15;
      return DateTimeConstants.UnixEpoch.AddSeconds((double) num4).AddTicks((long) (num3 / 100U));
    }
  }
}
