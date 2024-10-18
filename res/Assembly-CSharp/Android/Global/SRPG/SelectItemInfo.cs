// Decompiled with JetBrains decompiler
// Type: SRPG.SelectItemInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  public class SelectItemInfo : MonoBehaviour, IFlowInterface
  {
    private void Awake()
    {
    }

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
      ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(GlobalVars.ItemSelectListItemData.iiname);
      ItemData itemDataByItemId = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(itemParam.iname);
      DataSource.Bind<ItemParam>(this.gameObject, itemParam);
      DataSource.Bind<ItemSelectListItemData>(this.gameObject, GlobalVars.ItemSelectListItemData);
      DataSource.Bind<ItemData>(this.gameObject, itemDataByItemId);
      GameParameter.UpdateAll(this.gameObject);
    }
  }
}
