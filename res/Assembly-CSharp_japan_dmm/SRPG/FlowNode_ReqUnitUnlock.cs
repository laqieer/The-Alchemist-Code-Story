// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqUnitUnlock
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/Unit/ReqUnitUnlock", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(2, "Download Assets", FlowNode.PinTypes.Output, 2)]
  public class FlowNode_ReqUnitUnlock : FlowNode_Network
  {
    [StringIsResourcePath(typeof (GameObject))]
    public string ResultPrefabPath;
    public string RarityIntName = "rarity";
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
        ((Behaviour) this).enabled = true;
      }
    }

    private void Success()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(1);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        int errCode = (int) Network.ErrCode;
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
      return (IEnumerator) new FlowNode_ReqUnitUnlock.\u003CWaitDownloadAsync\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }
  }
}
