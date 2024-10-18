// Decompiled with JetBrains decompiler
// Type: MessagePack.Resolvers.DynamicEnumAsStringResolver
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Formatters;
using System;
using System.Reflection;

#nullable disable
namespace MessagePack.Resolvers
{
  public sealed class DynamicEnumAsStringResolver : IFormatterResolver
  {
    public static readonly IFormatterResolver Instance = (IFormatterResolver) new DynamicEnumAsStringResolver();

    private DynamicEnumAsStringResolver()
    {
    }

    public IMessagePackFormatter<T> GetFormatter<T>()
    {
      return DynamicEnumAsStringResolver.FormatterCache<T>.formatter;
    }

    private static class FormatterCache<T>
    {
      public static readonly IMessagePackFormatter<T> formatter;

      static FormatterCache()
      {
        TypeInfo typeInfo1 = System.Reflection.ReflectionExtensions.GetTypeInfo(typeof (T));
        if (typeInfo1.IsNullable())
        {
          TypeInfo typeInfo2 = System.Reflection.ReflectionExtensions.GetTypeInfo(typeInfo1.GenericTypeArguments[0]);
          if (!typeInfo2.IsEnum)
            return;
          object formatterDynamic = DynamicEnumAsStringResolver.Instance.GetFormatterDynamic(typeInfo2.AsType());
          if (formatterDynamic == null)
            return;
          DynamicEnumAsStringResolver.FormatterCache<T>.formatter = (IMessagePackFormatter<T>) Activator.CreateInstance(typeof (StaticNullableFormatter<>).MakeGenericType(typeInfo2.AsType()), formatterDynamic);
        }
        else
        {
          if (!typeInfo1.IsEnum)
            return;
          DynamicEnumAsStringResolver.FormatterCache<T>.formatter = (IMessagePackFormatter<T>) new EnumAsStringFormatter<T>();
        }
      }
    }
  }
}
