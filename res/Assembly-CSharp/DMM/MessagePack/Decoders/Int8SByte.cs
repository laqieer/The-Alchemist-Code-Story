// Decompiled with JetBrains decompiler
// Type: MessagePack.Decoders.Int8SByte
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Decoders
{
  internal sealed class Int8SByte : ISByteDecoder
  {
    internal static readonly ISByteDecoder Instance = (ISByteDecoder) new Int8SByte();

    private Int8SByte()
    {
    }

    public sbyte Read(byte[] bytes, int offset, out int readSize)
    {
      readSize = 2;
      return (sbyte) bytes[offset + 1];
    }
  }
}
