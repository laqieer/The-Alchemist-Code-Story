// Decompiled with JetBrains decompiler
// Type: SRPG.CharacterQuestListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
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
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.unitIcon1, (UnityEngine.Object) null))
        DataSource.Bind<UnitData>(this.unitIcon1, unitData1);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.unitIcon2, (UnityEngine.Object) null))
        DataSource.Bind<UnitData>(this.unitIcon2, unitData2);
      if (unitData1 == null || unitData2 == null || questParam == null || questParam == null)
        return;
      List<QuestParam> questParamList = questParam.DetectNotClearConditionQuests();
      if (questParamList == null || questParamList.Count <= 0)
        return;
      this.conditionText.text = string.Join(",", questParamList.ConvertAll<string>((Converter<QuestParam, string>) (q => q.name)).ToArray()) + "をクリア";
    }
  }
}
