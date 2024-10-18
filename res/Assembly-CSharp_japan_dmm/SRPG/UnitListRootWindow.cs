// Decompiled with JetBrains decompiler
// Type: SRPG.UnitListRootWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class UnitListRootWindow : FlowWindowBase
  {
    public const string UNITLIST_KEY = "unitlist";
    public const string PIECELIST_KEY = "piecelist";
    public const string SUPPORTLIST_KEY = "supportlist";
    public const int BTN_GROUP = 1;
    public const int TEXT_GROUP = 2;
    private UnitListRootWindow.SerializeParam m_Param;
    private SerializeValueList m_ValueList;
    private UnitListWindow m_Root;
    private GameObject m_UnitList;
    private GameObject m_SupportList;
    private GameObject m_PieceList;
    private UnitListWindow.EditType m_EditType;
    private UnitListRootWindow.ContentType m_ContentType;
    private TabMaker m_Tab;
    private bool m_Destroy;
    private UnitListRootWindow.Content.ItemSource m_ContentSource;
    private ContentController m_ContentController;
    private Dictionary<string, UnitListRootWindow.ListData> m_Dict = new Dictionary<string, UnitListRootWindow.ListData>();
    private GameObject m_AccessoryRoot;
    private RectTransform[] m_MainSlotLabel;
    private RectTransform[] m_SubSlotLabel;
    private static UnitListRootWindow m_Instance;
    private ContentController m_PieceController;
    private const float SUPPORT_REFRESH_LOCK_TIME = 10f;
    private ContentController m_SupportController;
    private SerializeValueList m_SupportValueList;
    private float m_SupportRefreshLock;
    private ContentController m_UnitController;
    private List<UnitData> mSameUnitList;

    public override string name => nameof (UnitListRootWindow);

    private GameObject Param_UnitList
    {
      get
      {
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_UnitList, (UnityEngine.Object) null))
        {
          if (this.m_Param == null || this.m_Param.unitList == null || UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_Param.listParent, (UnityEngine.Object) null))
            return (GameObject) null;
          this.m_UnitList = UnityEngine.Object.Instantiate<GameObject>(AssetManager.Load<GameObject>(this.m_Param.unitList));
          this.m_UnitList.transform.SetParent(this.m_Param.listParent.transform, false);
          this.InitializeUnitList();
          ContentNode[] componentsInChildren = this.m_UnitList.GetComponentsInChildren<ContentNode>(true);
          for (int index = 0; index < componentsInChildren.Length; ++index)
            this.m_ValueList.SetField(((UnityEngine.Object) componentsInChildren[index]).name.IndexOf("tower", StringComparison.OrdinalIgnoreCase) <= 0 ? "node_unit" : "node_tower", ((Component) componentsInChildren[index]).gameObject);
        }
        return this.m_UnitList;
      }
    }

    private GameObject Param_PieceList
    {
      get
      {
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_PieceList, (UnityEngine.Object) null))
        {
          if (this.m_Param == null || this.m_Param.pieceList == null || UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_Param.listParent, (UnityEngine.Object) null))
            return (GameObject) null;
          this.m_PieceList = UnityEngine.Object.Instantiate<GameObject>(AssetManager.Load<GameObject>(this.m_Param.pieceList));
          this.m_PieceList.transform.SetParent(this.m_Param.listParent.transform, false);
          this.InitializePieceList();
          this.m_ValueList.SetField("node_piece", ((Component) this.m_PieceList.GetComponentInChildren<ContentNode>(true)).gameObject);
        }
        return this.m_PieceList;
      }
    }

    private GameObject Param_SupportList
    {
      get
      {
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_SupportList, (UnityEngine.Object) null))
        {
          if (this.m_Param == null || this.m_Param.supportList == null || UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_Param.listParent, (UnityEngine.Object) null))
            return (GameObject) null;
          this.m_SupportList = UnityEngine.Object.Instantiate<GameObject>(AssetManager.Load<GameObject>(this.m_Param.supportList));
          this.m_SupportList.transform.SetParent(this.m_Param.listParent.transform, false);
          this.InitializeSupportList();
          this.m_ValueList.SetField("node_support", ((Component) this.m_SupportList.GetComponentInChildren<ContentNode>(true)).gameObject);
        }
        return this.m_SupportList;
      }
    }

    public static UnitListRootWindow instance => UnitListRootWindow.m_Instance;

    public static bool hasInstance
    {
      get
      {
        if (UnitListRootWindow.m_Instance != null)
        {
          if (UnitListRootWindow.m_Instance.isValid)
            return true;
          UnitListRootWindow.m_Instance = (UnitListRootWindow) null;
        }
        return false;
      }
    }

    public override void Initialize(FlowWindowBase.SerializeParamBase param)
    {
      UnitListRootWindow.m_Instance = this;
      base.Initialize(param);
      this.m_Param = param as UnitListRootWindow.SerializeParam;
      if (this.m_Param == null)
        throw new Exception(this.ToString() + " > Failed serializeParam null.");
      SerializeValueBehaviour childComponent = this.GetChildComponent<SerializeValueBehaviour>("root");
      this.m_ValueList = !UnityEngine.Object.op_Inequality((UnityEngine.Object) childComponent, (UnityEngine.Object) null) ? new SerializeValueList() : childComponent.list;
      this.m_ValueList.SetActive(1, false);
      this.m_ValueList.SetActive(2, false);
      this.Close(true);
    }

    public override void Release()
    {
      this.ReleaseContentList();
      base.Release();
    }

    public override int Update()
    {
      base.Update();
      if (this.isClosed)
        this.SetActiveChild(false);
      if (this.m_ContentType == UnitListRootWindow.ContentType.SUPPORT)
        this.Update_Support();
      return -1;
    }

    public UnitListRootWindow.ListData GetListData(string key)
    {
      UnitListRootWindow.ListData listData = (UnitListRootWindow.ListData) null;
      this.m_Dict.TryGetValue(key, out listData);
      return listData;
    }

    public UnitListRootWindow.ListData AddListData(string key)
    {
      UnitListRootWindow.ListData listData = new UnitListRootWindow.ListData();
      listData.key = key;
      this.m_Dict.Add(key, listData);
      return listData;
    }

    public void RemoveListDataAll()
    {
      foreach (KeyValuePair<string, UnitListRootWindow.ListData> keyValuePair in this.m_Dict)
        keyValuePair.Value.Delete();
    }

    private string[] GetTabKeys(UnitListRootWindow.Tab[] tabs)
    {
      List<string> stringList = new List<string>();
      for (int index = 0; index < tabs.Length; ++index)
        stringList.Add(tabs[index].ToString());
      return stringList.ToArray();
    }

    private void SetupTab(UnitListRootWindow.Tab[] tabs, UnitListRootWindow.Tab start)
    {
      this.m_Tab = this.m_ValueList.GetComponent<TabMaker>("tab");
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Tab, (UnityEngine.Object) null))
        return;
      this.m_Tab.Create(this.GetTabKeys(tabs), new Action<GameObject, SerializeValueList>(this.SetupTabNode));
      if (this.m_EditType == UnitListWindow.EditType.SUPPORT)
      {
        int data = this.GetData<int>("data_element", 0);
        if (data != 0)
        {
          start = this.GetTab((EElement) data);
          TabMaker.Info[] infos = this.m_Tab.GetInfos();
          for (int index = 0; index < infos.Length; ++index)
          {
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) infos[index].ev, (UnityEngine.Object) null))
              infos[index].ev.ResetFlag(ButtonEvent.Flag.AUTOLOCK);
            if ((UnitListRootWindow.Tab) infos[index].element.value != start)
              infos[index].SetColor(new Color(0.5f, 0.5f, 0.5f));
            infos[index].interactable = false;
          }
        }
      }
      this.m_Tab.SetOn((Enum) start, true);
    }

    private void SetupTabNode(GameObject gobj, SerializeValueList value)
    {
      TabMaker.Element element = value.GetObject<TabMaker.Element>("element", (TabMaker.Element) null);
      if (element == null)
        return;
      object obj = Enum.Parse(typeof (UnitListRootWindow.Tab), element.key);
      if (obj == null)
        return;
      element.value = (int) obj;
    }

    public UnitListRootWindow.Tab GetCurrentTab()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Tab, (UnityEngine.Object) null))
      {
        TabMaker.Info onIfno = this.m_Tab.GetOnIfno();
        if (onIfno != null)
          return (UnitListRootWindow.Tab) onIfno.element.value;
      }
      return UnitListRootWindow.Tab.ALL;
    }

    public Vector2 GetCurrentTabAnchore()
    {
      return UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_ContentController, (UnityEngine.Object) null) ? this.m_ContentController.anchoredPosition : Vector2.zero;
    }

    public UnitListRootWindow.Tab GetTab(EElement element)
    {
      UnitListRootWindow.Tab tab = UnitListRootWindow.Tab.ALL;
      switch (element)
      {
        case EElement.None:
          tab = UnitListRootWindow.Tab.MAINSUPPORT;
          break;
        case EElement.Fire:
          tab = UnitListRootWindow.Tab.FIRE;
          break;
        case EElement.Water:
          tab = UnitListRootWindow.Tab.WATER;
          break;
        case EElement.Wind:
          tab = UnitListRootWindow.Tab.WIND;
          break;
        case EElement.Thunder:
          tab = UnitListRootWindow.Tab.THUNDER;
          break;
        case EElement.Shine:
          tab = UnitListRootWindow.Tab.LIGHT;
          break;
        case EElement.Dark:
          tab = UnitListRootWindow.Tab.DARK;
          break;
      }
      return tab;
    }

    public static EElement GetElement(UnitListRootWindow.Tab tab)
    {
      EElement element = EElement.None;
      switch (tab)
      {
        case UnitListRootWindow.Tab.FIRE:
          element = EElement.Fire;
          break;
        case UnitListRootWindow.Tab.WATER:
          element = EElement.Water;
          break;
        case UnitListRootWindow.Tab.THUNDER:
          element = EElement.Thunder;
          break;
        case UnitListRootWindow.Tab.WIND:
          element = EElement.Wind;
          break;
        case UnitListRootWindow.Tab.LIGHT:
          element = EElement.Shine;
          break;
        case UnitListRootWindow.Tab.DARK:
          element = EElement.Dark;
          break;
      }
      return element;
    }

    public EElement GetElement()
    {
      UnitListRootWindow.Tab currentTab = this.GetCurrentTab();
      EElement element = EElement.None;
      switch (currentTab)
      {
        case UnitListRootWindow.Tab.FIRE:
          element = EElement.Fire;
          break;
        case UnitListRootWindow.Tab.WATER:
          element = EElement.Water;
          break;
        case UnitListRootWindow.Tab.THUNDER:
          element = EElement.Thunder;
          break;
        case UnitListRootWindow.Tab.WIND:
          element = EElement.Wind;
          break;
        case UnitListRootWindow.Tab.LIGHT:
          element = EElement.Shine;
          break;
        case UnitListRootWindow.Tab.DARK:
          element = EElement.Dark;
          break;
      }
      return element;
    }

    public void CalcUnit(List<UnitListWindow.Data> list)
    {
      UnitListRootWindow.Tab currentTab = this.GetCurrentTab();
      for (int index = list.Count - 1; index >= 0; --index)
      {
        if ((list[index].tabMask & currentTab) == UnitListRootWindow.Tab.NONE)
          list.RemoveAt(index);
      }
    }

    public void InitializeContentList(UnitListRootWindow.ContentType contentType)
    {
      this.m_Destroy = false;
      this.m_ContentType = contentType;
      switch (this.m_ContentType)
      {
        case UnitListRootWindow.ContentType.UNIT:
          this.m_ContentSource = this.SetupUnitList((UnitListRootWindow.Content.ItemSource) null);
          break;
        case UnitListRootWindow.ContentType.PIECE:
          this.m_ContentSource = this.SetupPieceList((UnitListRootWindow.Content.ItemSource) null);
          break;
        case UnitListRootWindow.ContentType.SUPPORT:
          this.m_ContentSource = this.SetupSupportList((UnitListRootWindow.Content.ItemSource) null);
          break;
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_ContentController, (UnityEngine.Object) null))
        return;
      if (this.m_ContentController.GetNodeCount() == 0)
        this.m_ContentController.Initialize((ContentSource) this.m_ContentSource, Vector2.zero);
      else
        this.m_ContentController.SetCurrentSource((ContentSource) this.m_ContentSource);
    }

    public void RefreshContentList()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_ContentController, (UnityEngine.Object) null))
        this.m_ContentController.SetCurrentSource((ContentSource) null);
      switch (this.m_ContentType)
      {
        case UnitListRootWindow.ContentType.UNIT:
          this.SetupUnitList(this.m_ContentSource);
          break;
        case UnitListRootWindow.ContentType.PIECE:
          this.SetupPieceList(this.m_ContentSource);
          break;
        case UnitListRootWindow.ContentType.SUPPORT:
          this.SetupSupportList(this.m_ContentSource);
          break;
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_ContentController, (UnityEngine.Object) null))
        return;
      this.m_ContentController.SetCurrentSource((ContentSource) this.m_ContentSource);
    }

    public void ReleaseContentList()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_ContentController, (UnityEngine.Object) null))
        this.m_ContentController.Release();
      this.m_ContentSource = (UnitListRootWindow.Content.ItemSource) null;
    }

    private void ShowToolTip(string path, UnitData unit)
    {
      if (string.IsNullOrEmpty(path) || unit == null)
        return;
      GameObject root = UnityEngine.Object.Instantiate<GameObject>(AssetManager.Load<GameObject>(this.m_Param.tooltipPrefab));
      DataSource.Bind<UnitData>(root, unit);
      UnitJobDropdown componentInChildren1 = root.GetComponentInChildren<UnitJobDropdown>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren1, (UnityEngine.Object) null))
      {
        ((Component) componentInChildren1).gameObject.SetActive(true);
        Selectable component1 = ((Component) componentInChildren1).gameObject.GetComponent<Selectable>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component1, (UnityEngine.Object) null))
          component1.interactable = false;
        Image component2 = ((Component) componentInChildren1).gameObject.GetComponent<Image>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component2, (UnityEngine.Object) null))
          ((Graphic) component2).color = new Color(0.5f, 0.5f, 0.5f);
      }
      ArtifactSlots componentInChildren2 = root.GetComponentInChildren<ArtifactSlots>();
      AbilitySlots componentInChildren3 = root.GetComponentInChildren<AbilitySlots>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren2, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren3, (UnityEngine.Object) null))
      {
        componentInChildren2.Refresh(false);
        componentInChildren3.Refresh(false);
      }
      ConceptCardSlots componentInChildren4 = root.GetComponentInChildren<ConceptCardSlots>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren4, (UnityEngine.Object) null))
        componentInChildren4.Refresh(false);
      GameParameter.UpdateAll(root);
    }

    public void SetRoot(UnitListWindow root) => this.m_Root = root;

    public void ClearData()
    {
      List<SerializeValue> list = this.m_ValueList.list;
      for (int index = 0; index < list.Count; ++index)
      {
        if (list[index].key.IndexOf("data_") != -1)
        {
          this.m_ValueList.RemoveFieldAt(index);
          --index;
        }
      }
    }

    public void RemoveData(string key) => this.m_ValueList.RemoveField(key);

    public void AddData(string key, object value) => this.m_ValueList.AddObject(key, value);

    public object GetData(string key) => this.m_ValueList.GetObject(key);

    public object GetData(string key, object defaultValue)
    {
      return this.m_ValueList.GetObject(key, defaultValue);
    }

    public T GetData<T>(string key) => this.m_ValueList.GetObject<T>(key);

    public T GetData<T>(string key, T defaultValue)
    {
      return this.m_ValueList.GetObject<T>(key, defaultValue);
    }

    public UnitListWindow.EditType GetEditType() => this.m_EditType;

    public UnitListRootWindow.ContentType GetContentType() => this.m_ContentType;

    public RectTransform AttachSlotLabel(UnitListWindow.Data data, ContentNode node)
    {
      RectTransform rectTransform = (RectTransform) null;
      if (!this.GetData<bool>("data_heroOnly", false))
      {
        if (data.partyMainSlot != -1)
        {
          if (this.m_MainSlotLabel != null && data.partyMainSlot >= 0 && data.partyMainSlot < this.m_MainSlotLabel.Length)
            rectTransform = this.m_MainSlotLabel[data.partyMainSlot];
        }
        else if (data.partySubSlot != -1 && this.m_SubSlotLabel != null && data.partySubSlot >= 0 && data.partySubSlot < this.m_SubSlotLabel.Length)
          rectTransform = this.m_SubSlotLabel[data.partySubSlot];
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) rectTransform, (UnityEngine.Object) null))
        {
          rectTransform.anchoredPosition = new Vector2(0.0f, 0.0f);
          ((Component) rectTransform).gameObject.SetActive(true);
        }
      }
      return rectTransform;
    }

    public void DettachSlotLabel(RectTransform tr)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) tr, (UnityEngine.Object) null))
        return;
      tr.anchoredPosition = Vector2.zero;
      ((Component) tr).gameObject.SetActive(false);
    }

    public void RegistAnchorePos(Vector2 pos)
    {
      foreach (KeyValuePair<string, UnitListRootWindow.ListData> keyValuePair in this.m_Dict)
      {
        UnitListRootWindow.ListData listData = keyValuePair.Value;
        if (listData != null)
          listData.anchorePos = pos;
      }
    }

    public void RegistAnchorePos(string key, Vector2 pos)
    {
      UnitListRootWindow.ListData listData = this.GetListData(key);
      if (listData == null)
        return;
      listData.anchorePos = pos;
    }

    public override int OnActivate(int pinId)
    {
      if (this.m_ContentType == UnitListRootWindow.ContentType.UNIT || pinId == 100 || pinId == 101 || pinId == 102 || pinId == 103 || pinId == 105 || pinId == 120 || pinId == 104 || pinId == 310)
        return this.OnActivate_Unit(pinId);
      if (this.m_ContentType == UnitListRootWindow.ContentType.PIECE || pinId == 110)
        return this.OnActivate_Piece(pinId);
      return this.m_ContentType == UnitListRootWindow.ContentType.SUPPORT || pinId == 300 ? this.OnActivate_Support(pinId) : -1;
    }

    private int OnActivate_Base(int pinId)
    {
      switch (pinId)
      {
        case 400:
          this.RegistAnchorePos(Vector2.zero);
          this.RefreshContentList();
          if (this.isClosed)
            this.Open();
          ButtonEvent.ResetLock("unitlist");
          break;
        case 410:
          this.RegistAnchorePos(this.m_ContentController.anchoredPosition);
          this.RefreshContentList();
          if (this.isClosed)
            this.Open();
          ButtonEvent.ResetLock("unitlist");
          break;
        case 430:
          this.RemoveListDataAll();
          this.RegistAnchorePos(this.m_ContentController.anchoredPosition);
          this.RefreshContentList();
          if (this.isClosed)
            this.Open();
          ButtonEvent.ResetLock("unitlist");
          break;
        case 490:
          this.m_ContentType = UnitListRootWindow.ContentType.NONE;
          this.RegistAnchorePos(this.m_ContentController.anchoredPosition);
          this.m_Destroy = true;
          this.Close();
          ButtonEvent.ResetLock("unitlist");
          return 491;
      }
      return -1;
    }

    protected override int OnOpened() => 191;

    protected override int OnClosed() => this.m_Destroy ? 492 : -1;

    public static UnitListRootWindow.Tab GetTabMask(UnitListWindow.Data data)
    {
      if (data.param == null)
        return UnitListRootWindow.Tab.ALL;
      UnitParam unitParam = data.param;
      UnitListRootWindow.Tab tabMask = UnitListRootWindow.Tab.NONE;
      if (data.unit != null && data.unit.IsFavorite)
        tabMask |= UnitListRootWindow.Tab.FAVORITE;
      if (unitParam != null)
      {
        if (unitParam.element == EElement.Fire)
          tabMask |= UnitListRootWindow.Tab.FIRE;
        else if (unitParam.element == EElement.Water)
          tabMask |= UnitListRootWindow.Tab.WATER;
        else if (unitParam.element == EElement.Thunder)
          tabMask |= UnitListRootWindow.Tab.THUNDER;
        else if (unitParam.element == EElement.Wind)
          tabMask |= UnitListRootWindow.Tab.WIND;
        else if (unitParam.element == EElement.Shine)
          tabMask |= UnitListRootWindow.Tab.LIGHT;
        else if (unitParam.element == EElement.Dark)
          tabMask |= UnitListRootWindow.Tab.DARK;
      }
      return tabMask;
    }

    private void InitializePieceList()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_PieceList, (UnityEngine.Object) null))
        return;
      this.m_PieceController = this.m_PieceList.GetComponentInChildren<ContentController>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_PieceController, (UnityEngine.Object) null))
        this.m_PieceController.SetWork((object) this);
      this.m_PieceList.SetActive(false);
    }

    public UnitListRootWindow.ListData CreatePieceList()
    {
      UnitListRootWindow.ListData pieceList1 = this.GetListData("piecelist");
      if (pieceList1 == null)
        pieceList1 = this.AddListData("piecelist");
      else
        pieceList1.Delete();
      List<ItemParam> items = MonoSingleton<GameManager>.Instance.MasterParam.Items;
      UnitParam[] allUnits = MonoSingleton<GameManager>.Instance.MasterParam.GetAllUnits();
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      for (int index = 0; index < items.Count; ++index)
      {
        ItemParam item = items[index];
        if (item.type == EItemType.UnitPiece)
        {
          UnitParam focus = Array.Find<UnitParam>(allUnits, (Predicate<UnitParam>) (p => p.piece == item.iname));
          if (focus != null && focus.IsSummon() && player.FindUnitDataByUnitID(focus.iname) == null && focus.CheckAvailable(TimeManager.ServerTime) && pieceList1.data.FindIndex((Predicate<UnitListWindow.Data>) (prop => prop.param == focus)) == -1)
          {
            UnitListWindow.Data data = new UnitListWindow.Data(focus);
            if (data.unlockable)
              pieceList1.data.Add(data);
          }
        }
      }
      for (int index = 0; index < pieceList1.data.Count; ++index)
        pieceList1.data[index].sortPriority = (long) index;
      pieceList1.data.Sort((Comparison<UnitListWindow.Data>) ((a, b) =>
      {
        int pieceList2 = b.pieceAmount - a.pieceAmount;
        if (pieceList2 != 0)
          return pieceList2;
        int num = (int) b.param.rare - (int) a.param.rare;
        return num != 0 ? num : a.sortPriority.CompareTo(b.sortPriority);
      }));
      pieceList1.calcData.AddRange((IEnumerable<UnitListWindow.Data>) pieceList1.data);
      pieceList1.isValid = true;
      return pieceList1;
    }

    private UnitListRootWindow.Content.ItemSource SetupPieceList(
      UnitListRootWindow.Content.ItemSource source)
    {
      bool flag = false;
      UnitListRootWindow.ListData listData1 = this.GetListData("unitlist");
      UnitListRootWindow.ListData listData2 = this.GetListData("piecelist");
      if (listData2 == null || !listData2.isValid || source == null)
      {
        listData2 = this.CreatePieceList();
        flag = true;
      }
      if (source == null)
      {
        source = new UnitListRootWindow.Content.ItemSource();
        UnitListRootWindow.Tab start = listData1 == null || listData1.selectTab == UnitListRootWindow.Tab.NONE ? UnitListRootWindow.Tab.ALL : listData1.selectTab;
        if (start == UnitListRootWindow.Tab.FAVORITE)
          start = UnitListRootWindow.Tab.ALL;
        this.SetupTab(new UnitListRootWindow.Tab[7]
        {
          UnitListRootWindow.Tab.ALL,
          UnitListRootWindow.Tab.FIRE,
          UnitListRootWindow.Tab.WATER,
          UnitListRootWindow.Tab.THUNDER,
          UnitListRootWindow.Tab.WIND,
          UnitListRootWindow.Tab.LIGHT,
          UnitListRootWindow.Tab.DARK
        }, start);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Param_PieceList, (UnityEngine.Object) null))
        {
          ContentNode component = this.m_ValueList.GetComponent<ContentNode>("node_piece");
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          {
            this.m_ContentController = this.m_PieceController;
            this.m_ContentController.m_Node = component;
            ((Component) this.m_ContentController).gameObject.SetActive(true);
          }
          this.Param_PieceList.SetActive(true);
        }
        this.m_ValueList.SetActive(1, false);
        this.m_ValueList.SetActive(2, false);
        this.m_ValueList.SetActive("btn_close", true);
        this.m_ValueList.SetActive("desc_piece", true);
        listData1.anchorePos = this.m_ContentController.anchoredPosition;
        flag = true;
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_UnitList, (UnityEngine.Object) null))
        this.m_UnitList.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_SupportList, (UnityEngine.Object) null))
        this.m_SupportList.SetActive(false);
      if (listData2 != null)
      {
        if (flag)
          listData2.RefreshTabMask();
        listData1.selectTab = this.GetCurrentTab();
        listData2.calcData.Clear();
        listData2.calcData.AddRange((IEnumerable<UnitListWindow.Data>) listData2.data);
        this.CalcUnit(listData2.calcData);
        for (int index = 0; index < listData2.calcData.Count; ++index)
        {
          UnitListWindow.Data data = listData2.calcData[index];
          if (data != null)
          {
            UnitListRootWindow.Content.ItemSource.ItemParam itemParam = new UnitListRootWindow.Content.ItemSource.ItemParam(this.m_Root, data);
            if (itemParam != null && itemParam.IsValid())
              source.Add(itemParam);
          }
        }
        source.AnchorePos(listData2.anchorePos);
      }
      return source;
    }

    private int OnActivate_Piece(int pinId)
    {
      switch (pinId)
      {
        case 110:
          this.InitializeContentList(UnitListRootWindow.ContentType.PIECE);
          this.Open();
          break;
        case 400:
          return this.OnActivate_Base(pinId);
        case 410:
          return this.OnActivate_Base(pinId);
        case 420:
          if (FlowNode_ButtonEvent.currentValue is SerializeValueList currentValue)
          {
            UnitParam dataSource = currentValue.GetDataSource<UnitParam>("_self");
            if (dataSource != null)
            {
              GlobalVars.UnlockUnitID = dataSource.iname;
              return 423;
            }
            break;
          }
          break;
        case 430:
          this.RemoveListDataAll();
          return this.OnActivate_Base(pinId);
        case 490:
          this.InitializeContentList(UnitListRootWindow.ContentType.UNIT);
          this.Open();
          break;
      }
      return -1;
    }

    private void InitializeSupportList()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_SupportList, (UnityEngine.Object) null))
        return;
      this.m_SupportController = this.m_SupportList.GetComponentInChildren<ContentController>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_SupportController, (UnityEngine.Object) null))
        this.m_SupportController.SetWork((object) this);
      this.m_SupportList.SetActive(false);
      SerializeValueBehaviour component = this.m_SupportList.GetComponent<SerializeValueBehaviour>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        this.m_SupportValueList = component.list;
      else
        this.m_SupportValueList = new SerializeValueList();
    }

    private void Update_Support()
    {
      if ((double) this.m_SupportRefreshLock <= 0.0)
        return;
      if ((double) (Time.realtimeSinceStartup - this.m_SupportRefreshLock) > 10.0)
      {
        this.m_SupportRefreshLock = 0.0f;
        this.m_ValueList.SetInteractable("btn_refresh", true);
      }
      else
        this.m_ValueList.SetInteractable("btn_refresh", false);
    }

    private string GetSupportListKey(EElement element) => "supportlist_" + element.ToString();

    private UnitListRootWindow.ListData GetSupportList(EElement element)
    {
      return this.GetListData(this.GetSupportListKey(element));
    }

    private UnitListRootWindow.ListData GetSupportList(FlowNode_ReqSupportList.SupportList support)
    {
      return this.GetSupportList(support == null ? EElement.None : support.m_Element);
    }

    public UnitListRootWindow.ListData CreateSupportList(EElement element)
    {
      UnitListRootWindow.ListData supportList = this.GetSupportList(element);
      if (supportList == null)
      {
        supportList = this.AddListData(this.GetSupportListKey(element));
        supportList.selectTab = this.GetTab(element);
      }
      else
        supportList.Delete();
      if (supportList.response is FlowNode_ReqSupportList.SupportList response && response.m_SupportData != null && element == response.m_Element)
      {
        for (int index = 0; index < response.m_SupportData.Length; ++index)
        {
          if (response.m_SupportData[index] != null)
            supportList.data.Add(new UnitListWindow.Data(response.m_SupportData[index]));
        }
      }
      SupportData[] data1 = this.GetData<SupportData[]>("data_support");
      int data2 = this.GetData<int>("data_party_index", -1);
      if (data1 != null)
      {
        SupportData sup_2 = data1[data2];
        if (sup_2 != null)
        {
          UnitListWindow.Data data3 = new UnitListWindow.Data("empty");
          data3.RefreshPartySlotPriority();
          supportList.data.Insert(0, data3);
          for (int index = 0; index < supportList.data.Count; ++index)
            supportList.data[index].partySelect = this.IsSameSupportUnit(supportList.data[index].support, sup_2);
        }
      }
      QuestParam data4 = this.GetData<QuestParam>("data_quest", (QuestParam) null);
      if (data4 != null)
      {
        for (int index1 = 0; index1 < supportList.data.Count; ++index1)
        {
          UnitListWindow.Data data5 = supportList.data[index1];
          if (data5.unit != null)
          {
            bool flag = data4.IsAvailableUnit(data5.unit);
            if (data4.type == QuestTypes.Ordeal)
            {
              int index2;
              if (this.FindIncludingTeamIndexFromSupport(data1, data5.support, out index2))
              {
                if (index2 != data2)
                  flag = false;
                data5.partyIndex = index2;
              }
              else
                data5.partyIndex = -1;
            }
            data5.interactable = flag;
          }
        }
      }
      supportList.calcData.AddRange((IEnumerable<UnitListWindow.Data>) supportList.data);
      supportList.isValid = true;
      return supportList;
    }

    private bool IsSameSupportUnit(SupportData sup_1, SupportData sup_2)
    {
      return sup_1 != null && sup_2.Unit != null && sup_1.FUID == sup_2.FUID && sup_1.UnitID == sup_2.UnitID && sup_1.Unit.SupportElement == sup_2.Unit.SupportElement;
    }

    private bool FindIncludingTeamIndexFromSupport(
      SupportData[] supports,
      SupportData target,
      out int index)
    {
      index = 0;
      for (int index1 = 0; index1 < supports.Length; ++index1)
      {
        SupportData support = supports[index1];
        if (support != null && this.IsSameSupportUnit(support, target))
        {
          index = index1;
          return true;
        }
      }
      return false;
    }

    private UnitListRootWindow.Content.ItemSource SetupSupportList(
      UnitListRootWindow.Content.ItemSource source)
    {
      bool flag = false;
      EElement element = UnitListRootWindow.GetElement(this.GetCurrentTab());
      UnitListRootWindow.ListData supportList1 = this.GetSupportList(element);
      if (supportList1 == null || !supportList1.isValid || source == null)
      {
        supportList1 = this.CreateSupportList(element);
        flag = true;
      }
      UnitListRootWindow.ListData supportList2 = this.GetSupportList(EElement.None);
      if (supportList2 == null || !supportList2.isValid)
        supportList2 = this.CreateSupportList(EElement.None);
      this.RemoveData("data_supportres");
      if (source == null)
      {
        source = new UnitListRootWindow.Content.ItemSource();
        this.SetupTab(new UnitListRootWindow.Tab[7]
        {
          UnitListRootWindow.Tab.MAINSUPPORT,
          UnitListRootWindow.Tab.FIRE,
          UnitListRootWindow.Tab.WATER,
          UnitListRootWindow.Tab.THUNDER,
          UnitListRootWindow.Tab.WIND,
          UnitListRootWindow.Tab.LIGHT,
          UnitListRootWindow.Tab.DARK
        }, supportList2 != null ? supportList2.selectTab : UnitListRootWindow.Tab.MAINSUPPORT);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Param_SupportList, (UnityEngine.Object) null))
        {
          ContentNode component = this.m_ValueList.GetComponent<ContentNode>("node_support");
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          {
            this.m_ContentController = this.m_SupportController;
            this.m_ContentController.m_Node = component;
            ((Component) this.m_ContentController).gameObject.SetActive(true);
          }
          this.Param_SupportList.SetActive(true);
        }
        this.m_ValueList.SetActive(1, false);
        this.m_ValueList.SetActive(2, false);
        this.m_ValueList.SetActive("btn_close", true);
        this.m_ValueList.SetActive("btn_refresh", true);
        this.m_ValueList.SetActive("btn_help_support", true);
        flag = true;
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_UnitList, (UnityEngine.Object) null))
        this.m_UnitList.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_PieceList, (UnityEngine.Object) null))
        this.m_PieceList.SetActive(false);
      if (supportList1 != null)
      {
        if (flag)
          supportList1.RefreshTabMask();
        if (supportList2 != null)
          supportList2.selectTab = this.GetCurrentTab();
        supportList1.selectTab = this.GetCurrentTab();
        supportList1.calcData.Clear();
        supportList1.calcData.AddRange((IEnumerable<UnitListWindow.Data>) supportList1.data);
        this.CalcUnit(supportList1.calcData);
        int num = 0;
        for (int index = 0; index < supportList1.calcData.Count; ++index)
        {
          UnitListWindow.Data data = supportList1.calcData[index];
          if (data != null)
          {
            UnitListRootWindow.Content.ItemSource.ItemParam itemParam = new UnitListRootWindow.Content.ItemSource.ItemParam(this.m_Root, data);
            if (itemParam != null && itemParam.IsValid())
              num = source.Add(itemParam);
          }
        }
        source.AnchorePos(supportList2 == null ? Vector2.zero : supportList2.anchorePos);
        if (num == 0)
          this.m_ValueList.SetActive("text_nosupport", true);
        else
          this.m_ValueList.SetActive("text_nosupport", false);
      }
      return source;
    }

    private int GetOutputSupportWebApiPin(EElement element, bool isForce)
    {
      this.m_SupportValueList.SetField(nameof (element), (int) element);
      return isForce ? 481 : 480;
    }

    private int OnActivate_Support(int pinId)
    {
      switch (pinId)
      {
        case 300:
          this.m_ValueList.SetActive("btn_rental", false);
          this.InitializeContentList(UnitListRootWindow.ContentType.SUPPORT);
          return this.OnActivate_Support(400);
        case 400:
          EElement element1 = UnitListRootWindow.GetElement(this.GetCurrentTab());
          UnitListRootWindow.ListData supportList = this.GetSupportList(element1);
          supportList?.Delete();
          if (supportList != null && supportList.response != null)
            return this.OnActivate_Base(pinId);
          this.m_ValueList.SetInteractable("btn_refresh", false);
          return this.GetOutputSupportWebApiPin(element1, true);
        case 410:
          return this.OnActivate_Base(pinId);
        case 419:
          if (!string.IsNullOrEmpty(this.m_Param.tooltipPrefab) && FlowNode_ButtonEvent.currentValue is SerializeValueList currentValue1)
          {
            SupportData dataSource = currentValue1.GetDataSource<SupportData>("_self");
            if (dataSource != null)
              this.ShowToolTip(this.m_Param.tooltipPrefab, dataSource.Unit);
          }
          return 429;
        case 420:
          if (FlowNode_ButtonEvent.currentValue is SerializeValueList currentValue2)
          {
            this.RegistAnchorePos(this.m_ContentController.anchoredPosition);
            return currentValue2.GetDataSource<SupportData>("_self") != null ? 426 : 427;
          }
          break;
        case 430:
          EElement element2 = UnitListRootWindow.GetElement(this.GetCurrentTab());
          UnitListRootWindow.ListData listData = this.GetSupportList(element2) ?? this.CreateSupportList(element2);
          listData.Delete();
          listData.response = (object) this.GetData<FlowNode_ReqSupportList.SupportList>("data_supportres");
          this.RemoveData("data_supportres");
          this.RegistAnchorePos(this.m_ContentController.anchoredPosition);
          this.RefreshContentList();
          this.m_SupportRefreshLock = Time.realtimeSinceStartup;
          if (this.isClosed)
            this.Open();
          ButtonEvent.ResetLock("unitlist");
          break;
        case 440:
          this.m_ValueList.SetInteractable("btn_refresh", false);
          return this.GetOutputSupportWebApiPin(UnitListRootWindow.GetElement(this.GetCurrentTab()), false);
        case 490:
          return this.OnActivate_Base(pinId);
      }
      return -1;
    }

    private void InitializeUnitList()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_UnitList, (UnityEngine.Object) null))
        return;
      this.m_UnitController = this.m_UnitList.GetComponentInChildren<ContentController>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_UnitController, (UnityEngine.Object) null))
      {
        Transform transform = ((Component) this.m_UnitController).transform;
        for (int index = 0; index < transform.childCount; ++index)
          ((Component) transform.GetChild(index)).gameObject.SetActive(false);
        this.m_UnitController.SetWork((object) this);
      }
      this.m_UnitList.SetActive(false);
      RectTransform rectTransform = Array.Find<RectTransform>(this.m_UnitList.GetComponentsInChildren<RectTransform>(), (Predicate<RectTransform>) (mono => ((UnityEngine.Object) mono).name == "accessory"));
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) rectTransform, (UnityEngine.Object) null))
        return;
      this.m_AccessoryRoot = ((Component) rectTransform).gameObject;
      List<RectTransform> rectTransformList = new List<RectTransform>();
      this.SetActiveChild(this.m_AccessoryRoot, false);
      rectTransformList.Add(this.GetChildComponent<RectTransform>(this.m_AccessoryRoot, "party_leader"));
      rectTransformList.Add(this.GetChildComponent<RectTransform>(this.m_AccessoryRoot, "party_main2"));
      rectTransformList.Add(this.GetChildComponent<RectTransform>(this.m_AccessoryRoot, "party_main3"));
      rectTransformList.Add(this.GetChildComponent<RectTransform>(this.m_AccessoryRoot, "party_main4"));
      rectTransformList.Add(this.GetChildComponent<RectTransform>(this.m_AccessoryRoot, "party_main5"));
      this.m_MainSlotLabel = rectTransformList.ToArray();
      rectTransformList.Clear();
      rectTransformList.Add(this.GetChildComponent<RectTransform>(this.m_AccessoryRoot, "party_sub1"));
      rectTransformList.Add(this.GetChildComponent<RectTransform>(this.m_AccessoryRoot, "party_sub2"));
      this.m_SubSlotLabel = rectTransformList.ToArray();
    }

    public UnitListRootWindow.ListData CreateUnitList(
      UnitListWindow.EditType editType,
      UnitData[] units)
    {
      UnitListRootWindow.ListData list = this.GetListData("unitlist");
      if (this.mSameUnitList == null)
        this.mSameUnitList = new List<UnitData>();
      this.mSameUnitList.Clear();
      if (list == null)
        list = this.AddListData("unitlist");
      else
        list.Delete();
      if (editType == UnitListWindow.EditType.SUPPORT)
      {
        eOverWritePartyType overWritePartyType = UnitOverWriteUtility.Element2OverWritePartyType((EElement) this.GetData<int>("data_element", 0));
        GlobalVars.OverWritePartyType.Set(overWritePartyType);
      }
      if (units == null)
        units = MonoSingleton<GameManager>.Instance.Player.Units.ToArray();
      if (units != null)
      {
        for (int index = 0; index < units.Length; ++index)
        {
          if (units[index] != null)
          {
            if (UnitOverWriteUtility.IsNeedOverWrite((eOverWritePartyType) GlobalVars.OverWritePartyType))
              units[index] = UnitOverWriteUtility.Apply(units[index], (eOverWritePartyType) GlobalVars.OverWritePartyType);
            list.data.Add(new UnitListWindow.Data(units[index]));
          }
        }
        switch (editType)
        {
          case UnitListWindow.EditType.PARTY:
          case UnitListWindow.EditType.PARTY_TOWER:
            PartyEditData[] data1 = this.GetData<PartyEditData[]>("data_party", (PartyEditData[]) null);
            int data2 = this.GetData<int>("data_party_index", -1);
            int data3 = this.GetData<int>("data_slot", -1);
            QuestParam data4 = this.GetData<QuestParam>("data_quest", (QuestParam) null);
            PartyEditData edit = data1?[data2];
            if (edit != null && data3 != -1 && data3 < edit.PartyData.MAX_UNIT)
            {
              int mainMemberCount = edit.GetMainMemberCount();
              bool flag1 = edit.IsSubSlot(data3);
              for (int slotNo = 0; slotNo < edit.PartyData.MAX_UNIT; ++slotNo)
              {
                UnitData unit = edit.Units[slotNo];
                if (unit != null)
                {
                  UnitListWindow.Data data5 = list.data.Find((Predicate<UnitListWindow.Data>) (prop => prop.unit != null && prop.unit.UnitID == unit.UnitID));
                  if (data5 != null)
                  {
                    if (slotNo == 0 && flag1 && mainMemberCount <= 1 && edit.Units[data3] == null)
                    {
                      list.data.Remove(data5);
                    }
                    else
                    {
                      if (edit.IsSubSlot(slotNo))
                        data5.partySubSlot = edit.GetSubSlotNum(data5.GetUniq());
                      else
                        data5.partyMainSlot = edit.GetMainSlotNum(data5.GetUniq());
                      data5.partySelect = data3 == slotNo;
                      data5.RefreshPartySlotPriority();
                    }
                  }
                }
              }
              if (edit.Units[data3] != null && (data3 != 0 || this.GetData<bool>("data_heroOnly", false)))
              {
                UnitListWindow.Data data6 = new UnitListWindow.Data("empty");
                data6.RefreshPartySlotPriority();
                list.data.Insert(0, data6);
              }
              List<UnitSameGroupParam> unitSameGroupList = (List<UnitSameGroupParam>) null;
              List<string> unitSameNameList = (List<string>) null;
              this.CreateUnitSameList(edit, data3, data4, out unitSameGroupList, out unitSameNameList);
              for (int index1 = 0; index1 < list.data.Count; ++index1)
              {
                UnitListWindow.Data data = list.data[index1];
                if (data.unit != null)
                {
                  bool flag2 = true;
                  if (data4 != null)
                  {
                    bool flag3 = data4.IsUnitAllowed(data.unit) || data.partyMainSlot != -1 || data.partySubSlot != -1;
                    string error = (string) null;
                    flag2 = flag3 & data4.IsEntryQuestCondition(data.unit, ref error);
                    if (data4.type == QuestTypes.Ordeal)
                    {
                      int index2;
                      if (this.FindIncludingTeamIndexFromParty(data1, data.unit.UniqueID, out index2))
                      {
                        if (index2 != data2)
                          flag2 = false;
                        data.partyIndex = index2;
                      }
                      else
                        data.partyIndex = -1;
                    }
                    if (data4.type == QuestTypes.GvG)
                    {
                      GvGManager instance = GvGManager.Instance;
                      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instance, (UnityEngine.Object) null) && instance.UsedUnitData != null)
                      {
                        GvGUsedUnitData gvGusedUnitData = instance.UsedUnitData.Find((Predicate<GvGUsedUnitData>) (p => p.uiid == data.GetUniq()));
                        if (gvGusedUnitData != null && gvGusedUnitData.remainDay > 0)
                        {
                          data.gvgUsedUnit = gvGusedUnitData.remainDay;
                          data.gvgUsedUnitSelect = false;
                        }
                      }
                    }
                    if (data4.type == QuestTypes.WorldRaid && UnityEngine.Object.op_Inequality((UnityEngine.Object) WorldRaidBossManager.Instance, (UnityEngine.Object) null))
                      flag2 &= !WorldRaidBossManager.GetCurrentBossUsedUnitInameList().Contains(data.unit.UnitID);
                  }
                  else if (edit.PartyData.PartyType == PlayerPartyTypes.GvG)
                  {
                    GvGManager instance = GvGManager.Instance;
                    if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instance, (UnityEngine.Object) null) && instance.UsedUnitData != null)
                    {
                      GvGUsedUnitData gvGusedUnitData = instance.UsedUnitData.Find((Predicate<GvGUsedUnitData>) (p => p.uiid == data.GetUniq()));
                      if (gvGusedUnitData != null && gvGusedUnitData.remainDay > 0)
                      {
                        data.gvgUsedUnit = gvGusedUnitData.remainDay;
                        data.gvgUsedUnitSelect = true;
                      }
                    }
                  }
                  if (edit.Units != null && data.unit != null && unitSameGroupList != null && !unitSameNameList.Contains(data.unit.UnitID))
                  {
                    for (int index3 = 0; index3 < unitSameGroupList.Count; ++index3)
                    {
                      if (unitSameGroupList[index3] != null && unitSameGroupList[index3].IsInGroup(data.unit.UnitID))
                      {
                        this.mSameUnitList.Add(data.unit);
                        data.sameUnit = true;
                        break;
                      }
                    }
                  }
                  data.interactable = flag2;
                }
              }
              break;
            }
            break;
          case UnitListWindow.EditType.EQUIP:
          case UnitListWindow.EditType.SHOP_EQUIP:
            for (int index = 0; index < list.data.Count; ++index)
            {
              UnitListWindow.Data data7 = list.data[index];
              if (data7.unit != null)
                data7.interactable = data7.unit.CheckEnableEnhanceEquipment();
            }
            break;
          case UnitListWindow.EditType.SUPPORT:
            UnitData data8 = this.GetData<UnitData>("data_unit", (UnitData) null);
            if ((byte) this.GetData<int>("data_element", 0) != (byte) 0 && data8 != null)
            {
              UnitListWindow.Data data9 = new UnitListWindow.Data("empty");
              data9.RefreshPartySlotPriority();
              list.data.Insert(0, data9);
            }
            for (int index = 0; index < list.data.Count; ++index)
            {
              UnitListWindow.Data data10 = list.data[index];
              data10.partySelect = data8 != null && data10 != null && data10.unit != null && data10.unit.UniqueID == data8.UniqueID;
            }
            break;
          case UnitListWindow.EditType.PIECE_EXCHANGE:
            if (MonoSingleton<GameManager>.Instance.MasterParam.ConvertUnitPieceExclude != null)
            {
              List<ConvertUnitPieceExcludeParam> list1 = ((IEnumerable<ConvertUnitPieceExcludeParam>) MonoSingleton<GameManager>.Instance.MasterParam.ConvertUnitPieceExclude).ToList<ConvertUnitPieceExcludeParam>();
              for (int i = list.data.Count - 1; i >= 0; --i)
              {
                int index = list1.FindIndex((Predicate<ConvertUnitPieceExcludeParam>) (ex => ex.unit_piece_iname == list.data[i].param.piece));
                if (index >= 0)
                {
                  list.data.RemoveAt(i);
                  list1.RemoveAt(index);
                }
              }
              break;
            }
            break;
        }
      }
      list.calcData.AddRange((IEnumerable<UnitListWindow.Data>) list.data);
      list.isValid = true;
      return list;
    }

    private bool FindIncludingTeamIndexFromParty(
      PartyEditData[] partyEditDataAry,
      long uniqueID,
      out int index)
    {
      index = 0;
      for (int index1 = 0; index1 < partyEditDataAry.Length; ++index1)
      {
        foreach (UnitData unit in partyEditDataAry[index1].Units)
        {
          if (unit != null && unit.UniqueID == uniqueID)
          {
            index = index1;
            return true;
          }
        }
      }
      return false;
    }

    public void CreateUnitSameList(
      PartyEditData edit,
      int selectSlot,
      QuestParam quest,
      out List<UnitSameGroupParam> unitSameGroupList,
      out List<string> unitSameNameList)
    {
      unitSameNameList = new List<string>();
      unitSameGroupList = new List<UnitSameGroupParam>();
      if (edit == null || edit.Units == null)
        return;
      for (int index = 0; index < edit.Units.Length; ++index)
      {
        if (edit.Units[index] != null && selectSlot != index)
        {
          UnitSameGroupParam unitSameGroup = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitSameGroup(edit.Units[index].UnitID);
          if (unitSameGroup != null)
          {
            unitSameNameList.Add(edit.Units[index].UnitID);
            unitSameGroupList.Add(unitSameGroup);
          }
        }
      }
      if (quest == null)
        return;
      string[] strArray = quest.questParty == null ? quest.units.GetList() : ((IEnumerable<PartySlotTypeUnitPair>) quest.questParty.GetMainSubSlots()).Where<PartySlotTypeUnitPair>((Func<PartySlotTypeUnitPair, bool>) (slot => slot.Type == PartySlotType.ForcedHero || slot.Type == PartySlotType.Forced)).Select<PartySlotTypeUnitPair, string>((Func<PartySlotTypeUnitPair, string>) (slot => slot.Unit)).ToArray<string>();
      if (strArray == null)
        return;
      for (int index = 0; index < strArray.Length; ++index)
      {
        UnitSameGroupParam unitSameGroup = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitSameGroup(strArray[index]);
        if (unitSameGroup != null)
        {
          unitSameNameList.Add(strArray[index]);
          unitSameGroupList.Add(unitSameGroup);
        }
      }
    }

    private UnitListRootWindow.Content.ItemSource SetupUnitList(
      UnitListRootWindow.Content.ItemSource source)
    {
      bool flag1 = false;
      UnitListRootWindow.TabRegister data1 = this.GetData<UnitListRootWindow.TabRegister>("data_register", (UnitListRootWindow.TabRegister) null);
      this.m_EditType = this.GetData<UnitListWindow.EditType>("data_edit", UnitListWindow.EditType.DEFAULT);
      UnitListRootWindow.ListData list = this.GetListData("unitlist");
      if (list == null || !list.isValid || source == null)
      {
        list = this.CreateUnitList(this.m_EditType, this.GetData<UnitData[]>("data_units"));
        flag1 = true;
      }
      if (source == null)
      {
        source = new UnitListRootWindow.Content.ItemSource();
        UnitListRootWindow.Tab start = list.selectTab != UnitListRootWindow.Tab.NONE ? list.selectTab : UnitListRootWindow.Tab.ALL;
        if (data1 != null)
          start = data1.tab;
        if (this.m_EditType == UnitListWindow.EditType.DEFAULT && UnityEngine.Object.op_Inequality((UnityEngine.Object) UnitManagementWindow.Instance, (UnityEngine.Object) null) && UnitManagementWindow.Instance.GetCurrentTab() != UnitListRootWindow.Tab.NONE)
          start = UnitManagementWindow.Instance.GetCurrentTab();
        this.SetupTab(new UnitListRootWindow.Tab[8]
        {
          UnitListRootWindow.Tab.ALL,
          UnitListRootWindow.Tab.FAVORITE,
          UnitListRootWindow.Tab.FIRE,
          UnitListRootWindow.Tab.WATER,
          UnitListRootWindow.Tab.THUNDER,
          UnitListRootWindow.Tab.WIND,
          UnitListRootWindow.Tab.LIGHT,
          UnitListRootWindow.Tab.DARK
        }, start);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Param_UnitList, (UnityEngine.Object) null))
        {
          ContentNode component1 = this.m_ValueList.GetComponent<ContentNode>(this.m_EditType != UnitListWindow.EditType.PARTY_TOWER ? "node_unit" : "node_tower");
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component1, (UnityEngine.Object) null))
          {
            SerializeValueBehaviour component2 = ((Component) component1).GetComponent<SerializeValueBehaviour>();
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component2, (UnityEngine.Object) null))
            {
              component2.list.SetActive("empty", false);
              component2.list.SetActive("body", false);
              component2.list.SetActive("select", false);
            }
            this.m_ContentController = this.m_UnitController;
            this.m_ContentController.m_Node = component1;
            ((Component) this.m_ContentController).gameObject.SetActive(true);
          }
          this.Param_UnitList.SetActive(true);
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_PieceList, (UnityEngine.Object) null))
          this.m_PieceList.SetActive(false);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_SupportList, (UnityEngine.Object) null))
          this.m_SupportList.SetActive(false);
        this.m_ValueList.SetActive(1, false);
        this.m_ValueList.SetActive(2, false);
        if (this.m_Root.sortWindow != null)
          this.m_ValueList.SetActive("btn_sort", true);
        if (this.m_Root.filterWindow != null)
          this.m_ValueList.SetActive("btn_filter", true);
        if (this.m_EditType == UnitListWindow.EditType.PARTY || this.m_EditType == UnitListWindow.EditType.PARTY_TOWER)
        {
          this.m_ValueList.SetActive("btn_close", true);
          MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) instance, (UnityEngine.Object) null) || !instance.IsConnectedInRoom() || GlobalVars.SelectedMultiPlayRoomType != JSON_MyPhotonRoomParam.EType.RAID)
            this.m_ValueList.SetActive("btn_attackable", true);
        }
        else if (this.m_EditType == UnitListWindow.EditType.EQUIP)
          this.m_ValueList.SetActive("btn_backhome", true);
        else if (this.m_EditType == UnitListWindow.EditType.SHOP_EQUIP)
          this.m_ValueList.SetActive("btn_close", true);
        else if (this.m_EditType == UnitListWindow.EditType.PIECE_EXCHANGE)
          this.m_ValueList.SetActive("btn_close", true);
        else if (this.m_EditType == UnitListWindow.EditType.SUPPORT)
          this.m_ValueList.SetActive("btn_close", true);
        else if (this.m_EditType == UnitListWindow.EditType.HOME_PREVIEW)
        {
          this.m_ValueList.SetActive("btn_close", true);
        }
        else
        {
          this.m_ValueList.SetActive("btn_backhome", true);
          this.m_ValueList.SetActive("btn_help", true);
          this.m_ValueList.SetActive("btn_piece", true);
          this.m_ValueList.SetActive("btn_rune_disassembly", true);
        }
        flag1 = true;
      }
      if (list != null)
      {
        if (flag1)
          list.Refresh();
        list.selectTab = this.GetCurrentTab();
        if (this.m_EditType == UnitListWindow.EditType.DEFAULT && UnityEngine.Object.op_Inequality((UnityEngine.Object) UnitManagementWindow.Instance, (UnityEngine.Object) null))
          UnitManagementWindow.Instance.SetCurrentTab(list.selectTab);
        list.calcData.Clear();
        list.calcData.AddRange((IEnumerable<UnitListWindow.Data>) list.data);
        bool flag2 = false;
        int index1 = list.calcData.FindIndex((Predicate<UnitListWindow.Data>) (d => d.unit != null && d.unit.IsRental));
        if (index1 >= 0)
        {
          list.calcData.RemoveAt(index1);
          flag2 = true;
        }
        if (this.m_EditType == UnitListWindow.EditType.PARTY)
        {
          QuestParam data2 = this.GetData<QuestParam>("data_quest", (QuestParam) null);
          if (data2 != null && data2.IsGvG && UnityEngine.Object.op_Inequality((UnityEngine.Object) GvGManager.Instance, (UnityEngine.Object) null))
          {
            GvGRuleParam currentRule = GvGManager.Instance.CurrentRule;
            if (currentRule != null)
            {
              List<UnitData> gvg_disable_units = currentRule.GetDisableUnits(MonoSingleton<GameManager>.Instance.Player.Units);
              for (int i = 0; i < gvg_disable_units.Count; ++i)
              {
                int index2 = list.calcData.FindIndex((Predicate<UnitListWindow.Data>) (d => d.unit != null && d.unit.UniqueID == gvg_disable_units[i].UniqueID));
                if (index2 >= 0)
                  list.calcData.RemoveAt(index2);
              }
            }
          }
        }
        this.CalcUnit(list.calcData);
        if (this.m_Root.filterWindow != null)
        {
          FilterUtility.FilterPrefs filter = FilterUtility.Load_UnitFilter();
          this.m_Root.filterWindow.CalcUnit(list.calcData, filter);
        }
        if (this.m_Root.sortWindow != null)
          this.m_Root.sortWindow.CalcUnit(list.calcData);
        if (this.m_EditType == UnitListWindow.EditType.PARTY || this.m_EditType == UnitListWindow.EditType.PARTY_TOWER)
        {
          QuestParam data3 = this.GetData<QuestParam>("data_quest", (QuestParam) null);
          if (this.m_ValueList.GetUIOn("btn_attackable") && data3 != null)
          {
            string empty = string.Empty;
            int data4 = this.GetData<int>("data_party_index", -1);
            for (int i = 0; i < list.calcData.Count; ++i)
            {
              if (list.calcData[i].unit != null)
              {
                if (!data3.IsEntryQuestCondition(list.calcData[i].unit, ref empty))
                {
                  list.calcData.RemoveAt(i);
                  --i;
                }
                else if (data3.type == QuestTypes.Ordeal)
                {
                  if (list.calcData[i].partyIndex >= 0 && list.calcData[i].partyIndex != data4)
                  {
                    list.calcData.RemoveAt(i);
                    --i;
                  }
                }
                else if (this.mSameUnitList != null && this.mSameUnitList.Find((Predicate<UnitData>) (u => u.UnitID == list.calcData[i].unit.UnitID)) != null)
                {
                  list.calcData.RemoveAt(i);
                  --i;
                }
              }
            }
          }
          bool sw = false;
          if (data3 != null && data3.EnableRentalUnit && flag2)
            sw = true;
          this.m_ValueList.SetActive("btn_rental", sw);
        }
        if (this.m_EditType == UnitListWindow.EditType.PARTY || this.m_EditType == UnitListWindow.EditType.PARTY_TOWER || this.m_EditType == UnitListWindow.EditType.SUPPORT)
          SortUtility.StableSort<UnitListWindow.Data>(list.calcData, (Comparison<UnitListWindow.Data>) ((p1, p2) => p1.subSortPriority.CompareTo(p2.subSortPriority)));
        long forcus = data1 == null ? 0L : data1.forcus;
        int index3 = -1;
        int num = 0;
        for (int index4 = 0; index4 < list.calcData.Count; ++index4)
        {
          UnitListWindow.Data data5 = list.calcData[index4];
          if (data5 != null)
          {
            UnitListRootWindow.Content.ItemSource.ItemParam itemParam = new UnitListRootWindow.Content.ItemSource.ItemParam(this.m_Root, data5);
            if (itemParam != null && itemParam.IsValid())
            {
              if (forcus == data5.GetUniq())
                index3 = num;
              num = source.Add(itemParam);
            }
          }
        }
        if (data1 != null)
        {
          source.AnchorePos(data1.anchorePos);
          source.ForcusIndex(index3);
        }
        else
          source.AnchorePos(list.anchorePos);
        if (num == 0)
          this.m_ValueList.SetActive("text_nounit", true);
        else
          this.m_ValueList.SetActive("text_nounit", false);
        this.RemoveData("data_register");
      }
      return source;
    }

    private int OnActivate_Unit(int pinId)
    {
      switch (pinId)
      {
        case 100:
        case 101:
        case 102:
        case 103:
        case 104:
        case 105:
        case 120:
        case 310:
          if (!this.m_ValueList.HasField("data_edit"))
          {
            UnitListWindow.EditType editType = UnitListWindow.EditType.DEFAULT;
            switch (pinId)
            {
              case 101:
                editType = UnitListWindow.EditType.PARTY;
                break;
              case 102:
                editType = UnitListWindow.EditType.PARTY_TOWER;
                break;
              case 103:
                editType = UnitListWindow.EditType.EQUIP;
                break;
              case 104:
                editType = UnitListWindow.EditType.SUPPORT;
                break;
              case 105:
                editType = UnitListWindow.EditType.SHOP_EQUIP;
                break;
              case 120:
                editType = UnitListWindow.EditType.PIECE_EXCHANGE;
                break;
              case 310:
                editType = UnitListWindow.EditType.HOME_PREVIEW;
                break;
            }
            this.AddData("data_edit", (object) editType);
            if (pinId == 104)
              this.AddData("data_unit", (object) this.m_ValueList.GetDataSource<UnitData>("_self"));
          }
          this.InitializeContentList(UnitListRootWindow.ContentType.UNIT);
          this.Open();
          break;
        case 110:
          this.InitializeContentList(UnitListRootWindow.ContentType.PIECE);
          this.Open();
          break;
        case 400:
          return this.OnActivate_Base(pinId);
        case 410:
          return this.OnActivate_Base(pinId);
        case 419:
          if (this.m_EditType != UnitListWindow.EditType.DEFAULT && !string.IsNullOrEmpty(this.m_Param.tooltipPrefab) && FlowNode_ButtonEvent.currentValue is SerializeValueList currentValue1)
          {
            UnitData dataSource = currentValue1.GetDataSource<UnitData>("_self");
            if (dataSource != null)
              this.ShowToolTip(this.m_Param.tooltipPrefab, dataSource);
          }
          return 429;
        case 420:
          if (FlowNode_ButtonEvent.currentValue is SerializeValueList currentValue2)
          {
            this.RegistAnchorePos(this.m_ContentController.anchoredPosition);
            UnitData dataSource = currentValue2.GetDataSource<UnitData>("_self");
            if (dataSource == null)
              return 422;
            UnitListRootWindow.ListData listData = this.GetListData("unitlist");
            if (listData != null)
            {
              listData.selectUniqueID = dataSource.UniqueID;
              if (this.m_EditType == UnitListWindow.EditType.DEFAULT || this.m_EditType == UnitListWindow.EditType.EQUIP || this.m_EditType == UnitListWindow.EditType.SHOP_EQUIP || this.m_EditType == UnitListWindow.EditType.SUPPORT || this.m_EditType == UnitListWindow.EditType.HOME_PREVIEW)
              {
                GlobalVars.SelectedUnitUniqueID.Set(dataSource.UniqueID);
                GlobalVars.SelectedUnitJobIndex.Set(dataSource.JobIndex);
                GlobalVars.SelectedLSChangeUnitUniqueID.Set(dataSource.UniqueID);
              }
              return 421;
            }
            break;
          }
          break;
        case 430:
          this.RemoveListDataAll();
          return this.OnActivate_Base(pinId);
        case 490:
          return this.OnActivate_Base(pinId);
        case 700:
          UnitData rentalUnit = MonoSingleton<GameManager>.Instance.Player.GetRentalUnit();
          if (rentalUnit != null)
          {
            if (FlowNode_ButtonEvent.currentValue is SerializeValueList currentValue3)
              DataSource.Bind<UnitData>(currentValue3.GetGameObject("_self"), rentalUnit, true);
            UnitListRootWindow.ListData listData = this.GetListData("unitlist");
            if (listData != null)
            {
              listData.selectUniqueID = rentalUnit.UniqueID;
              if (this.m_EditType == UnitListWindow.EditType.DEFAULT || this.m_EditType == UnitListWindow.EditType.EQUIP || this.m_EditType == UnitListWindow.EditType.SHOP_EQUIP || this.m_EditType == UnitListWindow.EditType.SUPPORT || this.m_EditType == UnitListWindow.EditType.HOME_PREVIEW)
              {
                GlobalVars.SelectedUnitUniqueID.Set(rentalUnit.UniqueID);
                GlobalVars.SelectedUnitJobIndex.Set(rentalUnit.JobIndex);
                GlobalVars.SelectedLSChangeUnitUniqueID.Set(rentalUnit.UniqueID);
              }
              return 421;
            }
            break;
          }
          break;
      }
      return -1;
    }

    public enum ContentType
    {
      NONE,
      UNIT,
      PIECE,
      SUPPORT,
    }

    public enum Tab
    {
      NONE = 0,
      FAVORITE = 1,
      FIRE = 2,
      WATER = 4,
      THUNDER = 8,
      WIND = 16, // 0x00000010
      LIGHT = 32, // 0x00000020
      DARK = 64, // 0x00000040
      MAINSUPPORT = 128, // 0x00000080
      ALL = 65535, // 0x0000FFFF
    }

    public static class Content
    {
      public static UnitListRootWindow.Content.ItemAccessor clickItem;

      public class ItemAccessor
      {
        private static readonly string SVB_KEY_BODY = "body";
        private static readonly string SVB_KEY_SELECT = "select";
        private static readonly string SVB_KEY_TEAM = "team";
        private static readonly string SVB_KEY_USE = "use";
        private static readonly string SVB_KEY_BADGE = "badge";
        private static readonly string SVB_KEY_DIED = "died";
        private static readonly string SVB_KEY_NO_ENTRY = "no_entry";
        private static readonly string SVB_KEY_SAME_UNIT = "sameunit";
        private static readonly string SVB_KEY_NO_EQUIP = "noequip";
        private static readonly string SVB_KEY_FRIEND = "friend";
        private static readonly string SVB_KEY_LOCKED = "locked";
        private static readonly string SVB_KEY_SORT = "sort";
        private static readonly string SVB_KEY_LEVEL = "lv";
        private static readonly string SVB_KEY_GVG_USED_UNIT = "gvgusedunit";
        private static readonly string SVB_KEY_GVG_USED_TEXT = "gvgusedtext";
        private UnitListRootWindow m_RootWindow;
        private UnitListSortWindow m_SortWindow;
        private UnitListRootWindow.ContentType m_ContentType;
        private ContentNode m_Node;
        private UnitListWindow.Data m_Param;
        private DataSource m_DataSource;
        private SerializeValueBehaviour m_Value;
        private SortBadge m_SortBadge;
        private RectTransform m_SlotLabel;

        public ContentNode node => this.m_Node;

        public UnitListWindow.Data param => this.m_Param;

        public bool isValid => this.m_Param != null;

        public void Setup(UnitListWindow window, UnitListWindow.Data param)
        {
          this.m_RootWindow = window.rootWindow;
          this.m_SortWindow = window.sortWindow;
          this.m_ContentType = this.m_RootWindow.GetContentType();
          this.m_Param = param;
        }

        public void Release()
        {
          this.Clear();
          this.m_Node = (ContentNode) null;
          this.m_Param = (UnitListWindow.Data) null;
        }

        public void Bind(ContentNode node)
        {
          this.m_Node = node;
          this.m_DataSource = DataSource.Create(((Component) node).gameObject);
          this.m_DataSource.Add(typeof (UnitParam), (object) this.m_Param.param);
          if (this.m_Param.unit != null)
            this.m_DataSource.Add(typeof (UnitData), (object) this.m_Param.unit);
          if (this.m_Param.support != null)
            this.m_DataSource.Add(typeof (SupportData), (object) this.m_Param.support);
          this.m_Value = ((Component) this.m_Node).GetComponent<SerializeValueBehaviour>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Value, (UnityEngine.Object) null))
          {
            this.m_Value.list.SetActive(1, false);
            if (!string.IsNullOrEmpty(this.m_Param.body))
              this.m_Value.list.SetActive(this.m_Param.body, true);
            else
              this.m_Value.list.SetActive(UnitListRootWindow.Content.ItemAccessor.SVB_KEY_BODY, true);
            this.m_Value.list.SetActive(UnitListRootWindow.Content.ItemAccessor.SVB_KEY_SELECT, this.m_Param.partySelect);
            if (this.m_ContentType == UnitListRootWindow.ContentType.UNIT || this.m_ContentType == UnitListRootWindow.ContentType.UNIT)
            {
              if (this.m_RootWindow != null)
              {
                UnitListWindow.EditType editType = this.m_RootWindow.GetEditType();
                switch (editType)
                {
                  case UnitListWindow.EditType.DEFAULT:
                  case UnitListWindow.EditType.EQUIP:
                  case UnitListWindow.EditType.SHOP_EQUIP:
                    List<PartyData> partys = MonoSingleton<GameManager>.Instance.Player.Partys;
                    bool sw = false;
                    for (int index = 0; index < partys.Count; ++index)
                    {
                      if (partys[index].IsPartyUnit(this.m_Param.GetUniq()))
                      {
                        sw = true;
                        break;
                      }
                    }
                    this.m_Value.list.SetActive(UnitListRootWindow.Content.ItemAccessor.SVB_KEY_USE, sw);
                    if (editType == UnitListWindow.EditType.EQUIP || editType == UnitListWindow.EditType.SHOP_EQUIP)
                    {
                      this.m_Value.list.SetActive(UnitListRootWindow.Content.ItemAccessor.SVB_KEY_BADGE, false);
                      if (!this.m_Param.interactable)
                      {
                        this.m_Value.list.SetActive(UnitListRootWindow.Content.ItemAccessor.SVB_KEY_NO_EQUIP, true);
                        break;
                      }
                      break;
                    }
                    this.m_Value.list.SetActive(UnitListRootWindow.Content.ItemAccessor.SVB_KEY_BADGE, true);
                    break;
                  case UnitListWindow.EditType.PARTY:
                  case UnitListWindow.EditType.PARTY_TOWER:
                    this.m_SlotLabel = this.m_RootWindow.AttachSlotLabel(this.m_Param, this.m_Node);
                    this.m_Value.list.SetActive(UnitListRootWindow.Content.ItemAccessor.SVB_KEY_USE, false);
                    this.m_Value.list.SetActive(UnitListRootWindow.Content.ItemAccessor.SVB_KEY_BADGE, false);
                    this.m_Value.list.SetActive(UnitListRootWindow.Content.ItemAccessor.SVB_KEY_DIED, false);
                    this.m_Value.list.SetActive(UnitListRootWindow.Content.ItemAccessor.SVB_KEY_NO_ENTRY, false);
                    this.m_Value.list.SetActive(UnitListRootWindow.Content.ItemAccessor.SVB_KEY_SAME_UNIT, false);
                    this.m_Value.list.SetActive(UnitListRootWindow.Content.ItemAccessor.SVB_KEY_GVG_USED_UNIT, false);
                    this.m_Value.list.SetActive(UnitListRootWindow.Content.ItemAccessor.SVB_KEY_GVG_USED_TEXT, false);
                    CanvasGroup component1 = ((Component) this.m_Node).gameObject.GetComponent<CanvasGroup>();
                    if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component1, (UnityEngine.Object) null))
                      component1.interactable = this.m_Param.interactable;
                    bool flag = false;
                    if (this.m_Param.gvgUsedUnit > 0)
                    {
                      this.m_Value.list.SetActive(UnitListRootWindow.Content.ItemAccessor.SVB_KEY_GVG_USED_UNIT, true);
                      this.m_Value.list.SetActive(UnitListRootWindow.Content.ItemAccessor.SVB_KEY_GVG_USED_TEXT, true);
                      Text result = (Text) null;
                      this.m_Value.list.GetField(UnitListRootWindow.Content.ItemAccessor.SVB_KEY_GVG_USED_TEXT, ref result);
                      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) result, (UnityEngine.Object) null))
                        result.text = string.Format(LocalizedText.Get("sys.GVG_REMAIN_USED_UNIT"), (object) this.m_Param.gvgUsedUnit.ToString());
                      if (this.m_Param.gvgUsedUnitSelect)
                      {
                        GameObject gameObject = this.m_Value.list.GetGameObject(UnitListRootWindow.Content.ItemAccessor.SVB_KEY_GVG_USED_UNIT);
                        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
                        {
                          Image component2 = gameObject.GetComponent<Image>();
                          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component2, (UnityEngine.Object) null))
                            ((Graphic) component2).raycastTarget = false;
                        }
                      }
                    }
                    else if (editType == UnitListWindow.EditType.PARTY_TOWER && MonoSingleton<GameManager>.Instance.TowerResuponse.IsDied_PlayerUnit(this.m_Param.unit))
                      this.m_Value.list.SetActive(UnitListRootWindow.Content.ItemAccessor.SVB_KEY_DIED, true);
                    else if (!this.m_Param.interactable)
                      this.m_Value.list.SetActive(UnitListRootWindow.Content.ItemAccessor.SVB_KEY_NO_ENTRY, true);
                    else if (this.m_Param.sameUnit)
                      this.m_Value.list.SetActive(UnitListRootWindow.Content.ItemAccessor.SVB_KEY_SAME_UNIT, true);
                    else
                      flag = true;
                    ImageArray component3 = this.m_Value.list.GetComponent<ImageArray>(UnitListRootWindow.Content.ItemAccessor.SVB_KEY_TEAM);
                    if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component3, (UnityEngine.Object) null))
                    {
                      int data = this.m_RootWindow.GetData<int>("data_party_index", -1);
                      ((Component) component3).gameObject.SetActive(false);
                      if (this.m_Param.partyIndex >= 0 && this.m_Param.partyIndex < component3.Images.Length && data != this.m_Param.partyIndex)
                      {
                        component3.ImageIndex = this.m_Param.partyIndex;
                        ((Component) component3).gameObject.SetActive(true);
                      }
                    }
                    if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_SlotLabel, (UnityEngine.Object) null))
                    {
                      Image componentInChildren = ((Component) this.m_SlotLabel).GetComponentInChildren<Image>();
                      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren, (UnityEngine.Object) null))
                      {
                        float num = !flag ? 0.5f : 1f;
                        ((Graphic) componentInChildren).color = new Color(num, num, num, 1f);
                        break;
                      }
                      break;
                    }
                    break;
                  case UnitListWindow.EditType.SUPPORT:
                    this.m_Value.list.SetActive(UnitListRootWindow.Content.ItemAccessor.SVB_KEY_USE, false);
                    this.m_Value.list.SetActive(UnitListRootWindow.Content.ItemAccessor.SVB_KEY_BADGE, false);
                    break;
                  default:
                    this.m_Value.list.SetActive(UnitListRootWindow.Content.ItemAccessor.SVB_KEY_USE, false);
                    this.m_Value.list.SetActive(UnitListRootWindow.Content.ItemAccessor.SVB_KEY_BADGE, false);
                    break;
                }
              }
            }
            else if (this.m_ContentType == UnitListRootWindow.ContentType.SUPPORT)
            {
              this.m_Value.list.SetActive(UnitListRootWindow.Content.ItemAccessor.SVB_KEY_USE, this.m_Param.partySelect);
              if (this.m_Param.support != null && this.m_Param.support.IsFriend())
                this.m_Value.list.SetActive(UnitListRootWindow.Content.ItemAccessor.SVB_KEY_FRIEND, true);
              this.m_Value.list.SetActive(UnitListRootWindow.Content.ItemAccessor.SVB_KEY_LOCKED, !this.m_Param.interactable);
              ImageArray component = this.m_Value.list.GetComponent<ImageArray>(UnitListRootWindow.Content.ItemAccessor.SVB_KEY_TEAM);
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
              {
                ((Component) component).gameObject.SetActive(false);
                if (this.m_Param.partyIndex >= 0)
                {
                  component.ImageIndex = this.m_Param.partyIndex;
                  ((Component) component).gameObject.SetActive(true);
                }
              }
            }
            this.m_SortBadge = this.m_Value.list.GetComponent<SortBadge>(UnitListRootWindow.Content.ItemAccessor.SVB_KEY_SORT);
          }
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_SortBadge, (UnityEngine.Object) null))
            return;
          if (this.m_SortWindow != null)
          {
            UnitListSortWindow.SelectType section = this.m_SortWindow.GetSection();
            this.SetSortValue(section, UnitListSortWindow.GetSortStatus(this.m_Param, section), this.m_Param.param == null ? string.Empty : this.m_Param.param.name);
          }
          else
            this.SetSortValue(UnitListSortWindow.SelectType.TIME, 0, this.m_Param.param.name);
        }

        public void Clear()
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_DataSource, (UnityEngine.Object) null))
          {
            this.m_DataSource.Clear();
            this.m_DataSource = (DataSource) null;
          }
          this.m_SortBadge = (SortBadge) null;
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_SlotLabel, (UnityEngine.Object) null))
            return;
          if (this.m_RootWindow != null)
            this.m_RootWindow.DettachSlotLabel(this.m_SlotLabel);
          this.m_SlotLabel = (RectTransform) null;
        }

        public void ForceUpdate()
        {
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Node, (UnityEngine.Object) null))
            return;
          GameParameter.UpdateAll(((Component) this.m_Node).gameObject);
        }

        public void LateUpdate()
        {
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_SlotLabel, (UnityEngine.Object) null))
            return;
          this.m_SlotLabel.anchoredPosition = this.m_Node.GetWorldPos();
        }

        public void SetSortValue(UnitListSortWindow.SelectType section, int value, string name = "")
        {
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_SortBadge, (UnityEngine.Object) null))
            return;
          UnitListSortWindow.SelectType selectType = UnitListSortWindow.SelectType.TIME | UnitListSortWindow.SelectType.RARITY | UnitListSortWindow.SelectType.LEVEL;
          if ((section & selectType) == UnitListSortWindow.SelectType.NONE)
          {
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_SortBadge.Value, (UnityEngine.Object) null))
            {
              if (section == UnitListSortWindow.SelectType.NAME)
              {
                this.m_SortBadge.SetName(name);
                this.m_SortBadge.SetValue(string.Empty);
              }
              else
              {
                this.m_SortBadge.SetName(string.Empty);
                this.m_SortBadge.SetValue(value);
              }
            }
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_SortBadge.Icon, (UnityEngine.Object) null))
              this.m_SortBadge.Icon.sprite = UnitListSortWindow.GetIcon(section);
            ((Component) this.m_SortBadge).gameObject.SetActive(true);
            this.m_Value.list.SetActive(UnitListRootWindow.Content.ItemAccessor.SVB_KEY_LEVEL, false);
          }
          else
          {
            ((Component) this.m_SortBadge).gameObject.SetActive(false);
            this.m_Value.list.SetActive(UnitListRootWindow.Content.ItemAccessor.SVB_KEY_LEVEL, true);
          }
        }
      }

      public class ItemSource : ContentSource
      {
        private List<UnitListRootWindow.Content.ItemSource.ItemParam> m_Params = new List<UnitListRootWindow.Content.ItemSource.ItemParam>();
        private Vector2 m_AnchorePos = Vector2.zero;
        private int m_Forcus = -1;

        public override void Initialize(ContentController controller)
        {
          base.Initialize(controller);
          this.Setup();
        }

        public override void Release()
        {
          this.m_Params.Clear();
          base.Release();
        }

        public int Add(
          UnitListRootWindow.Content.ItemSource.ItemParam param)
        {
          this.m_Params.Add(param);
          return this.m_Params.Count;
        }

        public void AnchorePos(Vector2 pos) => this.m_AnchorePos = pos;

        public void ForcusIndex(int index) => this.m_Forcus = index;

        public void Setup()
        {
          this.Clear();
          this.SetTable((ContentSource.Param[]) this.m_Params.ToArray());
          this.contentController.Resize();
          if (this.m_Forcus != -1)
          {
            ContentSource.Param obj = this.GetParam(this.m_Forcus);
            if (obj != null)
            {
              ContentGrid grid = this.contentController.GetGrid(obj.id);
              Vector2 anchorePosFromGrid = this.contentController.GetAnchorePosFromGrid(grid.x, grid.y);
              anchorePosFromGrid.x += this.contentController.GetSpacing().x;
              anchorePosFromGrid.y -= this.contentController.GetSpacing().y;
              this.contentController.anchoredPosition = anchorePosFromGrid;
            }
            else
              this.contentController.anchoredPosition = Vector2.zero;
          }
          else
            this.contentController.anchoredPosition = this.m_AnchorePos;
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
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.contentController.scroller, (UnityEngine.Object) null))
            this.contentController.scroller.StopMovement();
          this.m_AnchorePos = Vector2.zero;
          this.m_Forcus = -1;
        }

        public class ItemParam : ContentSource.Param
        {
          private UnitListRootWindow.Content.ItemAccessor m_Accessor = new UnitListRootWindow.Content.ItemAccessor();

          public ItemParam(UnitListWindow window, UnitListWindow.Data param)
          {
            this.m_Accessor.Setup(window, param);
          }

          public override bool IsValid() => this.m_Accessor.isValid;

          public UnitListRootWindow.Content.ItemAccessor accerror => this.m_Accessor;

          public override void Release() => this.m_Accessor.Release();

          public override void LateUpdate() => this.m_Accessor.LateUpdate();

          public override void OnEnable(ContentNode node)
          {
            this.m_Accessor.Bind(node);
            this.m_Accessor.ForceUpdate();
          }

          public override void OnDisable(ContentNode node) => this.m_Accessor.Clear();

          public override void OnClick(ContentNode node)
          {
          }
        }
      }
    }

    [Serializable]
    public class SerializeParam : FlowWindowBase.SerializeParamBase
    {
      public string unitList;
      public string pieceList;
      public string supportList;
      public GameObject listParent;
      public string tooltipPrefab;

      public override System.Type type => typeof (UnitListRootWindow);
    }

    public class ListData
    {
      public bool isValid;
      public string key = string.Empty;
      public object response;
      public List<UnitListWindow.Data> data = new List<UnitListWindow.Data>();
      public List<UnitListWindow.Data> calcData = new List<UnitListWindow.Data>();
      public Vector2 anchorePos = Vector2.zero;
      public UnitListRootWindow.Tab selectTab;
      public long selectUniqueID;

      public void Delete()
      {
        this.isValid = false;
        this.data.Clear();
        this.calcData.Clear();
      }

      public void Refresh()
      {
        for (int index = 0; index < this.data.Count; ++index)
          this.data[index].Refresh();
      }

      public void RefreshTabMask()
      {
        for (int index = 0; index < this.data.Count; ++index)
          this.data[index].RefreshTabMask();
      }

      public List<long> GetUniqs()
      {
        List<long> uniqs = new List<long>();
        for (int index = 0; index < this.calcData.Count; ++index)
        {
          long uniq = this.calcData[index].GetUniq();
          if (uniq > 0L)
            uniqs.Add(uniq);
        }
        return uniqs;
      }
    }

    public class TabRegister
    {
      public UnitListRootWindow.Tab tab;
      public Vector2 anchorePos;
      public long forcus;
    }

    protected class Json_ReqSupporterResponse
    {
      public Json_Support[] supports;
    }
  }
}
