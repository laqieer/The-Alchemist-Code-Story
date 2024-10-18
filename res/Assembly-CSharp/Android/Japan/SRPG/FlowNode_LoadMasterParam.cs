// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_LoadMasterParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Threading;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("System/Master/LoadMasterParam", 16777215)]
  [FlowNode.Pin(0, "Start", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "Finished", FlowNode.PinTypes.Output, 100)]
  public class FlowNode_LoadMasterParam : FlowNode
  {
    private Mutex mMutex;
    private int mResult;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0 || this.enabled)
        return;
      this.mResult = 0;
      if (AssetManager.UseDLC && GameUtility.Config_UseAssetBundles.Value)
      {
        this.enabled = true;
        CriticalSection.Enter(CriticalSections.Default);
        FlowNode_LoadMasterParam.ThreadStartParam threadStartParam = new FlowNode_LoadMasterParam.ThreadStartParam();
        threadStartParam.self = this;
        threadStartParam.GameManager = MonoSingleton<GameManager>.Instance;
        Debug.Log((object) "Starting Thread");
        this.mMutex = new Mutex();
        new Thread(new ParameterizedThreadStart(FlowNode_LoadMasterParam.LoadMasterDataThread)).Start((object) threadStartParam);
      }
      else
      {
        this.enabled = false;
        this.ActivateOutputLinks(100);
      }
    }

    protected override void OnDestroy()
    {
      if (this.mMutex != null)
      {
        this.mMutex.WaitOne();
        this.mMutex.ReleaseMutex();
        this.mMutex.Close();
        this.mMutex = (Mutex) null;
      }
      base.OnDestroy();
    }

    private void Update()
    {
      if (this.mMutex == null)
        return;
      this.mMutex.WaitOne();
      bool flag = this.mResult != 0;
      this.mMutex.ReleaseMutex();
      if (!flag)
        return;
      this.mMutex.Close();
      this.mMutex = (Mutex) null;
      if (this.mResult < 0)
        DebugUtility.LogError("Failed to load MasterParam");
      this.enabled = false;
      CriticalSection.Leave(CriticalSections.Default);
      this.ActivateOutputLinks(100);
    }

    private static void LoadMasterDataThread(object param)
    {
      Debug.Log((object) nameof (LoadMasterDataThread));
      FlowNode_LoadMasterParam.ThreadStartParam threadStartParam = (FlowNode_LoadMasterParam.ThreadStartParam) param;
      Debug.Log((object) "LoadMasterDataThread START");
      int num = -1;
      try
      {
        num = !threadStartParam.GameManager.ReloadMasterData((string) null, (string) null) ? -1 : 1;
      }
      catch (Exception ex)
      {
        Debug.LogException(ex);
      }
      Debug.Log((object) "LoadMasterDataThread END");
      if (threadStartParam.self.mMutex == null)
        return;
      threadStartParam.self.mMutex.WaitOne();
      threadStartParam.self.mResult = num;
      threadStartParam.self.mMutex.ReleaseMutex();
    }

    private struct ThreadStartParam
    {
      public FlowNode_LoadMasterParam self;
      public GameManager GameManager;
    }
  }
}
