// Decompiled with JetBrains decompiler
// Type: SRPG.SelectItemInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
      DataSource.Bind<ItemParam>(this.gameObject, itemParam, false);
      DataSource.Bind<ItemSelectListItemData>(this.gameObject, GlobalVars.ItemSelectListItemData, false);
      DataSource.Bind<ItemData>(this.gameObject, itemDataByItemId, false);
      GameParameter.UpdateAll(this.gameObject);
    }
  }
}
