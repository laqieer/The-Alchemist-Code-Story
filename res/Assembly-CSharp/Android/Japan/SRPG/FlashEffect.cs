// Decompiled with JetBrains decompiler
// Type: SRPG.FlashEffect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class FlashEffect : MonoBehaviour
  {
    public float Strength = 1f;
    public float Duration = 0.3f;
    private RenderPipeline mTarget;
    private float mTime;

    private void Start()
    {
      this.mTarget = this.GetComponent<RenderPipeline>();
      if (!((UnityEngine.Object) this.mTarget == (UnityEngine.Object) null))
        return;
      UnityEngine.Object.Destroy((UnityEngine.Object) this);
    }

    private void OnDestroy()
    {
      if (!((UnityEngine.Object) this.mTarget != (UnityEngine.Object) null))
        return;
      this.mTarget.SwapEffect = RenderPipeline.SwapEffects.Copy;
    }

    private void Update()
    {
      this.mTime += Time.deltaTime;
      float num = Mathf.Clamp01(this.mTime / this.Duration);
      this.mTarget.SwapEffect = RenderPipeline.SwapEffects.Dodge;
      this.mTarget.SwapEffectOpacity = (1f - num) * this.Strength;
      if ((double) num < 1.0)
        return;
      UnityEngine.Object.Destroy((UnityEngine.Object) this);
    }
  }
}
