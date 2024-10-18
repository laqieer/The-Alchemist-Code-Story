// Decompiled with JetBrains decompiler
// Type: SRPG.LevelLock
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SRPG
{
  [RequireComponent(typeof (Selectable))]
  public class LevelLock : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
  {
    private static List<LevelLock> mInstances = new List<LevelLock>();
    public bool ToggleInteractable = true;
    public UnlockTargets Condition;
    public Text ConditionText;
    public GameObject ShowLocked;
    public GameObject ShowUnlocked;
    private int mUnlockLevel;
    private int mUnlockVipRank;

    public static void UpdateLockStates()
    {
      for (int index = 0; index < LevelLock.mInstances.Count; ++index)
        LevelLock.mInstances[index].UpdateLockState();
    }

    private void OnEnable()
    {
      LevelLock.mInstances.Add(this);
    }

    private void OnDisable()
    {
      LevelLock.mInstances.Remove(this);
    }

    private void Start()
    {
      foreach (UnlockParam unlock in MonoSingleton<GameManager>.Instance.MasterParam.Unlocks)
      {
        if (unlock != null && unlock.UnlockTarget == this.Condition)
        {
          this.mUnlockLevel = unlock.PlayerLevel;
          this.mUnlockVipRank = unlock.VipRank;
          break;
        }
      }
      this.UpdateLockState();
    }

    private void UpdateLockState()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      bool flag = MonoSingleton<GameManager>.Instance.Player.CheckUnlock(this.Condition);
      if (this.ToggleInteractable)
      {
        Selectable component = this.GetComponent<Selectable>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null)
          component.interactable = flag;
      }
      if ((UnityEngine.Object) this.ShowUnlocked != (UnityEngine.Object) null)
        this.ShowUnlocked.SetActive(flag);
      if ((UnityEngine.Object) this.ShowLocked != (UnityEngine.Object) null)
        this.ShowLocked.SetActive(!flag);
      if (!((UnityEngine.Object) this.ConditionText != (UnityEngine.Object) null))
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
          return LevelLock.ShowLockMessage(playerLv, unlock.PlayerLevel, playerVipRank, unlock.VipRank);
      }
      return false;
    }

    public static bool ShowLockMessage(int playerLv, int reqLv, int vipRank, int reqVipRank)
    {
      if (reqLv > playerLv)
      {
        UIUtility.SystemMessage((string) null, string.Format(LocalizedText.Get("sys.UNLOCK_REQLV"), (object) reqLv), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
        return true;
      }
      if (reqVipRank <= vipRank)
        return false;
      UIUtility.SystemMessage((string) null, string.Format(LocalizedText.Get("sys.UNLOCK_REQVIP"), (object) reqVipRank), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      return true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      LevelLock.ShowLockMessage(player.Lv, player.VipRank, this.Condition);
      eventData.Use();
    }
  }
}
