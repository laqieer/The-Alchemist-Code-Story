// Decompiled with JetBrains decompiler
// Type: SRPG.ChatLogItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class ChatLogItem : MonoBehaviour
  {
    [SerializeField]
    private SRPG_Button LeftIconRoot;
    [SerializeField]
    private SRPG_Button RightIconRoot;
    [SerializeField]
    private RawImage LeftIcon;
    [SerializeField]
    private RawImage RightIcon;
    [SerializeField]
    private GameObject MessageRoot;
    [SerializeField]
    private Image LeftMessageBgImage;
    [SerializeField]
    private Image RightMessageBgImage;
    [SerializeField]
    private GameObject LeftMessageStatus;
    [SerializeField]
    private GameObject RightMessageStatus;
    [SerializeField]
    private Text LeftMessageTextObj;
    [SerializeField]
    private Text RightMessageTextObj;
    [SerializeField]
    private GameObject LeftStampObj;
    [SerializeField]
    private GameObject RightStampObj;
    [SerializeField]
    private Text RightNameObj;
    [SerializeField]
    private Text RightFuIDObj;
    [SerializeField]
    private Text RightPostAtObj;
    [SerializeField]
    private Image RightStampImageObj;
    [SerializeField]
    private Text LeftNameObj;
    [SerializeField]
    private Text LeftFuIDObj;
    [SerializeField]
    private Text LeftPostAtObj;
    [SerializeField]
    private Image LeftStampImageObj;
    [SerializeField]
    private GameObject SystemMessageRootObj;
    [SerializeField]
    private Text SystemMessageTextObj;
    [SerializeField]
    private Button GuildInviteButton;
    [SerializeField]
    private Text GuildInviteMessage;
    [SerializeField]
    private GameObject GuildRaidPartyParent;
    [SerializeField]
    private Text GuildRaidPartyMessage;
    [SerializeField]
    private GameObject OfficialMessageRootObj;
    [SerializeField]
    private Text OfficialMessageTitleObj;
    [SerializeField]
    private Text OfficialMessageTextObj;
    [SerializeField]
    private WebHelpButton OfficialJumpURLObj;
    private ChatStampParam[] mStampParams;
    private bool IsStampSettings;
    private Coroutine mCoroutine;
    private ChatLogParam mChatLogParam;
    public readonly int STAMP_SIZE = 154;

    public ChatLogParam ChatLogParam => this.mChatLogParam;

    public void Awake()
    {
      GameUtility.SetGameObjectActive((Component) this.LeftIconRoot, false);
      GameUtility.SetGameObjectActive((Component) this.RightIconRoot, false);
      GameUtility.SetGameObjectActive(this.MessageRoot, false);
    }

    private void Start()
    {
      this.SetupChatStampMaster();
      this.IsStampSettings = true;
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
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.LeftIconRoot, (UnityEngine.Object) null))
        ((UnityEventBase) this.LeftIconRoot.onClick).RemoveAllListeners();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RightIconRoot, (UnityEngine.Object) null))
        ((UnityEventBase) this.RightIconRoot.onClick).RemoveAllListeners();
      this.mChatLogParam = (ChatLogParam) null;
      ((Component) this).gameObject.SetActive(false);
    }

    public void SetParam(ChatLogParam param, SRPG_Button.ButtonClickEvent OnClickEvent)
    {
      if (param == null)
      {
        ((Component) this).gameObject.SetActive(false);
        this.mChatLogParam = (ChatLogParam) null;
      }
      else
      {
        this.mChatLogParam = param;
        ChatWindow.MessageTemplateType type = ChatWindow.MessageTemplateType.OtherUser;
        if (MonoSingleton<GameManager>.Instance.Player.FUID == param.fuid)
          type = ChatWindow.MessageTemplateType.User;
        else if (param is ChatLogOfficialParam)
          type = ChatWindow.MessageTemplateType.Official;
        else if (string.IsNullOrEmpty(param.fuid))
          type = ChatWindow.MessageTemplateType.System;
        ((Component) this).gameObject.SetActive(true);
        this.Refresh(param, type);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.LeftIconRoot, (UnityEngine.Object) null))
        {
          ((UnityEventBase) this.LeftIconRoot.onClick).RemoveAllListeners();
          if (param.fuid != MonoSingleton<GameManager>.Instance.Player.FUID)
            this.LeftIconRoot.AddListener(OnClickEvent);
        }
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RightIconRoot, (UnityEngine.Object) null))
          return;
        ((UnityEventBase) this.RightIconRoot.onClick).RemoveAllListeners();
        if (!(param.fuid != MonoSingleton<GameManager>.Instance.Player.FUID))
          return;
        this.RightIconRoot.AddListener(OnClickEvent);
      }
    }

    private void RefreshView(ChatWindow.MessageTemplateType view_type, ChatLogParam chat_param)
    {
      switch (view_type)
      {
        case ChatWindow.MessageTemplateType.OtherUser:
          this.Setup_Other(chat_param);
          break;
        case ChatWindow.MessageTemplateType.User:
          this.Setup_Self(chat_param);
          break;
        case ChatWindow.MessageTemplateType.System:
          this.Setup_SystemMessage(chat_param);
          break;
        case ChatWindow.MessageTemplateType.Official:
          this.Setup_OfficialMessage(chat_param as ChatLogOfficialParam);
          break;
      }
    }

    private void DisableAll()
    {
      GameUtility.SetGameObjectActive(this.MessageRoot, false);
      GameUtility.SetGameObjectActive((Component) this.LeftMessageBgImage, false);
      GameUtility.SetGameObjectActive((Component) this.RightMessageBgImage, false);
      GameUtility.SetGameObjectActive((Component) this.LeftMessageTextObj, false);
      GameUtility.SetGameObjectActive((Component) this.RightMessageTextObj, false);
      GameUtility.SetGameObjectActive((Component) this.LeftIconRoot, false);
      GameUtility.SetGameObjectActive((Component) this.RightIconRoot, false);
      GameUtility.SetGameObjectActive(this.LeftMessageStatus, false);
      GameUtility.SetGameObjectActive(this.RightMessageStatus, false);
      GameUtility.SetGameObjectActive(this.LeftStampObj, false);
      GameUtility.SetGameObjectActive(this.RightStampObj, false);
      GameUtility.SetGameObjectActive(this.SystemMessageRootObj, false);
      GameUtility.SetGameObjectActive((Component) this.GuildInviteButton, false);
      GameUtility.SetGameObjectActive(this.GuildRaidPartyParent, false);
      GameUtility.SetGameObjectActive(this.OfficialMessageRootObj, false);
    }

    private void Setup_Other(ChatLogParam param)
    {
      switch (param.messageType)
      {
        case ChatLogParam.eChatMessageType.MESSAGE:
          this.Setup_OtherMessage(param);
          break;
        case ChatLogParam.eChatMessageType.STAMP:
          this.Setup_OtherStamp(param);
          break;
        case ChatLogParam.eChatMessageType.GUILD_INVITE:
          this.Setup_OtherGuildInvite(param);
          break;
        case ChatLogParam.eChatMessageType.GUILDRAID_PARTY:
          this.Setup_OtherGuildRaidReport(param);
          break;
      }
    }

    private void Setup_Self(ChatLogParam param)
    {
      switch (param.messageType)
      {
        case ChatLogParam.eChatMessageType.MESSAGE:
          this.Setup_SelfMessage(param);
          break;
        case ChatLogParam.eChatMessageType.STAMP:
          this.Setup_SelfStamp(param);
          break;
        case ChatLogParam.eChatMessageType.GUILD_INVITE:
          this.Setup_SelfGuildInvite(param);
          break;
        case ChatLogParam.eChatMessageType.GUILDRAID_PARTY:
          this.Setup_SelfGuildRaidReport(param);
          break;
      }
    }

    private void Setup_SystemMessage(ChatLogParam param)
    {
      GameUtility.SetGameObjectActive(this.SystemMessageRootObj, true);
      this.mCoroutine = this.StartCoroutine(this.RefreshTextLine(param.message, this.SystemMessageTextObj, (Image) null, true));
    }

    private void Setup_OfficialMessage(ChatLogOfficialParam param)
    {
      GameUtility.SetGameObjectActive(this.OfficialMessageRootObj, true);
      this.mCoroutine = this.StartCoroutine(this.RefreshOfficialTextLine(param.title, param.message, param.url));
    }

    private void Setup_OtherMessage(ChatLogParam param)
    {
      GameUtility.SetGameObjectActive(this.MessageRoot, true);
      GameUtility.SetGameObjectActive((Component) this.LeftIconRoot, true);
      GameUtility.SetGameObjectActive(this.LeftMessageStatus, true);
      this.LeftNameObj.text = param.name;
      this.LeftPostAtObj.text = ChatLogItem.GetPostAt(param.posted_at);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.LeftFuIDObj, (UnityEngine.Object) null))
        this.LeftFuIDObj.text = LocalizedText.Get("sys.TEXT_CHAT_FUID", (object) param.fuid.Substring(param.fuid.Length - 4, 4));
      this.Setup_UnitIcon(param, this.LeftIcon);
      this.mCoroutine = this.StartCoroutine(this.RefreshTextLine(param.message, this.LeftMessageTextObj, this.LeftMessageBgImage));
    }

    private void Setup_SelfMessage(ChatLogParam param)
    {
      GameUtility.SetGameObjectActive(this.MessageRoot, true);
      GameUtility.SetGameObjectActive((Component) this.RightIconRoot, true);
      GameUtility.SetGameObjectActive(this.RightMessageStatus, true);
      this.RightNameObj.text = param.name;
      this.RightPostAtObj.text = ChatLogItem.GetPostAt(param.posted_at);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RightFuIDObj, (UnityEngine.Object) null))
        this.RightFuIDObj.text = LocalizedText.Get("sys.TEXT_CHAT_FUID", (object) param.fuid.Substring(param.fuid.Length - 4, 4));
      this.Setup_UnitIcon(param, this.RightIcon);
      this.mCoroutine = this.StartCoroutine(this.RefreshTextLine(param.message, this.RightMessageTextObj, this.RightMessageBgImage));
    }

    private void Setup_OtherStamp(ChatLogParam param)
    {
      GameUtility.SetGameObjectActive(this.MessageRoot, true);
      GameUtility.SetGameObjectActive((Component) this.LeftIconRoot, true);
      GameUtility.SetGameObjectActive(this.LeftStampObj, true);
      GameUtility.SetGameObjectActive(this.LeftMessageStatus, true);
      this.LeftNameObj.text = param.name;
      this.LeftPostAtObj.text = ChatLogItem.GetPostAt(param.posted_at);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.LeftFuIDObj, (UnityEngine.Object) null))
        this.LeftFuIDObj.text = LocalizedText.Get("sys.TEXT_CHAT_FUID", (object) param.fuid.Substring(param.fuid.Length - 4, 4));
      this.Setup_UnitIcon(param, this.LeftIcon);
      this.mCoroutine = this.StartCoroutine(this.RefreshStamp(param.stamp_id, this.LeftStampImageObj));
    }

    private void Setup_SelfStamp(ChatLogParam param)
    {
      GameUtility.SetGameObjectActive(this.MessageRoot, true);
      GameUtility.SetGameObjectActive((Component) this.RightIconRoot, true);
      GameUtility.SetGameObjectActive(this.RightStampObj, true);
      GameUtility.SetGameObjectActive(this.RightMessageStatus, true);
      this.RightNameObj.text = param.name;
      this.RightPostAtObj.text = ChatLogItem.GetPostAt(param.posted_at);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RightFuIDObj, (UnityEngine.Object) null))
        this.RightFuIDObj.text = LocalizedText.Get("sys.TEXT_CHAT_FUID", (object) param.fuid.Substring(param.fuid.Length - 4, 4));
      this.Setup_UnitIcon(param, this.RightIcon);
      this.mCoroutine = this.StartCoroutine(this.RefreshStamp(param.stamp_id, this.RightStampImageObj));
    }

    private void Setup_OtherGuildInvite(ChatLogParam param)
    {
      GameUtility.SetGameObjectActive(this.MessageRoot, true);
      GameUtility.SetGameObjectActive((Component) this.LeftIconRoot, true);
      this.LeftNameObj.text = param.name;
      this.LeftPostAtObj.text = ChatLogItem.GetPostAt(param.posted_at);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.LeftFuIDObj, (UnityEngine.Object) null))
        this.LeftFuIDObj.text = LocalizedText.Get("sys.TEXT_CHAT_FUID", (object) param.fuid.Substring(param.fuid.Length - 4, 4));
      this.Setup_UnitIcon(param, this.LeftIcon);
      bool is_auto_approval = param.is_auto_approval != 0;
      this.mCoroutine = this.StartCoroutine(this.RefreshInviteTextLine(param.gid, param.guild_name, param.lower_level, is_auto_approval, param.message, param.award_id, param.policy));
    }

    private void Setup_SelfGuildInvite(ChatLogParam param)
    {
      GameUtility.SetGameObjectActive(this.MessageRoot, true);
      GameUtility.SetGameObjectActive((Component) this.RightIconRoot, true);
      this.RightNameObj.text = param.name;
      this.RightPostAtObj.text = ChatLogItem.GetPostAt(param.posted_at);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RightFuIDObj, (UnityEngine.Object) null))
        this.RightFuIDObj.text = LocalizedText.Get("sys.TEXT_CHAT_FUID", (object) param.fuid.Substring(param.fuid.Length - 4, 4));
      this.Setup_UnitIcon(param, this.RightIcon);
      bool is_auto_approval = param.is_auto_approval != 0;
      this.mCoroutine = this.StartCoroutine(this.RefreshInviteTextLine(param.gid, param.guild_name, param.lower_level, is_auto_approval, param.message, param.award_id, param.policy));
    }

    private void Setup_OtherGuildRaidReport(ChatLogParam param)
    {
      GameUtility.SetGameObjectActive(this.MessageRoot, true);
      GameUtility.SetGameObjectActive((Component) this.LeftIconRoot, true);
      GameUtility.SetGameObjectActive(this.LeftMessageStatus, true);
      this.LeftNameObj.text = param.name;
      this.LeftPostAtObj.text = ChatLogItem.GetPostAt(param.posted_at);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.LeftFuIDObj, (UnityEngine.Object) null))
        this.LeftFuIDObj.text = LocalizedText.Get("sys.TEXT_CHAT_FUID", (object) param.fuid.Substring(param.fuid.Length - 4, 4));
      this.Setup_UnitIcon(param, this.LeftIcon);
      this.mCoroutine = this.StartCoroutine(this.RefreshGuildRaidPartyTextLine(param.message, param.report_id));
    }

    private void Setup_SelfGuildRaidReport(ChatLogParam param)
    {
      GameUtility.SetGameObjectActive(this.MessageRoot, true);
      GameUtility.SetGameObjectActive((Component) this.RightIconRoot, true);
      GameUtility.SetGameObjectActive(this.RightMessageStatus, true);
      this.RightNameObj.text = param.name;
      this.RightPostAtObj.text = ChatLogItem.GetPostAt(param.posted_at);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RightFuIDObj, (UnityEngine.Object) null))
        this.RightFuIDObj.text = LocalizedText.Get("sys.TEXT_CHAT_FUID", (object) param.fuid.Substring(param.fuid.Length - 4, 4));
      this.Setup_UnitIcon(param, this.RightIcon);
      this.mCoroutine = this.StartCoroutine(this.RefreshGuildRaidPartyTextLine(param.message, param.report_id));
    }

    private void Setup_UnitIcon(ChatLogParam param, RawImage icon)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) icon, (UnityEngine.Object) null))
        return;
      UnitParam unitParam = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(param.icon);
      if (unitParam == null)
        return;
      if (!string.IsNullOrEmpty(param.skin_iname) && UnityEngine.Object.op_Inequality((UnityEngine.Object) icon, (UnityEngine.Object) null))
      {
        ((Component) icon).gameObject.SetActive(true);
        ArtifactParam skin = Array.Find<ArtifactParam>(MonoSingleton<GameManager>.Instance.MasterParam.Artifacts.ToArray(), (Predicate<ArtifactParam>) (p => p.iname == param.skin_iname));
        MonoSingleton<GameManager>.Instance.ApplyTextureAsync(icon, AssetPath.UnitSkinIconSmall(unitParam, skin, param.job_iname));
      }
      else
        MonoSingleton<GameManager>.Instance.ApplyTextureAsync(icon, AssetPath.UnitIconSmall(unitParam, param.job_iname));
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
      this.DisableAll();
      this.RefreshView(type, param);
    }

    public static string GetPostAt(long postat)
    {
      string empty = string.Empty;
      TimeSpan timeSpan = DateTime.Now - GameUtility.UnixtimeToLocalTime(postat);
      int days = timeSpan.Days;
      int hours = timeSpan.Hours;
      int minutes = timeSpan.Minutes;
      int seconds = timeSpan.Seconds;
      string postAt;
      if (days > 0)
        postAt = LocalizedText.Get("sys.CHAT_POSTAT_DAY", (object) days.ToString());
      else if (hours > 0)
        postAt = LocalizedText.Get("sys.CHAT_POSTAT_HOUR", (object) hours.ToString());
      else if (minutes > 0)
        postAt = LocalizedText.Get("sys.CHAT_POSTAT_MINUTE", (object) minutes.ToString());
      else if (seconds > 10)
        postAt = LocalizedText.Get("sys.CHAT_POSTAT_SECOND", (object) seconds.ToString());
      else
        postAt = LocalizedText.Get("sys.CHAT_POSTAT_NOW");
      return postAt;
    }

    [DebuggerHidden]
    private IEnumerator RefreshTextLine(
      string text,
      Text text_obj,
      Image image_bg,
      bool is_use_richtext = false,
      string url = null)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ChatLogItem.\u003CRefreshTextLine\u003Ec__Iterator0()
      {
        text_obj = text_obj,
        image_bg = image_bg,
        is_use_richtext = is_use_richtext,
        text = text,
        \u0024this = this
      };
    }

    [DebuggerHidden]
    private IEnumerator RefreshInviteTextLine(
      long guild_id,
      string guild_name,
      int lower_lv,
      bool is_auto_approval,
      string text,
      string emblem,
      int policy)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ChatLogItem.\u003CRefreshInviteTextLine\u003Ec__Iterator1()
      {
        text = text,
        lower_lv = lower_lv,
        is_auto_approval = is_auto_approval,
        policy = policy,
        guild_id = guild_id,
        guild_name = guild_name,
        emblem = emblem,
        \u0024this = this
      };
    }

    [DebuggerHidden]
    private IEnumerator RefreshOfficialTextLine(string title, string body, string url)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ChatLogItem.\u003CRefreshOfficialTextLine\u003Ec__Iterator2()
      {
        title = title,
        body = body,
        url = url,
        \u0024this = this
      };
    }

    [DebuggerHidden]
    private IEnumerator RefreshGuildRaidPartyTextLine(string text, int report_id)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ChatLogItem.\u003CRefreshGuildRaidPartyTextLine\u003Ec__Iterator3()
      {
        text = text,
        report_id = report_id,
        \u0024this = this
      };
    }

    [DebuggerHidden]
    private IEnumerator RefreshStamp(int id, Image target_image)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ChatLogItem.\u003CRefreshStamp\u003Ec__Iterator4()
      {
        id = id,
        target_image = target_image,
        \u0024this = this
      };
    }

    private bool SetupChatStampMaster()
    {
      string src = AssetManager.LoadTextData(ChatStamp.CHAT_STAMP_MASTER_PATH);
      if (string.IsNullOrEmpty(src))
        return false;
      try
      {
        JSON_ChatStampParam[] jsonArray = JSONParser.parseJSONArray<JSON_ChatStampParam>(src);
        this.mStampParams = jsonArray != null ? new ChatStampParam[jsonArray.Length] : throw new InvalidJSONException();
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

    private class OfficialChatUrlJump : MonoBehaviour, IWebHelp
    {
      private string URL;
      private string Title;

      public void Setup(string url, string title)
      {
        this.URL = url;
        this.Title = title;
      }

      public bool GetHelpURL(out string url, out string title)
      {
        url = this.URL;
        title = this.Title;
        return true;
      }
    }
  }
}
