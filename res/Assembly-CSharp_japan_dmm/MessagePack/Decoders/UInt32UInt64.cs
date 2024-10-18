// Decompiled with JetBrains decompiler
// Type: MessagePack.Decoders.UInt32UInt64
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Decoders
{
  internal sealed class UInt32UInt64 : IUInt64Decoder
  {
    internal static readonly IUInt64Decoder Instance = (IUInt64Decoder) new UInt32UInt64();

    private UInt32UInt64()
    {
    }

    public ulong Read(byte[] bytes, int offset, out int readSize)
    {
      readSize = 5;
      return ((ulong) bytes[offset + 1] << 24) + (ulong) ((int) bytes[offset + 2] << 16) + (ulong) ((int) bytes[offset + 3] << 8) + (ulong) bytes[offset + 4];
    }
  }
}
