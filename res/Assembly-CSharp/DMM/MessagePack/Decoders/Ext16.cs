// Decompiled with JetBrains decompiler
// Type: MessagePack.Decoders.Ext16
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace MessagePack.Decoders
{
  internal sealed class Ext16 : IExtDecoder
  {
    internal static readonly IExtDecoder Instance = (IExtDecoder) new Ext16();

    private Ext16()
    {
    }

    public ExtensionResult Read(byte[] bytes, int offset, out int readSize)
    {
      int count = (int) (ushort) ((uint) bytes[offset + 1] << 8) | (int) bytes[offset + 2];
      sbyte typeCode = (sbyte) bytes[offset + 3];
      byte[] numArray = new byte[count];
      readSize = count + 4;
      Buffer.BlockCopy((Array) bytes, offset + 4, (Array) numArray, 0, count);
      return new ExtensionResult(typeCode, numArray);
    }
  }
}
