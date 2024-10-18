﻿// Decompiled with JetBrains decompiler
// Type: SRPG.CameraShakeEffect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class CameraShakeEffect : MonoBehaviour
  {
    public float Duration = 0.3f;
    public float FrequencyX = 10f;
    public float FrequencyY = 10f;
    public float AmplitudeX = 1f;
    public float AmplitudeY = 1f;
    private float mSeedX;
    private float mSeedY;
    private float mTime;

    private void Awake()
    {
      this.mSeedX = Random.value;
      this.mSeedY = Random.value;
    }

    private void Update()
    {
      this.mTime += Time.deltaTime;
      if ((double) this.mTime < (double) this.Duration)
        return;
      UnityEngine.Object.Destroy((UnityEngine.Object) this);
    }

    private void OnPreCull()
    {
      float num = 1f - Mathf.Clamp01(this.mTime / this.Duration);
      this.transform.rotation = this.transform.rotation * (Quaternion.AngleAxis(Mathf.Sin((float) (((double) Time.time + (double) this.mSeedX) * (double) this.FrequencyX * 3.14159274101257)) * this.AmplitudeX * num, Vector3.up) * Quaternion.AngleAxis(Mathf.Sin((float) (((double) Time.time + (double) this.mSeedY) * (double) this.FrequencyY * 3.14159274101257)) * this.AmplitudeY * num, Vector3.right));
    }
  }
}
