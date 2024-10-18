// Decompiled with JetBrains decompiler
// Type: SRPG.AbilityPowerUpResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class AbilityPowerUpResult : MonoBehaviour
  {
    private List<AbilityPowerUpResultContent.Param> paramList = new List<AbilityPowerUpResultContent.Param>();
    private List<AbilityPowerUpResultContent> contentList = new List<AbilityPowerUpResultContent>();
    [SerializeField]
    private AbilityPowerUpResultContent contentBase;
    [SerializeField]
    private Transform contanteParent;
    [SerializeField]
    private int onePageContentsMax;

    public bool IsEnd
    {
      get
      {
        return this.paramList.Count == 0;
      }
    }

    private void Start()
    {
      if (!((UnityEngine.Object) this.contentBase != (UnityEngine.Object) null) || !this.contentBase.gameObject.activeInHierarchy)
        return;
      this.contentBase.gameObject.SetActive(false);
    }

    public void SetData(ConceptCardData currentCardData, int prevAwakeCount)
    {
      List<ConceptCardEquipEffect> abilities = currentCardData.GetAbilities();
      int count = abilities.Count;
      for (int index = 0; index < count; ++index)
      {
        AbilityData ability = abilities[index].Ability;
        this.paramList.Add(new AbilityPowerUpResultContent.Param()
        {
          data = ability.Param
        });
      }
    }

    public void ApplyContent()
    {
      int count = this.paramList.Count;
      int num = count >= this.onePageContentsMax ? this.onePageContentsMax : count;
      if (this.contentList.Count == 0)
      {
        for (int index = 0; index < num; ++index)
        {
          AbilityPowerUpResultContent powerUpResultContent = UnityEngine.Object.Instantiate<AbilityPowerUpResultContent>(this.contentBase);
          powerUpResultContent.transform.SetParent(this.contanteParent, false);
          this.contentList.Add(powerUpResultContent);
        }
      }
      else if (num > count)
      {
        for (int index = count - 1; index < num; ++index)
          this.contentList[index].gameObject.SetActive(false);
      }
      for (int index = 0; index < num; ++index)
        this.contentList[index].SetData(this.paramList[index]);
      for (int index = 0; index < num; ++index)
        this.paramList.RemoveAt(0);
    }
  }
}
