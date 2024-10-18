﻿// Decompiled with JetBrains decompiler
// Type: MessagePack.Decoders.Int8Int64
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Decoders
{
  internal sealed class Int8Int64 : IInt64Decoder
  {
    internal static readonly IInt64Decoder Instance = (IInt64Decoder) new Int8Int64();

    private Int8Int64()
    {
    }

    public long Read(byte[] bytes, int offset, out int readSize)
    {
      readSize = 2;
      return (long) (sbyte) bytes[offset + 1];
    }
  }
}
