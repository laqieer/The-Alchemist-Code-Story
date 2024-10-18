// Decompiled with JetBrains decompiler
// Type: SRPG.RaidBossIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class RaidBossIcon : MonoBehaviour
  {
    [SerializeField]
    private Button mButton;
    [SerializeField]
    private GameObject mClosed;
    [SerializeField]
    private GameObject mChallenge;
    [SerializeField]
    private GameObject mCleared;
    [SerializeField]
    private GameObject mCursor;
    [SerializeField]
    private CustomSound mCursorSound;
    private RaidBossInfo mRaidBossInfo;
    private int mNo;

    public bool IsClosed => this.mRaidBossInfo == null;

    public int No => this.mNo;

    public void Setup(RaidBossInfo info, int no)
    {
      this.mRaidBossInfo = info;
      this.mNo = no;
      if (this.mRaidBossInfo == null)
      {
        if (RaidManager.Instance.CurrentRaidBossData != null && RaidManager.Instance.CurrentRaidBossData.RaidBossInfo.No == no)
          this.mRaidBossInfo = RaidManager.Instance.CurrentRaidBossData.RaidBossInfo;
        else if (this.mNo > 0)
          return;
      }
      int boss_id = this.mNo <= 0 ? MonoSingleton<GameManager>.Instance.MasterParam.GetRaidArea(RaidManager.Instance.CurrentRaidAreaId).AreaBossId : this.mRaidBossInfo.BossId;
      RaidBossParam raidBoss = MonoSingleton<GameManager>.Instance.MasterParam.GetRaidBoss(boss_id);
      if (raidBoss == null)
        return;
      DataSource.Bind<RaidBossParam>(((Component) this).gameObject, raidBoss);
      UnitParam unitParam = MonoSingleton<GameManager>.Instance.GetUnitParam(raidBoss.UnitIName);
      if (unitParam != null)
        DataSource.Bind<UnitParam>(((Component) this).gameObject, unitParam);
      if (RaidManager.Instance.CurrentRaidBossData == null || RaidManager.Instance.CurrentRaidBossData.RaidBossInfo.BossId != boss_id)
        return;
      this.SetCursor(true, false);
    }

    public void Active()
    {
      GameObject gameObject = RaidManager.Instance.CurrentRaidBossData == null || RaidManager.Instance.CurrentRaidBossData.RaidBossInfo.No != this.mNo ? (this.mRaidBossInfo != null ? this.mCleared : this.mClosed) : this.mChallenge;
      if (Object.op_Equality((Object) gameObject, (Object) null))
        return;
      if (Object.op_Inequality((Object) this.mButton, (Object) null))
        ((Selectable) this.mButton).targetGraphic = (Graphic) gameObject.GetComponent<Image_Transparent>();
      gameObject.SetActive(true);
    }

    public void SetCursor(bool active, bool sound = true)
    {
      if (Object.op_Equality((Object) this.mCursor, (Object) null))
        return;
      if (Object.op_Inequality((Object) this.mCursorSound, (Object) null) && sound)
        this.mCursorSound.Play();
      this.mCursor.SetActive(active);
    }

    public void OnClearedDetail() => RaidManager.Instance.ShowDetail(this.mRaidBossInfo);
  }
}
