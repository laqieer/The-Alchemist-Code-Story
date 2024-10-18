// Decompiled with JetBrains decompiler
// Type: SRPG.UnitListItemBadge
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class UnitListItemBadge : MonoBehaviour
  {
    private const float CHANGE_INTERBAL = 2f;
    [CustomGroup("バッジ")]
    [CustomField("強化", CustomFieldAttribute.Type.GameObject)]
    public GameObject m_Badge;
    [CustomGroup("バッジ")]
    [CustomField("ストーリー", CustomFieldAttribute.Type.GameObject)]
    public GameObject m_CharacterQuest;
    [CustomGroup("バッジ")]
    [CustomField("お気に入り", CustomFieldAttribute.Type.GameObject)]
    public GameObject m_Favorite;
    private UnitParam m_UnitParam;
    private UnitData m_UnitData;
    private List<GameObject> m_DispOn = new List<GameObject>();
    private List<GameObject> m_DispOff = new List<GameObject>();
    private int m_Index;
    private float m_Time;

    private void Start()
    {
    }

    private void Update() => this.UpdateBadge();

    private void UpdateBadge()
    {
      if (this.m_DispOn.Count <= 1)
        return;
      this.m_Time += Time.deltaTime;
      if ((double) this.m_Time <= 2.0)
        return;
      this.m_Time -= 2f;
      ++this.m_Index;
      if (this.m_Index >= this.m_DispOn.Count)
        this.m_Index = 0;
      this.UpdateBadgeAlternate(this.m_Index);
    }

    private void UpdateBadgeAlternate(int index)
    {
      for (int index1 = 0; index1 < this.m_DispOff.Count; ++index1)
      {
        if (Object.op_Inequality((Object) this.m_DispOff[index1], (Object) null) && this.m_DispOff[index1].activeSelf)
          this.m_DispOff[index1].SetActive(false);
      }
      for (int index2 = 0; index2 < this.m_DispOn.Count; ++index2)
      {
        if (Object.op_Inequality((Object) this.m_DispOn[index2], (Object) null))
        {
          bool flag = index == index2;
          if (this.m_DispOn[index2].activeSelf != flag)
            this.m_DispOn[index2].SetActive(flag);
        }
      }
    }

    public void Refresh()
    {
      this.m_DispOn.Clear();
      this.m_DispOff.Clear();
      this.m_UnitParam = DataSource.FindDataOfClass<UnitParam>(((Component) this).gameObject, (UnitParam) null);
      this.m_UnitData = DataSource.FindDataOfClass<UnitData>(((Component) this).gameObject, (UnitData) null);
      if (this.m_UnitData != null)
      {
        if (Object.op_Inequality((Object) this.m_Badge, (Object) null))
        {
          if (this.m_UnitData.BadgeState != (UnitBadgeTypes) 0)
            this.m_DispOn.Add(this.m_Badge);
          else
            this.m_DispOff.Add(this.m_Badge);
        }
        if (Object.op_Inequality((Object) this.m_CharacterQuest, (Object) null))
        {
          if (this.m_UnitData.IsOpenCharacterQuest() && this.m_UnitData.GetCurrentCharaEpisodeData() != null)
            this.m_DispOn.Add(this.m_CharacterQuest);
          else
            this.m_DispOff.Add(this.m_CharacterQuest);
        }
        if (Object.op_Inequality((Object) this.m_Favorite, (Object) null))
        {
          if (this.m_UnitData.IsFavorite)
            this.m_DispOn.Add(this.m_Favorite);
          else
            this.m_DispOff.Add(this.m_Favorite);
        }
      }
      else if (this.m_UnitParam != null)
      {
        if (Object.op_Inequality((Object) this.m_Badge, (Object) null))
        {
          if (MonoSingleton<GameManager>.Instance.CheckEnableUnitUnlock(this.m_UnitParam))
            this.m_DispOn.Add(this.m_Badge);
          else
            this.m_DispOff.Add(this.m_Badge);
        }
        if (Object.op_Inequality((Object) this.m_CharacterQuest, (Object) null))
          this.m_DispOff.Add(this.m_CharacterQuest);
        if (Object.op_Inequality((Object) this.m_Favorite, (Object) null))
          this.m_DispOff.Add(this.m_Favorite);
      }
      else
      {
        if (Object.op_Inequality((Object) this.m_Badge, (Object) null))
          this.m_DispOff.Add(this.m_Badge);
        if (Object.op_Inequality((Object) this.m_CharacterQuest, (Object) null))
          this.m_DispOff.Add(this.m_CharacterQuest);
        if (Object.op_Inequality((Object) this.m_Favorite, (Object) null))
          this.m_DispOff.Add(this.m_Favorite);
      }
      this.m_Time = 0.0f;
      this.m_Index = 0;
      this.UpdateBadgeAlternate(0);
    }

    private void OnEnable() => this.Refresh();

    private void OnDisable()
    {
    }
  }
}
