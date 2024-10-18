// Decompiled with JetBrains decompiler
// Type: SRPG.NewBadgeParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
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

    public bool use_newflag => this.mIsUseNewFlag;

    public bool is_new => this.mIsNew;

    public NewBadgeType type => this.mType;
  }
}
