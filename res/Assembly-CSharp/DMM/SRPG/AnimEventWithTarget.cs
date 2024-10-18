// Decompiled with JetBrains decompiler
// Type: SRPG.AnimEventWithTarget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using SRPG.AnimEvents;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public abstract class AnimEventWithTarget : AnimEvent
  {
    protected const string BONE_NAME_CAMERA = "CAMERA";
    public string BoneName = string.Empty;
    public Vector3 Offset = Vector3.zero;
    public Vector3 Rotation = Vector3.zero;
    public bool LocalOffset = true;
    public bool LocalRotation = true;

    public void CalcPosition(
      GameObject go,
      GameObject prefab,
      out Vector3 spawnPos,
      out Quaternion spawnRot)
    {
      this.CalcPosition(go, prefab.transform.localPosition, prefab.transform.localRotation, out spawnPos, out spawnRot);
    }

    public void CalcPosition(
      GameObject go,
      Vector3 deltaOffset,
      Quaternion deltaRotation,
      out Vector3 spawnPos,
      out Quaternion spawnRot)
    {
      spawnPos = Vector3.op_Addition(this.Offset, deltaOffset);
      spawnRot = Quaternion.op_Multiply(Quaternion.Euler(this.Rotation.x, this.Rotation.y, this.Rotation.z), deltaRotation);
      Transform transform = string.IsNullOrEmpty(this.BoneName) ? go.transform : GameUtility.findChildRecursively(go.transform, this.BoneName);
      if (this is ParticleGenerator && this.BoneName == "CAMERA" && Object.op_Implicit((Object) Camera.main))
        transform = ((Component) Camera.main).transform;
      if (!Object.op_Inequality((Object) transform, (Object) null))
        return;
      spawnPos = !this.LocalOffset ? Vector3.op_Addition(transform.TransformPoint(Vector3.zero), spawnPos) : transform.TransformPoint(spawnPos);
      if (!this.LocalRotation)
        return;
      spawnRot = Quaternion.op_Multiply(transform.rotation, spawnRot);
    }
  }
}
