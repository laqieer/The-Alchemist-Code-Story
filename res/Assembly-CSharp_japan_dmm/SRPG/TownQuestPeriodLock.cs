// Decompiled with JetBrains decompiler
// Type: SRPG.TownQuestPeriodLock
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class TownQuestPeriodLock : MonoBehaviour
  {
    [SerializeField]
    private TownQuestPeriodLock.PeriodlockTargets Target = TownQuestPeriodLock.PeriodlockTargets.None;
    [SerializeField]
    private bool isDraw = true;
    [SerializeField]
    private GameObject LockObject;

    private void Start()
    {
      Button component = ((Component) this).GetComponent<Button>();
      bool flag = false;
      switch (this.Target)
      {
        case TownQuestPeriodLock.PeriodlockTargets.Genesis:
          GenesisParam genesisParam = MonoSingleton<GameManager>.Instance.MasterParam.GetGenesisParam();
          if (genesisParam != null)
          {
            flag = !genesisParam.IsWithinPeriod();
            break;
          }
          break;
        case TownQuestPeriodLock.PeriodlockTargets.Advance:
          flag = !AdvanceEventParam.IsWithinPeriod();
          break;
      }
      bool active = !this.isDraw ? !flag : flag;
      GameUtility.SetGameObjectActive(this.LockObject, active);
      if (!active)
        return;
      GameUtility.SetButtonIntaractable(component, false);
    }

    [Flags]
    public enum PeriodlockTargets
    {
      None = 1,
      Genesis = 2,
      Advance = 4,
    }
  }
}
