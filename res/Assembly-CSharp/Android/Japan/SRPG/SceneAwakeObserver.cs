// Decompiled with JetBrains decompiler
// Type: SRPG.SceneAwakeObserver
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

namespace SRPG
{
  public static class SceneAwakeObserver
  {
    private static SceneAwakeObserver.SceneEvent mListeners = (SceneAwakeObserver.SceneEvent) (go => {});

    public static void ClearListeners()
    {
      SceneAwakeObserver.mListeners = (SceneAwakeObserver.SceneEvent) (go => {});
    }

    public static void AddListener(SceneAwakeObserver.SceneEvent listener)
    {
      SceneAwakeObserver.mListeners += listener;
    }

    public static void RemoveListener(SceneAwakeObserver.SceneEvent listener)
    {
      SceneAwakeObserver.mListeners -= listener;
    }

    public static void Invoke(GameObject scene)
    {
      if (SceneAwakeObserver.mListeners == null)
        return;
      Delegate[] invocationList = SceneAwakeObserver.mListeners.GetInvocationList();
      for (int index = 0; index < invocationList.Length; ++index)
      {
        if ((object) (invocationList[index].Target as UnityEngine.Object) == null || (UnityEngine.Object) invocationList[index].Target != (UnityEngine.Object) null)
          ((SceneAwakeObserver.SceneEvent) invocationList[index])(scene);
      }
    }

    public delegate void SceneEvent(GameObject sceneRoot);
  }
}
