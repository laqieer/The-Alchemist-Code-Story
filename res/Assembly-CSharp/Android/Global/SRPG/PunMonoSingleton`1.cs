﻿// Decompiled with JetBrains decompiler
// Type: SRPG.PunMonoSingleton`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using Photon;
using UnityEngine;

namespace SRPG
{
  public abstract class PunMonoSingleton<T> : PunBehaviour where T : PunBehaviour
  {
    private static T instance_;

    public static T Instance
    {
      get
      {
        if ((UnityEngine.Object) PunMonoSingleton<T>.instance_ == (UnityEngine.Object) null)
        {
          System.Type type = typeof (T);
          PunMonoSingleton<T>.instance_ = UnityEngine.Object.FindObjectOfType(type) as T;
          if ((UnityEngine.Object) PunMonoSingleton<T>.instance_ == (UnityEngine.Object) null)
          {
            GameObject gameObject = new GameObject(type.ToString(), new System.Type[1]{ type });
            if (!((UnityEngine.Object) gameObject == (UnityEngine.Object) null))
              ;
            gameObject.transform.name = type.Name;
            PunMonoSingleton<T>.instance_ = gameObject.GetComponent<T>();
            if ((UnityEngine.Object) PunMonoSingleton<T>.instance_ == (UnityEngine.Object) null)
              UnityEngine.Object.Destroy((UnityEngine.Object) gameObject);
          }
        }
        return PunMonoSingleton<T>.instance_;
      }
    }

    private void Awake()
    {
      if ((UnityEngine.Object) PunMonoSingleton<T>.Instance != (UnityEngine.Object) this)
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
      this.Release();
      PunMonoSingleton<T>.instance_ = (T) null;
    }

    private void OnApplicationQuit()
    {
      this.Release();
      PunMonoSingleton<T>.instance_ = (T) null;
    }
  }
}
