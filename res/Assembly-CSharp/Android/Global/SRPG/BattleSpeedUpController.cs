// Decompiled with JetBrains decompiler
// Type: SRPG.BattleSpeedUpController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  public class BattleSpeedUpController : MonoBehaviour
  {
    private const float speedMul = 2f;
    [SerializeField]
    private Toggle speedUp;

    public bool isSpeedUpAllowed
    {
      get
      {
        if (GameUtility.GetCurrentScene() == GameUtility.EScene.BATTLE_MULTI)
          return false;
        QuestParam currentQuest = SceneBattle.Instance.CurrentQuest;
        return currentQuest != null && currentQuest.CheckAllowedAutoBattle() || currentQuest.type == QuestTypes.Arena;
      }
    }

    private void Start()
    {
      if (this.isSpeedUpAllowed)
      {
        bool enabled = false;
        if (PlayerPrefs.HasKey("SPEED_UP") && PlayerPrefs.GetInt("SPEED_UP") == 1)
          enabled = true;
        this.speedUp.gameObject.SetActive(true);
        GameUtility.SetToggle(this.speedUp, enabled);
        this.ToggleSpeedUp(enabled);
      }
      else
        this.speedUp.gameObject.SetActive(false);
      this.speedUp.onValueChanged.AddListener((UnityAction<bool>) (isEnabled => this.ToggleSpeedUp(isEnabled)));
    }

    private void ToggleSpeedUp(bool enabled)
    {
      float num = 1f;
      if (enabled)
      {
        num = 2f;
        PlayerPrefs.SetInt("SPEED_UP", 1);
      }
      else
        PlayerPrefs.SetInt("SPEED_UP", 0);
      TimeManager.SetTimeScaleWithPrevSaved(TimeManager.TimeScaleGroups.Game, num);
    }
  }
}
