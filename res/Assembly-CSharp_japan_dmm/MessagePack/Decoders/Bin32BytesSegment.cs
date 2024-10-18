// Decompiled with JetBrains decompiler
// Type: MessagePack.Decoders.Bin32BytesSegment
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace MessagePack.Decoders
{
  internal sealed class Bin32BytesSegment : IBytesSegmentDecoder
  {
    internal static readonly IBytesSegmentDecoder Instance = (IBytesSegmentDecoder) new Bin32BytesSegment();

    private Bin32BytesSegment()
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
