// Decompiled with JetBrains decompiler
// Type: SRPG.NetworkIndicator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class NetworkIndicator : MonoBehaviour
  {
    public float FadeTime = 1f;
    public float KeepUp = 0.5f;
    public GameObject Body;
    private CanvasGroup mCanvasGroup;
    private float mRemainingTime;

    private void Start()
    {
      if (!((UnityEngine.Object) this.Body != (UnityEngine.Object) null))
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
        if (Network.IsBusy || !AssetDownloader.isDone || (FlowNode_NetworkIndicator.NeedDisplay() || EventAction.IsLoading))
          this.mRemainingTime = this.KeepUp + this.FadeTime;
        if ((double) this.mRemainingTime <= 0.0)
          return;
        this.mRemainingTime -= Time.unscaledDeltaTime;
        if ((UnityEngine.Object) this.mCanvasGroup != (UnityEngine.Object) null && (double) this.FadeTime > 0.0)
          this.mCanvasGroup.alpha = Mathf.Clamp01(this.mRemainingTime / this.FadeTime);
        if (!((UnityEngine.Object) this.Body != (UnityEngine.Object) null))
          return;
        this.Body.SetActive((double) this.mRemainingTime > 0.0);
      }
    }
  }
}
