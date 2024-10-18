// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_MoveActor2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [EventActionInfo("New/アクター/移動", "アクターを指定パスに沿って移動させます。", 6702148, 11158596)]
  public class EventAction_MoveActor2 : EventAction
  {
    [StringIsActorList]
    public string ActorID;
    [SerializeField]
    [HideInInspector]
    private IntVector2[] mPoints = new IntVector2[1];
    [SerializeField]
    public Vector3[] m_WayPoints = new Vector3[1];
    public float Delay;
    public bool Async;
    public bool GotoRealPosition;
    public bool m_StartActorPos;
    protected TacticsUnitController mController;
    [HideInInspector]
    public float Angle = -1f;
    [Tooltip("マス目にスナップするか？")]
    public bool MoveSnap = true;
    [Tooltip("地面にスナップするか？")]
    public bool GroundSnap = true;
    [Tooltip("移動速度")]
    public float RunSpeed = 4f;
    protected float BackupRunSpeed = 4f;
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
      if (this.GotoRealPosition && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mController, (UnityEngine.Object) null) && this.mController.Unit != null)
      {
        Array.Resize<Vector3>(ref this.m_WayPoints, this.m_WayPoints.Length + 1);
        this.m_WayPoints[this.m_WayPoints.Length - 1] = new Vector3((float) this.mController.Unit.x, 0.0f, (float) this.mController.Unit.y);
      }
      else
        this.GotoRealPosition = false;
      Vector3[] route = new Vector3[this.m_WayPoints.Length];
      for (int index = 0; index < this.m_WayPoints.Length; ++index)
        route[index] = this.m_WayPoints[index];
      if (this.m_StartActorPos && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mController, (UnityEngine.Object) null))
      {
        Transform transform = ((Component) this.mController).transform;
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
      TacticsUnitController controller = TacticsUnitController.FindByUniqueName(this.ActorID);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) controller, (UnityEngine.Object) null))
        controller = TacticsUnitController.FindByUnitID(this.ActorID);
      return controller;
    }

    public override void OnActivate()
    {
      this.mController = this.GetController();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mController, (UnityEngine.Object) null) || this.mPoints.Length == 0)
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
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mController, (UnityEngine.Object) null) && this.mController.IsLoading)
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
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mController))
        return false;
      if (this.m_WayPoints.Length > this.mMoveIndex)
      {
        Vector3 position = ((Component) this.mController).transform.position;
        Vector3 vector3_1 = Vector3.op_Subtraction(this.m_WayPoints[this.mMoveIndex], position);
        if ((double) ((Vector3) ref vector3_1).sqrMagnitude < 9.9999997473787516E-05)
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
          ((Vector3) ref vector3_1).Normalize();
          Vector3 a = Vector3.op_Addition(position, Vector3.op_Multiply(Vector3.op_Multiply(vector3_1, this.RunSpeed), Time.deltaTime));
          ((Component) this.mController).transform.position = a;
          if (!this.LockRotation)
          {
            float num1 = GameUtility.CalcDistance2D(a, this.m_WayPoints[this.mMoveIndex]);
            if ((double) num1 < 0.5 && this.mMoveIndex < this.m_WayPoints.Length - 1)
            {
              Vector3 vector3_2 = Vector3.op_Subtraction(this.m_WayPoints[this.mMoveIndex + 1], this.m_WayPoints[this.mMoveIndex]);
              vector3_2.y = 0.0f;
              ((Vector3) ref vector3_2).Normalize();
              float num2 = (float) ((1.0 - (double) num1 / 0.5) * 0.5);
              ((Component) this.mController).transform.rotation = Quaternion.Slerp(Quaternion.LookRotation(vector3_1), Quaternion.LookRotation(vector3_2), num2);
            }
            else if (this.mMoveIndex > 1)
            {
              float num3 = GameUtility.CalcDistance2D(a, this.m_WayPoints[this.mMoveIndex - 1]);
              if ((double) num3 < 0.5)
              {
                Vector3 vector3_3 = Vector3.op_Subtraction(this.m_WayPoints[this.mMoveIndex - 1], this.m_WayPoints[this.mMoveIndex - 2]);
                vector3_3.y = 0.0f;
                ((Vector3) ref vector3_3).Normalize();
                float num4 = (float) (0.5 + (double) num3 / 0.5 * 0.5);
                ((Component) this.mController).transform.rotation = Quaternion.Slerp(Quaternion.LookRotation(vector3_3), Quaternion.LookRotation(vector3_1), num4);
              }
              else
                ((Component) this.mController).transform.rotation = Quaternion.LookRotation(vector3_1);
            }
            else
              ((Component) this.mController).transform.rotation = Quaternion.LookRotation(vector3_1);
          }
        }
      }
      return flag;
    }
  }
}
