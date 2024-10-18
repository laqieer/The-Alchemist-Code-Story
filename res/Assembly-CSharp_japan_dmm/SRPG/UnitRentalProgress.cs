// Decompiled with JetBrains decompiler
// Type: SRPG.UnitRentalProgress
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(0, "Open", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Close", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "Refresh", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(10, "パネル表示", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "ヘルプ表示", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(21, "好感度MAXじゃない", FlowNode.PinTypes.Output, 21)]
  [FlowNode.Pin(31, "好感度MAX", FlowNode.PinTypes.Output, 31)]
  public class UnitRentalProgress : MonoBehaviour, IFlowInterface
  {
    public const int PIN_OPEN = 0;
    public const int PIN_CLOSE = 1;
    public const int PIN_REFRESH = 2;
    public const int PIN_SHOW_PANEL = 10;
    public const int PIN_SHOW_HELP = 11;
    public const int PIN_OUT_FAVOLIT_POINT_NOT_MAX = 21;
    public const int PIN_OUT_FAVOLIT_POINT_MAX = 31;
    public Button ButtonHelp;
    public Button ButtonDetail;
    public Button ButtonReward;
    private readonly string EVENT_UNIT_RENTALICON_SHOW = "UNIT_RENTALICON_SHOW";
    private readonly string EVENT_UNIT_RENTALICON_HIDE = "UNIT_RENTALICON_HIDE";

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 0:
          this.Refresh(false);
          break;
        case 1:
          if (!((Component) this).gameObject.activeSelf)
            break;
          ((Component) this).gameObject.SetActive(false);
          break;
        case 2:
          this.Refresh(true);
          break;
      }
    }

    private void OnEnable() => GlobalEvent.Invoke(this.EVENT_UNIT_RENTALICON_HIDE, (object) this);

    private void OnDisable() => GlobalEvent.Invoke(this.EVENT_UNIT_RENTALICON_SHOW, (object) this);

    private void Awake()
    {
      if (Object.op_Inequality((Object) this.ButtonDetail, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.ButtonDetail.onClick).AddListener(new UnityAction((object) this, __methodptr(OnClickDetail)));
      }
      if (Object.op_Inequality((Object) this.ButtonHelp, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.ButtonHelp.onClick).AddListener(new UnityAction((object) this, __methodptr(OnClickHelp)));
      }
      if (!Object.op_Inequality((Object) this.ButtonReward, (Object) null))
        return;
      // ISSUE: method pointer
      ((UnityEvent) this.ButtonReward.onClick).AddListener(new UnityAction((object) this, __methodptr(OnClickDetail)));
    }

    private void OnDestroy()
    {
      if (Object.op_Inequality((Object) this.ButtonReward, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.ButtonReward.onClick).RemoveListener(new UnityAction((object) this, __methodptr(OnClickDetail)));
      }
      if (Object.op_Inequality((Object) this.ButtonHelp, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.ButtonHelp.onClick).RemoveListener(new UnityAction((object) this, __methodptr(OnClickHelp)));
      }
      if (!Object.op_Inequality((Object) this.ButtonDetail, (Object) null))
        return;
      // ISSUE: method pointer
      ((UnityEvent) this.ButtonDetail.onClick).RemoveListener(new UnityAction((object) this, __methodptr(OnClickDetail)));
    }

    private void Update()
    {
    }

    private void Refresh(bool fromRefresh)
    {
      UnitData rentalUnit = MonoSingleton<GameManager>.Instance.Player.GetRentalUnit();
      UnitRentalParam activeUnitRentalParam = UnitRentalParam.GetActiveUnitRentalParam();
      if (rentalUnit == null || activeUnitRentalParam == null)
        return;
      DataSource.Bind<UnitData>(((Component) this).gameObject, rentalUnit);
      GameParameter.UpdateAll(((Component) this).gameObject);
      if (rentalUnit.RentalFavoritePoint < (int) activeUnitRentalParam.PtMax)
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 21);
      else
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 31);
    }

    private void OnClickDetail() => FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);

    private void OnClickHelp()
    {
      TrophyParam dataOfClass = DataSource.FindDataOfClass<TrophyParam>(((Component) this).gameObject, (TrophyParam) null);
      if (dataOfClass == null)
        return;
      if (dataOfClass.help == 0)
      {
        string str = LocalizedText.Get("sys.UNITRENTAL_HELP_" + dataOfClass.Objectives[0].type.ToString().ToUpper());
        FlowNode_Variable.Set(HelpWindow.VAR_NAME_MENU_ID, str);
      }
      else
        FlowNode_Variable.Set(HelpWindow.VAR_NAME_MENU_ID, dataOfClass.help.ToString());
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 11);
    }
  }
}
