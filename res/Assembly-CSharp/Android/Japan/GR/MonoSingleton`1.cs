// Decompiled with JetBrains decompiler
// Type: GR.MonoSingleton`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

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
        if ((Object) MonoSingleton<T>.instance_ == (Object) null)
        {
          if (MonoSingleton<T>.mDestroyedBySystem)
          {
            Debug.LogError((object) "mDestroyedBySystem is set");
            return (T) null;
          }
          Type type = typeof (T);
          MonoSingleton<T>.instance_ = Object.FindObjectOfType(type) as T;
          if ((Object) MonoSingleton<T>.instance_ == (Object) null)
          {
            GameObject gameObject = new GameObject(type.ToString(), new Type[1]{ type });
            if ((Object) gameObject == (Object) null)
              Debug.LogError((object) (type.ToString() + "instance is nothing."));
            gameObject.transform.name = type.Name;
            MonoSingleton<T>.instance_ = gameObject.GetComponent<T>();
            if ((Object) MonoSingleton<T>.instance_ == (Object) null)
            {
              Debug.LogError((object) (type.ToString() + "component is nothing."));
              Object.Destroy((Object) gameObject);
            }
          }
        }
        return MonoSingleton<T>.instance_;
      }
    }

    public static T GetInstanceDirect()
    {
      return MonoSingleton<T>.instance_;
    }

    public static T GetInstanceIfInitialized()
    {
      if ((Object) MonoSingleton<T>.instance_ != (Object) null)
        return MonoSingleton<T>.instance_;
      return (T) null;
    }

    private void Awake()
    {
      if ((Object) MonoSingleton<T>.Instance != (Object) this)
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
