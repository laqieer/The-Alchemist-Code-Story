﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ChatLogItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class ChatLogItem : MonoBehaviour
  {
    public static readonly string CHAT_STAMP_IMG = "Stamps/StampTable";
    public readonly int STAMP_SIZE = 154;
    [SerializeField]
    private LayoutElement Element;
    [SerializeField]
    private GameObject Icon;
    [SerializeField]
    private RawImage LeftIcon;
    [SerializeField]
    private RawImage RightIcon;
    [SerializeField]
    private GameObject MessageIcon;
    [SerializeField]
    private GameObject MessageLog;
    [SerializeField]
    private GameObject MyMessageIcon;
    [SerializeField]
    private GameObject MyMessageLog;
    [SerializeField]
    private RectTransform AnyLogRoot;
    [SerializeField]
    private RectTransform MyLogRoot;
    [SerializeField]
    private GameObject MyStampObj;
    [SerializeField]
    private GameObject AnyStampObj;
    [SerializeField]
    private Text MyNameObj;
    [SerializeField]
    private Text MyFuIDObj;
    [SerializeField]
    private Text MyPostAtObj;
    [SerializeField]
    private Image MyStampImageObj;
    [SerializeField]
    private Text MyMessageTextObj;
    [SerializeField]
    private Text AnyNameObj;
    [SerializeField]
    private Text AnyFuIDObj;
    [SerializeField]
    private Text AnyPostAtObj;
    [SerializeField]
    private Image AnyStampImageObj;
    [SerializeField]
    private Text AnyMessageTextObj;
    [SerializeField]
    private GameObject SystemMessageRootObj;
    [SerializeField]
    private Text SystemMessageTextObj;
    private Transform mStampRoot;
    private RectTransform mLogRoot;
    private Image mLogImg;
    private ChatStampParam[] mStampParams;
    private bool IsStampSettings;
    private Coroutine mCoroutine;
    private GameObject mRoot;
    private Text mNameObj;
    private Text mPostAtObj;
    private Text mFuIDObj;
    private Image mStampImageObj;
    private Text mMessageObj;
    private ChatLogParam mChatLogParam;
    private static SpriteSheet mStampImages;
    private static bool IsStampLoaded;
    private static bool mStampLoaded;
    private static string mStampLanguage;

    public GameObject GetIcon
    {
      get
      {
        return this.Icon;
      }
    }

    public ChatLogParam ChatLogParam
    {
      get
      {
        return this.mChatLogParam;
      }
    }

    public void Awake()
    {
      if ((UnityEngine.Object) this.MessageIcon != (UnityEngine.Object) null)
        this.MessageIcon.SetActive(false);
      if ((UnityEngine.Object) this.MessageLog != (UnityEngine.Object) null)
        this.MessageLog.SetActive(false);
      if ((UnityEngine.Object) this.MyMessageIcon != (UnityEngine.Object) null)
        this.MyMessageIcon.SetActive(false);
      if (!((UnityEngine.Object) this.MyMessageLog != (UnityEngine.Object) null))
        return;
      this.MyMessageLog.SetActive(false);
    }

    private void Start()
    {
      this.SetupChatStampMaster();
      this.IsStampSettings = true;
      if (ChatLogItem.mStampLanguage != GameUtility.Config_Language)
      {
        ChatLogItem.mStampImages = (SpriteSheet) null;
        ChatLogItem.mStampLoaded = false;
      }
      if (!((UnityEngine.Object) ChatLogItem.mStampImages == (UnityEngine.Object) null) || ChatLogItem.mStampLoaded)
        return;
      this.StartCoroutine(this.LoadStampImages());
    }

    public void OnDisable()
    {
      if (this.mCoroutine == null)
        return;
      this.StopCoroutine(this.mCoroutine);
      this.mCoroutine = (Coroutine) null;
    }

    public void Clear()
    {
      this.gameObject.SetActive(false);
      SRPG_Button component = this.GetIcon.GetComponent<SRPG_Button>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      component.onClick.RemoveAllListeners();
      this.mChatLogParam = (ChatLogParam) null;
    }

    public void SetParam(ChatLogParam param, SRPG_Button.ButtonClickEvent OnClickEvent)
    {
      if (param == null)
      {
        this.gameObject.SetActive(false);
        this.mChatLogParam = (ChatLogParam) null;
      }
      else
      {
        this.mChatLogParam = param;
        ChatWindow.MessageTemplateType type = ChatWindow.MessageTemplateType.OtherUser;
        if (MonoSingleton<GameManager>.Instance.Player.FUID == param.fuid)
          type = ChatWindow.MessageTemplateType.User;
        else if (string.IsNullOrEmpty(param.fuid))
          type = ChatWindow.MessageTemplateType.System;
        this.gameObject.SetActive(true);
        this.Refresh(param, type);
        SRPG_Button component = this.GetIcon.GetComponent<SRPG_Button>();
        if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
          return;
        component.onClick.RemoveAllListeners();
        if (!(param.fuid != MonoSingleton<GameManager>.Instance.Player.FUID))
          return;
        component.AddListener(OnClickEvent);
      }
    }

    public void Refresh(ChatLogParam param, ChatWindow.MessageTemplateType type)
    {
      if (param == null)
        return;
      if (this.mCoroutine != null)
      {
        this.StopCoroutine(this.mCoroutine);
        this.mCoroutine = (Coroutine) null;
      }
      if ((UnityEngine.Object) this.mRoot == (UnityEngine.Object) null)
      {
        if (!((UnityEngine.Object) this.transform.parent != (UnityEngine.Object) null))
          return;
        this.mRoot = this.transform.parent.gameObject;
      }
      this.MessageIcon.SetActive(false);
      this.MessageLog.SetActive(false);
      this.MyMessageIcon.SetActive(false);
      this.MyMessageLog.SetActive(false);
      this.SystemMessageRootObj.SetActive(false);
      switch (type)
      {
        case ChatWindow.MessageTemplateType.OtherUser:
          this.MessageIcon.SetActive(true);
          this.MessageLog.SetActive(true);
          this.mStampRoot = !((UnityEngine.Object) this.AnyStampObj != (UnityEngine.Object) null) ? (Transform) null : this.AnyStampObj.transform;
          this.mNameObj = this.AnyNameObj;
          this.mFuIDObj = this.AnyFuIDObj;
          this.mPostAtObj = this.AnyPostAtObj;
          this.mStampImageObj = this.AnyStampImageObj;
          this.mMessageObj = this.AnyMessageTextObj;
          this.mLogRoot = this.AnyLogRoot;
          this.mLogImg = this.AnyLogRoot.GetComponent<Image>();
          break;
        case ChatWindow.MessageTemplateType.User:
          this.MyMessageIcon.SetActive(true);
          this.MyMessageLog.SetActive(true);
          this.mStampRoot = !((UnityEngine.Object) this.MyStampObj != (UnityEngine.Object) null) ? (Transform) null : this.MyStampObj.transform;
          this.mNameObj = this.MyNameObj;
          this.mFuIDObj = this.MyFuIDObj;
          this.mPostAtObj = this.MyPostAtObj;
          this.mStampImageObj = this.MyStampImageObj;
          this.mMessageObj = this.MyMessageTextObj;
          this.mLogRoot = this.MyLogRoot;
          this.mLogImg = this.MyLogRoot.GetComponent<Image>();
          break;
        case ChatWindow.MessageTemplateType.System:
          this.SystemMessageRootObj.SetActive(true);
          this.SystemMessageTextObj.text = param.message;
          this.mCoroutine = this.StartCoroutine(this.RefreshTextLine(param.message));
          return;
      }
      if ((UnityEngine.Object) this.Icon != (UnityEngine.Object) null && (UnityEngine.Object) this.LeftIcon != (UnityEngine.Object) null && (UnityEngine.Object) this.RightIcon != (UnityEngine.Object) null)
      {
        RawImage target = type != ChatWindow.MessageTemplateType.User ? this.LeftIcon : this.RightIcon;
        UnitParam unitParam = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(param.icon);
        if (unitParam != null)
        {
          if (!string.IsNullOrEmpty(param.skin_iname) && (UnityEngine.Object) target != (UnityEngine.Object) null)
          {
            ArtifactParam skin = Array.Find<ArtifactParam>(MonoSingleton<GameManager>.Instance.MasterParam.Artifacts.ToArray(), (Predicate<ArtifactParam>) (p => p.iname == param.skin_iname));
            MonoSingleton<GameManager>.Instance.ApplyTextureAsync(target, AssetPath.UnitSkinIconSmall(unitParam, skin, param.job_iname));
          }
          else
            MonoSingleton<GameManager>.Instance.ApplyTextureAsync(target, AssetPath.UnitIconSmall(unitParam, param.job_iname));
        }
      }
      if ((UnityEngine.Object) this.mNameObj != (UnityEngine.Object) null)
        this.mNameObj.text = param.name;
      if ((UnityEngine.Object) this.mFuIDObj != (UnityEngine.Object) null)
        this.mFuIDObj.text = LocalizedText.Get("sys.TEXT_CHAT_FUID", new object[1]
        {
          (object) param.fuid.Substring(param.fuid.Length - 4, 4)
        });
      if ((UnityEngine.Object) this.mPostAtObj != (UnityEngine.Object) null)
        this.mPostAtObj.text = ChatLogItem.GetPostAt(param.posted_at);
      if (param.message_type == (byte) 1)
      {
        if (!((UnityEngine.Object) this.mRoot != (UnityEngine.Object) null) || !this.mRoot.activeInHierarchy)
          return;
        if ((UnityEngine.Object) this.mStampRoot != (UnityEngine.Object) null)
          this.mStampRoot.gameObject.SetActive(false);
        this.mCoroutine = this.StartCoroutine(this.RefreshTextLine(param.message));
      }
      else
      {
        if (param.message_type != (byte) 2 || !((UnityEngine.Object) this.mRoot != (UnityEngine.Object) null) || !this.mRoot.activeInHierarchy)
          return;
        if ((UnityEngine.Object) this.mStampRoot != (UnityEngine.Object) null)
          this.mStampRoot.gameObject.SetActive(true);
        if ((UnityEngine.Object) this.Element != (UnityEngine.Object) null)
        {
          int stampSize = this.STAMP_SIZE;
          VerticalLayoutGroup component = this.mLogRoot.GetComponent<VerticalLayoutGroup>();
          this.Element.minHeight = (float) (stampSize + component.padding.top + component.padding.bottom + (int) Mathf.Abs(this.mLogRoot.anchoredPosition.y));
        }
        this.mLogImg.enabled = false;
        this.mCoroutine = this.StartCoroutine(this.RefreshStamp(param.stamp_id));
      }
    }

    public static string GetPostAt(long postat)
    {
      string empty = string.Empty;
      TimeSpan timeSpan = TimeManager.ServerTime - GameUtility.UnixtimeToLocalTime(postat);
      int days = timeSpan.Days;
      int hours = timeSpan.Hours;
      int minutes = timeSpan.Minutes;
      int seconds = timeSpan.Seconds;
      string str;
      if (days > 0)
        str = LocalizedText.Get("sys.CHAT_POSTAT_DAY", new object[1]
        {
          (object) days.ToString()
        });
      else if (hours > 0)
        str = LocalizedText.Get("sys.CHAT_POSTAT_HOUR", new object[1]
        {
          (object) hours.ToString()
        });
      else if (minutes > 0)
        str = LocalizedText.Get("sys.CHAT_POSTAT_MINUTE", new object[1]
        {
          (object) minutes.ToString()
        });
      else if (seconds > 10)
        str = LocalizedText.Get("sys.CHAT_POSTAT_SECOND", new object[1]
        {
          (object) seconds.ToString()
        });
      else
        str = LocalizedText.Get("sys.CHAT_POSTAT_NOW");
      return str;
    }

    [DebuggerHidden]
    private IEnumerator RefreshTextLine(string text)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ChatLogItem.\u003CRefreshTextLine\u003Ec__IteratorEF() { text = text, \u003C\u0024\u003Etext = text, \u003C\u003Ef__this = this };
    }

    [DebuggerHidden]
    private IEnumerator RefreshStamp(int id)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ChatLogItem.\u003CRefreshStamp\u003Ec__IteratorF0() { id = id, \u003C\u0024\u003Eid = id, \u003C\u003Ef__this = this };
    }

    private bool SetupChatStampMaster()
    {
      string src = AssetManager.LoadTextData(ChatStamp.CHAT_STAMP_MASTER_PATH);
      if (string.IsNullOrEmpty(src))
        return false;
      try
      {
        JSON_ChatStampParam[] jsonArray = JSONParser.parseJSONArray<JSON_ChatStampParam>(src);
        if (jsonArray == null)
          throw new InvalidJSONException();
        this.mStampParams = new ChatStampParam[jsonArray.Length];
        for (int index = 0; index < jsonArray.Length; ++index)
        {
          ChatStampParam chatStampParam = new ChatStampParam();
          if (chatStampParam.Deserialize(jsonArray[index]))
            this.mStampParams[index] = chatStampParam;
        }
      }
      catch
      {
        return false;
      }
      return true;
    }

    [DebuggerHidden]
    private IEnumerator LoadStampImages()
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      ChatLogItem.\u003CLoadStampImages\u003Ec__IteratorF1 imagesCIteratorF1 = new ChatLogItem.\u003CLoadStampImages\u003Ec__IteratorF1();
      return (IEnumerator) imagesCIteratorF1;
    }
  }
}
