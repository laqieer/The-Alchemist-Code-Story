// Decompiled with JetBrains decompiler
// Type: LightMultipler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
[ExecuteInEditMode]
[DisallowMultipleComponent]
[AddComponentMenu("Rendering/Light Multipler")]
public class LightMultipler : MonoBehaviour
{
  public float Radius = 1f;
  public float Exponent = 2f;
  public Vector3 Multipler = Vector3.zero;
  protected Vector3 mPositionCache;

  public virtual void Cache() => this.mPositionCache = ((Component) this).transform.position;

  protected Vector3 CalcMultipler(float distance)
  {
    return Vector3.Lerp(Vector3.one, this.Multipler, Mathf.Pow(Mathf.Clamp01((float) (1.0 - (double) distance / (double) this.Radius)), this.Exponent));
  }

  public virtual Vector3 CalcMultiplerAtPoint(Vector3 position, Vector3 normal)
  {
    if ((double) this.Radius <= 0.0)
      return Vector3.one;
    Vector3 vector3 = Vector3.op_Subtraction(position, this.mPositionCache);
    return this.CalcMultipler(((Vector3) ref vector3).magnitude);
  }

  private void Awake()
  {
    ((Component) this).tag = "EditorOnly";
    ((Behaviour) this).enabled = false;
  }
}
