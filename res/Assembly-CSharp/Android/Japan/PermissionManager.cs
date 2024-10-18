// Decompiled with JetBrains decompiler
// Type: PermissionManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;

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

  public bool hasPermission(ePermissionTypes permissionType)
  {
    return true;
  }

  public void requestPermission(ePermissionTypes permissionType, PermissionManager.OnRequestPermissionResult callback)
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
    if (permissionType == ePermissionTypes.LOCATION)
      return "android.permission.ACCESS_FINE_LOCATION";
    return string.Empty;
  }

  public static string GetPermissionNameText(ePermissionTypes permissionType)
  {
    if (permissionType == ePermissionTypes.WRITE_EXTERNAL_STORAGE)
      return LocalizedText.Get("embed.PERMISSION_NAME_WRITE_EXTERNAL_STORAGE");
    if (permissionType == ePermissionTypes.LOCATION)
      return LocalizedText.Get("embed.PERMISSION_NAME_LOCATION");
    return string.Empty;
  }

  public static bool GetPermissionTypeFromID(string permissionID, ref ePermissionTypes permissionType)
  {
    if (permissionID != null)
    {
      if (!(permissionID == "android.permission.ACCESS_FINE_LOCATION"))
      {
        if (permissionID == "android.permission.WRITE_EXTERNAL_STORAGE")
        {
          permissionType = ePermissionTypes.WRITE_EXTERNAL_STORAGE;
          return true;
        }
      }
      else
      {
        permissionType = ePermissionTypes.LOCATION;
        return true;
      }
    }
    return false;
  }

  public delegate void OnRequestPermissionResult(PermissionResultData permissionResultData);
}
