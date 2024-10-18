// Decompiled with JetBrains decompiler
// Type: TunagiScene
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class TunagiScene : MonoBehaviour
{
  public static bool AutoDeactivateScene;
  public static TunagiScene.LoadCompleteEvent OnLoadComplete;
  private static List<TunagiScene> mScenes = new List<TunagiScene>();
  public float ZMin;
  public float ZMax = 15f;

  public static int SceneCount => TunagiScene.mScenes.Count;

  public static void SetScenesActive(bool active)
  {
    for (int index = 0; index < TunagiScene.mScenes.Count; ++index)
      ((Component) TunagiScene.mScenes[index]).gameObject.SetActive(active);
  }

  private void Awake()
  {
    TunagiScene.mScenes.Add(this);
    if (TunagiScene.OnLoadComplete != null)
      TunagiScene.OnLoadComplete(this);
    if (!TunagiScene.AutoDeactivateScene)
      return;
    ((Component) this).gameObject.SetActive(false);
    TunagiScene.AutoDeactivateScene = false;
  }

  private void OnDestroy() => TunagiScene.mScenes.Remove(this);

  public static void PopFirstScene()
  {
    if (TunagiScene.mScenes.Count <= 0)
      return;
    Object.DestroyImmediate((Object) ((Component) TunagiScene.mScenes[0]).gameObject);
  }

  public static void DestroyAllScenes()
  {
    while (TunagiScene.mScenes.Count > 0)
      Object.DestroyImmediate((Object) ((Component) TunagiScene.mScenes[TunagiScene.mScenes.Count - 1]).gameObject);
  }

  public static TunagiScene LastScene
  {
    get
    {
      return TunagiScene.mScenes.Count > 0 ? TunagiScene.mScenes[TunagiScene.mScenes.Count - 1] : (TunagiScene) null;
    }
  }

  public static TunagiScene FirstScene
  {
    get => TunagiScene.mScenes.Count > 0 ? TunagiScene.mScenes[0] : (TunagiScene) null;
  }

  public delegate void LoadCompleteEvent(TunagiScene scene);
}
