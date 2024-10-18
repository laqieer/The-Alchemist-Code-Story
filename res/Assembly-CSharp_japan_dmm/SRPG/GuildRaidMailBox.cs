// Decompiled with JetBrains decompiler
// Type: SRPG.GuildRaidMailBox
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(0, "Refresh", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Receive All", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(10, "Received", FlowNode.PinTypes.Output, 10)]
  public class GuildRaidMailBox : MonoBehaviour, IFlowInterface
  {
    private const int PIN_IN_REFRESH = 0;
    private const int PIN_IN_RECEIVE_ALL = 1;
    private const int PIN_OUT_RECEIVE = 10;
    [SerializeField]
    private Button ReceiveAllButton;
    [SerializeField]
    private GuildRaidMailBoxItem mTemplate;
    private List<GuildRaidMailBoxItem> mItemList = new List<GuildRaidMailBoxItem>();

    public void Activated(int pinID)
    {
      if (pinID == 0)
      {
        this.Refresh();
      }
      else
      {
        if (pinID != 1)
          return;
        this.OnReceiveAllItem();
      }
    }

    private void Refresh()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) GuildRaidManager.Instance, (UnityEngine.Object) null) || GuildRaidManager.Instance.MailCurrentPageItemList == null)
        return;
      this.mItemList.ForEach((Action<GuildRaidMailBoxItem>) (item => UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) item).gameObject)));
      this.mItemList.Clear();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mTemplate, (UnityEngine.Object) null))
      {
        ((Component) this.mTemplate).gameObject.SetActive(false);
        GuildRaidManager.Instance.MailCurrentPageItemList.ForEach((Action<GuildRaidMailListItem>) (mail =>
        {
          GuildRaidMailBoxItem guildRaidMailBoxItem = UnityEngine.Object.Instantiate<GuildRaidMailBoxItem>(this.mTemplate, ((Component) this.mTemplate).transform.parent);
          guildRaidMailBoxItem.Setup(mail, new SRPG_Button.ButtonClickEvent(this.OnReceiveItem));
          ((Component) guildRaidMailBoxItem).gameObject.SetActive(true);
          this.mItemList.Add(guildRaidMailBoxItem);
        }));
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ReceiveAllButton, (UnityEngine.Object) null))
        return;
      ((Selectable) this.ReceiveAllButton).interactable = this.mItemList.Count > 0;
    }

    private void OnReceiveItem(SRPG_Button button)
    {
      GuildRaidMailListItem dataOfClass = DataSource.FindDataOfClass<GuildRaidMailListItem>(((Component) button).gameObject, (GuildRaidMailListItem) null);
      if (dataOfClass == null)
        return;
      GuildRaidManager instance = GuildRaidManager.Instance;
      instance.ResetMailReceivingIdList();
      instance.AddMailReceivingIdList(dataOfClass.MailId);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
    }

    private void OnReceiveAllItem()
    {
      GuildRaidManager grm = GuildRaidManager.Instance;
      grm.ResetMailReceivingIdList();
      GuildRaidManager.Instance.MailCurrentPageItemList.ForEach((Action<GuildRaidMailListItem>) (mail => grm.AddMailReceivingIdList(mail.MailId)));
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
    }
  }
}
