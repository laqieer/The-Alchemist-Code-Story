// Decompiled with JetBrains decompiler
// Type: SRPG.ScrollClamped_VersusTowerInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [RequireComponent(typeof (ScrollListController))]
  public class ScrollClamped_VersusTowerInfo : MonoBehaviour, ScrollListSetUp
  {
    private readonly int MARGIN = 5;
    public float Space = 1f;
    private int m_Max;

    public void Start()
    {
    }

    public void OnSetUpItems()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      VersusTowerParam[] versusTowerParam = instance.GetVersusTowerParam();
      if (versusTowerParam != null)
      {
        for (int index = 0; index < versusTowerParam.Length; ++index)
        {
          if (string.Equals((string) versusTowerParam[index].VersusTowerID, instance.VersusTowerMatchName))
            ++this.m_Max;
        }
        this.m_Max += this.MARGIN;
      }
      ScrollListController component1 = ((Component) this).GetComponent<ScrollListController>();
      // ISSUE: method pointer
      component1.OnItemUpdate.AddListener(new UnityAction<int, GameObject>((object) this, __methodptr(OnUpdateItems)));
      ((Component) this).GetComponentInParent<ScrollRect>().movementType = (ScrollRect.MovementType) 2;
      RectTransform component2 = ((Component) this).GetComponent<RectTransform>();
      Vector2 sizeDelta = component2.sizeDelta;
      sizeDelta.y = component1.ItemScale * this.Space * (float) this.m_Max;
      component2.sizeDelta = sizeDelta;
    }

    public void OnUpdateItems(int idx, GameObject obj)
    {
      if (idx < 0 || idx >= this.m_Max)
      {
        obj.SetActive(false);
      }
      else
      {
        obj.SetActive(true);
        VersusTowerFloor component = obj.GetComponent<VersusTowerFloor>();
        if (!Object.op_Inequality((Object) component, (Object) null))
          return;
        component.Refresh(idx, this.m_Max - this.MARGIN);
      }
    }
  }
}
