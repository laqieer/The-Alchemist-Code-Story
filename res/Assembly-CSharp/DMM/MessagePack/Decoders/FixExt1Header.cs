// Decompiled with JetBrains decompiler
// Type: MessagePack.Decoders.FixExt1Header
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Decoders
{
  internal sealed class FixExt1Header : IExtHeaderDecoder
  {
    internal static readonly IExtHeaderDecoder Instance = (IExtHeaderDecoder) new FixExt1Header();

    private FixExt1Header()
    {
    }

    public ExtensionHeader Read(byte[] bytes, int offset, out int readSize)
    {
      readSize = 2;
      return new ExtensionHeader((sbyte) bytes[offset + 1], 1U);
    }
  }
}
