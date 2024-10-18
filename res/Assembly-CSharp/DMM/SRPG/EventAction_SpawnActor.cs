// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_SpawnActor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [EventActionInfo("アクター/配置", "キャラクターを配置します", 6702148, 11158596)]
  public class EventAction_SpawnActor : EventAction
  {
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
    public bool ShowEquipments = true;
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
      return (IEnumerator) new EventAction_SpawnActor.\u003CPreloadAssets\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    public override void OnActivate()
    {
      if (Object.op_Inequality((Object) this.mController, (Object) null))
      {
        ((Component) this.mController).transform.position = new Vector3((float) this.Position.x + 0.5f, 0.0f, (float) this.Position.y + 0.5f);
        ((Component) this.mController).transform.rotation = Quaternion.AngleAxis((float) this.Angle, Vector3.up);
        this.mController.SetVisible(true);
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
