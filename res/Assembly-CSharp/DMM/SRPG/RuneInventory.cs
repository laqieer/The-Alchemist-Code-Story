// Decompiled with JetBrains decompiler
// Type: SRPG.RuneInventory
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(0, "セットアップ開始", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(1, "セットアップ完了", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(9, "表示更新", FlowNode.PinTypes.Input, 9)]
  [FlowNode.Pin(10, "ルーンスロット１選択", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(11, "ルーンスロット２選択", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(12, "ルーンスロット３選択", FlowNode.PinTypes.Input, 12)]
  [FlowNode.Pin(13, "ルーンスロット４選択", FlowNode.PinTypes.Input, 13)]
  [FlowNode.Pin(14, "ルーンスロット５選択", FlowNode.PinTypes.Input, 14)]
  [FlowNode.Pin(15, "ルーンスロット６選択", FlowNode.PinTypes.Input, 15)]
  [FlowNode.Pin(40, "装備すべて外す", FlowNode.PinTypes.Input, 40)]
  [FlowNode.Pin(41, "装備すべて外す通信開始", FlowNode.PinTypes.Output, 41)]
  [FlowNode.Pin(42, "装備すべて外す通信完了", FlowNode.PinTypes.Input, 42)]
  [FlowNode.Pin(50, "装備分解", FlowNode.PinTypes.Input, 50)]
  [FlowNode.Pin(60, "ルーン逆引きを開く", FlowNode.PinTypes.Input, 60)]
  [FlowNode.Pin(70, "ルーン所持一覧の更新", FlowNode.PinTypes.Input, 70)]
  [FlowNode.Pin(101, "ヘルプ画面を表示", FlowNode.PinTypes.Input, 101)]
  [FlowNode.Pin(100, "ルーン画面を閉じる", FlowNode.PinTypes.Input, 100)]
  public class RuneInventory : MonoBehaviour, IFlowInterface
  {
    private const int OUTPUT_SETUP_START = 0;
    private const int INPUT_SETUP_FINISH = 1;
    private const int INPUT_RUNE_REFRESH = 9;
    private const int INPUT_RUNE_SELECT_SLOT_1 = 10;
    private const int INPUT_RUNE_SELECT_SLOT_2 = 11;
    private const int INPUT_RUNE_SELECT_SLOT_3 = 12;
    private const int INPUT_RUNE_SELECT_SLOT_4 = 13;
    private const int INPUT_RUNE_SELECT_SLOT_5 = 14;
    private const int INPUT_RUNE_SELECT_SLOT_6 = 15;
    private const int INPUT_RUNE_UNEQUIP_ALL = 40;
    private const int OUTPUT_START_NETWORK_UNEQUIP_ALL = 41;
    private const int INPUT_FINISH_NETWORK_UNEQUIP_ALL = 42;
    private const int INPUT_RUNE_DISASSEMBLY = 50;
    private const int INPUT_RUNE_DROP_WINDOW_OPEN = 60;
    private const int INPUT_RUNE_SELECTABLE_LIST_REFRESH = 70;
    private const int INPUT_RUNE_CLOSE = 100;
    private const int INPUT_RUNE_HELP = 101;
    [SerializeField]
    private RuneManager mRuneManager;
    [SerializeField]
    private GameObject UnitNamePlate;
    [SerializeField]
    private GameObject mSelectableListParent;
    [SerializeField]
    private GameObject mEquipPopupParent;
    [SerializeField]
    private GameObject mUnEquipPopupParent;
    [SerializeField]
    private GameObject mDisassemblyParent;
    [SerializeField]
    private string PREFAB_PATH_RUNE_DROP_QUEST_WINDOW = "UI/Rune/RuneDropQuestWindow";
    private UnitData mCurrentUnit;
    private bool mIsRestore;
    private GameObject mRuneDropQuestWindow;

    public void Awake()
    {
      if (!Object.op_Equality((Object) this.mRuneManager, (Object) null))
        return;
      DebugUtility.LogError("RuneInventory.mRuneManager の設定がされていません");
    }

    public void Init(bool is_restore)
    {
      this.mIsRestore = is_restore;
      if (Object.op_Implicit((Object) this.mRuneManager))
        this.mRuneManager.CloseRuneWindow();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 0);
    }

    private void Setup()
    {
      if (!this.RefreshCurrentUnit())
      {
        DebugUtility.LogError("GlobalVars.SelectedUnitUniqueID からユニットを取得できなかった");
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      }
      else
      {
        GlobalVars.SelectedEquipmentSlot.Set(-1);
        this.mRuneManager.Initialize(this.mCurrentUnit);
        this.RefreshAll();
        if (!this.mIsRestore || !RuneDropQuestWindow.IsSelectedTab)
          return;
        this.OpenRuneDropQuestWindow(this.mIsRestore);
      }
    }

    private void OnDestroy()
    {
      if (!Object.op_Inequality((Object) this.mRuneDropQuestWindow, (Object) null))
        return;
      Object.Destroy((Object) this.mRuneDropQuestWindow);
      this.mRuneDropQuestWindow = (GameObject) null;
    }

    private bool RefreshCurrentUnit()
    {
      UnitData unitDataByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID((long) GlobalVars.SelectedUnitUniqueID);
      if (unitDataByUniqueId == null)
        return false;
      this.mCurrentUnit = new UnitData();
      this.mCurrentUnit.Setup(unitDataByUniqueId);
      return true;
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 10:
        case 11:
        case 12:
        case 13:
        case 14:
        case 15:
          this.mRuneManager.SelectedRuneSlot((RuneSlotIndex) (byte) (pinID - 10));
          break;
        case 40:
          UIUtility.ConfirmBox(LocalizedText.Get("sys.RUNE_UNEQUIP_ALL_CONFIRM"), (UIUtility.DialogResultEvent) (yes_button =>
          {
            if (Object.op_Inequality((Object) UnitEnhanceV3.Instance, (Object) null))
              UnitEnhanceV3.Instance.BeginStatusChangeCheck();
            this.mRuneManager.SetDoUnEquipmentAll();
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 41);
          }), (UIUtility.DialogResultEvent) null);
          break;
        case 42:
          this.StartEquipEffect();
          this.mRuneManager.RefreshRuneEquipFinished();
          break;
        default:
          if (pinID != 1)
          {
            if (pinID != 50)
            {
              if (pinID != 60)
              {
                if (pinID != 70)
                {
                  if (pinID != 100)
                    break;
                  UnitEnhanceV3.Instance.RuneUIActive(false);
                  break;
                }
                this.mRuneManager.RefreshSelectableList();
                break;
              }
              this.OpenRuneDropQuestWindow(false);
              break;
            }
            this.mRuneManager.OpenDisassembly();
            break;
          }
          this.Setup();
          break;
      }
    }

    private void StartEquipEffect()
    {
      this.StartCoroutine(UnitEnhanceV3.Instance.PlayRuneEquipmentEffect(false));
    }

    private void RefreshAll() => this.RefreshNamePlate();

    public void SetParentPopupObject(GameObject obj, RuneInventory.WindowParent type)
    {
      switch (type)
      {
        case RuneInventory.WindowParent.SelectableList:
          if (!Object.op_Implicit((Object) this.mSelectableListParent))
            break;
          obj.transform.SetParent(this.mSelectableListParent.transform, false);
          break;
        case RuneInventory.WindowParent.EquipPopup:
          if (!Object.op_Implicit((Object) this.mEquipPopupParent))
            break;
          obj.transform.SetParent(this.mEquipPopupParent.transform, false);
          break;
        case RuneInventory.WindowParent.UnEquipPopup:
          if (!Object.op_Implicit((Object) this.mUnEquipPopupParent))
            break;
          obj.transform.SetParent(this.mUnEquipPopupParent.transform, false);
          break;
        case RuneInventory.WindowParent.Disassembly:
          if (!Object.op_Implicit((Object) this.mDisassemblyParent))
            break;
          obj.transform.SetParent(this.mDisassemblyParent.transform, false);
          break;
      }
    }

    private void RefreshNamePlate()
    {
      if (Object.op_Equality((Object) this.UnitNamePlate, (Object) null) || this.mCurrentUnit == null)
        return;
      DataSource.Bind<UnitData>(this.UnitNamePlate, this.mCurrentUnit);
      GameParameter.UpdateAll(this.UnitNamePlate);
    }

    private void OpenRuneDropQuestWindow(bool is_restore)
    {
      GameObject gameObject = AssetManager.Load<GameObject>(this.PREFAB_PATH_RUNE_DROP_QUEST_WINDOW);
      if (!Object.op_Inequality((Object) gameObject, (Object) null))
        return;
      this.mRuneDropQuestWindow = Object.Instantiate<GameObject>(gameObject);
      RuneDropQuestWindow componentInChildren = this.mRuneDropQuestWindow.GetComponentInChildren<RuneDropQuestWindow>();
      if (!Object.op_Inequality((Object) componentInChildren, (Object) null))
        return;
      componentInChildren.Init(is_restore);
    }

    public enum WindowParent
    {
      SelectableList,
      EquipPopup,
      UnEquipPopup,
      Disassembly,
    }
  }
}
