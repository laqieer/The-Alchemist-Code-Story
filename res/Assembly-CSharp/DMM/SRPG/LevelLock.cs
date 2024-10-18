// Decompiled with JetBrains decompiler
// Type: SRPG.LevelLock
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [RequireComponent(typeof (Selectable))]
  public class LevelLock : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
  {
    private static List<LevelLock> mInstances = new List<LevelLock>();
    [HideInInspector]
    public long Condition;
    public Text ConditionText;
    public GameObject ShowLocked;
    public GameObject ShowUnlocked;
    public bool ToggleInteractable = true;
    public GameObject ReleaseStoryPart;
    [SerializeField]
    private Animator UnlockAnimator;
    [SerializeField]
    private bool UnlockAnimationOnStart;
    private int mUnlockLevel;
    private int mUnlockVipRank;

    public UnlockTargets condition
    {
      get => (UnlockTargets) this.Condition;
      set => this.Condition = (long) value;
    }

    public static void UpdateLockStates()
    {
      for (int index = 0; index < LevelLock.mInstances.Count; ++index)
        LevelLock.mInstances[index].UpdateLockState();
    }

    private void OnEnable() => LevelLock.mInstances.Add(this);

    private void OnDisable() => LevelLock.mInstances.Remove(this);

    private void Start()
    {
      foreach (UnlockParam unlock in MonoSingleton<GameManager>.Instance.MasterParam.Unlocks)
      {
        if (unlock != null && unlock.UnlockTarget == this.condition)
        {
          this.mUnlockLevel = unlock.PlayerLevel;
          this.mUnlockVipRank = unlock.VipRank;
          break;
        }
      }
      this.UpdateLockState();
      if (!this.UnlockAnimationOnStart)
        return;
      this.StartCoroutine(this.PlayUnlockAnimationAuto());
    }

    public void UpdateLockState()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      bool flag = MonoSingleton<GameManager>.Instance.Player.CheckUnlock(this.condition);
      if (this.ToggleInteractable)
      {
        Selectable component = ((Component) this).GetComponent<Selectable>();
        if (Object.op_Inequality((Object) component, (Object) null))
          component.interactable = flag;
      }
      if (Object.op_Inequality((Object) this.ShowUnlocked, (Object) null))
        this.ShowUnlocked.SetActive(flag);
      if (Object.op_Inequality((Object) this.ShowLocked, (Object) null))
        this.ShowLocked.SetActive(!flag);
      if (!Object.op_Inequality((Object) this.ConditionText, (Object) null))
        return;
      if (this.mUnlockLevel > 0 && player.Lv < this.mUnlockLevel)
      {
        this.ConditionText.text = string.Format(LocalizedText.Get("sys.UNLOCK_LV"), (object) this.mUnlockLevel);
      }
      else
      {
        if (this.mUnlockVipRank <= 0 || player.VipRank >= this.mUnlockVipRank)
          return;
        this.ConditionText.text = string.Format(LocalizedText.Get("sys.UNLOCK_VIP"), (object) this.mUnlockVipRank);
      }
    }

    public static bool ShowLockMessage(int playerLv, int playerVipRank, UnlockTargets target)
    {
      foreach (UnlockParam unlock in MonoSingleton<GameManager>.Instance.MasterParam.Unlocks)
      {
        if (unlock != null && unlock.UnlockTarget == target)
          return LevelLock.ShowLockMessage(playerLv, unlock.PlayerLevel, playerVipRank, unlock.VipRank, unlock);
      }
      return false;
    }

    public static bool ShowLockMessage(
      int playerLv,
      int reqLv,
      int vipRank,
      int reqVipRank,
      UnlockParam unlock_param)
    {
      if (reqLv > playerLv)
      {
        UIUtility.SystemMessage((string) null, string.Format(LocalizedText.Get("sys.UNLOCK_REQLV"), (object) reqLv), (UIUtility.DialogResultEvent) null);
        return true;
      }
      if (reqVipRank > vipRank)
      {
        UIUtility.SystemMessage((string) null, string.Format(LocalizedText.Get("sys.UNLOCK_REQVIP"), (object) reqVipRank), (UIUtility.DialogResultEvent) null);
        return true;
      }
      if (MonoSingleton<GameManager>.Instance.Player.IsClearUnclockConditions_Quest(unlock_param.ClearQuests))
        return false;
      string unlockCondsTextQuest = LevelLock.CreateUnlockCondsText_Quest(unlock_param);
      if (!string.IsNullOrEmpty(unlockCondsTextQuest))
        UIUtility.SystemMessage((string) null, unlockCondsTextQuest, (UIUtility.DialogResultEvent) null);
      return true;
    }

    public static string CreateUnlockCondsText_Quest(UnlockParam unlock_param)
    {
      if (unlock_param == null)
        return string.Empty;
      string[] clearQuests = unlock_param.ClearQuests;
      if (clearQuests == null || clearQuests.Length <= 0)
        return string.Empty;
      string empty = string.Empty;
      if (!string.IsNullOrEmpty(unlock_param.OverWriteQuestText))
        return string.Format(LocalizedText.Get("sys.UNLOCK_COND_QUEST"), (object) unlock_param.OverWriteQuestText);
      for (int index = 0; index < clearQuests.Length; ++index)
      {
        QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(clearQuests[index]);
        if (quest != null)
        {
          empty += string.Format(LocalizedText.Get("sys.UNLOCK_COND_QUEST_NAME"), (object) quest.title, (object) quest.name);
          if (index < clearQuests.Length - 1)
            empty += "\n";
        }
      }
      return string.Format(LocalizedText.Get("sys.UNLOCK_COND_QUEST"), (object) empty);
    }

    public static UnlockTargets GetTargetByQuestId(string quest_id, UnlockTargets default_value)
    {
      QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(quest_id);
      if (quest == null)
        return default_value;
      if (quest.type == QuestTypes.Event)
      {
        switch (quest.world)
        {
          case "WD_SEISEKI":
            return UnlockTargets.SeisekiQuest;
          case "WD_BABEL":
            return UnlockTargets.BabelQuest;
          default:
            return UnlockTargets.EventQuest;
        }
      }
      else
      {
        if (quest.type == QuestTypes.Multi || quest.type == QuestTypes.MultiGps)
          return UnlockTargets.MultiPlay;
        return quest.type == QuestTypes.Character ? UnlockTargets.CharacterQuest : default_value;
      }
    }

    public static bool IsNeedCheckUnlockConds(QuestParam quest_param)
    {
      return quest_param != null && (quest_param.type == QuestTypes.Event || quest_param.type == QuestTypes.Multi || quest_param.type == QuestTypes.MultiGps || quest_param.type == QuestTypes.Character);
    }

    public static bool IsPlayableQuest(QuestParam quest_param)
    {
      return !LevelLock.IsNeedCheckUnlockConds(quest_param) || MonoSingleton<GameManager>.Instance.Player.CheckUnlock(LevelLock.GetTargetByQuestId(quest_param.iname, UnlockTargets.EventQuest));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      LevelLock.ShowLockMessage(player.Lv, player.VipRank, this.condition);
      ((AbstractEventData) eventData).Use();
    }

    [DebuggerHidden]
    private IEnumerator WaitUnlockAnimation()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new LevelLock.\u003CWaitUnlockAnimation\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    [DebuggerHidden]
    private IEnumerator PlayUnlockAnimationAuto(LevelLock.OnUnlockAnimationEnd callback = null)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new LevelLock.\u003CPlayUnlockAnimationAuto\u003Ec__Iterator1()
      {
        callback = callback,
        \u0024this = this
      };
    }

    public void PlayUnlockAnimation(LevelLock.OnUnlockAnimationEnd callback)
    {
      this.StartCoroutine(this.PlayUnlockAnimationAuto(callback));
    }

    public bool GetIsUnlockAnimationPlayable()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      UnlockParam unlockParam = instance.MasterParam.FindUnlockParam(this.condition);
      return unlockParam != null && instance.Player.Lv >= unlockParam.PlayerLevel && !PlayerPrefsUtility.GetIsUnlockLevelAnimationPlayed(this.condition);
    }

    public delegate void OnUnlockAnimationEnd(UnlockTargets condition);
  }
}
