// Decompiled with JetBrains decompiler
// Type: OneShotParticle
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
[DisallowMultipleComponent]
public class OneShotParticle : MonoBehaviour
{
  private void LateUpdate()
  {
    ParticleSystem[] componentsInChildren1 = ((Component) this).gameObject.GetComponentsInChildren<ParticleSystem>();
    for (int index = componentsInChildren1.Length - 1; index >= 0; --index)
    {
      if (componentsInChildren1[index].IsAlive())
        return;
    }
    UIParticleSystem[] componentsInChildren2 = ((Component) this).gameObject.GetComponentsInChildren<UIParticleSystem>();
    for (int index = componentsInChildren2.Length - 1; index >= 0; --index)
    {
      if (componentsInChildren2[index].IsAlive())
        return;
    }
    Object.Destroy((Object) ((Component) this).gameObject);
  }
}
