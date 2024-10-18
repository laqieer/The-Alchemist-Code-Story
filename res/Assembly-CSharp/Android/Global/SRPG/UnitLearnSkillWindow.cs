// Decompiled with JetBrains decompiler
// Type: SRPG.UnitLearnSkillWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

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
      if (!((UnityEngine.Object) this.SkillTemplate != (UnityEngine.Object) null))
        return;
      this.SkillTemplate.SetActive(false);
    }

    private void Start()
    {
      this.Refresh();
    }

    private void Refresh()
    {
      if (this.Skills == null)
        return;
      for (int index = 0; index < this.Skills.Count; ++index)
      {
        SkillParam skill = this.Skills[index];
        if (skill != null)
        {
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.SkillTemplate);
          DataSource.Bind<SkillParam>(gameObject, skill);
          gameObject.transform.SetParent(this.SkillParent, false);
          gameObject.SetActive(true);
        }
      }
    }
  }
}
