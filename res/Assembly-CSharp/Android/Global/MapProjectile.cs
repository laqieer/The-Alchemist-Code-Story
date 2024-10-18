// Decompiled with JetBrains decompiler
// Type: MapProjectile
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using SRPG;
using UnityEngine;

[DisallowMultipleComponent]
public class MapProjectile : MonoBehaviour
{
  private readonly float G = 9.80665f;
  public float TimeScale = 1f;
  public MapProjectile.HitEvent OnHit;
  public TacticsUnitController.ProjectileData mProjectileData;
  public MapProjectile.DistanceChangeEvent OnDistanceUpdate;
  public Transform CameraTransform;
  public Vector3 StartCameraTargetPosition;
  public Vector3 EndCameraTargetPosition;
  public Vector3 GoalPosition;
  public float Speed;
  public float HitDelay;
  public bool IsArrow;
  public Vector2 MidPoint;
  private Transform mTransform;
  private Vector3 mStartPosition;
  private Vector3 mCameraInterpStart;
  private float mElapsedTime;
  private Vector3 mCameraOffset;
  private bool mReachedGoal;
  private Vector3 mPositionHistory;
  public float TopHeight;
  private float mMoveTime;
  private float mStartSpeed;
  private Vector3 mPrevPos;
  private bool mIsStartProc;

  private void InitGravity()
  {
    float num1 = GameUtility.CalcDistance2D(this.GoalPosition - this.mStartPosition);
    this.TopHeight = Mathf.Max(this.TopHeight, Mathf.Max(this.mStartPosition.y + 0.1f, this.GoalPosition.y + 0.1f));
    float num2 = this.TopHeight - this.GoalPosition.y;
    float num3 = this.mStartPosition.y - this.GoalPosition.y;
    float f1 = (float) (2.0 * (double) this.G * ((double) num2 - (double) num3));
    float f2 = 2f * this.G * num2;
    float num4 = (double) f1 <= 0.0 ? 0.0f : Mathf.Sqrt(f1);
    float num5 = (double) f2 <= 0.0 ? 0.0f : Mathf.Sqrt(f2);
    this.mMoveTime = (num4 + num5) / this.G;
    float num6 = Mathf.Sin(Mathf.Atan(this.mMoveTime * num4 / num1));
    this.mStartSpeed = Mathf.Sqrt(Mathf.Pow(num1 / this.mMoveTime, 2f) + (float) (2.0 * (double) this.G * ((double) num2 - (double) num3)));
    this.mStartSpeed *= num6;
  }

  private void Gravity()
  {
    this.mPrevPos = this.transform.position;
    float t = this.mElapsedTime / this.mMoveTime;
    float num = this.mStartSpeed * this.mElapsedTime - 0.5f * this.G * Mathf.Pow(this.mElapsedTime, 2f);
    Vector3 vector3 = this.GoalPosition - this.mStartPosition;
    vector3.y = 0.0f;
    this.transform.position = this.mStartPosition + vector3 * t + Vector3.up * num;
    Vector3 forward = this.transform.position - this.mPrevPos;
    if ((double) forward.sqrMagnitude > 0.0)
      this.transform.rotation = Quaternion.LookRotation(forward);
    if (!((UnityEngine.Object) this.CameraTransform != (UnityEngine.Object) null))
      return;
    this.CameraTransform.position = Vector3.Lerp(this.StartCameraTargetPosition, this.EndCameraTargetPosition, t) + this.mCameraOffset;
  }

  private void Start()
  {
    this.mTransform = this.transform;
    this.mStartPosition = this.mTransform.position;
    this.mCameraInterpStart = this.mTransform.position;
    this.mPositionHistory = this.mStartPosition;
    if ((UnityEngine.Object) this.CameraTransform != (UnityEngine.Object) null)
      this.mCameraOffset = this.CameraTransform.position - this.StartCameraTargetPosition;
    if (this.IsArrow)
      this.InitGravity();
    this.mIsStartProc = true;
    this.Update();
    this.mIsStartProc = false;
  }

