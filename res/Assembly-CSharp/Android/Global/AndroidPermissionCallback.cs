// Decompiled with JetBrains decompiler
// Type: AndroidPermissionCallback
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;

public class AndroidPermissionCallback : AndroidJavaProxy
{
  public AndroidPermissionCallback(Action<string> onGrantedCallback, Action<string> onDeniedCallback)
    : base("com.unity3d.plugin.UnityAndroidPermissions$IPermissionRequestResult")
  {
    if (onGrantedCallback != null)
      this.OnPermissionGrantedAction += onGrantedCallback;
    if (onDeniedCallback == null)
      return;
    this.OnPermissionDeniedAction += onDeniedCallback;
  }

  private event Action<string> OnPermissionGrantedAction;

  private event Action<string> OnPermissionDeniedAction;

  public virtual void OnPermissionGranted(string permissionName)
  {
    if (this.OnPermissionGrantedAction == null)
      return;
    this.OnPermissionGrantedAction(permissionName);
  }

  public virtual void OnPermissionDenied(string permissionName)
  {
    if (this.OnPermissionDeniedAction == null)
      return;
    this.OnPermissionDeniedAction(permissionName);
  }
}
