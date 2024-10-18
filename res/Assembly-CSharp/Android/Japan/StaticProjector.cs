// Decompiled with JetBrains decompiler
// Type: StaticProjector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[RequireComponent(typeof (MeshFilter))]
[RequireComponent(typeof (MeshRenderer))]
[AddComponentMenu("Rendering/Static Projector")]
[ExecuteInEditMode]
public class StaticProjector : MonoBehaviour
{
  public float FOVAngle = 45f;
  public float FarPlane = 3f;
  [SerializeField]
  [HideInInspector]
  private Mesh mMesh;

  private void Awake()
  {
    if (!Application.isPlaying)
      return;
    this.enabled = false;
  }
}
