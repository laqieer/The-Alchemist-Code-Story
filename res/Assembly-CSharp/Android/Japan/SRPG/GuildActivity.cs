// Decompiled with JetBrains decompiler
// Type: SRPG.GuildActivity
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(10, "チャットウィンドウを開く", FlowNode.PinTypes.Input, 10)]
  public class GuildActivity : MonoBehaviour, IFlowInterface
  {
    [SerializeField]
    private float REFRESH_TIMEING = 5f;
    private readonly float FIRST_REFRESH_TIMING = 1f;
    private List<ChatLogItem> mCreatedChatLogItems = new List<ChatLogItem>();
    private const int PIN_INPUT_OPEN_CHATWINDOW = 10;
    [SerializeField]
    private ChatLogItem mLogTemplate;
    [SerializeField]
    private ScrollRect mScrollRect;
    [SerializeField]
    private GameObject mMaintenancePanel;
    private float mRefreshTiming;
    private float mElapsedTime;
    private long mLastMessageId;
    private bool mIsNeedUpdateScroll;

    public void Activated(int pinID)
    {
      if (pinID != 10)
        return;
      this.OpenChatWindow();
    }

    private void Awake()
    {
      this.mElapsedTime = this.REFRESH_TIMEING;
    }

    private void Start()
    {
      MonoSingleton<ChatWindow>.Instance.RequestGuildChatLog();
    }

    private void Update()
    {
      if ((UnityEngine.Object) MonoSingleton<ChatWindow>.Instance == (UnityEngine.Object) null)
        return;
      if (MonoSingleton<ChatWindow>.Instance.IsClose)
        MonoSingleton<ChatWindow>.Instance.SetActiveOpenCloseButton(false);
      if (this.UpdateMaintenancePanel())
        return;
      this.CheckUpdateScroll();
      this.mRefreshTiming = this.mCreatedChatLogItems.Count > 0 ? this.REFRESH_TIMEING : this.FIRST_REFRESH_TIMING;
      this.mElapsedTime += Time.deltaTime;
      if ((double) this.mElapsedTime < (double) this.mRefreshTiming)
        return;
      this.mElapsedTime = 0.0f;
      this.Refresh();
    }

    public void Refresh()
    {
      this.mLogTemplate.gameObject.SetActive(false);
      ChatLog chatLogInstance = MonoSingleton<ChatWindow>.Instance.GetChatLogInstance(ChatWindow.eChatType.Guild);
      if (chatLogInstance == null)
        return;
      MonoSingleton<ChatWindow>.Instance.RefreshChatLogView(chatLogInstance);
      for (int index = 0; index < this.mCreatedChatLogItems.Count; ++index)
        this.mCreatedChatLogItems[index].gameObject.SetActive(false);
      int num = chatLogInstance.messages.Count - this.mCreatedChatLogItems.Count;
      for (int index = 0; index < num; ++index)
      {
        ChatLogItem chatLogItem = UnityEngine.Object.Instantiate<ChatLogItem>(this.mLogTemplate);
        chatLogItem.transform.SetParent(this.mLogTemplate.transform.parent, false);
        this.mCreatedChatLogItems.Add(chatLogItem);
      }
      for (int index = 0; index < chatLogInstance.messages.Count; ++index)
      {
        this.mCreatedChatLogItems[index].gameObject.SetActive(true);
        this.mCreatedChatLogItems[index].SetParam(chatLogInstance.messages[index], new SRPG_Button.ButtonClickEvent(this.OnTapUnitIcon));
      }
      if (this.mLastMessageId != chatLogInstance.LastMessageIdServer && (UnityEngine.Object) this.mScrollRect != (UnityEngine.Object) null)
      {
        this.mIsNeedUpdateScroll = true;
        this.mLastMessageId = chatLogInstance.LastMessageIdServer;
      }
      GameParameter.UpdateAll(this.gameObject);
    }

    private void CheckUpdateScroll()
    {
      if (!this.mIsNeedUpdateScroll)
        return;
      this.mScrollRect.verticalNormalizedPosition = 0.0f;
      this.mIsNeedUpdateScroll = false;
    }

    private void OpenChatWindow()
    {
      if ((UnityEngine.Object) MonoSingleton<ChatWindow>.Instance == (UnityEngine.Object) null)
        return;
      MonoSingleton<ChatWindow>.Instance.ChangeChatTypeTab(ChatWindow.eChatType.Guild);
      MonoSingleton<ChatWindow>.Instance.Open();
      MonoSingleton<ChatWindow>.Instance.SetActiveOpenCloseButton(true);
    }

    private void OnTapUnitIcon(SRPG_Button button)
    {
      if ((UnityEngine.Object) button == (UnityEngine.Object) null)
        return;
      ChatLogItem componentInParent = button.GetComponentInParent<ChatLogItem>();
      if ((UnityEngine.Object) componentInParent == (UnityEngine.Object) null || componentInParent.ChatLogParam == null || string.IsNullOrEmpty(componentInParent.ChatLogParam.uid))
        return;
      FlowNode_Variable.Set("SelectUserID", componentInParent.ChatLogParam.uid);
      FlowNode_Variable.Set("IsBlackList", "0");
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 30);
    }

    private bool UpdateMaintenancePanel()
    {
      if (MonoSingleton<ChatWindow>.Instance.IsMaintenance)
      {
        this.mMaintenancePanel.SetActive(true);
        return true;
      }
      this.mMaintenancePanel.SetActive(false);
      return false;
    }
  }
}
