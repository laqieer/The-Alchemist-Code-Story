// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_SpawnObject3
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [EventActionInfo("New/オブジェクト/配置2", "シーンにオブジェクトを配置します。", 4478293, 4491400)]
  public class EventAction_SpawnObject3 : EventAction
  {
    public Vector3 Scale = Vector3.one;
    public const string ResourceDir = "EventAssets/";
    [StringIsDemoResourcePath(typeof (GameObject), "EventAssets/")]
    public string ResourceID;
    [StringIsObjectList]
    public string ObjectID;
    private LoadRequest mResourceLoadRequest;
    public bool Persistent;
    public Vector3 Position;
    [Range(0.0f, 360f)]
    private float Angle;
    [Range(0.0f, 359f)]
    public float Rotate_x;
    [Range(0.0f, 359f)]
    public float Rotate_y;
    [Range(0.0f, 359f)]
    public float Rotate_z;
    public Vector3 mousepos;
    private GameObject mGO;

    public override void OnActivate()
    {
      if (this.mResourceLoadRequest != null && this.mResourceLoadRequest.asset != (UnityEngine.Object) null)
      {
        Transform transform = (this.mResourceLoadRequest.asset as GameObject).transform;
        Quaternion quaternion = Quaternion.Euler(this.Rotate_x, this.Rotate_y, this.Rotate_z);
        this.mGO = UnityEngine.Object.Instantiate(this.mResourceLoadRequest.asset, this.Position + transform.position, quaternion * transform.rotation) as GameObject;
        this.mGO.transform.localScale = Vector3.Scale(transform.localScale, this.Scale);
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
      return (IEnumerator) new EventAction_SpawnObject3.\u003CPreloadAssets\u003Ec__IteratorB3() { \u003C\u003Ef__this = this };
    }
  }
}
