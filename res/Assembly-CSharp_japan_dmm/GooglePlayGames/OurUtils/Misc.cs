// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.OurUtils.Misc
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace GooglePlayGames.OurUtils
{
  public static class Misc
  {
    public static bool BuffersAreIdentical(byte[] a, byte[] b)
    {
      if (a == b)
        return true;
      if (a == null || b == null || a.Length != b.Length)
        return false;
      for (int index = 0; index < a.Length; ++index)
      {
        if ((int) a[index] != (int) b[index])
          return false;
      }
      return true;
    }

    public static byte[] GetSubsetBytes(byte[] array, int offset, int length)
    {
      if (array == null)
        throw new ArgumentNullException(nameof (array));
      if (offset < 0 || offset >= array.Length)
        throw new ArgumentOutOfRangeException(nameof (offset));
      if (length < 0 || array.Length - offset < length)
        throw new ArgumentOutOfRangeException(nameof (length));
      if (offset == 0 && length == array.Length)
        return array;
      byte[] destinationArray = new byte[length];
      Array.Copy((Array) array, offset, (Array) destinationArray, 0, length);
      return destinationArray;
    }

    public static T CheckNotNull<T>(T value)
    {
      return (object) value != null ? value : throw new ArgumentNullException();
    }

    public static T CheckNotNull<T>(T value, string paramName)
    {
      return (object) value != null ? value : throw new ArgumentNullException(paramName);
    }
  }
}
