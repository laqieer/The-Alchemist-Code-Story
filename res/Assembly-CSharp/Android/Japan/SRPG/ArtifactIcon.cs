﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ArtifactIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class ArtifactIcon : BaseIcon
  {
    private int mLastLv = -1;
    private int mLastLvCap = -1;
    private int mLastExpNum = -1;
    public RawImage Icon;
    public Image Rarity;
    public Image RarityMax;
    public Text RarityText;
    public Text RarityMaxText;
    public Image Frame;
    public Text Lv;
    public Text LvCap;
    public Text PreLvCap;
    public Slider LvGauge;
    public Slider ExpGauge;
    public Slider PieceGauge;
    public Image Category;
    public GameObject Owner;
    public Image OwnerIcon;
    public Text DecCost;
    public Text DecKakeraNum;
    public Text TransmuteCost;
    public GameObject NotRarityUp;
    public GameObject CanRarityUp;
    public bool ForceMask;
    public ArtifactIcon.InstanceTypes InstanceType;
    public int DeriveTriggerIndex;
    [NonSerialized]
    public GameObject IndexBadge;
    public GameObject RarityUp;
    public GameObject CanCreate;
    public Image RarityUpBack;
    public Image CanCreateBack;
    public Image CanCreateGauge;
    public Image DefaultGauge;
    public Image DefaultBack;
    public Text RarityUpCost;
    public Text PieceNum;
    public Image[] NotCreateGrayIcon;
    public RawImage[] NotCreateGrayRawIcon;
    public GameObject Favorite;
    public GameObject LockMask;

    private void Start()
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

    public override void UpdateValue()
    {
      ArtifactData data = (ArtifactData) null;
      ArtifactParam artifactParam = (ArtifactParam) null;
      GameManager instance1 = MonoSingleton<GameManager>.Instance;
      if (this.InstanceType == ArtifactIcon.InstanceTypes.ArtifactData)
        data = DataSource.FindDataOfClass<ArtifactData>(this.gameObject, (ArtifactData) null);
      else if (this.InstanceType == ArtifactIcon.InstanceTypes.SkillAbilityDeriveParam)
      {
        SkillAbilityDeriveParam dataOfClass = DataSource.FindDataOfClass<SkillAbilityDeriveParam>(this.gameObject, (SkillAbilityDeriveParam) null);
        if (dataOfClass != null)
          artifactParam = dataOfClass.GetTriggerArtifactParam(this.DeriveTriggerIndex);
      }
      else if (this.InstanceType == ArtifactIcon.InstanceTypes.ArtifactDataOrParam)
      {
        data = DataSource.FindDataOfClass<ArtifactData>(this.gameObject, (ArtifactData) null);
        if (data == null)
          artifactParam = DataSource.FindDataOfClass<ArtifactParam>(this.gameObject, (ArtifactParam) null);
      }
      else
        artifactParam = DataSource.FindDataOfClass<ArtifactParam>(this.gameObject, (ArtifactParam) null);
      if ((UnityEngine.Object) this.Lv != (UnityEngine.Object) null)
      {
        if (data != null)
        {
          if ((int) data.Lv != this.mLastLv)
          {
            this.mLastLv = (int) data.Lv;
            this.Lv.text = data.Lv.ToString();
          }
          this.Lv.gameObject.SetActive(true);
        }
        else
          this.Lv.gameObject.SetActive(false);
      }
      if ((UnityEngine.Object) this.PreLvCap != (UnityEngine.Object) null)
      {
        if (data != null && (int) data.Rarity > 0)
        {
          this.PreLvCap.text = MonoSingleton<GameManager>.Instance.GetRarityParam((int) data.Rarity - 1).ArtifactLvCap.ToString();
          this.PreLvCap.gameObject.SetActive(true);
        }
        else
          this.PreLvCap.gameObject.SetActive(false);
      }
      if ((UnityEngine.Object) this.LvCap != (UnityEngine.Object) null)
      {
        if (data != null)
        {
          if ((int) data.LvCap != this.mLastLvCap)
          {
            this.mLastLvCap = (int) data.LvCap;
            this.LvCap.text = data.LvCap.ToString();
          }
          this.LvCap.gameObject.SetActive(true);
        }
        else
          this.LvCap.gameObject.SetActive(false);
      }
      if ((UnityEngine.Object) this.LvGauge != (UnityEngine.Object) null)
      {
        if (data != null)
        {
          if (data.Exp != this.mLastExpNum)
          {
            this.LvGauge.minValue = 1f;
            this.LvGauge.maxValue = (float) (int) data.LvCap;
            this.LvGauge.value = (float) (int) data.Lv;
          }
          this.LvGauge.gameObject.SetActive(true);
        }
        else
          this.LvGauge.gameObject.SetActive(false);
      }
      if ((UnityEngine.Object) this.ExpGauge != (UnityEngine.Object) null)
      {
        if (data != null)
        {
          if (data.Exp != this.mLastExpNum)
          {
            if ((int) data.Lv >= (int) data.LvCap)
            {
              this.ExpGauge.minValue = 0.0f;
              Slider expGauge = this.ExpGauge;
              float num1 = 1f;
              this.ExpGauge.value = num1;
              double num2 = (double) num1;
              expGauge.maxValue = (float) num2;
            }
            else
            {
              int showExp = data.GetShowExp();
              int nextExp = data.GetNextExp();
              this.ExpGauge.minValue = 0.0f;
              this.ExpGauge.maxValue = (float) (showExp + nextExp);
              this.ExpGauge.value = (float) showExp;
            }
          }
          this.ExpGauge.gameObject.SetActive(true);
        }
        else
          this.ExpGauge.gameObject.SetActive(false);
      }
      if ((UnityEngine.Object) this.PieceGauge != (UnityEngine.Object) null)
      {
        if (artifactParam != null)
        {
          this.PieceGauge.minValue = 0.0f;
          ItemData itemDataByItemId = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(artifactParam.kakera);
          this.PieceGauge.maxValue = (float) (int) MonoSingleton<GameManager>.Instance.GetRarityParam(artifactParam.rareini).ArtifactCreatePieceNum;
          this.PieceGauge.value = itemDataByItemId == null ? 0.0f : (float) itemDataByItemId.Num;
          this.PieceGauge.gameObject.SetActive(true);
        }
        else
          this.PieceGauge.gameObject.SetActive(false);
      }
      if ((UnityEngine.Object) this.Icon != (UnityEngine.Object) null)
      {
        if (data != null || artifactParam != null)
        {
          string path = AssetPath.ArtifactIcon(data == null ? artifactParam : data.ArtifactParam);
          instance1.ApplyTextureAsync(this.Icon, path);
        }
        else
        {
          instance1.CancelTextureLoadRequest(this.Icon);
          this.Icon.texture = (Texture) null;
        }
      }
      int index1 = 0;
      int index2 = 0;
      bool flag1 = false;
      if (data != null)
      {
        index1 = (int) data.Rarity;
        index2 = (int) data.RarityCap;
        flag1 = data.IsInspiration();
      }
      else if (artifactParam != null)
      {
        index1 = artifactParam.rareini;
        index2 = artifactParam.raremax;
      }
      if (data != null || artifactParam != null)
      {
        bool flag2 = data != null && data.CheckEnableRarityUp() == ArtifactData.RarityUpResults.Success;
        if ((UnityEngine.Object) this.RarityUp != (UnityEngine.Object) null)
          this.RarityUp.SetActive(flag2);
        if ((UnityEngine.Object) this.RarityUpBack != (UnityEngine.Object) null && (UnityEngine.Object) this.DefaultBack != (UnityEngine.Object) null)
        {
          this.RarityUpBack.enabled = flag2;
          this.DefaultBack.enabled = !flag2;
        }
        bool flag3 = false;
        if (artifactParam != null)
        {
          ItemData itemDataByItemId = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(artifactParam.kakera);
          if (itemDataByItemId != null)
          {
            RarityParam rarityParam = MonoSingleton<GameManager>.Instance.GetRarityParam(artifactParam.rareini);
            flag3 = itemDataByItemId.Num >= (int) rarityParam.ArtifactCreatePieceNum;
          }
          else
            flag3 = false;
        }
        if ((UnityEngine.Object) this.CanCreate != (UnityEngine.Object) null)
          this.CanCreate.SetActive(flag3);
        if ((UnityEngine.Object) this.CanCreateBack != (UnityEngine.Object) null && (UnityEngine.Object) this.DefaultBack != (UnityEngine.Object) null)
        {
          this.CanCreateBack.enabled = flag3;
          this.DefaultBack.enabled = !flag3;
        }
        if ((UnityEngine.Object) this.CanCreateGauge != (UnityEngine.Object) null && (UnityEngine.Object) this.DefaultBack != (UnityEngine.Object) null)
        {
          this.CanCreateGauge.enabled = flag3;
          this.DefaultBack.enabled = !flag3;
        }
        if (this.NotCreateGrayIcon != null && this.NotCreateGrayIcon.Length > 0)
        {
          if (flag3)
          {
            for (int index3 = 0; index3 < this.NotCreateGrayIcon.Length; ++index3)
              this.NotCreateGrayIcon[index3].color = Color.white;
          }
          else
          {
            for (int index3 = 0; index3 < this.NotCreateGrayIcon.Length; ++index3)
              this.NotCreateGrayIcon[index3].color = Color.cyan;
          }
        }
        if (this.NotCreateGrayRawIcon != null && this.NotCreateGrayRawIcon.Length > 0)
        {
          if (flag3)
          {
            for (int index3 = 0; index3 < this.NotCreateGrayRawIcon.Length; ++index3)
              this.NotCreateGrayRawIcon[index3].color = Color.white;
          }
          else
          {
            for (int index3 = 0; index3 < this.NotCreateGrayRawIcon.Length; ++index3)
              this.NotCreateGrayRawIcon[index3].color = Color.cyan;
          }
        }
        if (data != null && (UnityEngine.Object) this.NotRarityUp != (UnityEngine.Object) null && (UnityEngine.Object) this.CanRarityUp != (UnityEngine.Object) null)
        {
          bool flag4 = (int) data.Rarity == (int) data.RarityCap;
          this.NotRarityUp.SetActive(flag4);
          this.CanRarityUp.SetActive(!flag4);
        }
        if (data != null && (UnityEngine.Object) this.RarityUpCost != (UnityEngine.Object) null)
          this.RarityUpCost.text = data.GetKakeraNeedNum().ToString();
        if (artifactParam != null && (UnityEngine.Object) this.TransmuteCost != (UnityEngine.Object) null)
          this.TransmuteCost.text = (int) MonoSingleton<GameManager>.Instance.GetRarityParam(artifactParam.rareini).ArtifactCreatePieceNum.ToString();
        if ((UnityEngine.Object) this.PieceNum != (UnityEngine.Object) null)
        {
          ItemData itemData = data == null ? MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(artifactParam.kakera) : MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemParam(data.Kakera);
          if (itemData != null)
          {
            this.PieceNum.text = itemData.Num.ToString();
            if (data != null && data.CheckEnableRarityUp() == ArtifactData.RarityUpResults.Success)
              this.PieceNum.color = Color.yellow;
            else
              this.PieceNum.color = Color.white;
          }
          else
          {
            this.PieceNum.text = "0";
            this.PieceNum.color = Color.white;
          }
        }
        if ((UnityEngine.Object) this.Rarity != (UnityEngine.Object) null)
        {
          GameSettings instance2 = GameSettings.Instance;
          if ((UnityEngine.Object) instance2 != (UnityEngine.Object) null && index1 < instance2.ArtifactIcon_Rarity.Length)
            this.Rarity.sprite = instance2.ArtifactIcon_Rarity[index1];
        }
        if ((UnityEngine.Object) this.RarityMax != (UnityEngine.Object) null)
        {
          GameSettings instance2 = GameSettings.Instance;
          if ((UnityEngine.Object) instance2 != (UnityEngine.Object) null && index2 < instance2.ArtifactIcon_RarityBG.Length)
            this.RarityMax.sprite = instance2.ArtifactIcon_RarityBG[index2];
        }
        if ((UnityEngine.Object) this.RarityText != (UnityEngine.Object) null)
          this.RarityText.text = (index1 + 1).ToString();
        if ((UnityEngine.Object) this.RarityMaxText != (UnityEngine.Object) null)
          this.RarityMaxText.text = (index2 + 1).ToString();
        if ((UnityEngine.Object) this.Frame != (UnityEngine.Object) null)
        {
          GameSettings instance2 = GameSettings.Instance;
          Sprite[] spriteArray = flag1 ? instance2.ArtifactIcon_InspSkillFrames : instance2.ArtifactIcon_Frames;
          if ((UnityEngine.Object) instance2 != (UnityEngine.Object) null && index1 < spriteArray.Length)
            this.Frame.sprite = spriteArray[index1];
        }
        if ((UnityEngine.Object) this.Category != (UnityEngine.Object) null)
        {
          GameSettings instance2 = GameSettings.Instance;
          if ((UnityEngine.Object) instance2 != (UnityEngine.Object) null && (data != null || artifactParam != null))
          {
            switch (data == null ? artifactParam.type : data.ArtifactParam.type)
            {
              case ArtifactTypes.Arms:
                this.Category.sprite = instance2.ArtifactIcon_Weapon;
                break;
              case ArtifactTypes.Armor:
                this.Category.sprite = instance2.ArtifactIcon_Armor;
                break;
              case ArtifactTypes.Accessory:
                this.Category.sprite = instance2.ArtifactIcon_Misc;
                break;
            }
          }
        }
        if ((UnityEngine.Object) this.DecKakeraNum != (UnityEngine.Object) null)
          this.DecKakeraNum.text = data.GetKakeraChangeNum().ToString();
        if ((UnityEngine.Object) this.DecCost != (UnityEngine.Object) null)
          this.DecCost.text = data.RarityParam.ArtifactChangeCost.ToString();
      }
      else
      {
        if ((UnityEngine.Object) this.Rarity != (UnityEngine.Object) null)
          this.Rarity.sprite = (Sprite) null;
        if ((UnityEngine.Object) this.RarityMax != (UnityEngine.Object) null)
          this.RarityMax.sprite = (Sprite) null;
        if ((UnityEngine.Object) this.Frame != (UnityEngine.Object) null)
          this.Frame.sprite = (Sprite) null;
        if ((UnityEngine.Object) this.Category != (UnityEngine.Object) null)
          this.Category.sprite = (Sprite) null;
      }
      bool flag5 = false;
      if ((UnityEngine.Object) this.Owner != (UnityEngine.Object) null)
      {
        if (data != null && this.SetOwnerIcon(instance1, data))
        {
          this.Owner.SetActive(true);
          flag5 = true;
        }
        else
          this.Owner.SetActive(false);
      }
      if ((UnityEngine.Object) this.Favorite != (UnityEngine.Object) null)
      {
        if (data != null && data.IsFavorite)
        {
          this.Favorite.SetActive(true);
          flag5 = true;
        }
        else
          this.Favorite.SetActive(false);
      }
      if (this.ForceMask)
        flag5 = true;
      if ((UnityEngine.Object) this.LockMask != (UnityEngine.Object) null)
        this.LockMask.SetActive(flag5);
      if (data == null)
        return;
      this.mLastExpNum = data.Exp;
    }

    private bool SetOwnerIcon(GameManager gm, ArtifactData data)
    {
      UnitData unit;
      JobData job;
      if (!gm.Player.FindOwner(data, out unit, out job))
        return false;
      SpriteSheet spriteSheet = AssetManager.Load<SpriteSheet>("ItemIcon/small");
      ItemParam itemParam = gm.GetItemParam(unit.UnitParam.piece);
      if ((UnityEngine.Object) this.OwnerIcon != (UnityEngine.Object) null)
        this.OwnerIcon.sprite = spriteSheet.GetSprite(itemParam.icon);
      return true;
    }

    public enum InstanceTypes
    {
      ArtifactData,
      ArtifactParam,
      SkillAbilityDeriveParam,
      ArtifactDataOrParam,
    }
  }
}
