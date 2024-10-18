// Decompiled with JetBrains decompiler
// Type: SRPG.EventShopListItemArena
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class EventShopListItemArena : MonoBehaviour
  {
    public GameObject mLockObject;
    public Text mLockText;

    private void Start()
    {
      Button component = ((Component) this).GetComponent<Button>();
      if (!Object.op_Implicit((Object) component) || !Object.op_Implicit((Object) this.mLockObject) || !Object.op_Implicit((Object) this.mLockText))
        return;
      if (MonoSingleton<GameManager>.Instance.Player.CheckUnlock(UnlockTargets.Arena))
      {
        ((Selectable) component).interactable = true;
        this.mLockObject.SetActive(false);
      }
      else
      {
        int num = 0;
        UnlockParam[] unlocks = MonoSingleton<GameManager>.Instance.MasterParam.Unlocks;
        if (unlocks == null)
          return;
        for (int index = 0; index < unlocks.Length; ++index)
        {
          UnlockParam unlockParam = unlocks[index];
          if (unlockParam != null && unlockParam.UnlockTarget == UnlockTargets.Arena)
          {
            num = unlockParam.PlayerLevel;
            break;
          }
        }
        ((Selectable) component).interactable = false;
        this.mLockObject.SetActive(true);
        this.mLockText.text = LocalizedText.Get("sys.COINLIST_ARENA_LOCK", (object) num);
      }
    }
  }
}
