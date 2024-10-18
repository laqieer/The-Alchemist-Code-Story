// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_SpawnObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [EventActionInfo("オブジェクト/配置", "シーンにオブジェクトを配置します。", 4478293, 4491400)]
  public class EventAction_SpawnObject : EventAction
  {
    public const string ResourceDir = "EventAssets/";
    [StringIsResourcePath(typeof (GameObject), "EventAssets/")]
    public string ResourceID;
    public string ObjectID;
    private LoadRequest mResourceLoadRequest;
    public bool Persistent;
    public Vector3 Position;
    private GameObject mGO;

    public override void OnActivate()
    {
      if (this.mResourceLoadRequest != null && this.mResourceLoadRequest.asset != (UnityEngine.Object) null)
      {
        Transform transform = (this.mResourceLoadRequest.asset as GameObject).transform;
        this.mGO = UnityEngine.Object.Instantiate(this.mResourceLoadRequest.asset, this.Position + transform.position, transform.rotation) as GameObject;
        if (!string.IsNullOrEmpty(this.ObjectID))
          GameUtility.RequireComponent<GameObjectID>(this.mGO).ID = this.ObjectID;
        if (this.Persistent && (UnityEngine.Object) TacticsSceneSettings.Instance != (UnityEngine.Object) null)
          this.mGO.transform.SetParent(TacticsSceneSettings.Instance.transform, true);
        this.Sequence.SpawnedObjects.Add(this.mGO);
      }
      this.ActivateNext();
    }

    protected override void OnDestroy()
    {
      base.OnDestroy();
      if (!((UnityEngine.Object) this.mGO != (UnityEngine.Object) null) || this.Persistent && !((UnityEngine.Object) this.mGO.transform.parent == (UnityEngine.Object) null))
        return;
      UnityEngine.Object.Destroy((UnityEngine.Object) this.mGO);
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
      return (IEnumerator) new EventAction_SpawnObject.\u003CPreloadAssets\u003Ec__IteratorB1() { \u003C\u003Ef__this = this };
    }
  }
}
