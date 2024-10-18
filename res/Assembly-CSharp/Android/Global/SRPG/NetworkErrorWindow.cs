// Decompiled with JetBrains decompiler
// Type: SRPG.NetworkErrorWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  public class NetworkErrorWindow : MonoBehaviour
  {
    public Text Title;
    public Text StatusCode;
    public Text Message;

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
          this.StatusCode.text = LocalizedText.Get("errorcode." + ((int) Network.ErrCode).ToString() + "_TITLE");
          this.StatusCode.gameObject.SetActive(true);
        }
        else
          this.StatusCode.gameObject.SetActive(false);
      }
      if ((UnityEngine.Object) this.Message != (UnityEngine.Object) null)
      {
        if (Network.MultiMessage != null)
        {
          string empty = string.Empty;
          string configLanguage = GameUtility.Config_Language;
          string str;
          if (configLanguage != null)
          {
            // ISSUE: reference to a compiler-generated field
            if (NetworkErrorWindow.\u003C\u003Ef__switch\u0024map1A == null)
            {
              // ISSUE: reference to a compiler-generated field
              NetworkErrorWindow.\u003C\u003Ef__switch\u0024map1A = new Dictionary<string, int>(3)
              {
                {
                  "french",
                  0
                },
                {
                  "german",
                  1
                },
                {
                  "spanish",
                  2
                }
              };
            }
            int num;
            // ISSUE: reference to a compiler-generated field
            if (NetworkErrorWindow.\u003C\u003Ef__switch\u0024map1A.TryGetValue(configLanguage, out num))
            {
              switch (num)
              {
                case 0:
                  str = Network.MultiMessage.French;
                  goto label_18;
                case 1:
                  str = Network.MultiMessage.German;
                  goto label_18;
                case 2:
                  str = Network.MultiMessage.Spanish;
                  goto label_18;
              }
            }
          }
          str = Network.MultiMessage.English;
label_18:
          this.Message.text = str;
        }
        else if (string.IsNullOrEmpty(Network.ErrMsg))
          this.Message.text = LocalizedText.Get("embed.APP_REBOOT", new object[1]
          {
            (object) Network.ErrCode.ToString()
          });
        else
          this.Message.text = LocalizedText.Get("errorcode." + ((int) Network.ErrCode).ToString() + "_MESSAGE");
      }
      Transform child = this.transform.FindChild("window/Button");
      if (!((UnityEngine.Object) child != (UnityEngine.Object) null))
        return;
      Button component = child.GetComponent<Button>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      component.onClick.AddListener(new UnityAction(this.OnClick));
    }

    private void OnClick()
    {
      if (Network.ErrCode != Network.EErrCode.Authorize)
        return;
      MonoSingleton<GameManager>.Instance.ResetAuth();
    }

    public void OpenMaintenanceSite()
    {
      Application.OpenURL(Network.OfficialUrl);
    }

    public void OpenVersionUpSite()
    {
      Application.OpenURL(Network.OfficialUrl);
    }

    public void OpenStore()
    {
      Application.OpenURL("market://details?id=sg.gumi.alchemistww");
    }
  }
}
