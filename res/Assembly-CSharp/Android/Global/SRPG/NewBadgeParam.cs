// Decompiled with JetBrains decompiler
// Type: SRPG.NewBadgeParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class NewBadgeParam
  {
    private bool mIsUseNewFlag;
    private bool mIsNew;
    private NewBadgeType mType;

    public NewBadgeParam(bool use, bool isnew, NewBadgeType type)
    {
      this.mIsUseNewFlag = use;
      this.mIsNew = isnew;
      this.mType = type;
    }

    public bool use_newflag
    {
      get
      {
        return this.mIsUseNewFlag;
      }
    }

    public bool is_new
    {
      get
      {
        return this.mIsNew;
      }
    }

    public NewBadgeType type
    {
      get
      {
        return this.mType;
      }
    }
  }
}
