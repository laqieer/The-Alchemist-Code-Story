// Decompiled with JetBrains decompiler
// Type: SRPG.TipsQuestItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class TipsQuestItem : MonoBehaviour
  {
    [SerializeField]
    private GameObject BadgeObj;
    [SerializeField]
    private GameObject CompleteObj;
    [SerializeField]
    private Text TitleTxt;
    [SerializeField]
    private Text NameTxt;
    public string Title;
    public string Name;
    public bool IsCompleted;

    public void UpdateContent()
    {
      if ((UnityEngine.Object) this.BadgeObj != (UnityEngine.Object) null)
        this.BadgeObj.SetActive(!this.IsCompleted);
      if ((UnityEngine.Object) this.CompleteObj != (UnityEngine.Object) null)
        this.CompleteObj.SetActive(this.IsCompleted);
      if ((UnityEngine.Object) this.TitleTxt != (UnityEngine.Object) null)
        this.TitleTxt.text = this.Title;
      if (!((UnityEngine.Object) this.NameTxt != (UnityEngine.Object) null))
        return;
      this.NameTxt.text = this.Name;
    }
  }
}
