// Decompiled with JetBrains decompiler
// Type: SRPG.UnitPickerButtonChanger
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class UnitPickerButtonChanger : MonoBehaviour
  {
    [CustomGroup("ウィンド")]
    [CustomField("ウィンド", CustomFieldAttribute.Type.GameObject)]
    public GameObject m_Root;
    [CustomGroup("差し替えボタン画像")]
    [CustomField("OFF", CustomFieldAttribute.Type.UISprite)]
    public Sprite m_ImageDefault;
    [CustomGroup("差し替えボタン画像")]
    [CustomField("ON", CustomFieldAttribute.Type.UISprite)]
    public Sprite m_ImageOn;
    [CustomGroup("オブジェクト")]
    [CustomField("イメージ", CustomFieldAttribute.Type.UIImage)]
    public Image m_Image;
    [CustomGroup("オブジェクト")]
    [CustomField("テキスト", CustomFieldAttribute.Type.UIText)]
    public Text m_Text;
    private UnitListWindow m_Window;
    private UnitListSortWindow.SelectType m_Sort;

    private void Awake()
    {
      if (!Object.op_Inequality((Object) this.m_Root, (Object) null))
        return;
      this.m_Window = this.m_Root.GetComponent<UnitListWindow>();
    }

    private void Start()
    {
    }

    private void Update()
    {
      if (Object.op_Equality((Object) this.m_Window, (Object) null))
        return;
      if (Object.op_Inequality((Object) this.m_Text, (Object) null) && this.m_Window.sortWindow != null && this.m_Sort != this.m_Window.sortWindow.GetSectionReg())
      {
        this.m_Sort = this.m_Window.sortWindow.GetSectionReg();
        string text = UnitListSortWindow.GetText(this.m_Sort);
        if (!string.IsNullOrEmpty(text) && this.m_Text.text != text)
          this.m_Text.text = text;
      }
      if (!Object.op_Inequality((Object) this.m_Image, (Object) null) || this.m_Window.filterWindow == null)
        return;
      if (this.m_Window.filterWindow.GetSelectReg().Count > 0)
        this.m_Image.sprite = this.m_ImageOn;
      else
        this.m_Image.sprite = this.m_ImageDefault;
    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }
  }
}
