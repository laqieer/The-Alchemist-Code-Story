// Decompiled with JetBrains decompiler
// Type: MessagePack.Internal.NativeDateTimeResolverGetFormatterHelper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Formatters;
using System;

#nullable disable
namespace MessagePack.Internal
{
  internal static class NativeDateTimeResolverGetFormatterHelper
  {
    internal static object GetFormatter(Type t)
    {
      if ((object) t == (object) typeof (DateTime))
        return (object) NativeDateTimeFormatter.Instance;
      if ((object) t == (object) typeof (DateTime?))
        return (object) new StaticNullableFormatter<DateTime>((IMessagePackFormatter<DateTime>) NativeDateTimeFormatter.Instance);
      return (object) t == (object) typeof (DateTime[]) ? (object) NativeDateTimeArrayFormatter.Instance : (object) null;
    }
  }
}
