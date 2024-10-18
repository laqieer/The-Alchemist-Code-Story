// Decompiled with JetBrains decompiler
// Type: SRPG.UnitSwitchLeaderSkillConfirm
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "Init", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(5, "Initialized", FlowNode.PinTypes.Output, 5)]
  [FlowNode.Pin(10, "リーダースキルを切り替えた", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(20, "リーダースキルを切り替えなかった", FlowNode.PinTypes.Output, 20)]
  public class UnitSwitchLeaderSkillConfirm : MonoBehaviour, IFlowInterface
  {
    public const int INIT = 1;
    public const int OUTPUT_INITIALIZED = 5;
    public const int OUTPUT_SWITCHED_LEADERSKILL = 10;
    public const int OUTPUT_NOT_SWITCHED_LEADERSKILL = 20;
    [SerializeField]
    private GameObject UnitLeaderSkillObject;
    [SerializeField]
    private GameObject CCLeaderSkillObject;
    [SerializeField]
    private GameObject Prefab_LeaderSkillDetail;
    [SerializeField]
    private SRPG_Button UnitLeaderSkillButton;
    [SerializeField]
    private SRPG_Button CCLeaderSkillButton;
    [SerializeField]
    private SRPG_Button ButtonOK;
    private UnitData mCurrentUnit;
    private SkillData mUnitLeaderSkill;
    private SkillData mCCLeaderSkill;
    private GameObject mSkillDetail;
    private bool mEquipConceptCardLeaderSkill;

    private bool Init()
    {
      ((Selectable) this.ButtonOK).interactable = false;
      this.ButtonOK.AddListener(new SRPG_Button.ButtonClickEvent(this.OnButtonClickOK));
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (Object.op_Equality((Object) instance, (Object) null))
        return false;
      PlayerData player = instance.Player;
      if (player == null)
        return false;
      this.mCurrentUnit = player.GetUnitData((long) GlobalVars.SelectedLSChangeUnitUniqueID);
      this.mCurrentUnit = UnitOverWriteUtility.Apply(this.mCurrentUnit, (eOverWritePartyType) GlobalVars.OverWritePartyType);
      if (this.mCurrentUnit == null || this.mCurrentUnit.MainConceptCard == null)
        return false;
      this.mUnitLeaderSkill = this.mCurrentUnit.LeaderSkill;
      this.mCCLeaderSkill = this.mCurrentUnit.MainConceptCard.LeaderSkill;
      if (this.mCCLeaderSkill == null)
        return false;
      DataSource.Bind<SkillData>(this.UnitLeaderSkillObject, this.mUnitLeaderSkill);
      DataSource.Bind<SkillData>(this.CCLeaderSkillObject, this.mCCLeaderSkill);
      this.mEquipConceptCardLeaderSkill = this.mCurrentUnit.IsEquipConceptLeaderSkill();
      this.UnitLeaderSkillButton.AddListener(new SRPG_Button.ButtonClickEvent(this.OnSwitchLeaderSkill));
      this.CCLeaderSkillButton.AddListener(new SRPG_Button.ButtonClickEvent(this.OnSwitchLeaderSkill));
      ((Component) this.UnitLeaderSkillButton).gameObject.SetActive(this.mEquipConceptCardLeaderSkill);
      ((Component) this.CCLeaderSkillButton).gameObject.SetActive(!this.mEquipConceptCardLeaderSkill);
      return true;
    }

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      if (!this.Init())
        DebugUtility.LogError("ユニットIDが不正、もしくは真理念装のデータが不正です。");
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 5);
    }

    private void OnButtonClickOK(SRPG_Button go)
    {
      if (!((Selectable) this.ButtonOK).IsInteractable())
        return;
      if (this.mCurrentUnit.IsEquipConceptLeaderSkill() != this.mEquipConceptCardLeaderSkill)
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
      else
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 20);
    }

    private void OnSwitchLeaderSkill(SRPG_Button go)
    {
      this.mEquipConceptCardLeaderSkill = !this.mEquipConceptCardLeaderSkill;
      ((Component) this.UnitLeaderSkillButton).gameObject.SetActive(this.mEquipConceptCardLeaderSkill);
      ((Component) this.CCLeaderSkillButton).gameObject.SetActive(!this.mEquipConceptCardLeaderSkill);
      ((Selectable) this.ButtonOK).interactable = this.mCurrentUnit.IsEquipConceptLeaderSkill() != this.mEquipConceptCardLeaderSkill;
    }
  }
}
