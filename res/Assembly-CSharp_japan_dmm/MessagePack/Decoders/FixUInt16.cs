// Decompiled with JetBrains decompiler
// Type: MessagePack.Decoders.FixUInt16
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Decoders
{
  internal sealed class FixUInt16 : IUInt16Decoder
  {
    internal static readonly IUInt16Decoder Instance = (IUInt16Decoder) new FixUInt16();

    private FixUInt16()
    {
    }

    public ushort Read(byte[] bytes, int offset, out int readSize)
    {
      readSize = 1;
      return (ushort) bytes[offset];
    }
  }
}
