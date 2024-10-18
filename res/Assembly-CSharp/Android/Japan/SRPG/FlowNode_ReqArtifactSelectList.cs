// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqArtifactSelectList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.NodeType("System/ReqArtifact/ReqArtifactSelectList", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_ReqArtifactSelectList : FlowNode_Network
  {
    public ArtifactSelectListData mArtifactSelectListData;
    public GetArtifactWindow mGetArtifactWindow;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0 || this.enabled)
        return;
      if (Network.Mode == Network.EConnectMode.Online)
      {
        MailData mail = MonoSingleton<GameManager>.Instance.FindMail((long) GlobalVars.SelectedMailUniqueID);
        if (mail == null)
        {
          this.enabled = false;
        }
        else
        {
          this.enabled = true;
          this.ExecRequest((WebAPI) new ReqMailSelect(mail.Find(GiftTypes.SelectArtifactItem).iname, ReqMailSelect.type.artifact, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        }
      }
      else
      {
        this.Deserialize(this.DummyResponse());
        this.Success();
      }
    }

    private Json_ArtifactSelectResponse DummyResponse()
    {
      string[] strArray = new string[1]{ "AF_ARMS_SWO_MITHRIL_GREEN" };
      int length = strArray.Length;
      Json_ArtifactSelectResponse artifactSelectResponse = new Json_ArtifactSelectResponse();
      artifactSelectResponse.select = new Json_ArtifactSelectItem[length];
      if ((UnityEngine.Object) MonoSingleton<GameManager>.GetInstanceDirect() == (UnityEngine.Object) null)
      {
        GameManager instance = MonoSingleton<GameManager>.Instance;
      }
      for (int index = 0; index < length; ++index)
        artifactSelectResponse.select[index] = new Json_ArtifactSelectItem()
        {
          iname = strArray[index]
        };
      return artifactSelectResponse;
    }

    private void Deserialize(Json_ArtifactSelectResponse json)
    {
      this.mArtifactSelectListData = new ArtifactSelectListData();
      this.mArtifactSelectListData.Deserialize(json);
      this.mGetArtifactWindow.Refresh(this.mArtifactSelectListData.items.ToArray());
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        int errCode = (int) Network.ErrCode;
        this.OnRetry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_ArtifactSelectResponse> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_ArtifactSelectResponse>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          this.OnRetry();
        }
        else
        {
          Network.RemoveAPI();
          for (int index = 0; index < jsonObject.body.select.Length; ++index)
          {
            Json_ArtifactSelectItem artifactSelectItem = jsonObject.body.select[index];
            if (artifactSelectItem.num > (short) 1)
              DebugUtility.LogError("武具は一つしか付与できません " + artifactSelectItem.iname);
          }
          this.Deserialize(jsonObject.body);
          this.Success();
        }
      }
    }

    private void Success()
    {
      this.enabled = false;
      this.ActivateOutputLinks(1);
    }
  }
}
