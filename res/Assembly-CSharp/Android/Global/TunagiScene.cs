// Decompiled with JetBrains decompiler
// Type: TunagiScene
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class TunagiScene : MonoBehaviour
{
  private static List<TunagiScene> mScenes = new List<TunagiScene>();
  public float ZMax = 15f;
  public static bool AutoDeactivateScene;
  public static TunagiScene.LoadCompleteEvent OnLoadComplete;
  public float ZMin;

  public static int SceneCount
  {
    get
    {
      return TunagiScene.mScenes.Count;
    }
  }

  public static void SetScenesActive(bool active)
  {
    for (int index = 0; index < TunagiScene.mScenes.Count; ++index)
      TunagiScene.mScenes[index].gameObject.SetActive(active);
  }

  private void Awake()
  {
    TunagiScene.mScenes.Add(this);
    if (TunagiScene.OnLoadComplete != null)
      TunagiScene.OnLoadComplete(this);
    if (!TunagiScene.AutoDeactivateScene)
      return;
    this.gameObject.SetActive(false);
    TunagiScene.AutoDeactivateScene = false;
  }

  private void OnDestroy()
  {
    TunagiScene.mScenes.Remove(this);
  }

  public static void PopFirstScene()
  {
    if (TunagiScene.mScenes.Count <= 0)
      return;
    Object.DestroyImmediate((Object) TunagiScene.mScenes[0].gameObject);
  }

  public static void DestroyAllScenes()
  {
    while (TunagiScene.mScenes.Count > 0)
      Object.DestroyImmediate((Object) TunagiScene.mScenes[TunagiScene.mScenes.Count - 1].gameObject);
  }

  public static TunagiScene LastScene
  {
    get
    {
      if (TunagiScene.mScenes.Count > 0)
        return TunagiScene.mScenes[TunagiScene.mScenes.Count - 1];
      return (TunagiScene) null;
    }
  }

  public static TunagiScene FirstScene
  {
    get
    {
      if (TunagiScene.mScenes.Count > 0)
        return TunagiScene.mScenes[0];
      return (TunagiScene) null;
    }
  }

  public delegate void LoadCompleteEvent(TunagiScene scene);
}
