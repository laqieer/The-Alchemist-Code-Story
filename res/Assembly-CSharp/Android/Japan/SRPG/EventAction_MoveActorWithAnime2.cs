// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_MoveActorWithAnime2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [EventActionInfo("New/アクター/移動2(アニメーション再生付)", "アクターを指定パスに沿って移動させます。", 6702148, 11158596)]
  public class EventAction_MoveActorWithAnime2 : EventAction
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
    [HideInInspector]
    [Tooltip("移動速度")]
    public float RunSpeed = 4f;
    private float BackupRunSpeed = 4f;
    [HideInInspector]
    [Tooltip("移動時間")]
    public float RunTime = 4f;
    [HideInInspector]
    public EventAction_MoveActorWithAnime2.PREFIX_PATH Path = EventAction_MoveActorWithAnime2.PREFIX_PATH.Default;
    private List<float> distanceList = new List<float>();
    private List<float> timeAtPointList = new List<float>();
    [StringIsActorList]
    public string ActorID;
    public float Delay;
    public bool Async;
    public bool GotoRealPosition;
    public bool m_StartActorPos;
    [Tooltip("移動する時にモーションを固定化するか")]
    public bool LockMotion;
    private bool FoldoutLockRotation;
    [HideInInspector]
    public bool LockRotationX;
    [HideInInspector]
    public bool LockRotationY;
    [HideInInspector]
    public bool LockRotationZ;
    [HideInInspector]
    public EventAction_MoveActorWithAnime2.RunTypes RunType;
    [HideInInspector]
    public string m_AnimationName;
    [HideInInspector]
    public bool m_Loop;
    [HideInInspector]
    public EventAction_MoveActorWithAnime2.AnimeType m_AnimeType;
    private string m_AnimationID;
    private const string MOVIE_PATH = "Movies/";
    private const string DEMO_PATH = "Demo/";
    private bool FoldoutAnimation;
    private int mMoveIndex;
    private float mTime;
    private TacticsUnitController mController;
    private bool mReady;
    private bool mMoving;
    private bool mActorCollideGround;
    private Vector3 mActorRotation;

    protected void StartMove()
    {
      if (this.GotoRealPosition && (UnityEngine.Object) this.mController != (UnityEngine.Object) null && this.mController.Unit != null)
      {
        Array.Resize<Vector3>(ref this.m_WayPoints, this.m_WayPoints.Length + 1);
        this.m_WayPoints[this.m_WayPoints.Length - 1] = new Vector3((float) this.mController.Unit.x, 0.0f, (float) this.mController.Unit.y);
      }
      else
        this.GotoRealPosition = false;
      if (this.m_WayPoints.Length == 1)
      {
        this.timeAtPointList.Add(0.0f);
      }
      else
      {
        for (int index = 0; index < this.m_WayPoints.Length - 1; ++index)
          this.distanceList.Add(!this.GroundSnap ? Vector3.Distance(this.m_WayPoints[index], this.m_WayPoints[index + 1]) : GameUtility.CalcDistance2D(this.m_WayPoints[index], this.m_WayPoints[index + 1]));
        float num1 = 0.0f;
        for (int index = 0; index < this.distanceList.Count; ++index)
          num1 += this.distanceList[index];
        float num2 = this.RunType != EventAction_MoveActorWithAnime2.RunTypes.Time ? num1 / this.RunSpeed : this.RunTime;
        float num3 = 0.0f;
        for (int index = 0; index < this.m_WayPoints.Length; ++index)
        {
          if (index > 0)
            num3 += this.distanceList[index - 1];
          this.timeAtPointList.Add(num3 / num1 * num2);
        }
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

    public override bool IsPreloadAssets
    {
      get
      {
        return true;
      }
    }

    [DebuggerHidden]
    public override IEnumerator PreloadAssets()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new EventAction_MoveActorWithAnime2.\u003CPreloadAssets\u003Ec__Iterator0() { \u0024this = this };
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
        this.mReady = false;
        this.mActorCollideGround = this.mController.CollideGround;
        this.mController.CollideGround = this.GroundSnap;
        this.mActorRotation = this.mController.transform.rotation.eulerAngles;
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
        if (this.UpdateMove())
          return;
        if (this.GotoRealPosition)
          this.mController.AutoUpdateRotation = true;
        if (this.m_AnimeType == EventAction_MoveActorWithAnime2.AnimeType.Custom && !string.IsNullOrEmpty(this.m_AnimationName))
        {
          this.mController.RootMotionMode = AnimationPlayer.RootMotionModes.Velocity;
          this.mController.PlayAnimation(this.m_AnimationID, this.m_Loop, 0.1f, 0.0f);
        }
        else if (this.m_AnimeType == EventAction_MoveActorWithAnime2.AnimeType.Idel)
          this.mController.PlayIdle(0.0f);
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

    private bool UpdateMove()
    {
      bool flag = true;
      if (!(bool) ((UnityEngine.Object) this.mController))
        return false;
      Vector3 vector3;
      if ((double) this.mTime >= (double) this.timeAtPointList[this.timeAtPointList.Count - 1])
      {
        vector3 = this.m_WayPoints[this.m_WayPoints.Length - 1];
        if (!this.LockMotion)
          this.mController.StopRunning();
        flag = false;
      }
      else
      {
        for (int mMoveIndex = this.mMoveIndex; mMoveIndex < this.m_WayPoints.Length; ++mMoveIndex)
        {
          if ((double) this.mTime < (double) this.timeAtPointList[mMoveIndex])
          {
            this.mMoveIndex = mMoveIndex;
            break;
          }
        }
        vector3 = (this.m_WayPoints[this.mMoveIndex] - this.m_WayPoints[this.mMoveIndex - 1]) * (float) (((double) this.mTime - (double) this.timeAtPointList[this.mMoveIndex - 1]) / ((double) this.timeAtPointList[this.mMoveIndex] - (double) this.timeAtPointList[this.mMoveIndex - 1])) + this.m_WayPoints[this.mMoveIndex - 1];
      }
      this.mController.transform.position = vector3;
      if (!this.LockRotationX || !this.LockRotationY || !this.LockRotationZ)
      {
        Quaternion quaternion1 = new Quaternion();
        Quaternion quaternion2;
        if (this.m_WayPoints.Length == 1)
        {
          quaternion2 = (double) this.Angle <= 0.0 ? Quaternion.Euler(this.mActorRotation) : Quaternion.Euler(new Vector3(0.0f, this.Angle, 0.0f));
        }
        else
        {
          Vector3 forward1 = this.m_WayPoints[this.mMoveIndex] - this.m_WayPoints[this.mMoveIndex - 1];
          float num1 = this.timeAtPointList[this.mMoveIndex] - this.mTime;
          if ((double) num1 < 0.100000001490116)
          {
            if (this.mMoveIndex < this.m_WayPoints.Length - 1)
            {
              Vector3 forward2 = this.m_WayPoints[this.mMoveIndex + 1] - this.m_WayPoints[this.mMoveIndex];
              float t = (float) ((1.0 - (double) num1 / 0.100000001490116) * 0.5);
              quaternion2 = Quaternion.Slerp(Quaternion.LookRotation(forward1), Quaternion.LookRotation(forward2), t);
            }
            else if ((double) this.Angle >= 0.0)
            {
              float t = (float) (1.0 - (double) num1 / 0.100000001490116);
              quaternion2 = Quaternion.Slerp(Quaternion.LookRotation(forward1), Quaternion.Euler(new Vector3(0.0f, this.Angle, 0.0f)), t);
            }
            else
            {
              Vector3 forward2 = forward1;
              forward2.y = 0.0f;
              float t = (float) (1.0 - (double) num1 / 0.100000001490116);
              quaternion2 = Quaternion.Slerp(Quaternion.LookRotation(forward1), Quaternion.LookRotation(forward2), t);
            }
          }
          else
          {
            float num2 = this.mTime - this.timeAtPointList[this.mMoveIndex - 1];
            if ((double) num2 < 0.100000001490116)
            {
              if (this.mMoveIndex > 1)
              {
                Vector3 forward2 = this.m_WayPoints[this.mMoveIndex - 1] - this.m_WayPoints[this.mMoveIndex - 2];
                float t = (float) (0.5 + (double) num2 / 0.100000001490116 * 0.5);
                quaternion2 = Quaternion.Slerp(Quaternion.LookRotation(forward2), Quaternion.LookRotation(forward1), t);
              }
              else if (this.m_StartActorPos)
              {
                float t = num2 / 0.1f;
                quaternion2 = Quaternion.Slerp(Quaternion.Euler(this.mActorRotation), Quaternion.LookRotation(forward1), t);
              }
              else
                quaternion2 = Quaternion.LookRotation(forward1);
            }
            else
              quaternion2 = Quaternion.LookRotation(forward1);
          }
        }
        if (this.LockRotationX || this.LockRotationY || this.LockRotationZ)
        {
          Vector3 eulerAngles = quaternion2.eulerAngles;
          if (this.LockRotationX)
            eulerAngles.x = this.mActorRotation.x;
          if (this.LockRotationY)
            eulerAngles.y = this.mActorRotation.y;
          if (this.LockRotationZ)
            eulerAngles.z = this.mActorRotation.z;
          quaternion2 = Quaternion.Euler(eulerAngles);
        }
        this.mController.transform.rotation = quaternion2;
      }
      this.mTime += Time.deltaTime;
      return flag;
    }

    public enum RunTypes
    {
      Time,
      Speed,
    }

    public enum AnimeType
    {
      Custom,
      Idel,
    }

    public enum PREFIX_PATH
    {
      Demo,
      Movie,
      Default,
    }
  }
}
