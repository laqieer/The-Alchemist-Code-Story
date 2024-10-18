// Decompiled with JetBrains decompiler
// Type: SRPG.WorldRaidBattleResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "Start Attack Damage", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "Skip Attack Damage", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(101, "Finish Attack Damage", FlowNode.PinTypes.Output, 3)]
  [FlowNode.Pin(11, "Start Total Damage", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(12, "Skip Total Damage", FlowNode.PinTypes.Input, 12)]
  [FlowNode.Pin(102, "Finish Total Damage", FlowNode.PinTypes.Output, 13)]
  [FlowNode.Pin(21, "Start Reward", FlowNode.PinTypes.Input, 21)]
  [FlowNode.Pin(22, "Skip Reward", FlowNode.PinTypes.Input, 22)]
  [FlowNode.Pin(103, "Finish Reward", FlowNode.PinTypes.Output, 23)]
  [FlowNode.Pin(41, "Finish All Effect", FlowNode.PinTypes.Input, 41)]
  [FlowNode.Pin(105, "Finish All Effect", FlowNode.PinTypes.Output, 42)]
  public class WorldRaidBattleResult : MonoBehaviour, IFlowInterface
  {
    private const int PIN_IN_START_ATTACK_DAMAGE = 1;
    private const int PIN_IN_SKIP_ATTACK_DAMAGE = 2;
    private const int PIN_IN_START_TOTAL_DAMAGE = 11;
    private const int PIN_IN_SKIP_TOTAL_DAMAGE = 12;
    private const int PIN_IN_START_REWARD = 21;
    private const int PIN_IN_SKIP_REWARD = 22;
    private const int PIN_IN_FINISH_ALL_EFFECT = 41;
    private const int PIN_OUT_FINISH_ATTACK_DAMAGE = 101;
    private const int PIN_OUT_FINISH_TOTAL_DAMAGE = 102;
    private const int PIN_OUT_FINISH_REWARD = 103;
    private const int PIN_OUT_FINISH_ALL_EFFECT = 105;
    private const string ITEM_INAME_REWARD_EMPTY = "NONE";
    private const string REWARD_ICON_PATH = "UI/RaidRewardIcon";
    private const string REWARD_ICON_PARENT_NAME = "item";
    private const string REWARD_ICON_LASTATTACK_BADGE = "lastattack";
    [HeaderBar("▼キャラクタイメージ")]
    [SerializeField]
    private GameObject CharacterImage;
    [HeaderBar("▼ダメージ表示")]
    [SerializeField]
    private GameObject AttackDamageParent;
    [SerializeField]
    private Text AttackDamageText01;
    [SerializeField]
    private Text AttackDamageText02;
    [SerializeField]
    private Text AttackDamageText03;
    [HeaderBar("▼トータルダメージ表示")]
    [SerializeField]
    private GameObject TotalDamageParent;
    [SerializeField]
    private Text TotalDamageText01;
    [SerializeField]
    private Text TotalDamageText02;
    [SerializeField]
    private Text TotalDamageText03;
    [HeaderBar("▼報酬")]
    [SerializeField]
    private GameObject RewardParent;
    [SerializeField]
    private GameObject RewardIconBase;
    private SRPG.StateMachine<WorldRaidBattleResult> StateMachine;
    private GameObject RewardIconPrefab;

    private void Start()
    {
      this.StateMachine = new SRPG.StateMachine<WorldRaidBattleResult>(this);
      this.StateMachine.GotoState<WorldRaidBattleResult.State_Idle>();
      Unit data = (Unit) null;
      SceneBattle sceneBattle = SceneBattle.Instance;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) sceneBattle, (UnityEngine.Object) null))
        data = sceneBattle.Battle.Units.Find((Predicate<Unit>) (u => u == sceneBattle.Battle.Leader && u.Side == EUnitSide.Player && u.IsEntry && !u.IsSub));
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CharacterImage, (UnityEngine.Object) null))
      {
        DataSource.Bind<Unit>(this.CharacterImage, data);
        GameParameter.UpdateAll(this.CharacterImage);
      }
      this.RewardIconPrefab = AssetManager.Load<GameObject>("UI/RaidRewardIcon");
    }

    private void Update()
    {
      if (this.StateMachine == null)
        return;
      this.StateMachine.Update();
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          this.StateMachine.GotoState<WorldRaidBattleResult.State_AttackDamage>();
          break;
        case 2:
          this.StateMachine.Command("skip");
          break;
        case 11:
          this.StateMachine.GotoState<WorldRaidBattleResult.State_TotalDamage>();
          break;
        case 12:
          this.StateMachine.Command("skip");
          break;
        case 21:
          this.StateMachine.GotoState<WorldRaidBattleResult.State_DisplayReward>();
          break;
        case 22:
          this.StateMachine.Command("skip");
          break;
        case 41:
          this.StateMachine.GotoState<WorldRaidBattleResult.State_Finish>();
          break;
      }
    }

    private class State_Idle : State<WorldRaidBattleResult>
    {
    }

    private abstract class State_DisplayDamage : State<WorldRaidBattleResult>
    {
      protected const float NEXT_NUMBER_SECOND = 0.5f;
      protected const string DECIDE_NUMBER_SE = "SE_1075";
      protected const string FINISH_DECIDE_NUMBER_SE = "SE_1076";
      protected const string EFFECT_TEXT_NAME = "count_font_add";
      protected const float LAST_DIGIT_SCALE = 1f;
      private const int CHANGE_COLOR_DIGITS_NUM_01 = 6;
      private const int CHANGE_COLOR_DIGITS_NUM_02 = 9;
      private List<WorldRaidBattleResult.State_DisplayDamage.DamageText> DamageTextList = new List<WorldRaidBattleResult.State_DisplayDamage.DamageText>();
      private int CountupDigitsNum;
      private float CountupTime;

      protected void Init(
        GameObject parent,
        GameObject hideTarget,
        Text text1,
        Text text2,
        Text text3,
        long damage)
      {
        GameUtility.SetGameObjectActive(hideTarget, false);
        GameUtility.SetGameObjectActive((Component) text1, false);
        GameUtility.SetGameObjectActive((Component) text2, false);
        GameUtility.SetGameObjectActive((Component) text3, false);
        long num1 = damage;
        int num2 = 0;
        Text text4;
        while (true)
        {
          int num3 = (int) (num1 % 10L);
          Text text5 = num2 >= 6 ? (num2 >= 9 ? text3 : text2) : text1;
          text4 = UnityEngine.Object.Instantiate<Text>(text5, ((Component) text5).transform.parent);
          this.DamageTextList.Add(new WorldRaidBattleResult.State_DisplayDamage.DamageText(text4, num3));
          ((Component) ((Component) text4).transform.Find("count_font_add")).GetComponent<Text>().text = num3.ToString();
          num1 /= 10L;
          if (num1 > 0L)
            ++num2;
          else
            break;
        }
        ((Transform) ((Component) text4).GetComponent<RectTransform>()).localScale = new Vector3(1f, 1f, 1f);
        GameUtility.SetGameObjectActive(parent, true);
      }

      public override void Update(WorldRaidBattleResult self)
      {
        if (this.Next())
          return;
        self.StateMachine.GotoState<WorldRaidBattleResult.State_Idle>();
      }

      public override void Command(WorldRaidBattleResult self, string cmd)
      {
        if (!(cmd == "skip"))
          return;
        this.Skip();
        self.StateMachine.GotoState<WorldRaidBattleResult.State_Idle>();
      }

      protected bool Next()
      {
        if (this.DamageTextList.Count == 0 || this.DamageTextList.Count <= this.CountupDigitsNum)
          return false;
        WorldRaidBattleResult.State_DisplayDamage.DamageText damageText = this.DamageTextList[this.CountupDigitsNum];
        GameUtility.SetGameObjectActive((Component) damageText.text, true);
        Text text = damageText.text;
        this.CountupTime += Time.deltaTime;
        if ((double) this.CountupTime < 0.5)
        {
          text.text = Random.Range(0, 9).ToString();
          return true;
        }
        damageText.text.text = damageText.num.ToString();
        ++this.CountupDigitsNum;
        this.CountupTime = 0.0f;
        if (this.CountupDigitsNum < this.DamageTextList.Count)
        {
          MonoSingleton<MySound>.Instance.PlaySEOneShot("SE_1075");
          return true;
        }
        MonoSingleton<MySound>.Instance.PlaySEOneShot("SE_1076");
        for (int index = 0; index < this.DamageTextList.Count; ++index)
          GameUtility.SetGameObjectActive((Component) ((Component) this.DamageTextList[index].text).transform.Find("count_font_add"), true);
        return false;
      }

      public void Skip()
      {
        for (int index = 0; index < this.DamageTextList.Count; ++index)
        {
          GameUtility.SetGameObjectActive((Component) this.DamageTextList[index].text, true);
          this.DamageTextList[index].text.text = this.DamageTextList[index].num.ToString();
          GameUtility.SetGameObjectActive((Component) ((Component) this.DamageTextList[index].text).transform.Find("count_font_add"), true);
        }
      }

      protected struct DamageText
      {
        public Text text;
        public int num;

        public DamageText(Text text, int num)
        {
          this.text = text;
          this.num = num;
        }
      }
    }

    private class State_AttackDamage : WorldRaidBattleResult.State_DisplayDamage
    {
      public override void Begin(WorldRaidBattleResult self)
      {
        long currentWorldRaidBossHp = (long) GlobalVars.CurrentWorldRaidBossHP;
        long num = 0;
        SceneBattle instance = SceneBattle.Instance;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instance, (UnityEngine.Object) null))
        {
          Unit unit = instance.Battle.Enemys.Find((Predicate<Unit>) (e => e.SettingNPC != null && (bool) e.SettingNPC.is_raid_boss));
          if (unit != null)
            num = unit.GetCurrentHP() - (long) unit.OverKillDamage;
        }
        long damage = Math.Max(currentWorldRaidBossHp - num, 0L);
        this.Init(self.AttackDamageParent, (GameObject) null, self.AttackDamageText01, self.AttackDamageText02, self.AttackDamageText03, damage);
      }

      public override void End(WorldRaidBattleResult self)
      {
        FlowNode_GameObject.ActivateOutputLinks((Component) self, 101);
      }
    }

    private class State_TotalDamage : WorldRaidBattleResult.State_DisplayDamage
    {
      public override void Begin(WorldRaidBattleResult self)
      {
        long totalDamage = MonoSingleton<GameManager>.Instance.RecentWorldRaidBtlEndParam.total_damage;
        this.Init(self.TotalDamageParent, self.AttackDamageParent, self.TotalDamageText01, self.TotalDamageText02, self.TotalDamageText03, totalDamage);
      }

      public override void End(WorldRaidBattleResult self)
      {
        FlowNode_GameObject.ActivateOutputLinks((Component) self, 102);
      }
    }

    private class State_DisplayReward : State<WorldRaidBattleResult>
    {
      protected const float NEXT_ITEM_SECOND = 0.2f;
      private List<GameObject> RewardIconList = new List<GameObject>();
      private List<Coroutine> CoroutineList = new List<Coroutine>();
      private float CountupTime;
      private int IconIndex;

      public override void Begin(WorldRaidBattleResult self)
      {
        GameUtility.SetGameObjectActive(self.TotalDamageParent, false);
        if (MonoSingleton<GameManager>.Instance.RecentWorldRaidBtlEndParam == null || MonoSingleton<GameManager>.Instance.RecentWorldRaidBtlEndParam.reward == null)
        {
          self.StateMachine.GotoState<WorldRaidBattleResult.State_Finish>();
        }
        else
        {
          JSON_WorldRaidBattleRewardData[] dropLottery = MonoSingleton<GameManager>.Instance.RecentWorldRaidBtlEndParam.reward.drop_lottery;
          JSON_WorldRaidBattleRewardData[] lastAttack = MonoSingleton<GameManager>.Instance.RecentWorldRaidBtlEndParam.reward.last_attack;
          if ((dropLottery == null || dropLottery.Length <= 0) && (lastAttack == null || lastAttack.Length <= 0))
          {
            self.StateMachine.GotoState<WorldRaidBattleResult.State_Finish>();
          }
          else
          {
            GameUtility.SetGameObjectActive(self.RewardParent, true);
            GameUtility.SetGameObjectActive(self.RewardIconBase, false);
            if (dropLottery != null)
            {
              for (int index = 0; index < dropLottery.Length; ++index)
              {
                if (!(dropLottery[index].item_iname == "NONE"))
                {
                  WorldRaidBattleRewardData battleRewardData = new WorldRaidBattleRewardData();
                  battleRewardData.Deserialize(dropLottery[index]);
                  GameObject gameObject1 = UnityEngine.Object.Instantiate<GameObject>(self.RewardIconBase, self.RewardIconBase.transform.parent);
                  GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(self.RewardIconPrefab, gameObject1.transform.Find("item"));
                  GameUtility.SetGameObjectActive(gameObject2, true);
                  GameUtility.SetGameObjectActive((Component) gameObject1.transform.Find("lastattack"), false);
                  GiftData reward = new GiftData();
                  reward.Deserialize(battleRewardData.ConvertToJsonGift());
                  gameObject2.GetComponent<RaidRewardIcon>().Initialize(reward);
                  this.RewardIconList.Add(gameObject1);
                }
              }
            }
            if (lastAttack == null)
              return;
            for (int index = 0; index < lastAttack.Length; ++index)
            {
              if (!(lastAttack[index].item_iname == "NONE"))
              {
                WorldRaidBattleRewardData battleRewardData = new WorldRaidBattleRewardData();
                battleRewardData.Deserialize(lastAttack[index]);
                GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(self.RewardIconBase, self.RewardIconBase.transform.parent);
                GameObject gameObject4 = UnityEngine.Object.Instantiate<GameObject>(self.RewardIconPrefab, gameObject3.transform.Find("item"));
                GameUtility.SetGameObjectActive(gameObject4, true);
                GameUtility.SetGameObjectActive((Component) gameObject3.transform.Find("lastattack"), true);
                GiftData reward = new GiftData();
                reward.Deserialize(battleRewardData.ConvertToJsonGift());
                gameObject4.GetComponent<RaidRewardIcon>().Initialize(reward);
                this.RewardIconList.Add(gameObject3);
              }
            }
          }
        }
      }

      public override void Update(WorldRaidBattleResult self)
      {
        if (this.IconIndex >= this.RewardIconList.Count)
        {
          this.Finish();
          self.StateMachine.GotoState<WorldRaidBattleResult.State_Idle>();
        }
        else
        {
          this.CountupTime += Time.deltaTime;
          if ((double) this.CountupTime < 0.20000000298023224)
            return;
          this.CountupTime = 0.0f;
          GameUtility.SetGameObjectActive(this.RewardIconList[this.IconIndex], true);
          this.CoroutineList.Add(self.StartCoroutine(this.Anim(this.RewardIconList[this.IconIndex])));
          ++this.IconIndex;
        }
      }

      public override void Command(WorldRaidBattleResult self, string cmd)
      {
        if (!(cmd == "skip"))
          return;
        this.Skip();
        this.Finish();
        self.StateMachine.GotoState<WorldRaidBattleResult.State_Idle>();
      }

      [DebuggerHidden]
      private IEnumerator Anim(GameObject target)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new WorldRaidBattleResult.State_DisplayReward.\u003CAnim\u003Ec__Iterator0()
        {
          target = target
        };
      }

      private void Skip()
      {
        for (int index = 0; index < this.CoroutineList.Count; ++index)
          this.self.StopCoroutine(this.CoroutineList[index]);
        for (int index = 0; index < this.RewardIconList.Count; ++index)
        {
          GameUtility.SetGameObjectActive(this.RewardIconList[index], true);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RewardIconList[index], (UnityEngine.Object) null))
            this.RewardIconList[index].transform.localScale = Vector3.one;
        }
      }

      private void Finish() => FlowNode_GameObject.ActivateOutputLinks((Component) this.self, 103);
    }

    private class State_Finish : State<WorldRaidBattleResult>
    {
      public override void Begin(WorldRaidBattleResult self)
      {
        GameUtility.SetGameObjectActive(self.RewardParent, false);
        self.StateMachine.GotoState<WorldRaidBattleResult.State_Idle>();
      }

      public override void End(WorldRaidBattleResult self)
      {
        FlowNode_GameObject.ActivateOutputLinks((Component) self, 105);
      }
    }
  }
}
