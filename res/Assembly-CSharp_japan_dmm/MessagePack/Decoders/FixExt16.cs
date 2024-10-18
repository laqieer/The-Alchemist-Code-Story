// Decompiled with JetBrains decompiler
// Type: MessagePack.Decoders.FixExt16
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Decoders
{
  internal sealed class FixExt16 : IExtDecoder
  {
    internal static readonly IExtDecoder Instance = (IExtDecoder) new FixExt16();

    private FixExt16()
    {
    }

    public ExtensionResult Read(byte[] bytes, int offset, out int readSize)
    {
      readSize = 18;
      return new ExtensionResult((sbyte) bytes[offset + 1], new byte[16]
      {
        bytes[offset + 2],
        bytes[offset + 3],
        bytes[offset + 4],
        bytes[offset + 5],
        bytes[offset + 6],
        bytes[offset + 7],
        bytes[offset + 8],
        bytes[offset + 9],
        bytes[offset + 10],
        bytes[offset + 11],
        bytes[offset + 12],
        bytes[offset + 13],
        bytes[offset + 14],
        bytes[offset + 15],
        bytes[offset + 16],
        bytes[offset + 17]
      });
    }
  }
}
