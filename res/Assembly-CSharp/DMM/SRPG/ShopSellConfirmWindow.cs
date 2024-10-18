// Decompiled with JetBrains decompiler
// Type: SRPG.ShopSellConfirmWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  public class ShopSellConfirmWindow : MonoBehaviour, IFlowInterface
  {
    public RectTransform ItemLayoutParent;
    public GameObject ItemTemplate;
    public Text TxtCaution;
    private List<GameObject> mSellItems = new List<GameObject>(10);

    private void Awake()
    {
      if (!Object.op_Inequality((Object) this.ItemTemplate, (Object) null) || !this.ItemTemplate.activeInHierarchy)
        return;
      this.ItemTemplate.SetActive(false);
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
      if (Object.op_Inequality((Object) this.TxtCaution, (Object) null))
      {
        bool flag = false;
        for (int index = 0; index < GlobalVars.SellItemList.Count; ++index)
        {
          if (GlobalVars.SellItemList[index].item.Rarity > 1)
          {
            flag = true;
            break;
          }
        }
        ((Component) this.TxtCaution).gameObject.SetActive(flag);
      }
      List<SellItem> sellItemList = GlobalVars.SellItemList;
      for (int index = 0; index < this.mSellItems.Count; ++index)
        this.mSellItems[index].gameObject.SetActive(false);
      for (int index = 0; index < sellItemList.Count; ++index)
      {
        if (index >= this.mSellItems.Count)
        {
          GameObject gameObject = Object.Instantiate<GameObject>(this.ItemTemplate);
          gameObject.transform.SetParent((Transform) this.ItemLayoutParent, false);
          this.mSellItems.Add(gameObject);
        }
        GameObject mSellItem = this.mSellItems[index];
        DataSource.Bind<SellItem>(mSellItem, sellItemList[index]);
        mSellItem.SetActive(true);
      }
      DataSource.Bind<List<SellItem>>(((Component) this).gameObject, GlobalVars.SellItemList);
      GameParameter.UpdateAll(((Component) this).gameObject);
    }
  }
}
