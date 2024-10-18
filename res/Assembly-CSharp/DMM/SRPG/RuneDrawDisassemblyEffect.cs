// Decompiled with JetBrains decompiler
// Type: SRPG.RuneDrawDisassemblyEffect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class RuneDrawDisassemblyEffect : MonoBehaviour
  {
    [SerializeField]
    private ImageArray[] mTitleImage;
    [SerializeField]
    private Animator mAnimator;
    private readonly string AnimatorTrigger = "success";
    private ReqRuneDisassembly.Response.Result mResult;

    public void Awake()
    {
    }

    public void OnDestroy()
    {
    }

    public void SetDrawParam(ReqRuneDisassembly.Response.Result result)
    {
      this.mResult = result;
      this.Refresh();
    }

    public void Refresh()
    {
      if (Object.op_Implicit((Object) this.mAnimator))
        this.mAnimator.SetTrigger(this.AnimatorTrigger);
      if (this.mTitleImage == null)
        return;
      for (int index = 0; index < this.mTitleImage.Length; ++index)
      {
        if (Object.op_Inequality((Object) this.mTitleImage[index], (Object) null))
          this.mTitleImage[index].ImageIndex = (int) this.mResult;
      }
    }
  }
}
