// Decompiled with JetBrains decompiler
// Type: Gsc.Network.Data.Entity`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace Gsc.Network.Data
{
  public abstract class Entity<T> : IEntity, IObject where T : Entity<T>
  {
    private uint ver;

    public string pk { get; protected set; }

    public abstract void Update();

    public abstract void ResolveRefs();

    public T Clone()
    {
      return (T) this.MemberwiseClone();
    }

    IEntity IEntity.Clone()
    {
      return (IEntity) this.Clone();
    }

    protected bool IsUpdatedOnce()
    {
      bool flag = (int) this.ver != (int) EntityRepository.ver;
      this.ver = EntityRepository.ver;
      return flag;
    }
  }
}
