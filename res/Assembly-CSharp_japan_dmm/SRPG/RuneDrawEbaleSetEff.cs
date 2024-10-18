// Decompiled with JetBrains decompiler
// Type: SRPG.RuneDrawEbaleSetEff
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class RuneDrawEbaleSetEff : MonoBehaviour
  {
    [SerializeField]
    private GameObject mSetEffParentOn;
    [SerializeField]
    private GameObject mSetEffParentOff;
    [SerializeField]
    private RectTransform mSetEffListParent;
    [SerializeField]
    private GameObject mSetEffStatusList;
    private List<GameObject> mListItems = new List<GameObject>();
    private UnitData mUnitData;

    private void Start()
    {
      GameUtility.SetGameObjectActive(this.mSetEffStatusList, false);
      GameUtility.SetGameObjectActive(this.mSetEffParentOn, false);
      GameUtility.SetGameObjectActive(this.mSetEffParentOff, false);
    }

    public void SetData(UnitData unit)
    {
      this.mUnitData = unit;
      this.Refresh();
    }

    public void Refresh()
    {
      if (this.mUnitData == null)
        return;
      this.ClearObjects();
      List<RuneSetEff> enable_set_effects = RuneUtility.GetEnableRuneSetEffects(this.mUnitData.EquipRunes);
      if (enable_set_effects == null || enable_set_effects.Count == 0)
      {
        GameUtility.SetGameObjectActive(this.mSetEffParentOn, false);
        GameUtility.SetGameObjectActive(this.mSetEffParentOff, true);
      }
      else
      {
        GameUtility.SetGameObjectActive(this.mSetEffParentOn, true);
        GameUtility.SetGameObjectActive(this.mSetEffParentOff, false);
        List<RuneDrawEbaleSetEff.ViewData> viewDataList = new List<RuneDrawEbaleSetEff.ViewData>();
        for (int i = 0; i < enable_set_effects.Count; ++i)
        {
          RuneDrawEbaleSetEff.ViewData viewData = viewDataList.Find((Predicate<RuneDrawEbaleSetEff.ViewData>) (vd => vd.m_SetEffType == enable_set_effects[i].seteff_type));
          if (viewData == null)
          {
            viewData = new RuneDrawEbaleSetEff.ViewData();
            viewDataList.Add(viewData);
          }
          viewData.m_Name = enable_set_effects[i].name;
          viewData.m_IconIndex = (int) enable_set_effects[i].icon_index;
          viewData.m_SetEffType = enable_set_effects[i].seteff_type;
          enable_set_effects[i].AddRuneSetEffectBaseStatus(EElement.None, ref viewData.m_AddStatus, ref viewData.m_ScaleStatus, true);
        }
        for (int index = 0; index < viewDataList.Count; ++index)
        {
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.mSetEffStatusList, (Transform) this.mSetEffListParent, false);
          gameObject.SetActive(true);
          RuneStatusList componentInChildren;
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) (componentInChildren = gameObject.GetComponentInChildren<RuneStatusList>()), (UnityEngine.Object) null))
          {
            DebugUtility.LogError(string.Format("{0} に RuneStatusList コンポーネントがアタッチされていません。", (object) ((UnityEngine.Object) this.mSetEffStatusList).name));
            break;
          }
          componentInChildren.SetValues(viewDataList[index].m_AddStatus, viewDataList[index].m_ScaleStatus);
          componentInChildren.SetRuneSetEffect(viewDataList[index].m_IconIndex, viewDataList[index].m_Name);
          this.mListItems.Add(gameObject);
        }
      }
    }

    private void ClearObjects()
    {
      for (int index = 0; index < this.mListItems.Count; ++index)
      {
        if (!UnityEngine.Object.op_Equality((UnityEngine.Object) this.mListItems[index], (UnityEngine.Object) null))
          UnityEngine.Object.Destroy((UnityEngine.Object) this.mListItems[index]);
      }
      this.mListItems.Clear();
    }

    private class ViewData
    {
      public string m_Name;
      public int m_IconIndex;
      public int m_SetEffType;
      public BaseStatus m_AddStatus = (BaseStatus) new DrawBaseStatus();
      public BaseStatus m_ScaleStatus = (BaseStatus) new DrawBaseStatus();
    }
  }
}
