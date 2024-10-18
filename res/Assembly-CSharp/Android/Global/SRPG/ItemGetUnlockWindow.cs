// Decompiled with JetBrains decompiler
// Type: SRPG.ItemGetUnlockWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "Unlock", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "Selected Quest", FlowNode.PinTypes.Output, 101)]
  public class ItemGetUnlockWindow : MonoBehaviour, IFlowInterface
  {
    private ItemParam UnlockItem;

    private void Start()
    {
      this.Refresh();
    }

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.Refresh();
    }

    private void Refresh()
    {
      this.UnlockItem = MonoSingleton<GameManager>.Instance.GetItemParam(GlobalVars.ItemSelectListItemData.iiname);
      DataSource.Bind<ItemParam>(this.gameObject, this.UnlockItem);
      DataSource.Bind<ItemSelectListItemData>(this.gameObject, GlobalVars.ItemSelectListItemData);
      ItemData itemDataByItemParam = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemParam(this.UnlockItem);
      if (itemDataByItemParam != null)
        DataSource.Bind<ItemData>(this.gameObject, itemDataByItemParam);
      GameParameter.UpdateAll(this.gameObject);
    }
  }
}
