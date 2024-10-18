// Decompiled with JetBrains decompiler
// Type: GR.Singleton`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;

namespace GR
{
  public abstract class Singleton<T> where T : class, new()
  {
    private static T instance_;

    public static T Instance
    {
      get
      {
        if ((object) Singleton<T>.instance_ == null)
          Singleton<T>.instance_ = Activator.CreateInstance<T>();
        return Singleton<T>.instance_;
      }
    }
  }
}
