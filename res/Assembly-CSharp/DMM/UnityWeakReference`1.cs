// Decompiled with JetBrains decompiler
// Type: UnityWeakReference`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using UnityEngine;

#nullable disable
internal class UnityWeakReference<T> : WeakReference where T : Object
{
  public UnityWeakReference(T target)
    : base((object) target)
  {
  }

  public override bool IsAlive => Object.op_Inequality((Object) this.Target, (Object) null);

  public T Target
  {
    get => base.Target as T;
    set => this.Target = (object) value;
  }

  public bool TryGetTarget(out T target)
  {
    target = base.Target as T;
    return Object.op_Inequality((Object) target, (Object) null);
  }
}
