// Decompiled with JetBrains decompiler
// Type: SRPG.EventPopupListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class EventPopupListItem : MonoBehaviour
  {
    [SerializeField]
    private Image BannerImage;
    [SerializeField]
    private Text EndAtText;
    [SerializeField]
    private Text MessageText;
    private BannerParam m_Param;

    public void SetupBannerParam(BannerParam _param)
    {
      this.m_Param = _param;
      this.Refresh();
    }

    private void Refresh()
    {
      if (this.m_Param == null)
      {
        DebugUtility.LogError("イベントバナー情報がセットされていません.");
      }
      else
      {
        if (Object.op_Inequality((Object) this.BannerImage, (Object) null))
        {
          EventBanner2 component = ((Component) this.BannerImage).GetComponent<EventBanner2>();
          if (Object.op_Inequality((Object) component, (Object) null))
          {
            DataSource.Bind<BannerParam>(((Component) this.BannerImage).gameObject, this.m_Param);
            component.Refresh();
          }
        }
        if (Object.op_Inequality((Object) this.EndAtText, (Object) null))
        {
          ((Component) this.EndAtText).gameObject.SetActive(false);
          if (this.m_Param != null && !string.IsNullOrEmpty(this.m_Param.end_at))
          {
            this.EndAtText.text = this.m_Param.end_at + " まで";
            ((Component) this.EndAtText).gameObject.SetActive(true);
          }
        }
        if (!Object.op_Inequality((Object) this.MessageText, (Object) null))
          return;
        ((Component) this.MessageText).gameObject.SetActive(false);
        if (this.m_Param == null || string.IsNullOrEmpty(this.m_Param.message))
          return;
        this.MessageText.text = this.m_Param.message;
        ((Component) this.MessageText).gameObject.SetActive(true);
      }
    }
  }
}
