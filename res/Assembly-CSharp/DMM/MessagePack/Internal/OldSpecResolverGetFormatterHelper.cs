// Decompiled with JetBrains decompiler
// Type: MessagePack.Internal.OldSpecResolverGetFormatterHelper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Formatters;
using System;

#nullable disable
namespace MessagePack.Internal
{
  internal static class OldSpecResolverGetFormatterHelper
  {
    internal static object GetFormatter(Type t)
    {
      if ((object) t == (object) typeof (string))
        return (object) OldSpecStringFormatter.Instance;
      if ((object) t == (object) typeof (string[]))
        return (object) new ArrayFormatter<string>();
      return (object) t == (object) typeof (byte[]) ? (object) OldSpecBinaryFormatter.Instance : (object) null;
    }
  }
}
