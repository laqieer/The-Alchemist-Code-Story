// Decompiled with JetBrains decompiler
// Type: MessagePack.Internal.ByteArrayComparer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Internal
{
  public static class ByteArrayComparer
  {
    public static bool Equals(byte[] xs, int xsOffset, int xsCount, byte[] ys)
    {
      if (xs == null || ys == null || xsCount != ys.Length)
        return false;
      for (int index = 0; index < ys.Length; ++index)
      {
        if ((int) xs[xsOffset++] != (int) ys[index])
          return false;
      }
      return true;
    }

    public static bool Equals(
      byte[] xs,
      int xsOffset,
      int xsCount,
      byte[] ys,
      int ysOffset,
      int ysCount)
    {
      if (xs == null || ys == null || xsCount != ysCount)
        return false;
      for (int index = 0; index < xsCount; ++index)
      {
        if ((int) xs[xsOffset++] != (int) ys[ysOffset++])
          return false;
      }
      return true;
    }
  }
}
