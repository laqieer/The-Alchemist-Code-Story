// Decompiled with JetBrains decompiler
// Type: SRPG.GuildEntryConditions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class GuildEntryConditions
  {
    private int mLowerLevel;
    private bool mIsAutoApproval;
    private string mComment;
    private int mPolicy;

    public GuildEntryConditions()
    {
      this.mLowerLevel = 0;
      this.mIsAutoApproval = false;
      this.mComment = string.Empty;
      this.mPolicy = 0;
    }

    public int LowerLevel
    {
      get => this.mLowerLevel;
      set => this.mLowerLevel = value;
    }

    public bool IsAutoApproval
    {
      get => this.mIsAutoApproval;
      set => this.mIsAutoApproval = value;
    }

    public string Comment
    {
      get => this.mComment;
      set => this.mComment = value;
    }

    public int Policy
    {
      get => this.mPolicy;
      set => this.mPolicy = value;
    }

    public bool Deserialize(JSON_GuildEntryCondition json)
    {
      this.mLowerLevel = json.lower_level;
      this.mIsAutoApproval = json.is_auto_approval != 0;
      this.mComment = !string.IsNullOrEmpty(json.recruit_comment) ? json.recruit_comment : string.Empty;
      this.mPolicy = json.policy;
      return true;
    }
  }
}
