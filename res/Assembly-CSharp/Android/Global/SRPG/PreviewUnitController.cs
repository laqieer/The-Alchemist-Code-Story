// Decompiled with JetBrains decompiler
// Type: SRPG.PreviewUnitController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using SRPG.AnimEvents;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class PreviewUnitController : UnitController
  {
    private static readonly string SLOT0 = "0";
    public string UnitID = "UN010000";
    public string JobID = "ninja";
    private List<string> mCameraAnimationNames = new List<string>();
    public string mCurrentAnimation = string.Empty;
    public List<string> mAnimationFiles = new List<string>();
    private float mSpeed = 1f;
    private Vector3 mStartPosition;
    private Transform mCameraPos;
    private Transform mEnemyPos;
    private int mCameraID;
    private Quaternion mCameraShakeOffset;
    private float mCameraShakeSeedX;
    private float mCameraShakeSeedY;
    private AnimationClip mCameraAnimation;
    private bool mUseCamera;
    private Vector2 mAnimListScrollPos;
    private Vector2 mCameraAnimListScrollPos;
    private bool mLoadingAnimation;
    private bool mLoopAnimation;
    private bool mMirror;
    private bool mAnimationLoaded;

    private void Awake()
    {
      MonoSingleton<GameManager>.Instance.Deserialize(JSONParser.parseJSONObject<JSON_MasterParam>(Resources.Load<TextAsset>("Data/MasterParam").text));
      UnitData unitData = new UnitData();
      unitData.Setup(this.UnitID, 0, 0, 0, this.JobID, 0, EElement.None);
      this.SetupUnit(unitData, -1);
      this.mStartPosition = this.transform.position;
    }

    protected override void PostSetup()
    {
    }

    protected override void OnEventStart(AnimEvent e, float weight)
    {
      if (e is ToggleCamera)
      {
        this.mCameraID = (e as ToggleCamera).CameraID;
        if (this.mCameraID >= 0 && this.mCameraID <= 1)
          return;
        Debug.LogError((object) ("Invalid CameraID: " + (object) this.mCameraID));
      }
      else
      {
        if (!(e is CameraShake))
          return;
        this.mCameraShakeSeedX = Random.value;
        this.mCameraShakeSeedY = Random.value;
      }
    }

    protected override void OnEvent(AnimEvent e, float time, float weight)
    {
      if (!(e is CameraShake))
        return;
      this.mCameraShakeOffset = (e as CameraShake).CalcOffset(time, this.mCameraShakeSeedX, this.mCameraShakeSeedY);
    }

    private new void Update()
    {
      this.mCameraShakeOffset = Quaternion.identity;
      base.Update();
      if (!this.mLoadingAnimation || this.IsLoading)
        return;
      this.mAnimationLoaded = true;
      this.mLoadingAnimation = false;
      this.transform.position = this.mStartPosition;
      this.mCameraID = 0;
      this.PlayAnimation(PreviewUnitController.SLOT0, this.mLoopAnimation);
      AnimDef animation = this.GetAnimation(PreviewUnitController.SLOT0);
      if ((UnityEngine.Object) this.mCameraAnimation != (UnityEngine.Object) null)
      {
        this.mCameraPos.GetComponent<Animation>().AddClip(this.mCameraAnimation, PreviewUnitController.SLOT0);
        this.mCameraPos.GetComponent<Animation>().Play(PreviewUnitController.SLOT0);
        this.mCameraPos.GetComponent<Animation>()[PreviewUnitController.SLOT0].speed = (float) (1.0 / (double) this.mCameraAnimation.length * 1.0) / animation.Length;
      }
      else
      {
        this.mCameraPos.GetComponent<Animation>().AddClip(animation.animation, PreviewUnitController.SLOT0);
        this.mCameraPos.GetComponent<Animation>().Play(PreviewUnitController.SLOT0);
        this.mCameraPos.GetComponent<Animation>()[PreviewUnitController.SLOT0].speed = 1f;
      }
      this.mEnemyPos.GetComponent<Animation>().AddClip(animation.animation, PreviewUnitController.SLOT0);
      this.mEnemyPos.GetComponent<Animation>().Play(PreviewUnitController.SLOT0);
    }

    private new void LateUpdate()
    {
      base.LateUpdate();
      if (!this.mUseCamera)
        return;
      Transform child = this.mCameraPos.FindChild(this.mCameraID != 0 ? "Camera002" : "Camera001");
      Transform transform = UnityEngine.Camera.main.transform;
      Vector3 position = child.position;
      Quaternion rotation = child.rotation;
      if (this.mMirror)
      {
        rotation.z = -rotation.z;
        rotation.y = -rotation.y;
      }
      rotation *= GameUtility.Yaw180;
      transform.position = position;
      transform.rotation = this.mCameraShakeOffset * rotation;
      Debug.DrawLine(position, position + transform.forward * 5f, Color.yellow);
      PreviewUnitController.DrawAxis(position);
      Debug.DrawLine(child.position, child.position - child.forward * 5f, Color.magenta);
      PreviewUnitController.DrawAxis(child.position);
    }

    private static void DrawAxis(Vector3 center)
    {
      Debug.DrawLine(center, center + Vector3.right, Color.red);
      Debug.DrawLine(center, center + Vector3.up, Color.green);
      Debug.DrawLine(center, center + Vector3.forward, Color.blue);
    }

    private void OnDrawGizmos()
    {
    }

    private void OnGUI()
    {
    }
  }
}
