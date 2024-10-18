// Decompiled with JetBrains decompiler
// Type: MessagePack.Decoders.FixExt4DateTime
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using System;

#nullable disable
namespace MessagePack.Decoders
{
  internal sealed class FixExt4DateTime : IDateTimeDecoder
  {
    internal static readonly IDateTimeDecoder Instance = (IDateTimeDecoder) new FixExt4DateTime();

    private FixExt4DateTime()
    {
    }

    public DateTime Read(byte[] bytes, int offset, out int readSize)
    {
      sbyte num1 = (sbyte) bytes[offset + 1];
      if (num1 != (sbyte) -1)
        throw new InvalidOperationException(string.Format("typeCode is invalid. typeCode:{0}", (object) num1));
      uint num2 = (uint) ((int) bytes[offset + 2] << 24 | (int) bytes[offset + 3] << 16 | (int) bytes[offset + 4] << 8) | (uint) bytes[offset + 5];
      readSize = 6;
      return DateTimeConstants.UnixEpoch.AddSeconds((double) num2);
    }
  }
}
