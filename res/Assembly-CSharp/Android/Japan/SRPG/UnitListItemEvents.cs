// Decompiled with JetBrains decompiler
// Type: SRPG.UnitListItemEvents
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class UnitListItemEvents : ListItemEvents
  {
    private const float CHANGE_INTERBAL = 2f;
    public Image[] EqIcons;
    public Image[] AttrIcons;
    public GameObject Badge;
    public GameObject CharacterQuestBadge;
    public GameObject FavoriteBadge;
    public bool DispAlternate;
    private int m_Index;
    private float m_Time;

    private void OnEnable()
    {
      this.Refresh();
    }

    private void Start()
    {
      if (!((UnityEngine.Object) this.Badge != (UnityEngine.Object) null))
        return;
      this.Badge.SetActive(false);
    }

    public void Refresh()
    {
      UnitData dataOfClass = DataSource.FindDataOfClass<UnitData>(this.gameObject, (UnitData) null);
      if (dataOfClass == null)
        return;
      EquipData[] currentEquips = dataOfClass.CurrentEquips;
      if (currentEquips != null && this.EqIcons != null && (this.EqIcons.Length >= currentEquips.Length && this.AttrIcons != null) && this.AttrIcons.Length >= currentEquips.Length)
      {
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        SpriteSheet spriteSheet = AssetManager.Load<SpriteSheet>("ItemIcon/small");
        for (int index = 0; index < currentEquips.Length; ++index)
        {
          EquipData equipData = currentEquips[index];
          if (!((UnityEngine.Object) this.EqIcons[index] == (UnityEngine.Object) null) && !((UnityEngine.Object) this.AttrIcons[index] == (UnityEngine.Object) null))
          {
            this.EqIcons[index].gameObject.SetActive(false);
            this.AttrIcons[index].gameObject.SetActive(false);
            if (equipData == null || !equipData.IsValid())
            {
              this.EqIcons[index].sprite = (Sprite) null;
            }
            else
            {
              this.EqIcons[index].gameObject.SetActive(true);
              this.EqIcons[index].sprite = spriteSheet.GetSprite(equipData.ItemID);
              if (!equipData.IsEquiped())
              {
                this.AttrIcons[index].gameObject.SetActive(true);
                this.AttrIcons[index].sprite = !player.HasItem(equipData.ItemID) ? (!player.CheckEnableCreateItem(equipData.ItemParam, true, 1, (NeedEquipItemList) null) ? spriteSheet.GetSprite("plus2") : spriteSheet.GetSprite(equipData.ItemParam.equipLv <= dataOfClass.Lv ? "plus0" : "plus1")) : spriteSheet.GetSprite(equipData.ItemParam.equipLv <= dataOfClass.Lv ? "plus0" : "plus1");
              }
            }
          }
        }
      }
      this.m_Time = 2f;
      this.Update();
      GameParameter.UpdateAll(this.gameObject);
    }

    private void Update()
    {
      if (!this.DispAlternate)
        this.UpdateBadgeDefault();
      else
        this.UpdateBadgeAlternate();
    }

    private void UpdateBadgeDefault()
    {
      UnitData dataOfClass1 = DataSource.FindDataOfClass<UnitData>(this.gameObject, (UnitData) null);
      if (dataOfClass1 != null)
      {
        if ((UnityEngine.Object) this.Badge != (UnityEngine.Object) null)
          this.Badge.SetActive(dataOfClass1.BadgeState != (UnitBadgeTypes) 0);
        if ((UnityEngine.Object) this.CharacterQuestBadge != (UnityEngine.Object) null)
          this.CharacterQuestBadge.SetActive(dataOfClass1.IsOpenCharacterQuest() && dataOfClass1.GetCurrentCharaEpisodeData() != null);
        if (!((UnityEngine.Object) this.FavoriteBadge != (UnityEngine.Object) null))
          return;
        this.FavoriteBadge.SetActive(dataOfClass1.IsFavorite);
      }
      else
      {
        UnitParam dataOfClass2 = DataSource.FindDataOfClass<UnitParam>(this.gameObject, (UnitParam) null);
        if (dataOfClass2 == null || !((UnityEngine.Object) this.Badge != (UnityEngine.Object) null))
          return;
        this.Badge.SetActive(MonoSingleton<GameManager>.Instance.CheckEnableUnitUnlock(dataOfClass2));
      }
    }

    private void UpdateBadgeAlternate()
    {
      List<GameObject> gameObjectList1 = new List<GameObject>();
      List<GameObject> gameObjectList2 = new List<GameObject>();
      UnitData dataOfClass1 = DataSource.FindDataOfClass<UnitData>(this.gameObject, (UnitData) null);
      if (dataOfClass1 != null)
      {
        if ((UnityEngine.Object) this.Badge != (UnityEngine.Object) null)
        {
          if (dataOfClass1.BadgeState != (UnitBadgeTypes) 0)
            gameObjectList1.Add(this.Badge);
          else
            gameObjectList2.Add(this.Badge);
        }
        if ((UnityEngine.Object) this.CharacterQuestBadge != (UnityEngine.Object) null)
        {
          if (dataOfClass1.IsOpenCharacterQuest() && dataOfClass1.GetCurrentCharaEpisodeData() != null)
            gameObjectList1.Add(this.CharacterQuestBadge);
          else
            gameObjectList2.Add(this.CharacterQuestBadge);
        }
        if ((UnityEngine.Object) this.FavoriteBadge != (UnityEngine.Object) null)
        {
          if (dataOfClass1.IsFavorite)
            gameObjectList1.Add(this.FavoriteBadge);
          else
            gameObjectList2.Add(this.FavoriteBadge);
        }
      }
      else
      {
        UnitParam dataOfClass2 = DataSource.FindDataOfClass<UnitParam>(this.gameObject, (UnitParam) null);
        if (dataOfClass2 != null)
        {
          if ((UnityEngine.Object) this.Badge != (UnityEngine.Object) null)
          {
            if (MonoSingleton<GameManager>.Instance.CheckEnableUnitUnlock(dataOfClass2))
              gameObjectList1.Add(this.Badge);
            else
              gameObjectList2.Add(this.Badge);
          }
          if ((UnityEngine.Object) this.CharacterQuestBadge != (UnityEngine.Object) null)
            gameObjectList2.Add(this.CharacterQuestBadge);
          if ((UnityEngine.Object) this.FavoriteBadge != (UnityEngine.Object) null)
            gameObjectList2.Add(this.FavoriteBadge);
        }
      }
      for (int index = 0; index < gameObjectList2.Count; ++index)
      {
        if ((UnityEngine.Object) gameObjectList2[index] != (UnityEngine.Object) null)
          gameObjectList2[index].SetActive(false);
      }
      this.m_Time += Time.deltaTime;
      if ((double) this.m_Time > 2.0)
      {
        this.m_Time -= 2f;
        ++this.m_Index;
        if (this.m_Index >= gameObjectList1.Count)
          this.m_Index = 0;
      }
      for (int index = 0; index < gameObjectList1.Count; ++index)
      {
        if ((UnityEngine.Object) gameObjectList1[index] != (UnityEngine.Object) null)
          gameObjectList1[index].SetActive(this.m_Index == index);
      }
    }
  }
}
