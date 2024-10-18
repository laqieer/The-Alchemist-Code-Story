// Decompiled with JetBrains decompiler
// Type: MessagePack.Decoders.Bin8BytesSegment
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace MessagePack.Decoders
{
  internal sealed class Bin8BytesSegment : IBytesSegmentDecoder
  {
    internal static readonly IBytesSegmentDecoder Instance = (IBytesSegmentDecoder) new Bin8BytesSegment();

    private Bin8BytesSegment()
    {
    }

    public ArraySegment<byte> Read(byte[] bytes, int offset, out int readSize)
    {
      byte count = bytes[offset + 1];
      readSize = (int) count + 2;
      return new ArraySegment<byte>(bytes, offset + 2, (int) count);
    }
  }
}
