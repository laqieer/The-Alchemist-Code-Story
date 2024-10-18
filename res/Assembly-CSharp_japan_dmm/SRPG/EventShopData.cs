// Decompiled with JetBrains decompiler
// Type: SRPG.EventShopData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class EventShopData
  {
    public List<EventShopItem> items = new List<EventShopItem>();
    public int UpdateCount;
    private ShopData mShopData = new ShopData();

    public bool Deserialize(Json_EventShopResponse response)
    {
      if (response.shopitems == null || !this.Deserialize(response.shopitems))
        return false;
      this.UpdateCount = response.relcnt;
      GlobalVars.ConceptCardNum.Set(response.concept_count);
      return true;
    }

    public bool Deserialize(Json_EventShopUpdateResponse response)
    {
      if (response.currencies == null)
        return false;
      try
      {
        MonoSingleton<GameManager>.Instance.Player.Deserialize(response.currencies);
      }
      catch (Exception ex)
      {
        DebugUtility.LogException(ex);
        return false;
      }
      if (response.items == null)
        return false;
      try
      {
        MonoSingleton<GameManager>.Instance.Player.Deserialize(response.items);
      }
      catch (Exception ex)
      {
        DebugUtility.LogException(ex);
        return false;
      }
      if (response.shopitems == null || !this.Deserialize(response.shopitems))
        return false;
      this.UpdateCount = response.relcnt;
      GlobalVars.ConceptCardNum.Set(response.concept_count);
      return true;
    }

    public bool Deserialize(Json_EventShopBuyResponse response)
    {
      if (response.currencies == null)
        return false;
      try
      {
        MonoSingleton<GameManager>.Instance.Player.Deserialize(response.currencies);
      }
      catch (Exception ex)
      {
        DebugUtility.LogException(ex);
        return false;
      }
      if (response.items == null)
        return false;
      try
      {
        MonoSingleton<GameManager>.Instance.Player.Deserialize(response.items);
      }
      catch (Exception ex)
      {
        DebugUtility.LogException(ex);
        return false;
      }
      if (response.shopitems == null)
        return false;
      JSON_EventShopItemListSet[] shopitems = response.shopitems;
      for (int i = 0; i < shopitems.Length; ++i)
      {
        EventShopItem eventShopItem1 = this.items[i];
        if (eventShopItem1 == null)
        {
          eventShopItem1 = new EventShopItem();
          this.items.Add(eventShopItem1);
        }
        else
        {
          EventShopItem eventShopItem2 = this.items.Find((Predicate<EventShopItem>) (it => it.id == shopitems[i].id));
          if (eventShopItem2 != null && shopitems[i] != null && shopitems[i].sold == 0 && shopitems[i].item != null && shopitems[i].item.step > eventShopItem2.step && shopitems[i].cost != null)
            ShopBuyStepupInfoData.Set(eventShopItem2.iname, shopitems[i].item.itype, eventShopItem2.max_num, eventShopItem2.saleValue, shopitems[i].cost.value, eventShopItem2.saleType);
        }
        if (!eventShopItem1.Deserialize(shopitems[i]))
          return false;
      }
      if (response.mail_info == null)
        return false;
      try
      {
        MonoSingleton<GameManager>.Instance.Player.Deserialize(response.mail_info);
      }
      catch (Exception ex)
      {
        DebugUtility.LogException(ex);
        return false;
      }
      try
      {
        MonoSingleton<GameManager>.Instance.Player.Deserialize(response.units);
      }
      catch (Exception ex)
      {
        DebugUtility.LogException(ex);
        return false;
      }
      GlobalVars.ConceptCardNum.Set(response.concept_count);
      try
      {
        MonoSingleton<GameManager>.Instance.Player.TrophyData.OverwriteTrophyProgress(response.trophyprogs);
        MonoSingleton<GameManager>.Instance.Player.TrophyData.OverwriteTrophyProgress(response.bingoprogs);
      }
      catch (Exception ex)
      {
        DebugUtility.LogException(ex);
        return false;
      }
      return true;
    }

    public bool Deserialize(JSON_EventShopItemListSet[] shopitems)
    {
      if (shopitems == null)
        return true;
      this.items.Clear();
      for (int index = 0; index < shopitems.Length; ++index)
      {
        EventShopItem eventShopItem = new EventShopItem();
        if (!eventShopItem.Deserialize(shopitems[index]))
          return false;
        this.items.Add(eventShopItem);
      }
      return true;
    }

    public ShopData GetShopData()
    {
      this.mShopData.items = new List<ShopItem>();
      for (int index = 0; index < this.items.Count; ++index)
        this.mShopData.items.Add((ShopItem) this.items[index]);
      this.mShopData.UpdateCount = this.UpdateCount;
      return this.mShopData;
    }

    public void SetShopData(ShopData shopData) => this.mShopData = shopData;
  }
}
