﻿// Decompiled with JetBrains decompiler
// Type: SRPG.AppealItemEventShop
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
      if (this.LoadAppealMaster(this.MASTER_PATH))
        this.StartCoroutine(this.LoadAppealResources(this.SPRITES_PATH));
      GlobalVars.IsEventShopOpen.Set(!string.IsNullOrEmpty(this.mAppealID));
    }

    protected override void Update()
    {
      if (!this.IsLoaded || this.IsInitalize)
        return;
      this.Refresh();
    }

    protected override void Destroy() => base.Destroy();

    protected override void Refresh()
    {
      ((Component) this.AppealChara).gameObject.SetActive(UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AppealChara, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CharaSprite, (UnityEngine.Object) null));
      ((Component) this.AppealTextL).gameObject.SetActive(UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AppealTextL, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TextLSprite, (UnityEngine.Object) null));
      ((Component) this.AppealTextR).gameObject.SetActive(UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AppealTextR, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TextRSprite, (UnityEngine.Object) null));
      this.AppealChara.sprite = this.CharaSprite;
      this.AppealTextL.sprite = this.TextLSprite;
      this.AppealTextR.sprite = this.TextRSprite;
      this.AppealCharaRect.anchoredPosition = new Vector2(this.mPosX_Chara, this.AppealCharaRect.anchoredPosition.y);
      this.AppealTextRect.anchoredPosition = new Vector2(this.mPosX_Text, this.AppealTextRect.anchoredPosition.y);
      ((Selectable) this.EventShopButton).interactable = true;
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
      return (IEnumerator) new AppealItemEventShop.\u003CLoadAppealResources\u003Ec__Iterator0()
      {
        path = path,
        \u0024this = this
      };
    }
  }
}
