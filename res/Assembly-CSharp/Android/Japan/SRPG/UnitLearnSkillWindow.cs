// Decompiled with JetBrains decompiler
// Type: SRPG.UnitLearnSkillWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
          DataSource.Bind<SkillParam>(gameObject, skill, false);
          gameObject.transform.SetParent(this.SkillParent, false);
          gameObject.SetActive(true);
        }
      }
    }
  }
}
