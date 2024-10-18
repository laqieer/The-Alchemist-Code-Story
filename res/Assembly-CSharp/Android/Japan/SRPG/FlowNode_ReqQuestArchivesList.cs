// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqQuestArchivesList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;

namespace SRPG
{
  [FlowNode.NodeType("System/ReqQuest/ArchivesList", 32741)]
  [FlowNode.Pin(50, "書庫内の開放中のクエスト一覧を取得する", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(200, "書庫内の開放中のクエスト一覧を取得した", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(300, "書庫内の開放中のクエスト一覧の取得に失敗した", FlowNode.PinTypes.Output, 2)]
  public class FlowNode_ReqQuestArchivesList : FlowNode_Network
  {
    public const int INPUT_REQUEST_GET_QUEST_ARCHIVES_LIST = 50;
    public const int OUTPUT_REQUEST_QUEST_ARCHIVES_LIST_OK = 200;
    public const int OUTPUT_REQUEST_QUEST_ARCHIVES_LIST_FAIL = 300;

    public override void OnActivate(int pinID)
    {
      if (pinID == 50)
        this.ExecRequest((WebAPI) new FlowNode_ReqQuestArchivesList.ReqQuestArchivesList(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
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
        int errCode = (int) Network.ErrCode;
        this.OnBack();
      }
      else
      {
        WebAPI.JSON_BodyResponse<JSON_OpenedQuestArchivesListResponse> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_OpenedQuestArchivesListResponse>>(www.text);
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

    public class ReqQuestArchivesList : WebAPI
    {
      public ReqQuestArchivesList(Network.ResponseCallback response)
      {
        this.name = "archive";
        this.body = WebAPI.GetRequestString((string) null);
        this.callback = response;
      }
    }
  }
}
