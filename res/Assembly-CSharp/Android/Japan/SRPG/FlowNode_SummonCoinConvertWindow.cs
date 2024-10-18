// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SummonCoinConvertWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

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
      if (!((UnityEngine.Object) this.Instance != (UnityEngine.Object) null))
        return;
      GachaCoinChangeWindow componentInChildren = this.Instance.GetComponentInChildren<GachaCoinChangeWindow>(true);
      if (!((UnityEngine.Object) componentInChildren != (UnityEngine.Object) null))
        return;
      componentInChildren.Refresh(this.coinType);
    }
  }
}
