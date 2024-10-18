// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_LoadMasterParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Threading;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/Master/LoadMasterParam", 16777215)]
  [FlowNode.Pin(0, "Start", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "Finished", FlowNode.PinTypes.Output, 100)]
  public class FlowNode_LoadMasterParam : FlowNode
  {
    private Mutex mMutex;
    private GameManager.LoadMasterDataResult mResult;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0 || ((Behaviour) this).enabled)
        return;
      if (AssetManager.UseDLC && GameUtility.Config_UseAssetBundles.Value && !GameUtility.Config_UseServerData.Value)
      {
        ((Behaviour) this).enabled = true;
        CriticalSection.Enter();
        FlowNode_LoadMasterParam.ThreadStartParam parameter = new FlowNode_LoadMasterParam.ThreadStartParam();
        parameter.self = this;
        parameter.GameManager = MonoSingleton<GameManager>.Instance;
        parameter.IsUseSerialized = GameUtility.Config_UseSerializedParams.Value;
        parameter.IsUseEncryption = GameUtility.Config_UseEncryption.Value;
        Debug.Log((object) "Starting Thread");
        this.mMutex = new Mutex();
        new Thread(new ParameterizedThreadStart(FlowNode_LoadMasterParam.LoadMasterDataThread)).Start((object) parameter);
      }
      else
      {
        ((Behaviour) this).enabled = false;
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
      bool flag = this.mResult.Result != GameManager.ELoadMasterDataResult.NOT_YET_MATE;
      this.mMutex.ReleaseMutex();
      if (!flag)
        return;
      this.mMutex.Close();
      this.mMutex = (Mutex) null;
      GameManager.HandleAnyLoadMasterDataErrors(this.mResult, true);
      ((Behaviour) this).enabled = false;
      CriticalSection.Leave();
      this.ActivateOutputLinks(100);
    }

    private static void LoadMasterDataThread(object param)
    {
      Debug.Log((object) nameof (LoadMasterDataThread));
      FlowNode_LoadMasterParam.ThreadStartParam threadStartParam = (FlowNode_LoadMasterParam.ThreadStartParam) param;
      Debug.Log((object) "LoadMasterDataThread START");
      GameManager.LoadMasterDataResult masterDataResult = new GameManager.LoadMasterDataResult();
      try
      {
        masterDataResult = threadStartParam.IsUseSerialized || threadStartParam.IsUseEncryption ? threadStartParam.GameManager.ReloadMasterData(new GameManager.MasterDataInBinary().Load(threadStartParam.IsUseSerialized, threadStartParam.IsUseEncryption)) : threadStartParam.GameManager.ReloadMasterData();
      }
      catch (Exception ex)
      {
        Debug.LogException(ex);
      }
      Debug.Log((object) "LoadMasterDataThread END");
      if (threadStartParam.self.mMutex == null)
        return;
      threadStartParam.self.mMutex.WaitOne();
      threadStartParam.self.mResult = masterDataResult;
      threadStartParam.self.mMutex.ReleaseMutex();
    }

    private class ThreadStartParam
    {
      public FlowNode_LoadMasterParam self;
      public GameManager GameManager;
      public bool IsUseSerialized;
      public bool IsUseEncryption;
    }
  }
}
