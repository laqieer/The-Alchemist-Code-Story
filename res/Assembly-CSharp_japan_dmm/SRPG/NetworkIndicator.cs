// Decompiled with JetBrains decompiler
// Type: SRPG.NetworkIndicator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class NetworkIndicator : MonoBehaviour
  {
    public GameObject Body;
    public float FadeTime = 1f;
    public float KeepUp = 0.5f;
    private CanvasGroup mCanvasGroup;
    private float mRemainingTime;

    private void Start()
    {
      if (!Object.op_Inequality((Object) this.Body, (Object) null))
        return;
      this.mCanvasGroup = this.Body.GetComponent<CanvasGroup>();
      this.Body.SetActive(false);
    }

    private void Update()
    {
      if (!Network.IsIndicator)
      {
        this.Body.SetActive(false);
      }
      else
      {
        if (Network.IsBusy || !AssetDownloader.isDone || FlowNode_NetworkIndicator.NeedDisplay() || EventAction.IsLoading)
          this.mRemainingTime = this.KeepUp + this.FadeTime;
        if ((double) this.mRemainingTime <= 0.0)
          return;
        this.mRemainingTime -= Time.unscaledDeltaTime;
        if (Object.op_Inequality((Object) this.mCanvasGroup, (Object) null) && (double) this.FadeTime > 0.0)
          this.mCanvasGroup.alpha = Mathf.Clamp01(this.mRemainingTime / this.FadeTime);
        if (!Object.op_Inequality((Object) this.Body, (Object) null))
          return;
        this.Body.SetActive((double) this.mRemainingTime > 0.0);
      }
    }
  }
}
