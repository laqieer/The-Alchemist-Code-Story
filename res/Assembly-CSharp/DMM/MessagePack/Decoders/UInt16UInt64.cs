// Decompiled with JetBrains decompiler
// Type: MessagePack.Decoders.UInt16UInt64
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Decoders
{
  internal sealed class UInt16UInt64 : IUInt64Decoder
  {
    internal static readonly IUInt64Decoder Instance = (IUInt64Decoder) new UInt16UInt64();

    private UInt16UInt64()
    {
    }

    public ulong Read(byte[] bytes, int offset, out int readSize)
    {
      readSize = 3;
      return (ulong) ((int) bytes[offset + 1] << 8 | (int) bytes[offset + 2]);
    }
  }
}
