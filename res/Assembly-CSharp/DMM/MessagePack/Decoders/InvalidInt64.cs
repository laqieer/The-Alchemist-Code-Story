﻿// Decompiled with JetBrains decompiler
// Type: MessagePack.Decoders.InvalidInt64
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace MessagePack.Decoders
{
  internal sealed class InvalidInt64 : IInt64Decoder
  {
    internal static readonly IInt64Decoder Instance = (IInt64Decoder) new InvalidInt64();

    private InvalidInt64()
    {
    }

    public long Read(byte[] bytes, int offset, out int readSize)
    {
      throw new InvalidOperationException(string.Format("code is invalid. code:{0} format:{1}", (object) bytes[offset], (object) MessagePackCode.ToFormatName(bytes[offset])));
    }
  }
}
