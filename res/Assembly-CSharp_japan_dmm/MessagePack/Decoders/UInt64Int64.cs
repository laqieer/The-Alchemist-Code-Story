// Decompiled with JetBrains decompiler
// Type: MessagePack.Decoders.UInt64Int64
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Decoders
{
  internal sealed class UInt64Int64 : IInt64Decoder
  {
    internal static readonly IInt64Decoder Instance = (IInt64Decoder) new UInt64Int64();

    private UInt64Int64()
    {
    }

    public long Read(byte[] bytes, int offset, out int readSize)
    {
      readSize = 9;
      return (long) bytes[checked (offset + 1)] << 56 | (long) bytes[checked (offset + 2)] << 48 | (long) bytes[checked (offset + 3)] << 40 | (long) bytes[checked (offset + 4)] << 32 | (long) bytes[checked (offset + 5)] << 24 | (long) bytes[checked (offset + 6)] << 16 | (long) bytes[checked (offset + 7)] << 8 | (long) bytes[checked (offset + 8)];
    }
  }
}
