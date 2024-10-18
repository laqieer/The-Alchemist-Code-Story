// Decompiled with JetBrains decompiler
// Type: SRPG.NetworkErrorWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
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
      if (Object.op_Inequality((Object) this.Title, (Object) null))
        this.Title.text = LocalizedText.Get("embed.CONN_ERR");
      if (Object.op_Inequality((Object) this.StatusCode, (Object) null))
      {
        if (GameUtility.IsDebugBuild)
        {
          this.StatusCode.text = LocalizedText.Get("embed.CONN_ERRCODE", (object) Network.ErrCode.ToString());
          ((Component) this.StatusCode).gameObject.SetActive(true);
        }
        else
          ((Component) this.StatusCode).gameObject.SetActive(false);
      }
      if (Object.op_Inequality((Object) this.Message, (Object) null))
      {
        if (string.IsNullOrEmpty(Network.ErrMsg))
          this.Message.text = LocalizedText.Get("embed.APP_REBOOT", (object) Network.ErrCode.ToString());
        else
          this.Message.text = Network.ErrMsg;
      }
      if (!Object.op_Inequality((Object) this.m_Button, (Object) null))
        return;
      // ISSUE: method pointer
      ((UnityEvent) this.m_Button.onClick).AddListener(new UnityAction((object) this, __methodptr(OnClick)));
    }

    private void OnClick()
    {
      if (Network.ErrCode != Network.EErrCode.Authorize)
        return;
      MonoSingleton<GameManager>.Instance.ResetAuth();
    }

    public void OpenMaintenanceSite() => Application.OpenURL(Network.SiteHost);

    public void OpenVersionUpSite()
    {
      string str = Network.SiteHost;
      if (string.IsNullOrEmpty(str))
        str = Network.DefaultSiteHost;
      Application.OpenURL(str);
    }

    public void OpenStore()
    {
    }
  }
}
