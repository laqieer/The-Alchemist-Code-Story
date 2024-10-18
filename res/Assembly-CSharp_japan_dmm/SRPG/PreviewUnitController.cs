// Decompiled with JetBrains decompiler
// Type: SRPG.PreviewUnitController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using SRPG.AnimEvents;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class PreviewUnitController : UnitController
  {
    private Vector3 mStartPosition;
    public string UnitID = "UN010000";
    public string JobID = "ninja";
    private List<string> mCameraAnimationNames = new List<string>();
    private Transform mCameraPos;
    private Transform mEnemyPos;
    private int mCameraID;
    private Quaternion mCameraShakeOffset;
    private float mCameraShakeSeedX;
    private float mCameraShakeSeedY;
    public string mCurrentAnimation = string.Empty;
    public List<string> mAnimationFiles = new List<string>();
    private AnimationClip mCameraAnimation;
    private bool mUseCamera;
    private Vector2 mAnimListScrollPos;
    private Vector2 mCameraAnimListScrollPos;
    private bool mLoadingAnimation;
    private bool mLoopAnimation;
    private bool mMirror;
    private float mSpeed = 1f;
    private bool mAnimationLoaded;
    private static readonly string SLOT0 = "0";

    private void Awake()
    {
      MonoSingleton<GameManager>.Instance.Deserialize(JSONParser.parseJSONObject<JSON_MasterParam>(Resources.Load<TextAsset>("Data/MasterParam").text));
      UnitData unitData = new UnitData();
      unitData.Setup(this.UnitID, 0, 0, 0, this.JobID, 0);
      this.SetupUnit(unitData);
      this.mStartPosition = ((Component) this).transform.position;
    }

    protected override void PostSetup()
    {
    }

    protected override void OnEventStart(AnimEvent e, float weight)
    {
      switch (e)
      {
        case ToggleCamera _:
          this.mCameraID = (e as ToggleCamera).CameraID;
          if (this.mCameraID >= 0 && this.mCameraID <= 1)
            break;
          Debug.LogError((object) ("Invalid CameraID: " + (object) this.mCameraID));
          break;
        case CameraShake _:
          this.mCameraShakeSeedX = Random.value;
          this.mCameraShakeSeedY = Random.value;
          break;
      }
    }

    protected override void OnEvent(AnimEvent e, float time, float weight)
    {
      if (!(e is CameraShake))
        return;
      this.mCameraShakeOffset = (e as CameraShake).CalcOffset(time, this.mCameraShakeSeedX, this.mCameraShakeSeedY);
    }

    protected override void OnDestroy() => base.OnDestroy();

    private new void Update()
    {
      this.mCameraShakeOffset = Quaternion.identity;
      base.Update();
      if (!this.mLoadingAnimation || this.IsLoading)
        return;
      this.mAnimationLoaded = true;
      this.mLoadingAnimation = false;
      ((Component) this).transform.position = this.mStartPosition;
      this.mCameraID = 0;
      this.PlayAnimation(PreviewUnitController.SLOT0, this.mLoopAnimation);
      AnimDef animation = this.GetAnimation(PreviewUnitController.SLOT0);
      if (Object.op_Inequality((Object) this.mCameraAnimation, (Object) null))
      {
        ((Component) this.mCameraPos).GetComponent<Animation>().AddClip(this.mCameraAnimation, PreviewUnitController.SLOT0);
        ((Component) this.mCameraPos).GetComponent<Animation>().Play(PreviewUnitController.SLOT0);
        ((Component) this.mCameraPos).GetComponent<Animation>()[PreviewUnitController.SLOT0].speed = (float) (1.0 / (double) this.mCameraAnimation.length * 1.0) / animation.Length;
      }
      else
      {
        ((Component) this.mCameraPos).GetComponent<Animation>().AddClip(animation.animation, PreviewUnitController.SLOT0);
        ((Component) this.mCameraPos).GetComponent<Animation>().Play(PreviewUnitController.SLOT0);
        ((Component) this.mCameraPos).GetComponent<Animation>()[PreviewUnitController.SLOT0].speed = 1f;
      }
      ((Component) this.mEnemyPos).GetComponent<Animation>().AddClip(animation.animation, PreviewUnitController.SLOT0);
      ((Component) this.mEnemyPos).GetComponent<Animation>().Play(PreviewUnitController.SLOT0);
    }

    private new void LateUpdate()
    {
      base.LateUpdate();
      if (!this.mUseCamera)
        return;
      Transform transform1 = this.mCameraPos.Find(this.mCameraID != 0 ? "Camera002" : "Camera001");
      Transform transform2 = ((Component) Camera.main).transform;
      Vector3 position = transform1.position;
      Quaternion quaternion = transform1.rotation;
      if (this.mMirror)
      {
        quaternion.z = -quaternion.z;
        quaternion.y = -quaternion.y;
      }
      quaternion = Quaternion.op_Multiply(quaternion, GameUtility.Yaw180);
      transform2.position = position;
      transform2.rotation = Quaternion.op_Multiply(this.mCameraShakeOffset, quaternion);
      Debug.DrawLine(position, Vector3.op_Addition(position, Vector3.op_Multiply(transform2.forward, 5f)), Color.yellow);
      PreviewUnitController.DrawAxis(position);
      Debug.DrawLine(transform1.position, Vector3.op_Subtraction(transform1.position, Vector3.op_Multiply(transform1.forward, 5f)), Color.magenta);
      PreviewUnitController.DrawAxis(transform1.position);
    }

    private static void DrawAxis(Vector3 center)
    {
      Debug.DrawLine(center, Vector3.op_Addition(center, Vector3.right), Color.red);
      Debug.DrawLine(center, Vector3.op_Addition(center, Vector3.up), Color.green);
      Debug.DrawLine(center, Vector3.op_Addition(center, Vector3.forward), Color.blue);
    }

    private void OnDrawGizmos()
    {
    }

    private void OnGUI()
    {
    }
  }
}
