// Decompiled with JetBrains decompiler
// Type: Gsc.Core.NativeRootObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace Gsc.Core
{
  public class NativeRootObject : MonoBehaviour
  {
    private static NativeRootObject _instance;

    public static NativeRootObject Instance
    {
      get
      {
        if (Object.op_Equality((Object) NativeRootObject._instance, (Object) null))
        {
          GameObject gameObject = new GameObject("GSCC.NativeRootObject");
          ((Object) gameObject).hideFlags = (HideFlags) 61;
          Object.DontDestroyOnLoad((Object) gameObject);
          gameObject.AddComponent<NativeRootObject>();
        }
        return NativeRootObject._instance;
      }
    }

    private void Awake() => NativeRootObject._instance = this;
  }
}
