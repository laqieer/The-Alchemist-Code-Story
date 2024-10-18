// Decompiled with JetBrains decompiler
// Type: SRPG.AnimEvents.CameraShake
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG.AnimEvents
{
  public class CameraShake : AnimEvent
  {
    public float FrequencyX = 10f;
    public float FrequencyY = 10f;
    public float AmplitudeX = 1f;
    public float AmplitudeY = 1f;
    [Curve(0.0f, 0.0f, 1f, 1f, true)]
    public AnimationCurve ShakeCurve = AnimationCurve.Linear(0.0f, 1f, 1f, 0.0f);

    public Quaternion CalcOffset(float time, float randX, float randY)
    {
      float num1 = (float) (1.0 - ((double) this.Start >= (double) this.End ? 0.0 : ((double) time - (double) this.Start) / ((double) this.End - (double) this.Start)));
      float num2 = num1;
      if (this.ShakeCurve != null)
        num2 = this.ShakeCurve.Evaluate(1f - num1);
      float num3 = Mathf.Sin((float) (((double) time + (double) randX) * (double) this.FrequencyX * 3.1415927410125732)) * this.AmplitudeX * num2;
      float num4 = Mathf.Sin((float) (((double) time + (double) randY) * (double) this.FrequencyY * 3.1415927410125732)) * this.AmplitudeY * num2;
      return Quaternion.op_Multiply(Quaternion.AngleAxis(num3, Vector3.up), Quaternion.AngleAxis(num4, Vector3.forward));
    }
  }
}
