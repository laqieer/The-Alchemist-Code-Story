// Decompiled with JetBrains decompiler
// Type: SRPG.ChatChannelPanel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

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
      if (!((UnityEngine.Object) this.lists != (UnityEngine.Object) null))
        return;
      int childCount = this.lists.transform.childCount;
      this.items = new Transform[childCount];
      for (int index = 0; index < childCount; ++index)
      {
        Transform child = this.lists.transform.GetChild(index);
        this.items[index] = child;
        child.gameObject.SetActive(false);
        SRPG_ToggleButton component = child.GetComponent<SRPG_ToggleButton>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null)
          component.AddListener(new SRPG_Button.ButtonClickEvent(this.OnChannelChange));
      }
    }

    public void Activated(int piniD)
    {
    }

    public void Refresh(ChatChannelParam[] ch_params)
    {
      if (ch_params == null || ch_params.Length < 0 || (this.items == null || this.items.Length < 0))
        return;
      if ((UnityEngine.Object) this.Title != (UnityEngine.Object) null)
        this.Title.text = LocalizedText.Get("sys.TEXT_SELECT_CHANNEL", (object) ch_params[0].id.ToString(), (object) ch_params[ch_params.Length - 1].id.ToString());
      for (int index = 0; index < this.items.Length; ++index)
      {
        Transform transform = this.items[index];
        if ((UnityEngine.Object) transform != (UnityEngine.Object) null)
        {
          ChatChannelItem component1 = transform.GetComponent<ChatChannelItem>();
          if ((UnityEngine.Object) component1 != (UnityEngine.Object) null)
          {
            transform.gameObject.SetActive(true);
            component1.Refresh(ch_params[index]);
          }
          SRPG_ToggleButton component2 = transform.GetComponent<SRPG_ToggleButton>();
          if ((UnityEngine.Object) component2 != (UnityEngine.Object) null)
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
      if (!button.IsInteractable())
        return false;
      ChatChannelItem component = button.gameObject.GetComponent<ChatChannelItem>();
      if ((UnityEngine.Object) component != (UnityEngine.Object) null)
        this.mActiveChanneID = component.ChannelID;
      return true;
    }
  }
}
