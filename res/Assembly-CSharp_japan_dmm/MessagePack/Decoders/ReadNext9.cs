﻿// Decompiled with JetBrains decompiler
// Type: MessagePack.Decoders.ReadNext9
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Decoders
{
  internal sealed class ReadNext9 : IReadNextDecoder
  {
    internal static readonly IReadNextDecoder Instance = (IReadNextDecoder) new ReadNext9();

    private ReadNext9()
    {
    }

    public int Read(byte[] bytes, int offset) => 9;
  }
}
