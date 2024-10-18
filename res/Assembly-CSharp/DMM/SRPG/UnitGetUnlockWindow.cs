// Decompiled with JetBrains decompiler
// Type: SRPG.UnitGetUnlockWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "Piece Convert Check", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(100, "Unlock", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "Selected Quest", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "Confirmbox Ok", FlowNode.PinTypes.Output, 102)]
  public class UnitGetUnlockWindow : MonoBehaviour, IFlowInterface
  {
    private const int REFRESH = 1;
    private const int PIECE_CONVERT_CHECK = 2;
    private const int UNLOCK = 100;
    private const int SELECTED_QUEST = 101;
    private const int CONFIRMBOX_OK = 102;
    private UnitParam UnlockUnit;
    public Text UnitName;
    public GameObject DecideButton;
    private GameObject ConfirmBox;

    private void Start() => this.Refresh();

    public void Activated(int pinID)
    {
      if (pinID == 1)
        this.Refresh();
      if (pinID != 2)
        return;
      this.CheckPieceConvert();
    }

    private void Refresh()
    {
      if (GlobalVars.SelectUnitTicketDataValue != null && !string.IsNullOrEmpty(GlobalVars.SelectUnitTicketDataValue.SelectUnitId))
      {
        this.UnlockUnit = MonoSingleton<GameManager>.Instance.GetUnitParam(GlobalVars.SelectUnitTicketDataValue.SelectUnitId);
        DataSource.Bind<UnitParam>(((Component) this).gameObject, this.UnlockUnit);
        this.UnitName.text = LocalizedText.Get("sys.GET_UNIT_WINDOW_UNIT_NAME", (object) this.UnlockUnit.name);
        GameParameter.UpdateAll(((Component) this).gameObject);
      }
      else
      {
        if (!Object.op_Inequality((Object) this.DecideButton, (Object) null))
          return;
        this.DecideButton.SetActive(false);
      }
    }

    private void CheckPieceConvert()
    {
      string text = LocalizedText.Get("sys.GET_UNIT_WINDOW_PIECE");
      if (this.UnlockUnit != null && MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(this.UnlockUnit.piece) != null)
      {
        string name = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(this.UnlockUnit.piece).name;
        if (GlobalVars.SelectUnitTicketDataValue != null && GlobalVars.SelectUnitTicketDataValue.ConvertPieceNum > 0)
          text = string.Format(LocalizedText.Get("sys.GET_UNIT_WINDOW_PIECE_VALUE"), (object) name, (object) GlobalVars.SelectUnitTicketDataValue.ConvertPieceNum);
      }
      this.ConfirmBox = UIUtility.ConfirmBox(text, new UIUtility.DialogResultEvent(this.OnClickOK), new UIUtility.DialogResultEvent(this.OnClickCancel), systemModalPriority: 0);
    }

    private void OnClickOK(GameObject go)
    {
      if (Object.op_Equality((Object) this.ConfirmBox, (Object) null))
        return;
      this.ConfirmBox = (GameObject) null;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);
    }

    private void OnClickCancel(GameObject go)
    {
      if (Object.op_Equality((Object) this.ConfirmBox, (Object) null))
        return;
      this.ConfirmBox = (GameObject) null;
    }
  }
}
