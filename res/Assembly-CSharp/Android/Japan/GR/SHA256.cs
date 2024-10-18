// Decompiled with JetBrains decompiler
// Type: GR.SHA256
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Security.Cryptography;
using System.Text;

namespace GR
{
  public sealed class SHA256
  {
    private SHA256 mSHA256;

    ~SHA256()
    {
      this.Clear();
    }

    public void Create()
    {
      if (this.mSHA256 != null)
        return;
      this.mSHA256 = SHA256.Create();
    }

    public void Clear()
    {
      if (this.mSHA256 == null)
        return;
      this.mSHA256.Clear();
      this.mSHA256 = (SHA256) null;
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
