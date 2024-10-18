// Decompiled with JetBrains decompiler
// Type: MessagePack.Decoders.FixExt2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Decoders
{
  internal sealed class FixExt2 : IExtDecoder
  {
    internal static readonly IExtDecoder Instance = (IExtDecoder) new FixExt2();

    private FixExt2()
    {
    }

    public ExtensionResult Read(byte[] bytes, int offset, out int readSize)
    {
      readSize = 4;
      return new ExtensionResult((sbyte) bytes[offset + 1], new byte[2]
      {
        bytes[offset + 2],
        bytes[offset + 3]
      });
    }
  }
}
