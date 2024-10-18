// Decompiled with JetBrains decompiler
// Type: SRPG.GuildEntryConditions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class GuildEntryConditions
  {
    private int mLowerLevel;
    private bool mIsAutoApproval;
    private string mComment;

    public GuildEntryConditions()
    {
      this.mLowerLevel = 0;
      this.mIsAutoApproval = false;
      this.mComment = string.Empty;
    }

    public int LowerLevel
    {
      get
      {
        return this.mLowerLevel;
      }
      set
      {
        this.mLowerLevel = value;
      }
    }

    public bool IsAutoApproval
    {
      get
      {
        return this.mIsAutoApproval;
      }
      set
      {
        this.mIsAutoApproval = value;
      }
    }

    public string Comment
    {
      get
      {
        return this.mComment;
      }
      set
      {
        this.mComment = value;
      }
    }

    public bool Deserialize(JSON_GuildEntryCondition json)
    {
      this.mLowerLevel = json.lower_level;
      this.mIsAutoApproval = json.is_auto_approval != 0;
      this.mComment = !string.IsNullOrEmpty(json.recruit_comment) ? json.recruit_comment : string.Empty;
      return true;
    }
  }
}
