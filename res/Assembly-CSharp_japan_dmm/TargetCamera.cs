// Decompiled with JetBrains decompiler
// Type: TargetCamera
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
public class TargetCamera : MonoBehaviour
{
  private Vector3 mYawPitchRoll = new Vector3(-45f, 0.0f, 0.0f);
  private Vector3 mTargetPosition;
  private Transform mTransform;
  private float mCameraDistance = 10f;

  public float CameraDistance
  {
    set
    {
      this.mCameraDistance = value;
      this.UpdatePosition();
    }
    get => this.mCameraDistance;
  }

  public Vector3 TargetPosition
  {
    get => this.mTargetPosition;
    set
    {
      this.mTargetPosition = value;
      this.UpdatePosition();
    }
  }

  public float Yaw
  {
    get => this.mYawPitchRoll.y;
    set
    {
      this.mYawPitchRoll.y = value;
      this.UpdatePosition();
    }
  }

  public float Pitch
  {
    get => this.mYawPitchRoll.x;
    set
    {
      this.mYawPitchRoll.x = value;
      this.UpdatePosition();
    }
  }

  public void SetPositionYaw(Vector3 pos, float yaw)
  {
    this.mTargetPosition = pos;
    this.mYawPitchRoll.y = yaw;
    this.UpdatePosition();
  }

  public void SetPositionYawPitch(Vector3 pos, float yaw, float pitch)
  {
    this.mTargetPosition = pos;
    this.mYawPitchRoll.x = pitch;
    this.mYawPitchRoll.y = yaw;
    this.UpdatePosition();
  }

  public void Reset()
  {
    this.mTargetPosition = Vector3.op_Addition(this.mTransform.position, Vector3.op_Multiply(this.mTransform.forward, this.CameraDistance));
    Vector3 forward = this.mTransform.forward;
    forward.y = 0.0f;
    ((Vector3) ref forward).Normalize();
    this.mYawPitchRoll.x = Mathf.Acos(Vector3.Dot(this.mTransform.forward, forward)) * 57.29578f;
    this.mYawPitchRoll.y = (float) (-(double) Mathf.Atan2(this.mTransform.forward.x, this.mTransform.forward.z) * 57.295780181884766);
    if ((double) Vector3.Dot(this.mTransform.forward, Vector3.up) >= 0.0)
      return;
    this.mYawPitchRoll.x = -this.mYawPitchRoll.x;
  }

  public static void CalcCameraPosition(
    out Vector3 position,
    out Quaternion rotation,
    Vector3 target,
    Vector3 rot,
    float distance)
  {
    rotation = TargetCamera.CalcCameraRotation(rot);
    position = Vector3.op_Subtraction(target, Vector3.op_Multiply(Quaternion.op_Multiply(rotation, Vector3.forward), distance));
  }

  public static Quaternion CalcCameraRotation(Vector3 ypr)
  {
    return Quaternion.op_Multiply(Quaternion.LookRotation(Vector3.forward), Quaternion.Euler(-ypr.x, -ypr.y, ypr.z));
  }

  private void UpdatePosition()
  {
    Vector3 position;
    Quaternion rotation;
    TargetCamera.CalcCameraPosition(out position, out rotation, this.mTargetPosition, this.mYawPitchRoll, this.CameraDistance);
    this.mTransform.position = position;
    this.mTransform.rotation = rotation;
  }

  private void Awake()
  {
    this.mTransform = ((Component) this).transform;
    this.Reset();
  }

  public static TargetCamera Get(GameObject go) => GameUtility.RequireComponent<TargetCamera>(go);

  public static TargetCamera Get(Component go)
  {
    return GameUtility.RequireComponent<TargetCamera>(go.gameObject);
  }
}
