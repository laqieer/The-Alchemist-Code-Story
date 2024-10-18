// Decompiled with JetBrains decompiler
// Type: MessagePack.Decoders.FixSByte
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Decoders
{
  internal sealed class FixSByte : ISByteDecoder
  {
    internal static readonly ISByteDecoder Instance = (ISByteDecoder) new FixSByte();

    private FixSByte()
    {
    }

    public sbyte Read(byte[] bytes, int offset, out int readSize)
    {
      readSize = 1;
      return (sbyte) bytes[offset];
    }
  }
}
