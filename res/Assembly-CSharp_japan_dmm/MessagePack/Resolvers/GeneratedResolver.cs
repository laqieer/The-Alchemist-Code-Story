// Decompiled with JetBrains decompiler
// Type: MessagePack.Resolvers.GeneratedResolver
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Formatters;

#nullable disable
namespace MessagePack.Resolvers
{
  public class GeneratedResolver : IFormatterResolver
  {
    public static readonly IFormatterResolver Instance = (IFormatterResolver) new GeneratedResolver();

    private GeneratedResolver()
    {
    }

    public IMessagePackFormatter<T> GetFormatter<T>()
    {
      return GeneratedResolver.FormatterCache<T>.formatter;
    }

    private static class FormatterCache<T>
    {
      public static readonly IMessagePackFormatter<T> formatter;

      static FormatterCache()
      {
        object formatter = GeneratedResolverGetFormatterHelper.GetFormatter(typeof (T));
        if (formatter == null)
          return;
        GeneratedResolver.FormatterCache<T>.formatter = (IMessagePackFormatter<T>) formatter;
      }
    }
  }
}
