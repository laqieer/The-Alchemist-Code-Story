// Decompiled with JetBrains decompiler
// Type: MapProjectile
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using SRPG;
using UnityEngine;

#nullable disable
[DisallowMultipleComponent]
public class MapProjectile : MonoBehaviour
{
  public MapProjectile.HitEvent OnHit;
  public TacticsUnitController.ProjectileData mProjectileData;
  public MapProjectile.DistanceChangeEvent OnDistanceUpdate;
  public Transform CameraTransform;
  public Vector3 StartCameraTargetPosition;
  public Vector3 EndCameraTargetPosition;
  public Vector3 GoalPosition;
  public float Speed;
  public float TeleportTime;
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
  private readonly float G = 9.80665f;
  public float TimeScale = 1f;
  public float TopHeight;
  private float mMoveTime;
  private float mStartSpeed;
  private Vector3 mPrevPos;
  private bool mIsStartProc;

  private void InitGravity()
  {
    float num1 = GameUtility.CalcDistance2D(Vector3.op_Subtraction(this.GoalPosition, this.mStartPosition));
    this.TopHeight = Mathf.Max(this.TopHeight, Mathf.Max(this.mStartPosition.y + 0.1f, this.GoalPosition.y + 0.1f));
    float num2 = this.TopHeight - this.GoalPosition.y;
    float num3 = this.mStartPosition.y - this.GoalPosition.y;
    float num4 = (float) (2.0 * (double) this.G * ((double) num2 - (double) num3));
    float num5 = 2f * this.G * num2;
    float num6 = (double) num4 <= 0.0 ? 0.0f : Mathf.Sqrt(num4);
    float num7 = (double) num5 <= 0.0 ? 0.0f : Mathf.Sqrt(num5);
    this.mMoveTime = (num6 + num7) / this.G;
    float num8 = Mathf.Sin(Mathf.Atan(this.mMoveTime * num6 / num1));
    this.mStartSpeed = Mathf.Sqrt(Mathf.Pow(num1 / this.mMoveTime, 2f) + (float) (2.0 * (double) this.G * ((double) num2 - (double) num3)));
    this.mStartSpeed *= num8;
  }

  private void Gravity()
  {
    this.mPrevPos = ((Component) this).transform.position;
    float num1 = this.mElapsedTime / this.mMoveTime;
    float num2 = this.mStartSpeed * this.mElapsedTime - 0.5f * this.G * Mathf.Pow(this.mElapsedTime, 2f);
    Vector3 vector3_1 = Vector3.op_Subtraction(this.GoalPosition, this.mStartPosition);
    vector3_1.y = 0.0f;
    ((Component) this).transform.position = Vector3.op_Addition(Vector3.op_Addition(this.mStartPosition, Vector3.op_Multiply(vector3_1, num1)), Vector3.op_Multiply(Vector3.up, num2));
    Vector3 vector3_2 = Vector3.op_Subtraction(((Component) this).transform.position, this.mPrevPos);
    if ((double) ((Vector3) ref vector3_2).sqrMagnitude > 0.0)
      ((Component) this).transform.rotation = Quaternion.LookRotation(vector3_2);
    if (!Object.op_Inequality((Object) this.CameraTransform, (Object) null))
      return;
    this.CameraTransform.position = Vector3.op_Addition(Vector3.Lerp(this.StartCameraTargetPosition, this.EndCameraTargetPosition, num1), this.mCameraOffset);
  }

