// Decompiled with JetBrains decompiler
// Type: SRPG.VersusEnableTimeParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class VersusEnableTimeParam
  {
    private int mScheduleId;
    private VERSUS_TYPE mVersusType;
    private DateTime mBeginAt;
    private DateTime mEndAt;
    private int mDraftId;
    private VersusDraftType mDraftType;
    private List<int> mFriendDraftIdList;
    private List<VersusEnableTimeScheduleParam> mSchedule;

    public int ScheduleId => this.mScheduleId;

    public VERSUS_TYPE VersusType => this.mVersusType;

    public DateTime BeginAt => this.mBeginAt;

    public DateTime EndAt => this.mEndAt;

    public int DraftId => this.mDraftId;

    public VersusDraftType DraftType => this.mDraftType;

    public List<int> FriendDraftIdList => this.mFriendDraftIdList;

    public List<VersusEnableTimeScheduleParam> Schedule => this.mSchedule;

    public bool Deserialize(JSON_VersusEnableTimeParam json)
    {
      if (json == null)
        return false;
      this.mScheduleId = json.id;
      this.mVersusType = (VERSUS_TYPE) json.mode;
      try
      {
        if (!string.IsNullOrEmpty(json.begin_at))
          this.mBeginAt = DateTime.Parse(json.begin_at);
        if (!string.IsNullOrEmpty(json.end_at))
          this.mEndAt = DateTime.Parse(json.end_at);
      }
      catch (Exception ex)
      {
        DebugUtility.LogError(ex.Message);
        return false;
      }
      this.mDraftId = json.draft_id;
      this.mDraftType = (VersusDraftType) json.draft_type;
      this.mFriendDraftIdList = new List<int>();
      if (json.friend_draft_ids != null)
        this.mFriendDraftIdList.AddRange((IEnumerable<int>) json.friend_draft_ids);
      this.mSchedule = new List<VersusEnableTimeScheduleParam>();
      for (int index = 0; index < json.schedule.Length; ++index)
      {
        VersusEnableTimeScheduleParam timeScheduleParam = new VersusEnableTimeScheduleParam();
        if (timeScheduleParam.Deserialize(json.schedule[index]))
          this.mSchedule.Add(timeScheduleParam);
      }
      return true;
    }
  }
}
