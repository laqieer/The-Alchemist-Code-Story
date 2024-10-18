// Decompiled with JetBrains decompiler
// Type: SRPG.TipsQuestItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
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
      if (Object.op_Inequality((Object) this.BadgeObj, (Object) null))
        this.BadgeObj.SetActive(!this.IsCompleted);
      if (Object.op_Inequality((Object) this.CompleteObj, (Object) null))
        this.CompleteObj.SetActive(this.IsCompleted);
      if (Object.op_Inequality((Object) this.TitleTxt, (Object) null))
        this.TitleTxt.text = this.Title;
      if (!Object.op_Inequality((Object) this.NameTxt, (Object) null))
        return;
      this.NameTxt.text = this.Name;
    }
  }
}
