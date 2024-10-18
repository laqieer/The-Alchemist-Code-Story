﻿// Decompiled with JetBrains decompiler
// Type: SRPG.CharacterQuestListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
        DataSource.Bind<UnitData>(this.unitIcon1, unitData1, false);
      if ((UnityEngine.Object) this.unitIcon2 != (UnityEngine.Object) null)
        DataSource.Bind<UnitData>(this.unitIcon2, unitData2, false);
      if (unitData1 == null || unitData2 == null || (questParam == null || questParam == null))
        return;
      List<QuestParam> questParamList = questParam.DetectNotClearConditionQuests();
      if (questParamList == null || questParamList.Count <= 0)
        return;
      this.conditionText.text = string.Join(",", questParamList.ConvertAll<string>((Converter<QuestParam, string>) (q => q.name)).ToArray()) + "をクリア";
    }
  }
}
