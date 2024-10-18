// Decompiled with JetBrains decompiler
// Type: SRPG.ChatChannelWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(2, "Request(PageIndex)", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(99, "Close", FlowNode.PinTypes.Output, 99)]
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(0, "Refresh", FlowNode.PinTypes.Input, 0)]
  public class ChatChannelWindow : MonoBehaviour, IFlowInterface
  {
    public int ONE_VIEW = 20;
    private const int page_item_max = 20;
    [SerializeField]
    private GameObject PanelTemplate1;
    [SerializeField]
    private GameObject PanelTemplate2;
    [SerializeField]
    private Transform ChatChannelBtn;
    [SerializeField]
    private Transform ChatChannelPagePanel;
    [SerializeField]
    private Transform PageItemRoot;
    [SerializeField]
    private GameObject ChannelPageItem;
    private GameObject[] mChannelPages;
    private ChatChannel mChannel;
    private GameManager gm;

    public ChatChannel Channel
    {
      get
      {
        return this.mChannel;
      }
      set
      {
        this.mChannel = value;
      }
    }

    public void Activated(int pinID)
    {
      if (pinID != 0)
        return;
      FlowNode_Variable.Set("SelectChannelPage", string.Empty);
      this.Refresh();
    }

    private void Awake()
    {
      if ((UnityEngine.Object) this.PanelTemplate1 != (UnityEngine.Object) null)
        this.PanelTemplate1.SetActive(false);
      if ((UnityEngine.Object) this.PanelTemplate2 != (UnityEngine.Object) null)
        this.PanelTemplate2.SetActive(false);
      if ((UnityEngine.Object) this.ChatChannelPagePanel != (UnityEngine.Object) null)
        this.ChatChannelPagePanel.gameObject.SetActive(false);
      if ((UnityEngine.Object) this.ChannelPageItem != (UnityEngine.Object) null)
        this.ChannelPageItem.SetActive(false);
      this.gm = MonoSingleton<GameManager>.Instance;
    }

    private void Start()
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 1);
    }

    private void Refresh()
    {
      ChatChannelParam[] channels = this.mChannel.channels;
      this.RefreshPageButton(channels[0].id, channels[channels.Length - 1].id);
      ChatChannelParam[] ch_params1 = new ChatChannelParam[10];
      for (int index = 0; index < 10; ++index)
        ch_params1[index] = channels[index];
      ChatChannelParam[] ch_params2 = new ChatChannelParam[10];
      for (int index = 0; index < 10; ++index)
        ch_params2[index] = channels[10 + index];
      if ((UnityEngine.Object) this.PanelTemplate1 != (UnityEngine.Object) null)
      {
        ChatChannelPanel component = this.PanelTemplate1.GetComponent<ChatChannelPanel>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null)
        {
          this.PanelTemplate1.SetActive(true);
          component.Refresh(ch_params1);
        }
      }
      if ((UnityEngine.Object) this.PanelTemplate2 != (UnityEngine.Object) null)
      {
        ChatChannelPanel component = this.PanelTemplate2.GetComponent<ChatChannelPanel>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null)
        {
          this.PanelTemplate2.SetActive(true);
          component.Refresh(ch_params2);
        }
      }
      if (!((UnityEngine.Object) this.ChatChannelPagePanel != (UnityEngine.Object) null))
        return;
      LayoutElement[] componentsInChildren = this.PageItemRoot.transform.GetComponentsInChildren<LayoutElement>(false);
      if (componentsInChildren != null)
      {
        foreach (LayoutElement layoutElement in componentsInChildren)
        {
          if ((bool) ((UnityEngine.Object) layoutElement))
            UnityEngine.Object.Destroy((UnityEngine.Object) layoutElement.gameObject);
        }
      }
      ChatChannelMasterParam[] chatChannelMaster = this.gm.GetChatChannelMaster();
      if (chatChannelMaster == null || chatChannelMaster.Length < 0)
        return;
      int num1 = chatChannelMaster.Length / this.ONE_VIEW;
      for (int index1 = 0; index1 < num1; ++index1)
      {
        int index2 = 20 * index1;
        int num2 = chatChannelMaster.Length < index2 ? 0 : chatChannelMaster[index2].id;
        int index3 = index2 - 1 + 20;
        int num3 = chatChannelMaster.Length < index3 ? 0 : chatChannelMaster[index3].id;
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ChannelPageItem);
        gameObject.transform.SetParent(this.PageItemRoot, false);
        Transform child = gameObject.transform.FindChild("text");
        if ((UnityEngine.Object) child != (UnityEngine.Object) null)
        {
          UnityEngine.UI.Text component = child.GetComponent<UnityEngine.UI.Text>();
          StringBuilder stringBuilder = GameUtility.GetStringBuilder();
          stringBuilder.Append("CH ");
          stringBuilder.Append(LocalizedText.Get("sys.TEXT_CHAT_CHANNEL_TEMP", (object) num2.ToString(), (object) num3.ToString()));
          string str = stringBuilder.ToString();
          component.text = str;
        }
        SRPG_Button component1 = gameObject.GetComponent<SRPG_Button>();
        if ((UnityEngine.Object) component1 != (UnityEngine.Object) null)
        {
          int index = index1;
          component1.onClick.AddListener((UnityAction) (() => this.OnSelectPage(index)));
        }
        gameObject.SetActive(true);
      }
    }

    private void RefreshPageButton(int begin, int end)
    {
      Transform child = this.ChatChannelBtn.FindChild("text");
      if (!((UnityEngine.Object) child != (UnityEngine.Object) null))
        return;
      child.GetComponent<UnityEngine.UI.Text>().text = LocalizedText.Get("sys.TEXT_CHAT_CHANNEL_TEMP", (object) begin.ToString(), (object) end.ToString());
    }

    private void OnSelectPage(int page)
    {
      FlowNode_Variable.Set("SelectChannelPage", page.ToString());
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 2);
    }
  }
}
