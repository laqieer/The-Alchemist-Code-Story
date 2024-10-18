// Decompiled with JetBrains decompiler
// Type: SRPG.HelpWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
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

    public bool MiddleHelp => this.bMiddleHelp;

    public int SelectMiddleID => this.SelMiddleIdx;

    private void Start()
    {
      if (Object.op_Inequality((Object) this.BackButton, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.BackButton.onClick).AddListener(new UnityAction((object) this, __methodptr(OnCloseMain)));
        ((Component) ((Component) this.BackButton).transform.Find("Text")).GetComponent<LText>().text = LocalizedText.Get("help.BACK_BUTTON");
      }
      if (Object.op_Inequality((Object) this.MiddleBackButton, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.MiddleBackButton.onClick).AddListener(new UnityAction((object) this, __methodptr(OnBackList)));
        ((Component) ((Component) this.MiddleBackButton).transform.Find("Text")).GetComponent<LText>().text = LocalizedText.Get("help.BACK_BUTTON");
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

    private void OnDestroy() => GameUtility.DestroyGameObjects(this.mHelpMenuButtons);

    private void OnCloseMain()
    {
      this.m_HelpMain.SetActive(false);
      if (Object.op_Implicit((Object) this.MiddleBackButton))
        ((Component) this.MiddleBackButton).gameObject.SetActive(true);
      ((Component) this.BackButton).gameObject.SetActive(false);
    }

    public void OnBackList()
    {
      if (Object.op_Implicit((Object) this.MiddleBackButton))
        ((Component) this.MiddleBackButton).gameObject.SetActive(false);
      this.UpdateHelpList(false, 0);
    }

    public void UpdateHelpList(bool flag, int Idx)
    {
      GameObject gameObject = GameObject.Find("View");
      if (Object.op_Equality((Object) gameObject, (Object) null))
        return;
      ScrollListController component1 = gameObject.GetComponent<ScrollListController>();
      this.bMiddleHelp = flag;
      this.SelMiddleIdx = Idx;
      component1.UpdateList();
      if (this.MiddleHelp)
        ((Component) this.MiddleBackButton).gameObject.SetActive(true);
      Transform transform = ((Component) this).transform.Find("window/header");
      if (!Object.op_Inequality((Object) transform, (Object) null))
        return;
      LText component2 = ((Component) transform.Find("Text")).GetComponent<LText>();
      if (!Object.op_Inequality((Object) component2, (Object) null))
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
      if (MenuID < 0 || MenuID >= num1 || Object.op_Equality((Object) this.m_HelpMain, (Object) null))
        return;
      int num2 = MenuID + 1;
      float num3 = 0.0f;
      Transform transform1 = this.m_HelpMain.transform.Find("header");
      if (Object.op_Inequality((Object) transform1, (Object) null))
      {
        LText component = ((Component) transform1.Find("Text")).GetComponent<LText>();
        string str = LocalizedText.Get("help.MAIN_TITLE_" + (object) num2);
        if (string.IsNullOrEmpty(str) || str == "MAIN_TITLE_" + (object) num2)
          ((Component) transform1).gameObject.SetActive(false);
        else
          component.text = str;
      }
      Transform image1 = this.m_HelpMain.transform.Find("page/template/contents/viewport/layout/contents_image");
      if (this.SetImageData(image1, "Image", "Helps/help_image_" + (object) num2))
      {
        LayoutElement component = ((Component) image1).GetComponent<LayoutElement>();
        num3 += component.minHeight;
      }
      bool flag = false;
      Transform image2 = this.m_HelpMain.transform.Find("page/template/contents/viewport/layout/contents_image_small");
      if (flag | this.SetImageData(image2, "Image0", "Helps/help_image_" + (object) num2 + "_0") | this.SetImageData(image2, "Image1", "Helps/help_image_" + (object) num2 + "_1"))
      {
        ((Component) image2).gameObject.SetActive(true);
        LayoutElement component = ((Component) image2).GetComponent<LayoutElement>();
        num3 += component.minHeight;
      }
      Transform transform2 = this.m_HelpMain.transform.Find("page/template/contents/viewport/layout/contents_text");
      if (Object.op_Inequality((Object) transform2, (Object) null))
      {
        Transform transform3 = transform2.Find("Text");
        LText component1 = ((Component) transform3).GetComponent<LText>();
        string str1 = LocalizedText.Get("help.MAIN_TEXT_" + (object) num2);
        string str2 = "help.MAIN_TEXT_" + (object) num2;
        LayoutElement component2 = ((Component) transform2).GetComponent<LayoutElement>();
        if (string.IsNullOrEmpty(str1) || str1 == "MAIN_TEXT_" + (object) num2)
        {
          ((Component) transform2).gameObject.SetActive(false);
        }
        else
        {
          component1.text = str2;
          ((Component) transform2).gameObject.SetActive(true);
          num3 += component2.preferredHeight;
          HelpWindow.HELP_ID helpId = (HelpWindow.HELP_ID) num2;
          RectTransform component3 = ((Component) transform3).GetComponent<RectTransform>();
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
      RectTransform rectTransform = this.m_HelpMain.transform.Find("page/template/contents/viewport/layout") as RectTransform;
      Vector2 anchoredPosition1 = rectTransform.anchoredPosition;
      Vector2 sizeDelta = rectTransform.sizeDelta;
      sizeDelta.y = num3;
      rectTransform.sizeDelta = sizeDelta;
      anchoredPosition1.y = 0.0f;
      rectTransform.anchoredPosition = anchoredPosition1;
      this.m_HelpMain.SetActive(true);
      if (Object.op_Implicit((Object) this.MiddleBackButton))
        ((Component) this.MiddleBackButton).gameObject.SetActive(false);
      ((Component) this.BackButton).gameObject.SetActive(true);
    }

    private bool SetImageData(Transform image, string childname, string texname)
    {
      if (Object.op_Equality((Object) image, (Object) null))
        return false;
      Transform transform = image.Find(childname);
      RawImage component = ((Component) transform).GetComponent<RawImage>();
      Texture2D texture2D = AssetManager.Load<Texture2D>(texname);
      if (Object.op_Equality((Object) texture2D, (Object) null) || Object.op_Equality((Object) component, (Object) null))
      {
        ((Component) image).gameObject.SetActive(false);
        ((Component) transform).gameObject.SetActive(false);
        return false;
      }
      component.texture = (Texture) texture2D;
      ((Component) transform).gameObject.SetActive(true);
      ((Component) image).gameObject.SetActive(true);
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
