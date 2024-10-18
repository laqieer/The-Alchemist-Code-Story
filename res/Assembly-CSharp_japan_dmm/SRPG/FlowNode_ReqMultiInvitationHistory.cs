// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqMultiInvitationHistory
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Text;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/WebApi/MultiInvitationHistory", 32741)]
  [FlowNode.Pin(100, "マルチ招待ログ一覧", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(110, "マルチ招待ログ一覧取得完了", FlowNode.PinTypes.Output, 110)]
  [FlowNode.Pin(120, "マルチ招待ログ一覧取得失敗", FlowNode.PinTypes.Output, 120)]
  public class FlowNode_ReqMultiInvitationHistory : FlowNode_Network
  {
    public const int INPUT_MULTIINVITATION = 100;
    public const int OUTPUT_MULTIINVITATION_SUCCESS = 110;
    public const int OUTPUT_MULTIINVITATION_FAILED = 120;
    private FlowNode_ReqMultiInvitationHistory.ApiBase m_Api;

    public override void OnActivate(int pinID)
    {
      if (this.m_Api != null)
      {
        DebugUtility.LogError("同時に複数の通信が入ると駄目！");
      }
      else
      {
        if (pinID == 100)
        {
          MultiInvitationReceiveWindow instance = MultiInvitationReceiveWindow.instance;
          if (instance != null)
          {
            int page = 1;
            if (instance.GetLogPage() != page)
            {
              this.m_Api = (FlowNode_ReqMultiInvitationHistory.ApiBase) new FlowNode_ReqMultiInvitationHistory.Api_MultiInvitationHistory(this, page);
            }
            else
            {
              this.m_Api = (FlowNode_ReqMultiInvitationHistory.ApiBase) new FlowNode_ReqMultiInvitationHistory.Api_MultiInvitationHistory(this, page);
              this.m_Api.Success();
              this.m_Api = (FlowNode_ReqMultiInvitationHistory.ApiBase) null;
            }
          }
        }
        if (this.m_Api == null)
          return;
        this.m_Api.Start();
        ((Behaviour) this).enabled = true;
      }
    }

    public override void OnSuccess(WWWResult www)
    {
      if (this.m_Api == null)
        return;
      this.m_Api.Complete(www);
      this.m_Api = (FlowNode_ReqMultiInvitationHistory.ApiBase) null;
    }

    public class ApiBase
    {
      protected FlowNode_ReqMultiInvitationHistory m_Node;
      protected RequestAPI m_Request;

      public ApiBase(FlowNode_ReqMultiInvitationHistory node) => this.m_Node = node;

      public virtual string url => string.Empty;

      public virtual string req => (string) null;

      public virtual void Success()
      {
      }

      public virtual void Failed()
      {
      }

      public virtual void Complete(WWWResult www)
      {
      }

      public virtual void Start()
      {
        if (Network.Mode == Network.EConnectMode.Online)
          this.m_Node.ExecRequest((WebAPI) new RequestAPI(this.url, new Network.ResponseCallback(((FlowNode_Network) this.m_Node).ResponseCallback), this.req));
        else
          this.Failed();
      }
    }

    public class Api_MultiInvitationHistory : FlowNode_ReqMultiInvitationHistory.ApiBase
    {
      private int m_Page = 1;

      public Api_MultiInvitationHistory(FlowNode_ReqMultiInvitationHistory node, int page)
        : base(node)
      {
        this.m_Page = page;
      }

      public override string url => "btl/room/invitation/history";

      public override string req
      {
        get
        {
          StringBuilder stringBuilder = new StringBuilder(128);
          stringBuilder.Append("\"page\":" + (object) this.m_Page);
          stringBuilder.Append(",\"id\":0");
          return stringBuilder.ToString();
        }
      }

      public override void Success()
      {
        this.m_Node.ActivateOutputLinks(110);
        ((Behaviour) this.m_Node).enabled = false;
      }

      public override void Failed()
      {
        this.m_Node.ActivateOutputLinks(120);
        Network.RemoveAPI();
        Network.ResetError();
        ((Behaviour) this.m_Node).enabled = false;
      }

      public override void Complete(WWWResult www)
      {
        if (Network.IsError)
        {
          this.m_Node.OnFailed();
        }
        else
        {
          DebugMenu.Log("API", this.url + ":" + www.text);
          WebAPI.JSON_BodyResponse<FlowNode_ReqMultiInvitationHistory.Api_MultiInvitationHistory.Json> jsonBodyResponse = JsonUtility.FromJson<WebAPI.JSON_BodyResponse<FlowNode_ReqMultiInvitationHistory.Api_MultiInvitationHistory.Json>>(www.text);
          DebugUtility.Assert(jsonBodyResponse != null, "res == null");
          if (jsonBodyResponse.body != null)
            MultiInvitationReceiveWindow.instance?.DeserializeLogList(this.m_Page, jsonBodyResponse.body);
          Network.RemoveAPI();
          this.Success();
        }
      }

      [Serializable]
      public class JsonPlayer
      {
        public string uid;
        public string fuid;
        public string name;
        public int lv;
        public string lastlogin;
        public Json_Unit unit;
      }

      [Serializable]
      public class JsonList
      {
        public int id;
        public int roomid;
        public string iname;
        public string btype;
        public string created_at;
        public FlowNode_ReqMultiInvitationHistory.Api_MultiInvitationHistory.JsonPlayer player;
      }

      [Serializable]
      public class Json
      {
        public FlowNode_ReqMultiInvitationHistory.Api_MultiInvitationHistory.JsonList[] list;
      }
    }
  }
}
