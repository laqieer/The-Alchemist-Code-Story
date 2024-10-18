// Decompiled with JetBrains decompiler
// Type: SceneManagerHelper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine.SceneManagement;

#nullable disable
public class SceneManagerHelper
{
  public static string ActiveSceneName
  {
    get
    {
      Scene activeScene = SceneManager.GetActiveScene();
      return ((Scene) ref activeScene).name;
    }
  }

  public static int ActiveSceneBuildIndex
  {
    get
    {
      Scene activeScene = SceneManager.GetActiveScene();
      return ((Scene) ref activeScene).buildIndex;
    }
  }
}
