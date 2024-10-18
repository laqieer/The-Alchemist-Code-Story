// Decompiled with JetBrains decompiler
// Type: MessagePack.Decoders.Bin16Bytes
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace MessagePack.Decoders
{
  internal sealed class Bin16Bytes : IBytesDecoder
  {
    internal static readonly IBytesDecoder Instance = (IBytesDecoder) new Bin16Bytes();

    private Bin16Bytes()
    {
    }

    public byte[] Read(byte[] bytes, int offset, out int readSize)
    {
      int count = ((int) bytes[offset + 1] << 8) + (int) bytes[offset + 2];
      byte[] dst = new byte[count];
      Buffer.BlockCopy((Array) bytes, offset + 3, (Array) dst, 0, count);
      readSize = count + 3;
      return dst;
    }
  }
}
