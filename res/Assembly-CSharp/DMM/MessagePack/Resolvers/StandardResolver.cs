// Decompiled with JetBrains decompiler
// Type: MessagePack.Resolvers.StandardResolver
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Formatters;
using MessagePack.Internal;

#nullable disable
namespace MessagePack.Resolvers
{
  public sealed class StandardResolver : IFormatterResolver
  {
    public static readonly IFormatterResolver Instance = (IFormatterResolver) new StandardResolver();

    private StandardResolver()
    {
    }

    public IMessagePackFormatter<T> GetFormatter<T>()
    {
      return StandardResolver.FormatterCache<T>.formatter;
    }

    private static class FormatterCache<T>
    {
      public static readonly IMessagePackFormatter<T> formatter;

      static FormatterCache()
      {
        if ((object) typeof (T) == (object) typeof (object))
          StandardResolver.FormatterCache<T>.formatter = PrimitiveObjectResolver.Instance.GetFormatter<T>();
        else
          StandardResolver.FormatterCache<T>.formatter = StandardResolverCore.Instance.GetFormatter<T>();
      }
    }
  }
}
