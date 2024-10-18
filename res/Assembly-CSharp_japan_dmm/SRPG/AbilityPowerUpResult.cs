// Decompiled with JetBrains decompiler
// Type: SRPG.AbilityPowerUpResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class AbilityPowerUpResult : MonoBehaviour
  {
    [SerializeField]
    private AbilityPowerUpResultContent contentBase;
    [SerializeField]
    private Transform contanteParent;
    [SerializeField]
    private int onePageContentsMax;
    private List<AbilityPowerUpResultContent.Param> paramList = new List<AbilityPowerUpResultContent.Param>();
    private List<AbilityPowerUpResultContent> contentList = new List<AbilityPowerUpResultContent>();

    public bool IsEnd => this.paramList.Count == 0;

    private void Start()
    {
      if (!Object.op_Inequality((Object) this.contentBase, (Object) null) || !((Component) this.contentBase).gameObject.activeInHierarchy)
        return;
      ((Component) this.contentBase).gameObject.SetActive(false);
    }

    public void SetData(ConceptCardData currentCardData, int prevAwakeCount)
    {
      List<AbilityParam> lerningAbilities = currentCardData.GetMaxLerningAbilities();
      int count = lerningAbilities.Count;
      for (int index = 0; index < count; ++index)
        this.paramList.Add(new AbilityPowerUpResultContent.Param()
        {
          data = lerningAbilities[index]
        });
    }

    public void ApplyContent()
    {
      int count = this.paramList.Count;
      int num = count >= this.onePageContentsMax ? this.onePageContentsMax : count;
      if (this.contentList.Count == 0)
      {
        for (int index = 0; index < num; ++index)
        {
          AbilityPowerUpResultContent powerUpResultContent = Object.Instantiate<AbilityPowerUpResultContent>(this.contentBase);
          ((Component) powerUpResultContent).transform.SetParent(this.contanteParent, false);
          this.contentList.Add(powerUpResultContent);
        }
      }
      else if (num > count)
      {
        for (int index = count - 1; index < num; ++index)
          ((Component) this.contentList[index]).gameObject.SetActive(false);
      }
      for (int index = 0; index < num; ++index)
        this.contentList[index].SetData(this.paramList[index]);
      for (int index = 0; index < num; ++index)
        this.paramList.RemoveAt(0);
    }
  }
}
