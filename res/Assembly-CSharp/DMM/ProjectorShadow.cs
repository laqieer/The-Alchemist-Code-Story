// Decompiled with JetBrains decompiler
// Type: ProjectorShadow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
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
    Projector component = ((Component) this).GetComponent<Projector>();
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    Material material = component.material;
    material.SetFloat("_offsetFactor", factor);
    material.SetFloat("_offsetUnits", unit);
  }
}
