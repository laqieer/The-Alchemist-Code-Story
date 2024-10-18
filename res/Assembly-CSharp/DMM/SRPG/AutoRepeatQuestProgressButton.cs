// Decompiled with JetBrains decompiler
// Type: SRPG.AutoRepeatQuestProgressButton
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class AutoRepeatQuestProgressButton : MonoBehaviour
  {
    [SerializeField]
    private GameObject mButtonObj;
    [SerializeField]
    private GameObject mBadgeObj;

    private void Awake()
    {
      GameUtility.SetGameObjectActive(this.mButtonObj, false);
      GameUtility.SetGameObjectActive(this.mBadgeObj, false);
    }

    private void Update()
    {
      if (MonoSingleton<GameManager>.Instance.Player.AutoRepeatQuestProgress.IsExistRecord)
      {
        GameUtility.SetGameObjectActive(this.mButtonObj, true);
        if (MonoSingleton<GameManager>.Instance.Player.AutoRepeatQuestProgress.State != AutoRepeatQuestData.eState.AUTO_REPEAT_END)
          return;
        GameUtility.SetGameObjectActive(this.mBadgeObj, true);
      }
      else
      {
        GameUtility.SetGameObjectActive(this.mButtonObj, false);
        GameUtility.SetGameObjectActive(this.mBadgeObj, false);
      }
    }
  }
}
