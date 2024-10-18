// Decompiled with JetBrains decompiler
// Type: SRPG.TowerLevelLock
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SRPG
{
  [RequireComponent(typeof (Selectable))]
  public class TowerLevelLock : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
  {
    public bool ToggleInteractable = true;
    public UnityEngine.UI.Text ConditionText;
    public GameObject ShowLocked;
    public GameObject ShowUnlocked;
    private int mUnlockLevel;
    private TowerParam Param;

    private void Start()
    {
      this.Param = DataSource.FindDataOfClass<TowerParam>(this.gameObject, (TowerParam) null);
      this.mUnlockLevel = (int) this.Param.unlock_level;
      this.UpdateLockState();
    }

    public bool GetIsUnlock()
    {
      if (this.Param == null)
        return false;
      return this.Param.is_unlock;
    }

    private void UpdateLockState()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      bool isUnlock = this.GetIsUnlock();
      if (this.ToggleInteractable)
      {
        Selectable component = this.GetComponent<Selectable>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null)
          component.interactable = isUnlock;
      }
      if ((UnityEngine.Object) this.ShowUnlocked != (UnityEngine.Object) null)
        this.ShowUnlocked.SetActive(isUnlock);
      if ((UnityEngine.Object) this.ShowLocked != (UnityEngine.Object) null)
        this.ShowLocked.SetActive(!isUnlock);
      if (!((UnityEngine.Object) this.ConditionText != (UnityEngine.Object) null) || this.mUnlockLevel <= 0 || player.Lv >= this.mUnlockLevel)
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
          UIUtility.SystemMessage((string) null, msg, (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      }
      eventData.Use();
    }

    public TowerParam GetTowerParam(string tower_id)
    {
      return MonoSingleton<GameManager>.Instance.FindTower(tower_id);
    }

    public TowerFloorParam GetTowerFloorParam(string iname)
    {
      return MonoSingleton<GameManager>.Instance.FindTowerFloor(iname);
    }

    private bool IsIgnorePlayerLevel()
    {
      return this.mUnlockLevel == 0;
    }
  }
}
