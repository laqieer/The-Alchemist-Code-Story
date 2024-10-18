// Decompiled with JetBrains decompiler
// Type: MessagePack.Decoders.UInt64UInt64
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Decoders
{
  internal sealed class UInt64UInt64 : IUInt64Decoder
  {
    internal static readonly IUInt64Decoder Instance = (IUInt64Decoder) new UInt64UInt64();

    private UInt64UInt64()
    {
    }

    public ulong Read(byte[] bytes, int offset, out int readSize)
    {
      readSize = 9;
      return (ulong) ((long) bytes[offset + 1] << 56 | (long) bytes[offset + 2] << 48 | (long) bytes[offset + 3] << 40 | (long) bytes[offset + 4] << 32 | (long) bytes[offset + 5] << 24 | (long) bytes[offset + 6] << 16 | (long) bytes[offset + 7] << 8) | (ulong) bytes[offset + 8];
    }
  }
}
