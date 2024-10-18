// Decompiled with JetBrains decompiler
// Type: LoadRequest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class LoadRequest : IEnumerator
{
  public Coroutine StartCoroutine() => AssetManager.Instance.StartCoroutine((IEnumerator) this);

  public virtual Object asset => (Object) null;

  public virtual bool isDone => true;

  public void Reset()
  {
  }

  public virtual bool MoveNext() => false;

  public object Current => (object) null;

  public virtual float progress => 0.0f;

  public virtual void KeepSourceAlive()
  {
  }

  public static void UntrackTextComponents(Object obj)
  {
    if (!(obj is GameObject))
      return;
    foreach (Text componentsInChild in (obj as GameObject).GetComponentsInChildren<Text>(true))
      FontUpdateTracker.UntrackText(componentsInChild);
  }
}
