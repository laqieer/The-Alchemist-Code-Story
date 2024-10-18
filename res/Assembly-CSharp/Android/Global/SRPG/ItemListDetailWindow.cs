// Decompiled with JetBrains decompiler
// Type: SRPG.ItemListDetailWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Reload", FlowNode.PinTypes.Input, 1)]
  public class ItemListDetailWindow : MonoBehaviour, IFlowInterface
  {
    public void Activated(int pinID)
    {
      this.Refresh();
    }

    private void Start()
    {
      this.Refresh();
    }

    private void Refresh()
    {
      ItemData itemDataByItemId = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(GlobalVars.SelectedItemID);
      if (itemDataByItemId != null)
      {
        DataSource.Bind<ItemData>(this.gameObject, itemDataByItemId);
      }
      else
      {
        ItemParam itemParam = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(GlobalVars.SelectedItemID);
        if (itemParam == null)
          return;
        DataSource.Bind<ItemParam>(this.gameObject, itemParam);
      }
      GameParameter.UpdateAll(this.gameObject);
      this.enabled = true;
    }
  }
}
