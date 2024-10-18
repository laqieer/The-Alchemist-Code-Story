// Decompiled with JetBrains decompiler
// Type: SRPG.State`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class State<T>
  {
    public T self;

    public virtual void Begin(T self)
    {
    }

    public virtual void Update(T self)
    {
    }

    public virtual void End(T self)
    {
    }

    public virtual void Command(T self, string cmd)
    {
    }
  }
}
