// Decompiled with JetBrains decompiler
// Type: SRPG.GachaResultUnitDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class GachaResultUnitDetail : MonoBehaviour
  {
    public string PreviewParentID = "GACHARESULTUNITPREVIEW";
    public string PreviewBaseID = "GACHARESULTUNITPREVIEWBASE";
    private string BGUnitImageID = "GACHA_UNIT_IMG";
    public GameObject UnitInfo;
    public GameObject JobInfo;
    public GameObject LeaderSkillInfo;
    public GameObject AbilityTemplate;
    public Text UnitLv;
    public Text UnitLvMax;
    public Text Status_HP;
    public Text Status_Atk;
    public Text Status_Def;
    public Text Status_Mag;
    public Text Status_Mnd;
    public Text Status_Rec;
    public Text Status_Dex;
    public Text Status_Speed;
    public Text Status_Cri;
    public Text Status_Luck;
    public Text Status_Renkei;
    [SerializeField]
    private Text Status_Move;
    [SerializeField]
    private Text Status_Jump;
    [SerializeField]
    private GameObject JobData01;
    [SerializeField]
    private GameObject JobData02;
    [SerializeField]
    private GameObject JobData03;
    private UnitData mCurrentUnit;
    private int mCurrentUnitIndex;
    private Text[] mStatusParamSlots;
    private Transform mPreviewParent;
    private GameObject mPreviewBase;
    private UnitPreview mCurrentPreview;
    private List<UnitPreview> mPreviewControllers = new List<UnitPreview>();
    private RawImage mBGUnitImage;
    public Button LeaderSkillDetailButton;
    private GameObject mLeaderSkillDetail;
    [SerializeField]
    private GameObject Prefab_LeaderSkillDetail;
    [SerializeField]
    private string Prefab_LeaderSkillDetailPath = "UI/";
    [SerializeField]
    private Text LeaderSkillName;
    private bool mDesiredPreviewVisibility;
    private bool mUpdatePreviewVisibility;
    private float mBGUnitImgAlphaStart;
    private float mBGUnitImgAlphaEnd;
    private float mBGUnitImgFadeTime;
    private float mBGUnitImgFadeTimeMax;
    private List<GameObject> mAbilits = new List<GameObject>();

    [DebuggerHidden]
    private IEnumerator ShowLeaderSkillDetail(string _path)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GachaResultUnitDetail.\u003CShowLeaderSkillDetail\u003Ec__Iterator0()
      {
        _path = _path,
        \u0024this = this
      };
    }

    private void OpenLeaderSkillDetail()
    {
      if (!UnityEngine.Object.op_Equality((UnityEngine.Object) this.mLeaderSkillDetail, (UnityEngine.Object) null) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Prefab_LeaderSkillDetail, (UnityEngine.Object) null))
        return;
      this.mLeaderSkillDetail = UnityEngine.Object.Instantiate<GameObject>(this.Prefab_LeaderSkillDetail);
      DataSource.Bind<UnitData>(this.mLeaderSkillDetail, this.mCurrentUnit);
      Canvas component = this.mLeaderSkillDetail.GetComponent<Canvas>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      component.sortingOrder = 10;
    }

    private void Start()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.LeaderSkillDetailButton, (UnityEngine.Object) null))
        return;
      // ISSUE: method pointer
      ((UnityEvent) this.LeaderSkillDetailButton.onClick).AddListener(new UnityAction((object) this, __methodptr(OpenLeaderSkillDetail)));
    }

    private void Refresh()
    {
      this.mStatusParamSlots = new Text[StatusParam.MAX_STATUS];
      this.mStatusParamSlots[0] = this.Status_HP;
      this.mStatusParamSlots[3] = this.Status_Atk;
      this.mStatusParamSlots[4] = this.Status_Def;
      this.mStatusParamSlots[5] = this.Status_Mag;
      this.mStatusParamSlots[6] = this.Status_Mnd;
      this.mStatusParamSlots[7] = this.Status_Rec;
      this.mStatusParamSlots[8] = this.Status_Dex;
      this.mStatusParamSlots[9] = this.Status_Speed;
      this.mStatusParamSlots[10] = this.Status_Cri;
      this.mStatusParamSlots[11] = this.Status_Luck;
      if (this.mCurrentUnit == null)
        return;
      if (!string.IsNullOrEmpty(this.PreviewParentID))
      {
        this.mPreviewParent = GameObjectID.FindGameObject<Transform>(this.PreviewParentID);
        ((Component) this.mPreviewParent).transform.position = new Vector3(-0.2f, ((Component) this.mPreviewParent).transform.position.y, ((Component) this.mPreviewParent).transform.position.z);
      }
      if (!string.IsNullOrEmpty(this.PreviewBaseID))
      {
        this.mPreviewBase = GameObjectID.FindGameObject(this.PreviewBaseID);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPreviewBase, (UnityEngine.Object) null))
        {
          GameUtility.SetLayer(this.mPreviewBase, GameUtility.LayerUI, true);
          this.mPreviewBase.transform.position = new Vector3(-0.2f, this.mPreviewBase.transform.position.y, this.mPreviewBase.transform.position.z);
          this.mPreviewBase.SetActive(false);
        }
      }
      if (!string.IsNullOrEmpty(this.BGUnitImageID))
        this.mBGUnitImage = GameObjectID.FindGameObject<RawImage>(this.BGUnitImageID);
      this.StartCoroutine(this.RefreshAsync());
    }

    private void OnEnable()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.JobData01, (UnityEngine.Object) null))
        this.JobData01.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.JobData02, (UnityEngine.Object) null))
        this.JobData02.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.JobData03, (UnityEngine.Object) null))
        this.JobData03.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.LeaderSkillDetailButton, (UnityEngine.Object) null))
        ((Selectable) this.LeaderSkillDetailButton).interactable = false;
      this.Refresh();
    }

    private void OnDisable()
    {
      this.SetPreviewVisible(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPreviewBase, (UnityEngine.Object) null))
      {
        this.mPreviewBase.transform.position = new Vector3(0.2f, this.mPreviewBase.transform.position.y, this.mPreviewBase.transform.position.z);
        this.mPreviewBase.SetActive(false);
      }
      this.FadeUnitImage(0.0f, 0.0f, 0.0f);
    }

    [DebuggerHidden]
    private IEnumerator RefreshAsync(bool immediate = false)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GachaResultUnitDetail.\u003CRefreshAsync\u003Ec__Iterator1()
      {
        immediate = immediate,
        \u0024this = this
      };
    }

    private void Update()
    {
      if (this.mUpdatePreviewVisibility && this.mDesiredPreviewVisibility && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mCurrentPreview, (UnityEngine.Object) null) && !this.mCurrentPreview.IsLoading)
      {
        GameUtility.SetLayer((Component) this.mCurrentPreview, GameUtility.LayerUI, true);
        this.mUpdatePreviewVisibility = false;
      }
      if ((double) this.mBGUnitImgFadeTime >= (double) this.mBGUnitImgFadeTimeMax || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBGUnitImage, (UnityEngine.Object) null))
        return;
      this.mBGUnitImgFadeTime += Time.unscaledDeltaTime;
      float num = Mathf.Clamp01(this.mBGUnitImgFadeTime / this.mBGUnitImgFadeTimeMax);
      this.SetUnitImageAlpha(Mathf.Lerp(this.mBGUnitImgAlphaStart, this.mBGUnitImgAlphaEnd, num));
      if ((double) num < 1.0)
        return;
      this.mBGUnitImgFadeTime = 0.0f;
      this.mBGUnitImgFadeTimeMax = 0.0f;
    }

    private void OnDestroy()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mCurrentPreview, (UnityEngine.Object) null))
      {
        GameUtility.DestroyGameObject((Component) this.mCurrentPreview);
        this.mCurrentPreview = (UnitPreview) null;
      }
      GameUtility.DestroyGameObjects<UnitPreview>(this.mPreviewControllers);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mLeaderSkillDetail, (UnityEngine.Object) null))
        UnityEngine.Object.Destroy((UnityEngine.Object) this.mLeaderSkillDetail.gameObject);
      GameUtility.DestroyGameObjects(this.mAbilits);
    }

    public void Setup(int index)
    {
      UnitParam unit = GachaResultData.drops[index].unit;
      if (unit == null)
        return;
      this.Setup(this.CreateUnitData(unit));
    }

    public void Setup(UnitData _data) => this.mCurrentUnit = _data;

    private void SetPreviewVisible(bool visible)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mCurrentPreview, (UnityEngine.Object) null))
        return;
      this.mDesiredPreviewVisibility = visible;
      if (!visible)
      {
        ((Component) this.mPreviewParent).transform.position = new Vector3(0.2f, ((Component) this.mPreviewParent).transform.position.y, ((Component) this.mPreviewParent).transform.position.z);
        GameUtility.SetLayer((Component) this.mCurrentPreview, GameUtility.LayerHidden, true);
      }
      else
        this.mUpdatePreviewVisibility = true;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPreviewBase, (UnityEngine.Object) null) || this.mPreviewBase.activeSelf || !visible)
        return;
      this.mPreviewBase.SetActive(true);
    }

    private void ReloadPreviewModels()
    {
      if (this.mCurrentUnit == null || UnityEngine.Object.op_Equality((UnityEngine.Object) this.mPreviewParent, (UnityEngine.Object) null))
        return;
      GameUtility.DestroyGameObjects<UnitPreview>(this.mPreviewControllers);
      this.mPreviewControllers.Clear();
      this.mCurrentPreview = (UnitPreview) null;
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mCurrentPreview))
        return;
      GameObject gameObject = new GameObject("Preview", new System.Type[1]
      {
        typeof (UnitPreview)
      });
      this.mCurrentPreview = gameObject.GetComponent<UnitPreview>();
      this.mCurrentPreview.DefaultLayer = GameUtility.LayerHidden;
      this.mCurrentPreview.SetupUnit(this.mCurrentUnit);
      gameObject.transform.SetParent(this.mPreviewParent, false);
      this.mPreviewControllers.Add(this.mCurrentPreview);
    }

    private void RefreshStatus()
    {
      DataSource.Bind<UnitData>(this.UnitInfo, this.mCurrentUnit);
      RarityParam rarityParam = MonoSingleton<GameManager>.Instance.MasterParam.GetRarityParam(this.mCurrentUnit.Rarity);
      this.UnitLv.text = "1";
      this.UnitLvMax.text = rarityParam.UnitLvCap.ToString();
      for (int type = 0; type < StatusParam.MAX_STATUS; ++type)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mStatusParamSlots[type], (UnityEngine.Object) null))
          this.mStatusParamSlots[type].text = this.mCurrentUnit.Status.param[(StatusTypes) type].ToString();
      }
      this.Status_Renkei.text = this.mCurrentUnit.GetCombination().ToString();
      JobData jobData = this.mCurrentUnit.GetJobData(0);
      this.Status_Move.text = jobData.Param.mov.ToString();
      this.Status_Jump.text = jobData.Param.jmp.ToString();
      GameParameter.UpdateAll(this.UnitInfo);
    }

    private void RefreshLeaderSkillInfo()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.LeaderSkillInfo, (UnityEngine.Object) null))
        return;
      if (this.mCurrentUnit.LeaderSkill != null)
      {
        ((Selectable) this.LeaderSkillDetailButton).interactable = true;
        this.LeaderSkillName.text = this.mCurrentUnit.LeaderSkill.Name;
      }
      else
        this.LeaderSkillName.text = LocalizedText.Get("sys.UNIT_LEADERSKILL_NOTHAVE_MESSAGE");
    }

    private void RefreshJobInfo()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.JobInfo, (UnityEngine.Object) null))
        return;
      JobData[] jobs = this.mCurrentUnit.Jobs;
      DataSource.Bind<JobParam>(this.JobData01, jobs[0].Param);
      this.JobData01.SetActive(true);
      if (jobs.Length >= 2)
      {
        DataSource.Bind<JobParam>(this.JobData02, jobs[1].Param);
        this.JobData02.SetActive(true);
      }
      if (jobs.Length >= 3)
      {
        DataSource.Bind<JobParam>(this.JobData03, jobs[2].Param);
        this.JobData03.SetActive(true);
      }
      GameParameter.UpdateAll(this.JobInfo);
    }

    private void RefreshAbilitList()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.AbilityTemplate, (UnityEngine.Object) null))
        return;
      GameUtility.DestroyGameObjects(this.mAbilits);
      this.mAbilits.Clear();
      List<AbilityData> learnedAbilities = this.mCurrentUnit.GetAllLearnedAbilities();
      for (int index = 0; index < learnedAbilities.Count; ++index)
      {
        AbilityData data = learnedAbilities[index];
        if (this.mCurrentUnit.MapEffectAbility != data)
        {
          GameObject gameObject1 = UnityEngine.Object.Instantiate<GameObject>(this.AbilityTemplate);
          GameObject gameObject2 = ((Component) gameObject1.transform.Find("icon")).gameObject;
          ((Component) gameObject1.transform.Find("locked")).gameObject.SetActive(false);
          gameObject2.GetComponent<ImageArray>().ImageIndex = (int) data.SlotType;
          gameObject1.transform.SetParent(this.AbilityTemplate.transform.parent, false);
          DataSource.Bind<AbilityData>(gameObject1, data);
          gameObject1.SetActive(true);
          this.mAbilits.Add(gameObject1);
        }
      }
      RarityParam rarityParam = MonoSingleton<GameManager>.Instance.GetRarityParam((int) this.mCurrentUnit.UnitParam.raremax);
      for (int lv = this.mCurrentUnit.CurrentJob.Rank + 1; lv < JobParam.MAX_JOB_RANK; ++lv)
      {
        OString[] learningAbilitys = this.mCurrentUnit.CurrentJob.Param.GetLearningAbilitys(lv);
        if (learningAbilitys != null && (int) rarityParam.UnitJobLvCap >= lv)
        {
          for (int index = 0; index < learningAbilitys.Length; ++index)
          {
            string key = (string) learningAbilitys[index];
            if (!string.IsNullOrEmpty(key))
            {
              AbilityParam abilityParam = MonoSingleton<GameManager>.Instance.GetAbilityParam(key);
              GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.AbilityTemplate);
              ((Component) gameObject.transform.Find("icon")).gameObject.GetComponent<ImageArray>().ImageIndex = (int) abilityParam.slot;
              gameObject.transform.SetParent(this.AbilityTemplate.transform.parent, false);
              DataSource.Bind<AbilityParam>(gameObject, abilityParam);
              gameObject.SetActive(true);
              this.mAbilits.Add(gameObject);
            }
          }
        }
      }
      GameParameter.UpdateAll(((Component) this.AbilityTemplate.transform.parent).gameObject);
    }

    private UnitData CreateUnitData(UnitParam uparam)
    {
      UnitData unitData = new UnitData();
      Json_Unit json = new Json_Unit()
      {
        iid = 1,
        iname = uparam.iname,
        exp = 0,
        lv = 1,
        plus = 0,
        rare = 0,
        select = new Json_UnitSelectable()
      };
      json.select.job = 0L;
      json.jobs = (Json_Job[]) null;
      json.abil = (Json_MasterAbility) null;
      if (uparam.jobsets != null && uparam.jobsets.Length > 0)
      {
        List<Json_Job> jsonJobList = new List<Json_Job>(uparam.jobsets.Length);
        int num = 1;
        for (int index = 0; index < uparam.jobsets.Length; ++index)
        {
          JobSetParam jobSetParam = MonoSingleton<GameManager>.Instance.GetJobSetParam(uparam.jobsets[index]);
          if (jobSetParam != null)
            jsonJobList.Add(new Json_Job()
            {
              iid = (long) num++,
              iname = jobSetParam.job,
              rank = 0,
              equips = (Json_Equip[]) null,
              abils = (Json_Ability[]) null
            });
        }
        json.jobs = jsonJobList.ToArray();
      }
      unitData.Deserialize(json);
      unitData.SetUniqueID(1L);
      unitData.JobRankUp(0);
      return unitData;
    }

    private void FadeUnitImage(float alphastart, float alphaend, float duration)
    {
      this.mBGUnitImgAlphaStart = alphastart;
      this.mBGUnitImgAlphaEnd = alphaend;
      this.mBGUnitImgFadeTime = 0.0f;
      this.mBGUnitImgFadeTimeMax = duration;
      if ((double) duration > 0.0)
        return;
      this.SetUnitImageAlpha(this.mBGUnitImgAlphaEnd);
    }

    private void SetUnitImageAlpha(float alpha)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mBGUnitImage, (UnityEngine.Object) null))
        return;
      Color color = ((Graphic) this.mBGUnitImage).color;
      color.a = alpha;
      ((Graphic) this.mBGUnitImage).color = color;
    }

    [DebuggerHidden]
    private IEnumerator RefreshUnitImage()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GachaResultUnitDetail.\u003CRefreshUnitImage\u003Ec__Iterator2()
      {
        \u0024this = this
      };
    }
  }
}
