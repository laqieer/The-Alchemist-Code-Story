// Decompiled with JetBrains decompiler
// Type: Gsc.Core.NativeRootObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
