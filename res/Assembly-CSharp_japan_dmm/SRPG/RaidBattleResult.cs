// Decompiled with JetBrains decompiler
// Type: SRPG.RaidBattleResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "AttackCountON", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "TotalCountON", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(3, "RewardObjectON", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(4, "AttackCountSkip", FlowNode.PinTypes.Input, 4)]
  [FlowNode.Pin(6, "TotalCountSkip", FlowNode.PinTypes.Input, 6)]
  [FlowNode.Pin(7, "GetRewardSkip", FlowNode.PinTypes.Input, 7)]
  [FlowNode.Pin(101, "Common Output", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "AttackCountupEnd", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(103, "TotalCountupEnd", FlowNode.PinTypes.Output, 103)]
  [FlowNode.Pin(104, "GetRaidBattleRatioReward", FlowNode.PinTypes.Output, 104)]
  [FlowNode.Pin(105, "NotRaidBattleReward", FlowNode.PinTypes.Output, 105)]
  [FlowNode.Pin(107, "GetRaidBattleAmountReward", FlowNode.PinTypes.Output, 107)]
  [FlowNode.Pin(106, "GetRewardEnd", FlowNode.PinTypes.Output, 106)]
  public class RaidBattleResult : MonoBehaviour, IFlowInterface
  {
    private const int PIN_ATTACK_COUNT_ON = 1;
    private const int PIN_TOTAL_COUNT_ON = 2;
    private const int PIN_REWARD_ON = 3;
    private const int PIN_ATTACK_COUNT_SKIP = 4;
    private const int PIN_TOTAL_COUNT_SKIP = 6;
    private const int PIN_GET_REWARD_SKIP = 7;
    private const int PIN_COMMON_OUTPUT = 101;
    private const int PIN_ATTACK_COUNTUP_END = 102;
    private const int PIN_TOTAL_COUNTUP_END = 103;
    private const int PIN_GET_RAID_BATTLE_RATIOREWARD = 104;
    private const int PIN_GET_RAID_BATTLE_AMOUNTREWARD = 107;
    private const int PIN_NOT_RAID_BATTLE_REWARD = 105;
    private const int PIN_GET_REWARD_END = 106;
    private const int CHANGE_COLOR_DIGITS_NUM_01 = 6;
    private const int CHANGE_COLOR_DIGITS_NUM_02 = 8;
    [HeaderBar("▼表示物オブジェクト")]
    [SerializeField]
    private GameObject AttackObject;
    [SerializeField]
    private GameObject TotalObject;
    [SerializeField]
    private GameObject RewardObject;
    [HeaderBar("▼キャラクタイメージ")]
    [SerializeField]
    private GameObject CharacterImage;
    [HeaderBar("▼ダメージ表記")]
    [SerializeField]
    private Transform AttackDamageGrid;
    [SerializeField]
    private GameObject AttackDamageFont01;
    [SerializeField]
    private GameObject AttackDamageFont02;
    [SerializeField]
    private GameObject AttackDamageFont03;
    [SerializeField]
    private Transform TotalDamageGrid;
    [SerializeField]
    private GameObject TotalDamageFont01;
    [SerializeField]
    private GameObject TotalDamageFont02;
    [SerializeField]
    private GameObject TotalDamageFont03;
    [SerializeField]
    private float NextNumberSecond = 1.5f;
    [SerializeField]
    private string CountActionObjectName = "count_font_add";
    [HeaderBar("▼割合報酬オブジェクト")]
    [SerializeField]
    private string RaidRewardIconPath = "UI/RaidRewardIcon";
    [SerializeField]
    private string ItemPath = "item";
    [SerializeField]
    private int ChangeGridItemNum = 10;
    [SerializeField]
    private Transform BattleRatioRewardItemGrid;
    [SerializeField]
    private GameObject BattleRatioRewardItemBase;
    [SerializeField]
    private Transform OverBattleRatioRewardItemGrid;
    [SerializeField]
    private GameObject OverBattleRatioRewardItemBase;
    [HeaderBar("▼到達報酬オブジェクト")]
    [SerializeField]
    private Transform BattleAmountRewardItemGrid;
    [SerializeField]
    private GameObject BattleAmountRewardItemBase;
    [SerializeField]
    private Transform OverBattleAmountRewardItemGrid;
    [SerializeField]
    private GameObject OverBattleAmountRewardItemBase;
    [HeaderBar("▼数値表示演出時のSE")]
    [SerializeField]
    private string DecideNumberSE = string.Empty;
    [SerializeField]
    private string FinishDecideNumberSE = string.Empty;
    private RaidBattleResult.eState mState;
    private ReqRaidBtlEnd.Response mRaidBtlEndParam;
    private ReqGuildRaidBtlEnd.Response mGuildRaidBtlEndParam;
    private int mBeforeRaidBossHp;
    private int mAfterRaidBossHp;
    private int mCountupDigitsNum;
    private float mCountupTime;
    private CustomSound mCustomSound;
    private RaidBattleResult.eRaidRewardType mReawardStatus;
    private List<RaidBattleResult.DamageNumber> mAttackDamageNumber = new List<RaidBattleResult.DamageNumber>();
    private List<RaidBattleResult.DamageNumber> mTotalDamageNumber = new List<RaidBattleResult.DamageNumber>();
    private List<GameObject> mItemRewardIcon = new List<GameObject>();

    private void Start()
    {
      SceneBattle scene = SceneBattle.Instance;
      int num1;
      if (scene.Battle.IsRaidQuest)
      {
        this.mRaidBtlEndParam = MonoSingleton<GameManager>.Instance.RaidBtlEndParam;
        if (this.mRaidBtlEndParam == null)
        {
          UnityEngine.Object.Destroy((UnityEngine.Object) this);
          return;
        }
        num1 = this.mRaidBtlEndParam.total_damage;
      }
      else
      {
        this.mGuildRaidBtlEndParam = MonoSingleton<GameManager>.Instance.RecentGuildRaidBtlEndParam;
        if (this.mGuildRaidBtlEndParam == null)
        {
          UnityEngine.Object.Destroy((UnityEngine.Object) this);
          return;
        }
        num1 = MonoSingleton<GameManager>.Instance.RecentGuildRaidBtlEndParam.total_damage;
      }
      this.mBeforeRaidBossHp = (int) GlobalVars.CurrentRaidBossHP;
      Unit unit = scene.Battle.Enemys.Find((Predicate<Unit>) (enemy => enemy.SettingNPC != null && (bool) enemy.SettingNPC.is_raid_boss));
      this.mAfterRaidBossHp = (int) unit.CurrentStatus.param.hp - unit.OverKillDamage;
      int num2 = this.mBeforeRaidBossHp - this.mAfterRaidBossHp;
      if (num2 < 0)
      {
        DebugUtility.LogWarning("RaidBattleResult : attack_damage < 0 : " + (object) num2);
        num2 = 0;
      }
      string str1 = num2.ToString();
      for (int index = str1.Length - 1; index >= 0; --index)
      {
        GameObject _go = str1.Length - (index + 1) < 8 ? (str1.Length - (index + 1) < 6 ? UnityEngine.Object.Instantiate<GameObject>(this.AttackDamageFont01) : UnityEngine.Object.Instantiate<GameObject>(this.AttackDamageFont02)) : UnityEngine.Object.Instantiate<GameObject>(this.AttackDamageFont03);
        _go.transform.SetParent(this.AttackDamageGrid, false);
        _go.GetComponent<Text>().text = str1[index].ToString();
        Transform transform = _go.transform.Find(this.CountActionObjectName);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) transform, (UnityEngine.Object) null))
          ((Component) transform).GetComponent<Text>().text = str1[index].ToString();
        _go.SetActive(false);
        this.mAttackDamageNumber.Add(new RaidBattleResult.DamageNumber(_go, int.Parse(str1[index].ToString())));
      }
      this.AttackDamageFont01.SetActive(false);
      this.AttackDamageFont02.SetActive(false);
      this.AttackDamageFont03.SetActive(false);
      if (num1 < 0)
      {
        DebugUtility.LogWarning("RaidBattleResult : total_damage < 0 : " + (object) num1);
        num1 = 0;
      }
      string str2 = num1.ToString();
      for (int index = str2.Length - 1; index >= 0; --index)
      {
        GameObject _go = str2.Length - (index + 1) < 8 ? (str2.Length - (index + 1) < 6 ? UnityEngine.Object.Instantiate<GameObject>(this.TotalDamageFont01) : UnityEngine.Object.Instantiate<GameObject>(this.TotalDamageFont02)) : UnityEngine.Object.Instantiate<GameObject>(this.TotalDamageFont03);
        _go.transform.SetParent(this.TotalDamageGrid, false);
        _go.GetComponent<Text>().text = str2[index].ToString();
        Transform transform = _go.transform.Find(this.CountActionObjectName);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) transform, (UnityEngine.Object) null))
          ((Component) transform).GetComponent<Text>().text = str2[index].ToString();
        _go.SetActive(false);
        this.mTotalDamageNumber.Add(new RaidBattleResult.DamageNumber(_go, int.Parse(str2[index].ToString())));
      }
      this.TotalDamageFont01.SetActive(false);
      this.TotalDamageFont02.SetActive(false);
      this.TotalDamageFont03.SetActive(false);
      ((Transform) this.mAttackDamageNumber[this.mAttackDamageNumber.Count - 1].go.GetComponent<RectTransform>()).localScale = new Vector3(1f, 1f, 1f);
      ((Transform) this.mTotalDamageNumber[this.mTotalDamageNumber.Count - 1].go.GetComponent<RectTransform>()).localScale = new Vector3(1f, 1f, 1f);
      DataSource.Bind<Unit>(this.CharacterImage, scene.Battle.Units.Find((Predicate<Unit>) (u => u == scene.Battle.Leader && u.Side == EUnitSide.Player && u.IsEntry && !u.IsSub)));
      GameParameter.UpdateAll(this.CharacterImage);
      this.mCustomSound = ((Component) this).gameObject.GetComponent<CustomSound>();
    }

    private void Update()
    {
      switch (this.mState)
      {
        case RaidBattleResult.eState.AttackCountup:
          if (this.Countup(this.mAttackDamageNumber))
            break;
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mCustomSound, (UnityEngine.Object) null))
            this.mCustomSound.Stop();
          this.mState = RaidBattleResult.eState.None;
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);
          break;
        case RaidBattleResult.eState.TotalCountup:
          if (this.Countup(this.mTotalDamageNumber))
            break;
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mCustomSound, (UnityEngine.Object) null))
            this.mCustomSound.Stop();
          this.mState = RaidBattleResult.eState.None;
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 103);
          break;
      }
    }

    private bool Countup(List<RaidBattleResult.DamageNumber> list)
    {
      if (list == null || list.Count == 0 || list.Count <= this.mCountupDigitsNum)
        return false;
      if (!list[this.mCountupDigitsNum].go.GetActive())
        list[this.mCountupDigitsNum].go.SetActive(true);
      Text component = list[this.mCountupDigitsNum].go.GetComponent<Text>();
      this.mCountupTime += Time.deltaTime;
      if ((double) this.mCountupTime >= (double) this.NextNumberSecond)
      {
        component.text = list[this.mCountupDigitsNum].num.ToString();
        ++this.mCountupDigitsNum;
        this.mCountupTime = 0.0f;
        if (list.Count == this.mCountupDigitsNum)
        {
          if (!string.IsNullOrEmpty(this.FinishDecideNumberSE))
            MonoSingleton<MySound>.Instance.PlaySEOneShot(this.FinishDecideNumberSE);
          for (int index = 0; index < list.Count; ++index)
          {
            Transform transform = list[index].go.transform.Find(this.CountActionObjectName);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) transform, (UnityEngine.Object) null))
              ((Component) transform).gameObject.SetActive(true);
          }
        }
        else if (!string.IsNullOrEmpty(this.DecideNumberSE))
          MonoSingleton<MySound>.Instance.PlaySEOneShot(this.DecideNumberSE);
      }
      else
        component.text = Random.Range(0, 9).ToString();
      return list.Count > this.mCountupDigitsNum;
    }

    private void CountAllOn(List<RaidBattleResult.DamageNumber> list)
    {
      for (int index = 0; index < list.Count; ++index)
      {
        list[index].go.SetActive(true);
        list[index].go.GetComponent<Text>().text = list[index].num.ToString();
      }
    }

    private void GetDamageRewardItems(RaidBattleResult.eRaidRewardType type)
    {
      this.mReawardStatus = type;
      this.mItemRewardIcon.Clear();
      if (type == RaidBattleResult.eRaidRewardType.Ratio && this.mRaidBtlEndParam.damage_ratio_reward_ids != null && this.mRaidBtlEndParam.damage_ratio_reward_ids.Length > 0)
      {
        GameObject gameObject1 = AssetManager.Load<GameObject>(this.RaidRewardIconPath);
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject1, (UnityEngine.Object) null))
          return;
        int num = 0;
        for (int index = 0; index < this.mRaidBtlEndParam.damage_ratio_reward_ids.Length; ++index)
        {
          string damageRatioRewardId = this.mRaidBtlEndParam.damage_ratio_reward_ids[index];
          if (!string.IsNullOrEmpty(damageRatioRewardId))
          {
            List<RaidReward> raidRewardList = MonoSingleton<GameManager>.Instance.MasterParam.GetRaidRewardList(damageRatioRewardId);
            if (raidRewardList != null && raidRewardList.Count != 0)
              num += raidRewardList.Count;
          }
        }
        Transform ratioRewardItemGrid = this.BattleRatioRewardItemGrid;
        GameObject ratioRewardItemBase = this.BattleRatioRewardItemBase;
        if (num > this.ChangeGridItemNum)
        {
          ratioRewardItemGrid = this.OverBattleRatioRewardItemGrid;
          ratioRewardItemBase = this.OverBattleRatioRewardItemBase;
        }
        for (int index1 = 0; index1 < this.mRaidBtlEndParam.damage_ratio_reward_ids.Length; ++index1)
        {
          string damageRatioRewardId = this.mRaidBtlEndParam.damage_ratio_reward_ids[index1];
          if (!string.IsNullOrEmpty(damageRatioRewardId))
          {
            List<RaidReward> raidRewardList = MonoSingleton<GameManager>.Instance.MasterParam.GetRaidRewardList(damageRatioRewardId);
            if (raidRewardList != null && raidRewardList.Count != 0)
            {
              for (int index2 = 0; index2 < raidRewardList.Count; ++index2)
              {
                GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(ratioRewardItemBase);
                gameObject2.transform.SetParent(ratioRewardItemGrid, false);
                GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(gameObject1);
                Transform transform = gameObject2.transform.Find(this.ItemPath);
                gameObject3.transform.SetParent(transform, false);
                gameObject3.SetActive(true);
                gameObject3.GetComponent<RaidRewardIcon>().Initialize(raidRewardList[index2]);
                this.mItemRewardIcon.Add(gameObject3);
              }
            }
          }
        }
        this.BattleRatioRewardItemBase.SetActive(false);
        this.OverBattleRatioRewardItemBase.SetActive(false);
      }
      if (type != RaidBattleResult.eRaidRewardType.Amount || this.mRaidBtlEndParam.damage_amount_reward_ids == null || this.mRaidBtlEndParam.damage_amount_reward_ids.Length <= 0)
        return;
      GameObject gameObject4 = AssetManager.Load<GameObject>(this.RaidRewardIconPath);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject4, (UnityEngine.Object) null))
        return;
      int num1 = 0;
      for (int index = 0; index < this.mRaidBtlEndParam.damage_amount_reward_ids.Length; ++index)
      {
        string damageAmountRewardId = this.mRaidBtlEndParam.damage_amount_reward_ids[index];
        if (!string.IsNullOrEmpty(damageAmountRewardId))
        {
          List<RaidReward> raidRewardList = MonoSingleton<GameManager>.Instance.MasterParam.GetRaidRewardList(damageAmountRewardId);
          if (raidRewardList != null && raidRewardList.Count != 0)
            num1 += raidRewardList.Count;
        }
      }
      Transform amountRewardItemGrid = this.BattleAmountRewardItemGrid;
      GameObject amountRewardItemBase = this.BattleAmountRewardItemBase;
      if (num1 > this.ChangeGridItemNum)
      {
        amountRewardItemGrid = this.OverBattleAmountRewardItemGrid;
        amountRewardItemBase = this.OverBattleAmountRewardItemBase;
      }
      for (int index3 = 0; index3 < this.mRaidBtlEndParam.damage_amount_reward_ids.Length; ++index3)
      {
        string damageAmountRewardId = this.mRaidBtlEndParam.damage_amount_reward_ids[index3];
        if (!string.IsNullOrEmpty(damageAmountRewardId))
        {
          List<RaidReward> raidRewardList = MonoSingleton<GameManager>.Instance.MasterParam.GetRaidRewardList(damageAmountRewardId);
          if (raidRewardList != null && raidRewardList.Count != 0)
          {
            for (int index4 = 0; index4 < raidRewardList.Count; ++index4)
            {
              GameObject gameObject5 = UnityEngine.Object.Instantiate<GameObject>(amountRewardItemBase);
              gameObject5.transform.SetParent(amountRewardItemGrid, false);
              GameObject gameObject6 = UnityEngine.Object.Instantiate<GameObject>(gameObject4);
              Transform transform = gameObject5.transform.Find(this.ItemPath);
              gameObject6.transform.SetParent(transform, false);
              gameObject6.SetActive(true);
              gameObject6.GetComponent<RaidRewardIcon>().Initialize(raidRewardList[index4]);
              this.mItemRewardIcon.Add(gameObject6);
            }
          }
        }
      }
      this.BattleAmountRewardItemBase.SetActive(false);
      this.OverBattleAmountRewardItemBase.SetActive(false);
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mCustomSound, (UnityEngine.Object) null))
            this.mCustomSound.Play();
          this.mState = RaidBattleResult.eState.AttackCountup;
          this.mCountupDigitsNum = 0;
          break;
        case 2:
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mCustomSound, (UnityEngine.Object) null))
            this.mCustomSound.Play();
          this.mState = RaidBattleResult.eState.TotalCountup;
          this.mCountupDigitsNum = 0;
          break;
        case 3:
          if (SceneBattle.Instance.Battle.IsRaidQuest)
          {
            if (this.mReawardStatus == RaidBattleResult.eRaidRewardType.None)
              this.GetDamageRewardItems(RaidBattleResult.eRaidRewardType.Ratio);
            else if (this.mReawardStatus == RaidBattleResult.eRaidRewardType.Ratio)
            {
              this.GetDamageRewardItems(RaidBattleResult.eRaidRewardType.Amount);
            }
            else
            {
              FlowNode_GameObject.ActivateOutputLinks((Component) this, 105);
              break;
            }
            if (this.mItemRewardIcon.Count > 0)
            {
              if (this.mReawardStatus == RaidBattleResult.eRaidRewardType.Ratio)
              {
                FlowNode_GameObject.ActivateOutputLinks((Component) this, 104);
                break;
              }
              FlowNode_GameObject.ActivateOutputLinks((Component) this, 107);
              break;
            }
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 106);
            break;
          }
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 105);
          break;
        case 4:
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mCustomSound, (UnityEngine.Object) null))
            this.mCustomSound.Stop();
          this.CountAllOn(this.mAttackDamageNumber);
          this.mState = RaidBattleResult.eState.None;
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);
          break;
        case 6:
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mCustomSound, (UnityEngine.Object) null))
            this.mCustomSound.Stop();
          this.CountAllOn(this.mTotalDamageNumber);
          this.mState = RaidBattleResult.eState.None;
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 103);
          break;
        case 7:
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 106);
          break;
      }
    }

    private enum eState
    {
      None,
      AttackCountup,
      TotalCountup,
    }

    private enum eRaidRewardType
    {
      None,
      Ratio,
      Amount,
    }

    private struct DamageNumber
    {
      public GameObject go;
      public int num;

      public DamageNumber(GameObject _go, int _num)
      {
        this.go = _go;
        this.num = _num;
      }
    }
  }
}
