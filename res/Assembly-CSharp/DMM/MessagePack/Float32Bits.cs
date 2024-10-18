// Decompiled with JetBrains decompiler
// Type: MessagePack.Float32Bits
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace MessagePack
{
  [StructLayout(LayoutKind.Explicit)]
  internal struct Float32Bits
  {
    [FieldOffset(0)]
    public readonly float Value;
    [FieldOffset(0)]
    public readonly byte Byte0;
    [FieldOffset(1)]
    public readonly byte Byte1;
    [FieldOffset(2)]
    public readonly byte Byte2;
    [FieldOffset(3)]
    public readonly byte Byte3;

    public Float32Bits(float value)
    {
      this = new Float32Bits();
      this.Value = value;
    }

    public Float32Bits(byte[] bigEndianBytes, int offset)
    {
      this = new Float32Bits();
      if (BitConverter.IsLittleEndian)
      {
        this.Byte0 = bigEndianBytes[offset + 3];
        this.Byte1 = bigEndianBytes[offset + 2];
        this.Byte2 = bigEndianBytes[offset + 1];
        this.Byte3 = bigEndianBytes[offset];
      }
      else
      {
        this.Byte0 = bigEndianBytes[offset];
        this.Byte1 = bigEndianBytes[offset + 1];
        this.Byte2 = bigEndianBytes[offset + 2];
        this.Byte3 = bigEndianBytes[offset + 3];
      }
    }
  }
}
