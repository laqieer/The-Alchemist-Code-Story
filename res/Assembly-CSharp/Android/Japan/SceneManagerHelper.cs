// Decompiled with JetBrains decompiler
// Type: SceneManagerHelper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
