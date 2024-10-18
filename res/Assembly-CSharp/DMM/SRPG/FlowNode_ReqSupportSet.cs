// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqSupportSet
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Text;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/ReqSupport/ReqSupportSet", 32741)]
  [FlowNode.Pin(100, "傭兵設定", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(110, "傭兵設定成功", FlowNode.PinTypes.Output, 110)]
  [FlowNode.Pin(120, "傭兵設定失敗", FlowNode.PinTypes.Output, 120)]
  public class FlowNode_ReqSupportSet : FlowNode_Network
  {
    public const int INPUT_SUPPORT_SET = 100;
    public const int OUTPUT_SUPPORT_SET_SUCCESS = 110;
    public const int OUTPUT_SUPPORT_SET_FAILED = 120;
    [SerializeField]
    private SupportSettingRootWindow m_TargetWindow;
    private FlowNode_ReqSupportSet.ApiBase m_Api;

    public override void OnActivate(int pinID)
    {
      if (this.m_Api != null)
      {
        DebugUtility.LogError("同時に複数の通信が入ると駄目！");
      }
      else
      {
        this.m_Api = (FlowNode_ReqSupportSet.ApiBase) new FlowNode_ReqSupportSet.Api_SupportSet(this, this.m_TargetWindow.GetSupportUnitData());
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
      this.m_Api = (FlowNode_ReqSupportSet.ApiBase) null;
    }

    public class ApiBase
    {
      protected FlowNode_ReqSupportSet m_Node;
      protected RequestAPI m_Request;

      public ApiBase(FlowNode_ReqSupportSet node) => this.m_Node = node;

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

    public class Api_SupportSet : FlowNode_ReqSupportSet.ApiBase
    {
      private SupportSettingRootWindow.OwnSupportData[] m_SupportData;

      public Api_SupportSet(
        FlowNode_ReqSupportSet node,
        SupportSettingRootWindow.OwnSupportData[] ownSupportData)
        : base(node)
      {
        this.m_SupportData = ownSupportData;
      }

      public override string url => "support/set";

      public override string req
      {
        get
        {
          StringBuilder stringBuilder = new StringBuilder(128);
          stringBuilder.Append("\"units\":[");
          for (int index = 0; index < this.m_SupportData.Length; ++index)
          {
            if (this.m_SupportData[index] != null)
            {
              if (index != 0)
                stringBuilder.Append(",");
              stringBuilder.Append("{");
              stringBuilder.Append("\"id\":");
              stringBuilder.Append(this.m_SupportData[index].m_UniqueID);
              stringBuilder.Append(",\"elem\":");
              stringBuilder.Append((int) this.m_SupportData[index].m_Element);
              stringBuilder.Append("}");
            }
          }
          stringBuilder.Append("]");
          return stringBuilder.ToString();
        }
      }

      public override void Success()
      {
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Node, (UnityEngine.Object) null))
          return;
        this.m_Node.ActivateOutputLinks(110);
        ((Behaviour) this.m_Node).enabled = false;
      }

      public override void Failed()
      {
        Network.RemoveAPI();
        Network.ResetError();
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Node, (UnityEngine.Object) null))
          return;
        this.m_Node.ActivateOutputLinks(120);
        ((Behaviour) this.m_Node).enabled = false;
      }

      public override void Complete(WWWResult www)
      {
        if (Network.IsError)
        {
          this.Failed();
        }
        else
        {
          WebAPI.JSON_BodyResponse<FlowNode_ReqSupportSet.Api_SupportSet.ResponseSupportSet> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_ReqSupportSet.Api_SupportSet.ResponseSupportSet>>(www.text);
          DebugUtility.Assert(jsonObject != null, "res == null");
          Network.RemoveAPI();
          try
          {
            if (jsonObject != null)
            {
              if (jsonObject.body != null)
                MonoSingleton<GameManager>.Instance.Player.Deserialize(jsonObject.body.party_decks);
            }
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
            return;
          }
          for (int index = 0; index < this.m_SupportData.Length; ++index)
          {
            if (this.m_SupportData[index] != null && this.m_SupportData[index].m_Element == EElement.None)
            {
              GlobalVars.SelectedSupportUnitUniqueID.Set(this.m_SupportData[index].m_UniqueID);
              break;
            }
          }
          this.Success();
        }
      }

      [Serializable]
      public class ResponseSupportSet
      {
        public JSON_PartyOverWrite[] party_decks;
      }
    }
  }
}
