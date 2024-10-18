// Decompiled with JetBrains decompiler
// Type: SRPG.ScrollClamped
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
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
      HelpWindow componentInParent = ((Component) ((Component) this).transform).GetComponentInParent<HelpWindow>();
      if (Object.op_Equality((Object) componentInParent, (Object) null))
        return;
      ScrollListController component1 = ((Component) this).GetComponent<ScrollListController>();
      // ISSUE: method pointer
      component1.OnItemUpdate.AddListener(new UnityAction<int, GameObject>((object) this, __methodptr(OnUpdateItems)));
      ((Component) this).GetComponentInParent<ScrollRect>().movementType = (ScrollRect.MovementType) 2;
      RectTransform component2 = ((Component) this).GetComponent<RectTransform>();
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
