// Decompiled with JetBrains decompiler
// Type: MessagePack.Resolvers.AttributeFormatterResolver
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Formatters;
using System;
using System.Reflection;

#nullable disable
namespace MessagePack.Resolvers
{
  public sealed class AttributeFormatterResolver : IFormatterResolver
  {
    public static IFormatterResolver Instance = (IFormatterResolver) new AttributeFormatterResolver();

    private AttributeFormatterResolver()
    {
    }

    public IMessagePackFormatter<T> GetFormatter<T>()
    {
      return AttributeFormatterResolver.FormatterCache<T>.formatter;
    }

    private static class FormatterCache<T>
    {
      public static readonly IMessagePackFormatter<T> formatter;

      static FormatterCache()
      {
        MessagePackFormatterAttribute customAttribute = ReflectionExtensions.GetTypeInfo(typeof (T)).GetCustomAttribute<MessagePackFormatterAttribute>();
        if (customAttribute == null)
          return;
        if (customAttribute.Arguments == null)
          AttributeFormatterResolver.FormatterCache<T>.formatter = (IMessagePackFormatter<T>) Activator.CreateInstance(customAttribute.FormatterType);
        else
          AttributeFormatterResolver.FormatterCache<T>.formatter = (IMessagePackFormatter<T>) Activator.CreateInstance(customAttribute.FormatterType, customAttribute.Arguments);
      }
    }
  }
}
