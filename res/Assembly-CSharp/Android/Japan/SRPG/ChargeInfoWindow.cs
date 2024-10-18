// Decompiled with JetBrains decompiler
// Type: SRPG.ChargeInfoWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

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
    private string m_CurrentAppealImg = string.Empty;
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
      if ((UnityEngine.Object) this.MoveBuyButton != (UnityEngine.Object) null)
        this.MoveBuyButton.SetActive(false);
      if (!((UnityEngine.Object) this.MoveResultButton != (UnityEngine.Object) null))
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
      return (IEnumerator) new ChargeInfoWindow.\u003CCreateResultView\u003Ec__Iterator0() { \u0024this = this };
    }

    private void Refresh()
    {
      if ((UnityEngine.Object) this.AppealObject != (UnityEngine.Object) null)
      {
        Image component = this.AppealObject.GetComponent<Image>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null)
          component.sprite = this.m_CacheAppealSprite;
      }
      FirstChargeState firstChargeStatus = (FirstChargeState) MonoSingleton<GameManager>.Instance.Player.FirstChargeStatus;
      if ((UnityEngine.Object) this.MoveResultButton != (UnityEngine.Object) null)
        this.MoveResultButton.SetActive(firstChargeStatus == FirstChargeState.Purchased);
      if (!((UnityEngine.Object) this.MoveBuyButton != (UnityEngine.Object) null))
        return;
      this.MoveBuyButton.SetActive(firstChargeStatus == FirstChargeState.NotPurchase);
    }

    [DebuggerHidden]
    private IEnumerator LoadImages(string _path)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ChargeInfoWindow.\u003CLoadImages\u003Ec__Iterator1() { _path = _path, \u0024this = this };
    }
  }
}
