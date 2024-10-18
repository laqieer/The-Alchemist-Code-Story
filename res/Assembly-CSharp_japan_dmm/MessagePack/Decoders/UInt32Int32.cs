// Decompiled with JetBrains decompiler
// Type: MessagePack.Decoders.UInt32Int32
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Decoders
{
  internal sealed class UInt32Int32 : IInt32Decoder
  {
    internal static readonly IInt32Decoder Instance = (IInt32Decoder) new UInt32Int32();

    private UInt32Int32()
    {
    }

    public int Read(byte[] bytes, int offset, out int readSize)
    {
      readSize = 5;
      return checked ((int) (uint) (unchecked ((int) checked ((uint) ((int) bytes[offset + 1] << 24))) | unchecked ((int) checked ((uint) ((int) bytes[offset + 2] << 16))) | unchecked ((int) checked ((uint) ((int) bytes[offset + 3] << 8))) | (int) bytes[offset + 4]));
    }
  }
}
