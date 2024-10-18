// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_SpawnActor2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [EventActionInfo("New/アクター/配置", "キャラクターを配置します", 6702148, 11158596)]
  public class EventAction_SpawnActor2 : EventAction
  {
    public bool ShowEquipments = true;
    [Tooltip("マス目にスナップさせるか？")]
    public bool MoveSnap = true;
    [Tooltip("地面にスナップさせるか？")]
    public bool GroundSnap = true;
    [Tooltip("表示設定")]
    public bool Display = true;
    [Tooltip("ゆれもの設定")]
    public bool Yuremono = true;
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
    public TacticsUnitController.PostureTypes Posture;

    private GameObject GetPersistentScene()
    {
      if ((UnityEngine.Object) SceneBattle.Instance != (UnityEngine.Object) null)
        return SceneBattle.Instance.CurrentScene;
      return (GameObject) null;
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
      return (IEnumerator) new EventAction_SpawnActor2.\u003CPreloadAssets\u003Ec__Iterator0() { \u0024this = this };
    }

    public override void OnActivate()
    {
      if ((UnityEngine.Object) this.mController != (UnityEngine.Object) null)
      {
        this.mController.transform.position = this.Position;
        this.mController.CollideGround = this.GroundSnap;
        this.mController.transform.rotation = Quaternion.Euler(this.RotationX, this.RotationY, this.RotationZ);
        this.mController.SetVisible(this.Display);
        if (!this.Yuremono)
        {
          foreach (Behaviour componentsInChild in this.mController.gameObject.GetComponentsInChildren<YuremonoInstance>())
            componentsInChild.enabled = false;
        }
      }
      this.ActivateNext();
    }

    protected override void OnDestroy()
    {
      if (!((UnityEngine.Object) this.mController != (UnityEngine.Object) null) || this.Persistent)
        return;
      UnityEngine.Object.Destroy((UnityEngine.Object) this.mController.gameObject);
    }
  }
}
