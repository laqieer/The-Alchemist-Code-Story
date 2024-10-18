// Decompiled with JetBrains decompiler
// Type: SRPG.HelpWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  public class HelpWindow : MonoBehaviour
  {
    public static readonly string VAR_NAME_MENU_ID = "HELP_MENU_ID";
    public bool ReferenceFlowVariable;
    public Button BackButton;
    public Button MiddleBackButton;
    private bool bMiddleHelp;
    private int SelMiddleIdx;
    private GameObject[] mHelpMenuButtons;
    public GameObject m_HelpMain;

    public bool MiddleHelp
    {
      get
      {
        return this.bMiddleHelp;
      }
    }

    public int SelectMiddleID
    {
      get
      {
        return this.SelMiddleIdx;
      }
    }

    private void Start()
    {
      if ((UnityEngine.Object) this.BackButton != (UnityEngine.Object) null)
      {
        this.BackButton.onClick.AddListener(new UnityAction(this.OnCloseMain));
        this.BackButton.transform.FindChild("Text").GetComponent<LText>().text = LocalizedText.Get("help.BACK_BUTTON");
      }
      if ((UnityEngine.Object) this.MiddleBackButton != (UnityEngine.Object) null)
      {
        this.MiddleBackButton.onClick.AddListener(new UnityAction(this.OnBackList));
        this.MiddleBackButton.transform.FindChild("Text").GetComponent<LText>().text = LocalizedText.Get("help.BACK_BUTTON");
      }
      string s1 = LocalizedText.Get("help.MENU_NUM");
      if (string.IsNullOrEmpty(s1))
        return;
      this.mHelpMenuButtons = new GameObject[int.Parse(s1)];
      if (!this.ReferenceFlowVariable)
        return;
      string s2 = FlowNode_Variable.Get(HelpWindow.VAR_NAME_MENU_ID);
      if (string.IsNullOrEmpty(s2))
        return;
      int result;
      if (int.TryParse(s2, out result))
        this.CreateMainWindow(result - 1);
      FlowNode_Variable.Set(HelpWindow.VAR_NAME_MENU_ID, string.Empty);
    }

    private void OnDestroy()
    {
      GameUtility.DestroyGameObjects(this.mHelpMenuButtons);
    }

    private void OnCloseMain()
    {
      this.m_HelpMain.SetActive(false);
      if ((bool) ((UnityEngine.Object) this.MiddleBackButton))
        this.MiddleBackButton.gameObject.SetActive(true);
      this.BackButton.gameObject.SetActive(false);
    }

    public void OnBackList()
    {
      if ((bool) ((UnityEngine.Object) this.MiddleBackButton))
        this.MiddleBackButton.gameObject.SetActive(false);
      this.UpdateHelpList(false, 0);
    }

    public void UpdateHelpList(bool flag, int Idx)
    {
      GameObject gameObject = GameObject.Find("View");
      if ((UnityEngine.Object) gameObject == (UnityEngine.Object) null)
        return;
      ScrollListController component1 = gameObject.GetComponent<ScrollListController>();
      this.bMiddleHelp = flag;
      this.SelMiddleIdx = Idx;
      component1.UpdateList();
      if (this.MiddleHelp)
        this.MiddleBackButton.gameObject.SetActive(true);
      Transform child = this.transform.FindChild("window/header");
      if (!((UnityEngine.Object) child != (UnityEngine.Object) null))
        return;
      LText component2 = child.FindChild("Text").GetComponent<LText>();
      if (!((UnityEngine.Object) component2 != (UnityEngine.Object) null))
        return;
      string str = LocalizedText.Get("help.TITLE");
      if (this.MiddleHelp)
        str = str + "-" + LocalizedText.Get("help.MENU_CATE_NAME_" + (object) (Idx + 1));
      if (string.IsNullOrEmpty(str))
        return;
      component2.text = str;
    }

    public void CreateMainWindow(int MenuID)
    {
      string s = LocalizedText.Get("help.MENU_NUM");
      if (s == null)
        return;
      int num1 = int.Parse(s);
      if (MenuID < 0 || MenuID >= num1 || (UnityEngine.Object) this.m_HelpMain == (UnityEngine.Object) null)
        return;
      int num2 = MenuID + 1;
      float num3 = 0.0f;
      Transform child1 = this.m_HelpMain.transform.FindChild("header");
      if ((UnityEngine.Object) child1 != (UnityEngine.Object) null)
      {
        LText component = child1.FindChild("Text").GetComponent<LText>();
        string str = LocalizedText.Get("help.MAIN_TITLE_" + (object) num2);
        if (string.IsNullOrEmpty(str) || str == "MAIN_TITLE_" + (object) num2)
          child1.gameObject.SetActive(false);
        else
          component.text = str;
      }
      Transform image1 = this.m_HelpMain.transform.Find("page/template/contents/viewport/layout/contents_image");
      if (this.SetImageData(image1, "Image", "Helps/help_image_" + (object) num2))
      {
        LayoutElement component = image1.GetComponent<LayoutElement>();
        num3 += component.minHeight;
      }
      bool flag = false;
      Transform image2 = this.m_HelpMain.transform.Find("page/template/contents/viewport/layout/contents_image_small");
      if (flag | this.SetImageData(image2, "Image0", "Helps/help_image_" + (object) num2 + "_0") | this.SetImageData(image2, "Image1", "Helps/help_image_" + (object) num2 + "_1"))
      {
        image2.gameObject.SetActive(true);
        LayoutElement component = image2.GetComponent<LayoutElement>();
        num3 += component.minHeight;
      }
      Transform child2 = this.m_HelpMain.transform.FindChild("page/template/contents/viewport/layout/contents_text");
      if ((UnityEngine.Object) child2 != (UnityEngine.Object) null)
      {
        Transform child3 = child2.FindChild("Text");
        LText component1 = child3.GetComponent<LText>();
        string str1 = LocalizedText.Get("help.MAIN_TEXT_" + (object) num2);
        string str2 = "help.MAIN_TEXT_" + (object) num2;
        LayoutElement component2 = child2.GetComponent<LayoutElement>();
        if (string.IsNullOrEmpty(str1) || str1 == "MAIN_TEXT_" + (object) num2)
        {
          child2.gameObject.SetActive(false);
        }
        else
        {
          component1.text = str2;
          child2.gameObject.SetActive(true);
          num3 += component2.preferredHeight;
          HelpWindow.HELP_ID helpId = (HelpWindow.HELP_ID) num2;
          RectTransform component3 = child3.GetComponent<RectTransform>();
          Vector2 anchoredPosition = component3.anchoredPosition;
          switch (helpId)
          {
            case HelpWindow.HELP_ID.ACTION:
              anchoredPosition.y = 150f;
              component3.anchoredPosition = anchoredPosition;
              num3 -= component2.preferredHeight;
              break;
            case HelpWindow.HELP_ID.REACTION:
            case HelpWindow.HELP_ID.SUPPORT:
              anchoredPosition.y = 250f;
              component3.anchoredPosition = anchoredPosition;
              num3 = component2.preferredHeight;
              break;
            case HelpWindow.HELP_ID.SHOP:
              anchoredPosition.y = 200f;
              component3.anchoredPosition = anchoredPosition;
              num3 -= component2.preferredHeight;
              break;
            default:
              anchoredPosition.y = 0.0f;
              component3.anchoredPosition = anchoredPosition;
              break;
          }
        }
      }
      RectTransform child4 = this.m_HelpMain.transform.FindChild("page/template/contents/viewport/layout") as RectTransform;
      Vector2 anchoredPosition1 = child4.anchoredPosition;
      Vector2 sizeDelta = child4.sizeDelta;
      sizeDelta.y = num3;
      child4.sizeDelta = sizeDelta;
      anchoredPosition1.y = 0.0f;
      child4.anchoredPosition = anchoredPosition1;
      this.m_HelpMain.SetActive(true);
      if ((bool) ((UnityEngine.Object) this.MiddleBackButton))
        this.MiddleBackButton.gameObject.SetActive(false);
      this.BackButton.gameObject.SetActive(true);
    }

    private bool SetImageData(Transform image, string childname, string texname)
    {
      if ((UnityEngine.Object) image == (UnityEngine.Object) null)
        return false;
      Transform child = image.FindChild(childname);
      RawImage component = child.GetComponent<RawImage>();
      Texture2D texture2D = AssetManager.Load<Texture2D>(texname);
      if ((UnityEngine.Object) texture2D == (UnityEngine.Object) null || (UnityEngine.Object) component == (UnityEngine.Object) null)
      {
        image.gameObject.SetActive(false);
        child.gameObject.SetActive(false);
        return false;
      }
      component.texture = (Texture) texture2D;
      child.gameObject.SetActive(true);
      image.gameObject.SetActive(true);
      return true;
    }

    private float TextHeight(string text, LText ltext)
    {
      float num = (float) ltext.fontSize * 2f;
      for (int index = 0; index < text.Length; ++index)
      {
        if (text[index] == '\n')
          num += (float) ltext.fontSize + ltext.lineSpacing;
      }
      return num;
    }

    private enum HELP_ID
    {
      ACTION = 61, // 0x0000003D
      REACTION = 62, // 0x0000003E
      SUPPORT = 63, // 0x0000003F
      SHOP = 96, // 0x00000060
    }
  }
}
