// Decompiled with JetBrains decompiler
// Type: Gsc.Network.Data.EntityNotification`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using Gsc.Components;

namespace Gsc.Network.Data
{
  public class EntityNotification<T> : INotification where T : Gsc.Network.Data.Entity<T>
  {
    public readonly T Entity;
    public readonly EntityNotificationType NotificationType;

    public EntityNotification(T entity, EntityNotificationType type)
    {
      this.Entity = entity;
      this.NotificationType = type;
    }
  }
}
