// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_MoveActor2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

namespace SRPG
{
  [EventActionInfo("New/アクター/移動", "アクターを指定パスに沿って移動させます。", 6702148, 11158596)]
  public class EventAction_MoveActor2 : EventAction
  {
    [SerializeField]
    [HideInInspector]
    private IntVector2[] mPoints = new IntVector2[1];
    [SerializeField]
    public Vector3[] m_WayPoints = new Vector3[1];
    [HideInInspector]
    public float Angle = -1f;
    [Tooltip("マス目にスナップするか？")]
    public bool MoveSnap = true;
    [Tooltip("地面にスナップするか？")]
    public bool GroundSnap = true;
    [Tooltip("移動速度")]
    public float RunSpeed = 4f;
    protected float BackupRunSpeed = 4f;
    [StringIsActorList]
    public string ActorID;
    public float Delay;
    public bool Async;
    public bool GotoRealPosition;
    public bool m_StartActorPos;
    protected TacticsUnitController mController;
    [Tooltip("移動する時に向きを固定化するか")]
    public bool LockRotation;
    [Tooltip("移動する時にモーションを固定化するか")]
    public bool LockMotion;
    private int mMoveIndex;
    protected bool mMoving;
    protected bool mFinishMove;
    protected bool mActorCollideGround;
    protected bool mReady;

    protected void StartMove()
    {
      if (this.GotoRealPosition && (UnityEngine.Object) this.mController != (UnityEngine.Object) null && this.mController.Unit != null)
      {
        Array.Resize<Vector3>(ref this.m_WayPoints, this.m_WayPoints.Length + 1);
        this.m_WayPoints[this.m_WayPoints.Length - 1] = new Vector3((float) this.mController.Unit.x, 0.0f, (float) this.mController.Unit.y);
      }
      else
        this.GotoRealPosition = false;
      Vector3[] route = new Vector3[this.m_WayPoints.Length];
      for (int index = 0; index < this.m_WayPoints.Length; ++index)
        route[index] = this.m_WayPoints[index];
      if (this.m_StartActorPos && (UnityEngine.Object) this.mController != (UnityEngine.Object) null)
      {
        Transform transform = this.mController.transform;
        route[0] = transform.position;
      }
      this.mMoveIndex = 0;
      if (!this.LockRotation && !this.LockMotion && this.GroundSnap)
      {
        double num = (double) this.mController.StartMove(route, this.Angle);
      }
      else
      {
        if (this.LockMotion)
          return;
        this.mController.StartRunning();
      }
    }

    private TacticsUnitController GetController()
    {
      TacticsUnitController tacticsUnitController = TacticsUnitController.FindByUniqueName(this.ActorID);
      if ((UnityEngine.Object) tacticsUnitController == (UnityEngine.Object) null)
        tacticsUnitController = TacticsUnitController.FindByUnitID(this.ActorID);
      return tacticsUnitController;
    }

    public override void OnActivate()
    {
      this.mController = this.GetController();
      if ((UnityEngine.Object) this.mController == (UnityEngine.Object) null || this.mPoints.Length == 0)
      {
        this.ActivateNext();
      }
      else
      {
        this.mController.SetRunningSpeed(this.RunSpeed);
        this.mReady = false;
        this.mActorCollideGround = this.mController.CollideGround;
        this.mController.CollideGround = this.GroundSnap;
      }
    }

    public override void Update()
    {
      if (!this.mReady)
      {
        if ((UnityEngine.Object) this.mController != (UnityEngine.Object) null && this.mController.IsLoading)
          return;
        if (this.Async)
          this.ActivateNext(true);
        this.mReady = true;
      }
      if (!this.mMoving)
      {
        if (this.mController.IsLoading)
          return;
        if ((double) this.Delay > 0.0)
        {
          this.Delay -= Time.deltaTime;
        }
        else
        {
          this.StartMove();
          this.mMoving = true;
        }
      }
      else
      {
        if (this.LockRotation || this.LockMotion || !this.GroundSnap)
        {
          if (this.UpdateMove())
            return;
        }
        else if (this.mController.isMoving)
          return;
        if (this.GotoRealPosition)
          this.mController.AutoUpdateRotation = true;
        if (!this.Async)
        {
          this.ActivateNext();
          this.mController.SetRunningSpeed(this.BackupRunSpeed);
          this.mController.CollideGround = this.mActorCollideGround;
        }
        else
        {
          this.enabled = false;
          this.mController.SetRunningSpeed(this.BackupRunSpeed);
          this.mController.CollideGround = this.mActorCollideGround;
        }
      }
    }

    protected bool UpdateMove()
    {
      bool flag = true;
      if (!(bool) ((UnityEngine.Object) this.mController))
        return false;
      if (this.m_WayPoints.Length > this.mMoveIndex)
      {
        Vector3 position = this.mController.transform.position;
        Vector3 forward1 = this.m_WayPoints[this.mMoveIndex] - position;
        if ((double) forward1.sqrMagnitude < 9.99999974737875E-05)
        {
          if (++this.mMoveIndex >= this.m_WayPoints.Length)
          {
            if (!this.LockMotion)
              this.mController.StopRunning();
            flag = false;
          }
        }
        else
        {
          forward1.Normalize();
          Vector3 a = position + forward1 * this.RunSpeed * Time.deltaTime;
          this.mController.transform.position = a;
          if (!this.LockRotation)
          {
            float num1 = GameUtility.CalcDistance2D(a, this.m_WayPoints[this.mMoveIndex]);
            if ((double) num1 < 0.5 && this.mMoveIndex < this.m_WayPoints.Length - 1)
            {
              Vector3 forward2 = this.m_WayPoints[this.mMoveIndex + 1] - this.m_WayPoints[this.mMoveIndex];
              forward2.y = 0.0f;
              forward2.Normalize();
              float t = (float) ((1.0 - (double) num1 / 0.5) * 0.5);
              this.mController.transform.rotation = Quaternion.Slerp(Quaternion.LookRotation(forward1), Quaternion.LookRotation(forward2), t);
            }
            else if (this.mMoveIndex > 1)
            {
              float num2 = GameUtility.CalcDistance2D(a, this.m_WayPoints[this.mMoveIndex - 1]);
              if ((double) num2 < 0.5)
              {
                Vector3 forward2 = this.m_WayPoints[this.mMoveIndex - 1] - this.m_WayPoints[this.mMoveIndex - 2];
                forward2.y = 0.0f;
                forward2.Normalize();
                float t = (float) (0.5 + (double) num2 / 0.5 * 0.5);
                this.mController.transform.rotation = Quaternion.Slerp(Quaternion.LookRotation(forward2), Quaternion.LookRotation(forward1), t);
              }
              else
                this.mController.transform.rotation = Quaternion.LookRotation(forward1);
            }
            else
              this.mController.transform.rotation = Quaternion.LookRotation(forward1);
          }
        }
      }
      return flag;
    }
  }
}
