﻿// Decompiled with JetBrains decompiler
// Type: LoadRequest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoadRequest : IEnumerator
{
  public Coroutine StartCoroutine()
  {
    return AssetManager.Instance.StartCoroutine((IEnumerator) this);
  }

  public virtual Object asset
  {
    get
    {
      return (Object) null;
    }
  }

  public virtual bool isDone
  {
    get
    {
      return true;
    }
  }

  public void Reset()
  {
  }

  public virtual bool MoveNext()
  {
    return false;
  }

  public object Current
  {
    get
    {
      return (object) null;
    }
  }

  public virtual float progress
  {
    get
    {
      return 0.0f;
    }
  }

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
