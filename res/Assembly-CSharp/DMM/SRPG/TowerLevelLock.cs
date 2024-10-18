// Decompiled with JetBrains decompiler
// Type: SRPG.TowerLevelLock
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [RequireComponent(typeof (Selectable))]
  public class TowerLevelLock : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
  {
    public UnityEngine.UI.Text ConditionText;
    public GameObject ShowLocked;
    public GameObject ShowUnlocked;
    public bool ToggleInteractable = true;
    private int mUnlockLevel;
    private TowerParam Param;

    private void Start()
    {
      this.Param = DataSource.FindDataOfClass<TowerParam>(((Component) this).gameObject, (TowerParam) null);
      this.mUnlockLevel = (int) this.Param.unlock_level;
      this.UpdateLockState();
    }

    public bool GetIsUnlock() => this.Param != null && this.Param.is_unlock;

    private void UpdateLockState()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      bool isUnlock = this.GetIsUnlock();
      if (this.ToggleInteractable)
      {
        Selectable component = ((Component) this).GetComponent<Selectable>();
        if (Object.op_Inequality((Object) component, (Object) null))
          component.interactable = isUnlock;
      }
      if (Object.op_Inequality((Object) this.ShowUnlocked, (Object) null))
        this.ShowUnlocked.SetActive(isUnlock);
      if (Object.op_Inequality((Object) this.ShowLocked, (Object) null))
        this.ShowLocked.SetActive(!isUnlock);
      if (!Object.op_Inequality((Object) this.ConditionText, (Object) null) || this.mUnlockLevel <= 0 || player.Lv >= this.mUnlockLevel)
        return;
      this.ConditionText.text = string.Format(LocalizedText.Get("sys.UNLOCK_LV"), (object) this.mUnlockLevel);
    }

    public string ShowLockMessage(int playerLv, int reqLv)
    {
      return string.Format(LocalizedText.Get("sys.UNLOCK_COND_REQLV"), (object) reqLv);
    }

    public string ShowProgLockMessage()
    {
      if (this.Param == null || this.Param.is_unlock)
        return string.Empty;
      TowerFloorParam towerFloorParam = this.GetTowerFloorParam(this.Param.unlock_quest);
      if (towerFloorParam == null)
        return string.Empty;
      TowerParam towerParam = this.GetTowerParam(towerFloorParam.tower_id);
      if (towerParam == null)
        return string.Empty;
      return LocalizedText.Get("sys.UNLOCK_COND_TOWER_PROG", (object) towerParam.name, (object) towerFloorParam.name);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      StringBuilder stringBuilder = new StringBuilder();
      string str1 = this.ShowLockMessage(player.Lv, this.mUnlockLevel);
      string str2 = this.ShowProgLockMessage();
      if (this.Param != null && !this.Param.is_unlock && (!string.IsNullOrEmpty(str1) || !string.IsNullOrEmpty(str2)))
      {
        stringBuilder.Append(LocalizedText.Get("sys.UNLOCK_COND"));
        if (!this.IsIgnorePlayerLevel())
        {
          stringBuilder.Append(str1);
          stringBuilder.Append("\n");
        }
        stringBuilder.Append(str2);
        string msg = stringBuilder.ToString();
        if (!string.IsNullOrEmpty(msg))
          UIUtility.SystemMessage((string) null, msg, (UIUtility.DialogResultEvent) null);
      }
      ((AbstractEventData) eventData).Use();
    }

    public TowerParam GetTowerParam(string tower_id)
    {
      return MonoSingleton<GameManager>.Instance.FindTower(tower_id);
    }

    public TowerFloorParam GetTowerFloorParam(string iname)
    {
      return MonoSingleton<GameManager>.Instance.FindTowerFloor(iname);
    }

    private bool IsIgnorePlayerLevel() => this.mUnlockLevel == 0;
  }
}
