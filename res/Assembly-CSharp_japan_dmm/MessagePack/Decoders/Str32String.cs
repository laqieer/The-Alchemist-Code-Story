// Decompiled with JetBrains decompiler
// Type: MessagePack.Decoders.Str32String
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Decoders
{
  internal sealed class Str32String : IStringDecoder
  {
    internal static readonly IStringDecoder Instance = (IStringDecoder) new Str32String();

    private Str32String()
    {
    }

    public string Read(byte[] bytes, int offset, out int readSize)
    {
      int count = (int) bytes[offset + 1] << 24 | (int) bytes[offset + 2] << 16 | (int) bytes[offset + 3] << 8 | (int) bytes[offset + 4];
      readSize = count + 5;
      return StringEncoding.UTF8.GetString(bytes, offset + 5, count);
    }
  }
}
