// Decompiled with JetBrains decompiler
// Type: SRPG.UnitPickerButtonChanger
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class UnitPickerButtonChanger : MonoBehaviour
  {
    private UnitListFilterWindow.SelectType m_Filter = UnitListFilterWindow.SelectType.RARITY_1 | UnitListFilterWindow.SelectType.RARITY_2 | UnitListFilterWindow.SelectType.RARITY_3 | UnitListFilterWindow.SelectType.RARITY_4 | UnitListFilterWindow.SelectType.RARITY_5 | UnitListFilterWindow.SelectType.WEAPON_SLASH | UnitListFilterWindow.SelectType.WEAPON_STAB | UnitListFilterWindow.SelectType.WEAPON_BLOW | UnitListFilterWindow.SelectType.WEAPON_SHOT | UnitListFilterWindow.SelectType.WEAPON_MAG | UnitListFilterWindow.SelectType.WEAPON_NONE | UnitListFilterWindow.SelectType.BIRTH_ENV | UnitListFilterWindow.SelectType.BIRTH_WRATH | UnitListFilterWindow.SelectType.BIRTH_SAGA | UnitListFilterWindow.SelectType.BIRTH_SLOTH | UnitListFilterWindow.SelectType.BIRTH_LUST | UnitListFilterWindow.SelectType.BIRTH_WADATSUMI | UnitListFilterWindow.SelectType.BIRTH_DESERT | UnitListFilterWindow.SelectType.BIRTH_GREED | UnitListFilterWindow.SelectType.BIRTH_GLUTTONY | UnitListFilterWindow.SelectType.BIRTH_OTHER;
    [CustomGroup("ウィンド")]
    [CustomField("ウィンド", CustomFieldAttribute.Type.GameObject)]
    public GameObject m_Root;
    [CustomField("OFF", CustomFieldAttribute.Type.UISprite)]
    [CustomGroup("差し替えボタン画像")]
    public Sprite m_ImageDefault;
    [CustomGroup("差し替えボタン画像")]
    [CustomField("ON", CustomFieldAttribute.Type.UISprite)]
    public Sprite m_ImageOn;
    [CustomField("イメージ", CustomFieldAttribute.Type.UIImage)]
    [CustomGroup("オブジェクト")]
    public Image m_Image;
    [CustomField("テキスト", CustomFieldAttribute.Type.UIText)]
    [CustomGroup("オブジェクト")]
    public Text m_Text;
    private UnitListWindow m_Window;
    private UnitListSortWindow.SelectType m_Sort;

    private void Awake()
    {
      if (!((UnityEngine.Object) this.m_Root != (UnityEngine.Object) null))
        return;
      this.m_Window = this.m_Root.GetComponent<UnitListWindow>();
    }

    private void Start()
    {
    }

    private void Update()
    {
      if ((UnityEngine.Object) this.m_Window == (UnityEngine.Object) null)
        return;
      if ((UnityEngine.Object) this.m_Text != (UnityEngine.Object) null && this.m_Window.sortWindow != null && this.m_Sort != this.m_Window.sortWindow.GetSectionReg())
      {
        this.m_Sort = this.m_Window.sortWindow.GetSectionReg();
        string text = UnitListSortWindow.GetText(this.m_Sort);
        if (!string.IsNullOrEmpty(text) && this.m_Text.text != text)
          this.m_Text.text = text;
      }
      if (!((UnityEngine.Object) this.m_Image != (UnityEngine.Object) null) || this.m_Window.filterWindow == null)
        return;
      UnitListFilterWindow.SelectType selectReg = this.m_Window.filterWindow.GetSelectReg(UnitListFilterWindow.SelectType.RARITY_1 | UnitListFilterWindow.SelectType.RARITY_2 | UnitListFilterWindow.SelectType.RARITY_3 | UnitListFilterWindow.SelectType.RARITY_4 | UnitListFilterWindow.SelectType.RARITY_5 | UnitListFilterWindow.SelectType.WEAPON_SLASH | UnitListFilterWindow.SelectType.WEAPON_STAB | UnitListFilterWindow.SelectType.WEAPON_BLOW | UnitListFilterWindow.SelectType.WEAPON_SHOT | UnitListFilterWindow.SelectType.WEAPON_MAG | UnitListFilterWindow.SelectType.WEAPON_NONE | UnitListFilterWindow.SelectType.BIRTH_ENV | UnitListFilterWindow.SelectType.BIRTH_WRATH | UnitListFilterWindow.SelectType.BIRTH_SAGA | UnitListFilterWindow.SelectType.BIRTH_SLOTH | UnitListFilterWindow.SelectType.BIRTH_LUST | UnitListFilterWindow.SelectType.BIRTH_WADATSUMI | UnitListFilterWindow.SelectType.BIRTH_DESERT | UnitListFilterWindow.SelectType.BIRTH_GREED | UnitListFilterWindow.SelectType.BIRTH_GLUTTONY | UnitListFilterWindow.SelectType.BIRTH_OTHER);
      if (this.m_Filter == selectReg)
        return;
      this.m_Filter = selectReg;
      if ((this.m_Filter ^ (UnitListFilterWindow.SelectType.RARITY_1 | UnitListFilterWindow.SelectType.RARITY_2 | UnitListFilterWindow.SelectType.RARITY_3 | UnitListFilterWindow.SelectType.RARITY_4 | UnitListFilterWindow.SelectType.RARITY_5 | UnitListFilterWindow.SelectType.WEAPON_SLASH | UnitListFilterWindow.SelectType.WEAPON_STAB | UnitListFilterWindow.SelectType.WEAPON_BLOW | UnitListFilterWindow.SelectType.WEAPON_SHOT | UnitListFilterWindow.SelectType.WEAPON_MAG | UnitListFilterWindow.SelectType.WEAPON_NONE | UnitListFilterWindow.SelectType.BIRTH_ENV | UnitListFilterWindow.SelectType.BIRTH_WRATH | UnitListFilterWindow.SelectType.BIRTH_SAGA | UnitListFilterWindow.SelectType.BIRTH_SLOTH | UnitListFilterWindow.SelectType.BIRTH_LUST | UnitListFilterWindow.SelectType.BIRTH_WADATSUMI | UnitListFilterWindow.SelectType.BIRTH_DESERT | UnitListFilterWindow.SelectType.BIRTH_GREED | UnitListFilterWindow.SelectType.BIRTH_GLUTTONY | UnitListFilterWindow.SelectType.BIRTH_OTHER)) == UnitListFilterWindow.SelectType.NONE)
        this.m_Image.sprite = this.m_ImageDefault;
      else
        this.m_Image.sprite = this.m_ImageOn;
    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }
  }
}
