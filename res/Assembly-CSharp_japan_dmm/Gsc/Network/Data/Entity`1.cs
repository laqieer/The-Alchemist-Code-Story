// Decompiled with JetBrains decompiler
// Type: Gsc.Network.Data.Entity`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace Gsc.Network.Data
{
  public abstract class Entity<T> : IEntity, IObject where T : Entity<T>
  {
    private uint ver;

    public string pk { get; protected set; }

    public abstract void Update();

    public abstract void ResolveRefs();

    public T Clone() => (T) this.MemberwiseClone();

    IEntity IEntity.Clone() => (IEntity) this.Clone();

    protected bool IsUpdatedOnce()
    {
      bool flag = (int) this.ver != (int) EntityRepository.ver;
      this.ver = EntityRepository.ver;
      return flag;
    }
  }
}
