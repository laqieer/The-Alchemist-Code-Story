// Decompiled with JetBrains decompiler
// Type: MessagePack.Decoders.UInt32Int64
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Decoders
{
  internal sealed class UInt32Int64 : IInt64Decoder
  {
    internal static readonly IInt64Decoder Instance = (IInt64Decoder) new UInt32Int64();

    private UInt32Int64()
    {
    }

    public long Read(byte[] bytes, int offset, out int readSize)
    {
      readSize = 5;
      return (long) ((uint) ((int) bytes[offset + 1] << 24 | (int) bytes[offset + 2] << 16 | (int) bytes[offset + 3] << 8) | (uint) bytes[offset + 4]);
    }
  }
}
