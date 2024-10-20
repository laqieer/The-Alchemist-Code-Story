﻿// Decompiled with JetBrains decompiler
// Type: MessagePack.Decoders.UInt8UInt32
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Decoders
{
  internal sealed class UInt8UInt32 : IUInt32Decoder
  {
    internal static readonly IUInt32Decoder Instance = (IUInt32Decoder) new UInt8UInt32();

    private UInt8UInt32()
    {
    }

    public uint Read(byte[] bytes, int offset, out int readSize)
    {
      readSize = 2;
      return (uint) bytes[offset + 1];
    }
  }
}