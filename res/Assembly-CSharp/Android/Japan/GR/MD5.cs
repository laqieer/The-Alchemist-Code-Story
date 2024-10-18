// Decompiled with JetBrains decompiler
// Type: GR.MD5
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Security.Cryptography;
using System.Text;

namespace GR
{
  public sealed class MD5
  {
    private MD5 mMD5;

    ~MD5()
    {
      this.Clear();
    }

    public void Create()
    {
      if (this.mMD5 != null)
        return;
      this.mMD5 = MD5.Create();
    }

    public void Clear()
    {
      if (this.mMD5 == null)
        return;
      this.mMD5.Clear();
      this.mMD5 = (MD5) null;
    }

    public string Calc(string str, Encoding encode)
    {
      return this.Calc(encode.GetBytes(str));
    }

    public string Calc(byte[] src)
    {
      if (this.mMD5 == null)
        return (string) null;
      byte[] hash = this.mMD5.ComputeHash(src);
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < hash.Length; ++index)
        stringBuilder.Append(hash[index].ToString("x2"));
      return stringBuilder.ToString();
    }
  }
}
