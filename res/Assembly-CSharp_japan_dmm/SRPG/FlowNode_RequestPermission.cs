// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_RequestPermission
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Native/Permission/RequestPermission", 16750080)]
  [FlowNode.Pin(0, "要求", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "許可", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "不許可", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "不許可(今後は表示しない)", FlowNode.PinTypes.Output, 102)]
  public class FlowNode_RequestPermission : FlowNode
  {
    public const int INPUT_REQUEST = 0;
    public const int OUTPUT_APPROVED = 100;
    public const int OUTPUT_REJECTED = 101;
    public const int OUTPUT_REJECTED_HIDE_DIALOG = 102;
    [SerializeField]
    private ePermissionTypes m_RequestPermissionType;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      MonoSingleton<PermissionManager>.Instance.requestPermission(this.m_RequestPermissionType, new PermissionManager.OnRequestPermissionResult(this.OnRequestPermissionResult));
    }

    public void OnRequestPermissionResult(PermissionResultData permissionResultData)
    {
      if (permissionResultData == null)
      {
        DebugUtility.LogError("OnRequestPermissionResult => permissionResultData == null");
        this.ActivateOutputLinks(100);
      }
      else
      {
        bool flag1 = false;
        bool flag2 = false;
        for (int index = 0; index < permissionResultData.ResultCount; ++index)
        {
          PermissionInfo permissionInfo = permissionResultData.GetPermissionInfo(index);
          if (permissionInfo != null)
          {
            if (permissionInfo.IsRejected)
              flag1 = true;
            if (permissionInfo.IsRejected && permissionInfo.IsCheckedHideDialog)
              flag2 = true;
          }
        }
        if (flag1)
        {
          if (flag2)
            this.ActivateOutputLinks(102);
          else
            this.ActivateOutputLinks(101);
        }
        else
          this.ActivateOutputLinks(100);
      }
    }
  }
}
