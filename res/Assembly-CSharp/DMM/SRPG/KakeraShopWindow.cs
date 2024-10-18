// Decompiled with JetBrains decompiler
// Type: SRPG.KakeraShopWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(10, "魂の欠片に変換", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(100, "完了", FlowNode.PinTypes.Output, 100)]
  public class KakeraShopWindow : MonoBehaviour, IFlowInterface
  {
    public void Activated(int pinID)
    {
      if (pinID != 10)
        return;
      this.OnConvert();
    }

    private void OnConvert()
    {
      if (GlobalVars.ConvertAwakePieceList == null)
        GlobalVars.ConvertAwakePieceList = new List<SellItem>();
      else
        GlobalVars.ConvertAwakePieceList.Clear();
      GlobalVars.ConvertAwakePieceList.AddRange((IEnumerable<SellItem>) GlobalVars.SellItemList);
      GlobalVars.SellItemList.Clear();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
    }
  }
}
