// Decompiled with JetBrains decompiler
// Type: SRPG.ChallengeCountResetConfirmWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(0, "close", FlowNode.PinTypes.Output, 0)]
  public class ChallengeCountResetConfirmWindow : MonoBehaviour, IFlowInterface
  {
    [SerializeField]
    private Text ResetMessageText;
    [SerializeField]
    private GameObject ResetCostItem;
    [SerializeField]
    private Text ResetCostNum;
    [SerializeField]
    private Text ResetCostConsume;
    [SerializeField]
    private Text ResetCountCurrent;
    [SerializeField]
    private Text ResetCountLimit;
    [SerializeField]
    private Button DecideButton;
    [SerializeField]
    private Button CancelButton;
    public ChallengeCountResetConfirmWindow.ChallengeCountResetEvent DecideEvent;
    public ChallengeCountResetConfirmWindow.ChallengeCountResetEvent CancelEvent;

    private void Awake()
    {
      if (Object.op_Inequality((Object) this.DecideButton, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.DecideButton.onClick).AddListener(new UnityAction((object) this, __methodptr(OnClickDecide)));
      }
      if (!Object.op_Inequality((Object) this.CancelButton, (Object) null))
        return;
      // ISSUE: method pointer
      ((UnityEvent) this.CancelButton.onClick).AddListener(new UnityAction((object) this, __methodptr(OnClickCancel)));
    }

    private void Start()
    {
    }

    private void OnClickDecide()
    {
      if (this.DecideEvent == null)
        return;
      this.DecideEvent(((Component) this).gameObject);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 0);
    }

    private void OnClickCancel()
    {
      if (this.CancelEvent == null)
        return;
      this.CancelEvent(((Component) this).gameObject);
    }

    public bool Setup(
      ItemData item,
      int use_num,
      int reset_num,
      int reset_max,
      ChallengeCountResetConfirmWindow.ChallengeCountResetEvent okEvent,
      ChallengeCountResetConfirmWindow.ChallengeCountResetEvent cancelEvent)
    {
      if (Object.op_Inequality((Object) this.ResetMessageText, (Object) null))
        this.ResetMessageText.text = LocalizedText.Get("sys.QUEST_CHALLENGE_RESET", (object) item.Param.name, (object) use_num);
      if (Object.op_Inequality((Object) this.ResetCostItem, (Object) null))
      {
        DataSource.Bind<ItemData>(this.ResetCostItem, item);
        GameParameter.UpdateAll(this.ResetCostItem);
      }
      if (Object.op_Inequality((Object) this.ResetCostNum, (Object) null))
        this.ResetCostNum.text = item.Num.ToString();
      if (Object.op_Inequality((Object) this.ResetCostConsume, (Object) null))
        this.ResetCostConsume.text = (item.Num - use_num).ToString();
      if (Object.op_Inequality((Object) this.ResetCountCurrent, (Object) null))
        this.ResetCountCurrent.text = reset_num.ToString();
      if (Object.op_Inequality((Object) this.ResetCountLimit, (Object) null))
        this.ResetCountLimit.text = reset_max.ToString();
      this.DecideEvent = okEvent;
      this.CancelEvent = cancelEvent;
      return true;
    }

    public void Activated(int pinID)
    {
    }

    public delegate void ChallengeCountResetEvent(GameObject dialog);
  }
}
