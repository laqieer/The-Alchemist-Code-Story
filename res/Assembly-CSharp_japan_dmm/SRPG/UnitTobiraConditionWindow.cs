// Decompiled with JetBrains decompiler
// Type: SRPG.UnitTobiraConditionWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class UnitTobiraConditionWindow : MonoBehaviour
  {
    private static readonly string HAS_UNIT_OBJECT_NAME = "enable";
    private static readonly string NOT_HAS_UNIT_OBJECT_NAME = "none_unit";
    private static readonly string NOT_OPEN_JOB_OBJECT_NAME = "none_job";
    private static readonly string NOT_OPEN_TOBIRA_OBJECT_NAME = "none_tobira";
    private static readonly string VALUE_TEXT_NAME = "txt_current";
    private static readonly string VALUE_MAX_TEXT_NAME = "txt_max";
    private static readonly string JOB_LEVEL_TEXT_PAREANT_NAME = "level";
    private static readonly string JOB_LEVEL_TEXT_NAME = "txt_current";
    private static readonly string JOB_LEVEL_MAX_TEXT_NAME = "txt_max";
    private static readonly string STRING_FORMAT_CONDS_CLEAR = "sys.TOBIRA_CONDITIONS_TEXT_COLOR_CLEAR";
    private static readonly string STRING_FORMAT_CONDS_NOT_CLEAR = "sys.TOBIRA_CONDITIONS_TEXT_COLOR_NOT_CLEAR";
    [SerializeField]
    private Transform mConditionObjectParent;
    [SerializeField]
    private GameObject mConditionObjectTemplate;
    [SerializeField]
    private GameObject mConditionLayoutParent;
    [SerializeField]
    private GameObject mTitleTextObject;
    [SerializeField]
    private Transform mTitleTextObjectParent;
    [SerializeField]
    private ImageArray mIconImageArray;
    [HeaderBar("▼条件表示用のテンプレート")]
    [SerializeField]
    private GameObject mLayout_UnitLevel;
    [SerializeField]
    private GameObject mLayout_UnitAwake;
    [SerializeField]
    private GameObject mLayout_JobLevel;
    [SerializeField]
    private GameObject mLayout_TobiraLevel;
    [SerializeField]
    private GameObject mLayout_TobiraOpen;
    [SerializeField]
    private GameObject mLayout_None;
    private UnitTobiraConditionWindow.ViewParam mViewParam;
    private GameObject[] mLayoutObjects;

    private void Start()
    {
      if (Object.op_Equality((Object) this.mConditionObjectTemplate, (Object) null))
        return;
      this.mConditionObjectTemplate.SetActive(false);
      UnitData unitDataByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID((long) GlobalVars.SelectedUnitUniqueID);
      TobiraParam.Category unitTobiraCategory = (TobiraParam.Category) GlobalVars.PreBattleUnitTobiraCategory;
      if (unitDataByUniqueId == null || unitTobiraCategory <= TobiraParam.Category.START || unitTobiraCategory >= TobiraParam.Category.MAX)
        return;
      this.mLayoutObjects = new GameObject[6];
      this.mLayoutObjects[1] = this.mLayout_UnitLevel;
      this.mLayoutObjects[2] = this.mLayout_UnitAwake;
      this.mLayoutObjects[3] = this.mLayout_JobLevel;
      this.mLayoutObjects[4] = this.mLayout_TobiraLevel;
      this.mLayoutObjects[5] = this.mLayout_TobiraOpen;
      this.mLayoutObjects[0] = this.mLayout_None;
      TobiraConditionParam[] conditionsForUnit = MonoSingleton<GameManager>.Instance.MasterParam.GetTobiraConditionsForUnit(unitDataByUniqueId.UnitID, unitTobiraCategory);
      List<ConditionsResult> conditionsResultList = TobiraUtility.TobiraConditionsCheck(unitDataByUniqueId, conditionsForUnit);
      for (int index = 0; index < conditionsResultList.Count; ++index)
        this.CreateLayout(conditionsResultList[index]);
    }

    private void CreateLayout(ConditionsResult conds)
    {
      if (conds.isConditionsQuestClear)
        return;
      if (conds.isConditionsUnitLv)
      {
        ConditionsResult_UnitLv conditionsResultUnitLv = (ConditionsResult_UnitLv) conds;
        this.mViewParam = new UnitTobiraConditionWindow.ViewParam();
        this.mViewParam.type = UnitTobiraConditionWindow.eLayoutType.UnitLevel;
        this.mViewParam.title = conds.text;
        this.mViewParam.value_max = conditionsResultUnitLv.targetValue.ToString();
        this.mViewParam.is_clear = conditionsResultUnitLv.isClear;
        this.mViewParam.has_unit = conditionsResultUnitLv.hasUnitData;
        this.mViewParam.unit_data = conditionsResultUnitLv.unitData;
        if (conditionsResultUnitLv.hasUnitData)
          this.mViewParam.value = conditionsResultUnitLv.currentValue.ToString();
      }
      if (conds.isConditionsAwake)
      {
        ConditionsResult_AwakeLv conditionsResultAwakeLv = (ConditionsResult_AwakeLv) conds;
        this.mViewParam = new UnitTobiraConditionWindow.ViewParam();
        this.mViewParam.type = UnitTobiraConditionWindow.eLayoutType.UnitAwake;
        this.mViewParam.title = conditionsResultAwakeLv.text;
        this.mViewParam.value_max = conditionsResultAwakeLv.targetValue.ToString();
        this.mViewParam.is_clear = conditionsResultAwakeLv.isClear;
        this.mViewParam.has_unit = conditionsResultAwakeLv.hasUnitData;
        this.mViewParam.unit_data = conditionsResultAwakeLv.unitData;
        if (conditionsResultAwakeLv.hasUnitData)
          this.mViewParam.value = conditionsResultAwakeLv.currentValue.ToString();
      }
      if (conds.isConditionsJobLv)
      {
        ConditionsResult_JobLv conditionsResultJobLv = (ConditionsResult_JobLv) conds;
        this.mViewParam = new UnitTobiraConditionWindow.ViewParam();
        this.mViewParam.type = UnitTobiraConditionWindow.eLayoutType.JobLevel;
        this.mViewParam.title = conditionsResultJobLv.text;
        this.mViewParam.value_max = conditionsResultJobLv.targetValue.ToString();
        this.mViewParam.is_clear = conditionsResultJobLv.isClear;
        this.mViewParam.has_unit = conditionsResultJobLv.hasUnitData;
        this.mViewParam.job_param = conditionsResultJobLv.CurrentJobParam;
        this.mViewParam.value = conditionsResultJobLv.currentValue.ToString();
      }
      if (conds.isConditionsTobiraLv)
      {
        ConditionsResult_TobiraLv conditionsResultTobiraLv = (ConditionsResult_TobiraLv) conds;
        this.mViewParam = new UnitTobiraConditionWindow.ViewParam();
        this.mViewParam.type = UnitTobiraConditionWindow.eLayoutType.TobiraLevel;
        this.mViewParam.title = conditionsResultTobiraLv.text;
        this.mViewParam.value_max = (conditionsResultTobiraLv.targetValue - 1).ToString();
        this.mViewParam.is_clear = conditionsResultTobiraLv.isClear;
        this.mViewParam.has_unit = conditionsResultTobiraLv.hasUnitData;
        this.mViewParam.tobira_data = conditionsResultTobiraLv.mTobiraData;
        this.mViewParam.value = Mathf.Max(0, conditionsResultTobiraLv.currentValue - 1).ToString();
      }
      if (conds.isConditionsTobiraNoConditions)
      {
        ConditionsResult_TobiraNoConditions tobiraNoConditions = (ConditionsResult_TobiraNoConditions) conds;
        this.mViewParam = new UnitTobiraConditionWindow.ViewParam();
        this.mViewParam.type = UnitTobiraConditionWindow.eLayoutType.None;
        this.mViewParam.title = tobiraNoConditions.text;
        this.mViewParam.is_clear = tobiraNoConditions.isClear;
      }
      this.CreateLayoutObject(this.mViewParam);
    }

    private void CreateLayoutObject(UnitTobiraConditionWindow.ViewParam view_param)
    {
      if (view_param == null || Object.op_Equality((Object) this.mConditionObjectTemplate, (Object) null) || Object.op_Equality((Object) this.mConditionObjectParent, (Object) null) || Object.op_Equality((Object) this.mConditionLayoutParent, (Object) null))
        return;
      GameObject gameObject = Object.Instantiate<GameObject>(this.mConditionObjectTemplate);
      gameObject.transform.SetParent(this.mConditionObjectParent, false);
      gameObject.SetActive(true);
      Transform transform1 = (Transform) null;
      Transform transform2 = (Transform) null;
      foreach (Transform transform3 in gameObject.transform)
      {
        if (((Object) transform3).name == ((Object) this.mConditionLayoutParent).name)
          transform2 = transform3;
        if (((Object) transform3).name == ((Object) this.mTitleTextObjectParent).name)
          transform1 = transform3;
      }
      if (Object.op_Equality((Object) transform2, (Object) null))
        return;
      string format = LocalizedText.Get(!view_param.is_clear ? UnitTobiraConditionWindow.STRING_FORMAT_CONDS_NOT_CLEAR : UnitTobiraConditionWindow.STRING_FORMAT_CONDS_CLEAR);
      foreach (Transform transform4 in transform2)
      {
        bool flag = ((Object) transform4).name == ((Object) this.mLayoutObjects[(int) view_param.type]).name;
        ((Component) transform4).gameObject.SetActive(flag);
        if (flag)
        {
          foreach (Transform transform5 in transform4)
          {
            ((Component) transform5).gameObject.SetActive(false);
            if (((Object) transform5).name == UnitTobiraConditionWindow.HAS_UNIT_OBJECT_NAME)
            {
              ((Component) transform5).gameObject.SetActive(view_param.has_unit);
              foreach (Transform transform6 in transform5)
              {
                if (((Object) transform6).name == UnitTobiraConditionWindow.VALUE_TEXT_NAME)
                  ((Component) transform6).GetComponent<Text>().text = string.Format(format, (object) view_param.value);
                if (((Object) transform6).name == UnitTobiraConditionWindow.VALUE_MAX_TEXT_NAME)
                  ((Component) transform6).GetComponent<Text>().text = view_param.value_max;
                if (((Object) transform6).name == UnitTobiraConditionWindow.JOB_LEVEL_TEXT_PAREANT_NAME)
                {
                  foreach (Transform transform7 in transform6)
                  {
                    if (((Object) transform7).name == UnitTobiraConditionWindow.JOB_LEVEL_TEXT_NAME)
                      ((Component) transform7).GetComponent<Text>().text = string.Format(format, (object) view_param.value);
                    if (((Object) transform7).name == UnitTobiraConditionWindow.JOB_LEVEL_MAX_TEXT_NAME)
                      ((Component) transform7).GetComponent<Text>().text = view_param.value_max;
                  }
                }
              }
              if (view_param.type == UnitTobiraConditionWindow.eLayoutType.JobLevel && int.Parse(view_param.value) <= 0)
                ((Component) transform5).gameObject.SetActive(false);
              if (view_param.type == UnitTobiraConditionWindow.eLayoutType.TobiraLevel && view_param.tobira_data == null)
                ((Component) transform5).gameObject.SetActive(false);
            }
            if (((Object) transform5).name == UnitTobiraConditionWindow.NOT_HAS_UNIT_OBJECT_NAME)
              ((Component) transform5).gameObject.SetActive(!view_param.has_unit);
            if (((Object) transform5).name == UnitTobiraConditionWindow.NOT_OPEN_JOB_OBJECT_NAME && view_param.has_unit)
              ((Component) transform5).gameObject.SetActive(int.Parse(view_param.value) <= 0);
            if (((Object) transform5).name == UnitTobiraConditionWindow.NOT_OPEN_TOBIRA_OBJECT_NAME && view_param.has_unit)
              ((Component) transform5).gameObject.SetActive(view_param.tobira_data == null);
          }
          if (view_param.unit_data != null)
            DataSource.Bind<UnitData>(((Component) transform4).gameObject, view_param.unit_data);
          if (view_param.job_param != null)
            DataSource.Bind<JobParam>(((Component) transform4).gameObject, view_param.job_param);
          if (view_param.tobira_data != null)
            DataSource.Bind<TobiraData>(((Component) transform4).gameObject, view_param.tobira_data);
          GameParameter.UpdateAll(((Component) transform4).gameObject);
        }
      }
      foreach (Transform transform8 in transform1)
      {
        if (((Object) transform8).name == ((Object) this.mTitleTextObject).name)
        {
          Text component = ((Component) transform8).GetComponent<Text>();
          if (Object.op_Inequality((Object) component, (Object) null))
            component.text = view_param.title;
        }
        if (((Object) transform8).name == ((Object) this.mIconImageArray).name)
        {
          int num = !view_param.is_clear ? 0 : 1;
          ImageArray component = ((Component) transform8).GetComponent<ImageArray>();
          if (Object.op_Inequality((Object) component, (Object) null))
            component.ImageIndex = num;
        }
      }
    }

    private enum eLayoutType
    {
      None,
      UnitLevel,
      UnitAwake,
      JobLevel,
      TobiraLevel,
      TobiraOpen,
      MAX,
    }

    private class ViewParam
    {
      public UnitTobiraConditionWindow.eLayoutType type;
      public string title;
      public string value;
      public string value_max;
      public bool is_clear;
      public bool has_unit;
      public UnitData unit_data;
      public JobParam job_param;
      public TobiraData tobira_data;
    }
  }
}
