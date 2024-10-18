// Decompiled with JetBrains decompiler
// Type: MessagePack.Decoders.Map16Header
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Decoders
{
  internal sealed class Map16Header : IMapHeaderDecoder
  {
    internal static readonly IMapHeaderDecoder Instance = (IMapHeaderDecoder) new Map16Header();

    private Map16Header()
    {
    }

    public uint Read(byte[] bytes, int offset, out int readSize)
    {
      readSize = 3;
      return (uint) bytes[offset + 1] << 8 | (uint) bytes[offset + 2];
    }
  }
}
