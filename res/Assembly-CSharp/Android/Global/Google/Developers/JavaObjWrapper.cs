// Decompiled with JetBrains decompiler
// Type: Google.Developers.JavaObjWrapper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using System.Reflection;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Google.Developers
{
  public class JavaObjWrapper
  {
    private IntPtr raw;

    protected JavaObjWrapper()
    {
    }

    public JavaObjWrapper(string clazzName)
    {
      this.raw = AndroidJNI.AllocObject(AndroidJNI.FindClass(clazzName));
    }

    public JavaObjWrapper(IntPtr rawObject)
    {
      this.raw = rawObject;
    }

    public IntPtr RawObject
    {
      get
      {
        return this.raw;
      }
    }

    public void CreateInstance(string clazzName, params object[] args)
    {
      if (this.raw != IntPtr.Zero)
        throw new Exception("Java object already set");
      IntPtr num = AndroidJNI.FindClass(clazzName);
      IntPtr constructorId = AndroidJNIHelper.GetConstructorID(num, args);
      jvalue[] args1 = JavaObjWrapper.ConstructArgArray(args);
      this.raw = AndroidJNI.NewObject(num, constructorId, args1);
    }

    protected static jvalue[] ConstructArgArray(object[] theArgs)
    {
      object[] args = new object[theArgs.Length];
      for (int index = 0; index < theArgs.Length; ++index)
        args[index] = !(theArgs[index] is JavaObjWrapper) ? theArgs[index] : (object) ((JavaObjWrapper) theArgs[index]).raw;
      jvalue[] jniArgArray = AndroidJNIHelper.CreateJNIArgArray(args);
      for (int index = 0; index < theArgs.Length; ++index)
      {
        if (theArgs[index] is JavaObjWrapper)
          jniArgArray[index].l = ((JavaObjWrapper) theArgs[index]).raw;
        else if (theArgs[index] is JavaInterfaceProxy)
        {
          IntPtr javaProxy = AndroidJNIHelper.CreateJavaProxy((AndroidJavaProxy) theArgs[index]);
          jniArgArray[index].l = javaProxy;
        }
      }
      if (jniArgArray.Length == 1)
      {
        for (int index = 0; index < jniArgArray.Length; ++index)
          Debug.Log((object) ("---- [" + (object) index + "] -- " + (object) jniArgArray[index].l));
      }
      return jniArgArray;
    }

    public static T StaticInvokeObjectCall<T>(string type, string name, string sig, params object[] args)
    {
      IntPtr clazz = AndroidJNI.FindClass(type);
      IntPtr staticMethodId = AndroidJNI.GetStaticMethodID(clazz, name, sig);
      jvalue[] args1 = JavaObjWrapper.ConstructArgArray(args);
      IntPtr num = AndroidJNI.CallStaticObjectMethod(clazz, staticMethodId, args1);
      ConstructorInfo constructor = typeof (T).GetConstructor(new System.Type[1]{ num.GetType() });
      if ((object) constructor != null)
        return (T) constructor.Invoke(new object[1]{ (object) num });
      if (typeof (T).IsArray)
        return AndroidJNIHelper.ConvertFromJNIArray<T>(num);
      Debug.Log((object) "Trying cast....");
      System.Type structureType = typeof (T);
      return (T) Marshal.PtrToStructure(num, structureType);
    }

    public static void StaticInvokeCallVoid(string type, string name, string sig, params object[] args)
    {
      IntPtr clazz = AndroidJNI.FindClass(type);
      IntPtr staticMethodId = AndroidJNI.GetStaticMethodID(clazz, name, sig);
      jvalue[] args1 = JavaObjWrapper.ConstructArgArray(args);
      AndroidJNI.CallStaticVoidMethod(clazz, staticMethodId, args1);
    }

    public static T GetStaticObjectField<T>(string clsName, string name, string sig)
    {
      IntPtr clazz = AndroidJNI.FindClass(clsName);
      IntPtr staticFieldId = AndroidJNI.GetStaticFieldID(clazz, name, sig);
      IntPtr staticObjectField = AndroidJNI.GetStaticObjectField(clazz, staticFieldId);
      ConstructorInfo constructor = typeof (T).GetConstructor(new System.Type[1]{ staticObjectField.GetType() });
      if ((object) constructor != null)
        return (T) constructor.Invoke(new object[1]{ (object) staticObjectField });
      System.Type structureType = typeof (T);
      return (T) Marshal.PtrToStructure(staticObjectField, structureType);
    }

    public static int GetStaticIntField(string clsName, string name)
    {
      IntPtr clazz = AndroidJNI.FindClass(clsName);
      IntPtr staticFieldId = AndroidJNI.GetStaticFieldID(clazz, name, "I");
      return AndroidJNI.GetStaticIntField(clazz, staticFieldId);
    }

    public static string GetStaticStringField(string clsName, string name)
    {
      IntPtr clazz = AndroidJNI.FindClass(clsName);
      IntPtr staticFieldId = AndroidJNI.GetStaticFieldID(clazz, name, "Ljava/lang/String;");
      return AndroidJNI.GetStaticStringField(clazz, staticFieldId);
    }

    public static float GetStaticFloatField(string clsName, string name)
    {
      IntPtr clazz = AndroidJNI.FindClass(clsName);
      IntPtr staticFieldId = AndroidJNI.GetStaticFieldID(clazz, name, "F");
      return AndroidJNI.GetStaticFloatField(clazz, staticFieldId);
    }

    public void InvokeCallVoid(string name, string sig, params object[] args)
    {
      AndroidJNI.CallVoidMethod(this.raw, AndroidJNI.GetMethodID(AndroidJNI.GetObjectClass(this.raw), name, sig), JavaObjWrapper.ConstructArgArray(args));
    }

    public T InvokeCall<T>(string name, string sig, params object[] args)
    {
      System.Type type = typeof (T);
      IntPtr methodId = AndroidJNI.GetMethodID(AndroidJNI.GetObjectClass(this.raw), name, sig);
      jvalue[] args1 = JavaObjWrapper.ConstructArgArray(args);
      if ((object) type == (object) typeof (bool))
        return (T) (ValueType) AndroidJNI.CallBooleanMethod(this.raw, methodId, args1);
      if ((object) type == (object) typeof (string))
        return (T) AndroidJNI.CallStringMethod(this.raw, methodId, args1);
      if ((object) type == (object) typeof (int))
        return (T) (ValueType) AndroidJNI.CallIntMethod(this.raw, methodId, args1);
      if ((object) type == (object) typeof (float))
        return (T) (ValueType) AndroidJNI.CallFloatMethod(this.raw, methodId, args1);
      if ((object) type == (object) typeof (double))
        return (T) (ValueType) AndroidJNI.CallDoubleMethod(this.raw, methodId, args1);
      if ((object) type == (object) typeof (byte))
        return (T) (ValueType) AndroidJNI.CallByteMethod(this.raw, methodId, args1);
      if ((object) type == (object) typeof (char))
        return (T) (ValueType) AndroidJNI.CallCharMethod(this.raw, methodId, args1);
      if ((object) type == (object) typeof (long))
        return (T) (ValueType) AndroidJNI.CallLongMethod(this.raw, methodId, args1);
      if ((object) type == (object) typeof (short))
        return (T) (ValueType) AndroidJNI.CallShortMethod(this.raw, methodId, args1);
      return this.InvokeObjectCall<T>(name, sig, args);
    }

    public static T StaticInvokeCall<T>(string type, string name, string sig, params object[] args)
    {
      System.Type type1 = typeof (T);
      IntPtr clazz = AndroidJNI.FindClass(type);
      IntPtr staticMethodId = AndroidJNI.GetStaticMethodID(clazz, name, sig);
      jvalue[] args1 = JavaObjWrapper.ConstructArgArray(args);
      if ((object) type1 == (object) typeof (bool))
        return (T) (ValueType) AndroidJNI.CallStaticBooleanMethod(clazz, staticMethodId, args1);
      if ((object) type1 == (object) typeof (string))
        return (T) AndroidJNI.CallStaticStringMethod(clazz, staticMethodId, args1);
      if ((object) type1 == (object) typeof (int))
        return (T) (ValueType) AndroidJNI.CallStaticIntMethod(clazz, staticMethodId, args1);
      if ((object) type1 == (object) typeof (float))
        return (T) (ValueType) AndroidJNI.CallStaticFloatMethod(clazz, staticMethodId, args1);
      if ((object) type1 == (object) typeof (double))
        return (T) (ValueType) AndroidJNI.CallStaticDoubleMethod(clazz, staticMethodId, args1);
      if ((object) type1 == (object) typeof (byte))
        return (T) (ValueType) AndroidJNI.CallStaticByteMethod(clazz, staticMethodId, args1);
      if ((object) type1 == (object) typeof (char))
        return (T) (ValueType) AndroidJNI.CallStaticCharMethod(clazz, staticMethodId, args1);
      if ((object) type1 == (object) typeof (long))
        return (T) (ValueType) AndroidJNI.CallStaticLongMethod(clazz, staticMethodId, args1);
      if ((object) type1 == (object) typeof (short))
        return (T) (ValueType) AndroidJNI.CallStaticShortMethod(clazz, staticMethodId, args1);
      return JavaObjWrapper.StaticInvokeObjectCall<T>(type, name, sig, args);
    }

    public T InvokeObjectCall<T>(string name, string sig, params object[] theArgs)
    {
      IntPtr ptr = AndroidJNI.CallObjectMethod(this.raw, AndroidJNI.GetMethodID(AndroidJNI.GetObjectClass(this.raw), name, sig), JavaObjWrapper.ConstructArgArray(theArgs));
      if (ptr.Equals((object) IntPtr.Zero))
        return default (T);
      ConstructorInfo constructor = typeof (T).GetConstructor(new System.Type[1]{ ptr.GetType() });
      if ((object) constructor != null)
        return (T) constructor.Invoke(new object[1]{ (object) ptr });
      System.Type structureType = typeof (T);
      return (T) Marshal.PtrToStructure(ptr, structureType);
    }
  }
}
