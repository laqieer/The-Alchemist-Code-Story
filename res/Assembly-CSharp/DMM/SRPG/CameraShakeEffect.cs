// Decompiled with JetBrains decompiler
// Type: SRPG.CameraShakeEffect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class CameraShakeEffect : MonoBehaviour
  {
    private float mSeedX;
    private float mSeedY;
    private float mTime;
    public float Duration = 0.3f;
    public float FrequencyX = 10f;
    public float FrequencyY = 10f;
    public float AmplitudeX = 1f;
    public float AmplitudeY = 1f;

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
      Object.Destroy((Object) this);
    }

    private void OnPreCull()
    {
      float num1 = 1f - Mathf.Clamp01(this.mTime / this.Duration);
      float num2 = Mathf.Sin((float) (((double) Time.time + (double) this.mSeedX) * (double) this.FrequencyX * 3.1415927410125732)) * this.AmplitudeX * num1;
      float num3 = Mathf.Sin((float) (((double) Time.time + (double) this.mSeedY) * (double) this.FrequencyY * 3.1415927410125732)) * this.AmplitudeY * num1;
      ((Component) this).transform.rotation = Quaternion.op_Multiply(((Component) this).transform.rotation, Quaternion.op_Multiply(Quaternion.AngleAxis(num2, Vector3.up), Quaternion.AngleAxis(num3, Vector3.right)));
    }
  }
}
