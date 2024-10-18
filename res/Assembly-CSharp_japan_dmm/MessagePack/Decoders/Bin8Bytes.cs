// Decompiled with JetBrains decompiler
// Type: MessagePack.Decoders.Bin8Bytes
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace MessagePack.Decoders
{
  internal sealed class Bin8Bytes : IBytesDecoder
  {
    internal static readonly IBytesDecoder Instance = (IBytesDecoder) new Bin8Bytes();

    private Bin8Bytes()
    {
    }

    public byte[] Read(byte[] bytes, int offset, out int readSize)
    {
      byte count = bytes[offset + 1];
      byte[] dst = new byte[(int) count];
      Buffer.BlockCopy((Array) bytes, offset + 2, (Array) dst, 0, (int) count);
      readSize = (int) count + 2;
      return dst;
    }
  }
}
