// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqGalleryHighlight
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;

namespace SRPG
{
  [FlowNode.NodeType("System/ReqGalleryHighlight", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "Success", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(1000, "ハイライト期間外", FlowNode.PinTypes.Output, 1000)]
  [FlowNode.Pin(1001, "ハイライトデータ作成中", FlowNode.PinTypes.Output, 1001)]
  public class FlowNode_ReqGalleryHighlight : FlowNode_Network
  {
    private const int PIN_IN_REQUEST = 0;
    private const int PIN_OUT_SUCCESS = 100;
    public const int PIN_OUT_HIGHLIGHT_OUTOFPERIOD = 1000;
    public const int PIN_OUT_HIGHLIGHT_INPROGRESS = 1001;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      this.enabled = true;
      this.ExecRequest((WebAPI) new ReqGalleryHighlight(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
    }

    private void Success()
    {
      this.enabled = false;
      this.ActivateOutputLinks(100);
    }

    public override void OnSuccess(WWWResult www)
    {
      WebAPI.JSON_BodyResponse<FlowNode_ReqGalleryHighlight.Json_Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_ReqGalleryHighlight.Json_Response>>(www.text);
      SerializeValueList currentValue = FlowNode_ButtonEvent.currentValue as SerializeValueList;
      if (currentValue == null)
      {
        DebugUtility.LogError("SerializeValueListが取得できません。");
        Network.RemoveAPI();
      }
      else
      {
        currentValue.AddObject("highlight", (object) jsonObject.body);
        switch (jsonObject.body.highlight_status)
        {
          case 0:
            this.ActivateOutputLinks(1000);
            Network.RemoveAPI();
            break;
          case 1:
            this.ActivateOutputLinks(1001);
            Network.RemoveAPI();
            break;
          default:
            Network.RemoveAPI();
            this.ActivateOutputLinks(100);
            break;
        }
      }
    }

    [Serializable]
    public class Json_Response
    {
      public string highlight_iname;
      public int highlight_status;
      public int is_highlight_rewarded;
      public int is_mail_reward;
      public FlowNode_ReqGalleryHighlight.JSON_HighlightInfo highlight_info;
    }

    [Serializable]
    public class JSON_HighlightInfo
    {
      public FlowNode_ReqGalleryHighlight.JSON_HighlightPlayer player;
      public FlowNode_ReqGalleryHighlight.JSON_HighlightQuest quest;
      public FlowNode_ReqGalleryHighlight.JSON_HighlightGallery gallery;
      public FlowNode_ReqGalleryHighlight.JSON_HighlightArena arena;
      public FlowNode_ReqGalleryHighlight.JSON_HighlightTower tower;
      public FlowNode_ReqGalleryHighlight.JSON_HighlightGuild guild;
      public FlowNode_ReqGalleryHighlight.JSON_HighlightFriend friend;
    }

    [Serializable]
    public class JSON_HighlightPlayer
    {
      public string name;
      public long game_start;
    }

    [Serializable]
    public class JSON_HighlightQuest
    {
      public int clear_count;
    }

    [Serializable]
    public class JSON_HighlightGallery
    {
      public int unit_count;
      public int concept_card_count;
      public int artifact_count;
    }

    [Serializable]
    public class JSON_HighlightArena
    {
      public int rank_best;
    }

    [Serializable]
    public class JSON_HighlightTower
    {
      public int veda_clear_floor;
      public int mebius_clear_floor;
    }

    [Serializable]
    public class JSON_HighlightGuild
    {
      public string guild_name;
      public int join_days;
      public int level;
      public int member_count;
      public int role_id;
    }

    [Serializable]
    public class JSON_HighlightFriend
    {
      public int count;
    }
  }
}
