// Decompiled with JetBrains decompiler
// Type: SRPG.EventShopListItemArena
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class EventShopListItemArena : MonoBehaviour
  {
    public GameObject mLockObject;
    public Text mLockText;

    private void Start()
    {
      Button component = this.GetComponent<Button>();
      if (!(bool) ((UnityEngine.Object) component) || !(bool) ((UnityEngine.Object) this.mLockObject) || !(bool) ((UnityEngine.Object) this.mLockText))
        return;
      if (MonoSingleton<GameManager>.Instance.Player.CheckUnlock(UnlockTargets.Arena))
      {
        component.interactable = true;
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
        component.interactable = false;
        this.mLockObject.SetActive(true);
        this.mLockText.text = LocalizedText.Get("sys.COINLIST_ARENA_LOCK", new object[1]
        {
          (object) num
        });
      }
    }
  }
}
