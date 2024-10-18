// Decompiled with JetBrains decompiler
// Type: MessagePack.Decoders.Ext8
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace MessagePack.Decoders
{
  internal sealed class Ext8 : IExtDecoder
  {
    internal static readonly IExtDecoder Instance = (IExtDecoder) new Ext8();

    private Ext8()
    {
    }

    public ExtensionResult Read(byte[] bytes, int offset, out int readSize)
    {
      byte count = bytes[offset + 1];
      sbyte typeCode = (sbyte) bytes[offset + 2];
      byte[] numArray = new byte[(int) count];
      readSize = (int) count + 3;
      Buffer.BlockCopy((Array) bytes, offset + 3, (Array) numArray, 0, (int) count);
      return new ExtensionResult(typeCode, numArray);
    }
  }
}
