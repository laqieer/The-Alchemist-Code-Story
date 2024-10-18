// Decompiled with JetBrains decompiler
// Type: Gsc.Network.Data.Entity`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace Gsc.Network.Data
{
  public abstract class Entity<T> : IEntity, IObject where T : Entity<T>
  {
    private uint ver;

    IEntity IEntity.Clone()
    {
      return (IEntity) this.Clone();
    }

    public string pk { get; protected set; }

    public abstract void Update();

    public abstract void ResolveRefs();

    public T Clone()
    {
      return (T) this.MemberwiseClone();
    }

    protected bool IsUpdatedOnce()
    {
      bool flag = (int) this.ver != (int) EntityRepository.ver;
      this.ver = EntityRepository.ver;
      return flag;
    }
  }
}
