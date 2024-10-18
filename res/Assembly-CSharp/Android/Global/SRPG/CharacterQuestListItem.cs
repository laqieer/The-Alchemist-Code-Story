// Decompiled with JetBrains decompiler
// Type: SRPG.CharacterQuestListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class CharacterQuestListItem : MonoBehaviour
  {
    [SerializeField]
    private GameObject unitIcon1;
    [SerializeField]
    private GameObject unitIcon2;
    [SerializeField]
    private Text conditionText;

    public void SetUp(UnitData unitData1, UnitData unitData2, QuestParam questParam)
    {
      if ((UnityEngine.Object) this.unitIcon1 != (UnityEngine.Object) null)
        DataSource.Bind<UnitData>(this.unitIcon1, unitData1);
      if ((UnityEngine.Object) this.unitIcon2 != (UnityEngine.Object) null)
        DataSource.Bind<UnitData>(this.unitIcon2, unitData2);
      if (unitData1 == null || unitData2 == null || (questParam == null || questParam == null))
        return;
      List<QuestParam> questParamList = questParam.DetectNotClearConditionQuests();
      if (questParamList == null || questParamList.Count <= 0)
        return;
      this.conditionText.text = LocalizedText.Get("sys.COLLABO_SKILL_QUEST_CONDITION", new object[1]
      {
        (object) string.Join(",", questParamList.ConvertAll<string>((Converter<QuestParam, string>) (q => q.name)).ToArray())
      });
    }
  }
}
