// Decompiled with JetBrains decompiler
// Type: SRPG.RuneIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class RuneIcon : MonoBehaviour
  {
    [SerializeField]
    private Button mButton;
    [SerializeField]
    private Image mCheckIcon;
    [SerializeField]
    private ImageSpriteSheet mSetType;
    [SerializeField]
    private ImageArray mEvoImage;
    [SerializeField]
    private ImageArray mEnhanceImage;
    [SerializeField]
    private GameObject mOwnerIconParent;
    [SerializeField]
    private Image mOwnerIcon;
    [SerializeField]
    private Image mSelectObject;
    [SerializeField]
    private Text mStatusCount;
    [SerializeField]
    private GameObject mFavorite;
    [SerializeField]
    private bool mIsReplaseRune;
    private BindRuneData mRune;
    private const float CHANGE_INTERVAL = 2f;
    private List<GameObject> mBadgeObjects = new List<GameObject>();
    private float mStartupTime;
    private int mBadgeIndex;

    public BindRuneData Rune => this.mRune;

    private void Awake() => this.mStartupTime = TimeManager.RealTimeSinceStartup;

    private void Update() => this.UpdateBadge();

    public void Setup(BindRuneData rune, bool is_owner_disable = false)
    {
      this.mRune = rune;
      this.Refresh();
      this.SetupBadge();
    }

    public void Refresh()
    {
      DataSource.Bind<BindRuneData>(((Component) this).gameObject, this.mRune);
      GameParameter.UpdateAll(((Component) this).gameObject);
      this.HideBadgeAll();
      this.mBadgeObjects.Clear();
      this.RefreshCheckIcon();
      this.RefreshSetType();
      this.RefreshEvoImage();
      this.RefreshEnhanceImage();
      this.RefreshOwnerIcon();
      this.RefreshSelectFrame();
      this.RefreshBaseStatusCount();
      this.RefreshFavorite();
    }

    private void RefreshSetType()
    {
      if (this.mRune == null || Object.op_Equality((Object) this.mSetType, (Object) null))
        return;
      RuneData rune = this.mRune.Rune;
      if (rune == null)
        return;
      RuneParam runeParam = rune.RuneParam;
      if (runeParam == null)
        return;
      this.mSetType.SetSprite(runeParam.SetEffTypeIconIndex.ToString());
    }

    private void RefreshEvoImage()
    {
      if (this.mRune == null || Object.op_Equality((Object) this.mEvoImage, (Object) null))
        return;
      RuneData rune = this.mRune.Rune;
      if (rune == null)
        return;
      if (rune.EvoNum <= 0)
      {
        ((Behaviour) this.mEvoImage).enabled = false;
        this.mEvoImage.ImageIndex = 0;
      }
      else
      {
        ((Behaviour) this.mEvoImage).enabled = true;
        this.mEvoImage.ImageIndex = rune.EvoNum - 1;
      }
    }

    private void RefreshEnhanceImage()
    {
      if (this.mRune == null)
        return;
      RuneData rune = this.mRune.Rune;
      if (rune == null || !Object.op_Inequality((Object) this.mEnhanceImage, (Object) null))
        return;
      ((Behaviour) this.mEnhanceImage).enabled = rune.EnhanceNum > 0;
      this.mEnhanceImage.ImageIndex = rune.EnhanceNum;
    }

    private void RefreshOwnerIcon()
    {
      if (this.mRune == null || Object.op_Equality((Object) this.mOwnerIcon, (Object) null))
        return;
      RuneData rune = this.mRune.Rune;
      if (rune == null)
        return;
      UnitData owner = rune.GetOwner();
      if (owner != null)
      {
        if (Object.op_Inequality((Object) this.mOwnerIconParent, (Object) null))
        {
          this.mOwnerIconParent.SetActive(true);
          this.mBadgeObjects.Add(this.mOwnerIconParent);
        }
        this.mOwnerIcon.sprite = AssetManager.Load<SpriteSheet>("ItemIcon/small").GetSprite(MonoSingleton<GameManager>.Instance.GetItemParam(owner.UnitParam.piece).icon);
        if (this.mIsReplaseRune)
        {
          if (!Object.op_Inequality((Object) this.mButton, (Object) null))
            return;
          ((Selectable) this.mButton).interactable = true;
        }
        else
        {
          if (!Object.op_Inequality((Object) this.mButton, (Object) null))
            return;
          ((Selectable) this.mButton).interactable = !this.mRune.is_owner_disable;
        }
      }
      else
      {
        if (Object.op_Inequality((Object) this.mOwnerIconParent, (Object) null))
          this.mOwnerIconParent.SetActive(false);
        this.mOwnerIcon.sprite = (Sprite) null;
        if (!Object.op_Inequality((Object) this.mButton, (Object) null))
          return;
        ((Selectable) this.mButton).interactable = true;
      }
    }

    private void RefreshCheckIcon()
    {
      if (this.mRune == null || !Object.op_Inequality((Object) this.mCheckIcon, (Object) null))
        return;
      ((Behaviour) this.mCheckIcon).enabled = this.mRune.is_check;
    }

    public void RefreshSelectFrame()
    {
      if (this.mRune == null || !Object.op_Inequality((Object) this.mSelectObject, (Object) null))
        return;
      ((Behaviour) this.mSelectObject).enabled = this.mRune.is_selected;
    }

    public UnitData GetOwner()
    {
      if (this.mRune == null)
        return (UnitData) null;
      return this.mRune.Rune?.GetOwner();
    }

    public void RefreshEnableParam(bool enable)
    {
      Button component = ((Component) ((Component) this).transform).gameObject.GetComponent<Button>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      ((Selectable) component).interactable = enable;
    }

    public void RefreshBaseStatusCount()
    {
      if (this.mRune == null || !Object.op_Implicit((Object) this.mStatusCount))
        return;
      this.mStatusCount.text = this.mRune.Rune.DisplayRuneBaseParam();
    }

    private void RefreshFavorite()
    {
      if (!Object.op_Inequality((Object) this.mFavorite, (Object) null))
        return;
      bool flag = false;
      if (this.mRune != null && this.mRune.Rune != null)
      {
        flag = this.mRune.Rune.IsFavorite;
        if (flag)
          this.mBadgeObjects.Add(this.mFavorite);
      }
      this.mFavorite.SetActive(flag);
    }

    private void SetupBadge()
    {
      this.mBadgeIndex = -1;
      this.HideBadgeAll();
      this.UpdateBadge();
    }

    private void UpdateBadge()
    {
      if (this.mBadgeObjects.Count <= 0)
        return;
      int num = (int) ((double) (TimeManager.RealTimeSinceStartup - this.mStartupTime) / 2.0) % this.mBadgeObjects.Count;
      if (this.mBadgeIndex == num)
        return;
      this.HideBadgeAll();
      this.mBadgeIndex = num;
      GameUtility.SetGameObjectActive(this.mBadgeObjects[this.mBadgeIndex], true);
    }

    private void HideBadgeAll()
    {
      for (int index = 0; index < this.mBadgeObjects.Count; ++index)
        GameUtility.SetGameObjectActive(this.mBadgeObjects[index], false);
    }
  }
}
