﻿// Decompiled with JetBrains decompiler
// Type: SRPG.AnimEvents.ParticleGenerator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG.AnimEvents
{
  public class ParticleGenerator : AnimEventWithTarget
  {
    public GameObject Template;
    public bool Attach;
    public bool NotParticle;

    public override void OnStart(GameObject go)
    {
      if ((UnityEngine.Object) this.Template == (UnityEngine.Object) null)
        return;
      Vector3 spawnPos;
      Quaternion spawnRot;
      this.CalcPosition(go, this.Template, out spawnPos, out spawnRot);
      GameObject go1 = (GameObject) UnityEngine.Object.Instantiate((UnityEngine.Object) this.Template, spawnPos, spawnRot);
      if (!this.NotParticle)
        GameUtility.RequireComponent<OneShotParticle>(go1);
      if ((double) go.transform.lossyScale.x * (double) go.transform.lossyScale.z < 0.0)
      {
        Vector3 localScale = go1.transform.localScale;
        localScale.z *= -1f;
        go1.transform.localScale = localScale;
      }
      if (this.Attach && !string.IsNullOrEmpty(this.BoneName))
      {
        Transform parent = GameUtility.findChildRecursively(go.transform, this.BoneName);
        if (this.BoneName == "CAMERA" && (bool) ((UnityEngine.Object) UnityEngine.Camera.main))
          parent = UnityEngine.Camera.main.transform;
        if ((UnityEngine.Object) parent != (UnityEngine.Object) null)
          go1.transform.SetParent(parent);
      }
      this.OnGenerate(go1);
      if ((double) this.End <= (double) this.Start + 0.100000001490116)
        return;
      DestructTimer destructTimer = GameUtility.RequireComponent<DestructTimer>(go1);
      if (!(bool) ((UnityEngine.Object) destructTimer))
        return;
      destructTimer.Timer = this.End - this.Start;
    }

    protected virtual void OnGenerate(GameObject go)
    {
    }
  }
}
