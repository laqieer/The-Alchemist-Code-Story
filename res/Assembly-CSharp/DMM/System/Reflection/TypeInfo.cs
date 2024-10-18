// Decompiled with JetBrains decompiler
// Type: System.Reflection.TypeInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace System.Reflection
{
  public class TypeInfo
  {
    private readonly Type type;

    public TypeInfo(Type type) => this.type = type;

    public string Name => this.type.Name;

    public TypeAttributes Attributes => this.type.Attributes;

    public bool IsClass => this.type.IsClass;

    public bool IsPublic => this.type.IsPublic;

    public bool IsInterface => this.type.IsInterface;

    public bool IsAbstract => this.type.IsAbstract;

    public bool IsArray => this.type.IsArray;

    public bool IsValueType => this.type.IsValueType;

    public bool IsNestedPublic => this.type.IsNestedPublic;

    public IEnumerable<ConstructorInfo> DeclaredConstructors
    {
      get
      {
        return ((IEnumerable<ConstructorInfo>) this.type.GetConstructors()).AsEnumerable<ConstructorInfo>();
      }
    }

    public bool IsGenericType => this.type.IsGenericType;

    public Type GetGenericTypeDefinition() => this.type.GetGenericTypeDefinition();

    public Type AsType() => this.type;

    public MethodInfo GetDeclaredMethod(string name) => this.type.GetMethod(name);

    public IEnumerable<MethodInfo> GetDeclaredMethods(string name)
    {
      return ((IEnumerable<MethodInfo>) this.type.GetMethods()).Where<MethodInfo>((Func<MethodInfo, bool>) (x => x.Name == name));
    }

    public Type[] GenericTypeArguments => this.type.GetGenericArguments();

    public bool IsEnum => this.type.IsEnum;

    public bool IsConstructedGenericType()
    {
      return this.type.IsGenericType && !this.type.IsGenericTypeDefinition;
    }

    public Type[] ImplementedInterfaces => this.type.GetInterfaces();

    public MethodInfo[] GetRuntimeMethods() => this.type.GetMethods();

    public bool IsAssignableFrom(TypeInfo c) => this.type.IsAssignableFrom(c.AsType());

    public PropertyInfo GetDeclaredProperty(string name) => this.type.GetProperty(name);

    public FieldInfo GetField(string name, BindingFlags flags) => this.type.GetField(name, flags);

    public PropertyInfo GetProperty(string name, BindingFlags flags)
    {
      return this.type.GetProperty(name, flags);
    }

    public T GetCustomAttribute<T>(bool inherit = true) where T : Attribute
    {
      return this.type.GetCustomAttributes(inherit).OfType<T>().FirstOrDefault<T>();
    }

    public IEnumerable<T> GetCustomAttributes<T>(bool inherit = true) where T : Attribute
    {
      return this.type.GetCustomAttributes(inherit).OfType<T>();
    }
  }
}
