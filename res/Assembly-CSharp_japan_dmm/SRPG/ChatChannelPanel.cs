// Decompiled with JetBrains decompiler
// Type: SRPG.ChatChannelPanel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(0, "Channel Select", FlowNode.PinTypes.Output, 0)]
  public class ChatChannelPanel : MonoBehaviour, IFlowInterface
  {
    [SerializeField]
    private Transform lists;
    [SerializeField]
    private Text Title;
    private Transform[] items;
    [SerializeField]
    private SRPG_ToggleButton[] ChannelButtons;
    private int mActiveChanneID;

    private void Awake()
    {
      if (!Object.op_Inequality((Object) this.lists, (Object) null))
        return;
      int childCount = ((Component) this.lists).transform.childCount;
      this.items = new Transform[childCount];
      for (int index = 0; index < childCount; ++index)
      {
        Transform child = ((Component) this.lists).transform.GetChild(index);
        this.items[index] = child;
        ((Component) child).gameObject.SetActive(false);
        SRPG_ToggleButton component = ((Component) child).GetComponent<SRPG_ToggleButton>();
        if (Object.op_Inequality((Object) component, (Object) null))
          component.AddListener(new SRPG_Button.ButtonClickEvent(this.OnChannelChange));
      }
    }

    public void Activated(int piniD)
    {
    }

    public void Refresh(ChatChannelParam[] ch_params)
    {
      if (ch_params == null || ch_params.Length < 0 || this.items == null || this.items.Length < 0)
        return;
      if (Object.op_Inequality((Object) this.Title, (Object) null))
      {
        int id1 = ch_params[0].id;
        int id2 = ch_params[ch_params.Length - 1].id;
        this.Title.text = LocalizedText.Get("sys.TEXT_SELECT_CHANNEL", (object) id1.ToString(), (object) id2.ToString());
      }
      for (int index = 0; index < this.items.Length; ++index)
      {
        Transform transform = this.items[index];
        if (Object.op_Inequality((Object) transform, (Object) null))
        {
          ChatChannelItem component1 = ((Component) transform).GetComponent<ChatChannelItem>();
          if (Object.op_Inequality((Object) component1, (Object) null))
          {
            ((Component) transform).gameObject.SetActive(true);
            component1.Refresh(ch_params[index]);
          }
          SRPG_ToggleButton component2 = ((Component) transform).GetComponent<SRPG_ToggleButton>();
          if (Object.op_Inequality((Object) component2, (Object) null))
            component2.IsOn = ch_params[index].id == (int) GlobalVars.CurrentChatChannel;
        }
      }
    }

    public void OnChannelChange(SRPG_Button button)
    {
      if (!this.ChannelChange(button))
        return;
      GlobalVars.CurrentChatChannel.Set(this.mActiveChanneID);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 0);
    }

    private bool ChannelChange(SRPG_Button button)
    {
      if (!((Selectable) button).IsInteractable())
        return false;
      ChatChannelItem component = ((Component) button).gameObject.GetComponent<ChatChannelItem>();
      if (Object.op_Inequality((Object) component, (Object) null))
        this.mActiveChanneID = component.ChannelID;
      return true;
    }
  }
}
