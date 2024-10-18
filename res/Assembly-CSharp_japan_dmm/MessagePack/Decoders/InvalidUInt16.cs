// Decompiled with JetBrains decompiler
// Type: MessagePack.Decoders.InvalidUInt16
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace MessagePack.Decoders
{
  internal sealed class InvalidUInt16 : IUInt16Decoder
  {
    internal static readonly IUInt16Decoder Instance = (IUInt16Decoder) new InvalidUInt16();

    private InvalidUInt16()
    {
    }

    public ushort Read(byte[] bytes, int offset, out int readSize)
    {
      throw new InvalidOperationException(string.Format("code is invalid. code:{0} format:{1}", (object) bytes[offset], (object) MessagePackCode.ToFormatName(bytes[offset])));
    }
  }
}
