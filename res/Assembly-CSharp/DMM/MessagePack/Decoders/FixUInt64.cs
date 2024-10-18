// Decompiled with JetBrains decompiler
// Type: MessagePack.Decoders.FixUInt64
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Decoders
{
  internal sealed class FixUInt64 : IUInt64Decoder
  {
    internal static readonly IUInt64Decoder Instance = (IUInt64Decoder) new FixUInt64();

    private FixUInt64()
    {
    }

    public ulong Read(byte[] bytes, int offset, out int readSize)
    {
      readSize = 1;
      return (ulong) bytes[offset];
    }
  }
}
