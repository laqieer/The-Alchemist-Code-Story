// Decompiled with JetBrains decompiler
// Type: SRPG.RecommendTeamWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

namespace SRPG
{
  public class RecommendTeamWindow : MonoBehaviour
  {
    private readonly RecommendTeamWindow.TypeAndStr[] items = new RecommendTeamWindow.TypeAndStr[13]{ new RecommendTeamWindow.TypeAndStr(GlobalVars.RecommendType.Total, "sys.RECOMMEND_TEAM_SORT_TOTAL_TEXT"), new RecommendTeamWindow.TypeAndStr(GlobalVars.RecommendType.Hp, "sys.RECOMMEND_TEAM_SORT_HP_TEXT"), new RecommendTeamWindow.TypeAndStr(GlobalVars.RecommendType.Attack, "sys.RECOMMEND_TEAM_SORT_ATTACK_TEXT"), new RecommendTeamWindow.TypeAndStr(GlobalVars.RecommendType.Defence, "sys.RECOMMEND_TEAM_SORT_DEFENCE_TEXT"), new RecommendTeamWindow.TypeAndStr(GlobalVars.RecommendType.Magic, "sys.RECOMMEND_TEAM_SORT_MAGIC_TEXT"), new RecommendTeamWindow.TypeAndStr(GlobalVars.RecommendType.Mind, "sys.RECOMMEND_TEAM_SORT_MIND_TEXT"), new RecommendTeamWindow.TypeAndStr(GlobalVars.RecommendType.Speed, "sys.RECOMMEND_TEAM_SORT_SPEED_TEXT"), new RecommendTeamWindow.TypeAndStr(GlobalVars.RecommendType.AttackTypeSlash, "sys.RECOMMEND_TEAM_SORT_ATTACK_TYPE_SLASH_TEXT"), new RecommendTeamWindow.TypeAndStr(GlobalVars.RecommendType.AttackTypeStab, "sys.RECOMMEND_TEAM_SORT_ATTACK_TYPE_STAB_TEXT"), new RecommendTeamWindow.TypeAndStr(GlobalVars.RecommendType.AttackTypeBlow, "sys.RECOMMEND_TEAM_SORT_ATTACK_TYPE_BLOW_TEXT"), new RecommendTeamWindow.TypeAndStr(GlobalVars.RecommendType.AttackTypeShot, "sys.RECOMMEND_TEAM_SORT_ATTACK_TYPE_SHOT_TEXT"), new RecommendTeamWindow.TypeAndStr(GlobalVars.RecommendType.AttackTypeMagic, "sys.RECOMMEND_TEAM_SORT_ATTACK_TYPE_MAGIC_TEXT"), new RecommendTeamWindow.TypeAndStr(GlobalVars.RecommendType.AttackTypeNone, "sys.RECOMMEND_TEAM_SORT_ATTACK_TYPE_NONE_TEXT") };
    private readonly RecommendTeamWindow.ElemAndStr[] elements = new RecommendTeamWindow.ElemAndStr[7]{ new RecommendTeamWindow.ElemAndStr(EElement.None, "sys.RECOMMEND_TEAM_ELEMENT_ALL_TEXT"), new RecommendTeamWindow.ElemAndStr(EElement.Fire, "sys.RECOMMEND_TEAM_ELEMENT_FIRE_TEXT"), new RecommendTeamWindow.ElemAndStr(EElement.Water, "sys.RECOMMEND_TEAM_ELEMENT_WATER_TEXT"), new RecommendTeamWindow.ElemAndStr(EElement.Wind, "sys.RECOMMEND_TEAM_ELEMENT_WIND_TEXT"), new RecommendTeamWindow.ElemAndStr(EElement.Thunder, "sys.RECOMMEND_TEAM_ELEMENT_THUNDER_TEXT"), new RecommendTeamWindow.ElemAndStr(EElement.Shine, "sys.RECOMMEND_TEAM_ELEMENT_SHINE_TEXT"), new RecommendTeamWindow.ElemAndStr(EElement.Dark, "sys.RECOMMEND_TEAM_ELEMENT_DARK_TEXT") };
    [SerializeField]
    private ScrollablePulldown TypePullDown;
    [SerializeField]
    private ElementDropdown ElemmentPullDown;
    private int currentTypeIndex;
    private int currentElemmentIndex;

