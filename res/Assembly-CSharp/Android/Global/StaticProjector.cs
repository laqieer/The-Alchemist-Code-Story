// Decompiled with JetBrains decompiler
// Type: StaticProjector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
