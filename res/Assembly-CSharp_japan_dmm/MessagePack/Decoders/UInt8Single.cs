// Decompiled with JetBrains decompiler
// Type: MessagePack.Decoders.UInt8Single
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Decoders
{
  internal sealed class UInt8Single : ISingleDecoder
  {
    internal static readonly ISingleDecoder Instance = (ISingleDecoder) new UInt8Single();

    private UInt8Single()
    {
    }

    public float Read(byte[] bytes, int offset, out int readSize)
    {
      return (float) UInt8Byte.Instance.Read(bytes, offset, out readSize);
    }
  }
}
