// Decompiled with JetBrains decompiler
// Type: SRPG.RaidRescueList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "Initialize", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "Open Popup", FlowNode.PinTypes.Output, 101)]
  public class RaidRescueList : MonoBehaviour, IFlowInterface
  {
    private List<RaidRescueListItem> mItemList = new List<RaidRescueListItem>();
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

    private void Awake()
    {
      this.mNoRequest.SetActive(false);
    }

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.Init();
    }

    private void Init()
    {
      if ((UnityEngine.Object) this.mItemParent == (UnityEngine.Object) null || (UnityEngine.Object) this.mItem == (UnityEngine.Object) null)
        return;
      for (int index = 0; index < this.mItemList.Count; ++index)
        UnityEngine.Object.Destroy((UnityEngine.Object) this.mItemList[index].gameObject);
      this.mItemList.Clear();
      if (RaidManager.Instance.RaidRescueMemberList == null || RaidManager.Instance.RaidRescueMemberList.Count <= 0)
      {
        this.mNoRequest.SetActive(true);
      }
      else
      {
        for (int index = 0; index < RaidManager.Instance.RaidRescueMemberList.Count; ++index)
        {
          RaidRescueListItem raidRescueListItem = UnityEngine.Object.Instantiate<RaidRescueListItem>(this.mItem, this.mItemParent);
          raidRescueListItem.Setup(index, RaidManager.Instance.RaidRescueMemberList[index]);
          raidRescueListItem.gameObject.SetActive(true);
          this.mItemList.Add(raidRescueListItem);
        }
        this.mNoRequest.SetActive(false);
      }
      if ((UnityEngine.Object) this.mUpdateButton == (UnityEngine.Object) null)
        return;
      this.mUpdateButton.interactable = false;
    }

    public void ListClick(RaidRescueListItem item)
    {
      RaidManager.Instance.SetRescueIndex(item.Index);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
    }

    private void Update()
    {
      if (!((UnityEngine.Object) this.mUpdateButton != (UnityEngine.Object) null) || this.mUpdateButton.interactable || !RaidManager.Instance.RescueListIsRefreshable)
        return;
      this.mUpdateButton.interactable = true;
    }
  }
}
