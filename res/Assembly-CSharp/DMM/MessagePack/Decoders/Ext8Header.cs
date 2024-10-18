// Decompiled with JetBrains decompiler
// Type: MessagePack.Decoders.Ext8Header
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Decoders
{
  internal sealed class Ext8Header : IExtHeaderDecoder
  {
    internal static readonly IExtHeaderDecoder Instance = (IExtHeaderDecoder) new Ext8Header();

    private Ext8Header()
    {
    }

    public ExtensionHeader Read(byte[] bytes, int offset, out int readSize)
    {
      byte length = bytes[offset + 1];
      sbyte typeCode = (sbyte) bytes[offset + 2];
      readSize = 3;
      return new ExtensionHeader(typeCode, (uint) length);
    }
  }
}
