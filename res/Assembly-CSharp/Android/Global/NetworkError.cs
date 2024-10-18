// Decompiled with JetBrains decompiler
// Type: NetworkError
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

public class NetworkError : MonoSingleton<NetworkError>
{
  protected override void Initialize()
  {
    Object.DontDestroyOnLoad((Object) this);
    Object.DontDestroyOnLoad(Object.Instantiate(Resources.Load("NetworkErrorHandler"), Vector3.zero, Quaternion.identity));
    Debug.Log((object) "NetworkError Initialized");
  }

  protected override void Release()
  {
  }
}
