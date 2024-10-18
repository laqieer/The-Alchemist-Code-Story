// Decompiled with JetBrains decompiler
// Type: MessagePack.Decoders.Ext32
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace MessagePack.Decoders
{
  internal sealed class Ext32 : IExtDecoder
  {
    internal static readonly IExtDecoder Instance = (IExtDecoder) new Ext32();

    private Ext32()
    {
    }

    public ExtensionResult Read(byte[] bytes, int offset, out int readSize)
    {
      uint count = (uint) ((int) bytes[offset + 1] << 24 | (int) bytes[offset + 2] << 16 | (int) bytes[offset + 3] << 8) | (uint) bytes[offset + 4];
      sbyte typeCode = (sbyte) bytes[offset + 5];
      byte[] numArray = new byte[(IntPtr) count];
      readSize = checked ((int) count + 6);
      Buffer.BlockCopy((Array) bytes, checked (offset + 6), (Array) numArray, 0, checked ((int) count));
      return new ExtensionResult(typeCode, numArray);
    }
  }
}
