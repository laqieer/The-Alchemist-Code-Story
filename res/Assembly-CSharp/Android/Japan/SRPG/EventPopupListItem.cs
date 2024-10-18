// Decompiled with JetBrains decompiler
// Type: SRPG.EventPopupListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

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
        if ((UnityEngine.Object) this.BannerImage != (UnityEngine.Object) null)
        {
          EventBanner2 component = this.BannerImage.GetComponent<EventBanner2>();
          if ((UnityEngine.Object) component != (UnityEngine.Object) null)
          {
            DataSource.Bind<BannerParam>(this.BannerImage.gameObject, this.m_Param, false);
            component.Refresh();
          }
        }
        if ((UnityEngine.Object) this.EndAtText != (UnityEngine.Object) null)
        {
          this.EndAtText.gameObject.SetActive(false);
          if (this.m_Param != null && !string.IsNullOrEmpty(this.m_Param.end_at))
          {
            this.EndAtText.text = this.m_Param.end_at + " まで";
            this.EndAtText.gameObject.SetActive(true);
          }
        }
        if (!((UnityEngine.Object) this.MessageText != (UnityEngine.Object) null))
          return;
        this.MessageText.gameObject.SetActive(false);
        if (this.m_Param == null || string.IsNullOrEmpty(this.m_Param.message))
          return;
        this.MessageText.text = this.m_Param.message;
        this.MessageText.gameObject.SetActive(true);
      }
    }
  }
}
