// Decompiled with JetBrains decompiler
// Type: SRPG.HelpButtonItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class HelpButtonItem : MonoBehaviour
  {
    public GameObject HelpMainTemplate;
    public GameObject m_MainWindowBase;
    public GameObject m_HelpMain;

    private void Start()
    {
    }

    private void Update()
    {
    }

    private void UpdateParam(int Idx)
    {
      HelpWindow componentInParent = ((Component) ((Component) this).transform).GetComponentInParent<HelpWindow>();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) componentInParent, (UnityEngine.Object) null))
        return;
      int num1 = Idx;
      Transform transform = ((Component) this).transform.Find("Label");
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) transform, (UnityEngine.Object) null))
      {
        LText component = ((Component) transform).GetComponent<LText>();
        if (componentInParent.MiddleHelp)
        {
          string a = LocalizedText.Get("help.MENU_CATE_NAME_" + (object) (componentInParent.SelectMiddleID + 1));
          string s = LocalizedText.Get("help.MENU_NUM");
          if (string.IsNullOrEmpty(s))
            return;
          int num2 = int.Parse(s);
          int num3 = 0;
          for (int index = 0; index < num2; ++index)
          {
            string b = LocalizedText.Get("help.MENU_CATE_" + (object) (index + 1));
            if (string.Equals(a, b))
            {
              num3 = index;
              break;
            }
          }
          component.text = LocalizedText.Get("help.MENU_" + (object) (num3 + Idx + 1));
          num1 = num3 + Idx;
        }
        else
          component.text = LocalizedText.Get("help.MENU_CATE_NAME_" + (object) (Idx + 1));
      }
      Button component1 = ((Component) ((Component) this).transform).GetComponent<Button>();
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: method pointer
      Func<int, UnityAction> func = (Func<int, UnityAction>) (n => new UnityAction((object) new HelpButtonItem.\u003CUpdateParam\u003Ec__AnonStorey0()
      {
        n = n,
        \u0024this = this
      }, __methodptr(\u003C\u003Em__0)));
      ((UnityEventBase) component1.onClick).RemoveAllListeners();
      ((UnityEvent) component1.onClick).AddListener(func(num1));
    }

    private void OnSelectMenu(int MenuID)
    {
      HelpWindow componentInParent = ((Component) ((Component) this).transform).GetComponentInParent<HelpWindow>();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) componentInParent, (UnityEngine.Object) null))
        return;
      if (componentInParent.MiddleHelp)
        componentInParent.CreateMainWindow(MenuID);
      else
        componentInParent.UpdateHelpList(true, MenuID);
    }
  }
}
