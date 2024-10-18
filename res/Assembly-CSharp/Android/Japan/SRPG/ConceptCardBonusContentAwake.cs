// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardBonusContentAwake
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

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

    public bool IsEnable
    {
      get
      {
        return this.mIsEnable;
      }
    }

    public void Setup(ConceptCardEffectsParam[] effect_params, int awake_count, int awake_count_cap, bool is_enable)
    {
      if ((UnityEngine.Object) this.mSkillParamTemplate == (UnityEngine.Object) null)
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
            GameObject root = UnityEngine.Object.Instantiate<GameObject>(this.mSkillParamTemplate);
            root.transform.SetParent(parent, false);
            root.GetComponentInChildren<StatusList>().SetValues(total_add, total_scale, false);
            if ((UnityEngine.Object) this.mAwakeIconImageArray != (UnityEngine.Object) null)
              this.mAwakeIconImageArray.ImageIndex = awake_count - 1;
            DataSource.Bind<SkillParam>(root, skillParam, false);
            DataSource.Bind<bool>(this.gameObject, is_enable, false);
            GameParameter.UpdateAll(root);
            stringList.Add(skillParam.iname);
            ++this.mCreatedCount;
          }
        }
      }
      if ((UnityEngine.Object) this.mAwakeIconBgArray != (UnityEngine.Object) null)
        this.mAwakeIconBgArray.ImageIndex = !is_enable ? 1 : 0;
      this.mSkillParamTemplate.SetActive(false);
      this.gameObject.SetActive(this.mCreatedCount > 0);
    }

    public void SetProgressLineImage(bool is_enable, bool is_active = true)
    {
      this.mProgressLine.ImageIndex = !is_enable ? 1 : 0;
      this.mProgressLine.gameObject.SetActive(is_active);
    }
  }
}
