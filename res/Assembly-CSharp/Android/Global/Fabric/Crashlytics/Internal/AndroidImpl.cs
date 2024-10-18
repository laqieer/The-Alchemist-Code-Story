// Decompiled with JetBrains decompiler
// Type: Fabric.Crashlytics.Internal.AndroidImpl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Fabric.Crashlytics.Internal
{
  internal class AndroidImpl : Impl
  {
    private readonly List<IntPtr> references = new List<IntPtr>();
    private AndroidJavaObject native;
    private AndroidJavaClass crashWrapper;
    private AndroidJavaObject instance;

    private AndroidJavaObject Native
    {
      get
      {
        if (this.native == null)
          this.native = new AndroidJavaObject("com.crashlytics.android.Crashlytics", new object[0]);
        return this.native;
      }
    }

    private AndroidJavaClass CrashWrapper
    {
      get
      {
        if (this.crashWrapper == null)
          this.crashWrapper = new AndroidJavaClass("io.fabric.unity.crashlytics.android.CrashlyticsAndroidWrapper");
        return this.crashWrapper;
      }
    }

    private AndroidJavaObject Instance
    {
      get
      {
        if (this.instance == null)
          this.instance = this.Native.CallStatic<AndroidJavaObject>("getInstance");
        if (this.instance == null)
          throw new AndroidImpl.JavaInteropException("Couldn't get an instance of the Crashlytics class!");
        return this.instance;
      }
    }

    public override void Crash()
    {
      this.CrashWrapper.CallStatic("crash");
    }

    public override void Log(string message)
    {
      this.Instance.CallStatic("log", new object[1]
      {
        (object) message
      });
    }

    public override void SetKeyValue(string key, string value)
    {
      this.Instance.CallStatic("setString", (object) key, (object) value);
    }

    public override void SetUserIdentifier(string identifier)
    {
      this.Instance.CallStatic("setUserIdentifier", new object[1]
      {
        (object) identifier
      });
    }

    public override void SetUserEmail(string email)
    {
      this.Instance.CallStatic("setUserEmail", new object[1]
      {
        (object) email
      });
    }

    public override void SetUserName(string name)
    {
      this.Instance.CallStatic("setUserName", new object[1]
      {
        (object) name
      });
    }

    public override void RecordCustomException(string name, string reason, StackTrace stackTrace)
    {
      this.RecordCustomException(name, reason, stackTrace.ToString());
    }

    public override void RecordCustomException(string name, string reason, string stackTraceString)
    {
      this.references.Clear();
      IntPtr clazz1 = AndroidJNI.FindClass("java/lang/Exception");
      IntPtr methodId1 = AndroidJNI.GetMethodID(clazz1, "<init>", "(Ljava/lang/String;)V");
      jvalue[] args1 = new jvalue[1];
      args1[0].l = AndroidJNI.NewStringUTF(name + " : " + reason);
      IntPtr num1 = AndroidJNI.NewObject(clazz1, methodId1, args1);
      this.references.Add(args1[0].l);
      this.references.Add(num1);
      IntPtr clazz2 = AndroidJNI.FindClass("java/lang/StackTraceElement");
      IntPtr methodId2 = AndroidJNI.GetMethodID(clazz2, "<init>", "(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;I)V");
      Dictionary<string, string>[] stackTraceString1 = Impl.ParseStackTraceString(stackTraceString);
      IntPtr array = AndroidJNI.NewObjectArray(stackTraceString1.Length, clazz2, IntPtr.Zero);
      this.references.Add(array);
      for (int index = 0; index < stackTraceString1.Length; ++index)
      {
        Dictionary<string, string> dictionary = stackTraceString1[index];
        jvalue[] args2 = new jvalue[4];
        args2[0].l = AndroidJNI.NewStringUTF(dictionary["class"]);
        args2[1].l = AndroidJNI.NewStringUTF(dictionary["method"]);
        args2[2].l = AndroidJNI.NewStringUTF(dictionary["file"]);
        this.references.Add(args2[0].l);
        this.references.Add(args2[1].l);
        this.references.Add(args2[2].l);
        args2[3].i = int.Parse(dictionary["line"]);
        IntPtr num2 = AndroidJNI.NewObject(clazz2, methodId2, args2);
        this.references.Add(num2);
        AndroidJNI.SetObjectArrayElement(array, index, num2);
      }
      IntPtr methodId3 = AndroidJNI.GetMethodID(clazz1, "setStackTrace", "([Ljava/lang/StackTraceElement;)V");
      jvalue[] args3 = new jvalue[1];
      args3[0].l = array;
      AndroidJNI.CallVoidMethod(num1, methodId3, args3);
      IntPtr clazz3 = AndroidJNI.FindClass("com/crashlytics/android/Crashlytics");
      IntPtr staticMethodId = AndroidJNI.GetStaticMethodID(clazz3, "logException", "(Ljava/lang/Throwable;)V");
      jvalue[] args4 = new jvalue[1];
      args4[0].l = num1;
      AndroidJNI.CallStaticVoidMethod(clazz3, staticMethodId, args4);
      using (List<IntPtr>.Enumerator enumerator = this.references.GetEnumerator())
      {
        while (enumerator.MoveNext())
          AndroidJNI.DeleteLocalRef(enumerator.Current);
      }
    }

    public class JavaInteropException : Exception
    {
      public JavaInteropException(string message)
        : base(message)
      {
      }
    }
  }
}
