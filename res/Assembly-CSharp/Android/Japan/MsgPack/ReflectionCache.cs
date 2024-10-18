// Decompiled with JetBrains decompiler
// Type: MsgPack.ReflectionCache
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace MsgPack
{
  public static class ReflectionCache
  {
    private static Dictionary<Type, ReflectionCacheEntry> _cache = new Dictionary<Type, ReflectionCacheEntry>();

    public static ReflectionCacheEntry Lookup(Type type)
    {
      lock ((object) ReflectionCache._cache)
      {
        ReflectionCacheEntry reflectionCacheEntry;
        if (ReflectionCache._cache.TryGetValue(type, out reflectionCacheEntry))
          return reflectionCacheEntry;
      }
      ReflectionCacheEntry reflectionCacheEntry1 = new ReflectionCacheEntry(type);
      lock ((object) ReflectionCache._cache)
        ReflectionCache._cache[type] = reflectionCacheEntry1;
      return reflectionCacheEntry1;
    }

    public static void RemoveCache(Type type)
    {
      lock ((object) ReflectionCache._cache)
        ReflectionCache._cache.Remove(type);
    }

    public static void Clear()
    {
      lock ((object) ReflectionCache._cache)
        ReflectionCache._cache.Clear();
    }
  }
}
