// Decompiled with JetBrains decompiler
// Type: SRPG.TipsItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class TipsItem : MonoBehaviour
  {
    [SerializeField]
    private GameObject BadgeObj;
    [SerializeField]
    private GameObject CompleteObj;
    [SerializeField]
    private Text TitleObj;
    [SerializeField]
    private GameObject OverayImageObj;
    [SerializeField]
    private Button SelfButton;
    public string Title;
    public bool IsCompleted;
    public bool IsHidden;

    public void UpdateContent()
    {
      if (Object.op_Inequality((Object) this.BadgeObj, (Object) null))
        this.BadgeObj.SetActive(!this.IsHidden && !this.IsCompleted);
      if (Object.op_Inequality((Object) this.CompleteObj, (Object) null))
        this.CompleteObj.SetActive(!this.IsHidden && this.IsCompleted);
      if (Object.op_Inequality((Object) this.TitleObj, (Object) null))
        this.TitleObj.text = this.Title;
      if (Object.op_Inequality((Object) this.OverayImageObj, (Object) null))
        this.OverayImageObj.SetActive(this.IsHidden);
      if (!Object.op_Inequality((Object) this.SelfButton, (Object) null))
        return;
      ((Selectable) this.SelfButton).interactable = !this.IsHidden;
    }
  }
}
