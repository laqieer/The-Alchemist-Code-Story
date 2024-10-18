// Decompiled with JetBrains decompiler
// Type: UrlScheme
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

public class UrlScheme : MonoSingleton<UrlScheme>
{
  public string ParamString { get; set; }

  public bool IsLaunch { get; set; }

  protected override void Initialize()
  {
    Object.DontDestroyOnLoad((Object) this);
    Object.DontDestroyOnLoad(Object.Instantiate(Resources.Load("UrlSchemeObserver"), Vector3.zero, Quaternion.identity));
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
