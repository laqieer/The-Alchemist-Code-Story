// Decompiled with JetBrains decompiler
// Type: UrlScheme
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
public class UrlScheme : MonoSingleton<UrlScheme>
{
  public string ParamString { get; set; }

  public bool IsLaunch { get; set; }

  protected override void Initialize()
  {
    Object.DontDestroyOnLoad((Object) this);
    Object.DontDestroyOnLoad((Object) Object.Instantiate<GameObject>((GameObject) Resources.Load("UrlSchemeObserver"), Vector3.zero, Quaternion.identity));
    DebugUtility.Log("UrlScheme Initialized");
    this.OnApplicationPause(false);
    this.IsLaunch = true;
  }

  protected override void Release()
  {
  }

  private void OnApplicationPause(bool pause)
  {
    if (pause)
      return;
    string str = UrlSchemePlugin.Read();
    if (string.IsNullOrEmpty(str))
      return;
    this.ParamString = str;
    this.IsLaunch = false;
  }
}
