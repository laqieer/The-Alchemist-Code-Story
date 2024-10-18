// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SetName
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("System/Player/SetName", 32741)]
  [FlowNode.Pin(100, "Start", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(2, "Failure", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(3, "Rename", FlowNode.PinTypes.Output, 12)]
  public class FlowNode_SetName : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 100)
        return;
      if (Network.Mode == Network.EConnectMode.Online)
      {
        if (string.IsNullOrEmpty(GlobalVars.EditPlayerName))
          GlobalVars.EditPlayerName = MonoSingleton<GameManager>.Instance.Player.Name;
        if (string.IsNullOrEmpty(GlobalVars.EditPlayerName))
        {
          UIUtility.SystemMessage((string) null, LocalizedText.Get("sys.RENAME_PLAYER_NAME"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
          this.ActivateOutputLinks(3);
          this.enabled = false;
        }
        else if (!MyMsgInput.isLegal(GlobalVars.EditPlayerName))
        {
          UIUtility.SystemMessage((string) null, LocalizedText.Get("sys.RENAME_PLAYER_NAME"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
          this.ActivateOutputLinks(3);
          this.enabled = false;
        }
        else
        {
          this.ExecRequest((WebAPI) new ReqSetName(GlobalVars.EditPlayerName, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          this.enabled = true;
        }
      }
      else
        this.Success();
    }

    private void Success()
    {
      if (Network.Mode == Network.EConnectMode.Online)
      {
        MyMetaps.TrackTutorialPoint("SetName");
        MyGrowthPush.registCustomerId(MonoSingleton<GameManager>.Instance.Player.OkyakusamaCode);
      }
      this.enabled = false;
      this.ActivateOutputLinks(1);
    }

    private void Failure()
    {
      this.enabled = false;
      this.ActivateOutputLinks(2);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        if (Network.ErrCode == Network.EErrCode.IllegalName)
          this.OnBack();
        else
          this.OnRetry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          this.OnRetry();
        }
        else
        {
          try
          {
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.units);
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.items);
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.mails);
          }
          catch (Exception ex)
          {
            Debug.LogException(ex);
            this.Failure();
            return;
          }
          GameParameter.UpdateValuesOfType(GameParameter.ParameterTypes.GLOBAL_PLAYER_NAME);
          Network.RemoveAPI();
          this.Success();
        }
      }
    }
  }
}
