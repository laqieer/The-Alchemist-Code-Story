// Decompiled with JetBrains decompiler
// Type: MessagePack.Decoders.Bin32Bytes
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace MessagePack.Decoders
{
  internal sealed class Bin32Bytes : IBytesDecoder
  {
    internal static readonly IBytesDecoder Instance = (IBytesDecoder) new Bin32Bytes();

    private Bin32Bytes()
    {
    }

    public byte[] Read(byte[] bytes, int offset, out int readSize)
    {
      int count = (int) bytes[offset + 1] << 24 | (int) bytes[offset + 2] << 16 | (int) bytes[offset + 3] << 8 | (int) bytes[offset + 4];
      byte[] dst = new byte[count];
      Buffer.BlockCopy((Array) bytes, offset + 5, (Array) dst, 0, count);
      readSize = count + 5;
      return dst;
    }
  }
}
