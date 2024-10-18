// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.AndroidAppStateClient
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GooglePlayGames.BasicApi;
using GooglePlayGames.Native.Cwrapper;
using GooglePlayGames.OurUtils;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GooglePlayGames.Native
{
  internal class AndroidAppStateClient : AppStateClient
  {
    private static AndroidJavaClass AppStateManager = new AndroidJavaClass("com.google.android.gms.appstate.AppStateManager");
    private const int STATUS_OK = 0;
    private const int STATUS_STALE_DATA = 3;
    private const int STATUS_NO_DATA = 4;
    private const int STATUS_KEY_NOT_FOUND = 2002;
    private const int STATUS_CONFLICT = 2000;
    private const string ResultCallbackClassname = "com.google.android.gms.common.api.ResultCallback";
    private readonly GooglePlayGames.Native.PInvoke.GameServices mServices;

    internal AndroidAppStateClient(GooglePlayGames.Native.PInvoke.GameServices services)
    {
      this.mServices = Misc.CheckNotNull<GooglePlayGames.Native.PInvoke.GameServices>(services);
    }

    private static AndroidJavaObject GetApiClient(GooglePlayGames.Native.PInvoke.GameServices services)
    {
      return JavaUtils.JavaObjectFromPointer(InternalHooks.InternalHooks_GetApiClient(services.AsHandle()));
    }

    public void LoadState(int slot, OnStateLoadedListener listener)
    {
      GooglePlayGames.OurUtils.Logger.d("LoadState, slot=" + (object) slot);
      using (AndroidJavaObject apiClient = AndroidAppStateClient.GetApiClient(this.mServices))
        AndroidAppStateClient.CallAppState(apiClient, "load", (AndroidJavaProxy) new AndroidAppStateClient.OnStateResultProxy(this.mServices, listener), (object) slot);
    }

    public void UpdateState(int slot, byte[] data, OnStateLoadedListener listener)
    {
      GooglePlayGames.OurUtils.Logger.d("UpdateState, slot=" + (object) slot);
      using (AndroidJavaObject apiClient = AndroidAppStateClient.GetApiClient(this.mServices))
        AndroidAppStateClient.AppStateManager.CallStatic("update", (object) apiClient, (object) slot, (object) data);
      if (listener == null)
        return;
      PlayGamesHelperObject.RunOnGameThread((Action) (() => listener.OnStateSaved(true, slot)));
    }

    private static object[] PrependApiClient(AndroidJavaObject apiClient, params object[] args)
    {
      List<object> objectList = new List<object>();
      objectList.Add((object) apiClient);
      objectList.AddRange((IEnumerable<object>) args);
      return objectList.ToArray();
    }

    private static void CallAppState(AndroidJavaObject apiClient, string methodName, params object[] args)
    {
      AndroidAppStateClient.AppStateManager.CallStatic(methodName, AndroidAppStateClient.PrependApiClient(apiClient, args));
    }

    private static void CallAppState(AndroidJavaObject apiClient, string methodName, AndroidJavaProxy callbackProxy, params object[] args)
    {
      AndroidJavaObject androidJavaObject = AndroidAppStateClient.AppStateManager.CallStatic<AndroidJavaObject>(methodName, AndroidAppStateClient.PrependApiClient(apiClient, args));
      using (androidJavaObject)
        androidJavaObject.Call("setResultCallback", new object[1]
        {
          (object) callbackProxy
        });
    }

    private static int GetStatusCode(AndroidJavaObject result)
    {
      if (result == null)
        return -1;
      return result.Call<AndroidJavaObject>("getStatus").Call<int>("getStatusCode");
    }

    internal static byte[] ToByteArray(AndroidJavaObject javaByteArray)
    {
      if (javaByteArray == null)
        return (byte[]) null;
      return AndroidJNIHelper.ConvertFromJNIArray<byte[]>(javaByteArray.GetRawObject());
    }

    private class OnStateResultProxy : AndroidJavaProxy
    {
      private readonly GooglePlayGames.Native.PInvoke.GameServices mServices;
      private readonly OnStateLoadedListener mListener;

      internal OnStateResultProxy(GooglePlayGames.Native.PInvoke.GameServices services, OnStateLoadedListener listener)
        : base("com.google.android.gms.common.api.ResultCallback")
      {
        this.mServices = Misc.CheckNotNull<GooglePlayGames.Native.PInvoke.GameServices>(services);
        this.mListener = listener;
      }

      private void OnStateConflict(int stateKey, string resolvedVersion, byte[] localData, byte[] serverData)
      {
        GooglePlayGames.OurUtils.Logger.d("OnStateResultProxy.onStateConflict called, stateKey=" + (object) stateKey + ", resolvedVersion=" + resolvedVersion);
        this.debugLogData(nameof (localData), localData);
        this.debugLogData(nameof (serverData), serverData);
        if (this.mListener != null)
        {
          GooglePlayGames.OurUtils.Logger.d("OnStateResultProxy.onStateConflict invoking conflict callback.");
          PlayGamesHelperObject.RunOnGameThread((Action) (() => this.ResolveState(stateKey, resolvedVersion, this.mListener.OnStateConflict(stateKey, localData, serverData), this.mListener)));
        }
        else
          GooglePlayGames.OurUtils.Logger.w("No conflict callback specified! Cannot resolve cloud save conflict.");
      }

      private void ResolveState(int slot, string resolvedVersion, byte[] resolvedData, OnStateLoadedListener listener)
      {
        GooglePlayGames.OurUtils.Logger.d(string.Format("AndroidClient.ResolveState, slot={0}, ver={1}, data={2}", (object) slot, (object) resolvedVersion, (object) resolvedData));
        using (AndroidJavaObject apiClient = AndroidAppStateClient.GetApiClient(this.mServices))
          AndroidAppStateClient.CallAppState(apiClient, "resolve", (AndroidJavaProxy) new AndroidAppStateClient.OnStateResultProxy(this.mServices, listener), (object) slot, (object) resolvedVersion, (object) resolvedData);
      }

      private void OnStateLoaded(int statusCode, int stateKey, byte[] localData)
      {
        GooglePlayGames.OurUtils.Logger.d("OnStateResultProxy.onStateLoaded called, status " + (object) statusCode + ", stateKey=" + (object) stateKey);
        this.debugLogData(nameof (localData), localData);
        bool success = false;
        int num = statusCode;
        switch (num)
        {
          case 0:
            GooglePlayGames.OurUtils.Logger.d("Status is OK, so success.");
            success = true;
            break;
          case 3:
            GooglePlayGames.OurUtils.Logger.d("Status is STALE DATA, so considering as success.");
            success = true;
            break;
          case 4:
            GooglePlayGames.OurUtils.Logger.d("Status is NO DATA (no network?), so it's a failure.");
            success = false;
            localData = (byte[]) null;
            break;
          default:
            if (num == 2002)
            {
              GooglePlayGames.OurUtils.Logger.d("Status is KEY NOT FOUND, which is a success, but with no data.");
              success = true;
              localData = (byte[]) null;
              break;
            }
            GooglePlayGames.OurUtils.Logger.e("Cloud load failed with status code " + (object) statusCode);
            success = false;
            localData = (byte[]) null;
            break;
        }
        if (this.mListener != null)
        {
          GooglePlayGames.OurUtils.Logger.d("OnStateResultProxy.onStateLoaded invoking load callback.");
          PlayGamesHelperObject.RunOnGameThread((Action) (() => this.mListener.OnStateLoaded(success, stateKey, localData)));
        }
        else
          GooglePlayGames.OurUtils.Logger.w("No load callback specified!");
      }

      private void debugLogData(string tag, byte[] data)
      {
        GooglePlayGames.OurUtils.Logger.d("   " + tag + ": " + GooglePlayGames.OurUtils.Logger.describe(data));
      }

      public void onResult(AndroidJavaObject result)
      {
        GooglePlayGames.OurUtils.Logger.d("OnStateResultProxy.onResult, result=" + (object) result);
        int statusCode = AndroidAppStateClient.GetStatusCode(result);
        GooglePlayGames.OurUtils.Logger.d("OnStateResultProxy: status code is " + (object) statusCode);
        if (result == null)
        {
          GooglePlayGames.OurUtils.Logger.e("OnStateResultProxy: result is null.");
        }
        else
        {
          GooglePlayGames.OurUtils.Logger.d("OnstateResultProxy: retrieving result objects...");
          AndroidJavaObject target1 = result.NullSafeCall("getLoadedResult");
          AndroidJavaObject target2 = result.NullSafeCall("getConflictResult");
          GooglePlayGames.OurUtils.Logger.d("Got result objects.");
          GooglePlayGames.OurUtils.Logger.d("loadedResult = " + (object) target1);
          GooglePlayGames.OurUtils.Logger.d("conflictResult = " + (object) target2);
          if (target2 != null)
          {
            GooglePlayGames.OurUtils.Logger.d("OnStateResultProxy: processing conflict.");
            int stateKey = target2.Call<int>("getStateKey");
            string resolvedVersion = target2.Call<string>("getResolvedVersion");
            byte[] byteArray1 = AndroidAppStateClient.ToByteArray(target2.NullSafeCall("getLocalData"));
            byte[] byteArray2 = AndroidAppStateClient.ToByteArray(target2.NullSafeCall("getServerData"));
            GooglePlayGames.OurUtils.Logger.d("OnStateResultProxy: conflict args parsed, calling.");
            this.OnStateConflict(stateKey, resolvedVersion, byteArray1, byteArray2);
          }
          else if (target1 != null)
          {
            GooglePlayGames.OurUtils.Logger.d("OnStateResultProxy: processing normal load.");
            int stateKey = target1.Call<int>("getStateKey");
            byte[] byteArray = AndroidAppStateClient.ToByteArray(target1.NullSafeCall("getLocalData"));
            GooglePlayGames.OurUtils.Logger.d("OnStateResultProxy: loaded args parsed, calling.");
            this.OnStateLoaded(statusCode, stateKey, byteArray);
          }
          else
            GooglePlayGames.OurUtils.Logger.e("OnStateResultProxy: both loadedResult and conflictResult are null!");
        }
      }
    }
  }
}
