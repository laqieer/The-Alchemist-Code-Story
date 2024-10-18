// Decompiled with JetBrains decompiler
// Type: MessagePack.Decoders.Str8String
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Decoders
{
  internal sealed class Str8String : IStringDecoder
  {
    internal static readonly IStringDecoder Instance = (IStringDecoder) new Str8String();

    private Str8String()
    {
    }

    public string Read(byte[] bytes, int offset, out int readSize)
    {
      int count = (int) bytes[offset + 1];
      readSize = count + 2;
      return StringEncoding.UTF8.GetString(bytes, offset + 2, count);
    }
  }
}
