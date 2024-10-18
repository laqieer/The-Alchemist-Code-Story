// Decompiled with JetBrains decompiler
// Type: SRPG.UnitLearnSkillWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class UnitLearnSkillWindow : MonoBehaviour, IFlowInterface
  {
    public List<SkillParam> Skills;
    public Transform SkillParent;
    public GameObject SkillTemplate;

    public void Activated(int pinID)
    {
    }

    private void Awake()
    {
      if (!Object.op_Inequality((Object) this.SkillTemplate, (Object) null))
        return;
      this.SkillTemplate.SetActive(false);
    }

    private void Start() => this.Refresh();

    private void Refresh()
    {
      if (this.Skills == null)
        return;
      for (int index = 0; index < this.Skills.Count; ++index)
      {
        SkillParam skill = this.Skills[index];
        if (skill != null)
        {
          GameObject gameObject = Object.Instantiate<GameObject>(this.SkillTemplate);
          DataSource.Bind<SkillParam>(gameObject, skill);
          gameObject.transform.SetParent(this.SkillParent, false);
          gameObject.SetActive(true);
        }
      }
    }
  }
}
