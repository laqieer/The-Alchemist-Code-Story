// Decompiled with JetBrains decompiler
// Type: FlowNode_OpenURL
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using SRPG;
using System;
using UnityEngine;

[FlowNode.NodeType("Common/OpenURL", 58751)]
[FlowNode.Pin(1, "Input", FlowNode.PinTypes.Input, 1)]
[FlowNode.Pin(2, "Output", FlowNode.PinTypes.Output, 2)]
public class FlowNode_OpenURL : FlowNode
{
  [SerializeField]
  private FlowNode_OpenURL.URL_Mode URLMode = FlowNode_OpenURL.URL_Mode.NewsHost;
  [SerializeField]
  [HideInInspector]
  private string URL;
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
      str = SRPG.Network.NewsHost;
    else if (this.URLMode == FlowNode_OpenURL.URL_Mode.SiteHost)
      str = SRPG.Network.SiteHost;
    else if (this.URLMode == FlowNode_OpenURL.URL_Mode.DLHost)
      str = SRPG.Network.DLHost;
    else if (this.URLMode == FlowNode_OpenURL.URL_Mode.APIHost)
      str = SRPG.Network.Host;
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
