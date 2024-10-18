// Decompiled with JetBrains decompiler
// Type: SRPG.DrawCardParticleEff
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class DrawCardParticleEff : MonoBehaviour
  {
    private UIParticleSystem[] mParticle;

    private void Awake()
    {
      this.mParticle = ((Component) this).gameObject.GetComponentsInChildren<UIParticleSystem>();
    }

    private void OnEnable()
    {
      if (this.mParticle == null)
        this.mParticle = ((Component) this).gameObject.GetComponentsInChildren<UIParticleSystem>();
      if (this.mParticle == null)
        return;
      foreach (UIParticleSystem uiParticleSystem in this.mParticle)
        uiParticleSystem.ResetParticleSystem();
    }
  }
}
