// Decompiled with JetBrains decompiler
// Type: SRPG.ArtifactListItemBadge
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class ArtifactListItemBadge : MonoBehaviour
  {
    private const float CHANGE_INTERBAL = 2f;
    [CustomGroup("バッジ")]
    [CustomField("クエスト", CustomFieldAttribute.Type.GameObject)]
    public GameObject m_Badge;
    [CustomGroup("バッジ")]
    [CustomField("闘技場", CustomFieldAttribute.Type.GameObject)]
    public GameObject m_Arena;
    [CustomGroup("バッジ")]
    [CustomField("傭兵", CustomFieldAttribute.Type.GameObject)]
    public GameObject m_Support;
    private ArtifactData m_ArtifactData;
    private List<GameObject> m_DispOn = new List<GameObject>();
    private List<GameObject> m_DispOff = new List<GameObject>();
    private int m_Index;
    private float m_Time;
    private List<UnitData> mQuestUnits = new List<UnitData>();
    private List<UnitData> mArenaUnits = new List<UnitData>();
    private List<UnitData> mArenaDefUnits = new List<UnitData>();
    private List<UnitData> mSupportUnits = new List<UnitData>();

    private void Awake()
    {
      this.mQuestUnits = MonoSingleton<GameManager>.Instance.Player.Units;
      this.mArenaUnits = UnitOverWriteUtility.GetPartyUnits(eOverWritePartyType.Arena, false);
      this.mArenaDefUnits = UnitOverWriteUtility.GetPartyUnits(eOverWritePartyType.ArenaDef, false);
      this.mSupportUnits = UnitOverWriteUtility.GetSupportAllUnits(false);
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
      this.m_ArtifactData = DataSource.FindDataOfClass<ArtifactData>(((Component) this).gameObject, (ArtifactData) null);
      if (this.m_ArtifactData != null)
      {
        UnitData owner1 = (UnitData) null;
        JobData owner_job = (JobData) null;
        if (Object.op_Inequality((Object) this.m_Badge, (Object) null))
        {
          if (this.m_ArtifactData.GetOwner(this.mQuestUnits, out owner1, out owner_job))
            this.m_DispOn.Add(this.m_Badge);
          else
            this.m_DispOff.Add(this.m_Badge);
        }
        if (Object.op_Inequality((Object) this.m_Arena, (Object) null))
        {
          bool owner2 = this.m_ArtifactData.GetOwner(this.mArenaUnits, out owner1, out owner_job);
          bool owner3 = this.m_ArtifactData.GetOwner(this.mArenaDefUnits, out owner1, out owner_job);
          if (owner2 || owner3)
            this.m_DispOn.Add(this.m_Arena);
          else
            this.m_DispOff.Add(this.m_Arena);
        }
        if (Object.op_Inequality((Object) this.m_Support, (Object) null))
        {
          if (this.m_ArtifactData.GetOwner(this.mSupportUnits, out owner1, out owner_job))
            this.m_DispOn.Add(this.m_Support);
          else
            this.m_DispOff.Add(this.m_Support);
        }
      }
      else
      {
        if (Object.op_Inequality((Object) this.m_Badge, (Object) null))
          this.m_DispOff.Add(this.m_Badge);
        if (Object.op_Inequality((Object) this.m_Arena, (Object) null))
          this.m_DispOff.Add(this.m_Arena);
        if (Object.op_Inequality((Object) this.m_Support, (Object) null))
          this.m_DispOff.Add(this.m_Support);
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
