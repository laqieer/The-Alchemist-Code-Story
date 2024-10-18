// Decompiled with JetBrains decompiler
// Type: SRPG.EventShopListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class EventShopListItem : MonoBehaviour
  {
    public LText l_text;
    public GameObject Body;
    public Text Timer;
    private long mEndTime;
    private float mRefreshInterval = 1f;
    public Image banner;
    public string EventShopSpritePath = "EventShopBanner/EventShopSprites";
    public GameObject mPaidCoinIcon;
    public GameObject mPaidCoinNum;
    public GameObject mLockObject;
    public Text mLockText;
    public EventShopInfo EventShopInfo = new EventShopInfo();

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

    public void SetShopList(JSON_ShopListArray.Shops shops, Json_ShopMsgResponse msg)
    {
      this.EventShopInfo.Setup(shops, msg);
      GachaTabSprites gachaTabSprites = AssetManager.Load<GachaTabSprites>(this.EventShopSpritePath);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gachaTabSprites, (UnityEngine.Object) null) && gachaTabSprites.Sprites != null && gachaTabSprites.Sprites.Length > 0)
      {
        Sprite[] sprites = gachaTabSprites.Sprites;
        for (int index = 0; index < sprites.Length; ++index)
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) sprites[index], (UnityEngine.Object) null) && ((UnityEngine.Object) sprites[index]).name == this.EventShopInfo.banner_sprite)
            this.banner.sprite = sprites[index];
        }
      }
      EventCoinData data = MonoSingleton<GameManager>.Instance.Player.EventCoinList.Find((Predicate<EventCoinData>) (f => f.iname.Equals(this.EventShopInfo.shop_cost_iname)));
      if (data == null)
      {
        data = new EventCoinData();
        data.param = MonoSingleton<GameManager>.Instance.MasterParam.Items.Find((Predicate<ItemParam>) (f => f.iname.Equals(this.EventShopInfo.shop_cost_iname)));
      }
      DataSource.Bind<ItemParam>(this.mPaidCoinIcon, data.param);
      DataSource.Bind<EventCoinData>(this.mPaidCoinNum, data);
      if (shops.unlock == null || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mLockObject, (UnityEngine.Object) null) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mLockText, (UnityEngine.Object) null))
        return;
      bool flag = shops.unlock.flg == 1;
      Button component = ((Component) this).GetComponent<Button>();
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) component))
        return;
      if (flag)
      {
        ((Selectable) component).interactable = true;
        this.mLockObject.SetActive(false);
        this.mLockText.text = string.Empty;
      }
      else
      {
        ((Selectable) component).interactable = false;
        this.mLockObject.SetActive(true);
        this.mLockText.text = shops.unlock.message != null ? shops.unlock.message : string.Empty;
      }
    }

    private void Refresh()
    {
      if (this.mEndTime <= 0L)
      {
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Body, (UnityEngine.Object) null))
          return;
        this.Body.SetActive(false);
      }
      else
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Body, (UnityEngine.Object) null))
          this.Body.SetActive(true);
        DateTime serverTime = TimeManager.ServerTime;
        TimeSpan timeSpan = TimeManager.FromUnixTime(this.mEndTime) - serverTime;
        string str;
        if (timeSpan.TotalDays >= 1.0)
          str = LocalizedText.Get("sys.QUEST_TIMELIMIT_D", (object) timeSpan.Days);
        else if (timeSpan.TotalHours >= 1.0)
          str = LocalizedText.Get("sys.QUEST_TIMELIMIT_H", (object) timeSpan.Hours);
        else
          str = LocalizedText.Get("sys.QUEST_TIMELIMIT_M", (object) Mathf.Max(timeSpan.Minutes, 0));
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Timer, (UnityEngine.Object) null) || !(this.Timer.text != str))
          return;
        this.Timer.text = str;
      }
    }

    public void UpdateValue()
    {
      this.mEndTime = 0L;
      if (this.EventShopInfo.shops == null)
        return;
      this.mEndTime = this.EventShopInfo.shops.end;
      this.Refresh();
    }
  }
}
