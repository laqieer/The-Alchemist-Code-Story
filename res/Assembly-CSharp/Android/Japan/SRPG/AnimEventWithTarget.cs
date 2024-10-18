// Decompiled with JetBrains decompiler
// Type: SRPG.AnimEventWithTarget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using SRPG.AnimEvents;
using UnityEngine;

namespace SRPG
{
  public abstract class AnimEventWithTarget : AnimEvent
  {
    public string BoneName = string.Empty;
    public Vector3 Offset = Vector3.zero;
    public Vector3 Rotation = Vector3.zero;
    public bool LocalOffset = true;
    public bool LocalRotation = true;
    protected const string BONE_NAME_CAMERA = "CAMERA";

    public void CalcPosition(GameObject go, GameObject prefab, out Vector3 spawnPos, out Quaternion spawnRot)
    {
      this.CalcPosition(go, prefab.transform.localPosition, prefab.transform.localRotation, out spawnPos, out spawnRot);
    }

    public void CalcPosition(GameObject go, Vector3 deltaOffset, Quaternion deltaRotation, out Vector3 spawnPos, out Quaternion spawnRot)
    {
      spawnPos = this.Offset + deltaOffset;
      spawnRot = Quaternion.Euler(this.Rotation.x, this.Rotation.y, this.Rotation.z) * deltaRotation;
      Transform transform = string.IsNullOrEmpty(this.BoneName) ? go.transform : GameUtility.findChildRecursively(go.transform, this.BoneName);
      if (this is ParticleGenerator && this.BoneName == "CAMERA" && (bool) ((UnityEngine.Object) UnityEngine.Camera.main))
        transform = UnityEngine.Camera.main.transform;
      if (!((UnityEngine.Object) transform != (UnityEngine.Object) null))
        return;
      spawnPos = !this.LocalOffset ? transform.TransformPoint(Vector3.zero) + spawnPos : transform.TransformPoint(spawnPos);
      if (!this.LocalRotation)
        return;
      spawnRot = transform.rotation * spawnRot;
    }
  }
}
