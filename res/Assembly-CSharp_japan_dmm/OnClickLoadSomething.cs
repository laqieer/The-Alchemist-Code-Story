// Decompiled with JetBrains decompiler
// Type: OnClickLoadSomething
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.SceneManagement;

#nullable disable
public class OnClickLoadSomething : MonoBehaviour
{
  public OnClickLoadSomething.ResourceTypeOption ResourceTypeToLoad;
  public string ResourceToLoad;

  public void OnClick()
  {
    switch (this.ResourceTypeToLoad)
    {
      case OnClickLoadSomething.ResourceTypeOption.Scene:
        SceneManager.LoadScene(this.ResourceToLoad);
        break;
      case OnClickLoadSomething.ResourceTypeOption.Web:
        Application.OpenURL(this.ResourceToLoad);
        break;
    }
  }

  public enum ResourceTypeOption : byte
  {
    Scene,
    Web,
  }
}
