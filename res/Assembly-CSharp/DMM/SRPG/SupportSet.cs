﻿// Decompiled with JetBrains decompiler
// Type: SRPG.SupportSet
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(0, "Refresh", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(3, "Unit Select", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(1, "Unit Selected", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(2, "EndRefresh", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(4, "Unit Select End", FlowNode.PinTypes.Output, 4)]
  public class SupportSet : MonoBehaviour, IFlowInterface
  {
    public SupportList UnitList;
    public GameObject Parent;
    public RectTransform UnitListHilit;
    private long SelectedUniqueId;

    private List<UnitData> mOwnUnits => MonoSingleton<GameManager>.Instance.Player.Units;

    private void Start()
    {
      this.UnitList.OnUnitSelect = new UnitListV2.UnitSelectEvent(this.UnitSelect);
    }

    private void Update()
    {
    }

    public void UnitSelect(long uniqueID)
    {
      DataSource.Bind<UnitData>(this.Parent, MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(uniqueID));
      this.SelectedUniqueId = uniqueID;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 1);
      GameParameter.UpdateAll(this.Parent);
    }

    public void Activated(int pinID)
    {
      if (pinID == 0)
      {
        this.UnitList.RefreshData();
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 2);
      }
      if (pinID != 3)
        return;
      GlobalVars.SelectedSupportUnitUniqueID.Set(this.SelectedUniqueId);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 4);
    }
  }
}