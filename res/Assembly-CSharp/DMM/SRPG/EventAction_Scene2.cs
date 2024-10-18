// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_Scene2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [EventActionInfo("New/シーン切り替え", "別シーンに切り替えます", 5592405, 4473992)]
  public class EventAction_Scene2 : EventAction_SceneBase
  {
    [StringIsSceneIDPopup]
    public string SceneID;
    private SceneRequest mAsyncOp;
    private GameObject mSceneRoot;
    [HideInInspector]
    public bool FadeIn = true;
    [HideInInspector]
    public bool WaitFadeIn = true;
    [HideInInspector]
    public float FadeInTime = 3f;
    [HideInInspector]
    public List<GameObject> SceneObject = new List<GameObject>();

    public override void OnActivate()
    {
      this.Sequence.Scene = (GameObject) null;
      LightmapSettings.lightmapsMode = (LightmapsMode) 0;
      if (!string.IsNullOrEmpty(this.SceneID))
      {
        if (this.FadeIn)
          GameUtility.FadeOut(0.0f);
        SceneAwakeObserver.AddListener(new SceneAwakeObserver.SceneEvent(this.OnSceneLoad));
        CriticalSection.Enter(CriticalSections.SceneChange);
        this.mAsyncOp = AssetManager.LoadSceneAsync(this.SceneID, true);
      }
      else
      {
        if (!Object.op_Inequality((Object) this.Sequence.Scene, (Object) null))
          return;
        this.Sequence.Scene = (GameObject) null;
        this.ActivateNext();
      }
    }

    private void OnSceneLoad(GameObject sceneRoot)
    {
      this.mSceneRoot = sceneRoot;
      this.Sequence.Scene = sceneRoot;
      Camera[] componentsInChildren = this.mSceneRoot.GetComponentsInChildren<Camera>(true);
      for (int index = componentsInChildren.Length - 1; index >= 0; --index)
        ((Component) componentsInChildren[index]).gameObject.SetActive(false);
      SceneAwakeObserver.RemoveListener(new SceneAwakeObserver.SceneEvent(this.OnSceneLoad));
    }

    public override void Update()
    {
      if (this.mAsyncOp != null)
      {
        if (this.mAsyncOp.canBeActivated)
          this.mAsyncOp.ActivateScene();
        if (!this.mAsyncOp.isDone)
          return;
        if (this.FadeIn)
          GameUtility.FadeIn(this.FadeInTime);
        CriticalSection.Leave(CriticalSections.SceneChange);
        this.mAsyncOp = (SceneRequest) null;
      }
      else
      {
        if (this.WaitFadeIn && GameUtility.IsScreenFading && this.FadeIn)
          return;
        this.ActivateNext();
      }
    }
  }
}
