// Decompiled with JetBrains decompiler
// Type: MessagePack.Decoders.Str16String
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Decoders
{
  internal sealed class Str16String : IStringDecoder
  {
    internal static readonly IStringDecoder Instance = (IStringDecoder) new Str16String();

    private Str16String()
    {
    }

    public string Read(byte[] bytes, int offset, out int readSize)
    {
      int count = ((int) bytes[offset + 1] << 8) + (int) bytes[offset + 2];
      readSize = count + 3;
      return StringEncoding.UTF8.GetString(bytes, offset + 3, count);
    }
  }
}
