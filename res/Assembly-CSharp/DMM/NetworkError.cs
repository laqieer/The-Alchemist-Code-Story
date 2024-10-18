// Decompiled with JetBrains decompiler
// Type: NetworkError
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
public class NetworkError : MonoSingleton<NetworkError>
{
  protected override void Initialize()
  {
    Object.DontDestroyOnLoad((Object) this);
    Object.DontDestroyOnLoad((Object) Object.Instantiate<GameObject>((GameObject) Resources.Load("NetworkErrorHandler"), Vector3.zero, Quaternion.identity));
    Debug.Log((object) "NetworkError Initialized");
  }

  protected override void Release()
  {
  }
}
