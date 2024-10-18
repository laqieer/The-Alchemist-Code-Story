﻿// Decompiled with JetBrains decompiler
// Type: MessagePack.Internal.ContractlessStandardResolverAllowPrivateCore
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
  internal sealed class ContractlessStandardResolverAllowPrivateCore : IFormatterResolver
  {
    public static readonly IFormatterResolver Instance = (IFormatterResolver) new ContractlessStandardResolverAllowPrivateCore();
    private static readonly IFormatterResolver[] resolvers = ((IEnumerable<IFormatterResolver>) StandardResolverHelper.DefaultResolvers).Concat<IFormatterResolver>((IEnumerable<IFormatterResolver>) new IFormatterResolver[2]
    {
      (IFormatterResolver) DynamicObjectResolverAllowPrivate.Instance,
      (IFormatterResolver) DynamicContractlessObjectResolverAllowPrivate.Instance
    }).ToArray<IFormatterResolver>();

    private ContractlessStandardResolverAllowPrivateCore()
    {
    }

    public IMessagePackFormatter<T> GetFormatter<T>()
    {
      return ContractlessStandardResolverAllowPrivateCore.FormatterCache<T>.formatter;
    }

    private static class FormatterCache<T>
    {
      public static readonly IMessagePackFormatter<T> formatter;

      static FormatterCache()
      {
        foreach (IFormatterResolver resolver in ContractlessStandardResolverAllowPrivateCore.resolvers)
        {
          IMessagePackFormatter<T> formatter = resolver.GetFormatter<T>();
          if (formatter != null)
          {
            ContractlessStandardResolverAllowPrivateCore.FormatterCache<T>.formatter = formatter;
            break;
          }
        }
      }
    }
  }
}
