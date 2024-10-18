// Decompiled with JetBrains decompiler
// Type: FlowNode_OpenURL
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using SRPG;
using System;
using UnityEngine;

#nullable disable
[FlowNode.NodeType("Common/OpenURL", 58751)]
[FlowNode.Pin(1, "Input", FlowNode.PinTypes.Input, 1)]
[FlowNode.Pin(2, "Output", FlowNode.PinTypes.Output, 2)]
public class FlowNode_OpenURL : FlowNode
{
  [SerializeField]
  [HideInInspector]
  private string URL;
  [SerializeField]
  private FlowNode_OpenURL.URL_Mode URLMode = FlowNode_OpenURL.URL_Mode.NewsHost;
  [SerializeField]
  [HideInInspector]
  private bool UseVariableURL;
  [SerializeField]
  [HideInInspector]
  private bool ResetVariableValue;
  [SerializeField]
  [HideInInspector]
  private string VariableName;

  public override void OnActivate(int pinID)
  {
    if (pinID != 1)
      return;
    this.Open();
  }

  private string BaseURL(FlowNode_OpenURL.URL_Mode urlMode)
  {
    string str = string.Empty;
    if (this.URLMode == FlowNode_OpenURL.URL_Mode.NewsHost)
      str = Network.NewsHost;
    else if (this.URLMode == FlowNode_OpenURL.URL_Mode.SiteHost)
      str = Network.SiteHost;
    else if (this.URLMode == FlowNode_OpenURL.URL_Mode.DLHost)
      str = Network.DLHost;
    else if (this.URLMode == FlowNode_OpenURL.URL_Mode.APIHost)
      str = Network.Host;
    return str;
  }

  private void Open()
  {
    string url = this.URL;
    if (this.UseVariableURL)
      url = FlowNode_Variable.Get(this.VariableName);
    if (this.URLMode == FlowNode_OpenURL.URL_Mode.Direct)
    {
      MonoSingleton<GameManager>.Instance.Player.UpdateViewNewsTrophy(url);
      Application.OpenURL(url);
    }
    else
    {
      Uri uri = new Uri(new Uri(this.BaseURL(this.URLMode)), url);
      MonoSingleton<GameManager>.Instance.Player.UpdateViewNewsTrophy(uri.AbsoluteUri);
      Application.OpenURL(uri.AbsoluteUri);
    }
    if (!this.UseVariableURL || !this.ResetVariableValue)
      return;
    FlowNode_Variable.Set(this.VariableName, string.Empty);
  }

  public enum URL_Mode
  {
    APIHost,
    DLHost,
    SiteHost,
    NewsHost,
    Direct,
  }
}
