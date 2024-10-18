// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_SpawnActor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [EventActionInfo("アクター/配置", "キャラクターを配置します", 6702148, 11158596)]
  public class EventAction_SpawnActor : EventAction
  {
    public bool ShowEquipments = true;
    [StringIsActorID]
    public string ActorID;
    [StringIsUnitID]
    public string UnitID;
    [StringIsJobID]
    public string JobID;
    [SerializeField]
    private IntVector2 Position;
    private TacticsUnitController mController;
    public bool Persistent;
    [Range(0.0f, 359f)]
    public int Angle;
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
      return (IEnumerator) new EventAction_SpawnActor.\u003CPreloadAssets\u003Ec__IteratorAD() { \u003C\u003Ef__this = this };
    }

    public override void OnActivate()
    {
      if ((UnityEngine.Object) this.mController != (UnityEngine.Object) null)
      {
        this.mController.transform.position = new Vector3((float) this.Position.x + 0.5f, 0.0f, (float) this.Position.y + 0.5f);
        this.mController.transform.rotation = Quaternion.AngleAxis((float) this.Angle, Vector3.up);
        this.mController.SetVisible(true);
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
