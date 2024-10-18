// Decompiled with JetBrains decompiler
// Type: MessagePack.Unity.UnityResolver
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Formatters;

#nullable disable
namespace MessagePack.Unity
{
  public class UnityResolver : IFormatterResolver
  {
    public static IFormatterResolver Instance = (IFormatterResolver) new UnityResolver();

    private UnityResolver()
    {
    }

    public IMessagePackFormatter<T> GetFormatter<T>() => UnityResolver.FormatterCache<T>.formatter;

    private static class FormatterCache<T>
    {
      public static readonly IMessagePackFormatter<T> formatter = (IMessagePackFormatter<T>) UnityResolveryResolverGetFormatterHelper.GetFormatter(typeof (T));
    }
  }
}
