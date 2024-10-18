// Decompiled with JetBrains decompiler
// Type: GR.Singleton`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
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
          Singleton<T>.instance_ = new T();
        return Singleton<T>.instance_;
      }
    }
  }
}
