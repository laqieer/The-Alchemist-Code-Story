// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SummonCoinConvertWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("UI/SummonCoinConvertWindow")]
  public class FlowNode_SummonCoinConvertWindow : FlowNode_GUI
  {
    [SerializeField]
    private GachaCoinChangeWindow.CoinType coinType;

    protected override void OnCreatePinActive()
    {
      base.OnCreatePinActive();
      if (!Object.op_Inequality((Object) this.Instance, (Object) null))
        return;
      GachaCoinChangeWindow componentInChildren = this.Instance.GetComponentInChildren<GachaCoinChangeWindow>(true);
      if (!Object.op_Inequality((Object) componentInChildren, (Object) null))
        return;
      componentInChildren.Refresh(this.coinType);
    }
  }
}
