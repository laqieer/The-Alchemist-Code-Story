// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardAwakeCheckWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class ConceptCardAwakeCheckWindow : MonoBehaviour
  {
    [SerializeField]
    private RectTransform ListParent;
    [SerializeField]
    private GameObject ListItemTemplate;
    [SerializeField]
    private GameObject StarStatusParent;
    [SerializeField]
    private GameObject AfterStarStatus;
    private ConceptCardData mCurrentConceptCard;
    private int mUpCount;
    private int mCurrentAwakeCount;

    private void Start()
    {
      this.CreateItemIcon();
      this.SetupText();
    }

    private void CreateItemIcon()
    {
      if (Object.op_Equality((Object) this.ListParent, (Object) null) || Object.op_Equality((Object) this.ListItemTemplate, (Object) null))
        return;
      this.ListItemTemplate.SetActive(false);
      ConceptCardManager instance = ConceptCardManager.Instance;
      if (Object.op_Equality((Object) instance, (Object) null))
        return;
      this.mCurrentConceptCard = instance.SelectedConceptCardData;
      if (this.mCurrentConceptCard == null)
        return;
      Dictionary<string, int> awakeMaterialList = instance.SelectedAwakeMaterialList;
      if (awakeMaterialList == null || awakeMaterialList.Count <= 0)
        return;
      this.mCurrentAwakeCount = (int) this.mCurrentConceptCard.AwakeCount;
      this.mUpCount = 0;
      foreach (KeyValuePair<string, int> keyValuePair in awakeMaterialList)
      {
        ConceptLimitUpItemParam limitUpItemParam = this.mCurrentConceptCard.Param.GetConcepLimitUpItemParam(keyValuePair.Key);
        if (limitUpItemParam != null && limitUpItemParam.num > 0)
        {
          GameObject gameObject = Object.Instantiate<GameObject>(this.ListItemTemplate, (Transform) this.ListParent);
          gameObject.SetActive(true);
          ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(keyValuePair.Key);
          DataSource.Bind<ItemParam>(gameObject, itemParam);
          DataSource.Bind<int>(gameObject, keyValuePair.Value);
          this.mUpCount += keyValuePair.Value / limitUpItemParam.num;
        }
      }
    }

    private void SetupText()
    {
      if (Object.op_Equality((Object) this.StarStatusParent, (Object) null) || this.mCurrentConceptCard == null)
        return;
      DataSource.Bind<ConceptCardData>(this.StarStatusParent, this.mCurrentConceptCard);
      this.RefreshAwakeIcons(this.mUpCount);
    }

    private void RefreshAwakeIcons(int add_awake_count)
    {
      int num = 0;
      foreach (Transform transform1 in this.AfterStarStatus.transform)
      {
        foreach (Component component in transform1)
          component.gameObject.SetActive(false);
        string str = "off";
        if (num < this.mCurrentAwakeCount)
          str = "on";
        else if (num < this.mCurrentAwakeCount + add_awake_count)
          str = "up";
        Transform transform2 = transform1.Find(str);
        if (Object.op_Inequality((Object) transform2, (Object) null))
        {
          ((Component) transform2).gameObject.SetActive(true);
          ++num;
        }
      }
    }
  }
}
