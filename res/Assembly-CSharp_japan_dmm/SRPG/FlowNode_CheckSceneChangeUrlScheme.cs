// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_CheckSceneChangeUrlScheme
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Scene/CheckSceneChangeUrlScheme", 32741)]
  [FlowNode.Pin(0, "CheckStart", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "MovieEndEvent", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(100, "CheckFinished", FlowNode.PinTypes.Output, 100)]
  public class FlowNode_CheckSceneChangeUrlScheme : FlowNodePersistent
  {
    private FlowNode_CheckSceneChangeUrlScheme.CheckFlag m_checkFlag;
    private bool startCheck;

    private void Start() => ((Behaviour) this).enabled = true;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      this.startCheck = true;
      this.m_checkFlag = (FlowNode_CheckSceneChangeUrlScheme.CheckFlag) 0;
    }

    private void Update()
    {
      if (!this.startCheck)
        return;
      if ((this.m_checkFlag & FlowNode_CheckSceneChangeUrlScheme.CheckFlag.FinishedCheck) != (FlowNode_CheckSceneChangeUrlScheme.CheckFlag) 0)
        this.Finished();
      else if ((this.m_checkFlag & FlowNode_CheckSceneChangeUrlScheme.CheckFlag.FinishEndMovie) != (FlowNode_CheckSceneChangeUrlScheme.CheckFlag) 0)
      {
        this.m_checkFlag |= FlowNode_CheckSceneChangeUrlScheme.CheckFlag.FinishedCheck;
        DebugUtility.Log("CheckSceneChangeUrlScheme => Flag = FinishedCheck");
      }
      else if (StreamingMovie.IsPlaying)
      {
        if ((this.m_checkFlag & FlowNode_CheckSceneChangeUrlScheme.CheckFlag.StartEndMovie) != (FlowNode_CheckSceneChangeUrlScheme.CheckFlag) 0)
          return;
        this.PushMovieEndEvent();
      }
      else
      {
        this.m_checkFlag |= FlowNode_CheckSceneChangeUrlScheme.CheckFlag.FinishEndMovie;
        DebugUtility.Log("CheckSceneChangeUrlScheme => Flag = FinishEndMovie");
      }
    }

    private void Finished()
    {
      this.startCheck = false;
      this.m_checkFlag = (FlowNode_CheckSceneChangeUrlScheme.CheckFlag) 0;
      this.ActivateOutputLinks(100);
      DebugUtility.Log("CheckSceneChangeUrlScheme => Finished");
    }

    private void PushMovieEndEvent()
    {
      MonoSingleton<StreamingMovie>.Instance.IsNotReplay = true;
      this.ActivateOutputLinks(10);
      DebugUtility.Log("CheckSceneChangeUrlScheme => Flag = StartEndMovie");
      this.m_checkFlag |= FlowNode_CheckSceneChangeUrlScheme.CheckFlag.StartEndMovie;
      DebugUtility.Log("CheckSceneChangeUrlScheme => PushMovieEndEvent");
    }

    private enum CheckFlag
    {
      StartEndMovie = 1,
      FinishEndMovie = 2,
      FinishedCheck = 1024, // 0x00000400
    }
  }
}
