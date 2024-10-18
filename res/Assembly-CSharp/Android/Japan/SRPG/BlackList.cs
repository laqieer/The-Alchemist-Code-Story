// Decompiled with JetBrains decompiler
// Type: SRPG.BlackList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(0, "Refresh", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Next", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "Prev", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(10, "Detail Request", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "BlackList Request", FlowNode.PinTypes.Output, 11)]
  public class BlackList : MonoBehaviour, IFlowInterface
  {
    public int LimitView = 10;
    private List<GameObject> mItems = new List<GameObject>();
    private int mCurrentPage = 1;
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
    private ChatBlackList mBlackList;
    private int mListTotal;

    public ChatBlackList BList
    {
      get
      {
        return this.mBlackList;
      }
      set
      {
        this.mBlackList = value;
      }
    }

    public int ListTotal
    {
      get
      {
        return this.mListTotal;
      }
      set
      {
        this.mListTotal = value;
      }
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
      if ((UnityEngine.Object) this.ItemTemplate != (UnityEngine.Object) null)
        this.ItemTemplate.SetActive(false);
      if (!((UnityEngine.Object) this.ItemEmpty != (UnityEngine.Object) null))
        return;
      this.ItemEmpty.SetActive(false);
    }

    private void Start()
    {
      if ((UnityEngine.Object) this.ItemTemplate == (UnityEngine.Object) null)
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
      if (string.IsNullOrEmpty(message) || !((UnityEngine.Object) this.ChatMaintenanceMsg != (UnityEngine.Object) null))
        return;
      this.ChatMaintenanceMsg.text = message;
    }

    private void Refresh()
    {
      FlowNode_Variable.Set("BLACKLIST_OFFSET", this.mCurrentPage.ToString());
      this.ResetBlackListItems();
      if (this.mBlackList == null || this.mBlackList.lists.Length <= 0)
      {
        if ((UnityEngine.Object) this.ItemEmpty != (UnityEngine.Object) null)
          this.ItemEmpty.SetActive(true);
        this.Pager.text = LocalizedText.Get("sys.TEXT_PAGER_TEMP", (object) "0", (object) "0");
        this.Next.interactable = false;
        this.Prev.interactable = false;
      }
      else
      {
        for (int index = 0; index < this.mBlackList.lists.Length; ++index)
        {
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ItemTemplate);
          gameObject.transform.SetParent(this.ItemRoot, false);
          BlackListItem component1 = gameObject.GetComponent<BlackListItem>();
          ChatBlackListParam param = this.mBlackList.lists[index];
          component1.Refresh(param);
          SRPG_Button component2 = gameObject.GetComponent<SRPG_Button>();
          if ((UnityEngine.Object) component2 != (UnityEngine.Object) null)
            component2.onClick.AddListener((UnityAction) (() => this.OnSelectItems(param.uid)));
          gameObject.SetActive(true);
          this.mItems.Add(gameObject);
        }
        int num = this.mBlackList.total % this.LimitView != 0 ? this.mBlackList.total / this.LimitView + 1 : this.mBlackList.total / this.LimitView;
        if ((UnityEngine.Object) this.Pager != (UnityEngine.Object) null)
          this.Pager.text = LocalizedText.Get("sys.TEXT_PAGER_TEMP", (object) this.mCurrentPage, (object) num);
        this.Next.interactable = this.mCurrentPage + 1 <= num;
        this.Prev.interactable = this.mCurrentPage - 1 > 0;
        if (!((UnityEngine.Object) this.ScrollView != (UnityEngine.Object) null))
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
