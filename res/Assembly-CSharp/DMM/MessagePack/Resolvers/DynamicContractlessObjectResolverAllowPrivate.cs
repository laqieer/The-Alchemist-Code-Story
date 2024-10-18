// Decompiled with JetBrains decompiler
// Type: MessagePack.Resolvers.DynamicContractlessObjectResolverAllowPrivate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Formatters;
using MessagePack.Internal;
using System;
using System.Reflection;

#nullable disable
namespace MessagePack.Resolvers
{
  public sealed class DynamicContractlessObjectResolverAllowPrivate : IFormatterResolver
  {
    public static readonly DynamicContractlessObjectResolverAllowPrivate Instance = new DynamicContractlessObjectResolverAllowPrivate();

    public IMessagePackFormatter<T> GetFormatter<T>()
    {
      return DynamicContractlessObjectResolverAllowPrivate.FormatterCache<T>.formatter;
    }

    private static class FormatterCache<T>
    {
      public static readonly IMessagePackFormatter<T> formatter;

      static FormatterCache()
      {
        if ((object) typeof (T) == (object) typeof (object))
          return;
        TypeInfo typeInfo1 = System.Reflection.ReflectionExtensions.GetTypeInfo(typeof (T));
        if (typeInfo1.IsInterface)
          return;
        if (typeInfo1.IsNullable())
        {
          TypeInfo typeInfo2 = System.Reflection.ReflectionExtensions.GetTypeInfo(typeInfo1.GenericTypeArguments[0]);
          object formatterDynamic = DynamicContractlessObjectResolverAllowPrivate.Instance.GetFormatterDynamic(typeInfo2.AsType());
          if (formatterDynamic == null)
            return;
          DynamicContractlessObjectResolverAllowPrivate.FormatterCache<T>.formatter = (IMessagePackFormatter<T>) Activator.CreateInstance(typeof (StaticNullableFormatter<>).MakeGenericType(typeInfo2.AsType()), formatterDynamic);
        }
        else if (typeInfo1.IsAnonymous())
          DynamicContractlessObjectResolverAllowPrivate.FormatterCache<T>.formatter = (IMessagePackFormatter<T>) DynamicObjectTypeBuilder.BuildFormatterToDynamicMethod(typeof (T), true, true, false);
        else
          DynamicContractlessObjectResolverAllowPrivate.FormatterCache<T>.formatter = (IMessagePackFormatter<T>) DynamicObjectTypeBuilder.BuildFormatterToDynamicMethod(typeof (T), true, true, true);
      }
    }
  }
}
