// Decompiled with JetBrains decompiler
// Type: MessagePack.Resolvers.OldSpecResolver
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Formatters;
using MessagePack.Internal;

#nullable disable
namespace MessagePack.Resolvers
{
  public sealed class OldSpecResolver : IFormatterResolver
  {
    public static readonly IFormatterResolver Instance = (IFormatterResolver) new OldSpecResolver();

    private OldSpecResolver()
    {
    }

    public IMessagePackFormatter<T> GetFormatter<T>()
    {
      return OldSpecResolver.FormatterCache<T>.formatter;
    }

    private static class FormatterCache<T>
    {
      public static readonly IMessagePackFormatter<T> formatter = (IMessagePackFormatter<T>) OldSpecResolverGetFormatterHelper.GetFormatter(typeof (T));
    }
  }
}
