﻿// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_Scene
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [EventActionInfo("シーン切り替え(2D)", "別シーンに切り替えます", 5592405, 4473992)]
  public class Event2dAction_Scene : EventAction_SceneBase
  {
    [HideInInspector]
    public bool FadeIn = true;
    [HideInInspector]
    public bool WaitFadeIn = true;
    [HideInInspector]
    public float FadeInTime = 3f;
    [StringIsSceneID]
    public string SceneID;
    private SceneRequest mAsyncOp;
    private GameObject mSceneRoot;

    public override void OnActivate()
    {
      this.Sequence.Scene = (GameObject) null;
      LightmapSettings.lightmapsMode = LightmapsMode.NonDirectional;
      if (!string.IsNullOrEmpty(this.SceneID))
      {
        if (this.FadeIn)
          GameUtility.FadeOut(0.0f);
        CriticalSection.Enter(CriticalSections.SceneChange);
        SceneAwakeObserver.AddListener(new SceneAwakeObserver.SceneEvent(this.OnSceneLoad));
        this.mAsyncOp = AssetManager.LoadSceneAsync(this.SceneID, true);
      }
      else
      {
        if (!((UnityEngine.Object) this.Sequence.Scene != (UnityEngine.Object) null))
          return;
        this.Sequence.Scene = (GameObject) null;
        this.ActivateNext();
      }
    }

    private void OnSceneLoad(GameObject sceneRoot)
    {
      this.mSceneRoot = sceneRoot;
      this.Sequence.Scene = sceneRoot;
      UnityEngine.Camera[] componentsInChildren = this.mSceneRoot.GetComponentsInChildren<UnityEngine.Camera>(true);
      for (int index = componentsInChildren.Length - 1; index >= 0; --index)
        componentsInChildren[index].gameObject.SetActive(false);
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
