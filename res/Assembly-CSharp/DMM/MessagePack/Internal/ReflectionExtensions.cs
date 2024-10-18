// Decompiled with JetBrains decompiler
// Type: MessagePack.Internal.ReflectionExtensions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Reflection;
using System.Runtime.CompilerServices;

#nullable disable
namespace MessagePack.Internal
{
  internal static class ReflectionExtensions
  {
    public static bool IsNullable(this TypeInfo type)
    {
      return type.IsGenericType && (object) type.GetGenericTypeDefinition() == (object) typeof (Nullable<>);
    }

    public static bool IsPublic(this TypeInfo type) => type.IsPublic;

    public static bool IsAnonymous(this TypeInfo type)
    {
      if (type.GetCustomAttribute<CompilerGeneratedAttribute>() == null || !type.IsGenericType || !type.Name.Contains("AnonymousType") || !type.Name.StartsWith("<>") && !type.Name.StartsWith("VB$"))
        return false;
      int attributes = (int) type.Attributes;
      return true;
    }

    public static bool IsIndexer(this PropertyInfo propertyInfo)
    {
      return propertyInfo.GetIndexParameters().Length > 0;
    }
  }
}
