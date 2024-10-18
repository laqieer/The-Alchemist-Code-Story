// Decompiled with JetBrains decompiler
// Type: SRPG.TowerEntryConditoion
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class TowerEntryConditoion : MonoBehaviour, IGameParameter
  {
    public void UpdateValue()
    {
      QuestParam dataOfClass = DataSource.FindDataOfClass<QuestParam>(((Component) this).gameObject, (QuestParam) null);
      List<string> entryQuestConditions = dataOfClass.GetEntryQuestConditions();
      int num = 0;
      if (dataOfClass.EntryCondition != null)
      {
        if (dataOfClass.EntryCondition.plvmin > 0)
          ++num;
        if (dataOfClass.EntryCondition.ulvmin > 0)
          ++num;
      }
      ((Component) this).gameObject.SetActive(entryQuestConditions.Count > num);
    }
  }
}
