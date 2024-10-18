// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardBonusContentAwake
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class ConceptCardBonusContentAwake : MonoBehaviour
  {
    [SerializeField]
    private GameObject mSkillParamTemplate;
    [SerializeField]
    private ImageArray mAwakeIconImageArray;
    [SerializeField]
    private ImageArray mAwakeIconBgArray;
    [SerializeField]
    private ImageArray mProgressLine;
    private int mCreatedCount;
    private bool mIsEnable;

    public bool IsEnable => this.mIsEnable;

    public void Setup(
      ConceptCardEffectsParam[] effect_params,
      int awake_count,
      int awake_count_cap,
      bool is_enable)
    {
      if (Object.op_Equality((Object) this.mSkillParamTemplate, (Object) null))
        return;
      this.mIsEnable = is_enable;
      Transform parent = this.mSkillParamTemplate.transform.parent;
      List<string> stringList = new List<string>();
      for (int index = 0; index < effect_params.Length; ++index)
      {
        if (!string.IsNullOrEmpty(effect_params[index].card_skill))
        {
          SkillParam skillParam = MonoSingleton<GameManager>.Instance.MasterParam.GetSkillParam(effect_params[index].card_skill);
          if (skillParam != null && !string.IsNullOrEmpty(effect_params[index].add_card_skill_buff_awake) && !stringList.Contains(skillParam.iname))
          {
            BaseStatus total_add = new BaseStatus();
            BaseStatus total_scale = new BaseStatus();
            effect_params[index].GetAddCardSkillBuffStatusAwake(awake_count, awake_count_cap, ref total_add, ref total_scale);
            GameObject root = Object.Instantiate<GameObject>(this.mSkillParamTemplate);
            root.transform.SetParent(parent, false);
            root.GetComponentInChildren<StatusList>().SetValues(total_add, total_scale);
            if (Object.op_Inequality((Object) this.mAwakeIconImageArray, (Object) null))
              this.mAwakeIconImageArray.ImageIndex = awake_count - 1;
            DataSource.Bind<SkillParam>(root, skillParam);
            DataSource.Bind<bool>(((Component) this).gameObject, is_enable);
            GameParameter.UpdateAll(root);
            stringList.Add(skillParam.iname);
            ++this.mCreatedCount;
          }
        }
      }
      if (Object.op_Inequality((Object) this.mAwakeIconBgArray, (Object) null))
        this.mAwakeIconBgArray.ImageIndex = !is_enable ? 1 : 0;
      this.mSkillParamTemplate.SetActive(false);
      ((Component) this).gameObject.SetActive(this.mCreatedCount > 0);
    }

    public void SetProgressLineImage(bool is_enable, bool is_active = true)
    {
      this.mProgressLine.ImageIndex = !is_enable ? 1 : 0;
      ((Component) this.mProgressLine).gameObject.SetActive(is_active);
    }
  }
}
