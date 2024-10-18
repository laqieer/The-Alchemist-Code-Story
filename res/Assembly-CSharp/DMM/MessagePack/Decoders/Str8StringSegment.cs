// Decompiled with JetBrains decompiler
// Type: MessagePack.Decoders.Str8StringSegment
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace MessagePack.Decoders
{
  internal sealed class Str8StringSegment : IStringSegmentDecoder
  {
    internal static readonly IStringSegmentDecoder Instance = (IStringSegmentDecoder) new Str8StringSegment();

    private Str8StringSegment()
    {
    }

    public ArraySegment<byte> Read(byte[] bytes, int offset, out int readSize)
    {
      int count = (int) bytes[offset + 1];
      readSize = count + 2;
      return new ArraySegment<byte>(bytes, offset + 2, count);
    }
  }
}
