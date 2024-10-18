// Decompiled with JetBrains decompiler
// Type: SRPG.NewBadgeParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
