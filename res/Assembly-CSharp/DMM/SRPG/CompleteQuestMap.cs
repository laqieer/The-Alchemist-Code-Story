// Decompiled with JetBrains decompiler
// Type: SRPG.CompleteQuestMap
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class CompleteQuestMap
  {
    public Dictionary<QuestTypes, int> mQuestTypeMap = new Dictionary<QuestTypes, int>();
    public Dictionary<string, CompleteQuestMap.CompleteQuestData> mChapterMap = new Dictionary<string, CompleteQuestMap.CompleteQuestData>();

    public void LoadData()
    {
      QuestParam[] quests = MonoSingleton<GameManager>.Instance.Quests;
      if (quests == null)
        return;
      for (int index = 0; index < quests.Length; ++index)
        this.Add(quests[index]);
    }

    public int GetAllCount()
    {
      int allCount = 0;
      foreach (KeyValuePair<QuestTypes, int> mQuestType in this.mQuestTypeMap)
        allCount += mQuestType.Value;
      return allCount;
    }

    public void Add(QuestParam quest)
    {
      if (quest == null || !quest.IsMissionCompleteALL())
        return;
      if (this.mQuestTypeMap.ContainsKey(quest.type))
      {
        Dictionary<QuestTypes, int> mQuestTypeMap;
        QuestTypes type;
        (mQuestTypeMap = this.mQuestTypeMap)[type = quest.type] = mQuestTypeMap[type] + 1;
      }
      else
        this.mQuestTypeMap.Add(quest.type, 1);
      if (quest.Chapter == null)
        return;
      if (this.mChapterMap.ContainsKey(quest.Chapter.iname))
      {
        ++this.mChapterMap[quest.Chapter.iname].mCount;
      }
      else
      {
        CompleteQuestMap.CompleteQuestData completeQuestData = new CompleteQuestMap.CompleteQuestData(quest.type, 1);
        this.mChapterMap.Add(quest.Chapter.iname, completeQuestData);
      }
    }

    public class CompleteQuestData
    {
      public QuestTypes mQuestType;
      public int mCount;

      public CompleteQuestData()
      {
      }

      public CompleteQuestData(QuestTypes questType, int count)
      {
        this.mQuestType = questType;
        this.mCount = count;
      }
    }
  }
}
