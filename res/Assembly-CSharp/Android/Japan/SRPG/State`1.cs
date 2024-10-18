// Decompiled with JetBrains decompiler
// Type: SRPG.State`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
