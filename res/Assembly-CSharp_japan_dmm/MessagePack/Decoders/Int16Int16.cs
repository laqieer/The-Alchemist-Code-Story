// Decompiled with JetBrains decompiler
// Type: MessagePack.Decoders.Int16Int16
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Decoders
{
  internal sealed class Int16Int16 : IInt16Decoder
  {
    internal static readonly IInt16Decoder Instance = (IInt16Decoder) new Int16Int16();

    private Int16Int16()
    {
    }

    public short Read(byte[] bytes, int offset, out int readSize)
    {
      readSize = 3;
      return (short) ((int) bytes[offset + 1] << 8 | (int) bytes[offset + 2]);
    }
  }
}
