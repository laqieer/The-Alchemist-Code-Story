// Decompiled with JetBrains decompiler
// Type: SRPG.LimitedShopData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class LimitedShopData
  {
    public List<LimitedShopItem> items = new List<LimitedShopItem>();
    public int UpdateCount;

    public bool Deserialize(Json_LimitedShopResponse response)
    {
      if (response.shopitems == null || !this.Deserialize(response.shopitems))
        return false;
      this.UpdateCount = response.relcnt;
      GlobalVars.ConceptCardNum.Set(response.concept_count);
      return true;
    }

    public bool Deserialize(Json_LimitedShopUpdateResponse response)
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
      if (response.shopitems == null || !this.Deserialize(response.shopitems))
        return false;
      this.UpdateCount = response.relcnt;
      GlobalVars.ConceptCardNum.Set(response.concept_count);
      if (response.items != null)
      {
        try
        {
          MonoSingleton<GameManager>.Instance.Player.Deserialize(response.items);
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          return false;
        }
      }
      return true;
    }

    public bool Deserialize(Json_LimitedShopBuyResponse response)
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
      if (response.items != null)
      {
        try
        {
          MonoSingleton<GameManager>.Instance.Player.Deserialize(response.items);
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          return false;
        }
      }
      if (response.shopitems == null)
        return false;
      JSON_LimitedShopItemListSet[] shopitems = response.shopitems;
      for (int i = 0; i < shopitems.Length; ++i)
      {
        LimitedShopItem limitedShopItem1 = this.items[i];
        if (limitedShopItem1 == null)
        {
          limitedShopItem1 = new LimitedShopItem();
          this.items.Add(limitedShopItem1);
        }
        else
        {
          LimitedShopItem limitedShopItem2 = this.items.Find((Predicate<LimitedShopItem>) (it => it.id == shopitems[i].id));
          if (limitedShopItem2 != null && shopitems[i] != null && shopitems[i].sold == 0 && shopitems[i].item != null && shopitems[i].item.step > limitedShopItem2.step && shopitems[i].cost != null)
            ShopBuyStepupInfoData.Set(limitedShopItem2.iname, shopitems[i].item.itype, limitedShopItem2.max_num, limitedShopItem2.saleValue, shopitems[i].cost.value, limitedShopItem2.saleType);
        }
        if (!limitedShopItem1.Deserialize(shopitems[i]))
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

    public bool Deserialize(JSON_LimitedShopItemListSet[] shopitems)
    {
      if (shopitems == null)
        return true;
      this.items.Clear();
      for (int index = 0; index < shopitems.Length; ++index)
      {
        LimitedShopItem limitedShopItem = new LimitedShopItem();
        if (!limitedShopItem.Deserialize(shopitems[index]))
          return false;
        this.items.Add(limitedShopItem);
      }
      return true;
    }

    public ShopData GetShopData()
    {
      ShopData shopData = new ShopData();
      shopData.items = new List<ShopItem>();
      for (int index = 0; index < shopData.items.Count; ++index)
        shopData.items[index] = (ShopItem) this.items[index];
      shopData.UpdateCount = this.UpdateCount;
      return shopData;
    }

    public void SetShopData(ShopData shopData)
    {
      shopData.items = new List<ShopItem>();
      for (int index = 0; index < shopData.items.Count; ++index)
        this.items[index].SetShopItem(shopData.items[index]);
      shopData.UpdateCount = this.UpdateCount;
    }
  }
}
