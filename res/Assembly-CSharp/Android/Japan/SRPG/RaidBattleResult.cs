// Decompiled with JetBrains decompiler
// Type: SRPG.RaidBattleResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
  [FlowNode.Pin(104, "GetRaidBattleReward", FlowNode.PinTypes.Output, 104)]
  [FlowNode.Pin(105, "NotRaidBattleReward", FlowNode.PinTypes.Output, 105)]
  [FlowNode.Pin(106, "GetRewardEnd", FlowNode.PinTypes.Output, 106)]
  public class RaidBattleResult : MonoBehaviour, IFlowInterface
  {
    [SerializeField]
    private float NextNumberSecond = 1.5f;
    [SerializeField]
    private string CountActionObjectName = "count_font_add";
    [HeaderBar("▼報酬オブジェクト")]
    [SerializeField]
    private string RaidRewardIconPath = "UI/RaidRewardIcon";
    [SerializeField]
    private string ItemPath = "item";
    [SerializeField]
    private int ChangeGridItemNum = 10;
    [HeaderBar("▼数値表示演出時のSE")]
    [SerializeField]
    private string DecideNumberSE = string.Empty;
    [SerializeField]
    private string FinishDecideNumberSE = string.Empty;
    private List<RaidBattleResult.DamageNumber> mAttackDamageNumber = new List<RaidBattleResult.DamageNumber>();
    private List<RaidBattleResult.DamageNumber> mTotalDamageNumber = new List<RaidBattleResult.DamageNumber>();
    private List<GameObject> mItemRewardIcon = new List<GameObject>();
    private const int PIN_ATTACK_COUNT_ON = 1;
    private const int PIN_TOTAL_COUNT_ON = 2;
    private const int PIN_REWARD_ON = 3;
    private const int PIN_ATTACK_COUNT_SKIP = 4;
    private const int PIN_TOTAL_COUNT_SKIP = 6;
    private const int PIN_GET_REWARD_SKIP = 7;
    private const int PIN_COMMON_OUTPUT = 101;
    private const int PIN_ATTACK_COUNTUP_END = 102;
    private const int PIN_TOTAL_COUNTUP_END = 103;
    private const int PIN_GET_RAID_BATTLE_REWARD = 104;
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
    private Transform BattleRewardItemGrid;
    [SerializeField]
    private GameObject BattleRewardItemBase;
    [SerializeField]
    private Transform OverBattleRewardItemGrid;
    [SerializeField]
    private GameObject OverBattleRewardItemBase;
    private RaidBattleResult.eState mState;
    private ReqRaidBtlEnd.Response mRaidBtlEndParam;
    private int mBeforeRaidBossHp;
    private int mAfterRaidBossHp;
    private int mCountupDigitsNum;
    private float mCountupTime;
    private CustomSound mCustomSound;

    private void Start()
    {
      this.mRaidBtlEndParam = MonoSingleton<GameManager>.Instance.RaidBtlEndParam;
      if (this.mRaidBtlEndParam == null)
      {
        UnityEngine.Object.Destroy((UnityEngine.Object) this);
      }
      else
      {
        this.mBeforeRaidBossHp = GlobalVars.CurrentRaidBossInfo.HP;
        SceneBattle scene = SceneBattle.Instance;
        Unit unit = scene.Battle.Enemys.Find((Predicate<Unit>) (enemy => enemy.SettingNPC != null && (bool) enemy.SettingNPC.is_raid_boss));
        this.mAfterRaidBossHp = (int) unit.CurrentStatus.param.hp - (int) unit.OverKillDamage;
        int num1 = this.mBeforeRaidBossHp - this.mAfterRaidBossHp;
        if (num1 < 0)
        {
          DebugUtility.LogWarning("RaidBattleResult : attack_damage < 0 : " + (object) num1);
          num1 = 0;
        }
        string str1 = num1.ToString();
        for (int index = str1.Length - 1; index >= 0; --index)
        {
          GameObject _go = str1.Length - (index + 1) < 8 ? (str1.Length - (index + 1) < 6 ? UnityEngine.Object.Instantiate<GameObject>(this.AttackDamageFont01) : UnityEngine.Object.Instantiate<GameObject>(this.AttackDamageFont02)) : UnityEngine.Object.Instantiate<GameObject>(this.AttackDamageFont03);
          _go.transform.SetParent(this.AttackDamageGrid, false);
          _go.GetComponent<Text>().text = str1[index].ToString();
          Transform transform = _go.transform.Find(this.CountActionObjectName);
          if ((UnityEngine.Object) transform != (UnityEngine.Object) null)
            transform.GetComponent<Text>().text = str1[index].ToString();
          _go.SetActive(false);
          this.mAttackDamageNumber.Add(new RaidBattleResult.DamageNumber(_go, int.Parse(str1[index].ToString())));
        }
        this.AttackDamageFont01.SetActive(false);
        this.AttackDamageFont02.SetActive(false);
        this.AttackDamageFont03.SetActive(false);
        int num2 = this.mRaidBtlEndParam.total_damage;
        if (num2 < 0)
        {
          DebugUtility.LogWarning("RaidBattleResult : total_damage < 0 : " + (object) num2);
          num2 = 0;
        }
        string str2 = num2.ToString();
        for (int index = str2.Length - 1; index >= 0; --index)
        {
          GameObject _go = str2.Length - (index + 1) < 8 ? (str2.Length - (index + 1) < 6 ? UnityEngine.Object.Instantiate<GameObject>(this.TotalDamageFont01) : UnityEngine.Object.Instantiate<GameObject>(this.TotalDamageFont02)) : UnityEngine.Object.Instantiate<GameObject>(this.TotalDamageFont03);
          _go.transform.SetParent(this.TotalDamageGrid, false);
          _go.GetComponent<Text>().text = str2[index].ToString();
          Transform transform = _go.transform.Find(this.CountActionObjectName);
          if ((UnityEngine.Object) transform != (UnityEngine.Object) null)
            transform.GetComponent<Text>().text = str2[index].ToString();
          _go.SetActive(false);
          this.mTotalDamageNumber.Add(new RaidBattleResult.DamageNumber(_go, int.Parse(str2[index].ToString())));
        }
        this.TotalDamageFont01.SetActive(false);
        this.TotalDamageFont02.SetActive(false);
        this.TotalDamageFont03.SetActive(false);
        this.mAttackDamageNumber[this.mAttackDamageNumber.Count - 1].go.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
        this.mTotalDamageNumber[this.mTotalDamageNumber.Count - 1].go.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
        DataSource.Bind<Unit>(this.CharacterImage, scene.Battle.Units.Find((Predicate<Unit>) (u =>
        {
          if (u == scene.Battle.Leader && u.Side == EUnitSide.Player && u.IsEntry)
            return !u.IsSub;
          return false;
        })), false);
        GameParameter.UpdateAll(this.CharacterImage);
        if (this.mRaidBtlEndParam.damage_ratio_reward_ids != null && this.mRaidBtlEndParam.damage_ratio_reward_ids.Length > 0)
        {
          GameObject original = AssetManager.Load<GameObject>(this.RaidRewardIconPath);
          if ((UnityEngine.Object) original == (UnityEngine.Object) null)
            return;
          int num3 = 0;
          for (int index = 0; index < this.mRaidBtlEndParam.damage_ratio_reward_ids.Length; ++index)
          {
            string damageRatioRewardId = this.mRaidBtlEndParam.damage_ratio_reward_ids[index];
            if (!string.IsNullOrEmpty(damageRatioRewardId))
            {
              List<RaidReward> raidRewardList = MonoSingleton<GameManager>.Instance.MasterParam.GetRaidRewardList(damageRatioRewardId);
              if (raidRewardList != null && raidRewardList.Count != 0)
                num3 += raidRewardList.Count;
            }
          }
          Transform battleRewardItemGrid = this.BattleRewardItemGrid;
          GameObject battleRewardItemBase = this.BattleRewardItemBase;
          if (num3 > this.ChangeGridItemNum)
          {
            battleRewardItemGrid = this.OverBattleRewardItemGrid;
            battleRewardItemBase = this.OverBattleRewardItemBase;
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
                  GameObject gameObject1 = UnityEngine.Object.Instantiate<GameObject>(battleRewardItemBase);
                  gameObject1.transform.SetParent(battleRewardItemGrid, false);
                  GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(original);
                  Transform parent = gameObject1.transform.Find(this.ItemPath);
                  gameObject2.transform.SetParent(parent, false);
                  gameObject2.SetActive(true);
                  gameObject2.GetComponent<RaidRewardIcon>().Initialize(raidRewardList[index2]);
                  this.mItemRewardIcon.Add(gameObject2);
                }
              }
            }
          }
          this.BattleRewardItemBase.SetActive(false);
          this.OverBattleRewardItemBase.SetActive(false);
        }
        this.mCustomSound = this.gameObject.GetComponent<CustomSound>();
      }
    }

    private void Update()
    {
      switch (this.mState)
      {
        case RaidBattleResult.eState.AttackCountup:
          if (this.Countup(this.mAttackDamageNumber))
            break;
          if ((UnityEngine.Object) this.mCustomSound != (UnityEngine.Object) null)
            this.mCustomSound.Stop();
          this.mState = RaidBattleResult.eState.None;
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);
          break;
        case RaidBattleResult.eState.TotalCountup:
          if (this.Countup(this.mTotalDamageNumber))
            break;
          if ((UnityEngine.Object) this.mCustomSound != (UnityEngine.Object) null)
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
            MonoSingleton<MySound>.Instance.PlaySEOneShot(this.FinishDecideNumberSE, 0.0f);
          for (int index = 0; index < list.Count; ++index)
          {
            Transform transform = list[index].go.transform.Find(this.CountActionObjectName);
            if ((UnityEngine.Object) transform != (UnityEngine.Object) null)
              transform.gameObject.SetActive(true);
          }
        }
        else if (!string.IsNullOrEmpty(this.DecideNumberSE))
          MonoSingleton<MySound>.Instance.PlaySEOneShot(this.DecideNumberSE, 0.0f);
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

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          if ((UnityEngine.Object) this.mCustomSound != (UnityEngine.Object) null)
            this.mCustomSound.Play();
          this.mState = RaidBattleResult.eState.AttackCountup;
          this.mCountupDigitsNum = 0;
          break;
        case 2:
          if ((UnityEngine.Object) this.mCustomSound != (UnityEngine.Object) null)
            this.mCustomSound.Play();
          this.mState = RaidBattleResult.eState.TotalCountup;
          this.mCountupDigitsNum = 0;
          break;
        case 3:
          if (this.mItemRewardIcon.Count > 0)
          {
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 104);
            break;
          }
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 105);
          break;
        case 4:
          if ((UnityEngine.Object) this.mCustomSound != (UnityEngine.Object) null)
            this.mCustomSound.Stop();
          this.CountAllOn(this.mAttackDamageNumber);
          this.mState = RaidBattleResult.eState.None;
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);
          break;
        case 6:
          if ((UnityEngine.Object) this.mCustomSound != (UnityEngine.Object) null)
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
