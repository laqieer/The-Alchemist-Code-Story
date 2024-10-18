﻿// Decompiled with JetBrains decompiler
// Type: SRPG.MultiPlayVersus_MapSelector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class MultiPlayVersus_MapSelector : SRPG_FixedList
  {
    public GameObject ItemTemplate;
    public RectTransform ItemLayoutParent;
    public GameObject SelectWindow;
    public Button ConfirmButton;
    private List<VersusMapParam> m_Param;

    protected override void Start()
    {
      if (Object.op_Equality((Object) this.ItemTemplate, (Object) null))
        return;
      if (Object.op_Inequality((Object) this.ConfirmButton, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.ConfirmButton.onClick).AddListener(new UnityAction((object) this, __methodptr(OnConfirm)));
      }
      base.Start();
      this.RefleshData();
    }

    protected override GameObject CreateItem() => Object.Instantiate<GameObject>(this.ItemTemplate);

    public override RectTransform ListParent
    {
      get
      {
        return Object.op_Inequality((Object) this.ItemLayoutParent, (Object) null) ? ((Component) this.ItemLayoutParent).GetComponent<RectTransform>() : (RectTransform) null;
      }
    }

    protected void RefleshData()
    {
      string selectedQuestId = GlobalVars.SelectedQuestID;
      List<QuestParam> questTypeList = MonoSingleton<GameManager>.Instance.GetQuestTypeList(QuestTypes.VersusFree);
      if (questTypeList == null)
        return;
      this.m_Param = new List<VersusMapParam>(questTypeList.Count);
      for (int index = 0; index < questTypeList.Count; ++index)
        this.m_Param.Add(new VersusMapParam()
        {
          quest = questTypeList[index],
          selected = questTypeList[index].iname == selectedQuestId
        });
      this.SetData((object[]) this.m_Param.ToArray(), typeof (VersusMapParam));
    }

    protected override void Update() => base.Update();

    protected override void OnItemSelect(GameObject go)
    {
      VersusMapParam dataOfClass = DataSource.FindDataOfClass<VersusMapParam>(go, (VersusMapParam) null);
      if (dataOfClass == null || !Object.op_Inequality((Object) this.SelectWindow, (Object) null))
        return;
      DataSource.Bind<QuestParam>(this.SelectWindow, dataOfClass.quest);
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "OPEN_SELECTWINDOW");
    }

    private void UpdateSelect()
    {
      for (int index = 0; index < this.m_Param.Count; ++index)
        this.m_Param[index].selected = this.m_Param[index].quest.iname == GlobalVars.SelectedQuestID;
    }

    private void OnConfirm()
    {
      QuestParam dataOfClass = DataSource.FindDataOfClass<QuestParam>(this.SelectWindow, (QuestParam) null);
      if (dataOfClass != null)
      {
        GlobalVars.SelectedQuestID = dataOfClass.iname;
        GlobalVars.EditMultiPlayRoomComment = GlobalVars.SelectedMultiPlayRoomComment;
        this.UpdateSelect();
      }
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "CLOSE_SELECT_WINDOW");
    }
  }
}
