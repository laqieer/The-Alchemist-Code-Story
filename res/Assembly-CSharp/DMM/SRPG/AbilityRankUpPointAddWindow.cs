// Decompiled with JetBrains decompiler
// Type: SRPG.AbilityRankUpPointAddWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "Close", FlowNode.PinTypes.Output, 0)]
  public class AbilityRankUpPointAddWindow : MonoBehaviour, IFlowInterface
  {
    [SerializeField]
    private Text ConfirmText;
    [SerializeField]
    private Text FreeCoin;
    [SerializeField]
    private Text PaidCoin;
    [SerializeField]
    private Text CurrentAmountCoin;
    [SerializeField]
    private Button CancelButton;
    [SerializeField]
    private Button DecideButton;
    [SerializeField]
    private Slider SelectSlider;
    [SerializeField]
    private Button PlusButton;
    [SerializeField]
    private Button MinusButton;
    [SerializeField]
    private Text SelectTotalNum;
    public AbilityRankUpPointAddWindow.DecideEvent OnDecide;
    public AbilityRankUpPointAddWindow.CancelEvent OnCancel;
    private GameManager gm;

    private void Start()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.gm, (UnityEngine.Object) null))
        this.gm = MonoSingleton<GameManager>.GetInstanceDirect();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CancelButton, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.CancelButton.onClick).AddListener(new UnityAction((object) this, __methodptr(Cancel)));
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DecideButton, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.DecideButton.onClick).AddListener(new UnityAction((object) this, __methodptr(Decide)));
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PlusButton, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.PlusButton.onClick).AddListener(new UnityAction((object) this, __methodptr(OnAdd)));
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.MinusButton, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.MinusButton.onClick).AddListener(new UnityAction((object) this, __methodptr(OnRemove)));
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SelectSlider, (UnityEngine.Object) null))
      {
        int num = Mathf.Min(this.gm.Player.Coin, Math.Min((int) this.gm.MasterParam.FixParam.AbilityRankUpPointAddMax, (int) this.gm.MasterParam.FixParam.AbilityRankUpPointMax - this.gm.Player.AbilityRankUpCountNum));
        this.SelectSlider.minValue = 1f;
        this.SelectSlider.maxValue = (float) num;
        // ISSUE: method pointer
        ((UnityEvent<float>) this.SelectSlider.onValueChanged).AddListener(new UnityAction<float>((object) this, __methodptr(OnValueChanged)));
        this.SelectSlider.value = this.SelectSlider.minValue;
      }
      this.Refresh();
    }

    private void Refresh()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.gm, (UnityEngine.Object) null))
        return;
      int num1 = this.gm.Player.FreeCoin + this.gm.Player.ComCoin;
      int paidCoin = this.gm.Player.PaidCoin;
      int coin = this.gm.Player.Coin;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.FreeCoin, (UnityEngine.Object) null))
        this.FreeCoin.text = num1.ToString();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PaidCoin, (UnityEngine.Object) null))
        this.PaidCoin.text = paidCoin.ToString();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CurrentAmountCoin, (UnityEngine.Object) null))
        this.CurrentAmountCoin.text = coin.ToString();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SelectTotalNum, (UnityEngine.Object) null))
        this.SelectTotalNum.text = "+" + this.SelectSlider.value.ToString();
      int num2 = (int) this.SelectSlider.value;
      string str = LocalizedText.Get("sys.CONFIRM_ABILITY_RANKUP_POINT_ADD", (object) ((int) this.gm.MasterParam.FixParam.AbilityRankupPointCoinRate * num2), (object) num2);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ConfirmText, (UnityEngine.Object) null))
        return;
      this.ConfirmText.text = str;
    }

    private void OnAdd()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SelectSlider, (UnityEngine.Object) null))
        return;
      this.SelectSlider.value = Mathf.Min(this.SelectSlider.maxValue, this.SelectSlider.value + 1f);
    }

    private void OnRemove()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SelectSlider, (UnityEngine.Object) null))
        return;
      this.SelectSlider.value = Mathf.Max(this.SelectSlider.minValue, this.SelectSlider.value - 1f);
    }

    private void Cancel()
    {
      if (this.OnCancel != null)
        this.OnCancel();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 1);
    }

    private void Decide()
    {
      if (this.OnDecide != null)
        this.OnDecide((int) this.SelectSlider.value);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 1);
    }

    private void OnValueChanged(float value)
    {
      this.SelectTotalNum.text = "+" + ((int) value).ToString();
      int num = (int) this.SelectSlider.value;
      string str = LocalizedText.Get("sys.CONFIRM_ABILITY_RANKUP_POINT_ADD", (object) ((int) this.gm.MasterParam.FixParam.AbilityRankupPointCoinRate * num), (object) num);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ConfirmText, (UnityEngine.Object) null))
        return;
      this.ConfirmText.text = str;
    }

    public void Activated(int pinID)
    {
    }

    public delegate void DecideEvent(int value);

    public delegate void CancelEvent();
  }
}
