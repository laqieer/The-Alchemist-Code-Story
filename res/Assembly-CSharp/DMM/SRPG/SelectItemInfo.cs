// Decompiled with JetBrains decompiler
// Type: SRPG.SelectItemInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  public class SelectItemInfo : MonoBehaviour, IFlowInterface
  {
    private void Awake()
    {
    }

    private void Start() => this.Refresh();

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
      DataSource.Bind<ItemParam>(((Component) this).gameObject, itemParam);
      DataSource.Bind<ItemSelectListItemData>(((Component) this).gameObject, GlobalVars.ItemSelectListItemData);
      DataSource.Bind<ItemData>(((Component) this).gameObject, itemDataByItemId);
      GameParameter.UpdateAll(((Component) this).gameObject);
    }
  }
}
