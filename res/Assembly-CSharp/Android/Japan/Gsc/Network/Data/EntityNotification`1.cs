// Decompiled with JetBrains decompiler
// Type: Gsc.Network.Data.EntityNotification`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
