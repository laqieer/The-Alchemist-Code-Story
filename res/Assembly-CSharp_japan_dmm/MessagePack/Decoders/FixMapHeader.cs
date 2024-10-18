// Decompiled with JetBrains decompiler
// Type: MessagePack.Decoders.FixMapHeader
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Decoders
{
  internal sealed class FixMapHeader : IMapHeaderDecoder
  {
    internal static readonly IMapHeaderDecoder Instance = (IMapHeaderDecoder) new FixMapHeader();

    private FixMapHeader()
    {
    }

    public uint Read(byte[] bytes, int offset, out int readSize)
    {
      readSize = 1;
      return (uint) bytes[offset] & 15U;
    }
  }
}
