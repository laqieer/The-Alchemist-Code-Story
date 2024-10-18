// Decompiled with JetBrains decompiler
// Type: StaticProjector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
[RequireComponent(typeof (MeshFilter))]
[RequireComponent(typeof (MeshRenderer))]
[AddComponentMenu("Rendering/Static Projector")]
[ExecuteInEditMode]
public class StaticProjector : MonoBehaviour
{
  [SerializeField]
  [HideInInspector]
  private Mesh mMesh;
  public float FOVAngle = 45f;
  public float FarPlane = 3f;

  private void Awake()
  {
    if (!Application.isPlaying)
      return;
    ((Behaviour) this).enabled = false;
  }
}
