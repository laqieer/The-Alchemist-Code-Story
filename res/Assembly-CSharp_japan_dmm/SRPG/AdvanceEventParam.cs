// Decompiled with JetBrains decompiler
// Type: SRPG.AdvanceEventParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class AdvanceEventParam
  {
    private const long TIME_MIN = 0;
    private const long TIME_MAX = 2147483647;
    private string mIname;
    private eTransType mTransType;
    private int mPriority;
    private string mAreaId;
    private string mName;
    private string mBoxIname;
    private int mEventUiIndex;
    private string mEventBanner;
    private string mEventDetailUrl;
    private string mBossHintUrl;
    private AdvanceEventModeInfoParam[] mModeInfo;
    private ChapterParam mChapterParam;

    public string Iname => this.mIname;

    public eTransType TransType => this.mTransType;

    public int Priority => this.mPriority;

    public string AreaId => this.mAreaId;

    public string Name => this.mName;

    public string BoxIname => this.mBoxIname;

    public int EventUiIndex => this.mEventUiIndex;

    public string EventBanner => this.mEventBanner;

    public string EventDetailUrl => this.mEventDetailUrl;

    public string BossHintUrl => this.mBossHintUrl;

    public ChapterParam ChapterParam
    {
      get
      {
        if (this.mChapterParam == null && UnityEngine.Object.op_Implicit((UnityEngine.Object) MonoSingleton<GameManager>.Instance))
          this.mChapterParam = MonoSingleton<GameManager>.Instance.FindArea(this.mAreaId);
        return this.mChapterParam;
      }
    }

    public void Deserialize(JSON_AdvanceEventParam json)
    {
      if (json == null)
        return;
      this.mIname = json.iname;
      this.mTransType = (eTransType) json.trans_type;
      this.mPriority = json.priority;
      this.mAreaId = json.area_id;
      this.mName = json.name;
      this.mBoxIname = json.box_iname;
      this.mEventUiIndex = json.event_ui_index;
      this.mEventBanner = json.event_banner;
      this.mEventDetailUrl = json.event_detail_url;
      this.mBossHintUrl = json.boss_hint_url;
      this.mModeInfo = (AdvanceEventModeInfoParam[]) null;
      if (json.mode_info == null || json.mode_info.Length == 0)
        return;
      this.mModeInfo = new AdvanceEventModeInfoParam[json.mode_info.Length];
      for (int index = 0; index < json.mode_info.Length; ++index)
      {
        this.mModeInfo[index] = new AdvanceEventModeInfoParam();
        this.mModeInfo[index].Deserialize(json.mode_info[index]);
      }
    }

    public List<QuestParam> GetQuestList(QuestDifficulties difficulty = QuestDifficulties.MAX, bool is_no_check_unlock = false)
    {
      return AdvanceEventParam.GetQuestTypeList(QuestTypes.AdvanceStory, this.mAreaId, difficulty, is_no_check_unlock);
    }

    public QuestParam GetBossQuest(QuestDifficulties difficulty, bool is_no_check_unlock = false)
    {
      List<QuestParam> questTypeList = AdvanceEventParam.GetQuestTypeList(QuestTypes.AdvanceBoss, this.mAreaId, difficulty, is_no_check_unlock);
      return questTypeList.Count != 0 ? questTypeList[0] : (QuestParam) null;
    }

    public AdvanceEventModeInfoParam GetModeInfo(QuestDifficulties difficulty)
    {
      if (this.mModeInfo == null)
        return (AdvanceEventModeInfoParam) null;
      int index = (int) difficulty;
      return index < 0 || index >= this.mModeInfo.Length ? (AdvanceEventModeInfoParam) null : this.mModeInfo[index];
    }

    public int GetMaxStarNum(List<QuestParam> quest_list)
    {
      int maxStarNum = 0;
      if (quest_list != null)
      {
        for (int index = 0; index < quest_list.Count; ++index)
        {
          QuestParam quest = quest_list[index];
          if (quest != null && quest.bonusObjective != null)
            maxStarNum += this.GetMaxStarNum(quest);
        }
      }
      return maxStarNum;
    }

    public int GetNowStarNum(List<QuestParam> quest_list)
    {
      int nowStarNum = 0;
      if (quest_list != null)
      {
        for (int index = 0; index < quest_list.Count; ++index)
        {
          QuestParam quest = quest_list[index];
          if (quest != null && quest.bonusObjective != null)
            nowStarNum += this.GetNowStarNum(quest);
        }
      }
      return nowStarNum;
    }

    public int GetMaxStarNum(QuestParam quest_param)
    {
      return quest_param == null || quest_param.bonusObjective == null ? 0 : quest_param.bonusObjective.Length;
    }

    public int GetNowStarNum(QuestParam quest_param)
    {
      if (quest_param == null || quest_param.bonusObjective == null)
        return 0;
      int nowStarNum = 0;
      for (int index = 0; index < quest_param.bonusObjective.Length; ++index)
      {
        if (quest_param.IsMissionClear(index))
          ++nowStarNum;
      }
      return nowStarNum;
    }

    public bool IsBossLiberation(QuestDifficulties difficulty)
    {
      QuestParam bossQuest = this.GetBossQuest(difficulty);
      if (bossQuest == null)
        return false;
      if (bossQuest.cond_quests != null && bossQuest.cond_quests.Length > 0)
        return bossQuest.IsQuestCondition();
      AdvanceEventModeInfoParam modeInfo = this.GetModeInfo(difficulty);
      if (modeInfo == null)
        return false;
      List<QuestParam> questList = this.GetQuestList(difficulty);
      if (questList.Count == 0)
        return false;
      int index = modeInfo.LiberationQuestNo - 1;
      if (index < 0 || index >= questList.Count)
        index = questList.Count - 1;
      return questList[index].state == QuestStates.Cleared;
    }

    public bool IsBossCondQuests(QuestDifficulties difficulty)
    {
      QuestParam bossQuest = this.GetBossQuest(difficulty);
      return bossQuest != null && bossQuest.cond_quests != null && bossQuest.cond_quests.Length > 0;
    }

    public static void Deserialize(ref List<AdvanceEventParam> list, JSON_AdvanceEventParam[] json)
    {
      if (json == null)
        return;
      if (list == null)
        list = new List<AdvanceEventParam>(json.Length);
      list.Clear();
      for (int index = 0; index < json.Length; ++index)
      {
        AdvanceEventParam advanceEventParam = new AdvanceEventParam();
        advanceEventParam.Deserialize(json[index]);
        list.Add(advanceEventParam);
      }
      SortUtility.StableSort<AdvanceEventParam>(list, (Comparison<AdvanceEventParam>) ((u1, u2) => u1.mPriority - u2.mPriority));
    }

    public static List<QuestParam> GetQuestTypeList(
      QuestTypes quest_type,
      string chapter_id,
      QuestDifficulties difficulty = QuestDifficulties.MAX,
      bool is_no_check_unlock = false)
    {
      List<QuestParam> questTypeList = new List<QuestParam>();
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) instance))
      {
        ChapterParam area = instance.FindArea(chapter_id);
        if (area != null && area.quests != null)
        {
          for (int index = 0; index < area.quests.Count; ++index)
          {
            QuestParam quest = area.quests[index];
            if (quest.type == quest_type && (difficulty == QuestDifficulties.MAX || quest.difficulty == difficulty) && (is_no_check_unlock || quest.IsDateUnlock()))
              questTypeList.Add(quest);
          }
        }
      }
      return questTypeList;
    }

    public static bool IsWithinPeriod(string chapter_id = null)
    {
      if (string.IsNullOrEmpty(chapter_id))
        return AdvanceEventParam.ExistsAvailableEventFromAll();
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) instance))
        return false;
      ChapterParam area = instance.FindArea(chapter_id);
      if (area != null)
        return area.IsDateUnlock(Network.GetServerTime());
      DebugUtility.LogError("[" + chapter_id + "] は無効なチャプターIDです");
      return false;
    }

    private static bool ExistsAvailableEventFromAll()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance, (UnityEngine.Object) null) || instance.Chapters == null)
        return false;
      for (int index = 0; index < instance.Chapters.Length; ++index)
      {
        ChapterParam chapter = instance.Chapters[index];
        if (chapter != null && !(chapter.section != "WD_ADVANCE") && chapter.IsDateUnlock(Network.GetServerTime()))
          return true;
      }
      return false;
    }

    public static DateTime GetStartDateTime(string chapter_id)
    {
      long unixtime = 0;
      if (!string.IsNullOrEmpty(chapter_id))
      {
        GameManager instance = MonoSingleton<GameManager>.Instance;
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) instance))
        {
          ChapterParam area = instance.FindArea(chapter_id);
          if (area != null)
            unixtime = area.start;
        }
      }
      return TimeManager.FromUnixTime(unixtime);
    }

    public static DateTime GetEndDateTime(string chapter_id)
    {
      long unixtime = 0;
      if (!string.IsNullOrEmpty(chapter_id))
      {
        GameManager instance = MonoSingleton<GameManager>.Instance;
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) instance))
        {
          ChapterParam area = instance.FindArea(chapter_id);
          if (area != null)
            unixtime = area.end;
        }
      }
      return TimeManager.FromUnixTime(unixtime);
    }
  }
}
