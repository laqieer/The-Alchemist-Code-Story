// Decompiled with JetBrains decompiler
// Type: System.Reflection.ReflectionExtensions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Linq;
using System.Reflection.Emit;

#nullable disable
namespace System.Reflection
{
  public static class ReflectionExtensions
  {
    public static TypeInfo GetTypeInfo(this Type type) => new TypeInfo(type);

    public static TypeInfo CreateTypeInfo(this TypeBuilder type) => new TypeInfo(type.CreateType());

    public static MethodInfo GetRuntimeMethod(this Type type, string name, Type[] types)
    {
      return type.GetMethod(name, types);
    }

    public static MethodInfo GetRuntimeMethod(this Type type, string name) => type.GetMethod(name);

    public static MethodInfo[] GetRuntimeMethods(this Type type) => type.GetMethods();

    public static PropertyInfo GetRuntimeProperty(this Type type, string name)
    {
      return type.GetProperty(name);
    }

    public static PropertyInfo[] GetRuntimeProperties(this Type type) => type.GetProperties();

    public static FieldInfo GetRuntimeField(this Type type, string name) => type.GetField(name);

    public static FieldInfo[] GetRuntimeFields(this Type type) => type.GetFields();

    public static T GetCustomAttribute<T>(this FieldInfo type, bool inherit) where T : Attribute
    {
      return type.GetCustomAttributes(inherit).OfType<T>().FirstOrDefault<T>();
    }

    public static T GetCustomAttribute<T>(this PropertyInfo type, bool inherit) where T : Attribute
    {
      return type.GetCustomAttributes(inherit).OfType<T>().FirstOrDefault<T>();
    }

    public static T GetCustomAttribute<T>(this ConstructorInfo type, bool inherit) where T : Attribute
    {
      return type.GetCustomAttributes(inherit).OfType<T>().FirstOrDefault<T>();
    }
  }
}
