﻿// Decompiled with JetBrains decompiler
// Type: FlowNode_ResetQRAccess
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;

[FlowNode.NodeType("QRCode/ResetQRCodeAccess", 32741)]
[FlowNode.Pin(0, "Reset", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(100, "Finished", FlowNode.PinTypes.Output, 100)]
public class FlowNode_ResetQRAccess : FlowNode
{
  public override void OnActivate(int pinID)
  {
    if (pinID != 0)
      return;
    DebugUtility.Log("Cancel QRCodeAccess");
    MonoSingleton<UrlScheme>.Instance.ParamString = (string) null;
    FlowNode_OnUrlSchemeLaunch.QRCampaignID = -1;
    FlowNode_OnUrlSchemeLaunch.QRSerialID = string.Empty;
    FlowNode_OnUrlSchemeLaunch.IsQRAccess = false;
    this.ActivateOutputLinks(100);
  }
}
