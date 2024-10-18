﻿// Decompiled with JetBrains decompiler
// Type: SRPG.MyEncrypt
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Text;

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
      byte[] source = new byte[msg.Length];
      Array.Copy((Array) msg, (Array) source, msg.Length);
      GUtility.Encrypt(source, source.Length);
      return source;
    }

    public static byte[] Decrypt(byte[] data)
    {
      if (data == null)
        return (byte[]) null;
      byte[] source = new byte[data.Length];
      Array.Copy((Array) data, (Array) source, data.Length);
      GUtility.Decrypt(source, source.Length);
      return source;
    }
  }
}
