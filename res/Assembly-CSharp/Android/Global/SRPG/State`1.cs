// Decompiled with JetBrains decompiler
// Type: SRPG.State`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
