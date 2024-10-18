// Decompiled with JetBrains decompiler
// Type: UnityWeakReference`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

internal class UnityWeakReference<T> : WeakReference where T : Object
{
  public UnityWeakReference(T target)
    : base((object) target)
  {
  }

  public override bool IsAlive
  {
    get
    {
      return (Object) this.Target != (Object) null;
    }
  }

  public T Target
  {
    get
    {
      return base.Target as T;
    }
    set
    {
      this.Target = (object) value;
    }
  }

  public bool TryGetTarget(out T target)
  {
    target = base.Target as T;
    return (Object) target != (Object) null;
  }
}
