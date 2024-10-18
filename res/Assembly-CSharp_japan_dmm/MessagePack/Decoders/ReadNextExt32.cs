﻿// Decompiled with JetBrains decompiler
// Type: MessagePack.Decoders.ReadNextExt32
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Decoders
{
  internal sealed class ReadNextExt32 : IReadNextDecoder
  {
    internal static readonly IReadNextDecoder Instance = (IReadNextDecoder) new ReadNextExt32();

    private ReadNextExt32()
    {
    }

    public int Read(byte[] bytes, int offset)
    {
      return (int) ((uint) ((int) bytes[offset + 1] << 24 | (int) bytes[offset + 2] << 16 | (int) bytes[offset + 3] << 8) | (uint) bytes[offset + 4]) + 6;
    }
  }
}
