// Decompiled with JetBrains decompiler
// Type: Gsc.Core.RootObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace Gsc.Core
{
  public class RootObject : MonoBehaviour
  {
    private static RootObject _instance;

    public static RootObject Instance
    {
      get
      {
        if ((UnityEngine.Object) RootObject._instance == (UnityEngine.Object) null)
        {
          GameObject gameObject = new GameObject("GSCC.RootObject");
          gameObject.hideFlags = HideFlags.HideAndDontSave;
          UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) gameObject);
          RootObject._instance = gameObject.AddComponent<RootObject>();
        }
        return RootObject._instance;
      }
    }

    private void Awake()
    {
      if (!((UnityEngine.Object) RootObject._instance != (UnityEngine.Object) null))
        return;
      UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
      UnityEngine.Debug.LogError((object) "Already instantiated.");
    }

    public static void Initialize()
    {
      if (!((UnityEngine.Object) RootObject._instance != (UnityEngine.Object) null))
        return;
      UnityEngine.Object.Destroy((UnityEngine.Object) RootObject._instance.gameObject);
      RootObject._instance = (RootObject) null;
    }

    public void DelayInvoke(Action f, float seconds)
    {
      this.StartCoroutine(RootObject._DelayInvoke(f, seconds));
    }

    public void DelayInvoke<T1>(Action<T1> f, T1 arg1, float seconds)
    {
      this.StartCoroutine(RootObject._DelayInvoke<T1>(f, arg1, seconds));
    }

    public void DelayInvoke<T1, T2>(Action<T1, T2> f, T1 arg1, T2 arg2, float seconds)
    {
      this.StartCoroutine(RootObject._DelayInvoke<T1, T2>(f, arg1, arg2, seconds));
    }

    public void DelayInvoke<T1, T2, T3>(Action<T1, T2, T3> f, T1 arg1, T2 arg2, T3 arg3, float seconds)
    {
      this.StartCoroutine(RootObject._DelayInvoke<T1, T2, T3>(f, arg1, arg2, arg3, seconds));
    }

    [DebuggerHidden]
    private static IEnumerator _DelayInvoke(Action f, float seconds)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new RootObject.\u003C_DelayInvoke\u003Ec__IteratorF()
      {
        seconds = seconds,
        f = f,
        \u003C\u0024\u003Eseconds = seconds,
        \u003C\u0024\u003Ef = f
      };
    }

    [DebuggerHidden]
    private static IEnumerator _DelayInvoke<T1>(Action<T1> f, T1 arg1, float seconds)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new RootObject.\u003C_DelayInvoke\u003Ec__Iterator10<T1>()
      {
        seconds = seconds,
        f = f,
        arg1 = arg1,
        \u003C\u0024\u003Eseconds = seconds,
        \u003C\u0024\u003Ef = f,
        \u003C\u0024\u003Earg1 = arg1
      };
    }

    [DebuggerHidden]
    private static IEnumerator _DelayInvoke<T1, T2>(Action<T1, T2> f, T1 arg1, T2 arg2, float seconds)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new RootObject.\u003C_DelayInvoke\u003Ec__Iterator11<T1, T2>()
      {
        seconds = seconds,
        f = f,
        arg1 = arg1,
        arg2 = arg2,
        \u003C\u0024\u003Eseconds = seconds,
        \u003C\u0024\u003Ef = f,
        \u003C\u0024\u003Earg1 = arg1,
        \u003C\u0024\u003Earg2 = arg2
      };
    }

    [DebuggerHidden]
    private static IEnumerator _DelayInvoke<T1, T2, T3>(Action<T1, T2, T3> f, T1 arg1, T2 arg2, T3 arg3, float seconds)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new RootObject.\u003C_DelayInvoke\u003Ec__Iterator12<T1, T2, T3>()
      {
        seconds = seconds,
        f = f,
        arg1 = arg1,
        arg2 = arg2,
        arg3 = arg3,
        \u003C\u0024\u003Eseconds = seconds,
        \u003C\u0024\u003Ef = f,
        \u003C\u0024\u003Earg1 = arg1,
        \u003C\u0024\u003Earg2 = arg2,
        \u003C\u0024\u003Earg3 = arg3
      };
    }
  }
}
