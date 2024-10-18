// Decompiled with JetBrains decompiler
// Type: MessagePack.Resolvers.PrimitiveObjectResolver
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Formatters;

#nullable disable
namespace MessagePack.Resolvers
{
  public sealed class PrimitiveObjectResolver : IFormatterResolver
  {
    public static IFormatterResolver Instance = (IFormatterResolver) new PrimitiveObjectResolver();

    private PrimitiveObjectResolver()
    {
    }

    public IMessagePackFormatter<T> GetFormatter<T>()
    {
      return PrimitiveObjectResolver.FormatterCache<T>.formatter;
    }

    private static class FormatterCache<T>
    {
      public static readonly IMessagePackFormatter<T> formatter = (object) typeof (T) != (object) typeof (object) ? (IMessagePackFormatter<T>) null : (IMessagePackFormatter<T>) PrimitiveObjectFormatter.Instance;
    }
  }
}
