// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.ObscuredTypes.BitconverterExt
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace CodeStage.AntiCheat.ObscuredTypes
{
  internal class BitconverterExt
  {
    public static byte[] GetBytes(Decimal dec)
    {
      int[] bits = Decimal.GetBits(dec);
      List<byte> byteList = new List<byte>();
      foreach (int num in bits)
        byteList.AddRange((IEnumerable<byte>) BitConverter.GetBytes(num));
      return byteList.ToArray();
    }

    public static Decimal ToDecimal(byte[] bytes)
    {
      if (bytes.Length != 16)
        throw new Exception("[ACTk] A decimal must be created from exactly 16 bytes");
      int[] bits = new int[4];
      for (int startIndex = 0; startIndex <= 15; startIndex += 4)
        bits[startIndex / 4] = BitConverter.ToInt32(bytes, startIndex);
      return new Decimal(bits);
    }
  }
}
