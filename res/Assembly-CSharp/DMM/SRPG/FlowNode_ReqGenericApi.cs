// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqGenericApi
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/WebApi/Generic", 32741)]
  [FlowNode.Pin(0, "Request Only(API呼び出しに成功すると常にTrueを出力)", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "==", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(101, "!=", FlowNode.PinTypes.Input, 101)]
  [FlowNode.Pin(102, "<", FlowNode.PinTypes.Input, 102)]
  [FlowNode.Pin(103, "<=", FlowNode.PinTypes.Input, 103)]
  [FlowNode.Pin(104, ">", FlowNode.PinTypes.Input, 104)]
  [FlowNode.Pin(105, ">=", FlowNode.PinTypes.Input, 105)]
  [FlowNode.Pin(1000, "True", FlowNode.PinTypes.Output, 1000)]
  [FlowNode.Pin(1001, "False", FlowNode.PinTypes.Output, 1001)]
  public class FlowNode_ReqGenericApi : FlowNode_Network
  {
    private const int PIN_IN_REQUEST_ONLY = 0;
    private const int PIN_IN_EQUAL = 100;
    private const int PIN_IN_NOT_EQUAL = 101;
    private const int PIN_IN_LESS_THAN = 102;
    private const int PIN_IN_LESS_THAN_OR_EQUAL = 103;
    private const int PIN_IN_GREATER_THAN = 104;
    private const int PIN_IN_GREATER_THAN_OR_EQUAL = 105;
    private const int PIN_OUT_TRUE = 1000;
    private const int PIN_OUT_FALSE = 1001;
    [SerializeField]
    private string ApiName;
    [SerializeField]
    private FlowNode_ReqGenericApi.CompareTarget Compare_Target;
    [SerializeField]
    private string Text;
    [SerializeField]
    private int Number;
    [SerializeField]
    private string Time = "2018/01/01 15:00:00";
    private int mPinId;

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 0:
        case 100:
        case 101:
        case 102:
        case 103:
        case 104:
        case 105:
          if (string.IsNullOrEmpty(this.ApiName))
          {
            DebugUtility.LogError("ApiNameが空です");
            break;
          }
          this.mPinId = pinID;
          this.ExecRequest((WebAPI) new ReqGeneric(this.ApiName, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          break;
        default:
          DebugUtility.LogError("[" + (object) pinID + "] は不正なInputです");
          break;
      }
    }

    public override void OnSuccess(WWWResult www)
    {
      WebAPI.JSON_BodyResponse<FlowNode_ReqGenericApi.JSON_Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_ReqGenericApi.JSON_Response>>(www.text);
      if (jsonObject.body == null || Network.IsError)
      {
        this.OnRetry();
      }
      else
      {
        Network.RemoveAPI();
        this.Compare(jsonObject.body);
      }
    }

    private void Compare(FlowNode_ReqGenericApi.JSON_Response res)
    {
      if (this.mPinId == 0)
      {
        this.ActivateOutputLinks(1000);
      }
      else
      {
        bool flag = false;
        if (this.Compare_Target == FlowNode_ReqGenericApi.CompareTarget.Text)
        {
          switch (this.mPinId)
          {
            case 100:
              flag = this.Text == res.result.text;
              break;
            case 101:
              flag = this.Text != res.result.text;
              break;
            default:
              DebugUtility.LogError("文字列 \"" + this.Text + "\" に対して == と != 以外の比較を行なおうとしています");
              return;
          }
        }
        else if (this.Compare_Target == FlowNode_ReqGenericApi.CompareTarget.Number)
        {
          switch (this.mPinId)
          {
            case 100:
              flag = this.Number == res.result.number;
              break;
            case 101:
              flag = this.Number != res.result.number;
              break;
            case 102:
              flag = this.Number < res.result.number;
              break;
            case 103:
              flag = this.Number <= res.result.number;
              break;
            case 104:
              flag = this.Number > res.result.number;
              break;
            case 105:
              flag = this.Number >= res.result.number;
              break;
          }
        }
        else if (this.Compare_Target == FlowNode_ReqGenericApi.CompareTarget.Time)
        {
          DateTime result;
          if (!DateTime.TryParse(this.Time, out result))
          {
            this.ActivateOutputLinks(1001);
            return;
          }
          long num = TimeManager.FromDateTime(result);
          switch (this.mPinId)
          {
            case 100:
              flag = num == res.result.time;
              break;
            case 101:
              flag = num != res.result.time;
              break;
            case 102:
              flag = num < res.result.time;
              break;
            case 103:
              flag = num <= res.result.time;
              break;
            case 104:
              flag = num > res.result.time;
              break;
            case 105:
              flag = num >= res.result.time;
              break;
          }
        }
        if (flag)
          this.ActivateOutputLinks(1000);
        else
          this.ActivateOutputLinks(1001);
      }
    }

    public enum CompareTarget
    {
      Text,
      Number,
      Time,
    }

    private class JSON_Response
    {
      public FlowNode_ReqGenericApi.JSON_ResponseContents result;
    }

    private class JSON_ResponseContents
    {
      public string text;
      public int number;
      public long time;
    }
  }
}
