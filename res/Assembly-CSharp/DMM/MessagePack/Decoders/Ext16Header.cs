// Decompiled with JetBrains decompiler
// Type: MessagePack.Decoders.Ext16Header
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Decoders
{
  internal sealed class Ext16Header : IExtHeaderDecoder
  {
    internal static readonly IExtHeaderDecoder Instance = (IExtHeaderDecoder) new Ext16Header();

    private Ext16Header()
    {
    }

    public ExtensionHeader Read(byte[] bytes, int offset, out int readSize)
    {
      uint length = (uint) (ushort) ((uint) bytes[offset + 1] << 8) | (uint) bytes[offset + 2];
      sbyte typeCode = (sbyte) bytes[offset + 3];
      readSize = 4;
      return new ExtensionHeader(typeCode, length);
    }
  }
}
