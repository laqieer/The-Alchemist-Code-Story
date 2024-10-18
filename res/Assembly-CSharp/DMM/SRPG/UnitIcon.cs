// Decompiled with JetBrains decompiler
// Type: SRPG.UnitIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class UnitIcon : BaseIcon
  {
    private const string TooltipPath = "UI/UnitTooltip_1";
    [Space(10f)]
    public GameParameter.UnitInstanceTypes InstanceType;
    public int InstanceIndex;
    public bool Tooltip;
    [Space(10f)]
    public RawImage Icon;
    public Image Frame;
    public Image Rarity;
    public Text Level;
    public Image Element;
    public RawImage Job;
    public GameObject LvParent;
    public SortBadge SortBadge;
    public bool AllowJobChange;
    private bool mIsLvActive = true;

    public override bool HasTooltip => this.Tooltip;

    private void Awake()
    {
    }

    private void OnEnable() => this.UpdateValue();

    private void OnDisable()
    {
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if (!Object.op_Inequality((Object) instanceDirect, (Object) null) || !Object.op_Inequality((Object) this.Icon, (Object) null))
        return;
      instanceDirect.CancelTextureLoadRequest(this.Icon);
    }

    protected virtual UnitData GetInstanceData()
    {
      return this.InstanceType.GetInstanceData(((Component) this).gameObject);
    }

    protected override void ShowTooltip(Vector2 screen)
    {
      if (!this.Tooltip)
        return;
      this.UpdatePartyWindow();
      this.GetInstanceData()?.ShowTooltip(((Component) this).gameObject, this.AllowJobChange, new UnitJobDropdown.ParentObjectEvent(((BaseIcon) this).UpdateValue));
    }

    public override void UpdateValue()
    {
      if (Object.op_Equality((Object) this, (Object) null))
        return;
      GameSettings instance = GameSettings.Instance;
      UnitData instanceData = this.GetInstanceData();
      if (Object.op_Inequality((Object) this.Icon, (Object) null))
      {
        if (instanceData != null)
        {
          ((Component) this.Icon).gameObject.SetActive(true);
          MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.Icon, AssetPath.UnitSkinIconSmall(instanceData.UnitParam, instanceData.GetSelectedSkin(), instanceData.CurrentJobId));
        }
        else if (DataSource.FindDataOfClass<UnitParam>(((Component) this).gameObject, (UnitParam) null) == null)
          ((Component) this.Icon).gameObject.SetActive(false);
      }
      if (Object.op_Inequality((Object) this.LvParent, (Object) null))
      {
        if (instanceData != null)
          this.LvParent.SetActive(this.mIsLvActive);
        else
          this.LvParent.SetActive(false);
      }
      if (Object.op_Inequality((Object) this.Level, (Object) null))
      {
        if (instanceData != null)
        {
          this.Level.text = instanceData.Lv.ToString();
          ((Component) this.Level).gameObject.SetActive(true);
        }
        else
          ((Component) this.Level).gameObject.SetActive(false);
      }
      if (Object.op_Inequality((Object) this.Rarity, (Object) null) && Object.op_Inequality((Object) instance, (Object) null) && instance.UnitIcon_Rarity.Length > 0)
      {
        if (instanceData != null)
        {
          int index = 0;
          if (instanceData.CurrentJob != null)
            index = Mathf.Clamp(instanceData.Rarity, 0, instance.UnitIcon_Rarity.Length - 1);
          this.Rarity.sprite = instance.UnitIcon_Rarity[index];
        }
        else
          this.Rarity.sprite = (Sprite) null;
      }
      if (Object.op_Inequality((Object) this.Frame, (Object) null) && Object.op_Inequality((Object) instance, (Object) null) && instance.UnitIcon_Frames.Length > 0)
      {
        if (instanceData != null)
        {
          int index = 0;
          if (instanceData.CurrentJob != null)
            index = Mathf.Clamp(instanceData.CurrentJob.Rank, 0, instance.UnitIcon_Frames.Length - 1);
          this.Frame.sprite = instance.UnitIcon_Frames[index];
        }
        else
          this.Frame.sprite = (Sprite) null;
      }
      if (Object.op_Inequality((Object) this.Element, (Object) null) && Object.op_Inequality((Object) instance, (Object) null))
        this.Element.sprite = instanceData == null || EElement.None > instanceData.Element || instanceData.Element >= (EElement) instance.Elements_IconSmall.Length ? (Sprite) null : instance.Elements_IconSmall[(int) instanceData.Element];
      if (!Object.op_Inequality((Object) this.Job, (Object) null))
        return;
      JobParam job = (JobParam) null;
      if (instanceData != null && instanceData.CurrentJob != null)
        job = instanceData.CurrentJob.Param;
      MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.Job, job == null ? (string) null : AssetPath.JobIconSmall(job));
    }

    public void SetSortValue(GameUtility.UnitSortModes mode, int value, bool isLevelActive = true)
    {
      if (!Object.op_Inequality((Object) this.SortBadge, (Object) null))
        return;
      if (mode != GameUtility.UnitSortModes.Level && mode != GameUtility.UnitSortModes.Rarity && mode != GameUtility.UnitSortModes.Time)
      {
        if (Object.op_Inequality((Object) this.SortBadge.Value, (Object) null))
          this.SortBadge.Value.text = value.ToString();
        if (Object.op_Inequality((Object) this.SortBadge.Icon, (Object) null))
          this.SortBadge.Icon.sprite = GameSettings.Instance.GetUnitSortModeIcon(mode);
        ((Component) this.SortBadge).gameObject.SetActive(true);
        this.mIsLvActive = isLevelActive;
      }
      else
      {
        ((Component) this.SortBadge).gameObject.SetActive(false);
        this.mIsLvActive = true;
      }
    }

    public void ClearSortValue()
    {
      if (!Object.op_Inequality((Object) this.SortBadge, (Object) null))
        return;
      ((Component) this.SortBadge).gameObject.SetActive(false);
      this.mIsLvActive = true;
    }

    public void UpdatePartyWindow()
    {
      PartyWindow2 componentInParent = ((Component) this).GetComponentInParent<PartyWindow2>();
      if (!Object.op_Inequality((Object) componentInParent, (Object) null))
        return;
      componentInParent.Refresh(true);
    }
  }
}
