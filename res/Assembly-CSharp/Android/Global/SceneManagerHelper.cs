// Decompiled with JetBrains decompiler
// Type: SceneManagerHelper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine.SceneManagement;

public class SceneManagerHelper
{
  public static string ActiveSceneName
  {
    get
    {
      return SceneManager.GetActiveScene().name;
    }
  }

  public static int ActiveSceneBuildIndex
  {
    get
    {
      return SceneManager.GetActiveScene().buildIndex;
    }
  }
}
