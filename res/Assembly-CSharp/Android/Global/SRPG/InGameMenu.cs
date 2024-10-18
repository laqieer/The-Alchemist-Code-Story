// Decompiled with JetBrains decompiler
// Type: SRPG.InGameMenu
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(2, "Give Up", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "Close Give Up Window", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Start Debug", FlowNode.PinTypes.Input, 0)]
  public class InGameMenu : MonoBehaviour, IFlowInterface
  {
    public const int PINID_DEBUG = 1;
    public const int PINID_GIVEUP = 2;
    public const int PINID_CLOSE_GIVEUP_WINDOW = 100;
    public GameObject MissionButton;
    public GameObject ExitButton;
    public GameObject OptionButton;
    public GameObject DebugButton;
    public GameObject ArenaSkipButton;
    public Button AutoPlayOn;
    public Button AutoPlayOff;
    public Toggle AutoPlay;
    public GameObject AutoMode_Parent;
    public GameObject AutoMode_Treasure;
    public GameObject AutoMode_Skill;
    private GameObject mGiveUpWindow;
    public bool HideMissionButton;

    private void Start()
    {
      QuestParam questParam = (QuestParam) null;
      SceneBattle instance = SceneBattle.Instance;
      if ((UnityEngine.Object) instance != (UnityEngine.Object) null)
      {
        questParam = instance.CurrentQuest;
        instance.OnQuestEnd += new SceneBattle.QuestEndEvent(this.OnQuestEnd);
        if (questParam != null && questParam.CheckAllowedAutoBattle())
        {
          if ((UnityEngine.Object) this.AutoPlayOn != (UnityEngine.Object) null)
          {
            this.AutoPlayOn.gameObject.SetActive(!instance.Battle.RequestAutoBattle);
            this.AutoPlayOn.onClick.AddListener(new UnityAction(this.TurnOnAutoPlay));
          }
          if ((UnityEngine.Object) this.AutoPlayOff != (UnityEngine.Object) null)
          {
            this.AutoPlayOff.gameObject.SetActive(instance.Battle.RequestAutoBattle);
            this.AutoPlayOff.onClick.AddListener(new UnityAction(this.TurnOffAutoPlay));
          }
          if ((UnityEngine.Object) this.AutoPlay != (UnityEngine.Object) null)
          {
            this.AutoPlay.gameObject.SetActive(true);
            GameUtility.SetToggle(this.AutoPlay, instance.Battle.RequestAutoBattle);
            this.AutoPlay.onValueChanged.AddListener((UnityAction<bool>) (yes => this.ToggleAutoPlay(yes)));
          }
          if ((UnityEngine.Object) this.AutoMode_Parent != (UnityEngine.Object) null)
            this.AutoMode_Parent.gameObject.SetActive(instance.Battle.RequestAutoBattle);
          if ((UnityEngine.Object) this.AutoMode_Treasure != (UnityEngine.Object) null)
            this.AutoMode_Treasure.SetActive(GameUtility.Config_AutoMode_Treasure.Value);
          if ((UnityEngine.Object) this.AutoMode_Skill != (UnityEngine.Object) null)
            this.AutoMode_Skill.SetActive(GameUtility.Config_AutoMode_DisableSkill.Value);
        }
        else
        {
          if ((UnityEngine.Object) this.AutoPlayOn != (UnityEngine.Object) null)
            this.AutoPlayOn.gameObject.SetActive(false);
          if ((UnityEngine.Object) this.AutoPlayOff != (UnityEngine.Object) null)
            this.AutoPlayOff.gameObject.SetActive(false);
          if ((UnityEngine.Object) this.AutoPlay != (UnityEngine.Object) null)
            this.AutoPlay.gameObject.SetActive(false);
          if ((UnityEngine.Object) this.AutoMode_Parent != (UnityEngine.Object) null)
            this.AutoMode_Parent.SetActive(false);
          if ((UnityEngine.Object) this.AutoMode_Treasure != (UnityEngine.Object) null)
            this.AutoMode_Treasure.SetActive(false);
          if ((UnityEngine.Object) this.AutoMode_Skill != (UnityEngine.Object) null)
            this.AutoMode_Skill.SetActive(false);
          if (SceneBattle.Instance.IsPlayingArenaQuest && (UnityEngine.Object) this.ArenaSkipButton != (UnityEngine.Object) null)
            this.ArenaSkipButton.SetActive(true);
        }
      }
      if ((UnityEngine.Object) this.DebugButton != (UnityEngine.Object) null)
        this.DebugButton.SetActive(false);
      if ((UnityEngine.Object) this.MissionButton != (UnityEngine.Object) null && questParam != null)
      {
        bool flag = !string.IsNullOrEmpty(questParam.mission);
        if (this.HideMissionButton)
        {
          this.MissionButton.SetActive(flag);
        }
        else
        {
          Selectable component = this.MissionButton.GetComponent<Selectable>();
          if ((UnityEngine.Object) component != (UnityEngine.Object) null)
            component.interactable = flag;
        }
      }
      if (!((UnityEngine.Object) this.ExitButton != (UnityEngine.Object) null))
        return;
      bool flag1 = questParam != null && questParam.CheckAllowedRetreat();
      this.ExitButton.SetActive(flag1);
      if (!flag1 || !((UnityEngine.Object) instance != (UnityEngine.Object) null) || !instance.IsPlayingArenaQuest)
        return;
      this.ExitButton.GetComponentInChildren<LText>(true).text = LocalizedText.Get("sys.BTN_RETIRE_ARENA");
      if (!instance.Battle.IsArenaSkip)
        return;
      Button component1 = this.ExitButton.GetComponent<Button>();
      if (!(bool) ((UnityEngine.Object) component1))
        return;
      component1.interactable = false;
    }

    private void ToggleAutoPlay(bool enable)
    {
      if ((UnityEngine.Object) SceneBattle.Instance != (UnityEngine.Object) null)
      {
        SceneBattle.Instance.Battle.RequestAutoBattle = enable;
        GameUtility.Config_UseAutoPlay.Value = enable;
        if (enable)
          GameUtility.SetNeverSleep();
        else
          GameUtility.SetDefaultSleepSetting();
      }
      if ((UnityEngine.Object) this.AutoPlayOn != (UnityEngine.Object) null)
        this.AutoPlayOn.gameObject.SetActive(!enable);
      if ((UnityEngine.Object) this.AutoPlayOff != (UnityEngine.Object) null)
        this.AutoPlayOff.gameObject.SetActive(enable);
      if (!((UnityEngine.Object) this.AutoMode_Parent != (UnityEngine.Object) null))
        return;
      this.AutoMode_Parent.gameObject.SetActive(true);
      Animator component = this.AutoMode_Parent.GetComponent<Animator>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      component.SetBool("open", enable);
    }

    private void TurnOnAutoPlay()
    {
      this.ToggleAutoPlay(true);
    }

    private void TurnOffAutoPlay()
    {
      this.ToggleAutoPlay(false);
    }

    private void OnDestroy()
    {
      if (!((UnityEngine.Object) SceneBattle.Instance != (UnityEngine.Object) null))
        return;
      SceneBattle.Instance.OnQuestEnd -= new SceneBattle.QuestEndEvent(this.OnQuestEnd);
    }

    private void OnQuestEnd()
    {
      this.Activated(100);
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 2:
          string text = LocalizedText.Get("sys.CONFIRM_GIVEUP");
          if ((UnityEngine.Object) SceneBattle.Instance != (UnityEngine.Object) null && SceneBattle.Instance.IsPlayingArenaQuest)
            text = LocalizedText.Get("sys.CONFIRM_GIVEUP_ARENA");
          this.mGiveUpWindow = UIUtility.ConfirmBox(text, new UIUtility.DialogResultEvent(this.OnGiveUp), (UIUtility.DialogResultEvent) null, (GameObject) null, true, 1, (string) null, (string) null);
          break;
        case 100:
          if (!((UnityEngine.Object) this.mGiveUpWindow != (UnityEngine.Object) null))
            break;
          Win_Btn_DecideCancel_FL_C component = this.mGiveUpWindow.GetComponent<Win_Btn_DecideCancel_FL_C>();
          if ((UnityEngine.Object) component != (UnityEngine.Object) null)
            component.BeginClose();
          this.mGiveUpWindow = (GameObject) null;
          break;
      }
    }

    private void OnGiveUp(GameObject go)
    {
      if ((UnityEngine.Object) SceneBattle.Instance != (UnityEngine.Object) null && SceneBattle.Instance.IsPlayingArenaQuest)
        SceneBattle.Instance.ForceEndQuestInArena();
      else
        SceneBattle.Instance.ForceEndQuest();
      CanvasGroup component = this.GetComponent<CanvasGroup>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null) || !((UnityEngine.Object) this.ArenaSkipButton == (UnityEngine.Object) null))
        return;
      component.blocksRaycasts = false;
    }
  }
}