  private void Start()
  {
    this.mTransform = ((Component) this).transform;
    this.mStartPosition = this.mTransform.position;
    this.mCameraInterpStart = this.mTransform.position;
    this.mPositionHistory = this.mStartPosition;
    if (Object.op_Inequality((Object) this.CameraTransform, (Object) null))
      this.mCameraOffset = Vector3.op_Subtraction(this.CameraTransform.position, this.StartCameraTargetPosition);
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
        Vector3 vector3_1 = Vector3.op_Subtraction(this.GoalPosition, this.mTransform.position);
        float magnitude = ((Vector3) ref vector3_1).magnitude;
        float num = this.Speed * Time.deltaTime;
        if ((double) magnitude > 9.9999997473787516E-05)
        {
          Vector3 vector3_2 = Vector3.op_Division(Vector3.op_Multiply(vector3_1, 1f), magnitude);
          this.mTransform.rotation = Quaternion.LookRotation(vector3_2);
          Transform mTransform = this.mTransform;
          mTransform.position = Vector3.op_Addition(mTransform.position, Vector3.op_Multiply(vector3_2, num));
        }
        if ((double) this.TeleportTime > 0.0)
        {
          this.TeleportTime -= Time.deltaTime;
          if ((double) this.TeleportTime <= 0.0)
          {
            this.TeleportTime = 0.0f;
            num = magnitude;
          }
        }
        if ((double) num >= (double) magnitude)
        {
          this.mTransform.position = this.GoalPosition;
          this.mReachedGoal = true;
          this.mCameraInterpStart = this.mTransform.position;
          this.mElapsedTime = 0.0f;
        }
        this.CalcMovedDistance();
        if (Object.op_Inequality((Object) this.CameraTransform, (Object) null))
        {
          Vector3 vector3_3 = Vector3.op_Subtraction(this.GoalPosition, this.mStartPosition);
          this.CameraTransform.position = Vector3.op_Addition(Vector3.Lerp(this.StartCameraTargetPosition, this.EndCameraTargetPosition, Vector3.Dot(Vector3.op_Subtraction(this.mTransform.position, this.mStartPosition), ((Vector3) ref vector3_3).normalized) / ((Vector3) ref vector3_3).magnitude), this.mCameraOffset);
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
      if ((double) this.Speed > 0.0 || !Object.op_Inequality((Object) this.CameraTransform, (Object) null))
        return;
      this.CameraTransform.position = Vector3.op_Addition(Vector3.Lerp(this.mCameraInterpStart, this.EndCameraTargetPosition, Mathf.Sin((float) ((double) this.mElapsedTime / (double) this.HitDelay * 3.1415927410125732 * 0.5))), this.mCameraOffset);
    }
    else
    {
      if (Object.op_Inequality((Object) this.CameraTransform, (Object) null))
        this.CameraTransform.position = Vector3.op_Addition(this.EndCameraTargetPosition, this.mCameraOffset);
      if (this.OnHit != null)
        this.OnHit(this.mProjectileData);
      if (Object.op_Equality((Object) ((Component) this).gameObject.GetComponent<OneShotParticle>(), (Object) null))
        ((Component) this).gameObject.AddComponent<OneShotParticle>();
      GameUtility.StopEmitters(((Component) this).gameObject);
      Object.Destroy((Object) this);
    }
  }

  private void CalcMovedDistance()
  {
    if (this.OnDistanceUpdate == null)
      return;
    Vector3 position = ((Component) this).transform.position;
    Vector3 vector3_1 = Vector3.op_Subtraction(position, this.mPositionHistory);
    if ((double) ((Vector3) ref vector3_1).magnitude <= 9.9999997473787516E-06)
      return;
    Vector3 vector3_2 = Vector3.op_Subtraction(this.GoalPosition, this.mStartPosition);
    vector3_2.y = 0.0f;
    ((Vector3) ref vector3_2).Normalize();
    this.OnDistanceUpdate(((Component) this).gameObject, Vector3.Dot(vector3_2, Vector3.op_Subtraction(position, this.mStartPosition)));
    this.mPositionHistory = position;
  }

  public float CalcDepth(Vector3 position)
  {
    Vector3 vector3 = Vector3.op_Subtraction(this.GoalPosition, this.mStartPosition);
    vector3.y = 0.0f;
    ((Vector3) ref vector3).Normalize();
    return Vector3.Dot(vector3, Vector3.op_Subtraction(position, this.mStartPosition));
  }

  public delegate void HitEvent(TacticsUnitController.ProjectileData pd);

  public delegate void DistanceChangeEvent(GameObject go, float distance);
}
