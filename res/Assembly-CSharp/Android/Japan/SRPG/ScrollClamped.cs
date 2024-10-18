// Decompiled with JetBrains decompiler
// Type: SRPG.ScrollClamped
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [RequireComponent(typeof (ScrollListController))]
  public class ScrollClamped : MonoBehaviour, ScrollListSetUp
  {
    private int m_Max;
    private int[] m_CategoryNum;

    public void Start()
    {
      string s1 = LocalizedText.Get("help.MENU_L_NUM");
      if (string.IsNullOrEmpty(s1))
        return;
      int length = int.Parse(s1);
      string s2 = LocalizedText.Get("help.MENU_NUM");
      if (string.IsNullOrEmpty(s2))
        return;
      int num1 = int.Parse(s2);
      this.m_CategoryNum = new int[length];
      int num2 = 0;
      for (int index = 0; index < length; ++index)
      {
        string b = LocalizedText.Get("help.MENU_CATE_NAME_" + (object) (index + 1));
        int num3 = 0;
        for (; num2 < num1 && string.Equals(LocalizedText.Get("help.MENU_CATE_" + (object) (num2 + 1)), b); ++num2)
          ++num3;
        this.m_CategoryNum[index] = num3;
      }
    }

    public void OnSetUpItems()
    {
      HelpWindow componentInParent = this.transform.GetComponentInParent<HelpWindow>();
      if ((UnityEngine.Object) componentInParent == (UnityEngine.Object) null)
        return;
      ScrollListController component1 = this.GetComponent<ScrollListController>();
      component1.OnItemUpdate.AddListener(new UnityAction<int, GameObject>(this.OnUpdateItems));
      this.GetComponentInParent<ScrollRect>().movementType = ScrollRect.MovementType.Clamped;
      RectTransform component2 = this.GetComponent<RectTransform>();
      Vector2 sizeDelta = component2.sizeDelta;
      if (componentInParent.MiddleHelp)
      {
        this.m_Max = this.m_CategoryNum[componentInParent.SelectMiddleID];
      }
      else
      {
        string s = LocalizedText.Get("help.MENU_L_NUM");
        if (string.IsNullOrEmpty(s))
          return;
        this.m_Max = int.Parse(s);
      }
      sizeDelta.y = component1.ItemScale * 1.2f * (float) this.m_Max;
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
        obj.SendMessage("UpdateParam", (object) idx);
      }
    }
  }
}
