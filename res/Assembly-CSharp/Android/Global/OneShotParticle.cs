﻿// Decompiled with JetBrains decompiler
// Type: OneShotParticle
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

[DisallowMultipleComponent]
public class OneShotParticle : MonoBehaviour
{
  private void LateUpdate()
  {
    ParticleSystem[] componentsInChildren1 = this.gameObject.GetComponentsInChildren<ParticleSystem>();
    for (int index = componentsInChildren1.Length - 1; index >= 0; --index)
    {
      if (componentsInChildren1[index].IsAlive())
        return;
    }
    UIParticleSystem[] componentsInChildren2 = this.gameObject.GetComponentsInChildren<UIParticleSystem>();
    for (int index = componentsInChildren2.Length - 1; index >= 0; --index)
    {
      if (componentsInChildren2[index].IsAlive())
        return;
    }
    Object.Destroy((Object) this.gameObject);
  }
}
