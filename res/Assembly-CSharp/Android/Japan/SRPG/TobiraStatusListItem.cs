// Decompiled with JetBrains decompiler
// Type: SRPG.TobiraStatusListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class TobiraStatusListItem : MonoBehaviour
  {
    [SerializeField]
    private Text m_TextTobiraName;
    [SerializeField]
    private ImageArray m_IconTobira;
    [SerializeField]
    private GameObject m_ObjectDetail;
    [SerializeField]
    private GameObject m_ObjectLock;
    [SerializeField]
    private GameObject m_ObjectCommingSoon;
    [SerializeField]
    private TobiraLearnSkill m_TobiraLearnSkillTemplate;
    [SerializeField]
    private RectTransform m_TobiraLearnSkillParent;
    [SerializeField]
    private StatusList m_StatusList;
    [SerializeField]
    private GameObject m_TobiraLvMaxObject;
    private TobiraParam.Category m_Category;

    private void Start()
    {
      GameUtility.SetGameObjectActive((Component) this.m_TobiraLearnSkillTemplate, false);
    }

    public void Setup(TobiraData tobiraData)
    {
      if (tobiraData == null)
      {
        DebugUtility.LogError("tobiraDataがnullの時はSetup(TobiraParam param)を使用してください");
      }
      else
      {
        this.m_Category = tobiraData.Param.TobiraCategory;
        this.m_TextTobiraName.text = TobiraParam.GetCategoryName(this.m_Category);
        this.m_IconTobira.ImageIndex = (int) this.m_Category;
        if (tobiraData.IsLearnedLeaderSkill)
        {
          TobiraLearnSkill listItem = this.CreateListItem();
          SkillData skill = new SkillData();
          skill.Setup(tobiraData.LearnedLeaderSkillIname, 1, 1, (MasterParam) null);
          listItem.Setup(skill);
        }
        List<AbilityData> newAbilitys = new List<AbilityData>();
        List<AbilityData> oldAbilitys = new List<AbilityData>();
        TobiraUtility.TryCraeteAbilityData(tobiraData.Param, tobiraData.Lv, newAbilitys, oldAbilitys, false);
        for (int index = 0; index < newAbilitys.Count; ++index)
          this.CreateListItem().Setup(newAbilitys[index]);
        BaseStatus add = new BaseStatus();
        BaseStatus scale = new BaseStatus();
        TobiraUtility.CalcTobiraParameter(tobiraData.Param.UnitIname, this.m_Category, tobiraData.Lv, ref add, ref scale);
        this.m_StatusList.SetValues(add, scale, false);
        GameUtility.SetGameObjectActive(this.m_ObjectDetail, true);
        GameUtility.SetGameObjectActive(this.m_ObjectLock, false);
        GameUtility.SetGameObjectActive(this.m_ObjectCommingSoon, false);
      }
    }

    public void Setup(TobiraParam param)
    {
      this.m_Category = param.TobiraCategory;
      this.m_TextTobiraName.text = TobiraParam.GetCategoryName(this.m_Category);
      this.m_IconTobira.ImageIndex = (int) this.m_Category;
      if (param.Enable)
      {
        BaseStatus add = new BaseStatus();
        BaseStatus scale = new BaseStatus();
        TobiraUtility.CalcTobiraParameter(param.UnitIname, this.m_Category, 1, ref add, ref scale);
        this.m_StatusList.SetValues(add, scale, true);
      }
      GameUtility.SetGameObjectActive(this.m_ObjectDetail, param.Enable);
      GameUtility.SetGameObjectActive(this.m_ObjectLock, param.Enable);
      GameUtility.SetGameObjectActive(this.m_ObjectCommingSoon, !param.Enable);
    }

    public void SetTobiraLvIsMax(bool isMax)
    {
      GameUtility.SetGameObjectActive(this.m_TobiraLvMaxObject, isMax);
    }

    private TobiraLearnSkill CreateListItem()
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.m_TobiraLearnSkillTemplate.gameObject);
      TobiraLearnSkill component = gameObject.GetComponent<TobiraLearnSkill>();
      gameObject.transform.SetParent((Transform) this.m_TobiraLearnSkillParent, false);
      gameObject.SetActive(true);
      return component;
    }
  }
}
