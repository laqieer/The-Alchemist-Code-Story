﻿// Decompiled with JetBrains decompiler
// Type: Gsc.Core.AssemblySupport
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Reflection;

#nullable disable
namespace Gsc.Core
{
  public static class AssemblySupport
  {
    public static T CreateInstance<T>() => AssemblySupport.CreateInstance<T>(typeof (T));

    public static T CreateInstance<T>(params object[] args)
    {
      return AssemblySupport.CreateInstance<T>(typeof (T), args);
    }

    public static T CreateInstance<T>(string typeName)
    {
      return AssemblySupport.CreateInstance<T>(AssemblySupport.GetType(typeName));
    }

    public static T CreateInstance<T>(string typeName, params object[] args)
    {
      return AssemblySupport.CreateInstance<T>(AssemblySupport.GetType(typeName), args);
    }

    public static T CreateInstance<T>(Type type) => (T) Activator.CreateInstance(type);

    public static T CreateInstance<T>(Type type, params object[] args)
    {
      return (T) Activator.CreateInstance(type, args);
    }

    public static Type GetType(string typeName)
    {
      Type type = Type.GetType(typeName + ", Assembly-CSharp");
      if ((object) type == null)
        type = Type.GetType(typeName + ", Assembly-CSharp-firstpass");
      return type;
    }

    public static AssemblySupport.MethodInfo GetConstructor(string typeName, params Type[] types)
    {
      return AssemblySupport.GetConstructor(AssemblySupport.GetType(typeName), types);
    }

    public static AssemblySupport.MethodInfo GetMethod(
      string typeName,
      string methodName,
      params Type[] types)
    {
      return AssemblySupport.GetMethod(AssemblySupport.GetType(typeName), methodName, types);
    }

    public static AssemblySupport.MethodInfo GetConstructor(Type type, params Type[] types)
    {
      return (object) type != null ? AssemblySupport.MethodInfo.Create(type, (MethodBase) type.GetConstructor(types)) : (AssemblySupport.MethodInfo) null;
    }

    public static AssemblySupport.MethodInfo GetMethod(
      Type type,
      string methodName,
      params Type[] types)
    {
      return (object) type != null ? AssemblySupport.MethodInfo.Create(type, (MethodBase) type.GetMethod(methodName, types)) : (AssemblySupport.MethodInfo) null;
    }

    public class MethodInfo
    {
      public readonly Type Type;
      private MethodBase methodInfo;

      private MethodInfo(Type type, MethodBase methodInfo)
      {
        this.Type = type;
        this.methodInfo = methodInfo;
      }

      public static AssemblySupport.MethodInfo Create(Type type, MethodBase methodInfo)
      {
        return (object) methodInfo != null ? new AssemblySupport.MethodInfo(type, methodInfo) : (AssemblySupport.MethodInfo) null;
      }

      public void CallVoidMethod(object obj, params object[] args)
      {
        this.methodInfo.Invoke(obj, args);
      }

      public T CallMethod<T>(object obj, params object[] args)
      {
        return (T) this.methodInfo.Invoke(obj, args);
      }

      public void CallStaticVoidMethod(params object[] args)
      {
        this.CallVoidMethod((object) null, args);
      }

      public T CallStaticMethod<T>(params object[] args) => this.CallMethod<T>((object) null, args);

      public T CreateInstance<T>(params object[] args) => this.CallStaticMethod<T>(args);
    }
  }
}
