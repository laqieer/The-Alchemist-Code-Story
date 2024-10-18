// Decompiled with JetBrains decompiler
// Type: SRPG.ItemListDetailWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "Reload", FlowNode.PinTypes.Input, 1)]
  public class ItemListDetailWindow : MonoBehaviour, IFlowInterface
  {
    [SerializeField]
    private GameObject mItemLimitedText;

    public void Activated(int pinID) => this.Refresh();

    private void Start() => this.Refresh();

    private void Refresh()
    {
      ItemData itemDataByItemId = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(GlobalVars.SelectedItemID);
      ItemParam itemParam;
      if (itemDataByItemId != null)
      {
        itemParam = itemDataByItemId.Param;
        DataSource.Bind<ItemData>(((Component) this).gameObject, itemDataByItemId);
      }
      else
      {
        itemParam = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(GlobalVars.SelectedItemID);
        if (itemParam == null)
          return;
        DataSource.Bind<ItemParam>(((Component) this).gameObject, itemParam);
      }
      if (itemParam != null)
        this.mItemLimitedText.SetActive(itemParam.IsLimited);
      GameParameter.UpdateAll(((Component) this).gameObject);
      ((Behaviour) this).enabled = true;
    }
  }
}
