// Decompiled with JetBrains decompiler
// Type: MessagePack.Decoders.ReadNextBin16
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Decoders
{
  internal sealed class ReadNextBin16 : IReadNextDecoder
  {
    internal static readonly IReadNextDecoder Instance = (IReadNextDecoder) new ReadNextBin16();

    private ReadNextBin16()
    {
    }

    public int Read(byte[] bytes, int offset)
    {
      return ((int) bytes[offset + 1] << 8 | (int) bytes[offset + 2]) + 3;
    }
  }
}
