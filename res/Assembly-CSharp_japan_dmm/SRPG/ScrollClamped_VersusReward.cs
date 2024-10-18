// Decompiled with JetBrains decompiler
// Type: SRPG.ScrollClamped_VersusReward
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
  public class ScrollClamped_VersusReward : MonoBehaviour, ScrollListSetUp
  {
    public float Space = 1f;
    public bool Arrival = true;
    private int m_Max;
    private List<VersusTowerParam> m_Param = new List<VersusTowerParam>();

    public void Start()
    {
    }

    public void OnSetUpItems()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      VersusTowerParam[] versusTowerParam = instance.GetVersusTowerParam();
      if (versusTowerParam != null)
      {
        for (int index = versusTowerParam.Length - 1; index >= 0; --index)
        {
          if (string.Equals((string) versusTowerParam[index].VersusTowerID, instance.VersusTowerMatchName))
          {
            if (this.Arrival)
            {
              if (string.IsNullOrEmpty((string) versusTowerParam[index].ArrivalIteminame))
                continue;
            }
            else if (versusTowerParam[index].SeasonIteminame == null || versusTowerParam[index].SeasonIteminame.Length == 0)
              continue;
            this.m_Param.Add(versusTowerParam[index]);
          }
        }
        this.m_Max = this.m_Param.Count;
      }
      ScrollListController component1 = ((Component) this).GetComponent<ScrollListController>();
      // ISSUE: method pointer
      component1.OnItemUpdate.AddListener(new UnityAction<int, GameObject>((object) this, __methodptr(OnUpdateItems)));
      ((Component) this).GetComponentInParent<ScrollRect>().movementType = (ScrollRect.MovementType) 2;
      RectTransform component2 = ((Component) this).GetComponent<RectTransform>();
      Vector2 sizeDelta = component2.sizeDelta;
      Vector2 anchoredPosition = component2.anchoredPosition;
      anchoredPosition.y = 0.0f;
      sizeDelta.y = component1.ItemScale * this.Space * (float) this.m_Max;
      component2.sizeDelta = sizeDelta;
      component2.anchoredPosition = anchoredPosition;
    }

    public void OnUpdateItems(int idx, GameObject obj)
    {
      if (idx < 0 || idx >= this.m_Max || this.m_Param == null)
      {
        obj.SetActive(false);
      }
      else
      {
        obj.SetActive(true);
        DataSource.Bind<VersusTowerParam>(obj, this.m_Param[idx]);
        if (this.Arrival)
        {
          VersusTowerRewardItem component = obj.GetComponent<VersusTowerRewardItem>();
          if (!Object.op_Inequality((Object) component, (Object) null))
            return;
          component.Refresh();
        }
        else
        {
          VersusSeasonRewardInfo component = obj.GetComponent<VersusSeasonRewardInfo>();
          if (!Object.op_Inequality((Object) component, (Object) null))
            return;
          component.Refresh();
        }
      }
    }
  }
}
