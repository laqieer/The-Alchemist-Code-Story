// Decompiled with JetBrains decompiler
// Type: ProjectorShadow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[AddComponentMenu("Scripts/ProjectorShadow")]
public class ProjectorShadow : MonoBehaviour
{
  private void Start()
  {
  }

  public void Initialize()
  {
  }

  public void Release()
  {
  }

  public void Update()
  {
  }

  private void SetZOffset(float factor, float unit)
  {
    Projector component = this.GetComponent<Projector>();
    if (!((Object) component != (Object) null))
      return;
    Material material = component.material;
    material.SetFloat("_offsetFactor", factor);
    material.SetFloat("_offsetUnits", unit);
  }
}
