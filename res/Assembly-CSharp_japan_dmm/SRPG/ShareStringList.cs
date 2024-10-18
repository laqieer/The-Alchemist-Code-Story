// Decompiled with JetBrains decompiler
// Type: SRPG.ShareStringList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;

#nullable disable
namespace SRPG
{
  public class ShareStringList
  {
    private short[] mIndexs;
    private ShareString.Type mType;

    public ShareStringList(ShareString.Type type) => this.mType = type;

    public int Length => this.mIndexs == null || this.mIndexs.Length < 0 ? 0 : this.mIndexs.Length;

    public void Setup(int length)
    {
      this.mIndexs = new short[length];
      for (int index = 0; index < length; ++index)
        this.mIndexs[index] = (short) -1;
    }

    public void Clear()
    {
      this.mIndexs = (short[]) null;
      this.mType = ShareString.Type.QuestParam_cond;
    }

    public bool IsNotNull() => this.mIndexs != null;

    public string[] GetList()
    {
      if (this.mIndexs == null || this.mIndexs.Length < 0)
        return (string[]) null;
      string[] list = new string[this.mIndexs.Length];
      for (int index = 0; index < this.mIndexs.Length; ++index)
        list[index] = Singleton<ShareVariable>.Instance.str.Get(this.mType, this.mIndexs[index]);
      return list;
    }

    public string Get(int index)
    {
      return this.mIndexs == null || index >= this.mIndexs.Length ? (string) null : Singleton<ShareVariable>.Instance.str.Get(this.mType, this.mIndexs[index]);
    }

    public void Set(int index, string value)
    {
      if (this.mIndexs == null || index >= this.mIndexs.Length)
        return;
      this.mIndexs[index] = Singleton<ShareVariable>.Instance.str.Set(this.mType, value);
    }
  }
}
