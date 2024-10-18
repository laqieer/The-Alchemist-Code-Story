// Decompiled with JetBrains decompiler
// Type: SRPG.LimitedShopListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class LimitedShopListItem : MonoBehaviour
  {
    private float mRefreshInterval = 1f;
    public string LimitedShopSpritePath = "LimitedShopBanner/LimitedShopSprites";
    public JSON_ShopListArray.Shops shops;
    public LText l_text;
    public GameObject Body;
    public Text Timer;
    private long mEndTime;
    public Image banner;
    public string banner_sprite;
    public bool btn_update;

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
      if (shops.info != null)
      {
        if (shops.info.msg != null)
        {
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
            if (jsonShopMsgResponse.update != null)
              this.btn_update = jsonShopMsgResponse.update.Equals("on");
          }
        }
      }
      GachaTabSprites gachaTabSprites = AssetManager.Load<GachaTabSprites>(this.LimitedShopSpritePath);
      if (!((UnityEngine.Object) gachaTabSprites != (UnityEngine.Object) null) || gachaTabSprites.Sprites == null || gachaTabSprites.Sprites.Length <= 0)
        return;
      Sprite[] sprites = gachaTabSprites.Sprites;
      for (int index = 0; index < sprites.Length; ++index)
      {
        if ((UnityEngine.Object) sprites[index] != (UnityEngine.Object) null && sprites[index].name == this.banner_sprite)
          this.banner.sprite = sprites[index];
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
