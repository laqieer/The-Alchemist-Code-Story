﻿// Decompiled with JetBrains decompiler
// Type: MessagePack.Decoders.Float64Double
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Decoders
{
  internal sealed class Float64Double : IDoubleDecoder
  {
    internal static readonly IDoubleDecoder Instance = (IDoubleDecoder) new Float64Double();

    private Float64Double()
    {
    }

    public double Read(byte[] bytes, int offset, out int readSize)
    {
      readSize = 9;
      return new Float64Bits(bytes, offset + 1).Value;
    }
  }
}