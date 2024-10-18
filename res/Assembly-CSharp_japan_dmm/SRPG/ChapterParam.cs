// Decompiled with JetBrains decompiler
// Type: SRPG.ChapterParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class ChapterParam
  {
    public string iname;
    public string name;
    public string expr;
    private short world_index = -1;
    public long start;
    public long end;
    public long key_end;
    private bool hidden;
    private short section_index = -1;
    public string banner;
    public string prefabPath;
    public ChapterParam parent;
    public List<ChapterParam> children = new List<ChapterParam>();
    public List<QuestParam> quests = new List<QuestParam>();
    public SectionParam sectionParam;
    public List<KeyItem> keys;
    public long keytime;
    public string helpURL;
    private int m_challenge_limit;
    public int challengeCount;
    private bool m_IsArchiveQuest;

    public string world
    {
      set
      {
        this.world_index = Singleton<ShareVariable>.Instance.str.Set(ShareString.Type.ChapterParam_world, value);
      }
      get
      {
        return Singleton<ShareVariable>.Instance.str.Get(ShareString.Type.ChapterParam_world, this.world_index);
      }
    }

    public string section
    {
      set
      {
        this.section_index = Singleton<ShareVariable>.Instance.str.Set(ShareString.Type.ChapterParam_section, value);
      }
      get
      {
        return Singleton<ShareVariable>.Instance.str.Get(ShareString.Type.ChapterParam_section, this.section_index);
      }
    }

    public bool IsArchiveQuest
    {
      get => this.m_IsArchiveQuest;
      set => this.m_IsArchiveQuest = value;
    }

    public void Deserialize(JSON_ChapterParam json)
    {
      this.iname = json != null ? json.iname : throw new InvalidJSONException();
      this.name = json.name;
      this.expr = json.expr;
      this.world = json.world;
      this.start = json.start;
      this.end = json.end;
      this.hidden = json.hide != 0;
      this.section = json.chap;
      this.banner = json.banr;
      this.prefabPath = json.item;
      this.helpURL = json.hurl;
      this.m_challenge_limit = json.limit;
      this.keys = new List<KeyItem>();
      if (!string.IsNullOrEmpty(json.keyitem1) && json.keynum1 > 0)
        this.keys.Add(new KeyItem()
        {
          iname = json.keyitem1,
          num = json.keynum1
        });
      if (this.keys.Count > 0)
        this.keytime = json.keytime;
      this.quests.Clear();
    }

    public bool IsAvailable(DateTime t)
    {
      if (this.end <= 0L)
        return !this.hidden;
      DateTime dateTime1 = TimeManager.FromUnixTime(this.start);
      DateTime dateTime2 = TimeManager.FromUnixTime(this.end);
      return dateTime1 <= t && t < dateTime2;
    }

    public bool IsKeyQuest() => this.keys.Count > 0 && !this.m_IsArchiveQuest;

    public KeyQuestTypes GetKeyQuestType()
    {
      if (!this.IsKeyQuest())
        return KeyQuestTypes.None;
      return this.keytime != 0L ? KeyQuestTypes.Timer : KeyQuestTypes.Count;
    }

    public bool IsGpsQuest()
    {
      if (this.quests != null)
      {
        for (int index = 0; index < this.quests.Count; ++index)
        {
          if (this.quests[index].type == QuestTypes.Gps)
            return true;
        }
      }
      for (int index = 0; index < this.children.Count; ++index)
      {
        if (this.children[index].IsGpsQuest())
          return true;
      }
      return false;
    }

    public bool IsTowerQuest()
    {
      if (this.quests != null)
      {
        for (int index = 0; index < this.quests.Count; ++index)
        {
          if (this.quests[index].type == QuestTypes.Tower || this.quests[index].type == QuestTypes.MultiTower)
            return true;
        }
      }
      for (int index = 0; index < this.children.Count; ++index)
      {
        if (this.children[index].IsTowerQuest())
          return true;
      }
      return false;
    }

    public bool IsBeginnerQuest()
    {
      if (this.quests != null)
      {
        for (int index = 0; index < this.quests.Count; ++index)
        {
          if (this.quests[index].type == QuestTypes.Beginner)
            return true;
        }
      }
      for (int index = 0; index < this.children.Count; ++index)
      {
        if (this.children[index].IsBeginnerQuest())
          return true;
      }
      return false;
    }

    public bool IsSeiseki()
    {
      if (this.quests != null)
      {
        for (int index = 0; index < this.quests.Count; ++index)
        {
          if (this.section == "WD_SEISEKI")
            return true;
        }
      }
      for (int index = 0; index < this.children.Count; ++index)
      {
        if (this.children[index].IsSeiseki())
          return true;
      }
      return false;
    }

    public bool IsBabel()
    {
      if (this.quests != null)
      {
        for (int index = 0; index < this.quests.Count; ++index)
        {
          if (this.section == "WD_BABEL")
            return true;
        }
      }
      for (int index = 0; index < this.children.Count; ++index)
      {
        if (this.children[index].IsBabel())
          return true;
      }
      return false;
    }

    public bool IsMultiGpsQuest()
    {
      if (this.quests != null)
      {
        for (int index = 0; index < this.quests.Count; ++index)
        {
          if (this.quests[index].type == QuestTypes.MultiGps)
            return true;
        }
      }
      for (int index = 0; index < this.children.Count; ++index)
      {
        if (this.children[index].IsMultiGpsQuest())
          return true;
      }
      return false;
    }

    public bool IsOrdealQuest()
    {
      if (this.quests != null)
      {
        for (int index = 0; index < this.quests.Count; ++index)
        {
          if (this.quests[index].type == QuestTypes.Ordeal)
            return true;
        }
      }
      for (int index = 0; index < this.children.Count; ++index)
      {
        if (this.children[index].IsOrdealQuest())
          return true;
      }
      return false;
    }

    public SubQuestTypes GetSubQuestType()
    {
      if (this.quests != null && this.quests.Count > 0)
        return this.quests[0].subtype;
      return this.children != null && this.children.Count > 0 ? this.children[0].GetSubQuestType() : SubQuestTypes.Normal;
    }

    public bool HasGpsQuest()
    {
      if (this.quests != null)
      {
        for (int index = 0; index < this.quests.Count; ++index)
        {
          if (this.quests[index].gps_enable)
            return true;
        }
      }
      for (int index = 0; index < this.children.Count; ++index)
      {
        if (this.children[index].HasGpsQuest())
          return true;
      }
      return false;
    }

    public bool IsDateUnlock(long unixtime)
    {
      for (int index = 0; index < this.quests.Count; ++index)
      {
        if (this.quests[index].IsDateUnlock(unixtime))
          return true;
      }
      return false;
    }

    public bool IsKeyUnlock(long unixtime)
    {
      if (!this.IsKeyQuest() || !this.IsDateUnlock(unixtime))
        return false;
      KeyQuestTypes keyQuestType = this.GetKeyQuestType();
      if (this.key_end <= 0L)
        return false;
      switch (keyQuestType)
      {
        case KeyQuestTypes.Timer:
          return unixtime < this.key_end;
        case KeyQuestTypes.Count:
          for (int index = 0; index < this.quests.Count; ++index)
          {
            if (this.quests[index].CheckEnableChallange())
              return true;
          }
          return false;
        default:
          return false;
      }
    }

    public bool CheckHasKeyItem()
    {
      for (int index = 0; index < this.keys.Count; ++index)
      {
        if (this.keys[index].IsHasItem())
          return true;
      }
      return false;
    }

    public bool CheckHasKey()
    {
      for (int index = 0; index < this.keys.Count; ++index)
      {
        if (this.keys[index].IsHas())
          return true;
      }
      return false;
    }

    public bool CheckEnableChallange() => this.CheckEnableChallange(out ChapterParam _);

    public bool CheckEnableChallange(out ChapterParam chapter)
    {
      chapter = (ChapterParam) null;
      int num = this.ChallengeLimitCount();
      if (num > 0)
      {
        chapter = this;
        if (this.parent != null)
        {
          ChapterParam chapter1 = (ChapterParam) null;
          this.parent.CheckEnableChallange(out chapter1);
          if (chapter1 != null && chapter1.iname != chapter.iname)
            DebugUtility.LogError("チャプター [" + chapter.iname + "] と [" + chapter1.iname + "] で重複した回数制限が行われています");
        }
        return chapter.challengeCount < num;
      }
      return this.parent == null || this.parent.CheckEnableChallange(out chapter);
    }

    public bool IncrementChallangeCount()
    {
      ChapterParam chapter;
      this.CheckEnableChallange(out chapter);
      if (chapter == null)
        return false;
      int num1 = chapter.ChallengeLimitCount();
      if (num1 <= 0)
        return false;
      int num2 = chapter.challengeCount + 1;
      if (num2 > num1)
      {
        DebugUtility.LogWarning("最大挑戦回数が" + (object) num1 + "回のチャプター [" + chapter.iname + "] に、" + (object) num2 + "を設定しようとしています。");
        num2 = num1;
      }
      chapter.challengeCount = num2;
      return true;
    }

    public bool HasChallengeLimit => this.ChallengeLimitCount() > 0;

    public bool IsQuestCondition()
    {
      if (this.quests == null || this.quests.Count <= 0)
        return false;
      foreach (QuestParam quest in this.quests)
      {
        if (quest.type != QuestTypes.AdvanceBoss && quest.type != QuestTypes.GenesisBoss && quest.IsQuestCondition())
          return true;
      }
      return false;
    }

    public int ChallengeLimitCount()
    {
      return MonoSingleton<GameManager>.Instance.Player.GetChallengeLimitCount(ExpansionPurchaseParam.eExpansionType.ChallengeCount, this.iname, this.m_challenge_limit);
    }
  }
}
