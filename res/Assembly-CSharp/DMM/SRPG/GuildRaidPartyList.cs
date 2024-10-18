// Decompiled with JetBrains decompiler
// Type: SRPG.GuildRaidPartyList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class GuildRaidPartyList : MonoBehaviour
  {
    [SerializeField]
    private GenericSlot UnitSlotTemplate;
    [SerializeField]
    private Transform MainMemberHolder;
    [SerializeField]
    private Transform SubMemberHolder;
    [SerializeField]
    private GenericSlot CardSlotTemplate;
    [SerializeField]
    private Transform MainMemberCardHolder;
    [SerializeField]
    private Transform SubMemberCardHolder;

    public void Setup(List<UnitData> party)
    {
      if (party == null || Object.op_Equality((Object) this.UnitSlotTemplate, (Object) null) || Object.op_Equality((Object) this.MainMemberHolder, (Object) null) || Object.op_Equality((Object) this.SubMemberCardHolder, (Object) null))
        return;
      ((Component) this.UnitSlotTemplate).gameObject.SetActive(false);
      if (Object.op_Equality((Object) this.CardSlotTemplate, (Object) null) || Object.op_Equality((Object) this.MainMemberCardHolder, (Object) null) || Object.op_Equality((Object) this.SubMemberCardHolder, (Object) null))
        return;
      ((Component) this.CardSlotTemplate).gameObject.SetActive(false);
      PartyData partyData = new PartyData(PlayerPartyTypes.GuildRaid);
      UnitData leader = party.Count <= 0 ? (UnitData) null : party[0];
      for (int index1 = 0; index1 < partyData.MAX_MAINMEMBER; ++index1)
      {
        int index2 = index1;
        PartySlotData data1 = new PartySlotData(PartySlotType.Free, (string) null, (PartySlotIndex) index2);
        GenericSlot genericSlot1 = Object.Instantiate<GenericSlot>(this.UnitSlotTemplate, this.MainMemberHolder);
        ((Component) genericSlot1).gameObject.SetActive(true);
        genericSlot1.SetSlotData<PartySlotData>(data1);
        UnitData data2 = party.Count <= index2 ? (UnitData) null : party[index2];
        genericSlot1.SetSlotData<UnitData>(data2);
        GenericSlot genericSlot2 = Object.Instantiate<GenericSlot>(this.CardSlotTemplate, this.MainMemberCardHolder);
        ((Component) genericSlot2).gameObject.SetActive(true);
        ((Component) genericSlot2).GetComponent<ConceptCardSlot>().SetLeaderUnit(leader);
        genericSlot2.SetSlotData<PartySlotData>(data1);
        genericSlot2.SetSlotData<ConceptCardData>(data2?.MainConceptCard);
        ((Component) genericSlot2).GetComponent<ConceptCardIcon>().Setup(data2?.MainConceptCard);
      }
      for (int index3 = 0; index3 < partyData.MAX_SUBMEMBER; ++index3)
      {
        int index4 = index3 + partyData.MAX_MAINMEMBER;
        PartySlotData data3 = new PartySlotData(PartySlotType.Free, (string) null, (PartySlotIndex) index4);
        GenericSlot genericSlot3 = Object.Instantiate<GenericSlot>(this.UnitSlotTemplate, this.SubMemberHolder);
        ((Component) genericSlot3).gameObject.SetActive(true);
        genericSlot3.SetSlotData<PartySlotData>(data3);
        UnitData data4 = party.Count <= index4 ? (UnitData) null : party[index4];
        genericSlot3.SetSlotData<UnitData>(data4);
        GenericSlot genericSlot4 = Object.Instantiate<GenericSlot>(this.CardSlotTemplate, this.SubMemberCardHolder);
        ((Component) genericSlot4).gameObject.SetActive(true);
        ((Component) genericSlot4).GetComponent<ConceptCardSlot>().SetLeaderUnit(leader);
        genericSlot4.SetSlotData<PartySlotData>(data3);
        genericSlot4.SetSlotData<ConceptCardData>(data4?.MainConceptCard);
        ((Component) genericSlot4).GetComponent<ConceptCardIcon>().Setup(data4?.MainConceptCard);
      }
    }
  }
}
