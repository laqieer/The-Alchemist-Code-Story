// Decompiled with JetBrains decompiler
// Type: SRPG.GvGInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class GvGInfo : MonoBehaviour
  {
    [SerializeField]
    private Text mPhaseText;
    [SerializeField]
    private Text mNextText;
    [SerializeField]
    private Button mUpdateButton;
    [SerializeField]
    private GameObject mRestUnitCountRoot;
    [SerializeField]
    private GameObject mRankingRoot;
    [SerializeField]
    private GameObject mRankingTemplate;
    [SerializeField]
    private GameObject mCoolTimeRankingRoot;
    [SerializeField]
    private GameObject mCoolTimeRankingTemplate;
    [SerializeField]
    private Button mPresetButton;
    [SerializeField]
    private bool isRankingDraw;
    private List<GameObject> coolTimeTempList = new List<GameObject>();
    private DateTime mEndTime;
    private DateTime mElapsedTime;
    private List<GameObject> mRankingList = new List<GameObject>();

    private void Awake() => this.Refresh();

    private void Start()
    {
      GameUtility.SetGameObjectActive(this.mRankingRoot, false);
      GameUtility.SetGameObjectActive(this.mRankingTemplate, false);
      GameUtility.SetGameObjectActive(this.mCoolTimeRankingRoot, false);
      GameUtility.SetGameObjectActive(this.mCoolTimeRankingTemplate, false);
    }

    public void Refresh()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPresetButton, (UnityEngine.Object) null))
        ((Selectable) this.mPresetButton).interactable = GvGPeriodParam.GetGvGPeriod() != null;
      this.mElapsedTime = TimeManager.ServerTime;
      this.mEndTime = GvGInfo.SetNextPhaseTime(this.mPhaseText, this.mNextText);
      List<GvGHalfTime> halfTimeList = GvGManager.Instance.HalfTimeList;
      if (GvGManager.Instance.IsGvGBattleCoolTimeFirst && halfTimeList != null && halfTimeList.Count > 0)
      {
        this.isRankingDraw = true;
        this.coolTimeTempList.ForEach((Action<GameObject>) (p => UnityEngine.Object.Destroy((UnityEngine.Object) p)));
        this.coolTimeTempList.Clear();
        for (int index = 0; index < halfTimeList.Count; ++index)
        {
          GameObject root = UnityEngine.Object.Instantiate<GameObject>(this.mCoolTimeRankingTemplate, this.mCoolTimeRankingTemplate.transform.parent);
          if (!UnityEngine.Object.op_Equality((UnityEngine.Object) root, (UnityEngine.Object) null))
          {
            DataSource.Bind<GvGHalfTime>(root, halfTimeList[index]);
            this.coolTimeTempList.Add(root);
            root.SetActive(true);
            GvGCoolTimeRankingContent dataOfClass = DataSource.FindDataOfClass<GvGCoolTimeRankingContent>(root, (GvGCoolTimeRankingContent) null);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) dataOfClass, (UnityEngine.Object) null))
              dataOfClass.Refresh();
          }
        }
      }
      else
      {
        GameUtility.SetGameObjectActive(this.mRankingRoot, this.isRankingDraw);
        List<GvGRankingGuildData> grankingViewList = GvGManager.Instance.GvGRankingViewList;
        if (grankingViewList != null)
        {
          this.mRankingList.ForEach((Action<GameObject>) (p => UnityEngine.Object.Destroy((UnityEngine.Object) p)));
          this.mRankingList.Clear();
          for (int index = 0; index < grankingViewList.Count; ++index)
          {
            GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.mRankingTemplate, this.mRankingTemplate.transform.parent);
            if (!UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
            {
              DataSource.Bind<GvGRankingGuildData>(gameObject, grankingViewList[index]);
              gameObject.SetActive(true);
              this.mRankingList.Add(gameObject);
            }
          }
        }
      }
      this.UpdateGameParameter();
    }

    public void OnChangeRankingDraw()
    {
      this.isRankingDraw = !this.isRankingDraw;
      if (GvGManager.Instance.IsGvGBattleCoolTimeFirst)
        GameUtility.SetGameObjectActive(this.mCoolTimeRankingRoot, this.isRankingDraw);
      else
        GameUtility.SetGameObjectActive(this.mRankingRoot, this.isRankingDraw);
    }

    public void UpdateGameParameter()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mRestUnitCountRoot, (UnityEngine.Object) null))
        return;
      this.mRestUnitCountRoot.SetActive(true);
      GameParameter.UpdateAll(this.mRestUnitCountRoot);
    }

    public static DateTime SetNextPhaseTime(Text phaseText, Text nextText)
    {
      DateTime dateTime = DateTime.MinValue;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) phaseText, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) nextText, (UnityEngine.Object) null))
      {
        GvGManager instance = GvGManager.Instance;
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance, (UnityEngine.Object) null))
          return dateTime;
        DateTime serverTime = TimeManager.ServerTime;
        GameUtility.SetGameObjectActive(((Component) nextText).gameObject, true);
        switch (instance.GvGStatusPhase())
        {
          case GvGManager.GvGStatus.Declaration:
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) phaseText, (UnityEngine.Object) null))
              phaseText.text = LocalizedText.Get("sys.GVG_PHASE_REMAIN_DECLARATON");
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) nextText, (UnityEngine.Object) null))
              nextText.text = instance.GvGPeriod.DeclarationEndTime.ToString();
            DateTime result1;
            DateTime.TryParse(serverTime.ToString("yyyy/MM/dd ") + instance.GvGPeriod.DeclarationEndTime, out result1);
            if (result1 < serverTime)
              result1 = result1.AddDays(1.0);
            dateTime = result1;
            break;
          case GvGManager.GvGStatus.OffenseFirstHalf:
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) phaseText, (UnityEngine.Object) null))
              phaseText.text = LocalizedText.Get("sys.GVG_PHASE_REMAIN_OFFENCEFIRSTHALF");
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) nextText, (UnityEngine.Object) null))
              nextText.text = instance.GvGPeriod.BattleCoolStartTime.ToString();
            DateTime result2;
            DateTime.TryParse(serverTime.ToString("yyyy/MM/dd ") + instance.GvGPeriod.BattleCoolStartTime, out result2);
            if (result2 < serverTime)
              result2 = result2.AddDays(1.0);
            dateTime = result2;
            break;
          case GvGManager.GvGStatus.OffenseCoolTime:
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) phaseText, (UnityEngine.Object) null))
              phaseText.text = LocalizedText.Get("sys.GVG_PHASE_REMAIN_OFFENCECOOLTIME");
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) nextText, (UnityEngine.Object) null))
              nextText.text = instance.GvGPeriod.BattleCoolEndTime.ToString();
            DateTime result3;
            DateTime.TryParse(serverTime.ToString("yyyy/MM/dd ") + instance.GvGPeriod.BattleCoolEndTime, out result3);
            if (result3 < serverTime)
              result3 = result3.AddDays(1.0);
            dateTime = result3;
            break;
          case GvGManager.GvGStatus.OffenseLatterHalf:
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) phaseText, (UnityEngine.Object) null))
              phaseText.text = LocalizedText.Get("sys.GVG_PHASE_REMAIN_OFFENCELATTERHALF");
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) nextText, (UnityEngine.Object) null))
              nextText.text = instance.GvGPeriod.BattleEndTime.ToString();
            DateTime result4;
            DateTime.TryParse(serverTime.ToString("yyyy/MM/dd ") + instance.GvGPeriod.BattleEndTime, out result4);
            if (result4 < serverTime)
              result4 = result4.AddDays(1.0);
            dateTime = result4;
            break;
          case GvGManager.GvGStatus.DeclarationCoolTime:
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) phaseText, (UnityEngine.Object) null))
              phaseText.text = LocalizedText.Get("sys.GVG_PHASE_START_OFFENCE");
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) nextText, (UnityEngine.Object) null))
              nextText.text = instance.GvGPeriod.BattleStartTime.ToString();
            DateTime result5;
            DateTime.TryParse(serverTime.ToString("yyyy/MM/dd ") + instance.GvGPeriod.BattleStartTime, out result5);
            if (result5 < serverTime)
              result5 = result5.AddDays(1.0);
            dateTime = result5;
            break;
          case GvGManager.GvGStatus.AggregationTime:
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) phaseText, (UnityEngine.Object) null))
              phaseText.text = LocalizedText.Get("sys.GVG_PHASE_START_DECLARATON");
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) nextText, (UnityEngine.Object) null))
              nextText.text = instance.GvGPeriod.DeclarationStartTime.ToString();
            DateTime result6;
            DateTime.TryParse(serverTime.ToString("yyyy/MM/dd ") + instance.GvGPeriod.DeclarationStartTime, out result6);
            if (result6 < serverTime)
              result6 = result6.AddDays(1.0);
            dateTime = result6;
            break;
          case GvGManager.GvGStatus.Finished:
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) phaseText, (UnityEngine.Object) null))
              phaseText.text = LocalizedText.Get("sys.GVG_PHASE_FINISH");
            GameUtility.SetGameObjectActive(((Component) nextText).gameObject, false);
            break;
        }
      }
      return dateTime;
    }

    private void Update()
    {
      this.mElapsedTime = this.mElapsedTime.AddSeconds((double) Time.unscaledDeltaTime);
      TimeSpan timeSpan = this.mEndTime - this.mElapsedTime;
      if (timeSpan <= TimeSpan.Zero)
        timeSpan = TimeSpan.Zero;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mNextText, (UnityEngine.Object) null))
        this.mNextText.text = string.Format(LocalizedText.Get("sys.GVG_PHASE_TIME"), (object) timeSpan.Hours, (object) timeSpan.Minutes, (object) timeSpan.Seconds);
      if (timeSpan <= TimeSpan.Zero)
        this.Refresh();
      GameUtility.SetButtonIntaractable(this.mUpdateButton, !GvGManager.Instance.CanRefresh);
    }

    public void Activated(int pinID)
    {
    }
  }
}
