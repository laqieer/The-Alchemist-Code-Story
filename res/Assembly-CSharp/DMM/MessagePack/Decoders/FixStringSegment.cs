// Decompiled with JetBrains decompiler
// Type: MessagePack.Decoders.FixStringSegment
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace MessagePack.Decoders
{
  internal sealed class FixStringSegment : IStringSegmentDecoder
  {
    internal static readonly IStringSegmentDecoder Instance = (IStringSegmentDecoder) new FixStringSegment();

    private FixStringSegment()
    {
    }

    public ArraySegment<byte> Read(byte[] bytes, int offset, out int readSize)
    {
      int count = (int) bytes[offset] & 31;
      readSize = count + 1;
      return new ArraySegment<byte>(bytes, offset + 1, count);
    }
  }
}
