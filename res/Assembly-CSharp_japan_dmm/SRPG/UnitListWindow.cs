// Decompiled with JetBrains decompiler
// Type: SRPG.UnitListWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("UI/UnitListWindow", 32741)]
  [FlowNode.Pin(100, "所持ユニットウィンド開く", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(101, "編成ユニットウィンド開く", FlowNode.PinTypes.Input, 101)]
  [FlowNode.Pin(102, "タワー編成ユニットウィンド開く", FlowNode.PinTypes.Input, 102)]
  [FlowNode.Pin(103, "装備強化ユニットウィンド開く", FlowNode.PinTypes.Input, 103)]
  [FlowNode.Pin(104, "サポート傭兵設定ユニットウィンド開く", FlowNode.PinTypes.Input, 104)]
  [FlowNode.Pin(105, "ショップ装備強化ユニットウィンド開く", FlowNode.PinTypes.Input, 105)]
  [FlowNode.Pin(110, "ユニット欠片ウィンド開く", FlowNode.PinTypes.Input, 110)]
  [FlowNode.Pin(120, "汎用欠片変換ウィンド開く", FlowNode.PinTypes.Input, 120)]
  [FlowNode.Pin(300, "傭兵雇用ウィンド開く", FlowNode.PinTypes.Input, 300)]
  [FlowNode.Pin(310, "ホームユニットウィンド開く", FlowNode.PinTypes.Input, 310)]
  [FlowNode.Pin(190, "ウィンド開く", FlowNode.PinTypes.Output, 398)]
  [FlowNode.Pin(191, "ウィンド開いた", FlowNode.PinTypes.Output, 399)]
  [FlowNode.Pin(400, "リストタブ切り替え", FlowNode.PinTypes.Input, 400)]
  [FlowNode.Pin(410, "リスト更新", FlowNode.PinTypes.Input, 410)]
  [FlowNode.Pin(430, "リスト再生成", FlowNode.PinTypes.Input, 411)]
  [FlowNode.Pin(420, "アイテム選択", FlowNode.PinTypes.Input, 419)]
  [FlowNode.Pin(419, "アイテムホールド", FlowNode.PinTypes.Input, 420)]
  [FlowNode.Pin(421, "ユニット選択", FlowNode.PinTypes.Output, 422)]
  [FlowNode.Pin(422, "ユニット解除", FlowNode.PinTypes.Output, 423)]
  [FlowNode.Pin(423, "欠片選択", FlowNode.PinTypes.Output, 423)]
  [FlowNode.Pin(426, "傭兵雇用選択", FlowNode.PinTypes.Output, 426)]
  [FlowNode.Pin(427, "傭兵雇用解除", FlowNode.PinTypes.Output, 427)]
  [FlowNode.Pin(429, "アイテムホールド", FlowNode.PinTypes.Output, 429)]
  [FlowNode.Pin(440, "サポートリスト更新", FlowNode.PinTypes.Input, 440)]
  [FlowNode.Pin(480, "サポートリスト取得", FlowNode.PinTypes.Output, 480)]
  [FlowNode.Pin(481, "サポートリスト強制取得", FlowNode.PinTypes.Output, 481)]
  [FlowNode.Pin(490, "ウィンド閉じる", FlowNode.PinTypes.Input, 490)]
  [FlowNode.Pin(491, "ウィンド閉じる", FlowNode.PinTypes.Output, 491)]
  [FlowNode.Pin(492, "ウィンド閉じた", FlowNode.PinTypes.Output, 492)]
  [FlowNode.Pin(500, "ソートウィンド開く", FlowNode.PinTypes.Input, 500)]
  [FlowNode.Pin(510, "ソートウィンド閉じる", FlowNode.PinTypes.Input, 510)]
  [FlowNode.Pin(520, "ソートウィンド確定", FlowNode.PinTypes.Input, 520)]
  [FlowNode.Pin(530, "ソートウィンドトグル切り替え", FlowNode.PinTypes.Input, 530)]
  [FlowNode.Pin(590, "ソートウィンド確定した", FlowNode.PinTypes.Output, 590)]
  [FlowNode.Pin(591, "ソートウィンドキャンセルした", FlowNode.PinTypes.Output, 591)]
  [FlowNode.Pin(600, "フィルタウィンド開く", FlowNode.PinTypes.Input, 600)]
  [FlowNode.Pin(610, "フィルタウィンド閉じる", FlowNode.PinTypes.Input, 610)]
  [FlowNode.Pin(620, "フィルタウィンド確定", FlowNode.PinTypes.Input, 620)]
  [FlowNode.Pin(640, "フィルタウィンドトグル全て選択", FlowNode.PinTypes.Input, 640)]
  [FlowNode.Pin(650, "フィルタウィンドトグル全てクリア", FlowNode.PinTypes.Input, 650)]
  [FlowNode.Pin(660, "フィルタウィンドタブ切り替え", FlowNode.PinTypes.Input, 660)]
  [FlowNode.Pin(690, "フィルタウィンド確定した", FlowNode.PinTypes.Output, 690)]
  [FlowNode.Pin(691, "フィルタウィンドキャンセルした", FlowNode.PinTypes.Output, 691)]
  [FlowNode.Pin(1000, "ウィンドロック", FlowNode.PinTypes.Input, 1000)]
  [FlowNode.Pin(700, "レンタルユニット選択", FlowNode.PinTypes.Input, 700)]
  [FlowNode.Pin(1010, "ウィンドロック解除", FlowNode.PinTypes.Input, 1010)]
  public class UnitListWindow : FlowNodePersistent
  {
    public const int INPUT_UNITWINDOW_OPEN = 100;
    public const int INPUT_UNITWINDOW_EDIT_OPEN = 101;
    public const int INPUT_UNITWINDOW_TWEDIT_OPEN = 102;
    public const int INPUT_UNITWINDOW_EQUIP_OPEN = 103;
    public const int INPUT_UNITWINDOW_SUPPORT_OPEN = 104;
    public const int INPUT_UNITWINDOW_SHOPEQUIP_OPEN = 105;
    public const int INPUT_PIECEWINDOW_OPEN = 110;
    public const int INPUT_PIECEEXCHANGE_OPEN = 120;
    public const int INPUT_SUPPORTWINDOW_OPEN = 300;
    public const int INPUT_HOMEUNITWINDOW_OPEN = 310;
    public const int OUTPUT_WINDOW_OPEN = 190;
    public const int OUTPUT_WINDOW_OPENED = 191;
    public const int INPUT_LIST_TABCHANGE = 400;
    public const int INPUT_LIST_REFRESH = 410;
    public const int INPUT_LIST_HOLD = 419;
    public const int INPUT_LIST_SELECT = 420;
    public const int INPUT_LIST_REMAKE = 430;
    public const int INPUT_SUPPORTLIST_REFRESH = 440;
    public const int OUTPUT_SELECT_UNIT = 421;
    public const int OUTPUT_REMOVE_UNIT = 422;
    public const int OUTPUT_SELECT_PIECE = 423;
    public const int OUTPUT_SELECT_SUPPORT = 426;
    public const int OUTPUT_REMOVE_SUPPORT = 427;
    public const int OUTPUT_HOLD_ITEM = 429;
    public const int OUTPUT_WEBAPI_SUPPORT_UPDATE = 480;
    public const int OUTPUT_WEBAPI_SUPPORT_UPDATEFORCE = 481;
    public const int INPUT_WINDOW_CLOSE = 490;
    public const int OUTPUT_WINDOW_CLOSE = 491;
    public const int OUTPUT_WINDOW_CLOSED = 492;
    public const int INPUT_SORTWINDOW_OPEN = 500;
    public const int INPUT_SORTWINDOW_CLOSE = 510;
    public const int INPUT_SORTWINDOW_OK = 520;
    public const int INPUT_SORTWINDOW_TGLCHANGE = 530;
    public const int OUTPUT_SORTWINDOW_CONFIRMED = 590;
    public const int OUTPUT_SORTWINDOW_CANCELED = 591;
    public const int OUTPUT_SORTWINDOW_CLOSED = 599;
    public const int INPUT_FILTERWINDOW_OPEN = 600;
    public const int INPUT_FILTERWINDOW_CLOSE = 610;
    public const int INPUT_FILTERWWINDOW_OK = 620;
    public const int INPUT_FILTERWWINDOW_ALL = 640;
    public const int INPUT_FILTERWWINDOW_CLEAR = 650;
    public const int INPUT_FILTERWWINDOW_TABCHANGE = 660;
    public const int OUTPUT_FILTERWINDOW_CONFIRMED = 690;
    public const int OUTPUT_FILTERWINDOW_CANCELED = 691;
    public const int OUTPUT_FILTERWINDOW_CLOSED = 699;
    public const int INPUT_DECIDE_RENTAL_UNIT = 700;
    public const int INPUT_LOCK = 1000;
    public const int INPUT_UNLOCK = 1010;
    public const string DATA_REGISTER = "data_register";
    public const string DATA_EDIT = "data_edit";
    public const string DATA_UNIT = "data_unit";
    public const string DATA_UNITS = "data_units";
    public const string DATA_PARTY = "data_party";
    public const string DATA_PARTY_INDEX = "data_party_index";
    public const string DATA_QUEST = "data_quest";
    public const string DATA_ELEMENT = "data_element";
    public const string DATA_SELECTSLOT = "data_slot";
    public const string DATA_HEROONLY = "data_heroOnly";
    public const string DATA_SUPPORT = "data_support";
    public const string DATA_SUPPORT_RESPONSE = "data_supportres";
    public UnitListRootWindow.SerializeParam m_RootWindowParam;
    public UnitListSortWindow.SerializeParam m_SortWindowParam;
    public UnitListFilterWindow.SerializeParam m_FilterWindowParam;
    public UnitListTotalCombatPower.SerializeParam m_TotalCombatPowerParam;
    private FlowWindowController m_WindowController = new FlowWindowController();
    private UnitListRootWindow m_RootWindow;
    private UnitListSortWindow m_SortWindow;
    private UnitListFilterWindow m_FilterWindow;
    private UnitListTotalCombatPower m_TotalCombatPowerWindow;

    public UnitListRootWindow rootWindow => this.m_RootWindow;

    public UnitListSortWindow sortWindow => this.m_SortWindow;

    public UnitListFilterWindow filterWindow => this.m_FilterWindow;

    public UnitListTotalCombatPower totalCombatPowerWindow => this.m_TotalCombatPowerWindow;

    protected override void Awake()
    {
      base.Awake();
      this.m_WindowController.Initialize((FlowNode) this);
      this.m_WindowController.Add((FlowWindowBase.SerializeParamBase) this.m_RootWindowParam);
      this.m_RootWindow = this.m_WindowController.GetWindow<UnitListRootWindow>();
      this.m_WindowController.Add((FlowWindowBase.SerializeParamBase) this.m_TotalCombatPowerParam);
      this.m_TotalCombatPowerWindow = this.m_WindowController.GetWindow<UnitListTotalCombatPower>();
      if (AssetManager.UseDLC || !GameUtility.Config_UseAssetBundles.Value)
      {
        this.m_WindowController.Add((FlowWindowBase.SerializeParamBase) this.m_SortWindowParam);
        this.m_WindowController.Add((FlowWindowBase.SerializeParamBase) this.m_FilterWindowParam);
        this.m_SortWindow = this.m_WindowController.GetWindow<UnitListSortWindow>();
        this.m_FilterWindow = this.m_WindowController.GetWindow<UnitListFilterWindow>();
      }
      if (this.m_RootWindow != null)
        this.m_RootWindow.SetRoot(this);
      if (this.m_SortWindow != null)
        this.m_SortWindow.SetRoot(this);
      if (this.m_FilterWindow != null)
        this.m_FilterWindow.SetRoot(this);
      if (this.m_TotalCombatPowerWindow == null)
        return;
      this.m_TotalCombatPowerWindow.SetRoot(this);
    }

    private void Start()
    {
    }

    protected override void OnDestroy()
    {
      this.m_RootWindow = (UnitListRootWindow) null;
      this.m_WindowController.Release();
      base.OnDestroy();
    }

    private void Update() => this.m_WindowController.Update();

    public void ClearData()
    {
      if (this.m_RootWindow == null)
        return;
      this.m_RootWindow.ClearData();
    }

    public void AddData(string key, object value)
    {
      if (this.m_RootWindow == null)
        return;
      this.m_RootWindow.AddData(key, value);
    }

    public object GetData(string key)
    {
      return this.m_RootWindow != null ? this.m_RootWindow.GetData(key) : (object) null;
    }

    public T GetData<T>(string key)
    {
      return this.m_RootWindow != null ? this.m_RootWindow.GetData<T>(key) : default (T);
    }

    public T GetData<T>(string key, T defaultValue)
    {
      return this.m_RootWindow != null ? this.m_RootWindow.GetData<T>(key, defaultValue) : default (T);
    }

    public void Enabled(bool value)
    {
      if (this.m_WindowController == null)
        return;
      this.m_WindowController.enabled = value;
    }

    public bool IsEnabled() => this.m_WindowController != null && this.m_WindowController.enabled;

    public bool IsState(string stateName)
    {
      Animator componentInChildren = ((Component) this).GetComponentInChildren<Animator>();
      if (!Object.op_Inequality((Object) componentInChildren, (Object) null))
        return false;
      AnimatorStateInfo animatorStateInfo = componentInChildren.GetCurrentAnimatorStateInfo(0);
      return ((AnimatorStateInfo) ref animatorStateInfo).IsName(stateName);
    }

    public override void OnActivate(int pinID)
    {
      if (this.m_WindowController == null)
        return;
      if (pinID == 1000)
        this.Enabled(false);
      else if (pinID == 1010)
        this.Enabled(true);
      else
        this.m_WindowController.OnActivate(pinID);
    }

    public enum EditType
    {
      DEFAULT,
      PARTY,
      PARTY_TOWER,
      EQUIP,
      SUPPORT,
      SHOP_EQUIP,
      PIECE_EXCHANGE,
      HOME_PREVIEW,
    }

    public class Data
    {
      public string body;
      public UnitParam param;
      public UnitData unit;
      public SupportData support;
      public bool interactable;
      public bool partySelect;
      public int partyMainSlot;
      public int partySubSlot;
      public int partyIndex;
      public bool sameUnit;
      public int gvgUsedUnit;
      public bool gvgUsedUnitSelect;
      public bool unlockable;
      public int pieceAmount;
      public UnitListRootWindow.Tab tabMask;
      public long sortPriority;
      public int subSortPriority;

      public Data()
      {
        this.body = (string) null;
        this.param = (UnitParam) null;
        this.unit = (UnitData) null;
        this.interactable = true;
        this.partySelect = false;
        this.partyMainSlot = -1;
        this.partySubSlot = -1;
        this.partyIndex = -1;
        this.tabMask = UnitListRootWindow.Tab.NONE;
        this.sortPriority = 0L;
        this.subSortPriority = 0;
      }

      public Data(string _body)
        : this()
      {
        this.body = _body;
      }

      public Data(UnitParam _param)
        : this()
      {
        this.param = _param;
        this.pieceAmount = MonoSingleton<GameManager>.Instance.Player.GetItemAmount(this.param.piece);
        this.unlockable = this.param.GetUnlockNeedPieces() <= this.pieceAmount || MonoSingleton<GameManager>.Instance.MasterParam.IsUnlockableUnit(this.param.unlock_time, TimeManager.ServerTime);
      }

      public Data(UnitData _unit)
        : this(_unit.UnitParam)
      {
        this.unit = _unit;
      }

      public Data(SupportData _support)
        : this(_support.Unit)
      {
        this.support = _support;
      }

      public void Refresh()
      {
        this.RefreshTabMask();
        this.sortPriority = 0L;
      }

      public void RefreshTabMask()
      {
        if (this.support != null || this.param == null || this.partyMainSlot != -1 || this.partySubSlot != -1)
          this.tabMask = UnitListRootWindow.Tab.ALL;
        else
          this.tabMask = UnitListRootWindow.GetTabMask(this);
      }

      public void RefreshSortPriority(UnitListSortWindow.SelectType sortType)
      {
        this.sortPriority = UnitListSortWindow.GetSortPriority(this, sortType);
      }

      public void RefreshPartySlotPriority()
      {
        this.subSortPriority = 0;
        if (this.body == "empty")
          this.subSortPriority = int.MinValue;
        else if (this.partyMainSlot != -1)
        {
          this.subSortPriority = -(100 - this.partyMainSlot);
        }
        else
        {
          if (this.partySubSlot == -1)
            return;
          this.subSortPriority = -(50 - this.partySubSlot);
        }
      }

      public long GetUniq() => this.unit != null ? this.unit.UniqueID : 0L;
    }
  }
}
