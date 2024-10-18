﻿// Decompiled with JetBrains decompiler
// Type: GR.SHA256
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Text;

namespace GR
{
  public sealed class SHA256
  {
    private System.Security.Cryptography.SHA256 mSHA256;

    ~SHA256()
    {
      this.Clear();
    }

    public void Create()
    {
      if (this.mSHA256 != null)
        return;
      this.mSHA256 = System.Security.Cryptography.SHA256.Create();
    }

    public void Clear()
    {
      if (this.mSHA256 == null)
        return;
      this.mSHA256.Clear();
      this.mSHA256 = (System.Security.Cryptography.SHA256) null;
    }

    public string Calc(string str, Encoding encode)
    {
      return this.Calc(encode.GetBytes(str));
    }

    public string Calc(byte[] src)
    {
      if (this.mSHA256 == null)
        return (string) null;
      byte[] hash = this.mSHA256.ComputeHash(src);
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < hash.Length; ++index)
        stringBuilder.AppendFormat("{0:x2}", (object) hash[index]);
      return stringBuilder.ToString();
    }
  }
}
