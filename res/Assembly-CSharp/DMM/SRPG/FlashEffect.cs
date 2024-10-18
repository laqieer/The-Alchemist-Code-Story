// Decompiled with JetBrains decompiler
// Type: SRPG.FlashEffect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class FlashEffect : MonoBehaviour
  {
    private RenderPipeline mTarget;
    public float Strength = 1f;
    public float Duration = 0.3f;
    private float mTime;

    private void Start()
    {
      this.mTarget = ((Component) this).GetComponent<RenderPipeline>();
      if (!Object.op_Equality((Object) this.mTarget, (Object) null))
        return;
      Object.Destroy((Object) this);
    }

    private void OnDestroy()
    {
      if (!Object.op_Inequality((Object) this.mTarget, (Object) null))
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
      Object.Destroy((Object) this);
    }
  }
}
