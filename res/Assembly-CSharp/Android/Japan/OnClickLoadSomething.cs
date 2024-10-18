// Decompiled with JetBrains decompiler
// Type: OnClickLoadSomething
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.SceneManagement;

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
