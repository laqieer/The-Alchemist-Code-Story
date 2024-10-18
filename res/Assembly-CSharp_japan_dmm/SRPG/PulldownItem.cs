// Decompiled with JetBrains decompiler
// Type: SRPG.PulldownItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class PulldownItem : MonoBehaviour
  {
    public Text Text;
    public Graphic Graphic;
    public int Value;
    public Image Overray;
    public GameObject LockObject;

    public virtual void OnStatusChanged(bool enabled)
    {
    }
  }
}
