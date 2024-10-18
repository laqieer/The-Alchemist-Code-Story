// Decompiled with JetBrains decompiler
// Type: SRPG.InGameMenu
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
  [FlowNode.Pin(1, "Start Debug", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(2, "Give Up", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "Close Give Up Window", FlowNode.PinTypes.Input, 0)]
  public class InGameMenu : MonoBehaviour, IFlowInterface
  {
    public const int PINID_DEBUG = 1;
    public const int PINID_GIVEUP = 2;
    public const int PINID_CLOSE_GIVEUP_WINDOW = 100;
    public GameObject MissionButton;
    public GameObject ExitButton;
    public GameObject OptionButton;
    public GameObject DebugButton;
    public Button AutoPlayOn;
    public Button AutoPlayOff;
    public Toggle AutoPlay;
    public GameObject AutoMode_Parent;
    public GameObject AutoMode_Treasure;
    public GameObject AutoMode_Skill;
    public GameObject MultiHiSpeedParent;
    private GameObject mGiveUpWindow;
    public bool HideMissionButton;
    private static InGameMenu mInstance;

    public static InGameMenu Instance => InGameMenu.mInstance;

    private void Awake()
    {
      InGameMenu.mInstance = this;
      if (!Object.op_Inequality((Object) SceneBattle.Instance, (Object) null) || !Object.op_Equality((Object) SceneBattle.Instance.mInGameMenu, (Object) null))
        return;
      SceneBattle.Instance.mInGameMenu = this;
    }

    private void Start()
    {
      QuestParam questParam = (QuestParam) null;
      SceneBattle instance = SceneBattle.Instance;
      if (Object.op_Inequality((Object) instance, (Object) null))
      {
        questParam = instance.CurrentQuest;
        instance.OnQuestEnd += new SceneBattle.QuestEndEvent(this.OnQuestEnd);
        if (questParam != null && questParam.CheckAllowedAutoBattle())
        {
          if (Object.op_Inequality((Object) this.AutoPlayOn, (Object) null))
          {
            ((Component) this.AutoPlayOn).gameObject.SetActive(!instance.Battle.RequestAutoBattle);
            // ISSUE: method pointer
            ((UnityEvent) this.AutoPlayOn.onClick).AddListener(new UnityAction((object) this, __methodptr(TurnOnAutoPlay)));
          }
          if (Object.op_Inequality((Object) this.AutoPlayOff, (Object) null))
          {
            ((Component) this.AutoPlayOff).gameObject.SetActive(instance.Battle.RequestAutoBattle);
            // ISSUE: method pointer
            ((UnityEvent) this.AutoPlayOff.onClick).AddListener(new UnityAction((object) this, __methodptr(TurnOffAutoPlay)));
          }
          if (Object.op_Inequality((Object) this.AutoPlay, (Object) null))
          {
            ((Component) this.AutoPlay).gameObject.SetActive(true);
            GameUtility.SetToggle(this.AutoPlay, instance.Battle.RequestAutoBattle);
            // ISSUE: method pointer
            ((UnityEvent<bool>) this.AutoPlay.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(\u003CStart\u003Em__0)));
            ((Selectable) this.AutoPlay).interactable = !MonoSingleton<GameManager>.Instance.Player.IsAutoRepeatQuestMeasuring;
          }
          if (Object.op_Inequality((Object) this.AutoMode_Parent, (Object) null))
            this.AutoMode_Parent.gameObject.SetActive(instance.Battle.RequestAutoBattle);
          if (Object.op_Inequality((Object) this.AutoMode_Treasure, (Object) null))
          {
            if (questParam.type == QuestTypes.Multi || questParam.type == QuestTypes.MultiGps)
              this.AutoMode_Treasure.SetActive(GameUtility.Config_AutoMode_TreasureMulti.Value);
            else
              this.AutoMode_Treasure.SetActive(GameUtility.Config_AutoMode_Treasure.Value);
            if (MonoSingleton<GameManager>.Instance.Player.IsAutoRepeatQuestMeasuring)
              this.AutoMode_Treasure.SetActive(false);
          }
          if (Object.op_Inequality((Object) this.AutoMode_Skill, (Object) null))
          {
            if (questParam.type == QuestTypes.Multi || questParam.type == QuestTypes.MultiGps)
              this.AutoMode_Skill.SetActive(GameUtility.Config_AutoMode_DisableSkillMulti.Value);
            else
              this.AutoMode_Skill.SetActive(GameUtility.Config_AutoMode_DisableSkill.Value);
          }
        }
        else
        {
          if (Object.op_Inequality((Object) this.AutoPlayOn, (Object) null))
            ((Component) this.AutoPlayOn).gameObject.SetActive(false);
          if (Object.op_Inequality((Object) this.AutoPlayOff, (Object) null))
            ((Component) this.AutoPlayOff).gameObject.SetActive(false);
          if (Object.op_Inequality((Object) this.AutoPlay, (Object) null))
            ((Component) this.AutoPlay).gameObject.SetActive(false);
          if (Object.op_Inequality((Object) this.AutoMode_Parent, (Object) null))
            this.AutoMode_Parent.SetActive(false);
          if (Object.op_Inequality((Object) this.AutoMode_Treasure, (Object) null))
            this.AutoMode_Treasure.SetActive(false);
          if (Object.op_Inequality((Object) this.AutoMode_Skill, (Object) null))
            this.AutoMode_Skill.SetActive(false);
          if ((questParam.type == QuestTypes.Multi || questParam.type == QuestTypes.MultiGps) && Object.op_Inequality((Object) this.AutoPlay, (Object) null))
          {
            ((Component) this.AutoPlay).gameObject.SetActive(true);
            this.AutoPlay.isOn = false;
            ((Selectable) this.AutoPlay).interactable = false;
          }
        }
        if ((questParam.type == QuestTypes.Multi || instance.CurrentQuest.type == QuestTypes.MultiGps || questParam.type == QuestTypes.MultiTower) && Object.op_Inequality((Object) this.MultiHiSpeedParent, (Object) null))
          this.MultiHiSpeedParent.SetActive(true);
      }
      if (Object.op_Inequality((Object) this.DebugButton, (Object) null))
        this.DebugButton.SetActive(false);
      if (Object.op_Inequality((Object) this.MissionButton, (Object) null) && questParam != null)
      {
        bool flag = questParam.HasMission();
        if (this.HideMissionButton)
        {
          this.MissionButton.SetActive(flag);
        }
        else
        {
          Selectable component = this.MissionButton.GetComponent<Selectable>();
          if (Object.op_Inequality((Object) component, (Object) null))
            component.interactable = flag;
        }
      }
      if (!Object.op_Inequality((Object) this.ExitButton, (Object) null))
        return;
      bool flag1 = questParam != null && questParam.CheckAllowedRetreat();
      this.ExitButton.SetActive(flag1);
      if (!flag1 || !Object.op_Inequality((Object) instance, (Object) null) || !instance.IsPlayingPreCalcResultQuest)
        return;
      this.ExitButton.GetComponentInChildren<LText>(true).text = LocalizedText.Get("sys.BTN_RETIRE_ARENA");
      if (!instance.Battle.IsArenaSkip)
        return;
      Button component1 = this.ExitButton.GetComponent<Button>();
      if (!Object.op_Implicit((Object) component1))
        return;
      ((Selectable) component1).interactable = false;
    }

    private void ToggleAutoPlay(bool enable)
    {
      if (Object.op_Inequality((Object) SceneBattle.Instance, (Object) null))
      {
        SceneBattle.Instance.Battle.RequestAutoBattle = enable;
        if (enable)
          GameUtility.SetNeverSleep();
        else
          GameUtility.SetDefaultSleepSetting();
      }
      if (Object.op_Inequality((Object) this.AutoPlayOn, (Object) null))
        ((Component) this.AutoPlayOn).gameObject.SetActive(!enable);
      if (Object.op_Inequality((Object) this.AutoPlayOff, (Object) null))
        ((Component) this.AutoPlayOff).gameObject.SetActive(enable);
      if (Object.op_Inequality((Object) this.AutoMode_Parent, (Object) null))
      {
        this.AutoMode_Parent.gameObject.SetActive(true);
        Animator component = this.AutoMode_Parent.GetComponent<Animator>();
        if (Object.op_Inequality((Object) component, (Object) null))
          component.SetBool("open", enable);
      }
      SceneBattle.Instance.ToggleAutoPlay(enable);
    }

    private void TurnOnAutoPlay() => this.ToggleAutoPlay(true);

    private void TurnOffAutoPlay() => this.ToggleAutoPlay(false);

    public void ToggleAutoBattle(bool is_active)
    {
      SceneBattle instance = SceneBattle.Instance;
      if (!Object.op_Implicit((Object) instance) || instance.CurrentQuest == null || !instance.CurrentQuest.CheckAllowedAutoBattle())
        return;
      if (is_active)
      {
        if (Object.op_Inequality((Object) this.AutoPlayOn, (Object) null))
          ((Component) this.AutoPlayOn).gameObject.SetActive(!instance.Battle.RequestAutoBattle);
        if (Object.op_Inequality((Object) this.AutoPlayOff, (Object) null))
          ((Component) this.AutoPlayOff).gameObject.SetActive(instance.Battle.RequestAutoBattle);
        if (Object.op_Inequality((Object) this.AutoPlay, (Object) null))
          ((Component) this.AutoPlay).gameObject.SetActive(true);
        if (Object.op_Inequality((Object) this.AutoMode_Parent, (Object) null))
          this.AutoMode_Parent.gameObject.SetActive(instance.Battle.RequestAutoBattle);
        if (Object.op_Inequality((Object) this.AutoMode_Treasure, (Object) null))
        {
          if (SceneBattle.Instance.CurrentQuest.type == QuestTypes.Multi || SceneBattle.Instance.CurrentQuest.type == QuestTypes.MultiGps)
            this.AutoMode_Treasure.SetActive(GameUtility.Config_AutoMode_TreasureMulti.Value);
          else
            this.AutoMode_Treasure.SetActive(GameUtility.Config_AutoMode_Treasure.Value);
        }
        if (!Object.op_Inequality((Object) this.AutoMode_Skill, (Object) null))
          return;
        if (SceneBattle.Instance.CurrentQuest.type == QuestTypes.Multi || SceneBattle.Instance.CurrentQuest.type == QuestTypes.MultiGps)
          this.AutoMode_Skill.SetActive(GameUtility.Config_AutoMode_DisableSkillMulti.Value);
        else
          this.AutoMode_Skill.SetActive(GameUtility.Config_AutoMode_DisableSkill.Value);
      }
      else
      {
        if (Object.op_Inequality((Object) this.AutoPlayOn, (Object) null))
          ((Component) this.AutoPlayOn).gameObject.SetActive(false);
        if (Object.op_Inequality((Object) this.AutoPlayOff, (Object) null))
          ((Component) this.AutoPlayOff).gameObject.SetActive(false);
        if (Object.op_Inequality((Object) this.AutoPlay, (Object) null))
          ((Component) this.AutoPlay).gameObject.SetActive(false);
        if (Object.op_Inequality((Object) this.AutoMode_Parent, (Object) null))
          this.AutoMode_Parent.SetActive(false);
        if (Object.op_Inequality((Object) this.AutoMode_Treasure, (Object) null))
          this.AutoMode_Treasure.SetActive(false);
        if (!Object.op_Inequality((Object) this.AutoMode_Skill, (Object) null))
          return;
        this.AutoMode_Skill.SetActive(false);
      }
    }

    public void MultiAutoDisplay(bool isActive)
    {
      if (Object.op_Equality((Object) this.AutoPlay, (Object) null) || SceneBattle.Instance.CurrentQuest.type != QuestTypes.Multi && SceneBattle.Instance.CurrentQuest.type != QuestTypes.Gps && SceneBattle.Instance.CurrentQuest.type != QuestTypes.RankMatch)
        return;
      ((Component) this.AutoPlay).gameObject.SetActive(isActive);
    }

    private void OnDestroy()
    {
      if (Object.op_Inequality((Object) SceneBattle.Instance, (Object) null))
        SceneBattle.Instance.OnQuestEnd -= new SceneBattle.QuestEndEvent(this.OnQuestEnd);
      InGameMenu.mInstance = (InGameMenu) null;
    }

    private void OnQuestEnd() => this.Activated(100);

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 2:
          string text = LocalizedText.Get("sys.CONFIRM_GIVEUP");
          if (Object.op_Inequality((Object) SceneBattle.Instance, (Object) null) && SceneBattle.Instance.IsPlayingPreCalcResultQuest)
            text = LocalizedText.Get("sys.CONFIRM_GIVEUP_ARENA");
          if (MonoSingleton<GameManager>.Instance.Player != null && MonoSingleton<GameManager>.Instance.Player.IsAutoRepeatQuestMeasuring)
            text = LocalizedText.Get("sys.CONFIRM_GIVEUP_AUTO_REPEAT_CHECK");
          this.mGiveUpWindow = UIUtility.ConfirmBox(text, new UIUtility.DialogResultEvent(this.OnGiveUp), (UIUtility.DialogResultEvent) null, systemModal: true, systemModalPriority: 1);
          break;
        case 100:
          if (!Object.op_Inequality((Object) this.mGiveUpWindow, (Object) null))
            break;
          Win_Btn_DecideCancel_FL_C component = this.mGiveUpWindow.GetComponent<Win_Btn_DecideCancel_FL_C>();
          if (Object.op_Inequality((Object) component, (Object) null))
            component.BeginClose();
          this.mGiveUpWindow = (GameObject) null;
          break;
      }
    }

    private void OnGiveUp(GameObject go)
    {
      if (Object.op_Equality((Object) SceneBattle.Instance, (Object) null))
        return;
      if (MonoSingleton<GameManager>.Instance.Player != null && MonoSingleton<GameManager>.Instance.Player.IsAutoRepeatQuestMeasuring)
        MonoSingleton<GameManager>.Instance.Player.AutoRepeatQuestProgress.Reset();
      if (SceneBattle.Instance.IsPlayingArenaQuest)
        SceneBattle.Instance.ForceEndQuestInArena();
      else if (SceneBattle.Instance.IsPlayingGvGQuest)
        SceneBattle.Instance.ForceEndQuestInGvG();
      else
        SceneBattle.Instance.ForceEndQuest();
      CanvasGroup component = ((Component) this).GetComponent<CanvasGroup>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      component.blocksRaycasts = false;
    }
  }
}
