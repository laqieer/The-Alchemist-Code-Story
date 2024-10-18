// Decompiled with JetBrains decompiler
// Type: AndroidPermissionsManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class AndroidPermissionsManager
{
  private static AndroidJavaObject m_Activity;
  private static AndroidJavaObject m_PermissionService;

  private static AndroidJavaObject GetActivity()
  {
    if (AndroidPermissionsManager.m_Activity == null)
      AndroidPermissionsManager.m_Activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
    return AndroidPermissionsManager.m_Activity;
  }

  private static AndroidJavaObject GetPermissionsService()
  {
    return AndroidPermissionsManager.m_PermissionService ?? (AndroidPermissionsManager.m_PermissionService = new AndroidJavaObject("com.unity3d.plugin.UnityAndroidPermissions", new object[0]));
  }

  public static bool IsPermissionGranted(string permissionName)
  {
    return AndroidPermissionsManager.GetPermissionsService().Call<bool>(nameof (IsPermissionGranted), (object) AndroidPermissionsManager.GetActivity(), (object) permissionName);
  }

  public static void RequestPermission(string[] permissionNames, AndroidPermissionCallback callback)
  {
    AndroidPermissionsManager.GetPermissionsService().Call("RequestPermissionAsync", (object) AndroidPermissionsManager.GetActivity(), (object) permissionNames, (object) callback);
  }
}
