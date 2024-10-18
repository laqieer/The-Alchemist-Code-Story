// Decompiled with JetBrains decompiler
// Type: SRPG.HelpButtonItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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
      HelpWindow componentInParent = this.transform.GetComponentInParent<HelpWindow>();
      if ((UnityEngine.Object) componentInParent == (UnityEngine.Object) null)
        return;
      int num1 = Idx;
      Transform transform = this.transform.Find("Label");
      if ((UnityEngine.Object) transform != (UnityEngine.Object) null)
      {
        LText component = transform.GetComponent<LText>();
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
      Button component1 = this.transform.GetComponent<Button>();
      Func<int, UnityAction> func = (Func<int, UnityAction>) (n => (UnityAction) (() => this.OnSelectMenu(n)));
      component1.onClick.RemoveAllListeners();
      component1.onClick.AddListener(func(num1));
    }

    private void OnSelectMenu(int MenuID)
    {
      HelpWindow componentInParent = this.transform.GetComponentInParent<HelpWindow>();
      if ((UnityEngine.Object) componentInParent == (UnityEngine.Object) null)
        return;
      if (componentInParent.MiddleHelp)
        componentInParent.CreateMainWindow(MenuID);
      else
        componentInParent.UpdateHelpList(true, MenuID);
    }
  }
}
