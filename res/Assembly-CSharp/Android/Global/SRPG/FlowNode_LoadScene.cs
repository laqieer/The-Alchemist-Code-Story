﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_LoadScene
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using Gsc.App;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SRPG
{
  [FlowNode.NodeType("LoadScene", 32741)]
  [FlowNode.Pin(21, "Booted", FlowNode.PinTypes.Output, 21)]
  [FlowNode.Pin(1, "Finished", FlowNode.PinTypes.Output, 3)]
  [FlowNode.Pin(100, "Start", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(20, "Started", FlowNode.PinTypes.Output, 2)]
  public class FlowNode_LoadScene : FlowNode
  {
    private const string StartSceneName = "1_CheckVersion";
    public FlowNode_LoadScene.SceneTypes SceneType;
    public string SceneName;
    public float WaitTime;
    private float mLoadStartTime;
    private SceneRequest mOp;

    public static void LoadBootScene()
    {
      BootLoader.Reboot();
      SceneManager.LoadScene("1_CheckVersion");
    }

    public override void OnActivate(int pinID)
    {
      if (pinID != 100 || this.enabled)
        return;
      string str = (string) null;
      switch (this.SceneType)
      {
        case FlowNode_LoadScene.SceneTypes.HomeTown:
          SectionParam homeWorld = HomeUnitController.GetHomeWorld();
          if (homeWorld != null)
          {
            str = homeWorld.home;
            break;
          }
          break;
        case FlowNode_LoadScene.SceneTypes.BootScene:
          FlowNode_LoadScene.LoadBootScene();
          this.ActivateOutputLinks(21);
          return;
        default:
          str = this.SceneName;
          break;
      }
      if (string.IsNullOrEmpty(str))
      {
        DebugUtility.LogError("No Scene to load");
      }
      else
      {
        this.enabled = true;
        CriticalSection.Enter(CriticalSections.SceneChange);
        this.ActivateOutputLinks(20);
        DebugUtility.Log("LoadScene [" + str + "]");
        if (AssetManager.IsAssetBundle(str))
          this.StartCoroutine(this.PreLoadSceneAsync(str));
        else
          this.StartSceneLoad(str);
      }
    }

    [DebuggerHidden]
    private IEnumerator PreLoadSceneAsync(string sceneName)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_LoadScene.\u003CPreLoadSceneAsync\u003Ec__IteratorCE() { sceneName = sceneName, \u003C\u0024\u003EsceneName = sceneName, \u003C\u003Ef__this = this };
    }

    private void PreLoadScene(string sceneName)
    {
      this.StartSceneLoad(sceneName);
    }

    private void StartSceneLoad(string sceneName)
    {
      this.mOp = this.SceneType != FlowNode_LoadScene.SceneTypes.Replace ? AssetManager.LoadSceneAsync(sceneName, true) : AssetManager.LoadSceneAsync(sceneName, false);
      this.mLoadStartTime = Time.time;
      this.StartCoroutine(this.LoadLevelAsync());
    }

    [DebuggerHidden]
    private IEnumerator LoadLevelAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_LoadScene.\u003CLoadLevelAsync\u003Ec__IteratorCF() { \u003C\u003Ef__this = this };
    }

    public enum SceneTypes
    {
      Additive,
      HomeTown,
      Replace,
      BootScene,
    }
  }
}
