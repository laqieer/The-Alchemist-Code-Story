// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_AndroidPermissions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Granted", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(2, "Not Granted", FlowNode.PinTypes.Output, 2)]
  [FlowNode.NodeType("System/Android Permission", 58751)]
  [FlowNode.Pin(0, "Start", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_AndroidPermissions : FlowNode
  {
    private const string STORAGE_PERMISSION = "android.permission.WRITE_EXTERNAL_STORAGE";
    private int permissionRequest;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (!this.CheckPermissions())
        this.RequestPermissions();
      else
        this.ActivateOutputLinks(1);
    }

    private bool CheckPermissions()
    {
      if (Application.platform != RuntimePlatform.Android)
        return true;
      return AndroidPermissionsManager.IsPermissionGranted("android.permission.WRITE_EXTERNAL_STORAGE");
    }

    public void RequestPermissions()
    {
      this.StartCoroutine(this.WaitForCallback());
      AndroidPermissionsManager.RequestPermission(new string[1]
      {
        "android.permission.WRITE_EXTERNAL_STORAGE"
      }, new AndroidPermissionCallback((Action<string>) (grantedPermission => this.permissionRequest = 1), (Action<string>) (deniedPermission => this.permissionRequest = 2)));
    }

    [DebuggerHidden]
    private IEnumerator WaitForCallback()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_AndroidPermissions.\u003CWaitForCallback\u003Ec__Iterator40() { \u003C\u003Ef__this = this };
    }
  }
}
