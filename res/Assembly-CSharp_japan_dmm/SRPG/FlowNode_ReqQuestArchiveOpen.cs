// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqQuestArchiveOpen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/ReqQuest/ArchiveOpen", 32741)]
  [FlowNode.Pin(50, "クエストを開放する", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(200, "クエストを開放した", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(300, "クエストの開放に失敗した", FlowNode.PinTypes.Output, 2)]
  public class FlowNode_ReqQuestArchiveOpen : FlowNode_Network
  {
    public const int INPUT_REQUEST_GET_QUEST_ARCHIVE_OPEN = 50;
    public const int OUTPUT_REQUEST_QUEST_ARCHIVE_OPEN_OK = 200;
    public const int OUTPUT_REQUEST_QUEST_ARCHIVE_OPEN_FAIL = 300;

    public override void OnActivate(int pinID)
    {
      if (pinID == 50)
        this.ExecRequest((WebAPI) new FlowNode_ReqQuestArchiveOpen.ReqQuestArchiveOpen(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
      ((Behaviour) this).enabled = true;
    }

    private void Success()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(200);
    }

    private void Failure()
    {
      FlowNode_Network.Failed();
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(300);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.QuestArchive_ArchiveNotFound:
            UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.QUEST_ARCHIVE_NOT_FOUND"), (UIUtility.DialogResultEvent) (go => this.OnBack()));
            break;
          case Network.EErrCode.QuestArchive_ArchiveAlreadyOpened:
            UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.QUEST_ARCHIVE_ALREADY_OPENED"), (UIUtility.DialogResultEvent) (go => this.OnBack()));
            break;
          default:
            this.OnBack();
            break;
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<JSON_QuestArchiveOpenResponse> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_QuestArchiveOpenResponse>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        try
        {
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body);
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          this.Failure();
          return;
        }
        this.Success();
      }
    }

    public class ReqQuestArchiveOpen : WebAPI
    {
      public ReqQuestArchiveOpen(Network.ResponseCallback response)
      {
        this.name = "archive/open";
        this.body = WebAPI.GetRequestString("\"iname\":\"" + GlobalVars.SelectedArchiveID + "\"");
        this.callback = response;
      }
    }
  }
}
