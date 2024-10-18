// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_SpawnActor2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [EventActionInfo("New/アクター/配置", "キャラクターを配置します", 6702148, 11158596)]
  public class EventAction_SpawnActor2 : EventAction
  {
    [StringIsActorID]
    public string ActorID;
    [StringIsLocalUnitIDPopup]
    public string UnitID;
    [StringIsJobIDPopup]
    public string JobID;
    [SerializeField]
    public Vector3 Position;
    protected TacticsUnitController mController;
    public bool Persistent;
    [HideInInspector]
    public int Angle;
    [Range(0.0f, 359f)]
    public float RotationX;
    [Range(0.0f, 359f)]
    public float RotationY;
    [Range(0.0f, 359f)]
    public float RotationZ;
    public bool ShowEquipments = true;
    [Tooltip("マス目にスナップさせるか？")]
    public bool MoveSnap = true;
    [Tooltip("地面にスナップさせるか？")]
    public bool GroundSnap = true;
    [Tooltip("表示設定")]
    public bool Display = true;
    [Tooltip("ゆれもの設定")]
    public bool Yuremono = true;
    public TacticsUnitController.PostureTypes Posture;

    private GameObject GetPersistentScene()
    {
      return Object.op_Inequality((Object) SceneBattle.Instance, (Object) null) ? SceneBattle.Instance.CurrentScene : (GameObject) null;
    }

    public override bool IsPreloadAssets => true;

    [DebuggerHidden]
    public override IEnumerator PreloadAssets()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new EventAction_SpawnActor2.\u003CPreloadAssets\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    public override void OnActivate()
    {
      if (Object.op_Inequality((Object) this.mController, (Object) null))
      {
        ((Component) this.mController).transform.position = this.Position;
        this.mController.CollideGround = this.GroundSnap;
        ((Component) this.mController).transform.rotation = Quaternion.Euler(this.RotationX, this.RotationY, this.RotationZ);
        this.mController.SetVisible(this.Display);
        if (!this.Yuremono)
        {
          foreach (Behaviour componentsInChild in ((Component) this.mController).gameObject.GetComponentsInChildren<YuremonoInstance>())
            componentsInChild.enabled = false;
        }
      }
      this.ActivateNext();
    }

    protected override void OnDestroy()
    {
      if (!Object.op_Inequality((Object) this.mController, (Object) null) || this.Persistent)
        return;
      Object.Destroy((Object) ((Component) this.mController).gameObject);
    }
  }
}
