// Decompiled with JetBrains decompiler
// Type: MessagePack.Resolvers.CompositeResolver
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Formatters;
using System;
using System.Reflection;

#nullable disable
namespace MessagePack.Resolvers
{
  public sealed class CompositeResolver : IFormatterResolver
  {
    public static readonly CompositeResolver Instance = new CompositeResolver();
    private static bool isFreezed = false;
    private static IMessagePackFormatter[] formatters = new IMessagePackFormatter[0];
    private static IFormatterResolver[] resolvers = new IFormatterResolver[0];

    private CompositeResolver()
    {
    }

    public static void Register(params IFormatterResolver[] resolvers)
    {
      if (CompositeResolver.isFreezed)
        throw new InvalidOperationException("Register must call on startup(before use GetFormatter<T>).");
      CompositeResolver.resolvers = resolvers;
    }

    public static void Register(params IMessagePackFormatter[] formatters)
    {
      if (CompositeResolver.isFreezed)
        throw new InvalidOperationException("Register must call on startup(before use GetFormatter<T>).");
      CompositeResolver.formatters = formatters;
    }

    public static void Register(IMessagePackFormatter[] formatters, IFormatterResolver[] resolvers)
    {
      if (CompositeResolver.isFreezed)
        throw new InvalidOperationException("Register must call on startup(before use GetFormatter<T>).");
      CompositeResolver.resolvers = resolvers;
      CompositeResolver.formatters = formatters;
    }

    public static void RegisterAndSetAsDefault(params IFormatterResolver[] resolvers)
    {
      CompositeResolver.Register(resolvers);
      MessagePackSerializer.SetDefaultResolver((IFormatterResolver) CompositeResolver.Instance);
    }

    public static void RegisterAndSetAsDefault(params IMessagePackFormatter[] formatters)
    {
      CompositeResolver.Register(formatters);
      MessagePackSerializer.SetDefaultResolver((IFormatterResolver) CompositeResolver.Instance);
    }

    public static void RegisterAndSetAsDefault(
      IMessagePackFormatter[] formatters,
      IFormatterResolver[] resolvers)
    {
      CompositeResolver.Register(formatters);
      CompositeResolver.Register(resolvers);
      MessagePackSerializer.SetDefaultResolver((IFormatterResolver) CompositeResolver.Instance);
    }

    public IMessagePackFormatter<T> GetFormatter<T>()
    {
      return CompositeResolver.FormatterCache<T>.formatter;
    }

    private static class FormatterCache<T>
    {
      public static readonly IMessagePackFormatter<T> formatter;

      static FormatterCache()
      {
        CompositeResolver.isFreezed = true;
        foreach (IMessagePackFormatter formatter in CompositeResolver.formatters)
        {
          foreach (Type implementedInterface in ReflectionExtensions.GetTypeInfo(formatter.GetType()).ImplementedInterfaces)
          {
            TypeInfo typeInfo = ReflectionExtensions.GetTypeInfo(implementedInterface);
            if (typeInfo.IsGenericType && (object) typeInfo.GenericTypeArguments[0] == (object) typeof (T))
            {
              CompositeResolver.FormatterCache<T>.formatter = (IMessagePackFormatter<T>) formatter;
              return;
            }
          }
        }
        foreach (IFormatterResolver resolver in CompositeResolver.resolvers)
        {
          IMessagePackFormatter<T> formatter = resolver.GetFormatter<T>();
          if (formatter != null)
          {
            CompositeResolver.FormatterCache<T>.formatter = formatter;
            break;
          }
        }
      }
    }
  }
}