  private void Update()
  {
    float deltaTime = Time.deltaTime;
    if (this.IsArrow)
      deltaTime *= this.TimeScale;
    this.mElapsedTime += deltaTime;
    if (!this.mReachedGoal && (double) this.Speed > 0.0)
    {
      if (this.IsArrow)
      {
        if ((double) this.mElapsedTime < (double) this.mMoveTime)
        {
          this.Gravity();
        }
        else
        {
          this.mElapsedTime = this.mMoveTime;
          this.Gravity();
          this.mReachedGoal = true;
          this.mCameraInterpStart = this.mTransform.position;
          this.mElapsedTime = 0.0f;
        }
      }
      else
      {
        Vector3 vector3_1 = this.GoalPosition - this.mTransform.position;
        float magnitude = vector3_1.magnitude;
        float num = this.Speed * Time.deltaTime;
        if ((double) magnitude > 9.99999974737875E-05)
        {
          Vector3 forward = vector3_1 * 1f / magnitude;
          this.mTransform.rotation = Quaternion.LookRotation(forward);
          this.mTransform.position += forward * num;
        }
        if ((double) num >= (double) magnitude)
        {
          this.mTransform.position = this.GoalPosition;
          this.mReachedGoal = true;
          this.mCameraInterpStart = this.mTransform.position;
          this.mElapsedTime = 0.0f;
        }
        this.CalcMovedDistance();
        if ((UnityEngine.Object) this.CameraTransform != (UnityEngine.Object) null)
        {
          Vector3 vector3_2 = this.GoalPosition - this.mStartPosition;
          this.CameraTransform.position = Vector3.Lerp(this.StartCameraTargetPosition, this.EndCameraTargetPosition, Vector3.Dot(this.mTransform.position - this.mStartPosition, vector3_2.normalized) / vector3_2.magnitude) + this.mCameraOffset;
        }
      }
      if (!this.mIsStartProc)
        return;
    }
    else
      this.mReachedGoal = true;
    if (!this.mReachedGoal)
      return;
    if ((double) this.mElapsedTime < (double) this.HitDelay)
    {
      if ((double) this.Speed > 0.0 || !((UnityEngine.Object) this.CameraTransform != (UnityEngine.Object) null))
        return;
      this.CameraTransform.position = Vector3.Lerp(this.mCameraInterpStart, this.EndCameraTargetPosition, Mathf.Sin((float) ((double) this.mElapsedTime / (double) this.HitDelay * 3.14159274101257 * 0.5))) + this.mCameraOffset;
    }
    else
    {
      if ((UnityEngine.Object) this.CameraTransform != (UnityEngine.Object) null)
        this.CameraTransform.position = this.EndCameraTargetPosition + this.mCameraOffset;
      if (this.OnHit != null)
        this.OnHit(this.mProjectileData);
      if ((UnityEngine.Object) this.gameObject.GetComponent<OneShotParticle>() == (UnityEngine.Object) null)
        this.gameObject.AddComponent<OneShotParticle>();
      GameUtility.StopEmitters(this.gameObject);
      UnityEngine.Object.Destroy((UnityEngine.Object) this);
    }
  }

  private void CalcMovedDistance()
  {
    if (this.OnDistanceUpdate == null)
      return;
    Vector3 position = this.transform.position;
    if ((double) (position - this.mPositionHistory).magnitude <= 9.99999974737875E-06)
      return;
    Vector3 lhs = this.GoalPosition - this.mStartPosition;
    lhs.y = 0.0f;
    lhs.Normalize();
    this.OnDistanceUpdate(this.gameObject, Vector3.Dot(lhs, position - this.mStartPosition));
    this.mPositionHistory = position;
  }

  public float CalcDepth(Vector3 position)
  {
    Vector3 lhs = this.GoalPosition - this.mStartPosition;
    lhs.y = 0.0f;
    lhs.Normalize();
    return Vector3.Dot(lhs, position - this.mStartPosition);
  }

  public delegate void HitEvent(TacticsUnitController.ProjectileData pd);

  public delegate void DistanceChangeEvent(GameObject go, float distance);
}
