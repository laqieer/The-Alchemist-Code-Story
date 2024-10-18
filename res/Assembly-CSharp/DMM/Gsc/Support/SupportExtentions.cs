// Decompiled with JetBrains decompiler
// Type: Gsc.Support.SupportExtentions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace Gsc.Support
{
  public static class SupportExtentions
  {
    public static T Get<T>(this WeakReference self)
    {
      return self != null && self.IsAlive ? (T) self.Target : default (T);
    }

    public static bool TryGet<T>(this WeakReference self, out T value)
    {
      if (self != null && self.IsAlive)
      {
        value = (T) self.Target;
        return true;
      }
      value = default (T);
      return false;
    }

    public static WeakReference Set<T>(this WeakReference self, T obj)
    {
      if (self == null)
        self = new WeakReference((object) obj);
      else
        self.Target = (object) obj;
      return self;
    }
  }
}
