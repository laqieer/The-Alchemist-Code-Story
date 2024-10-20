﻿// Decompiled with JetBrains decompiler
// Type: MessagePack.Decoders.FixNegativeInt32
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Decoders
{
  internal sealed class FixNegativeInt32 : IInt32Decoder
  {
    internal static readonly IInt32Decoder Instance = (IInt32Decoder) new FixNegativeInt32();

    private FixNegativeInt32()
    {
    }

    public int Read(byte[] bytes, int offset, out int readSize)
    {
      readSize = 1;
      return (int) (sbyte) bytes[offset];
    }
  }
}