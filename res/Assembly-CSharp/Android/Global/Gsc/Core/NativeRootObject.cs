// Decompiled with JetBrains decompiler
// Type: Gsc.Core.NativeRootObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace Gsc.Core
{
  public class NativeRootObject : MonoBehaviour
  {
    private static NativeRootObject _instance;

    public static NativeRootObject Instance
    {
      get
      {
        if ((Object) NativeRootObject._instance == (Object) null)
        {
          GameObject gameObject = new GameObject("GSCC.NativeRootObject");
          gameObject.hideFlags = HideFlags.HideAndDontSave;
          Object.DontDestroyOnLoad((Object) gameObject);
          gameObject.AddComponent<NativeRootObject>();
        }
        return NativeRootObject._instance;
      }
    }

    private void Awake()
    {
      NativeRootObject._instance = this;
    }
  }
}
