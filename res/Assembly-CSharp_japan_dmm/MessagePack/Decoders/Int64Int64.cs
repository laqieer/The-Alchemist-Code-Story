// Decompiled with JetBrains decompiler
// Type: MessagePack.Decoders.Int64Int64
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Decoders
{
  internal sealed class Int64Int64 : IInt64Decoder
  {
    internal static readonly IInt64Decoder Instance = (IInt64Decoder) new Int64Int64();

    private Int64Int64()
    {
    }

    public long Read(byte[] bytes, int offset, out int readSize)
    {
      readSize = 9;
      return (long) bytes[offset + 1] << 56 | (long) bytes[offset + 2] << 48 | (long) bytes[offset + 3] << 40 | (long) bytes[offset + 4] << 32 | (long) bytes[offset + 5] << 24 | (long) bytes[offset + 6] << 16 | (long) bytes[offset + 7] << 8 | (long) bytes[offset + 8];
    }
  }
}
