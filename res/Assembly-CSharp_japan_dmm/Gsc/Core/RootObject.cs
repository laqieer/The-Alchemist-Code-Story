// Decompiled with JetBrains decompiler
// Type: Gsc.Core.RootObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

#nullable disable
namespace Gsc.Core
{
  public class RootObject : MonoBehaviour
  {
    private static RootObject _instance;

    public static RootObject Instance
    {
      get
      {
        if (Object.op_Equality((Object) RootObject._instance, (Object) null))
        {
          GameObject gameObject = new GameObject("GSCC.RootObject");
          ((Object) gameObject).hideFlags = (HideFlags) 61;
          Object.DontDestroyOnLoad((Object) gameObject);
          RootObject._instance = gameObject.AddComponent<RootObject>();
        }
        return RootObject._instance;
      }
    }

    private void Awake()
    {
      if (!Object.op_Inequality((Object) RootObject._instance, (Object) null))
        return;
      Object.Destroy((Object) ((Component) this).gameObject);
      Debug.LogError((object) "Already instantiated.");
    }

    public static void Initialize()
    {
      if (!Object.op_Inequality((Object) RootObject._instance, (Object) null))
        return;
      Object.Destroy((Object) ((Component) RootObject._instance).gameObject);
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

    public void DelayInvoke<T1, T2, T3>(
      Action<T1, T2, T3> f,
      T1 arg1,
      T2 arg2,
      T3 arg3,
      float seconds)
    {
      this.StartCoroutine(RootObject._DelayInvoke<T1, T2, T3>(f, arg1, arg2, arg3, seconds));
    }

    [DebuggerHidden]
    private static IEnumerator _DelayInvoke(Action f, float seconds)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new RootObject.\u003C_DelayInvoke\u003Ec__Iterator0()
      {
        seconds = seconds,
        f = f
      };
    }

    [DebuggerHidden]
    private static IEnumerator _DelayInvoke<T1>(Action<T1> f, T1 arg1, float seconds)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new RootObject.\u003C_DelayInvoke\u003Ec__Iterator1<T1>()
      {
        seconds = seconds,
        f = f,
        arg1 = arg1
      };
    }

    [DebuggerHidden]
    private static IEnumerator _DelayInvoke<T1, T2>(
      Action<T1, T2> f,
      T1 arg1,
      T2 arg2,
      float seconds)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new RootObject.\u003C_DelayInvoke\u003Ec__Iterator2<T1, T2>()
      {
        seconds = seconds,
        f = f,
        arg1 = arg1,
        arg2 = arg2
      };
    }

    [DebuggerHidden]
    private static IEnumerator _DelayInvoke<T1, T2, T3>(
      Action<T1, T2, T3> f,
      T1 arg1,
      T2 arg2,
      T3 arg3,
      float seconds)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new RootObject.\u003C_DelayInvoke\u003Ec__Iterator3<T1, T2, T3>()
      {
        seconds = seconds,
        f = f,
        arg1 = arg1,
        arg2 = arg2,
        arg3 = arg3
      };
    }
  }
}
