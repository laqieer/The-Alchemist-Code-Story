﻿// Decompiled with JetBrains decompiler
// Type: MessagePack.Decoders.Int64Double
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Decoders
{
  internal sealed class Int64Double : IDoubleDecoder
  {
    internal static readonly IDoubleDecoder Instance = (IDoubleDecoder) new Int64Double();

    private Int64Double()
    {
    }

    public double Read(byte[] bytes, int offset, out int readSize)
    {
      return (double) Int64Int64.Instance.Read(bytes, offset, out readSize);
    }
  }
}
