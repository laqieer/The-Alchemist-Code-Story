// Decompiled with JetBrains decompiler
// Type: Gsc.Network.Data.EntityNotification`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Components;

#nullable disable
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
