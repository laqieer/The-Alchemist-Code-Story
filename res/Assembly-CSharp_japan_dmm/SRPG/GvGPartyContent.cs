// Decompiled with JetBrains decompiler
// Type: SRPG.GvGPartyContent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class GvGPartyContent : MonoBehaviour
  {
    [SerializeField]
    private bool IsSelf;
    [Space(10f)]
    [SerializeField]
    private string SVB_HpbGaugeName = "HP_Gauge";
    [Space(10f)]
    [SerializeField]
    private string SVB_HDeadName = "dead";
    [Space(10f)]
    [SerializeField]
    private GenericSlot UnitSlotTemplate;
    [SerializeField]
    private GenericSlot ConceptCardSlotTemplate;
    [Space(10f)]
    [SerializeField]
    private Text TeamText;
    [SerializeField]
    private ImageArray RoleImageArray;
    [SerializeField]
    private Text PlayerNameText;
    [SerializeField]
    private Slider StaminaSlider;
    [SerializeField]
    private ImageArray StaminaImageArray;
    [Space(10f)]
    [SerializeField]
    private GameObject mHpGauge;
    [SerializeField]
    private Button UnitSlotEditorButton;
    [SerializeField]
    private Button ConceptCardSlotEditorButton;
    [Space(10f)]
    [SerializeField]
    private Text BeatNumText;
    private List<GenericSlot> UnitList = new List<GenericSlot>();
    private List<GenericSlot> CardList = new List<GenericSlot>();

    public GvGParty Party { get; private set; }

    private void Awake()
    {
      GameUtility.SetGameObjectActive((Component) this.UnitSlotTemplate, false);
      GameUtility.SetGameObjectActive((Component) this.ConceptCardSlotTemplate, false);
      GameUtility.SetGameObjectActive(this.mHpGauge, false);
    }

    public bool Setup(GvGNodeData node, int number, GvGParty party)
    {
      return node != null && this.SetupParty(node, number, party);
    }

    public bool SetupParty(GvGNodeData node, int number, GvGParty party)
    {
      if (party == null)
        return false;
      this.Party = party;
      this.UnitList.ForEach((Action<GenericSlot>) (u => UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) u).gameObject)));
      this.CardList.ForEach((Action<GenericSlot>) (c => UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) c).gameObject)));
      this.UnitList.Clear();
      this.CardList.Clear();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TeamText, (UnityEngine.Object) null))
        this.TeamText.text = string.Format(LocalizedText.Get("sys.GVG_PARTY_TEAM_TITLE"), (object) number);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RoleImageArray, (UnityEngine.Object) null))
        this.RoleImageArray.ImageIndex = Mathf.Max(0, (int) (party.RoleId - 1));
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PlayerNameText, (UnityEngine.Object) null))
        this.PlayerNameText.text = party.PlayerName;
      GvGManager.Instance.GetStaminaImageIndex(node, this.StaminaSlider, this.StaminaImageArray, this.Party.WinNum);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitSlotEditorButton, (UnityEngine.Object) null))
        ((Selectable) this.UnitSlotEditorButton).interactable = party.WinNum == 0;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ConceptCardSlotEditorButton, (UnityEngine.Object) null))
        ((Selectable) this.ConceptCardSlotEditorButton).interactable = party.WinNum == 0;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BeatNumText, (UnityEngine.Object) null))
        this.BeatNumText.text = string.Format(LocalizedText.Get("sys.GVG_TEXT_BEATNUM", (object) party.BeatNum));
      for (int index = 0; index < 3; ++index)
      {
        PartySlotData data = new PartySlotData(PartySlotType.Free, (string) null, (PartySlotIndex) index);
        data.Index = (PartySlotIndex) index;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitSlotTemplate, (UnityEngine.Object) null))
        {
          GenericSlot genericSlot = UnityEngine.Object.Instantiate<GenericSlot>(this.UnitSlotTemplate, ((Component) this.UnitSlotTemplate).transform.parent);
          genericSlot.SetSlotData<PartySlotData>(data);
          ((Component) genericSlot).gameObject.SetActive(true);
          this.UnitList.Add(genericSlot);
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ConceptCardSlotTemplate, (UnityEngine.Object) null))
        {
          GenericSlot genericSlot = UnityEngine.Object.Instantiate<GenericSlot>(this.ConceptCardSlotTemplate, ((Component) this.ConceptCardSlotTemplate).transform.parent);
          genericSlot.SetSlotData<PartySlotData>(data);
          ((Component) genericSlot).gameObject.SetActive(true);
          this.CardList.Add(genericSlot);
        }
      }
      UnitData leader = (UnitData) null;
      for (int index = 0; index < this.Party.Units.Count; ++index)
      {
        GvGPartyUnit unit1 = this.Party.Units[index];
        if (this.IsSelf && party.WinNum == 0)
          unit1.TempFlags |= UnitData.TemporaryFlags.TemporaryUnitData | UnitData.TemporaryFlags.AllowJobChange;
        if (index == 0)
          leader = (UnitData) unit1;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitSlotTemplate, (UnityEngine.Object) null))
        {
          GenericSlot unit2 = this.UnitList[index];
          unit2.SetSlotData<UnitData>((UnitData) unit1);
          this.UnitList.Add(unit2);
          if (this.IsSelf && party.WinNum == 0)
            unit2.SetSlotData<PlayerPartyTypes>(PlayerPartyTypes.GvG);
          SerializeValueBehaviour component1 = ((Component) unit2).GetComponent<SerializeValueBehaviour>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component1, (UnityEngine.Object) null))
          {
            GameObject gameObject1 = component1.list.GetGameObject(this.SVB_HpbGaugeName);
            GameObject gameObject2 = component1.list.GetGameObject(this.SVB_HDeadName);
            GameUtility.SetGameObjectActive(gameObject2, false);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject1, (UnityEngine.Object) null))
            {
              Slider component2 = gameObject1.GetComponent<Slider>();
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component2, (UnityEngine.Object) null))
              {
                float num = 0.0f;
                if (unit1 != null)
                {
                  gameObject1.SetActive(true);
                  if (unit1.HP == -1)
                  {
                    num = 1f;
                  }
                  else
                  {
                    num = (float) unit1.HP / (float) (int) unit1.Status.param.hp;
                    GameUtility.SetGameObjectActive(gameObject2, unit1.HP == 0);
                  }
                }
                component2.value = num * 100f;
              }
            }
          }
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ConceptCardSlotTemplate, (UnityEngine.Object) null))
        {
          GenericSlot card = this.CardList[index];
          ConceptCardSlot component = ((Component) card).GetComponent<ConceptCardSlot>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
            component.SetLeaderUnit(leader);
          DataSource.Bind<UnitData>(((Component) card).gameObject, (UnitData) unit1);
          card.SetSlotData<ConceptCardData>(unit1?.MainConceptCard);
          ((Component) card).GetComponent<ConceptCardIcon>().Setup(unit1?.MainConceptCard);
        }
      }
      return true;
    }
  }
}
