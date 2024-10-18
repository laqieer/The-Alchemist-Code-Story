// Decompiled with JetBrains decompiler
// Type: SRPG.BlackList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(0, "Refresh", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Next", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "Prev", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(10, "Detail Request", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "BlackList Request", FlowNode.PinTypes.Output, 11)]
  public class BlackList : MonoBehaviour, IFlowInterface
  {
    [SerializeField]
    private Transform ItemRoot;
    [SerializeField]
    private GameObject ItemTemplate;
    [SerializeField]
    private GameObject ItemEmpty;
    [SerializeField]
    private Button Prev;
    [SerializeField]
    private Button Next;
    [SerializeField]
    private Text Pager;
    [SerializeField]
    private ListExtras ScrollView;
    [SerializeField]
    private Text ChatMaintenanceMsg;
    public int LimitView = 10;
    private List<GameObject> mItems = new List<GameObject>();
    private ChatBlackList mBlackList;
    private int mListTotal;
    private int mCurrentPage = 1;

    public ChatBlackList BList
    {
      get => this.mBlackList;
      set => this.mBlackList = value;
    }

    public int ListTotal
    {
      get => this.mListTotal;
      set => this.mListTotal = value;
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 0:
          this.Refresh();
          break;
        case 1:
          if (this.mBlackList == null || this.mCurrentPage + 1 > (this.mBlackList.total % this.LimitView != 0 ? this.mBlackList.total / this.LimitView + 1 : this.mBlackList.total / this.LimitView))
            break;
          this.OnSelectPage(this.mCurrentPage + 1);
          ++this.mCurrentPage;
          break;
        case 2:
          if (this.mBlackList == null || this.mCurrentPage - 1 <= 0)
            break;
          this.OnSelectPage(this.mCurrentPage - 1);
          --this.mCurrentPage;
          break;
      }
    }

    private void Awake()
    {
      if (Object.op_Inequality((Object) this.ItemTemplate, (Object) null))
        this.ItemTemplate.SetActive(false);
      if (!Object.op_Inequality((Object) this.ItemEmpty, (Object) null))
        return;
      this.ItemEmpty.SetActive(false);
    }

    private void Start()
    {
      if (!Object.op_Equality((Object) this.ItemTemplate, (Object) null))
        ;
    }

    private void OnDestroy()
    {
      FlowNode_Variable.Set("BLACKLIST_OFFSET", string.Empty);
      this.ResetBlackListItems();
    }

    private void ResetBlackListItems()
    {
      if (this.mItems == null || this.mItems.Count <= 0)
        return;
      GameUtility.DestroyGameObjects(this.mItems);
      this.mItems.Clear();
    }

    public void RefreshMaintenanceMessage(string message)
    {
      if (string.IsNullOrEmpty(message) || !Object.op_Inequality((Object) this.ChatMaintenanceMsg, (Object) null))
        return;
      this.ChatMaintenanceMsg.text = message;
    }

    private void Refresh()
    {
      FlowNode_Variable.Set("BLACKLIST_OFFSET", this.mCurrentPage.ToString());
      this.ResetBlackListItems();
      if (this.mBlackList == null || this.mBlackList.lists.Length <= 0)
      {
        if (Object.op_Inequality((Object) this.ItemEmpty, (Object) null))
          this.ItemEmpty.SetActive(true);
        this.Pager.text = LocalizedText.Get("sys.TEXT_PAGER_TEMP", (object) "0", (object) "0");
        ((Selectable) this.Next).interactable = false;
        ((Selectable) this.Prev).interactable = false;
      }
      else
      {
        for (int index = 0; index < this.mBlackList.lists.Length; ++index)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          BlackList.\u003CRefresh\u003Ec__AnonStorey0 refreshCAnonStorey0 = new BlackList.\u003CRefresh\u003Ec__AnonStorey0();
          // ISSUE: reference to a compiler-generated field
          refreshCAnonStorey0.\u0024this = this;
          GameObject gameObject = Object.Instantiate<GameObject>(this.ItemTemplate);
          gameObject.transform.SetParent(this.ItemRoot, false);
          BlackListItem component1 = gameObject.GetComponent<BlackListItem>();
          // ISSUE: reference to a compiler-generated field
          refreshCAnonStorey0.param = this.mBlackList.lists[index];
          // ISSUE: reference to a compiler-generated field
          component1.Refresh(refreshCAnonStorey0.param);
          SRPG_Button component2 = gameObject.GetComponent<SRPG_Button>();
          if (Object.op_Inequality((Object) component2, (Object) null))
          {
            // ISSUE: method pointer
            ((UnityEvent) component2.onClick).AddListener(new UnityAction((object) refreshCAnonStorey0, __methodptr(\u003C\u003Em__0)));
          }
          gameObject.SetActive(true);
          this.mItems.Add(gameObject);
        }
        int num = this.mBlackList.total % this.LimitView != 0 ? this.mBlackList.total / this.LimitView + 1 : this.mBlackList.total / this.LimitView;
        if (Object.op_Inequality((Object) this.Pager, (Object) null))
          this.Pager.text = LocalizedText.Get("sys.TEXT_PAGER_TEMP", (object) this.mCurrentPage, (object) num);
        ((Selectable) this.Next).interactable = this.mCurrentPage + 1 <= num;
        ((Selectable) this.Prev).interactable = this.mCurrentPage - 1 > 0;
        if (!Object.op_Inequality((Object) this.ScrollView, (Object) null))
          return;
        this.ScrollView.SetScrollPos(0.0f);
      }
    }

    private void OnSelectItems(string uid)
    {
      if (string.IsNullOrEmpty(uid))
        return;
      FlowNode_Variable.Set("SelectUserID", uid);
      FlowNode_Variable.Set("IsBlackList", "1");
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
    }

    private void OnSelectPage(int offset)
    {
      FlowNode_Variable.Set("BLACKLIST_OFFSET", offset.ToString());
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 11);
    }
  }
}
