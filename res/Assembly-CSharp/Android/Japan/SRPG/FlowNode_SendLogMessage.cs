﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SendLogMessage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using Gsc.Auth;
using LogKit;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.NodeType("SendLogKit/SendMessage", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "OutPut", FlowNode.PinTypes.Output, 10)]
  public class FlowNode_SendLogMessage : FlowNode
  {
    [SerializeField]
    private string mLoggerTitle = "application";
    [SerializeField]
    private string mTag = "login";
    [SerializeField]
    private LogLevel mLogLevel = LogLevel.Info;
    [SerializeField]
    private string mMessage = string.Empty;
    [SerializeField]
    private bool bDevieceID = true;
    [SerializeField]
    private bool bUserAgent = true;
    private const string BaseLoggerTitle = "application";
    private const LogLevel BaseLogLevel = LogLevel.Info;
    [SerializeField]
    private bool bOsname;
    [SerializeField]
    private bool bOkyakusamaCode;
    [SerializeField]
    private Image mImage;
    [SerializeField]
    private Text mText;

    public override void OnActivate(int pinID)
    {
      if (pinID == 0)
      {
        this.enabled = false;
        FlowNode_SendLogMessage.SendLogGenerator sendLogGenerator = new FlowNode_SendLogMessage.SendLogGenerator();
        sendLogGenerator.AddCommon(this.bDevieceID, this.bOsname, this.bOkyakusamaCode, this.bUserAgent);
        sendLogGenerator.Add("msg", this.mMessage);
        sendLogGenerator.AddOriginal(this.mImage, this.mText);
        LogKit.Logger.CreateLogger(this.mLoggerTitle).Post(this.mTag, this.mLogLevel, sendLogGenerator.GetSendMessage());
      }
      this.ActivateOutputLinks(10);
    }

    public static void SendLogMessage(FlowNode_SendLogMessage.SendLogGenerator dict, string tag)
    {
      LogKit.Logger.CreateLogger("application").Post(tag, LogLevel.Info, dict.GetSendMessage());
    }

    public static bool IsSetup()
    {
      return !string.IsNullOrEmpty(MonoSingleton<GameManager>.Instance.DeviceId);
    }

    public static void SceneChangeEvent(string category, string before, string after)
    {
      if (!FlowNode_SendLogMessage.IsSetup())
        return;
      FlowNode_SendLogMessage.SendLogGenerator sendLogGenerator = new FlowNode_SendLogMessage.SendLogGenerator();
      sendLogGenerator.AddCommon(true, false, false, true);
      sendLogGenerator.Add(nameof (before), before);
      sendLogGenerator.Add(nameof (after), after);
      LogKit.Logger.CreateLogger("application").Post(category, LogLevel.Info, sendLogGenerator.GetSendMessage());
    }

    public static void TrackEvent(string category, string name)
    {
      if (!FlowNode_SendLogMessage.IsSetup())
        return;
      FlowNode_SendLogMessage.SendLogGenerator sendLogGenerator = new FlowNode_SendLogMessage.SendLogGenerator();
      sendLogGenerator.Add(nameof (category), category);
      sendLogGenerator.Add(nameof (name), name);
      sendLogGenerator.AddCommon(true, false, false, true);
      LogKit.Logger.CreateLogger("application").Post(category, LogLevel.Info, sendLogGenerator.GetSendMessage());
    }

    public static void TrackEvent(string category, string name, int value)
    {
      if (!FlowNode_SendLogMessage.IsSetup())
        return;
      FlowNode_SendLogMessage.SendLogGenerator sendLogGenerator = new FlowNode_SendLogMessage.SendLogGenerator();
      sendLogGenerator.Add(nameof (category), category);
      sendLogGenerator.Add(nameof (name), name);
      sendLogGenerator.Add(nameof (value), value.ToString());
      sendLogGenerator.AddCommon(true, false, false, true);
      LogKit.Logger.CreateLogger("application").Post(category, LogLevel.Info, sendLogGenerator.GetSendMessage());
    }

    public static void TrackPurchase(string productId, string currency, double price = 0.0)
    {
      if (!FlowNode_SendLogMessage.IsSetup())
        return;
      FlowNode_SendLogMessage.SendLogGenerator sendLogGenerator = new FlowNode_SendLogMessage.SendLogGenerator();
      sendLogGenerator.Add(nameof (productId), productId);
      sendLogGenerator.Add(nameof (currency), currency);
      sendLogGenerator.Add(nameof (price), price.ToString());
      sendLogGenerator.AddCommon(true, false, false, true);
      LogKit.Logger.CreateLogger("application").Post("purchase", LogLevel.Info, sendLogGenerator.GetSendMessage());
    }

    public static void TrackSpend(string category, string name, int value)
    {
      if (!FlowNode_SendLogMessage.IsSetup())
        return;
      FlowNode_SendLogMessage.SendLogGenerator sendLogGenerator = new FlowNode_SendLogMessage.SendLogGenerator();
      sendLogGenerator.Add(nameof (category), category);
      sendLogGenerator.Add(nameof (name), name);
      sendLogGenerator.Add(nameof (value), value.ToString());
      sendLogGenerator.AddCommon(true, false, false, true);
      LogKit.Logger.CreateLogger("application").Post(category, LogLevel.Info, sendLogGenerator.GetSendMessage());
    }

    public class SendLogGenerator
    {
      private Dictionary<string, string> dict = new Dictionary<string, string>();

      public void Add(string tag, string msg)
      {
        if (string.IsNullOrEmpty(msg))
          return;
        this.dict.Add("\"" + tag + "\"", "\"" + msg + "\"");
      }

      public virtual void AddCommon(bool b_deviece_id, bool b_osname, bool b_okyakusama_code, bool b_user_agent)
      {
        if (b_deviece_id && !string.IsNullOrEmpty(MonoSingleton<GameManager>.Instance.DeviceId))
          this.dict.Add("\"DeviceID\"", "\"" + MonoSingleton<GameManager>.Instance.DeviceId + "\"");
        if (b_osname && !string.IsNullOrEmpty("windows"))
          this.dict.Add("\"OSNAME\"", "\"windows\"");
        if (b_okyakusama_code && !string.IsNullOrEmpty(GameUtility.Config_OkyakusamaCode))
          this.dict.Add("\"OkyakusamaCode\"", "\"" + GameUtility.Config_OkyakusamaCode + "\"");
        if (!b_user_agent || Session.DefaultSession == null || string.IsNullOrEmpty(Session.DefaultSession.UserAgent))
          return;
        this.dict.Add("\"UserAgent\"", Session.DefaultSession.UserAgent);
      }

      public void AddOriginal(Image image, Text txt)
      {
        if ((UnityEngine.Object) image != (UnityEngine.Object) null && (UnityEngine.Object) image.sprite != (UnityEngine.Object) null)
          this.dict.Add("\"image\"", "\"" + image.sprite.name + "\"");
        if (!((UnityEngine.Object) txt != (UnityEngine.Object) null))
          return;
        this.dict.Add("\"text\"", "\"" + txt.text + "\"");
      }

      public string GetSendMessage()
      {
        string str = string.Empty + "{";
        foreach (KeyValuePair<string, string> keyValuePair in this.dict)
        {
          str = str + keyValuePair.Key + ":";
          str = str + keyValuePair.Value + ",";
        }
        if (str.EndsWith(","))
          str = str.Substring(0, str.Length - 1);
        return str + "}";
      }
    }
  }
}