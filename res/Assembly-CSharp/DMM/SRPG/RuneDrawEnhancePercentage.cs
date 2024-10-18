// Decompiled with JetBrains decompiler
// Type: SRPG.RuneDrawEnhancePercentage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class RuneDrawEnhancePercentage : MonoBehaviour
  {
    [SerializeField]
    private Text mGaugeText;
    [SerializeField]
    private Image mGauge;
    [SerializeField]
    private ImageArray mGaugeButtonImage;
    [SerializeField]
    private Text mPercentage;
    [SerializeField]
    private float mAnimTime = 1f;
    [SerializeField]
    private GameObject mGaugeStartSE;
    [SerializeField]
    private GameObject mGaugeFullSE;
    private BindRuneData mRuneData;
    private bool mIsUseEnhanceGauge;
    private bool mIsGaugeAnim;
    private float mStartGauge;

    public void Awake()
    {
    }

    public void OnEnable() => this.StartGaugeAnim();

    public void SetDrawParam(BindRuneData rune_data, bool is_use_enhance_gauge)
    {
      this.mRuneData = rune_data;
      this.mIsUseEnhanceGauge = is_use_enhance_gauge;
      this.Refresh();
    }

    public void SetupGaugeAnim(float _start_gauge)
    {
      if (this.mIsGaugeAnim)
        return;
      this.mStartGauge = _start_gauge;
      if ((double) this.mStartGauge >= (double) RuneUtility.CalcPlayerGauge(this.mRuneData))
        return;
      this.mIsGaugeAnim = true;
      if (Object.op_Implicit((Object) this.mGaugeStartSE))
        this.mGaugeStartSE.SetActive(false);
      if (!Object.op_Implicit((Object) this.mGaugeFullSE))
        return;
      this.mGaugeFullSE.SetActive(false);
    }

    public void Refresh()
    {
      if (Object.op_Inequality((Object) this.mGaugeButtonImage, (Object) null))
        this.mGaugeButtonImage.ImageIndex = !this.mIsUseEnhanceGauge ? 0 : 1;
      int num1 = RuneUtility.CalcSuccessPercentage(this.mRuneData);
      int num2 = RuneUtility.CalcPlayerGauge(this.mRuneData);
      int num3;
      if (this.mIsUseEnhanceGauge && RuneUtility.IsCanUseGauge(this.mRuneData))
      {
        int num4 = num1 + num2;
        num3 = Mathf.Min(RuneUtility.CAN_USE_GAUGE_PERCENTAGE, num4);
      }
      else
        num3 = num1;
      if (Object.op_Implicit((Object) this.mPercentage))
        this.mPercentage.text = num3.ToString();
      if (Object.op_Implicit((Object) this.mGauge))
        this.mGauge.fillAmount = (float) num2 / 100f;
      if (!Object.op_Implicit((Object) this.mGaugeText))
        return;
      this.mGaugeText.text = num2.ToString();
    }

    public void StartGaugeAnim()
    {
      if (!this.mIsGaugeAnim)
        return;
      this.StartCoroutine(this.GaugeAnim());
    }

    [DebuggerHidden]
    private IEnumerator GaugeAnim()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new RuneDrawEnhancePercentage.\u003CGaugeAnim\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }
  }
}
