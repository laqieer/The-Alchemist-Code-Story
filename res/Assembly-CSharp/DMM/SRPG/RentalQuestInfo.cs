// Decompiled with JetBrains decompiler
// Type: SRPG.RentalQuestInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class RentalQuestInfo
  {
    public string QuestId;
    public OInt Point;
    private QuestParam mQuestParam;

    public RentalQuestInfo(string quest_id, int point)
    {
      this.QuestId = quest_id;
      this.Point = (OInt) point;
      this.mQuestParam = (QuestParam) null;
    }

    public QuestParam Quest
    {
      get
      {
        if (this.mQuestParam == null && !string.IsNullOrEmpty(this.QuestId) && Object.op_Implicit((Object) MonoSingleton<GameManager>.Instance))
          this.mQuestParam = MonoSingleton<GameManager>.Instance.FindQuest(this.QuestId);
        return this.mQuestParam;
      }
    }
  }
}
