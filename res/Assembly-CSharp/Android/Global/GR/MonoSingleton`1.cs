// Decompiled with JetBrains decompiler
// Type: GR.MonoSingleton`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
        if ((UnityEngine.Object) MonoSingleton<T>.instance_ == (UnityEngine.Object) null)
        {
          if (MonoSingleton<T>.mDestroyedBySystem)
          {
            Debug.LogError((object) "mDestroyedBySystem is set");
            return (T) null;
          }
          System.Type type = typeof (T);
          MonoSingleton<T>.instance_ = UnityEngine.Object.FindObjectOfType(type) as T;
          if ((UnityEngine.Object) MonoSingleton<T>.instance_ == (UnityEngine.Object) null)
          {
            GameObject gameObject = new GameObject(type.ToString(), new System.Type[1]{ type });
            if ((UnityEngine.Object) gameObject == (UnityEngine.Object) null)
              Debug.LogError((object) (type.ToString() + "instance is nothing."));
            gameObject.transform.name = type.Name;
            MonoSingleton<T>.instance_ = gameObject.GetComponent<T>();
            if ((UnityEngine.Object) MonoSingleton<T>.instance_ == (UnityEngine.Object) null)
            {
              Debug.LogError((object) (type.ToString() + "component is nothing."));
              UnityEngine.Object.Destroy((UnityEngine.Object) gameObject);
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
      if ((UnityEngine.Object) MonoSingleton<T>.instance_ != (UnityEngine.Object) null)
        return MonoSingleton<T>.instance_;
      return (T) null;
    }

    private void Awake()
    {
      if ((UnityEngine.Object) MonoSingleton<T>.Instance != (UnityEngine.Object) this)
        UnityEngine.Object.Destroy((UnityEngine.Object) this);
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
