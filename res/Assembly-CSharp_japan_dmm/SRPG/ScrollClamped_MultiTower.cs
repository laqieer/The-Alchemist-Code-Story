// Decompiled with JetBrains decompiler
// Type: SRPG.ScrollClamped_MultiTower
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [RequireComponent(typeof (ScrollListController))]
  public class ScrollClamped_MultiTower : MonoBehaviour, ScrollListSetUp
  {
    private readonly float OFFSET = 2f;
    private readonly int MARGIN = 5;
    private int mMax;
    public float Space = 1f;
    public ScrollAutoFit AutoFit;
    public MultiTowerInfo TowerInfo;

    public void Start()
    {
    }

    public void OnSetUpItems()
    {
      List<MultiTowerFloorParam> mtAllFloorParam = MonoSingleton<GameManager>.Instance.GetMTAllFloorParam(GlobalVars.SelectedMultiTowerID);
      if (mtAllFloorParam != null)
        this.mMax = mtAllFloorParam.Count;
      this.mMax += this.MARGIN;
      ScrollListController component1 = ((Component) this).GetComponent<ScrollListController>();
      // ISSUE: method pointer
      component1.OnItemUpdate.AddListener(new UnityAction<int, GameObject>((object) this, __methodptr(OnUpdateItems)));
      ((Component) this).GetComponentInParent<ScrollRect>().movementType = (ScrollRect.MovementType) 2;
      RectTransform component2 = ((Component) this).GetComponent<RectTransform>();
      Vector2 sizeDelta = component2.sizeDelta;
      Vector2 anchoredPosition = component2.anchoredPosition;
      float num1 = component1.ItemScale * this.Space;
      float num2 = num1 - component1.ItemScale;
      anchoredPosition.y = component1.ItemScale * this.OFFSET;
      sizeDelta.y = num1 * (float) (this.mMax - this.MARGIN) - num2;
      component2.sizeDelta = sizeDelta;
      component2.anchoredPosition = anchoredPosition;
      if (Object.op_Inequality((Object) this.AutoFit, (Object) null))
        this.AutoFit.ItemScale = component1.ItemScale * this.Space;
      this.TowerInfo.Init();
    }

    public void OnUpdateItems(int idx, GameObject obj)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (idx < 0 || idx >= this.mMax)
      {
        obj.SetActive(false);
      }
      else
      {
        obj.SetActive(true);
        MultiTowerFloorParam mtFloorParam = instance.GetMTFloorParam(GlobalVars.SelectedMultiTowerID, idx + 1);
        if (mtFloorParam != null)
        {
          DataSource.Bind<MultiTowerFloorParam>(obj, mtFloorParam);
        }
        else
        {
          DataSource component = obj.GetComponent<DataSource>();
          if (Object.op_Inequality((Object) component, (Object) null))
            component.Clear();
        }
        MultiTowerFloorInfo component1 = obj.GetComponent<MultiTowerFloorInfo>();
        if (!Object.op_Inequality((Object) component1, (Object) null))
          return;
        component1.Refresh();
      }
    }
  }
}
