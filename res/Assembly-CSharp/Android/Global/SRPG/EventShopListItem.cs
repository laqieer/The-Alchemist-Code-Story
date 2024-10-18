﻿// Decompiled with JetBrains decompiler
// Type: SRPG.EventShopListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class EventShopListItem : MonoBehaviour
  {
    private float mRefreshInterval = 1f;
    public string EventShopSpritePath = "EventShopBanner/EventShopSprites";
    public JSON_ShopListArray.Shops shops;
    public LText l_text;
    public GameObject Body;
    public Text Timer;
    private long mEndTime;
    public Image banner;
    public string shop_cost_iname;
    public bool btn_update;
    public string banner_sprite;
    public GameObject mPaidCoinIcon;
    public GameObject mPaidCoinNum;
    public GameObject mLockObject;
    public Text mLockText;

    private void Start()
    {
      this.UpdateValue();
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

    public void SetShopList(JSON_ShopListArray.Shops shops)
    {
      this.shops = shops;
      Json_ShopMsgResponse jsonShopMsgResponse;
      try
      {
        jsonShopMsgResponse = JSONParser.parseJSONObject<Json_ShopMsgResponse>(shops.info.msg);
      }
      catch (Exception ex)
      {
        Debug.LogError((object) ("Parse failed: " + shops.info.msg));
        jsonShopMsgResponse = (Json_ShopMsgResponse) null;
      }
      if (jsonShopMsgResponse != null)
      {
        this.banner_sprite = jsonShopMsgResponse.banner;
        this.shop_cost_iname = jsonShopMsgResponse.costiname;
        if (jsonShopMsgResponse.update != null)
          this.btn_update = jsonShopMsgResponse.update.Equals("on");
      }
      GachaTabSprites gachaTabSprites = AssetManager.Load<GachaTabSprites>(this.EventShopSpritePath);
      if ((UnityEngine.Object) gachaTabSprites != (UnityEngine.Object) null && gachaTabSprites.Sprites != null && gachaTabSprites.Sprites.Length > 0)
      {
        Sprite[] sprites = gachaTabSprites.Sprites;
        for (int index = 0; index < sprites.Length; ++index)
        {
          if ((UnityEngine.Object) sprites[index] != (UnityEngine.Object) null && sprites[index].name == this.banner_sprite)
            this.banner.sprite = sprites[index];
        }
      }
      EventCoinData data = MonoSingleton<GameManager>.Instance.Player.EventCoinList.Find((Predicate<EventCoinData>) (f => f.iname.Equals(this.shop_cost_iname)));
      if (data == null)
      {
        data = new EventCoinData();
        data.param = MonoSingleton<GameManager>.Instance.MasterParam.Items.Find((Predicate<ItemParam>) (f => f.iname.Equals(this.shop_cost_iname)));
      }
      DataSource.Bind<ItemParam>(this.mPaidCoinIcon, data.param);
      DataSource.Bind<EventCoinData>(this.mPaidCoinNum, data);
      if (shops.unlock == null || !((UnityEngine.Object) this.mLockObject != (UnityEngine.Object) null) || !((UnityEngine.Object) this.mLockText != (UnityEngine.Object) null))
        return;
      bool flag = shops.unlock.flg == 1;
      Button component = this.GetComponent<Button>();
      if (!(bool) ((UnityEngine.Object) component))
        return;
      if (flag)
      {
        component.interactable = true;
        this.mLockObject.SetActive(false);
        this.mLockText.text = string.Empty;
      }
      else
      {
        component.interactable = false;
        this.mLockObject.SetActive(true);
        this.mLockText.text = shops.unlock.message != null ? shops.unlock.message : string.Empty;
      }
    }

    private void Refresh()
    {
      if (this.mEndTime <= 0L)
      {
        if (!((UnityEngine.Object) this.Body != (UnityEngine.Object) null))
          return;
        this.Body.SetActive(false);
      }
      else
      {
        if ((UnityEngine.Object) this.Body != (UnityEngine.Object) null)
          this.Body.SetActive(true);
        TimeSpan timeSpan = TimeManager.FromUnixTime(this.mEndTime) - TimeManager.ServerTime;
        string str;
        if (timeSpan.TotalDays >= 1.0)
          str = LocalizedText.Get("sys.QUEST_TIMELIMIT_D", new object[1]
          {
            (object) timeSpan.Days
          });
        else if (timeSpan.TotalHours >= 1.0)
          str = LocalizedText.Get("sys.QUEST_TIMELIMIT_H", new object[1]
          {
            (object) timeSpan.Hours
          });
        else
          str = LocalizedText.Get("sys.QUEST_TIMELIMIT_M", new object[1]
          {
            (object) Mathf.Max(timeSpan.Minutes, 0)
          });
        if (!((UnityEngine.Object) this.Timer != (UnityEngine.Object) null) || !(this.Timer.text != str))
          return;
        this.Timer.text = str;
      }
    }

    public void UpdateValue()
    {
      this.mEndTime = 0L;
      if (this.shops == null)
        return;
      this.mEndTime = this.shops.end;
      this.Refresh();
    }
  }
}
