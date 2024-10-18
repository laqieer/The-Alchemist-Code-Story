// Decompiled with JetBrains decompiler
// Type: SRPG.AppealItemEventShop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class AppealItemEventShop : AppealItemBase
  {
    private readonly string SPRITES_PATH = "AppealSprites/eventshop";
    private readonly string MASTER_PATH = "Data/appeal/AppealEventShop";
    [SerializeField]
    private Image AppealChara;
    [SerializeField]
    private RectTransform AppealCharaRect;
    [SerializeField]
    private Image AppealTextL;
    [SerializeField]
    private Image AppealTextR;
    [SerializeField]
    private RectTransform AppealTextRect;
    [SerializeField]
    private SRPG_Button EventShopButton;
    [SerializeField]
    private GameObject LockObject;
    private string mAppealID;
    private float mPosX_Chara;
    private float mPosX_Text;
    private bool IsLoaded;
    private bool IsInitalize;
    private Sprite CharaSprite;
    private Sprite TextLSprite;
    private Sprite TextRSprite;

    protected override void Awake()
    {
      if ((UnityEngine.Object) this.AppealChara != (UnityEngine.Object) null)
        this.AppealChara.gameObject.SetActive(false);
      if ((UnityEngine.Object) this.AppealTextL != (UnityEngine.Object) null)
        this.AppealTextL.gameObject.SetActive(false);
      if ((UnityEngine.Object) this.AppealTextR != (UnityEngine.Object) null)
        this.AppealTextR.gameObject.SetActive(false);
      if ((UnityEngine.Object) this.AppealCharaRect != (UnityEngine.Object) null)
        this.mPosX_Chara = this.AppealCharaRect.anchoredPosition.x;
      if (!((UnityEngine.Object) this.AppealTextRect != (UnityEngine.Object) null))
        return;
      this.mPosX_Text = this.AppealTextRect.anchoredPosition.x;
    }

    protected override void Start()
    {
      if (this.LoadAppealMaster(this.MASTER_PATH))
        this.StartCoroutine(this.LoadAppealResources(this.SPRITES_PATH));
      if (string.IsNullOrEmpty(this.mAppealID) && (UnityEngine.Object) this.EventShopButton != (UnityEngine.Object) null)
      {
        this.EventShopButton.interactable = false;
        if ((UnityEngine.Object) this.LockObject != (UnityEngine.Object) null)
          this.LockObject.SetActive(true);
      }
      GlobalVars.IsEventShopOpen.Set(!string.IsNullOrEmpty(this.mAppealID));
    }

    protected override void Update()
    {
      if (!this.IsLoaded || this.IsInitalize)
        return;
      this.Refresh();
    }

    protected override void Destroy()
    {
      base.Destroy();
    }

    protected override void Refresh()
    {
      this.AppealChara.gameObject.SetActive((UnityEngine.Object) this.AppealChara != (UnityEngine.Object) null && (UnityEngine.Object) this.CharaSprite != (UnityEngine.Object) null);
      this.AppealTextL.gameObject.SetActive((UnityEngine.Object) this.AppealTextL != (UnityEngine.Object) null && (UnityEngine.Object) this.TextLSprite != (UnityEngine.Object) null);
      this.AppealTextR.gameObject.SetActive((UnityEngine.Object) this.AppealTextR != (UnityEngine.Object) null && (UnityEngine.Object) this.TextRSprite != (UnityEngine.Object) null);
      this.AppealChara.sprite = this.CharaSprite;
      this.AppealTextL.sprite = this.TextLSprite;
      this.AppealTextR.sprite = this.TextRSprite;
      this.AppealCharaRect.anchoredPosition = new Vector2(this.mPosX_Chara, this.AppealCharaRect.anchoredPosition.y);
      this.AppealTextRect.anchoredPosition = new Vector2(this.mPosX_Text, this.AppealTextRect.anchoredPosition.y);
      this.EventShopButton.interactable = true;
      if ((UnityEngine.Object) this.LockObject != (UnityEngine.Object) null)
        this.LockObject.SetActive(false);
      this.IsInitalize = true;
    }

    private bool LoadAppealMaster(string path)
    {
      if (string.IsNullOrEmpty(path))
        return false;
      string src = AssetManager.LoadTextData(path);
      if (string.IsNullOrEmpty(src))
        return false;
      try
      {
        JSON_AppealEventShopMaster[] jsonArray = JSONParser.parseJSONArray<JSON_AppealEventShopMaster>(src);
        if (jsonArray == null)
          throw new InvalidJSONException();
        long serverTime = Network.GetServerTime();
        AppealEventShopMaster appealEventShopMaster1 = new AppealEventShopMaster();
        foreach (JSON_AppealEventShopMaster json in jsonArray)
        {
          AppealEventShopMaster appealEventShopMaster2 = new AppealEventShopMaster();
          if (appealEventShopMaster2.Deserialize(json) && appealEventShopMaster2.start_at <= serverTime && appealEventShopMaster2.end_at > serverTime)
          {
            if (appealEventShopMaster1 == null)
              appealEventShopMaster1 = appealEventShopMaster2;
            else if (appealEventShopMaster1.priority < appealEventShopMaster2.priority)
              appealEventShopMaster1 = appealEventShopMaster2;
          }
        }
        if (appealEventShopMaster1 != null)
        {
          this.mAppealID = appealEventShopMaster1.appeal_id;
          this.mPosX_Chara = appealEventShopMaster1.position_chara;
          this.mPosX_Text = appealEventShopMaster1.position_text;
        }
      }
      catch (Exception ex)
      {
        DebugUtility.LogException(ex);
        return false;
      }
      return true;
    }

    [DebuggerHidden]
    private IEnumerator LoadAppealResources(string path)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new AppealItemEventShop.\u003CLoadAppealResources\u003Ec__IteratorE3() { path = path, \u003C\u0024\u003Epath = path, \u003C\u003Ef__this = this };
    }
  }
}
