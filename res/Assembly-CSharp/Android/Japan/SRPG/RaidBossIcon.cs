// Decompiled with JetBrains decompiler
// Type: SRPG.RaidBossIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

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

    public bool IsClosed
    {
      get
      {
        return this.mRaidBossInfo == null;
      }
    }

    public int No
    {
      get
      {
        return this.mNo;
      }
    }

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
      DataSource.Bind<RaidBossParam>(this.gameObject, raidBoss, false);
      UnitParam unitParam = MonoSingleton<GameManager>.Instance.GetUnitParam(raidBoss.UnitIName);
      if (unitParam != null)
        DataSource.Bind<UnitParam>(this.gameObject, unitParam, false);
      if (RaidManager.Instance.CurrentRaidBossData == null || RaidManager.Instance.CurrentRaidBossData.RaidBossInfo.BossId != boss_id)
        return;
      this.SetCursor(true, false);
    }

    public void Active()
    {
      GameObject gameObject = RaidManager.Instance.CurrentRaidBossData == null || RaidManager.Instance.CurrentRaidBossData.RaidBossInfo.No != this.mNo ? (this.mRaidBossInfo != null ? this.mCleared : this.mClosed) : this.mChallenge;
      if ((UnityEngine.Object) gameObject == (UnityEngine.Object) null)
        return;
      if ((UnityEngine.Object) this.mButton != (UnityEngine.Object) null)
        this.mButton.targetGraphic = (Graphic) gameObject.GetComponent<Image_Transparent>();
      gameObject.SetActive(true);
    }

    public void SetCursor(bool active, bool sound = true)
    {
      if ((UnityEngine.Object) this.mCursor == (UnityEngine.Object) null)
        return;
      if ((UnityEngine.Object) this.mCursorSound != (UnityEngine.Object) null && sound)
        this.mCursorSound.Play();
      this.mCursor.SetActive(active);
    }

    public void OnClearedDetail()
    {
      RaidManager.Instance.ShowDetail(this.mRaidBossInfo);
    }
  }
}
