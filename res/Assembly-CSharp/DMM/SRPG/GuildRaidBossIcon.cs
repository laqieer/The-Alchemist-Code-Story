// Decompiled with JetBrains decompiler
// Type: SRPG.GuildRaidBossIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "Next Effect", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "Next Effect Start", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(2, "Beat Effect Prepare", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(3, "Beat Effect Execute", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(4, "Challenge Effect Start", FlowNode.PinTypes.Input, 4)]
  public class GuildRaidBossIcon : MonoBehaviour, IFlowInterface
  {
    public const int PIN_INPUT_NEXT_EFFECT = 1;
    public const int PIN_INPUT_BEAT_PREPARE = 2;
    public const int PIN_INPUT_BEAT_EXECUTE = 3;
    public const int PIN_INPUT_START_CHELLENGE_EFFECT = 4;
    public const int PIN_OUTPUT_NEXT_EFFECT_START = 101;
    [SerializeField]
    private Button mButton;
    [SerializeField]
    private GameObject mClosed;
    [SerializeField]
    private GameObject mChallenge;
    [SerializeField]
    private GameObject mCleared;
    [SerializeField]
    private GameObject mWait;
    [SerializeField]
    private GameObject mNoCleared;
    [SerializeField]
    private GameObject mCursor;
    [SerializeField]
    private CustomSound mCursorSound;
    [SerializeField]
    private GameObject mDetail;
    [SerializeField]
    private GameObject mBattle;
    [SerializeField]
    private Text mNumberText;
    [SerializeField]
    private GameObject mBeatEffect;
    [SerializeField]
    private GameObject mChallengeObject;
    private GameObject mLastBeatIcon;
    private GuildRaidBossParam mGuildRaidBoss;
    private int mRound;
    private int mNo;
    private float animatorSpeed;
    private bool IsCurrentBoss;

    public bool IsClosed => this.mGuildRaidBoss == null;

    public void Awake()
    {
      GameUtility.SetGameObjectActive(this.mDetail, false);
      GameUtility.SetGameObjectActive(this.mBattle, false);
      GameUtility.SetGameObjectActive(this.mChallenge, false);
      GameUtility.SetGameObjectActive(this.mWait, false);
      GameUtility.SetGameObjectActive(this.mClosed, false);
      GameUtility.SetGameObjectActive(this.mCleared, false);
    }

    public void Setup(GuildRaidBossParam param, int round, int no)
    {
      this.mGuildRaidBoss = param;
      this.mRound = round;
      this.mNo = no;
      GuildRaidManager instance = GuildRaidManager.Instance;
      this.SetCursor(false, false);
      if (Object.op_Inequality((Object) this.mNumberText, (Object) null))
        this.mNumberText.text = (instance.AreaBossCount * (instance.CurrentRound - 1) + this.mNo).ToString();
      if (this.mGuildRaidBoss == null)
        return;
      DataSource.Bind<GuildRaidBossParam>(((Component) this).gameObject, this.mGuildRaidBoss);
      UnitParam unitParam = MonoSingleton<GameManager>.Instance.GetUnitParam(this.mGuildRaidBoss.UnitIName);
      if (unitParam != null)
        DataSource.Bind<UnitParam>(((Component) this).gameObject, unitParam);
      if (GuildRaidManager.Instance.CurrentRound == this.mRound && GuildRaidManager.Instance.CurrentAreaNo == param.AreaNo && !GuildRaidManager.Instance.IsFinishGuildRaid())
      {
        this.IsCurrentBoss = true;
        if (!GuildRaidManager.Instance.IsCloseSchedule())
          this.SetCursor(true, false);
        if (!Object.op_Inequality((Object) this.mBattle, (Object) null))
          return;
        this.mBattle.SetActive(true);
      }
      else
      {
        this.IsCurrentBoss = false;
        if (Object.op_Inequality((Object) this.mDetail, (Object) null))
          this.mDetail.SetActive(true);
        if (GuildRaidManager.Instance.CurrentRound != this.mRound || GuildRaidManager.Instance.CurrentAreaNo - 1 != param.AreaNo || GuildRaidManager.Instance.IsFinishGuildRaid())
          return;
        this.mLastBeatIcon = this.mBeatEffect;
      }
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          if (!this.IsCurrentBoss)
            break;
          GameUtility.SetGameObjectActive(this.mChallengeObject, true);
          if (Object.op_Inequality((Object) this.mChallengeObject, (Object) null))
          {
            Animator component = this.mChallengeObject.GetComponent<Animator>();
            if (Object.op_Inequality((Object) component, (Object) null) && (double) component.speed > 0.0)
            {
              this.animatorSpeed = component.speed;
              component.speed = 0.0f;
            }
          }
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
          break;
        case 2:
          if (Object.op_Equality((Object) this.mLastBeatIcon, (Object) this.mBeatEffect))
            GameUtility.SetGameObjectActive(this.mBeatEffect, false);
          if (!this.IsCurrentBoss)
            break;
          GameUtility.SetGameObjectActive(this.mChallengeObject, false);
          break;
        case 3:
          if (!Object.op_Equality((Object) this.mLastBeatIcon, (Object) this.mBeatEffect))
            break;
          GameUtility.SetGameObjectActive(this.mBeatEffect, true);
          break;
        case 4:
          if (!this.IsCurrentBoss || !Object.op_Inequality((Object) this.mChallengeObject, (Object) null) || !this.mChallengeObject.GetActive())
            break;
          Animator component1 = this.mChallengeObject.GetComponent<Animator>();
          if (!Object.op_Inequality((Object) component1, (Object) null) || (double) this.animatorSpeed <= 0.0)
            break;
          component1.speed = this.animatorSpeed;
          break;
      }
    }

    public void Active()
    {
      GameObject mChallenge = this.mChallenge;
      GameObject gameObject = !this.IsCurrentBoss ? (GuildRaidManager.Instance.CurrentRound != this.mRound || GuildRaidManager.Instance.CurrentAreaNo >= this.mNo || GuildRaidManager.Instance.IsFinishGuildRaid() ? (this.mGuildRaidBoss != null ? this.mCleared : this.mClosed) : this.mWait) : (!GuildRaidManager.Instance.IsCloseSchedule() ? this.mChallenge : this.mWait);
      if (Object.op_Equality((Object) gameObject, (Object) null))
        return;
      if (Object.op_Inequality((Object) this.mButton, (Object) null))
        ((Selectable) this.mButton).targetGraphic = (Graphic) gameObject.GetComponent<Image_Transparent>();
      gameObject.SetActive(true);
    }

    public void SetCursor(bool active, bool sound = true)
    {
      if (Object.op_Equality((Object) this.mCursor, (Object) null))
        return;
      if (Object.op_Inequality((Object) this.mCursorSound, (Object) null) && sound)
        this.mCursorSound.Play();
      this.mCursor.SetActive(active);
    }

    public void OnClearedDetail() => GuildRaidManager.Instance.ShowDetail(this.mGuildRaidBoss);
  }
}
