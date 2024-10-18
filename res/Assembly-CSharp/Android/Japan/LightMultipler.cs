// Decompiled with JetBrains decompiler
// Type: LightMultipler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[ExecuteInEditMode]
[DisallowMultipleComponent]
[AddComponentMenu("Rendering/Light Multipler")]
public class LightMultipler : MonoBehaviour
{
  public float Radius = 1f;
  public float Exponent = 2f;
  public Vector3 Multipler = Vector3.zero;
  protected Vector3 mPositionCache;

  public virtual void Cache()
  {
    this.mPositionCache = this.transform.position;
  }

  protected Vector3 CalcMultipler(float distance)
  {
    return Vector3.Lerp(Vector3.one, this.Multipler, Mathf.Pow(Mathf.Clamp01((float) (1.0 - (double) distance / (double) this.Radius)), this.Exponent));
  }

  public virtual Vector3 CalcMultiplerAtPoint(Vector3 position, Vector3 normal)
  {
    if ((double) this.Radius <= 0.0)
      return Vector3.one;
    return this.CalcMultipler((position - this.mPositionCache).magnitude);
  }

  private void Awake()
  {
    this.tag = "EditorOnly";
    this.enabled = false;
  }
}
