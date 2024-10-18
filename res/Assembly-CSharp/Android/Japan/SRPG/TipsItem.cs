// Decompiled with JetBrains decompiler
// Type: SRPG.TipsItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

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
      if ((UnityEngine.Object) this.BadgeObj != (UnityEngine.Object) null)
        this.BadgeObj.SetActive(!this.IsHidden && !this.IsCompleted);
      if ((UnityEngine.Object) this.CompleteObj != (UnityEngine.Object) null)
        this.CompleteObj.SetActive(!this.IsHidden && this.IsCompleted);
      if ((UnityEngine.Object) this.TitleObj != (UnityEngine.Object) null)
        this.TitleObj.text = this.Title;
      if ((UnityEngine.Object) this.OverayImageObj != (UnityEngine.Object) null)
        this.OverayImageObj.SetActive(this.IsHidden);
      if (!((UnityEngine.Object) this.SelfButton != (UnityEngine.Object) null))
        return;
      this.SelfButton.interactable = !this.IsHidden;
    }
  }
}
