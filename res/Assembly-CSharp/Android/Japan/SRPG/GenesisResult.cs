// Decompiled with JetBrains decompiler
// Type: SRPG.GenesisResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "開始", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "終了", FlowNode.PinTypes.Output, 101)]
  public class GenesisResult : MonoBehaviour, IFlowInterface
  {
    [SerializeField]
    private GameObject Window;
    [Space(5f)]
    [SerializeField]
    private Text TextRewardName;
    [SerializeField]
    private Text TextRewardNum;
    [SerializeField]
    private GameObject TextRewardConn;
    [SerializeField]
    private GenesisRewardIcon RewardIconTemplate;
    [SerializeField]
    private Transform TrRewardIconParent;
    [Space(5f)]
    [SerializeField]
    private SRPG_Button NextBtn;
    private const int PIN_IN_START = 1;
    private const int PIN_OUT_EXIT = 101;

    private void Awake()
    {
      if (!(bool) ((UnityEngine.Object) this.Window))
        return;
      this.Window.SetActive(false);
    }

    private void EffectStart()
    {
      GiftData reward = (GiftData) null;
      BattleCore battleCore = (BattleCore) null;
      if ((bool) ((UnityEngine.Object) SceneBattle.Instance))
        battleCore = SceneBattle.Instance.Battle;
      if (battleCore != null)
      {
        BattleCore.Record questRecord = battleCore.GetQuestRecord();
        if (questRecord != null && questRecord.mGenesisBossResultReward != null)
        {
          reward = new GiftData();
          reward.Deserialize(questRecord.mGenesisBossResultReward);
          string name;
          int amount;
          reward.GetRewardNameAndAmount(out name, out amount);
          if ((bool) ((UnityEngine.Object) this.TextRewardName))
            this.TextRewardName.text = name;
          if ((bool) ((UnityEngine.Object) this.TextRewardNum))
            this.TextRewardNum.text = amount.ToString();
          if ((bool) ((UnityEngine.Object) this.TextRewardConn))
            this.TextRewardConn.SetActive(!reward.CheckGiftTypeIncluded(GiftTypes.Gold) && !reward.CheckGiftTypeIncluded(GiftTypes.Coin));
          if ((bool) ((UnityEngine.Object) this.RewardIconTemplate))
            UnityEngine.Object.Instantiate<GenesisRewardIcon>(this.RewardIconTemplate, this.TrRewardIconParent).Initialize(reward);
        }
      }
      if (reward == null)
      {
        this.OnNext();
      }
      else
      {
        this.gameObject.SetActive(true);
        if ((bool) ((UnityEngine.Object) this.NextBtn))
        {
          this.NextBtn.onClick.RemoveListener(new UnityAction(this.OnNext));
          this.NextBtn.onClick.AddListener(new UnityAction(this.OnNext));
        }
        if (!(bool) ((UnityEngine.Object) this.Window))
          return;
        this.Window.SetActive(true);
        GameParameter.UpdateAll(this.Window);
      }
    }

    private void OnNext()
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
    }

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.EffectStart();
    }
  }
}
