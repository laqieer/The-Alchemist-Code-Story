﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ShopSellConfirmWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  public class ShopSellConfirmWindow : MonoBehaviour, IFlowInterface
  {
    private List<GameObject> mSellItems = new List<GameObject>(10);
    public RectTransform ItemLayoutParent;
    public GameObject ItemTemplate;
    public Text TxtCaution;

    private void Awake()
    {
      if (!((UnityEngine.Object) this.ItemTemplate != (UnityEngine.Object) null) || !this.ItemTemplate.activeInHierarchy)
        return;
      this.ItemTemplate.SetActive(false);
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
      if ((UnityEngine.Object) this.TxtCaution != (UnityEngine.Object) null)
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
        this.TxtCaution.gameObject.SetActive(flag);
      }
      List<SellItem> sellItemList = GlobalVars.SellItemList;
      for (int index = 0; index < this.mSellItems.Count; ++index)
        this.mSellItems[index].gameObject.SetActive(false);
      for (int index = 0; index < sellItemList.Count; ++index)
      {
        if (index >= this.mSellItems.Count)
        {
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ItemTemplate);
          gameObject.transform.SetParent((Transform) this.ItemLayoutParent, false);
          this.mSellItems.Add(gameObject);
        }
        GameObject mSellItem = this.mSellItems[index];
        DataSource.Bind<SellItem>(mSellItem, sellItemList[index], false);
        mSellItem.SetActive(true);
      }
      DataSource.Bind<List<SellItem>>(this.gameObject, GlobalVars.SellItemList, false);
      GameParameter.UpdateAll(this.gameObject);
    }
  }
}