// Decompiled with JetBrains decompiler
// Type: MessagePack.Internal.LZ4MemoryPool
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.LZ4;
using System;

#nullable disable
namespace MessagePack.Internal
{
  internal static class LZ4MemoryPool
  {
    [ThreadStatic]
    private static byte[] lz4buffer;

    public static byte[] GetBuffer()
    {
      if (LZ4MemoryPool.lz4buffer == null)
        LZ4MemoryPool.lz4buffer = new byte[LZ4Codec.MaximumOutputLength(65536)];
      return LZ4MemoryPool.lz4buffer;
    }
  }
}
