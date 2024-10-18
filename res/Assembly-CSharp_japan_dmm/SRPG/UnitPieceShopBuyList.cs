// Decompiled with JetBrains decompiler
// Type: SRPG.UnitPieceShopBuyList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class UnitPieceShopBuyList : MonoBehaviour
  {
    [SerializeField]
    private Text amount_text;
    [SerializeField]
    private Text cost_text;
    [SerializeField]
    private Button button;
    [SerializeField]
    private GameObject soldout;
    [SerializeField]
    private GameObject TimeLimitBase;
    [SerializeField]
    private GameObject TimeLimitPopup;
    [SerializeField]
    private Text TimeLimitText;
    private float mRefreshInterval = 1f;
    private UnitPieceShopItem mShopItem;
    private string mDay;
    private string mHour;
    private string mMinute;

    private string Day
    {
      get
      {
        if (string.IsNullOrEmpty(this.mDay))
          this.mDay = LocalizedText.Get("sys.SHOP_TIMELIMIT_D");
        return this.mDay;
      }
    }

    private string Hour
    {
      get
      {
        if (string.IsNullOrEmpty(this.mDay))
          this.mHour = LocalizedText.Get("sys.SHOP_TIMELIMIT_H");
        return this.mHour;
      }
    }

    private string Minute
    {
      get
      {
        if (string.IsNullOrEmpty(this.mDay))
          this.mMinute = LocalizedText.Get("sys.SHOP_TIMELIMIT_M");
        return this.mMinute;
      }
    }

    public void SetUp(UnitPieceShopItem shopItem)
    {
      this.mShopItem = shopItem;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.amount_text, (UnityEngine.Object) null))
        this.amount_text.text = string.Format(LocalizedText.Get("sys.SHOP_BUY_REMAINING"), (object) shopItem.RemainCount);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.cost_text, (UnityEngine.Object) null))
        this.cost_text.text = shopItem.CostNum.ToString();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.button, (UnityEngine.Object) null))
        ((Selectable) this.button).interactable = !shopItem.IsSoldOut;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.soldout, (UnityEngine.Object) null))
        this.soldout.SetActive(shopItem.IsSoldOut);
      this.RefreshExpire();
    }

    private void Update()
    {
      this.mRefreshInterval -= Time.unscaledDeltaTime;
      if ((double) this.mRefreshInterval > 0.0)
        return;
      this.RefreshExpire();
      this.mRefreshInterval = 1f;
    }

    private void RefreshExpire()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.TimeLimitBase, (UnityEngine.Object) null))
        return;
      UnitPieceShopItem mShopItem = this.mShopItem;
      if (mShopItem.IsSoldOut)
        this.TimeLimitBase.SetActive(false);
      else if (!mShopItem.IsExpired)
      {
        this.TimeLimitBase.SetActive(false);
      }
      else
      {
        TimeSpan timeSpan = mShopItem.EndAt - TimeManager.ServerTime;
        if (timeSpan.TotalDays >= 8.0)
          return;
        Color color = timeSpan.TotalDays >= 1.0 ? Color.yellow : Color.red;
        this.TimeLimitBase.SetActive(true);
        string str = (string) null;
        if (timeSpan.TotalDays >= 1.0)
          str = string.Format(this.Day, (object) timeSpan.Days);
        else if (timeSpan.TotalHours >= 1.0)
          str = string.Format(this.Hour, (object) timeSpan.Hours);
        else if (timeSpan.TotalSeconds > 0.0)
        {
          str = string.Format(this.Minute, (object) timeSpan.Minutes);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TimeLimitPopup, (UnityEngine.Object) null))
            this.TimeLimitPopup.SetActive(true);
        }
        else if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.button, (UnityEngine.Object) null))
          ((Selectable) this.button).interactable = false;
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TimeLimitText, (UnityEngine.Object) null) || !(this.TimeLimitText.text != str))
          return;
        ((Graphic) this.TimeLimitText).color = color;
        this.TimeLimitText.text = str;
      }
    }
  }
}
