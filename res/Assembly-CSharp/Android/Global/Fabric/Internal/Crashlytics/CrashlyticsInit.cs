// Decompiled with JetBrains decompiler
// Type: Fabric.Internal.Crashlytics.CrashlyticsInit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using Fabric.Internal.Runtime;
using System;
using UnityEngine;

namespace Fabric.Internal.Crashlytics
{
  public class CrashlyticsInit : MonoBehaviour
  {
    private static readonly string kitName = "Crashlytics";
    private static CrashlyticsInit instance;

    private void Awake()
    {
      if ((UnityEngine.Object) CrashlyticsInit.instance == (UnityEngine.Object) null)
      {
        this.AwakeOnce();
        CrashlyticsInit.instance = this;
        UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) this);
      }
      else
      {
        if (!((UnityEngine.Object) CrashlyticsInit.instance != (UnityEngine.Object) this))
          return;
        UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
      }
    }

    private void AwakeOnce()
    {
      CrashlyticsInit.RegisterExceptionHandlers();
    }

    private static void RegisterExceptionHandlers()
    {
      if (CrashlyticsInit.IsSDKInitialized())
      {
        Utils.Log(CrashlyticsInit.kitName, "Registering exception handlers");
        AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CrashlyticsInit.HandleException);
        Application.logMessageReceived += new Application.LogCallback(CrashlyticsInit.HandleLog);
      }
      else
        Utils.Log(CrashlyticsInit.kitName, "Did not register exception handlers: Crashlytics SDK was not initialized");
    }

    private static bool IsSDKInitialized()
    {
      AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.crashlytics.android.Crashlytics");
      AndroidJavaObject androidJavaObject;
      try
      {
        androidJavaObject = androidJavaClass.CallStatic<AndroidJavaObject>("getInstance");
      }
      catch
      {
        androidJavaObject = (AndroidJavaObject) null;
      }
      return androidJavaObject != null;
    }

    private static void HandleException(object sender, UnhandledExceptionEventArgs eArgs)
    {
      Exception exceptionObject = (Exception) eArgs.ExceptionObject;
      CrashlyticsInit.HandleLog(exceptionObject.Message.ToString(), exceptionObject.StackTrace.ToString(), LogType.Exception);
    }

    private static void HandleLog(string message, string stackTraceString, LogType type)
    {
      if (type != LogType.Exception)
        return;
      Utils.Log(CrashlyticsInit.kitName, "Recording exception: " + message);
      Utils.Log(CrashlyticsInit.kitName, "Exception stack trace: " + stackTraceString);
      string[] messageParts = CrashlyticsInit.getMessageParts(message);
      Fabric.Crashlytics.Crashlytics.RecordCustomException(messageParts[0], messageParts[1], stackTraceString);
    }

    private static string[] getMessageParts(string message)
    {
      char[] separator = new char[1]{ ':' };
      string[] strArray = message.Split(separator, 2, StringSplitOptions.None);
      foreach (string str in strArray)
        str.Trim();
      if (strArray.Length == 2)
        return strArray;
      return new string[2]{ "Exception", message };
    }
  }
}
