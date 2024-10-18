﻿// Decompiled with JetBrains decompiler
// Type: SRPG.LoginNewsInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.NodeType("System/Login/LoginNewsInfo", 32741)]
  [FlowNode.Pin(1, "SetUrl", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "OpenNews", FlowNode.PinTypes.Output, 2)]
  public class LoginNewsInfo : FlowNode
  {
    [SerializeField]
    public GameObject mRootNewsInfoObj;
    [SerializeField]
    public Text mTitleText;
    private static LoginNewsInfo.JSON_PubInfo mPubinfo;
    private LoginNewsInfo.JSON_PubInfo mCurrentPubInfo;
    private const int TEXT_LIMIT_LENGTH = 52;
    private const int PIN_SET_URL = 1;
    private const int PIN_OPEN_NEWS = 2;
    private const string URL_KEY = "LOGIN_NEWS_URL";
    private static bool isShowNews;

    public static LoginNewsInfo.JSON_PubInfo Pubinfo
    {
      get
      {
        return LoginNewsInfo.mPubinfo;
      }
    }

    public string PageID
    {
      get
      {
        return "?id=" + this.mCurrentPubInfo.id;
      }
    }

    protected override void Awake()
    {
      LoginInfoParam[] activeLoginInfos = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetActiveLoginInfos();
      if (activeLoginInfos == null || activeLoginInfos.Length <= 0)
        this.mRootNewsInfoObj.SetActive(false);
      else if (LoginNewsInfo.mPubinfo == null)
      {
        this.mRootNewsInfoObj.SetActive(false);
      }
      else
      {
        this.mCurrentPubInfo = LoginNewsInfo.mPubinfo;
        LoginNewsInfo.isShowNews = true;
        this.mRootNewsInfoObj.SetActive(true);
        this.Refresh();
      }
    }

    protected override void OnDestroy()
    {
      LoginNewsInfo.isShowNews = false;
      LoginNewsInfo.mPubinfo = (LoginNewsInfo.JSON_PubInfo) null;
    }

    private void Refresh()
    {
      if (this.mCurrentPubInfo == null)
        return;
      this.mRootNewsInfoObj.SetActive(true);
      if (string.IsNullOrEmpty(this.mCurrentPubInfo.message))
        return;
      this.mTitleText.text = this.mCurrentPubInfo.message;
      float num = this.mTitleText.preferredHeight - this.mTitleText.rectTransform.sizeDelta.y;
      if ((double) num <= 0.0)
        return;
      bool flag = true;
      if (this.mTitleText.text[this.mTitleText.text.Length - 1] == '\n')
      {
        flag = false;
        this.mTitleText.text = this.mTitleText.text.Remove(this.mTitleText.text.Length - 1);
        num = this.mTitleText.preferredHeight - this.mTitleText.rectTransform.sizeDelta.y;
      }
      while ((double) num > 0.0)
      {
        this.mTitleText.text = this.mTitleText.text.Remove(this.mTitleText.text.Length - 1);
        num = this.mTitleText.preferredHeight - this.mTitleText.rectTransform.sizeDelta.y;
        flag = true;
      }
      if (!flag)
        return;
      this.mTitleText.text = this.mTitleText.text.Remove(this.mTitleText.text.Length - 1);
      this.mTitleText.text += string.Format(LocalizedText.Get("sys.TEXT_OVER_SUBSTITUTION"));
    }

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      if (this.mCurrentPubInfo == null)
      {
        FlowNode_Variable.Set("LOGIN_NEWS_URL", Network.NewsHost);
      }
      else
      {
        FlowNode_Variable.Set("LOGIN_NEWS_URL", this.PageID);
        this.ActivateOutputLinks(2);
      }
    }

    public static void SetPubInfo(LoginNewsInfo.JSON_PubInfo pubinfo)
    {
      if (pubinfo == null)
        return;
      if (LoginNewsInfo.mPubinfo != null)
        PlayerPrefsUtility.SetString(GameUtility.BEFORE_LOGIN_NEWS_INFO_TOKEN, LoginNewsInfo.mPubinfo.token, false);
      LoginNewsInfo.mPubinfo = pubinfo;
    }

    public static bool IsChangePubInfo()
    {
      if (LoginNewsInfo.mPubinfo == null || LoginNewsInfo.isShowNews)
        return false;
      string str = PlayerPrefsUtility.GetString(GameUtility.BEFORE_LOGIN_NEWS_INFO_TOKEN, string.Empty);
      return !string.IsNullOrEmpty(str) && !string.IsNullOrEmpty(LoginNewsInfo.mPubinfo.token) && str != LoginNewsInfo.Pubinfo.token;
    }

    public static void UpdateBeforePubInfo()
    {
      if (LoginNewsInfo.mPubinfo == null)
        return;
      PlayerPrefsUtility.SetString(GameUtility.BEFORE_LOGIN_NEWS_INFO_TOKEN, LoginNewsInfo.mPubinfo.token, false);
    }

    public class JSON_PubInfo
    {
      public string id;
      public string token;
      public string category;
      public string message;
    }
  }
}
