// Decompiled with JetBrains decompiler
// Type: SRPG.ItemGetUnlockWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "Unlock", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "Selected Quest", FlowNode.PinTypes.Output, 101)]
  public class ItemGetUnlockWindow : MonoBehaviour, IFlowInterface
  {
    private ItemParam UnlockItem;

    private void Start() => this.Refresh();

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.Refresh();
    }

    private void Refresh()
    {
      this.UnlockItem = MonoSingleton<GameManager>.Instance.GetItemParam(GlobalVars.ItemSelectListItemData.iiname);
      DataSource.Bind<ItemParam>(((Component) this).gameObject, this.UnlockItem);
      DataSource.Bind<ItemSelectListItemData>(((Component) this).gameObject, GlobalVars.ItemSelectListItemData);
      ItemData itemDataByItemParam = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemParam(this.UnlockItem);
      if (itemDataByItemParam != null)
        DataSource.Bind<ItemData>(((Component) this).gameObject, itemDataByItemParam);
      GameParameter.UpdateAll(((Component) this).gameObject);
    }
  }
}
