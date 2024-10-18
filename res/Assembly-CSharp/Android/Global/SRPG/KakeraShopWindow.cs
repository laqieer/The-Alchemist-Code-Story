// Decompiled with JetBrains decompiler
// Type: SRPG.KakeraShopWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(100, "完了", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(10, "魂の欠片に変換", FlowNode.PinTypes.Input, 10)]
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
