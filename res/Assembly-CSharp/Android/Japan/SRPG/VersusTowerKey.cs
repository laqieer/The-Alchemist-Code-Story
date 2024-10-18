// Decompiled with JetBrains decompiler
// Type: SRPG.VersusTowerKey
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "鍵の更新", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(2, "階層更新", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(3, "到達報酬", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "演出終了", FlowNode.PinTypes.Output, 100)]
  public class VersusTowerKey : MonoBehaviour, IFlowInterface
  {
    private List<GameObject> mCreateKey = new List<GameObject>();
    public Text floortxt;
    public Text floorEfftxt;
    public Text okText;
    public Text arrivalNumText;
    public Text arrivalEffNumText;
    public Text arrivalRewardText;
    public GameObject key;
    public GameObject parent;
    public GameObject floorObj;
    public GameObject rewardObj;
    public GameObject unitObj;
    public GameObject itemRoot;
    public GameObject arrivalObj;
    public GameObject keyRoot;
    public GameObject rewardRoot;
    public GameObject reglegationRoot;
    public GameObject ArrivalInfoRoot;
    public GameObject ClearInfoRoot;
    public GameObject RightRoot;
    public GameObject infoText;
    public Text infoLastText;
    public Image frameObj;
    public Sprite coinBase;
    public Sprite goldBase;
    public RawImage rewardTex;
    public string keyGetAnim;
    public string keyDefAnim;
    public string keyLostAnim;
    public string updateFloorAnim;
    public string downFloorAnim;
    public string rewardGetAnim;
    public string trriggerIn;
    public string trriggerOut;
    public string trriggerRewardIn;
    public string trriggerInLastFloor;
    public string trriggerInLastFloorOut;
    public Color rankDownColor;
    public Texture CoinTex;
    public Texture GoldTex;
    private VersusTowerKey.KEY_RESULT_STATE mState;
    private VersusTowerKey.RESULT mBattleRes;
    private bool mUpdateAnim;
    private bool mUpdateFloor;
    private int mAnimKeyIndex;
    private int mMaxKeyCount;

    private void Start()
    {
      this.RefreshData();
    }

    private void RefreshData()
    {
      GameManager instance1 = MonoSingleton<GameManager>.Instance;
      PlayerData player = instance1.Player;
      PartyData partyOfType = player.FindPartyOfType(PlayerPartyTypes.Versus);
      VsTowerMatchEndParam towerMatchEndParam = instance1.GetVsTowerMatchEndParam();
      if (towerMatchEndParam == null)
        return;
      if (partyOfType != null)
      {
        UnitData unitDataByUniqueId = player.FindUnitDataByUniqueID(partyOfType.GetUnitUniqueID(partyOfType.LeaderIndex));
        if (unitDataByUniqueId != null)
          DataSource.Bind<UnitData>(this.gameObject, unitDataByUniqueId, false);
      }
      int versusTowerFloor = player.VersusTowerFloor;
      if ((UnityEngine.Object) this.floortxt != (UnityEngine.Object) null)
        this.floortxt.text = versusTowerFloor.ToString();
      if ((UnityEngine.Object) this.floorEfftxt != (UnityEngine.Object) null)
        this.floorEfftxt.text = versusTowerFloor.ToString();
      int versusTowerKey = player.VersusTowerKey;
      if ((UnityEngine.Object) this.key != (UnityEngine.Object) null)
      {
        VersusTowerParam versusTowerParam = instance1.GetCurrentVersusTowerParam(-1);
        if (versusTowerParam != null)
        {
          int num = 0;
          while (num < (int) versusTowerParam.RankupNum)
          {
            GameObject go = UnityEngine.Object.Instantiate<GameObject>(this.key);
            if ((UnityEngine.Object) go != (UnityEngine.Object) null)
            {
              if ((UnityEngine.Object) this.parent != (UnityEngine.Object) null)
                go.transform.SetParent(this.parent.transform, false);
              if (versusTowerKey > 0)
                GameUtility.SetAnimatorTrigger(go, this.keyDefAnim);
              this.mCreateKey.Add(go);
            }
            ++num;
            --versusTowerKey;
          }
          this.key.SetActive(false);
          SceneBattle instance2 = SceneBattle.Instance;
          if ((UnityEngine.Object) instance2 != (UnityEngine.Object) null)
          {
            BattleCore battle = instance2.Battle;
            if (battle != null)
            {
              BattleCore.Record questRecord = battle.GetQuestRecord();
              if (questRecord.result == BattleCore.QuestResult.Win)
              {
                int a = !towerMatchEndParam.rankup ? towerMatchEndParam.key : (int) versusTowerParam.RankupNum;
                this.mAnimKeyIndex = player.VersusTowerKey;
                this.mMaxKeyCount = Mathf.Min(a, (int) versusTowerParam.RankupNum);
                this.mUpdateFloor = towerMatchEndParam.rankup;
                this.mBattleRes = VersusTowerKey.RESULT.WIN;
                if (this.mUpdateFloor)
                {
                  if ((UnityEngine.Object) this.arrivalNumText != (UnityEngine.Object) null)
                    this.arrivalNumText.text = towerMatchEndParam.floor.ToString() + LocalizedText.Get("sys.MULTI_VERSUS_FLOOR");
                  if ((UnityEngine.Object) this.arrivalEffNumText != (UnityEngine.Object) null)
                    this.arrivalEffNumText.text = towerMatchEndParam.floor.ToString() + LocalizedText.Get("sys.MULTI_VERSUS_FLOOR");
                }
              }
              else if (questRecord.result == BattleCore.QuestResult.Lose && (int) versusTowerParam.LoseNum > 0)
              {
                this.mAnimKeyIndex = player.VersusTowerKey - 1;
                this.mMaxKeyCount = Math.Max(towerMatchEndParam.key, 0);
                this.mUpdateFloor = this.mAnimKeyIndex < 0 && (int) versusTowerParam.DownFloor > 0;
                this.mBattleRes = VersusTowerKey.RESULT.LOSE;
                if (this.mUpdateFloor && (UnityEngine.Object) this.arrivalNumText != (UnityEngine.Object) null)
                  this.arrivalNumText.text = Math.Max(towerMatchEndParam.floor, 1).ToString();
              }
              else
                this.mBattleRes = VersusTowerKey.RESULT.DRAW;
            }
          }
          if ((UnityEngine.Object) this.infoText != (UnityEngine.Object) null)
            this.infoText.SetActive(this.mBattleRes == VersusTowerKey.RESULT.WIN && (int) versusTowerParam.RankupNum > 0);
        }
      }
      if (!this.mUpdateFloor)
        return;
      this.SetButtonText(true);
    }

    private void SetButtonText(bool bNext)
    {
      if (!((UnityEngine.Object) this.okText != (UnityEngine.Object) null))
        return;
      this.okText.text = LocalizedText.Get(!bNext ? "sys.CMD_OK" : "sys.BTN_NEXT");
    }

    private void SetupRewardItem()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      VsTowerMatchEndParam towerMatchEndParam = instance.GetVsTowerMatchEndParam();
      VersusTowerParam versusTowerParam = instance.GetCurrentVersusTowerParam(towerMatchEndParam.floor);
      if (versusTowerParam == null || string.IsNullOrEmpty((string) versusTowerParam.ArrivalIteminame) || (UnityEngine.Object) this.rewardObj == (UnityEngine.Object) null)
        return;
      DataSource.Bind<VersusTowerParam>(this.rewardObj, versusTowerParam, false);
      VersusTowerRewardItem component = this.rewardObj.GetComponent<VersusTowerRewardItem>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      component.Refresh(VersusTowerRewardItem.REWARD_TYPE.Arrival, 0);
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          this.mUpdateAnim = true;
          this.mState = VersusTowerKey.KEY_RESULT_STATE.GET_KEY;
          if (this.mBattleRes == VersusTowerKey.RESULT.WIN)
          {
            this.StartCoroutine(this.UpdateKeyAnim());
            break;
          }
          if (this.mBattleRes == VersusTowerKey.RESULT.LOSE)
          {
            this.StartCoroutine(this.UpdateLostKeyAnim());
            break;
          }
          this.mUpdateAnim = false;
          break;
        case 2:
          this.mUpdateAnim = true;
          this.mState = VersusTowerKey.KEY_RESULT_STATE.UPDATE_FLOOR;
          this.StartCoroutine(this.UpdateFloorAnim());
          break;
        case 3:
          this.mUpdateAnim = true;
          this.mState = VersusTowerKey.KEY_RESULT_STATE.GET_REWARD;
          this.StartCoroutine(this.UpdateRewardAnim());
          break;
      }
    }

    [DebuggerHidden]
    public virtual IEnumerator UpdateKeyAnim()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new VersusTowerKey.\u003CUpdateKeyAnim\u003Ec__Iterator0() { \u0024this = this };
    }

    [DebuggerHidden]
    public virtual IEnumerator UpdateLostKeyAnim()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new VersusTowerKey.\u003CUpdateLostKeyAnim\u003Ec__Iterator1() { \u0024this = this };
    }

    [DebuggerHidden]
    public virtual IEnumerator UpdateFloorAnim()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new VersusTowerKey.\u003CUpdateFloorAnim\u003Ec__Iterator2() { \u0024this = this };
    }

    [DebuggerHidden]
    public virtual IEnumerator UpdateRewardAnim()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new VersusTowerKey.\u003CUpdateRewardAnim\u003Ec__Iterator3() { \u0024this = this };
    }

    private void ReqAnim(string trrigger, bool isAnimator = false)
    {
      if (!((UnityEngine.Object) this.RightRoot != (UnityEngine.Object) null))
        return;
      if (isAnimator)
      {
        Animator component = this.RightRoot.GetComponent<Animator>();
        if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
          return;
        component.Play(trrigger);
      }
      else
        GameUtility.SetAnimatorTrigger(this.RightRoot, trrigger);
    }

    public void OnClickNextButton()
    {
      if (this.mUpdateAnim)
        return;
      VsTowerMatchEndParam towerMatchEndParam = MonoSingleton<GameManager>.Instance.GetVsTowerMatchEndParam();
      bool bNext = towerMatchEndParam != null && towerMatchEndParam.arravied == 1;
      switch (this.mState)
      {
        case VersusTowerKey.KEY_RESULT_STATE.GET_KEY:
          MonoSingleton<MySound>.Instance.PlaySEOneShot(SoundSettings.Current.Tap, 0.0f);
          if (this.mUpdateFloor)
          {
            this.SetButtonText(bNext);
            FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "UPDATE_FLOOR");
            break;
          }
          FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "FINISH_KEY_RESULT");
          break;
        case VersusTowerKey.KEY_RESULT_STATE.UPDATE_FLOOR:
          MonoSingleton<MySound>.Instance.PlaySEOneShot(SoundSettings.Current.Tap, 0.0f);
          if (this.mBattleRes == VersusTowerKey.RESULT.WIN && bNext)
          {
            FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "GET_REWARD");
            break;
          }
          this.SetButtonText(false);
          FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "FINISH_KEY_RESULT");
          break;
        case VersusTowerKey.KEY_RESULT_STATE.GET_REWARD:
          MonoSingleton<MySound>.Instance.PlaySEOneShot(SoundSettings.Current.Tap, 0.0f);
          FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "FINISH_KEY_RESULT");
          break;
      }
    }

    private enum KEY_RESULT_STATE
    {
      NONE,
      GET_KEY,
      UPDATE_FLOOR,
      GET_REWARD,
    }

    private enum RESULT
    {
      WIN,
      LOSE,
      DRAW,
    }
  }
}
