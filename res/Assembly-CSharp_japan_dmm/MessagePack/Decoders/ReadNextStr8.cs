// Decompiled with JetBrains decompiler
// Type: MessagePack.Decoders.ReadNextStr8
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Decoders
{
  internal sealed class ReadNextStr8 : IReadNextDecoder
  {
    internal static readonly IReadNextDecoder Instance = (IReadNextDecoder) new ReadNextStr8();

    private ReadNextStr8()
    {
    }

    public int Read(byte[] bytes, int offset) => (int) bytes[offset + 1] + 2;
  }
}
