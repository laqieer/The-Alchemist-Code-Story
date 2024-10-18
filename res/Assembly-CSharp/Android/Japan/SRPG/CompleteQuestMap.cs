// Decompiled with JetBrains decompiler
// Type: SRPG.CompleteQuestMap
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;

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
      int num = 0;
      foreach (KeyValuePair<QuestTypes, int> mQuestType in this.mQuestTypeMap)
        num += mQuestType.Value;
      return num;
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
