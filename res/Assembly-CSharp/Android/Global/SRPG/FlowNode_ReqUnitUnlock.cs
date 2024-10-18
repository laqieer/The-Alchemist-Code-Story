// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqUnitUnlock
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(2, "Download Assets", FlowNode.PinTypes.Output, 2)]
  [FlowNode.NodeType("System/ReqUnitUnlock", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_ReqUnitUnlock : FlowNode_Network
  {
    public string RarityIntName = "rarity";
    [StringIsResourcePath(typeof (GameObject))]
    public string ResultPrefabPath;
    private LoadRequest mReq;
    private UnitParam mUnlockUnitParam;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (Network.Mode == Network.EConnectMode.Offline)
      {
        this.Success();
      }
      else
      {
        string unlockUnitId = GlobalVars.UnlockUnitID;
        this.mUnlockUnitParam = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(unlockUnitId);
        if (!string.IsNullOrEmpty(this.ResultPrefabPath))
          this.mReq = AssetManager.LoadAsync<GameObject>(this.ResultPrefabPath);
        this.ExecRequest((WebAPI) new ReqUnitUnlock(unlockUnitId, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        this.enabled = true;
      }
    }

    private void Success()
    {
      this.enabled = false;
      this.ActivateOutputLinks(1);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        Network.EErrCode errCode = Network.ErrCode;
        this.OnFailed();
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        if (jsonObject.body == null)
        {
          this.OnFailed();
        }
        else
        {
          try
          {
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.items);
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.units);
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
            this.OnFailed();
            return;
          }
          CriticalSection.Enter(CriticalSections.Network);
          this.StartCoroutine(this.WaitDownloadAsync());
        }
      }
    }

    [DebuggerHidden]
    private IEnumerator WaitDownloadAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_ReqUnitUnlock.\u003CWaitDownloadAsync\u003Ec__IteratorD8() { \u003C\u003Ef__this = this };
    }
  }
}
