// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_EmbedPermissionErrorWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;

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
      if (pinID == 0)
      {
        EmbedSystemMessageEx.Create(LocalizedText.Get(this.m_MessageTextID, new object[1]
        {
          (object) PermissionManager.GetPermissionNameText(this.m_PermissionType)
        })).AddButton(LocalizedText.Get("embed.CMD_OK"), true, (EmbedSystemMessageEx.SystemMessageEvent) (ok => this.ActivateOutputLinks(100)));
      }
      else
      {
        if (pinID != 10)
          return;
        string permissionNameText = PermissionManager.GetPermissionNameText(this.m_PermissionType);
        EmbedSystemMessageEx.Create(LocalizedText.Get(this.m_MessageTextID, new object[1]
        {
          (object) permissionNameText
        }) + LocalizedText.Get("embed.PERMISSION_NAVIGATION_FORMAT", new object[1]
        {
          (object) permissionNameText
        })).AddButton(LocalizedText.Get("embed.PERMISSION_BTN_OPEN_CONFIG"), true, (EmbedSystemMessageEx.SystemMessageEvent) (ok =>
        {
          MonoSingleton<PermissionManager>.Instance.openApplicationConfig();
          this.ActivateOutputLinks(100);
        }));
      }
    }
  }
}
