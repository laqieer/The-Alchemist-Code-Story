// Decompiled with JetBrains decompiler
// Type: OnClickLoadSomething
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
