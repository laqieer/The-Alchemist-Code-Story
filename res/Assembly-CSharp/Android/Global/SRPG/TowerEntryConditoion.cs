﻿// Decompiled with JetBrains decompiler
// Type: SRPG.TowerEntryConditoion
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class TowerEntryConditoion : MonoBehaviour, IGameParameter
  {
    public void UpdateValue()
    {
      QuestParam dataOfClass = DataSource.FindDataOfClass<QuestParam>(this.gameObject, (QuestParam) null);
      List<string> entryQuestConditions = dataOfClass.GetEntryQuestConditions(true, true, true);
      int num = 0;
      if (dataOfClass.EntryCondition != null)
      {
        if (dataOfClass.EntryCondition.plvmin > 0)
          ++num;
        if (dataOfClass.EntryCondition.ulvmin > 0)
          ++num;
      }
      this.gameObject.SetActive(entryQuestConditions.Count > num);
    }
  }
}
