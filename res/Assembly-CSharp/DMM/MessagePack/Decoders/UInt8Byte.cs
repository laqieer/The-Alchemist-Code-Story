// Decompiled with JetBrains decompiler
// Type: MessagePack.Decoders.UInt8Byte
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Decoders
{
  internal sealed class UInt8Byte : IByteDecoder
  {
    internal static readonly IByteDecoder Instance = (IByteDecoder) new UInt8Byte();

    private UInt8Byte()
    {
    }

    public byte Read(byte[] bytes, int offset, out int readSize)
    {
      readSize = 2;
      return bytes[offset + 1];
    }
  }
}
