// Decompiled with JetBrains decompiler
// Type: SRPG.ArtifactIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
      if (data != null)
      {
        index1 = (int) data.Rarity;
        index2 = (int) data.RarityCap;
      }
      else if (artifactParam != null)
      {
        index1 = artifactParam.rareini;
        index2 = artifactParam.raremax;
      }
      if (data != null || artifactParam != null)
      {
        bool flag1 = data != null && data.CheckEnableRarityUp() == ArtifactData.RarityUpResults.Success;
        if ((UnityEngine.Object) this.RarityUp != (UnityEngine.Object) null)
          this.RarityUp.SetActive(flag1);
        if ((UnityEngine.Object) this.RarityUpBack != (UnityEngine.Object) null && (UnityEngine.Object) this.DefaultBack != (UnityEngine.Object) null)
        {
          this.RarityUpBack.enabled = flag1;
          this.DefaultBack.enabled = !flag1;
        }
        bool flag2 = false;
        if (artifactParam != null)
        {
          ItemData itemDataByItemId = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(artifactParam.kakera);
          if (itemDataByItemId != null)
          {
            RarityParam rarityParam = MonoSingleton<GameManager>.Instance.GetRarityParam(artifactParam.rareini);
            flag2 = itemDataByItemId.Num >= (int) rarityParam.ArtifactCreatePieceNum;
          }
          else
            flag2 = false;
        }
        if ((UnityEngine.Object) this.CanCreate != (UnityEngine.Object) null)
          this.CanCreate.SetActive(flag2);
        if ((UnityEngine.Object) this.CanCreateBack != (UnityEngine.Object) null && (UnityEngine.Object) this.DefaultBack != (UnityEngine.Object) null)
        {
          this.CanCreateBack.enabled = flag2;
          this.DefaultBack.enabled = !flag2;
        }
        if ((UnityEngine.Object) this.CanCreateGauge != (UnityEngine.Object) null && (UnityEngine.Object) this.DefaultBack != (UnityEngine.Object) null)
        {
          this.CanCreateGauge.enabled = flag2;
          this.DefaultBack.enabled = !flag2;
        }
        if (this.NotCreateGrayIcon != null && this.NotCreateGrayIcon.Length > 0)
        {
          if (flag2)
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
          if (flag2)
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
          bool flag3 = (int) data.Rarity == (int) data.RarityCap;
          this.NotRarityUp.SetActive(flag3);
          this.CanRarityUp.SetActive(!flag3);
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
          if ((UnityEngine.Object) instance2 != (UnityEngine.Object) null && index1 < instance2.ArtifactIcon_Frames.Length)
            this.Frame.sprite = instance2.ArtifactIcon_Frames[index1];
        }
        if ((UnityEngine.Object) this.Category != (UnityEngine.Object) null)
        {
          GameSettings instance2 = GameSettings.Instance;
          if ((UnityEngine.Object) instance2 != (UnityEngine.Object) null && (data != null || artifactParam != null))
          {
            switch (data == null ? (int) artifactParam.type : (int) data.ArtifactParam.type)
            {
              case 1:
                this.Category.sprite = instance2.ArtifactIcon_Weapon;
                break;
              case 2:
                this.Category.sprite = instance2.ArtifactIcon_Armor;
                break;
              case 3:
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
      bool flag = false;
      if ((UnityEngine.Object) this.Owner != (UnityEngine.Object) null)
      {
        if (data != null && this.SetOwnerIcon(instance1, data))
        {
          this.Owner.SetActive(true);
          flag = true;
        }
        else
          this.Owner.SetActive(false);
      }
      if ((UnityEngine.Object) this.Favorite != (UnityEngine.Object) null)
      {
        if (data != null && data.IsFavorite)
        {
          this.Favorite.SetActive(true);
          flag = true;
        }
        else
          this.Favorite.SetActive(false);
      }
      if (this.ForceMask)
        flag = true;
      if ((UnityEngine.Object) this.LockMask != (UnityEngine.Object) null)
        this.LockMask.SetActive(flag);
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
      ItemParam itemParam = gm.GetItemParam((string) unit.UnitParam.piece);
      if ((UnityEngine.Object) this.OwnerIcon != (UnityEngine.Object) null)
        this.OwnerIcon.sprite = spriteSheet.GetSprite((string) itemParam.icon);
      return true;
    }

    public enum InstanceTypes
    {
      ArtifactData,
      ArtifactParam,
    }
  }
}
