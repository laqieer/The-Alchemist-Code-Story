// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_EmbedPermissionErrorWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("UI/EmbedPermissionErrorWindow", 32741)]
  [FlowNode.Pin(0, "開く", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "開く(誘導ボタン付)", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "閉じた", FlowNode.PinTypes.Output, 100)]
  public class FlowNode_EmbedPermissionErrorWindow : FlowNode
  {
    private const int INPUT_OPEN = 0;
    private const int INPUT_OPEN_ENABLE_NAVIGATION = 10;
    private const int OUTPUT_CLOSE = 100;
    [SerializeField]
    private string m_MessageTextID;
    [SerializeField]
    private ePermissionTypes m_PermissionType;

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 0:
          string msg = LocalizedText.Get(this.m_MessageTextID, (object) PermissionManager.GetPermissionNameText(this.m_PermissionType));
          string btn_text1 = LocalizedText.Get("embed.CMD_OK");
          EmbedSystemMessageEx.Create(msg).AddButton(btn_text1, true, (EmbedSystemMessageEx.SystemMessageEvent) (ok => this.ActivateOutputLinks(100)), true);
          break;
        case 10:
          string permissionNameText = PermissionManager.GetPermissionNameText(this.m_PermissionType);
          string str1 = LocalizedText.Get(this.m_MessageTextID, (object) permissionNameText);
          string str2 = LocalizedText.Get("embed.PERMISSION_NAVIGATION_FORMAT", (object) permissionNameText);
          string btn_text2 = LocalizedText.Get("embed.PERMISSION_BTN_OPEN_CONFIG");
          string btn_text3 = LocalizedText.Get("embed.PERMISSION_BTN_APP_QUIT");
          EmbedSystemMessageEx embedSystemMessageEx = EmbedSystemMessageEx.Create(str1 + str2);
          embedSystemMessageEx.AddCancelButton(btn_text3, true, (EmbedSystemMessageEx.SystemMessageEvent) (ok => Application.Quit()), true);
          embedSystemMessageEx.AddButton(btn_text2, true, (EmbedSystemMessageEx.SystemMessageEvent) (ok =>
          {
            MonoSingleton<PermissionManager>.Instance.openApplicationConfig();
            this.ActivateOutputLinks(100);
          }));
          break;
      }
    }
  }
}
