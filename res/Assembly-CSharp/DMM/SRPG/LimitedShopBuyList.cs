﻿// Decompiled with JetBrains decompiler
// Type: SRPG.LimitedShopBuyList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class LimitedShopBuyList : MonoBehaviour
  {
    public GameObject amount;
    public GameObject day_reset;
    public GameObject limit;
    public GameObject icon_set;
    public GameObject sold_count;
    public GameObject no_limited_price;
    public Text amount_text;
    public Button self_button;
    [HeaderBar("▼アイテム表示用")]
    public GameObject item_name;
    public GameObject item_icon;
    [HeaderBar("▼武具表示用")]
    public GameObject artifact_name;
    public GameObject artifact_icon;
    [HeaderBar("▼真理念装表示用")]
    public GameObject conceptCard_name;
    public ConceptCardIcon conceptCard_icon;
    [Space(10f)]
    public GameObject TimeLimitBase;
    public GameObject TimeLimitPopup;
    public Text TimeLimitText;
    private float mRefreshInterval = 1f;
    private long mEndTime;
    private string mDayLimit;
    private string mHourLimit;
    private string mMinuteLimit;
    private LimitedShopItem mLimitedShopItem;

    private void Awake()
    {
      this.mDayLimit = LocalizedText.Get("sys.SHOP_TIMELIMIT_D");
      this.mHourLimit = LocalizedText.Get("sys.SHOP_TIMELIMIT_H");
      this.mMinuteLimit = LocalizedText.Get("sys.SHOP_TIMELIMIT_M");
      this.Refresh();
    }

    private void Update()
    {
      this.mRefreshInterval -= Time.unscaledDeltaTime;
      if ((double) this.mRefreshInterval > 0.0)
        return;
      this.Refresh();
      this.mRefreshInterval = 1f;
    }

    private void Refresh()
    {
      if (this.mLimitedShopItem.is_soldout)
      {
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TimeLimitBase, (UnityEngine.Object) null))
          return;
        this.TimeLimitBase.SetActive(false);
      }
      else if (this.mEndTime <= 0L)
      {
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TimeLimitBase, (UnityEngine.Object) null))
          return;
        this.TimeLimitBase.SetActive(false);
      }
      else
      {
        DateTime serverTime = TimeManager.ServerTime;
        DateTime dateTime1 = TimeManager.FromUnixTime(this.mEndTime);
        DateTime dateTime2 = TimeManager.FromUnixTime(GlobalVars.LimitedShopItem.shops.end);
        if (dateTime1 > dateTime2)
        {
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TimeLimitBase, (UnityEngine.Object) null))
            return;
          this.TimeLimitBase.SetActive(false);
        }
        else
        {
          TimeSpan timeSpan = dateTime1 - serverTime;
          if (timeSpan.TotalDays >= 8.0)
            return;
          Color color = timeSpan.TotalDays >= 1.0 ? Color.yellow : Color.red;
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TimeLimitBase, (UnityEngine.Object) null))
            this.TimeLimitBase.SetActive(true);
          string str = (string) null;
          if (timeSpan.TotalDays >= 1.0)
            str = string.Format(this.mDayLimit, (object) timeSpan.Days);
          else if (timeSpan.TotalHours >= 1.0)
            str = string.Format(this.mHourLimit, (object) timeSpan.Hours);
          else if (timeSpan.TotalSeconds > 0.0)
          {
            str = string.Format(this.mMinuteLimit, (object) timeSpan.Minutes);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TimeLimitPopup, (UnityEngine.Object) null))
              this.TimeLimitPopup.SetActive(true);
          }
          else if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self_button, (UnityEngine.Object) null))
            ((Selectable) this.self_button).interactable = false;
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TimeLimitText, (UnityEngine.Object) null) || !(this.TimeLimitText.text != str))
            return;
          ((Graphic) this.TimeLimitText).color = color;
          this.TimeLimitText.text = str;
        }
      }
    }

    public LimitedShopItem limitedShopItem
    {
      set
      {
        this.mLimitedShopItem = value;
        GameUtility.SetGameObjectActive(this.day_reset, this.mLimitedShopItem.is_reset);
        GameUtility.SetGameObjectActive(this.limit, !this.mLimitedShopItem.is_reset);
        GameUtility.SetGameObjectActive(this.icon_set, this.mLimitedShopItem.saleType == ESaleType.Coin_P);
        GameUtility.SetGameObjectActive(this.sold_count, !this.limitedShopItem.IsNotLimited);
        GameUtility.SetGameObjectActive(this.no_limited_price, this.limitedShopItem.IsNotLimited);
        if (this.limitedShopItem.IsItem || this.limitedShopItem.IsSet)
        {
          GameUtility.SetGameObjectActive(this.item_name, true);
          GameUtility.SetGameObjectActive(this.item_icon, true);
          GameUtility.SetGameObjectActive(this.artifact_name, false);
          GameUtility.SetGameObjectActive(this.artifact_icon, false);
          GameUtility.SetGameObjectActive(this.conceptCard_name, false);
          GameUtility.SetGameObjectActive((Component) this.conceptCard_icon, false);
        }
        else if (this.limitedShopItem.IsArtifact)
        {
          GameUtility.SetGameObjectActive(this.item_name, false);
          GameUtility.SetGameObjectActive(this.item_icon, false);
          GameUtility.SetGameObjectActive(this.artifact_name, true);
          GameUtility.SetGameObjectActive(this.artifact_icon, true);
          GameUtility.SetGameObjectActive(this.conceptCard_name, false);
          GameUtility.SetGameObjectActive((Component) this.conceptCard_icon, false);
        }
        else if (this.limitedShopItem.IsConceptCard)
        {
          GameUtility.SetGameObjectActive(this.item_name, false);
          GameUtility.SetGameObjectActive(this.item_icon, false);
          GameUtility.SetGameObjectActive(this.artifact_name, false);
          GameUtility.SetGameObjectActive(this.artifact_icon, false);
          GameUtility.SetGameObjectActive(this.conceptCard_name, true);
          GameUtility.SetGameObjectActive((Component) this.conceptCard_icon, true);
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.amount_text, (UnityEngine.Object) null))
          this.amount_text.text = this.limitedShopItem.remaining_num.ToString();
        this.mEndTime = this.mLimitedShopItem.end;
      }
      get => this.mLimitedShopItem;
    }

    public void SetupConceptCard(ConceptCardData conceptCardData)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.conceptCard_icon, (UnityEngine.Object) null))
        return;
      this.conceptCard_icon.Setup(conceptCardData);
    }
  }
}
