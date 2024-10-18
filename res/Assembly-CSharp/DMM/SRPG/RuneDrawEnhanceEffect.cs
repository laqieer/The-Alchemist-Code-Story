// Decompiled with JetBrains decompiler
// Type: SRPG.RuneDrawEnhanceEffect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class RuneDrawEnhanceEffect : MonoBehaviour
  {
    [SerializeField]
    private ImageArray[] mTitleImage;
    [SerializeField]
    private GameObject mSwitchObj_success;
    [SerializeField]
    private GameObject mSwitchObj_miss;
    [SerializeField]
    private RawImage mSuccessBg_left;
    [SerializeField]
    private RawImage mSuccessBg_right;
    [SerializeField]
    private RawImage mMissBg_left;
    [SerializeField]
    private RawImage mMissBg_right;
    [SerializeField]
    private Animator mAnimator;
    private const string ENHANCE_EFFECT_BG_IMAGE_LEFT = "UI/Rune/RuneBG_005_00";
    private const string ENHANCE_EFFECT_BG_IMAGE_RIGHT = "UI/Rune/RuneBG_005_01";
    private readonly string[] AnimatorTrigger = new string[2]
    {
      "miss",
      "success"
    };
    private bool mIsEnhanceSuccess;

    public bool IsEnhanceSuccess => this.mIsEnhanceSuccess;

    public void Awake()
    {
    }

    public void OnDestroy()
    {
      MonoSingleton<GameManager>.Instance.CancelTextureLoadRequest(this.mSuccessBg_left);
      MonoSingleton<GameManager>.Instance.CancelTextureLoadRequest(this.mSuccessBg_right);
      MonoSingleton<GameManager>.Instance.CancelTextureLoadRequest(this.mMissBg_left);
      MonoSingleton<GameManager>.Instance.CancelTextureLoadRequest(this.mMissBg_right);
    }

    public void SetDrawParam(bool is_success)
    {
      this.mIsEnhanceSuccess = is_success;
      this.Refresh();
    }

    public void Refresh()
    {
      if (Object.op_Implicit((Object) this.mAnimator))
        this.mAnimator.SetTrigger(this.AnimatorTrigger[!this.mIsEnhanceSuccess ? 0 : 1]);
      this.LoadBgImages(this.mIsEnhanceSuccess);
      if (this.mTitleImage != null)
      {
        for (int index = 0; index < this.mTitleImage.Length; ++index)
        {
          if (Object.op_Inequality((Object) this.mTitleImage[index], (Object) null))
            this.mTitleImage[index].ImageIndex = !this.mIsEnhanceSuccess ? 0 : 1;
        }
      }
      GameUtility.SetGameObjectActive(this.mSwitchObj_success, this.mIsEnhanceSuccess);
      GameUtility.SetGameObjectActive(this.mSwitchObj_miss, !this.mIsEnhanceSuccess);
    }

    private void LoadBgImages(bool is_success)
    {
      if (is_success)
      {
        MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.mSuccessBg_left, "UI/Rune/RuneBG_005_00");
        MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.mSuccessBg_right, "UI/Rune/RuneBG_005_01");
      }
      else
      {
        MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.mMissBg_left, "UI/Rune/RuneBG_005_00");
        MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.mMissBg_right, "UI/Rune/RuneBG_005_01");
      }
    }

    private enum Result
    {
      Miss,
      Success,
    }
  }
}
