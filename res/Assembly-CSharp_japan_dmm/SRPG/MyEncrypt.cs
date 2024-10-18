// Decompiled with JetBrains decompiler
// Type: SRPG.MyEncrypt
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Text;

#nullable disable
namespace SRPG
{
  public class MyEncrypt
  {
    public static int EncryptCount;
    public static int DecryptCount;

    public static byte[] Encrypt(int seed, string msg, bool compress = false)
    {
      if (string.IsNullOrEmpty(msg))
        return (byte[]) null;
      byte[] bytes = Encoding.UTF8.GetBytes(msg);
      GUtility.Encrypt(bytes, bytes.Length);
      return bytes;
    }

    public static string Decrypt(int seed, byte[] data, bool decompress = false)
    {
      if (data == null)
        return (string) null;
      GUtility.Decrypt(data, data.Length);
      return Encoding.UTF8.GetString(data);
    }

    public static byte[] Encrypt(byte[] msg)
    {
      if (msg == null)
        return (byte[]) null;
      byte[] destinationArray = new byte[msg.Length];
      Array.Copy((Array) msg, (Array) destinationArray, msg.Length);
      GUtility.Encrypt(destinationArray, destinationArray.Length);
      return destinationArray;
    }

    public static byte[] Decrypt(byte[] data)
    {
      if (data == null)
        return (byte[]) null;
      byte[] destinationArray = new byte[data.Length];
      Array.Copy((Array) data, (Array) destinationArray, data.Length);
      GUtility.Decrypt(destinationArray, destinationArray.Length);
      return destinationArray;
    }
  }
}
