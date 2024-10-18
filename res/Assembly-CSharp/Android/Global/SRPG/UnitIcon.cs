// Decompiled with JetBrains decompiler
// Type: SRPG.UnitIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class UnitIcon : BaseIcon
  {
    private bool mIsLvActive = true;
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
    public GameObject UnitName;
    public GameObject LvParent;
    public SortBadge SortBadge;
    public bool AllowJobChange;
    private Text mNameText;

    public override bool HasTooltip
    {
      get
      {
        return this.Tooltip;
      }
    }

    private void Awake()
    {
      if (!((UnityEngine.Object) this.UnitName != (UnityEngine.Object) null))
        return;
      this.mNameText = this.UnitName.GetComponentInChildren<Text>();
    }

    private void OnEnable()
    {
      this.UpdateValue();
    }

    private void OnDisable()
    {
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if (!((UnityEngine.Object) instanceDirect != (UnityEngine.Object) null) || !((UnityEngine.Object) this.Icon != (UnityEngine.Object) null))
        return;
      instanceDirect.CancelTextureLoadRequest(this.Icon);
    }

    protected virtual UnitData GetInstanceData()
    {
      return this.InstanceType.GetInstanceData(this.gameObject);
    }

    protected override void ShowTooltip(Vector2 screen)
    {
      if (!this.Tooltip)
        return;
      this.UpdatePartyWindow();
      UnitData instanceData = this.GetInstanceData();
      if (instanceData == null)
        return;
      PlayerPartyTypes dataOfClass = DataSource.FindDataOfClass<PlayerPartyTypes>(this.gameObject, PlayerPartyTypes.Max);
      GameObject root = UnityEngine.Object.Instantiate<GameObject>(AssetManager.Load<GameObject>("UI/UnitTooltip_1"));
      UnitData data = new UnitData();
      data.Setup(instanceData);
      data.TempFlags = instanceData.TempFlags;
      DataSource.Bind<UnitData>(root, data);
      DataSource.Bind<PlayerPartyTypes>(root, dataOfClass);
      UnitJobDropdown componentInChildren1 = root.GetComponentInChildren<UnitJobDropdown>();
      if ((UnityEngine.Object) componentInChildren1 != (UnityEngine.Object) null)
      {
        bool flag = (instanceData.TempFlags & UnitData.TemporaryFlags.AllowJobChange) != (UnitData.TemporaryFlags) 0 && this.AllowJobChange && dataOfClass != PlayerPartyTypes.Max;
        componentInChildren1.gameObject.SetActive(true);
        componentInChildren1.UpdateValue = new UnitJobDropdown.ParentObjectEvent(this.UpdateValue);
        Selectable component1 = componentInChildren1.gameObject.GetComponent<Selectable>();
        if ((UnityEngine.Object) component1 != (UnityEngine.Object) null)
          component1.interactable = flag;
        Image component2 = componentInChildren1.gameObject.GetComponent<Image>();
        if ((UnityEngine.Object) component2 != (UnityEngine.Object) null)
          component2.color = !flag ? new Color(0.5f, 0.5f, 0.5f) : Color.white;
      }
      ArtifactSlots componentInChildren2 = root.GetComponentInChildren<ArtifactSlots>();
      AbilitySlots componentInChildren3 = root.GetComponentInChildren<AbilitySlots>();
      if ((UnityEngine.Object) componentInChildren2 != (UnityEngine.Object) null && (UnityEngine.Object) componentInChildren3 != (UnityEngine.Object) null)
      {
        bool enable = (instanceData.TempFlags & UnitData.TemporaryFlags.AllowJobChange) != (UnitData.TemporaryFlags) 0 && this.AllowJobChange && dataOfClass != PlayerPartyTypes.Max;
        componentInChildren2.Refresh(enable);
        componentInChildren3.Refresh(enable);
      }
      GameParameter.UpdateAll(root);
    }

    public override void UpdateValue()
    {
      GameSettings instance = GameSettings.Instance;
      UnitData instanceData = this.GetInstanceData();
      if ((UnityEngine.Object) this.Icon != (UnityEngine.Object) null)
        MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.Icon, instanceData == null ? (string) null : AssetPath.UnitSkinIconSmall(instanceData.UnitParam, instanceData.GetSelectedSkin(-1), instanceData.CurrentJobId));
      if ((UnityEngine.Object) this.LvParent != (UnityEngine.Object) null)
        this.LvParent.SetActive(this.mIsLvActive);
      if ((UnityEngine.Object) this.Level != (UnityEngine.Object) null)
      {
        if (instanceData != null)
        {
          this.Level.text = instanceData.Lv.ToString();
          this.Level.gameObject.SetActive(true);
        }
        else
          this.Level.gameObject.SetActive(false);
      }
      if ((UnityEngine.Object) this.Rarity != (UnityEngine.Object) null && (UnityEngine.Object) instance != (UnityEngine.Object) null && instance.UnitIcon_Rarity.Length > 0)
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
      if ((UnityEngine.Object) this.Frame != (UnityEngine.Object) null && (UnityEngine.Object) instance != (UnityEngine.Object) null && instance.UnitIcon_Frames.Length > 0)
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
      if ((UnityEngine.Object) this.Element != (UnityEngine.Object) null && (UnityEngine.Object) instance != (UnityEngine.Object) null)
        this.Element.sprite = instanceData == null || EElement.None > instanceData.Element || instanceData.Element >= (EElement) instance.Elements_IconSmall.Length ? (Sprite) null : instance.Elements_IconSmall[(int) instanceData.Element];
      if ((UnityEngine.Object) this.Job != (UnityEngine.Object) null)
      {
        JobParam job = (JobParam) null;
        if (instanceData != null && instanceData.CurrentJob != null)
          job = instanceData.CurrentJob.Param;
        MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.Job, job == null ? (string) null : AssetPath.JobIconSmall(job));
      }
      if ((UnityEngine.Object) this.UnitName != (UnityEngine.Object) null && (UnityEngine.Object) this.mNameText != (UnityEngine.Object) null)
      {
        if (GlobalVars.ShowUnitNames && instanceData != null)
        {
          this.UnitName.SetActive(true);
          this.mNameText.text = instanceData.UnitParam.name;
        }
        else
          this.UnitName.SetActive(false);
      }
      if (!MonoSingleton<GameManager>.Instance.IsTutorial() || instanceData == null || (!(MonoSingleton<GameManager>.Instance.GetNextTutorialStep() == "ShowUnitList") || !(instanceData.UnitID == "UN_V2_LOGI")))
        return;
      SGHighlightObject.Instance().highlightedObject = this.gameObject;
      SGHighlightObject.Instance().Highlight(string.Empty, "sg_tut_1.017", (SGHighlightObject.OnActivateCallback) null, EventDialogBubble.Anchors.BottomLeft, true, false, false);
    }

    public void SetSortValue(GameUtility.UnitSortModes mode, int value, bool isLevelActive = true)
    {
      if (!((UnityEngine.Object) this.SortBadge != (UnityEngine.Object) null))
        return;
      if (mode != GameUtility.UnitSortModes.Level && mode != GameUtility.UnitSortModes.Rarity && mode != GameUtility.UnitSortModes.Time)
      {
        if ((UnityEngine.Object) this.SortBadge.Value != (UnityEngine.Object) null)
          this.SortBadge.Value.text = value.ToString();
        if ((UnityEngine.Object) this.SortBadge.Icon != (UnityEngine.Object) null)
          this.SortBadge.Icon.sprite = GameSettings.Instance.GetUnitSortModeIcon(mode);
        this.SortBadge.gameObject.SetActive(true);
        this.mIsLvActive = isLevelActive;
      }
      else
      {
        this.SortBadge.gameObject.SetActive(false);
        this.mIsLvActive = true;
      }
    }

    public void ClearSortValue()
    {
      if (!((UnityEngine.Object) this.SortBadge != (UnityEngine.Object) null))
        return;
      this.SortBadge.gameObject.SetActive(false);
      this.mIsLvActive = true;
    }

    public void UpdatePartyWindow()
    {
      PartyWindow2 componentInParent = this.GetComponentInParent<PartyWindow2>();
      if (!((UnityEngine.Object) componentInParent != (UnityEngine.Object) null))
        return;
      componentInParent.Refresh(true);
    }
  }
}
