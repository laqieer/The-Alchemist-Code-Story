// Decompiled with JetBrains decompiler
// Type: SRPG.NetworkErrorWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  public class NetworkErrorWindow : MonoBehaviour
  {
    [SerializeField]
    private Text Title;
    [SerializeField]
    private Text StatusCode;
    [SerializeField]
    private Text Message;
    [SerializeField]
    private Button m_Button;

    private void Awake()
    {
    }

    private void Start()
    {
      if ((UnityEngine.Object) this.Title != (UnityEngine.Object) null)
        this.Title.text = LocalizedText.Get("embed.CONN_ERR");
      if ((UnityEngine.Object) this.StatusCode != (UnityEngine.Object) null)
      {
        if (GameUtility.IsDebugBuild)
        {
          this.StatusCode.text = LocalizedText.Get("embed.CONN_ERRCODE", new object[1]
          {
            (object) Network.ErrCode.ToString()
          });
          this.StatusCode.gameObject.SetActive(true);
        }
        else
          this.StatusCode.gameObject.SetActive(false);
      }
      if ((UnityEngine.Object) this.Message != (UnityEngine.Object) null)
      {
        if (string.IsNullOrEmpty(Network.ErrMsg))
          this.Message.text = LocalizedText.Get("embed.APP_REBOOT", new object[1]
          {
            (object) Network.ErrCode.ToString()
          });
        else
          this.Message.text = Network.ErrMsg;
      }
      if (!((UnityEngine.Object) this.m_Button != (UnityEngine.Object) null))
        return;
      this.m_Button.onClick.AddListener(new UnityAction(this.OnClick));
    }

    private void OnClick()
    {
      if (Network.ErrCode != Network.EErrCode.Authorize)
        return;
      MonoSingleton<GameManager>.Instance.ResetAuth();
    }

    public void OpenMaintenanceSite()
    {
      Application.OpenURL(Network.SiteHost);
    }

    public void OpenVersionUpSite()
    {
      Application.OpenURL(Network.SiteHost);
    }

    public void OpenStore()
    {
    }
  }
}
