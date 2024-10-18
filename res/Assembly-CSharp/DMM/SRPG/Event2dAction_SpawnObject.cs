// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_SpawnObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [EventActionInfo("オブジェクト/配置(2D)", "シーンにオブジェクトを配置します。", 5592405, 4473992)]
  public class Event2dAction_SpawnObject : EventAction
  {
    public const string ResourceDir = "Event2dAssets/";
    [StringIsResourcePath(typeof (GameObject), "Event2dAssets/")]
    public string ResourceID;
    public string ObjectID;
    private LoadRequest mResourceLoadRequest;
    public bool Persistent;
    public Vector3 Position;
    private GameObject mGO;

    public override void OnActivate()
    {
      if (this.mResourceLoadRequest != null && Object.op_Inequality(this.mResourceLoadRequest.asset, (Object) null))
      {
        Transform transform = (this.mResourceLoadRequest.asset as GameObject).transform;
        this.mGO = Object.Instantiate(this.mResourceLoadRequest.asset, Vector3.op_Addition(this.Position, transform.position), transform.rotation) as GameObject;
        if (!string.IsNullOrEmpty(this.ObjectID))
          GameUtility.RequireComponent<GameObjectID>(this.mGO).ID = this.ObjectID;
        if (this.Persistent && Object.op_Inequality((Object) TacticsSceneSettings.Instance, (Object) null))
          this.mGO.transform.SetParent(((Component) TacticsSceneSettings.Instance).transform, true);
        this.Sequence.SpawnedObjects.Add(this.mGO);
      }
      this.ActivateNext();
    }

    protected override void OnDestroy()
    {
      base.OnDestroy();
      if (!Object.op_Inequality((Object) this.mGO, (Object) null) || this.Persistent && !Object.op_Equality((Object) this.mGO.transform.parent, (Object) null))
        return;
      Object.Destroy((Object) this.mGO);
    }

    public override bool IsPreloadAssets => true;

    [DebuggerHidden]
    public override IEnumerator PreloadAssets()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new Event2dAction_SpawnObject.\u003CPreloadAssets\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }
  }
}
