﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ChargeInfoWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(0, "ClickBuyCoin", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "ClickResult", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(10, "ToBuyCoin", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "ToResult", FlowNode.PinTypes.Output, 11)]
  public class ChargeInfoWindow : MonoBehaviour, IFlowInterface
  {
    private static readonly string SPRITE_SHEET_PATH = "ChargeInfo/ChargeInfo";
    private static readonly string CHARGERESULT_PATH = "UI/ChargeInfoResult";
    private const int INPUT_CLICK_BUYCOIN = 0;
    private const int INPUT_CLICK_RESULT = 1;
    private const int OUTPUT_TO_BUYCOIN = 10;
    private const int OUTPUT_TO_RESULT = 11;
    [SerializeField]
    private GameObject AppealObject;
    [SerializeField]
    private GameObject MoveBuyButton;
    [SerializeField]
    private GameObject MoveResultButton;
    private string m_CurrentAppealImg = string.Empty;
    private bool m_loaded;
    private bool m_Refresh;
    private Sprite m_CacheAppealSprite;

    public void Activated(int pinID)
    {
      if (pinID == 0)
      {
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
      }
      else
      {
        if (pinID != 1)
          return;
        this.StartCoroutine(this.CreateResultView());
      }
    }

    private void Start()
    {
      if (Object.op_Inequality((Object) this.MoveBuyButton, (Object) null))
        this.MoveBuyButton.SetActive(false);
      if (!Object.op_Inequality((Object) this.MoveResultButton, (Object) null))
        return;
      this.MoveResultButton.SetActive(false);
    }

    private void Update()
    {
      if (!this.m_loaded || this.m_Refresh)
        return;
      this.m_Refresh = true;
      this.Refresh();
    }

    public void Setup(string _img_id)
    {
      if (string.IsNullOrEmpty(_img_id))
      {
        DebugUtility.LogError("初回購入キャンペーンの有効な訴求IDがありません.");
      }
      else
      {
        this.m_CurrentAppealImg = _img_id;
        this.StartCoroutine(this.LoadImages(ChargeInfoWindow.SPRITE_SHEET_PATH));
      }
    }

    [DebuggerHidden]
    private IEnumerator CreateResultView()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ChargeInfoWindow.\u003CCreateResultView\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    private void Refresh()
    {
      if (Object.op_Inequality((Object) this.AppealObject, (Object) null))
      {
        Image component = this.AppealObject.GetComponent<Image>();
        if (Object.op_Inequality((Object) component, (Object) null))
          component.sprite = this.m_CacheAppealSprite;
      }
      FirstChargeState firstChargeStatus = (FirstChargeState) MonoSingleton<GameManager>.Instance.Player.FirstChargeStatus;
      if (Object.op_Inequality((Object) this.MoveResultButton, (Object) null))
        this.MoveResultButton.SetActive(firstChargeStatus == FirstChargeState.Purchased);
      if (!Object.op_Inequality((Object) this.MoveBuyButton, (Object) null))
        return;
      this.MoveBuyButton.SetActive(firstChargeStatus == FirstChargeState.NotPurchase);
    }

    [DebuggerHidden]
    private IEnumerator LoadImages(string _path)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ChargeInfoWindow.\u003CLoadImages\u003Ec__Iterator1()
      {
        _path = _path,
        \u0024this = this
      };
    }
  }
}
