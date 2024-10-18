// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.ListenerSupport
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;

namespace Gsc.Purchase
{
  public static class ListenerSupport
  {
    private static bool IsAliveMethod(bool enabledInactiveCallback, Delegate method)
    {
      return ListenerSupport.IsAliveObject<object>(enabledInactiveCallback, method.Target);
    }

    public static bool IsAliveObject<T>(bool enabledInactiveCallback, T obj)
    {
      if ((object) obj == null)
        return false;
      if (!((object) obj is MonoBehaviour))
        return true;
      MonoBehaviour monoBehaviour = (object) obj as MonoBehaviour;
      if ((UnityEngine.Object) monoBehaviour == (UnityEngine.Object) null || !monoBehaviour.enabled)
        return false;
      if (!enabledInactiveCallback)
        return monoBehaviour.gameObject.activeInHierarchy;
      return true;
    }

    public static bool Call(bool enabledInactiveCallback, Action method)
    {
      if (!ListenerSupport.IsAliveMethod(enabledInactiveCallback, (Delegate) method))
        return false;
      method();
      return true;
    }

    public static bool Call<T1>(bool enabledInactiveCallback, Action<T1> method, T1 arg1)
    {
      if (!ListenerSupport.IsAliveMethod(enabledInactiveCallback, (Delegate) method))
        return false;
      method(arg1);
      return true;
    }

    public static bool Call<T1, T2>(bool enabledInactiveCallback, Action<T1, T2> method, T1 arg1, T2 arg2)
    {
      if (!ListenerSupport.IsAliveMethod(enabledInactiveCallback, (Delegate) method))
        return false;
      method(arg1, arg2);
      return true;
    }

    public static bool CallResult(bool enabledInactiveCallback, IPurchaseResultListener listener, ResultCode resultCode, FulfillmentResult result)
    {
      switch (resultCode)
      {
        case ResultCode.Succeeded:
          return ListenerSupport.Call<FulfillmentResult>(enabledInactiveCallback, new Action<FulfillmentResult>(listener.OnPurchaseSucceeded), result);
        case ResultCode.Canceled:
          return ListenerSupport.Call(enabledInactiveCallback, new Action(listener.OnPurchaseCanceled));
        case ResultCode.AlreadyOwned:
          return ListenerSupport.Call(enabledInactiveCallback, new Action(listener.OnPurchaseAlreadyOwned));
        case ResultCode.Deferred:
          return ListenerSupport.Call(enabledInactiveCallback, new Action(listener.OnPurchaseDeferred));
        case ResultCode.OverCreditLimit:
          return ListenerSupport.Call(enabledInactiveCallback, new Action(listener.OnOverCreditLimited));
        case ResultCode.InsufficientBalances:
          return ListenerSupport.Call(enabledInactiveCallback, new Action(listener.OnInsufficientBalances));
        default:
          return ListenerSupport.Call(enabledInactiveCallback, new Action(listener.OnPurchaseFailed));
      }
    }
  }
}
