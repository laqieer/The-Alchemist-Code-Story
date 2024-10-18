// Decompiled with JetBrains decompiler
// Type: SRPG.RuneEquipWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(10, "強化", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(11, "装備", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(12, "装備解除", FlowNode.PinTypes.Input, 12)]
  [FlowNode.Pin(13, "覚醒", FlowNode.PinTypes.Input, 13)]
  [FlowNode.Pin(14, "ステータス変更", FlowNode.PinTypes.Input, 14)]
  [FlowNode.Pin(20, "装備通信開始", FlowNode.PinTypes.Output, 20)]
  [FlowNode.Pin(100, "装備通信完了後の描画更新", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(101, "お気に入り通信完了後の描画更新", FlowNode.PinTypes.Input, 101)]
  [FlowNode.Pin(1000, "自身を閉じる", FlowNode.PinTypes.Output, 1000)]
  public class RuneEquipWindow : MonoBehaviour, IFlowInterface
  {
    private const int INPUT_RUNE_ENHANCE = 10;
    private const int INPUT_RUNE_EQUIP = 11;
    private const int INPUT_RUNE_UNEQUIP = 12;
    private const int INPUT_RUNE_EVO = 13;
    private const int INPUT_STATUS_RESET = 14;
    private const int OUTPUT_REQ_EQUIP = 20;
    private const int INPUT_FINISH_EQUIP = 100;
    private const int INPUT_FINISH_FAVORITE = 101;
    private const int OUTPUT_CLOSE_WINDOW = 1000;
    [SerializeField]
    private RuneDrawInfo mRuneDrawInfo;
    [SerializeField]
    private RuneIcon mRuneIcon;
    [SerializeField]
    private RuneDrawBaseState mRuneDrawBaseState;
    [SerializeField]
    private RuneDrawEvoState mRuneDrawEvoState;
    [SerializeField]
    private RuneDrawSetEff mRuneDrawSetEff;
    [SerializeField]
    private Button mEnhanceButton;
    [SerializeField]
    private Button mEvoButton;
    [SerializeField]
    private Button mEquipButton;
    [SerializeField]
    private Button mUnEquipButton;
    [SerializeField]
    private Button mFavorite;
    [SerializeField]
    private FlowNode_ReqRuneFavorite mReqFavoriteNode;
    private RuneManager mRuneManager;
    private UnitData mUnit;
    private RuneManager.RuneEquip mIsEquipWindow = RuneManager.RuneEquip.Equip;

    public BindRuneData mRuneData { get; private set; }

    public void Awake()
    {
    }

    private void OnDestroy()
    {
    }

    private void RuneEquip()
    {
      if (Object.op_Inequality((Object) UnitEnhanceV3.Instance, (Object) null))
        UnitEnhanceV3.Instance.BeginStatusChangeCheck();
      this.mRuneManager.SetDoEquipment(this.mRuneData);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 20);
    }

    private void RuneUnequip()
    {
      if (Object.op_Inequality((Object) UnitEnhanceV3.Instance, (Object) null))
        UnitEnhanceV3.Instance.BeginStatusChangeCheck();
      this.mRuneManager.SetDoUnEquipment(this.mRuneData);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 20);
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 10:
          this.mRuneManager.SelectedEnhance(this.mRuneData);
          break;
        case 11:
          RuneData rune = this.mRuneData.Rune;
          UnitData unitData = (UnitData) null;
          if (rune != null)
            unitData = rune.GetOwner();
          if (unitData != null && unitData.UnitID != this.mUnit.UnitID)
          {
            UIUtility.ConfirmBox(string.Format(LocalizedText.Get("sys.RUNE_EQUIP_ANOTHER_UNIT_CONFIRM"), (object) unitData.UnitParam.name), (UIUtility.DialogResultEvent) (yes_button => this.RuneEquip()), (UIUtility.DialogResultEvent) null);
            break;
          }
          if (this.mUnit.EquipRune((int) (byte) this.mRuneData.RuneParam.slot_index) != null)
          {
            UIUtility.ConfirmBox(LocalizedText.Get("sys.RUNE_EQUIP_CONFIRM"), (UIUtility.DialogResultEvent) (yes_button => this.RuneEquip()), (UIUtility.DialogResultEvent) null);
            break;
          }
          this.RuneEquip();
          break;
        case 12:
          this.RuneUnequip();
          break;
        case 13:
          this.mRuneManager.SelectedEvolution(this.mRuneData);
          break;
        case 14:
          this.mRuneManager.SelectedResetStatus(this.mRuneData);
          break;
        case 100:
          this.mRuneManager.RefreshRuneEquipFinished();
          this.StartEquipEffect();
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 1000);
          break;
        case 101:
          this.RefreshButton();
          break;
      }
    }

    private void StartEquipEffect()
    {
      this.StartCoroutine(UnitEnhanceV3.Instance.PlayRuneEquipmentEffect(this.mIsEquipWindow == RuneManager.RuneEquip.Equip));
    }

    public void Setup(
      RuneManager manager,
      UnitData unit,
      BindRuneData rune_data,
      RuneManager.RuneEquip rune_equip)
    {
      if (unit == null || rune_data == null)
        return;
      this.mRuneManager = manager;
      this.mUnit = unit;
      this.mRuneData = rune_data;
      this.mIsEquipWindow = rune_equip;
      if (Object.op_Implicit((Object) this.mRuneDrawInfo))
        this.mRuneDrawInfo.SetDrawParam(this.mRuneData);
      if (Object.op_Implicit((Object) this.mRuneIcon))
        this.mRuneIcon.Setup(this.mRuneData);
      if (Object.op_Implicit((Object) this.mRuneDrawBaseState))
        this.mRuneDrawBaseState.SetDrawParam(this.mRuneData);
      if (Object.op_Implicit((Object) this.mRuneDrawEvoState))
        this.mRuneDrawEvoState.SetDrawParam(this.mRuneData);
      if (Object.op_Implicit((Object) this.mRuneDrawSetEff))
        this.mRuneDrawSetEff.SetDrawParam(this.mRuneData);
      if (Object.op_Implicit((Object) this.mFavorite))
        DataSource.Bind<BindRuneData>(((Component) this.mFavorite).gameObject, this.mRuneData);
      if (Object.op_Implicit((Object) this.mReqFavoriteNode))
        this.mReqFavoriteNode.SetRuneData(this.mRuneData);
      this.Refresh();
    }

    public void Refresh()
    {
      if (Object.op_Implicit((Object) this.mRuneDrawInfo))
        this.mRuneDrawInfo.Refresh();
      if (Object.op_Implicit((Object) this.mRuneIcon))
        this.mRuneIcon.Refresh();
      if (Object.op_Implicit((Object) this.mRuneDrawBaseState))
        this.mRuneDrawBaseState.Refresh();
      if (Object.op_Implicit((Object) this.mRuneDrawEvoState))
        this.mRuneDrawEvoState.Refresh();
      if (Object.op_Implicit((Object) this.mFavorite))
        GameParameter.UpdateAll(((Component) this.mFavorite).gameObject);
      this.RefreshButton();
    }

    public void RefreshButton()
    {
      if (this.mRuneData == null)
        return;
      if (Object.op_Inequality((Object) this.mEquipButton, (Object) null) && Object.op_Inequality((Object) this.mUnEquipButton, (Object) null))
      {
        bool flag = this.mIsEquipWindow == RuneManager.RuneEquip.Equip;
        ((Component) this.mEquipButton).gameObject.SetActive(flag);
        ((Component) this.mUnEquipButton).gameObject.SetActive(!flag);
      }
      if (Object.op_Inequality((Object) this.mEnhanceButton, (Object) null) && Object.op_Inequality((Object) this.mEvoButton, (Object) null))
      {
        bool isEvoNext = this.mRuneData.IsEvoNext;
        ((Component) this.mEnhanceButton).gameObject.SetActive(!isEvoNext);
        ((Component) this.mEvoButton).gameObject.SetActive(isEvoNext);
        ((Selectable) this.mEvoButton).interactable = this.mRuneData.IsCanEvo;
      }
      if (Object.op_Inequality((Object) this.mFavorite, (Object) null))
        GameParameter.UpdateAll(((Component) this.mFavorite).gameObject);
      if (!Object.op_Inequality((Object) this.mRuneManager, (Object) null) || !Object.op_Inequality((Object) this.mRuneManager.RuneSelectableListWindow, (Object) null))
        return;
      this.mRuneManager.RuneSelectableListWindow.Refresh();
    }

    public void CloseSelf() => FlowNode_GameObject.ActivateOutputLinks((Component) this, 1000);
  }
}
