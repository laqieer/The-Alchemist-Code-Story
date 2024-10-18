// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_UpdateItemBind
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/Util/BindItem", 32741)]
  [FlowNode.Pin(0, "Bind", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Binded", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_UpdateItemBind : FlowNode
  {
    private const int PIN_IN_BIND = 0;
    private const int PIN_OTN_BINDED = 1;
    [SerializeField]
    private string VariableName = string.Empty;
    [SerializeField]
    private FlowNode_UpdateItemBind.SelectBindType BindType;
    [SerializeField]
    private FlowNode_UpdateItemBind.SelectUseType UseType;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      this.Bind();
      this.ActivateOutputLinks(1);
    }

    private void Bind()
    {
      DataSource.Bind<ItemData>(((Component) this).gameObject, (ItemData) null);
      DataSource.Bind<ItemParam>(((Component) this).gameObject, (ItemParam) null);
      if (this.UseType != FlowNode_UpdateItemBind.SelectUseType.UseVariable)
        return;
      if (string.IsNullOrEmpty(this.VariableName))
        return;
      string iname = FlowNode_Variable.Get(this.VariableName);
      if (string.IsNullOrEmpty(iname))
        return;
      if (this.BindType == FlowNode_UpdateItemBind.SelectBindType.ItemData)
      {
        ItemData data = MonoSingleton<GameManager>.Instance.Player.Items.Find((Predicate<ItemData>) (f => f.Param.iname == iname));
        if (data != null)
          DataSource.Bind<ItemData>(((Component) this).gameObject, data);
      }
      else if (this.BindType == FlowNode_UpdateItemBind.SelectBindType.ItemParam)
      {
        ItemParam itemParam = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(iname);
        if (itemParam != null)
          DataSource.Bind<ItemParam>(((Component) this).gameObject, itemParam);
      }
      GameParameter.UpdateAll(((Component) this).gameObject);
    }

    public enum SelectBindType
    {
      ItemParam,
      ItemData,
    }

    public enum SelectUseType
    {
      UseVariable,
    }
  }
}