    private void Awake()
    {
      this.TypePullDown.OnSelectionChangeDelegate = new ScrollablePulldownBase.SelectItemEvent(this.OnTypeItemSelect);
      this.ElemmentPullDown.OnSelectionChangeDelegate = new Pulldown.SelectItemEvent(this.OnElemmentItemSelect);
      if (GlobalVars.RecommendTeamSettingValue != null)
      {
        this.currentTypeIndex = PartyUtility.RecommendTypeToComparatorOrder(GlobalVars.RecommendTeamSettingValue.recommendedType);
        this.currentElemmentIndex = (int) GlobalVars.RecommendTeamSettingValue.recommendedElement;
      }
      else
      {
        this.currentTypeIndex = 0;
        this.currentElemmentIndex = 0;
      }
      this.Refresh();
    }

    private void OnTypeItemSelect(int value)
    {
      if (value < 0 || value >= this.items.Length || value == this.currentTypeIndex)
        return;
      this.currentTypeIndex = value;
    }

    private void OnElemmentItemSelect(int value)
    {
      if (value == this.currentElemmentIndex)
        return;
      this.currentElemmentIndex = value;
    }

    private void Refresh()
    {
      if ((UnityEngine.Object) this.TypePullDown != (UnityEngine.Object) null)
      {
        this.TypePullDown.ClearItems();
        for (int index = 0; index < this.items.Length; ++index)
          this.TypePullDown.AddItem(LocalizedText.Get(this.items[index].title), index);
        this.TypePullDown.Selection = this.currentTypeIndex;
        this.TypePullDown.gameObject.SetActive(true);
      }
      if (!((UnityEngine.Object) this.ElemmentPullDown != (UnityEngine.Object) null))
        return;
      this.ElemmentPullDown.ClearItems();
      GameSettings instance = GameSettings.Instance;
      for (int index = 0; index < this.elements.Length; ++index)
      {
        Sprite sprite = (Sprite) null;
        if (index < instance.Elements_IconSmall.Length && index != 0)
          sprite = instance.Elements_IconSmall[(int) this.elements[index].element];
        this.ElemmentPullDown.AddItem(LocalizedText.Get(this.elements[index].title), sprite, index);
      }
      this.ElemmentPullDown.Selection = this.currentElemmentIndex;
      this.ElemmentPullDown.gameObject.SetActive(true);
    }

    public void SaveSettings()
    {
      GlobalVars.RecommendType type = this.items[this.currentTypeIndex].type;
      EElement element = this.elements[this.currentElemmentIndex].element;
      if (Enum.IsDefined(typeof (GlobalVars.RecommendType), (object) type) && Enum.IsDefined(typeof (EElement), (object) element))
        GlobalVars.RecommendTeamSettingValue = new GlobalVars.RecommendTeamSetting(type, element);
      else
        GlobalVars.RecommendTeamSettingValue = (GlobalVars.RecommendTeamSetting) null;
    }

    public void Cancel()
    {
      GlobalVars.RecommendTeamSettingValue = (GlobalVars.RecommendTeamSetting) null;
    }

    private struct TypeAndStr
    {
      public readonly GlobalVars.RecommendType type;
      public readonly string title;

      public TypeAndStr(GlobalVars.RecommendType type, string title)
      {
        this.type = type;
        this.title = title;
      }
    }

    private struct ElemAndStr
    {
      public readonly EElement element;
      public readonly string title;

      public ElemAndStr(EElement element, string title)
      {
        this.element = element;
        this.title = title;
      }
    }
  }
}
