// Decompiled with JetBrains decompiler
// Type: SRPG.RuneDrawEvoStateOneSetting
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
  public class RuneDrawEvoStateOneSetting : MonoBehaviour
  {
    [SerializeField]
    private StatusList mEvoStatusList;
    [SerializeField]
    private GameObject mEvoStatusTexts;
    [SerializeField]
    private GameObject mEvoStatusLock;
    [SerializeField]
    private Image mEvoStatusGauge;
    [SerializeField]
    private Image mFrameImage;
    [Space(10f)]
    [SerializeField]
    private float mAnimTime = 1f;
    private BaseStatus mAddStatus;
    private BaseStatus mScaleStatus;
    private float mPercentage;
    private float mStartPercentage;
    private bool mIsShowFrame;
    private bool mIsAnim;

    public void Awake()
    {
    }

    public void StartGaugeAnim()
    {
      this.mIsAnim = true;
      this.StartCoroutine(this.GaugeAnim());
    }

    [DebuggerHidden]
    private IEnumerator GaugeAnim()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new RuneDrawEvoStateOneSetting.\u003CGaugeAnim\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    public void SetStatus(
      BaseStatus add_status,
      BaseStatus scale_status,
      float percentage,
      bool is_anim = false,
      float start_percentage = 0.0f)
    {
      this.mAddStatus = add_status;
      this.mScaleStatus = scale_status;
      this.mPercentage = percentage;
      this.mStartPercentage = start_percentage;
      this.mIsAnim = is_anim;
      this.Refresh();
    }

    public void SetShowFrame(bool is_show)
    {
      this.mIsShowFrame = is_show;
      this.RefreshFrame();
    }

    public void Refresh()
    {
      this.RefreshParam();
      this.RefreshFrame();
    }

    private void RefreshParam()
    {
      if (Object.op_Equality((Object) this.mEvoStatusList, (Object) null))
        return;
      if (this.mAddStatus != null && this.mScaleStatus != null)
      {
        GameUtility.SetGameObjectActive(this.mEvoStatusLock, false);
        this.mEvoStatusList.SetValues(this.mAddStatus, this.mScaleStatus);
        if (!Object.op_Implicit((Object) this.mEvoStatusGauge))
          return;
        this.mEvoStatusGauge.fillAmount = this.mIsAnim ? this.mStartPercentage : this.mPercentage;
      }
      else
      {
        GameUtility.SetGameObjectActive(this.mEvoStatusLock, true);
        if (!Object.op_Implicit((Object) this.mEvoStatusGauge))
          return;
        this.mEvoStatusGauge.fillAmount = 0.0f;
      }
    }

    private void RefreshFrame()
    {
      if (!Object.op_Implicit((Object) this.mFrameImage))
        return;
      ((Behaviour) this.mFrameImage).enabled = this.mIsShowFrame;
    }
  }
}
