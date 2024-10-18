// Decompiled with JetBrains decompiler
// Type: MessagePack.Decoders.FixExt8DateTime
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using System;

#nullable disable
namespace MessagePack.Decoders
{
  internal sealed class FixExt8DateTime : IDateTimeDecoder
  {
    internal static readonly IDateTimeDecoder Instance = (IDateTimeDecoder) new FixExt8DateTime();

    private FixExt8DateTime()
    {
    }

    public DateTime Read(byte[] bytes, int offset, out int readSize)
    {
      sbyte num1 = (sbyte) bytes[offset + 1];
      if (num1 != (sbyte) -1)
        throw new InvalidOperationException(string.Format("typeCode is invalid. typeCode:{0}", (object) num1));
      ulong num2 = (ulong) ((long) bytes[offset + 2] << 56 | (long) bytes[offset + 3] << 48 | (long) bytes[offset + 4] << 40 | (long) bytes[offset + 5] << 32 | (long) bytes[offset + 6] << 24 | (long) bytes[offset + 7] << 16 | (long) bytes[offset + 8] << 8) | (ulong) bytes[offset + 9];
      long num3 = (long) (num2 >> 34);
      ulong num4 = num2 & 17179869183UL;
      readSize = 10;
      return DateTimeConstants.UnixEpoch.AddSeconds((double) num4).AddTicks(num3 / 100L);
    }
  }
}
