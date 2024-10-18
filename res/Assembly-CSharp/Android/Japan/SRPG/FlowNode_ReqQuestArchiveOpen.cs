// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqQuestArchiveOpen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;

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
      this.enabled = true;
    }

    private void Success()
    {
      this.enabled = false;
      this.ActivateOutputLinks(200);
    }

    private void Failure()
    {
      FlowNode_Network.Failed();
      this.enabled = false;
      this.ActivateOutputLinks(300);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.QuestArchive_ArchiveNotFound:
            UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.QUEST_ARCHIVE_NOT_FOUND"), (UIUtility.DialogResultEvent) (go => this.OnBack()), (GameObject) null, false, -1);
            break;
          case Network.EErrCode.QuestArchive_ArchiveAlreadyOpened:
            UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.QUEST_ARCHIVE_ALREADY_OPENED"), (UIUtility.DialogResultEvent) (go => this.OnBack()), (GameObject) null, false, -1);
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
