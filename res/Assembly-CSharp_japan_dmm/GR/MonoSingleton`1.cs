// Decompiled with JetBrains decompiler
// Type: GR.MonoSingleton`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using UnityEngine;

#nullable disable
namespace GR
{
  public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
  {
    private static T instance_;
    private static bool mIsShuttingDown;
    private static bool mDestroyedBySystem;

    public static T Instance
    {
      get
      {
        if (Object.op_Equality((Object) (object) MonoSingleton<T>.instance_, (Object) null))
        {
          if (MonoSingleton<T>.mDestroyedBySystem)
          {
            Debug.LogError((object) "mDestroyedBySystem is set");
            return (T) null;
          }
          Type type = typeof (T);
          MonoSingleton<T>.instance_ = Object.FindObjectOfType(type) as T;
          if (Object.op_Equality((Object) (object) MonoSingleton<T>.instance_, (Object) null))
          {
            GameObject gameObject = new GameObject(type.ToString(), new Type[1]
            {
              type
            });
            if (Object.op_Equality((Object) gameObject, (Object) null))
              Debug.LogError((object) (type.ToString() + "instance is nothing."));
            ((Object) gameObject.transform).name = type.Name;
            MonoSingleton<T>.instance_ = gameObject.GetComponent<T>();
            if (Object.op_Equality((Object) (object) MonoSingleton<T>.instance_, (Object) null))
            {
              Debug.LogError((object) (type.ToString() + "component is nothing."));
              Object.Destroy((Object) gameObject);
            }
          }
        }
        return MonoSingleton<T>.instance_;
      }
    }

    public static T GetInstanceDirect() => MonoSingleton<T>.instance_;

    public static T GetInstanceIfInitialized()
    {
      return Object.op_Inequality((Object) (object) MonoSingleton<T>.instance_, (Object) null) ? MonoSingleton<T>.instance_ : (T) null;
    }

    private void Awake()
    {
      if (Object.op_Inequality((Object) (object) MonoSingleton<T>.Instance, (Object) this))
        Object.Destroy((Object) this);
      else
        this.Initialize();
    }

    protected virtual void Initialize()
    {
    }

    protected virtual void Release()
    {
    }

    private void OnDestroy()
    {
      if (MonoSingleton<T>.mIsShuttingDown)
        MonoSingleton<T>.mDestroyedBySystem = true;
      this.Release();
      MonoSingleton<T>.instance_ = (T) null;
    }

    private void OnApplicationQuit()
    {
      MonoSingleton<T>.mIsShuttingDown = true;
      this.Release();
      MonoSingleton<T>.instance_ = (T) null;
    }

    public void Ensure()
    {
    }
  }
}
