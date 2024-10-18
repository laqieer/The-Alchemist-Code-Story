// Decompiled with JetBrains decompiler
// Type: SRPG.WorldRaidHomeButton
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class WorldRaidHomeButton : MonoBehaviour
  {
    [SerializeField]
    private GameObject BannerObj;
    [SerializeField]
    private GameObject BannerUnderObj;
    [SerializeField]
    private Text TextObj;
    private bool mTextUpdateFlag;

    private void Start()
    {
      if (Object.op_Equality((Object) this.BannerObj, (Object) null) || Object.op_Equality((Object) ((Component) this).gameObject, (Object) null))
        return;
      if (!MonoSingleton<GameManager>.Instance.Player.CheckUnlock(UnlockTargets.WorldRaid))
      {
        ((Component) this).gameObject.SetActive(false);
      }
      else
      {
        WorldRaidParam currentWorldRaidParam = WorldRaidManager.GetCurrentWorldRaidParam();
        if (currentWorldRaidParam == null)
        {
          ((Component) this).gameObject.SetActive(false);
        }
        else
        {
          WorldRaidNoticeData worldRaidNoticeData = WorldRaidNoticeData.Load(currentWorldRaidParam);
          if (worldRaidNoticeData == null || worldRaidNoticeData.IsNeedNotice_Home())
          {
            if (!currentWorldRaidParam.IsWithinChallenge() && currentWorldRaidParam.IsWithinPeriod())
            {
              ((Component) this).gameObject.SetActive(true);
            }
            else
            {
              ((Component) this).gameObject.SetActive(false);
              return;
            }
          }
          if (currentWorldRaidParam.IsWithinChallenge())
          {
            GameUtility.SetGameObjectActive(this.BannerUnderObj, true);
            this.mTextUpdateFlag = true;
          }
          else if (currentWorldRaidParam.IsWithinPeriod())
          {
            GameUtility.SetGameObjectActive(this.BannerUnderObj, false);
          }
          else
          {
            ((Component) this).gameObject.SetActive(false);
            return;
          }
          this.BannerObj.SetActive(false);
        }
      }
    }

    private void Update()
    {
      if (!this.mTextUpdateFlag || !Object.op_Inequality((Object) this.TextObj, (Object) null))
        return;
      this.TextObj.text = LocalizedText.Get("sys.WORLDRAID_HOME_REMAIN") + WorldRaidManager.GetRemainTimeText();
    }
  }
}
