// Decompiled with JetBrains decompiler
// Type: MessagePack.Decoders.Str32StringSegment
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace MessagePack.Decoders
{
  internal sealed class Str32StringSegment : IStringSegmentDecoder
  {
    internal static readonly IStringSegmentDecoder Instance = (IStringSegmentDecoder) new Str32StringSegment();

    private Str32StringSegment()
    {
    }

    public ArraySegment<byte> Read(byte[] bytes, int offset, out int readSize)
    {
      int count = (int) bytes[offset + 1] << 24 | (int) bytes[offset + 2] << 16 | (int) bytes[offset + 3] << 8 | (int) bytes[offset + 4];
      readSize = count + 5;
      return new ArraySegment<byte>(bytes, offset + 5, count);
    }
  }
}
