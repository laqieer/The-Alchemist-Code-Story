// Decompiled with JetBrains decompiler
// Type: SRPG.ItemListWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [AddComponentMenu("UI/リスト/アイテム")]
  [FlowNode.Pin(1, "Start", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "Refresh", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "詳細表示", FlowNode.PinTypes.Output, 100)]
  public class ItemListWindow : MonoBehaviour, IFlowInterface
  {
    public GameObject ItemTemplate;
    public Toggle ToggleShowUsed;
    public Toggle ToggleShowEquip;
    public Toggle ToggleShowMaterial;
    public Toggle ToggleShowEvolMaterial;
    public Toggle ToggleShowUnitPiece;
    public Toggle ToggleShowArtifactPiece;
    public Toggle ToggleShowTicket;
    public Toggle ToggleShowOther;
    private ItemData SelectItem;
    private ItemListWindow.ItemSource m_ItemSource;
    private ContentController m_ContenController;

    private void Awake()
    {
      if ((UnityEngine.Object) this.ToggleShowUsed != (UnityEngine.Object) null)
        this.ToggleShowUsed.onValueChanged.AddListener(new UnityAction<bool>(this.OnShowUsed));
      if ((UnityEngine.Object) this.ToggleShowEquip != (UnityEngine.Object) null)
        this.ToggleShowEquip.onValueChanged.AddListener(new UnityAction<bool>(this.OnShowEquip));
      if ((UnityEngine.Object) this.ToggleShowMaterial != (UnityEngine.Object) null)
        this.ToggleShowMaterial.onValueChanged.AddListener(new UnityAction<bool>(this.OnShowMaterial));
      if ((UnityEngine.Object) this.ToggleShowEvolMaterial != (UnityEngine.Object) null)
        this.ToggleShowEvolMaterial.onValueChanged.AddListener(new UnityAction<bool>(this.OnShowEvolMaterial));
      if ((UnityEngine.Object) this.ToggleShowUnitPiece != (UnityEngine.Object) null)
        this.ToggleShowUnitPiece.onValueChanged.AddListener(new UnityAction<bool>(this.OnShowUnitPiece));
      if ((UnityEngine.Object) this.ToggleShowArtifactPiece != (UnityEngine.Object) null)
        this.ToggleShowArtifactPiece.onValueChanged.AddListener(new UnityAction<bool>(this.OnShowArtifactPiece));
      if ((UnityEngine.Object) this.ToggleShowTicket != (UnityEngine.Object) null)
        this.ToggleShowTicket.onValueChanged.AddListener(new UnityAction<bool>(this.OnShowTicket));
      if ((UnityEngine.Object) this.ToggleShowOther != (UnityEngine.Object) null)
        this.ToggleShowOther.onValueChanged.AddListener(new UnityAction<bool>(this.OnShowOther));
      this.m_ContenController = this.GetComponent<ContentController>();
      this.m_ContenController.SetWork((object) this);
    }

    private void Start()
    {
      if (!((UnityEngine.Object) this.m_ContenController != (UnityEngine.Object) null))
        return;
      this.m_ItemSource = new ItemListWindow.ItemSource();
      List<ItemData> items = MonoSingleton<GameManager>.Instance.Player.Items;
      for (int index = 0; index < items.Count; ++index)
      {
        ItemData itemData = items[index];
        if (itemData.Num != 0 && itemData.Param.CheckCanShowInList())
          this.m_ItemSource.Add(new ItemListWindow.ItemSource.ItemParam(itemData));
      }
      this.m_ContenController.Initialize((ContentSource) this.m_ItemSource, Vector2.zero);
    }

    private void OnDestroy()
    {
      if ((UnityEngine.Object) this.m_ContenController != (UnityEngine.Object) null)
        this.m_ContenController.Release();
      this.m_ContenController = (ContentController) null;
      this.m_ItemSource = (ItemListWindow.ItemSource) null;
    }

    private void Update()
    {
    }

    public void Activated(int pinID)
    {
    }

    public void SetupNodeEvent(ContentNode node)
    {
      if (!((UnityEngine.Object) node != (UnityEngine.Object) null))
        return;
      ListItemEvents component = node.GetComponent<ListItemEvents>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      component.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelect);
    }

    private void OnShowUsed(bool isActive)
    {
      if (!isActive || this.m_ItemSource == null)
        return;
      this.m_ItemSource.SelectType(EItemTabType.Used);
    }

    private void OnShowEquip(bool isActive)
    {
      if (!isActive || this.m_ItemSource == null)
        return;
      this.m_ItemSource.SelectType(EItemTabType.Equip);
    }

    private void OnShowMaterial(bool isActive)
    {
      if (!isActive || this.m_ItemSource == null)
        return;
      this.m_ItemSource.SelectType(EItemTabType.Material);
    }

    private void OnShowEvolMaterial(bool isActive)
    {
      if (!isActive || this.m_ItemSource == null)
        return;
      this.m_ItemSource.SelectType(EItemTabType.EvolutionMaterial);
    }

    private void OnShowUnitPiece(bool isActive)
    {
      if (!isActive || this.m_ItemSource == null)
        return;
      this.m_ItemSource.SelectType(EItemTabType.UnitPiece);
    }

    private void OnShowArtifactPiece(bool isActive)
    {
      if (!isActive || this.m_ItemSource == null)
        return;
      this.m_ItemSource.SelectType(EItemTabType.ArtifactPiece);
    }

    private void OnShowTicket(bool isActive)
    {
      if (!isActive || this.m_ItemSource == null)
        return;
      this.m_ItemSource.SelectType(EItemTabType.Ticket);
    }

    private void OnShowOther(bool isActive)
    {
      if (!isActive || this.m_ItemSource == null)
        return;
      this.m_ItemSource.SelectType(EItemTabType.Other);
    }

    private void OnSelect(GameObject go)
    {
      ItemData dataOfClass = DataSource.FindDataOfClass<ItemData>(go, (ItemData) null);
      if (dataOfClass == null)
        return;
      GlobalVars.SelectedItemID = dataOfClass.Param.iname;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
    }

    private class ItemNode : ContentNode
    {
      private DataSource m_DataSource;
      private GameParameter[] m_GameParameters;

      public DataSource dataSource
      {
        get
        {
          return this.m_DataSource;
        }
      }

      public override void Initialize(ContentController controller)
      {
        base.Initialize(controller);
        this.m_DataSource = DataSource.Create(this.gameObject);
        this.m_GameParameters = this.gameObject.GetComponentsInChildren<GameParameter>();
        ItemListWindow work = controller.GetWork() as ItemListWindow;
        if (!((UnityEngine.Object) work != (UnityEngine.Object) null))
          return;
        work.SetupNodeEvent((ContentNode) this);
      }

      public override void Release()
      {
        base.Release();
      }

      public void ForceUpdate()
      {
        if (this.m_GameParameters == null)
          return;
        for (int index = 0; index < this.m_GameParameters.Length; ++index)
        {
          GameParameter gameParameter = this.m_GameParameters[index];
          if ((UnityEngine.Object) gameParameter != (UnityEngine.Object) null)
            gameParameter.UpdateValue();
        }
      }

      private void OnSelect(GameObject go)
      {
        ItemData dataOfClass = DataSource.FindDataOfClass<ItemData>(go, (ItemData) null);
        if (dataOfClass == null)
          return;
        GlobalVars.SelectedItemID = dataOfClass.Param.iname;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      }
    }

    public class ItemSource : ContentSource
    {
      private List<ItemListWindow.ItemSource.ItemParam> m_Params = new List<ItemListWindow.ItemSource.ItemParam>();
      private EItemTabType m_ItemType;

      public override void Initialize(ContentController controller)
      {
        base.Initialize(controller);
        this.SelectType(EItemTabType.Used);
      }

      public override void Release()
      {
        base.Release();
        this.m_ItemType = EItemTabType.None;
      }

      public override ContentNode Instantiate(ContentNode res)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(res.gameObject);
        if ((UnityEngine.Object) gameObject != (UnityEngine.Object) null)
          return (ContentNode) gameObject.AddComponent<ItemListWindow.ItemNode>();
        return (ContentNode) null;
      }

      public void Add(ItemListWindow.ItemSource.ItemParam param)
      {
        if (!param.IsValid())
          return;
        this.m_Params.Add(param);
      }

      public void SelectType(EItemTabType itemType)
      {
        if (this.m_ItemType == itemType)
          return;
        this.Clear();
        this.SetTable((ContentSource.Param[]) this.m_Params.Where<ItemListWindow.ItemSource.ItemParam>((Func<ItemListWindow.ItemSource.ItemParam, bool>) (param => param.data.Param.tabtype == itemType)).ToArray<ItemListWindow.ItemSource.ItemParam>());
        this.contentController.Resize(0);
        bool flag = false;
        Vector2 anchoredPosition = this.contentController.anchoredPosition;
        Vector2 lastPageAnchorePos = this.contentController.GetLastPageAnchorePos();
        if ((double) anchoredPosition.x < (double) lastPageAnchorePos.x)
        {
          flag = true;
          anchoredPosition.x = lastPageAnchorePos.x;
        }
        if ((double) anchoredPosition.y < (double) lastPageAnchorePos.y)
        {
          flag = true;
          anchoredPosition.y = lastPageAnchorePos.y;
        }
        if (flag)
          this.contentController.anchoredPosition = anchoredPosition;
        this.contentController.scroller.StopMovement();
        this.m_ItemType = itemType;
      }

      public class ItemParam : ContentSource.Param
      {
        private ItemData m_Item;

        public ItemParam(ItemData item)
        {
          this.m_Item = item;
        }

        public override bool IsValid()
        {
          return this.m_Item != null;
        }

        public ItemData data
        {
          get
          {
            return this.m_Item;
          }
        }

        public EItemType itemType
        {
          get
          {
            return this.m_Item.ItemType;
          }
        }

        public override void OnSetup(ContentNode node)
        {
          ItemListWindow.ItemNode itemNode = node as ItemListWindow.ItemNode;
          if (!((UnityEngine.Object) itemNode != (UnityEngine.Object) null))
            return;
          itemNode.dataSource.Clear();
          itemNode.dataSource.Add(typeof (ItemData), (object) this.m_Item);
          itemNode.ForceUpdate();
        }
      }
    }
  }
}
