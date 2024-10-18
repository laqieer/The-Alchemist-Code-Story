// Decompiled with JetBrains decompiler
// Type: QuestLobbyNews
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using SRPG;
using System;
using System.Collections.Generic;

#nullable disable
public class QuestLobbyNews
{
  private const string TEXT_CATEGORY_NAME_BASE = "sys.QUEST_LOBBY_NEWS_CATEGORY_NAME_";
  private const string TEXT_SHOW_TYPE_BASE = "sys.QUEST_LOBBY_NEWS_TYPE_";
  private QuestLobbyNews.QuestLobbyCategory mCategory;
  private DateTime mBeginAt;
  private DateTime mEndAt;
  private int mShowType;

  public bool Deserialize(JSON_QuestLobbyNewsParam json)
  {
    this.mCategory = (QuestLobbyNews.QuestLobbyCategory) json.category;
    this.mBeginAt = DateTime.Parse(json.begin_at);
    this.mEndAt = DateTime.Parse(json.end_at);
    this.mShowType = json.show_type;
    return true;
  }

  public bool isShow()
  {
    DateTime serverTime = TimeManager.ServerTime;
    return !(serverTime < this.mBeginAt) && !(serverTime > this.mEndAt);
  }

  public string GetShowText()
  {
    string str = LocalizedText.Get("sys.QUEST_LOBBY_NEWS_CATEGORY_NAME_" + (object) (int) this.mCategory);
    if (this.mShowType != 0)
      return LocalizedText.Get("sys.QUEST_LOBBY_NEWS_TYPE_" + (object) this.mShowType);
    return LocalizedText.Get("sys.QUEST_LOBBY_NEWS_TYPE_" + (object) this.mShowType, (object) str);
  }

  public static QuestLobbyNews FindQuestLobbyNews(QuestLobbyNews.QuestLobbyCategory target)
  {
    List<QuestLobbyNews> mQuestLobbyNews = MonoSingleton<GameManager>.Instance.mQuestLobbyNews;
    if (mQuestLobbyNews != null)
    {
      for (int index = 0; index < mQuestLobbyNews.Count; ++index)
      {
        if (mQuestLobbyNews[index].mCategory == target && mQuestLobbyNews[index].isShow())
          return mQuestLobbyNews[index];
      }
    }
    return (QuestLobbyNews) null;
  }

  public enum QuestLobbyCategory
  {
    storyRoot,
    eventRoot,
    challengeRoot,
    multi,
    mainStory,
    seiseki,
    babel,
    character,
    eventQuest,
    dailyAndEnhance,
    key,
    tower,
    ordeal,
    arena,
    draft,
    archive,
    genesis,
    advance,
  }
}
