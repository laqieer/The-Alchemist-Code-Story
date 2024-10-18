// Decompiled with JetBrains decompiler
// Type: MessagePack.Internal.StandardResolverCore
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Formatters;
using MessagePack.Resolvers;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace MessagePack.Internal
{
  internal sealed class StandardResolverCore : IFormatterResolver
  {
    public static readonly IFormatterResolver Instance = (IFormatterResolver) new StandardResolverCore();
    private static readonly IFormatterResolver[] resolvers = ((IEnumerable<IFormatterResolver>) StandardResolverHelper.DefaultResolvers).Concat<IFormatterResolver>((IEnumerable<IFormatterResolver>) new IFormatterResolver[1]
    {
      (IFormatterResolver) DynamicObjectResolver.Instance
    }).ToArray<IFormatterResolver>();

    private StandardResolverCore()
    {
    }

    public IMessagePackFormatter<T> GetFormatter<T>()
    {
      return StandardResolverCore.FormatterCache<T>.formatter;
    }

    private static class FormatterCache<T>
    {
      public static readonly IMessagePackFormatter<T> formatter;

      static FormatterCache()
      {
        foreach (IFormatterResolver resolver in StandardResolverCore.resolvers)
        {
          IMessagePackFormatter<T> formatter = resolver.GetFormatter<T>();
          if (formatter != null)
          {
            StandardResolverCore.FormatterCache<T>.formatter = formatter;
            break;
          }
        }
      }
    }
  }
}
