// Decompiled with JetBrains decompiler
// Type: SRPG.RaidRescueList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "Initialize", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "Open Popup", FlowNode.PinTypes.Output, 101)]
  public class RaidRescueList : MonoBehaviour, IFlowInterface
  {
    public const int PIN_INPUT_INIT = 1;
    public const int PIN_OUTPUT_OPEN_POPUP = 101;
    [SerializeField]
    private Transform mItemParent;
    [SerializeField]
    private RaidRescueListItem mItem;
    [SerializeField]
    private GameObject mNoRequest;
    [SerializeField]
    private Button mUpdateButton;
    private List<RaidRescueListItem> mItemList = new List<RaidRescueListItem>();

    private void Awake() => this.mNoRequest.SetActive(false);

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.Init();
    }

    private void Init()
    {
      if (Object.op_Equality((Object) this.mItemParent, (Object) null) || Object.op_Equality((Object) this.mItem, (Object) null))
        return;
      for (int index = 0; index < this.mItemList.Count; ++index)
        Object.Destroy((Object) ((Component) this.mItemList[index]).gameObject);
      this.mItemList.Clear();
      if (RaidManager.Instance.RaidRescueMemberList == null || RaidManager.Instance.RaidRescueMemberList.Count <= 0)
      {
        this.mNoRequest.SetActive(true);
      }
      else
      {
        for (int index = 0; index < RaidManager.Instance.RaidRescueMemberList.Count; ++index)
        {
          RaidRescueListItem raidRescueListItem = Object.Instantiate<RaidRescueListItem>(this.mItem, this.mItemParent);
          raidRescueListItem.Setup(index, RaidManager.Instance.RaidRescueMemberList[index]);
          ((Component) raidRescueListItem).gameObject.SetActive(true);
          this.mItemList.Add(raidRescueListItem);
        }
        this.mNoRequest.SetActive(false);
      }
      if (Object.op_Equality((Object) this.mUpdateButton, (Object) null))
        return;
      ((Selectable) this.mUpdateButton).interactable = false;
    }

    public void ListClick(RaidRescueListItem item)
    {
      RaidManager.Instance.SetRescueIndex(item.Index);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
    }

    private void Update()
    {
      if (!Object.op_Inequality((Object) this.mUpdateButton, (Object) null) || ((Selectable) this.mUpdateButton).interactable || !RaidManager.Instance.RescueListIsRefreshable)
        return;
      ((Selectable) this.mUpdateButton).interactable = true;
    }
  }
}
