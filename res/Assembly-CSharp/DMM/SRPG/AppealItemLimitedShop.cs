// Decompiled with JetBrains decompiler
// Type: SRPG.AppealItemLimitedShop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class AppealItemLimitedShop : AppealItemBase
  {
    private readonly string SPRITES_PATH = "AppealSprites/limitedshop";
    private readonly string MASTER_PATH = "Data/appeal/AppealLimitedShop";
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
    private Button LimitedShopButton;
    [SerializeField]
    private GameObject LockObject;
    private string mAppealID;
    private float mPosX_Chara;
    private float mPosX_Text;
    private bool IsLoaded;
    private bool mIsInitialized;
    private Sprite CharaSprite;
    private Sprite TextLSprite;
    private Sprite TextRSprite;

    public bool IsInitialized => this.mIsInitialized;

    protected override void Awake()
    {
      base.Awake();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AppealChara, (UnityEngine.Object) null))
        ((Component) this.AppealChara).gameObject.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AppealTextL, (UnityEngine.Object) null))
        ((Component) this.AppealTextL).gameObject.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AppealTextR, (UnityEngine.Object) null))
        ((Component) this.AppealTextR).gameObject.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AppealCharaRect, (UnityEngine.Object) null))
        this.mPosX_Chara = this.AppealCharaRect.anchoredPosition.x;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AppealTextRect, (UnityEngine.Object) null))
        return;
      this.mPosX_Text = this.AppealTextRect.anchoredPosition.x;
    }

    protected override void Start()
    {
      base.Start();
      if (this.LoadAppealMaster(this.MASTER_PATH))
        this.StartCoroutine(this.LoadAppealResourcess(this.SPRITES_PATH));
      MonoSingleton<GameManager>.Instance.IsLimitedShopOpen = true;
      if (!string.IsNullOrEmpty(this.mAppealID) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.LimitedShopButton, (UnityEngine.Object) null))
        return;
      ((Selectable) this.LimitedShopButton).interactable = false;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.LockObject, (UnityEngine.Object) null))
        return;
      MonoSingleton<GameManager>.Instance.IsLimitedShopOpen = false;
      this.LockObject.SetActive(true);
    }

    protected override void Update()
    {
      if (!this.IsLoaded || this.mIsInitialized)
        return;
      this.Refresh();
    }

    protected override void Refresh()
    {
      ((Component) this.AppealChara).gameObject.SetActive(UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AppealChara, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CharaSprite, (UnityEngine.Object) null));
      ((Component) this.AppealTextL).gameObject.SetActive(UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AppealTextL, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TextLSprite, (UnityEngine.Object) null));
      ((Component) this.AppealTextR).gameObject.SetActive(UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AppealTextR, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TextRSprite, (UnityEngine.Object) null));
      this.AppealChara.sprite = this.CharaSprite;
      this.AppealTextL.sprite = this.TextLSprite;
      this.AppealTextR.sprite = this.TextRSprite;
      this.AppealTextRect.anchoredPosition = new Vector2(this.mPosX_Text, this.AppealTextRect.anchoredPosition.y);
      this.AppealCharaRect.anchoredPosition = new Vector2(this.mPosX_Chara, this.AppealCharaRect.anchoredPosition.y);
      ((Selectable) this.LimitedShopButton).interactable = true;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.LockObject, (UnityEngine.Object) null))
        this.LockObject.SetActive(false);
      this.mIsInitialized = true;
    }

    protected override void Destroy() => base.Destroy();

    private bool LoadAppealMaster(string path)
    {
      if (string.IsNullOrEmpty(path))
        return false;
      string src = AssetManager.LoadTextData(path);
      if (string.IsNullOrEmpty(src))
        return false;
      try
      {
        JSON_AppealLimitedShopMaster[] jsonArray = JSONParser.parseJSONArray<JSON_AppealLimitedShopMaster>(src);
        if (jsonArray == null)
          throw new InvalidJSONException();
        long serverTime = Network.GetServerTime();
        AppealLimitedShopMaster limitedShopMaster1 = new AppealLimitedShopMaster();
        foreach (JSON_AppealLimitedShopMaster json in jsonArray)
        {
          AppealLimitedShopMaster limitedShopMaster2 = new AppealLimitedShopMaster();
          if (limitedShopMaster2.Deserialize(json) && limitedShopMaster2.start_at <= serverTime && limitedShopMaster2.end_at > serverTime)
          {
            if (limitedShopMaster1 == null)
              limitedShopMaster1 = limitedShopMaster2;
            else if (limitedShopMaster1.priority < limitedShopMaster2.priority)
              limitedShopMaster1 = limitedShopMaster2;
          }
        }
        if (limitedShopMaster1 != null)
        {
          this.mAppealID = limitedShopMaster1.appeal_id;
          this.mPosX_Chara = limitedShopMaster1.pos_x_chara;
          this.mPosX_Text = limitedShopMaster1.pos_x_text;
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
    private IEnumerator LoadAppealResourcess(string path)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new AppealItemLimitedShop.\u003CLoadAppealResourcess\u003Ec__Iterator0()
      {
        path = path,
        \u0024this = this
      };
    }
  }
}
