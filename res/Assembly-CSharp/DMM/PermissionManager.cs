// Decompiled with JetBrains decompiler
// Type: PermissionManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;

#nullable disable
public class PermissionManager : MonoSingleton<PermissionManager>
{
  public const string PERMISSION_ID_WRITE_EXTERNAL_STORAGE = "android.permission.WRITE_EXTERNAL_STORAGE";
  public const string PERMISSION_ID_LOCATION = "android.permission.ACCESS_FINE_LOCATION";
  public const string PERMISSION_NAME_WRITE_EXTERNAL_STORAGE = "embed.PERMISSION_NAME_WRITE_EXTERNAL_STORAGE";
  public const string PERMISSION_NAME_LOCATION = "embed.PERMISSION_NAME_LOCATION";
  private PermissionManager.OnRequestPermissionResult onRequestPermissionResult;
  private IPermissionListener m_Listener;

  private void Awake()
  {
  }

  private void OnApplicationPause(bool pause)
  {
    if (pause || this.m_Listener == null || !this.m_Listener.IsReceivedPermissionResult())
      return;
    this.onRequestPermissionResult(this.m_Listener.GetPermissionResultData());
    this.onRequestPermissionResult = (PermissionManager.OnRequestPermissionResult) null;
    this.m_Listener.ResetState();
  }

  public bool hasPermission(ePermissionTypes permissionType) => true;

  public void requestPermission(
    ePermissionTypes permissionType,
    PermissionManager.OnRequestPermissionResult callback)
  {
  }

  public void openApplicationConfig()
  {
  }

  private static void setListener(IPermissionListener listener)
  {
  }

  public static string GetPermissionID(ePermissionTypes permissionType)
  {
    if (permissionType == ePermissionTypes.WRITE_EXTERNAL_STORAGE)
      return "android.permission.WRITE_EXTERNAL_STORAGE";
    return permissionType == ePermissionTypes.LOCATION ? "android.permission.ACCESS_FINE_LOCATION" : string.Empty;
  }

  public static string GetPermissionNameText(ePermissionTypes permissionType)
  {
    if (permissionType == ePermissionTypes.WRITE_EXTERNAL_STORAGE)
      return LocalizedText.Get("embed.PERMISSION_NAME_WRITE_EXTERNAL_STORAGE");
    return permissionType == ePermissionTypes.LOCATION ? LocalizedText.Get("embed.PERMISSION_NAME_LOCATION") : string.Empty;
  }

  public static bool GetPermissionTypeFromID(
    string permissionID,
    ref ePermissionTypes permissionType)
  {
    switch (permissionID)
    {
      case "android.permission.ACCESS_FINE_LOCATION":
        permissionType = ePermissionTypes.LOCATION;
        return true;
      case "android.permission.WRITE_EXTERNAL_STORAGE":
        permissionType = ePermissionTypes.WRITE_EXTERNAL_STORAGE;
        return true;
      default:
        return false;
    }
  }

  public delegate void OnRequestPermissionResult(PermissionResultData permissionResultData);
}
