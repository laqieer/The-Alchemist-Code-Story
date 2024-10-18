// Decompiled with JetBrains decompiler
// Type: SRPG.GvGDefenseSettings
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "Initialize", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(12, "Hide Chat", FlowNode.PinTypes.Input, 12)]
  [FlowNode.Pin(13, "Open Chat", FlowNode.PinTypes.Input, 13)]
  public class GvGDefenseSettings : MonoBehaviour, IFlowInterface, IPagination
  {
    public const int PIN_INPUT_INIT = 1;
    public const int PIN_INPUT_HIDECHAT = 12;
    public const int PIN_INPUT_OPENCHAT = 13;
    [SerializeField]
    private Text NodeNameText;
    [SerializeField]
    private GvGPartyContent PartyTemplate;
    [Space(10f)]
    [SerializeField]
    private Button PageNextButton;
    [SerializeField]
    private Button PagePrevButton;
    [SerializeField]
    private Text PageCurrentText;
    [SerializeField]
    private Text PageTotalText;
    [SerializeField]
    private ScrollRect PartyScrollRect;
    [SerializeField]
    private Text mDefenseTotalBeat;
    private GvGNodeData CurrentNode;
    private List<GvGParty> DefenseParties = new List<GvGParty>();
    private List<GvGPartyContent> PartyList = new List<GvGPartyContent>();
    private int TotalPage = 1;

    public static GvGDefenseSettings Instance { get; private set; }

    public long[] EditPartyIds { get; private set; }

    public int CurrentPage { get; private set; }

    public int TotalBeatNum { get; private set; }

    private void Awake() => GvGDefenseSettings.Instance = this;

    private void OnDestroy() => GvGDefenseSettings.Instance = (GvGDefenseSettings) null;

    private void Start()
    {
      this.CurrentPage = 1;
      GameUtility.SetGameObjectActive((Component) this.PartyTemplate, false);
      this.CurrentNode = GvGManager.Instance.NodeDataList.Find((Predicate<GvGNodeData>) (n => n.NodeId == GvGManager.Instance.SelectNodeId));
      if (this.CurrentNode == null)
        return;
      ChangeMaterialList component = ((Component) this).gameObject.GetComponent<ChangeMaterialList>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      GvGManager.Instance.SetNodeColor(this.CurrentNode, component);
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          this.Initialize();
          break;
        case 12:
          GvGManager.Instance.HideChatButton();
          break;
        case 13:
          GvGManager.Instance.HideChatButton(true);
          break;
      }
    }

    private void Initialize()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) MonoSingleton<GameManager>.Instance, (UnityEngine.Object) null) && this.CurrentNode == null || UnityEngine.Object.op_Equality((UnityEngine.Object) this.NodeNameText, (UnityEngine.Object) null))
        return;
      this.NodeNameText.text = this.CurrentNode.NodeParam.Name;
      if (this.DefenseParties == null)
        return;
      this.PartyList.ForEach((Action<GvGPartyContent>) (p => UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) p).gameObject)));
      this.PartyList.Clear();
      for (int index = 0; index < this.DefenseParties.Count; ++index)
      {
        if (this.DefenseParties[index] != null)
        {
          GvGPartyContent gvGpartyContent = UnityEngine.Object.Instantiate<GvGPartyContent>(this.PartyTemplate, ((Component) this.PartyTemplate).transform.parent);
          int number = index + 1 + (this.CurrentPage - 1) * GvGManager.Instance.ONE_PAGE_DEFENSE_PARTY_COUNT_MAX;
          if (gvGpartyContent.Setup(this.CurrentNode, number, this.DefenseParties[index]))
          {
            ((Component) gvGpartyContent).gameObject.SetActive(true);
            this.PartyList.Add(gvGpartyContent);
          }
        }
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mDefenseTotalBeat, (UnityEngine.Object) null))
        this.mDefenseTotalBeat.text = string.Format(LocalizedText.Get("sys.GVG_TEXT_ENEMYTOTALBEATNUM", (object) this.TotalBeatNum));
      GlobalVars.SelectedQuestID = this.CurrentNode.NodeParam.QuestId;
      this.EditPartyIds = new long[3];
      this.RefreshPagination();
    }

    private void RefreshPagination()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PageTotalText, (UnityEngine.Object) null))
        this.PageTotalText.text = Mathf.Max(this.TotalPage, 1).ToString();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PageCurrentText, (UnityEngine.Object) null))
        this.PageCurrentText.text = Mathf.Max(this.CurrentPage, 1).ToString();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PageNextButton, (UnityEngine.Object) null))
        ((Selectable) this.PageNextButton).interactable = this.CurrentPage < this.TotalPage;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PagePrevButton, (UnityEngine.Object) null))
        ((Selectable) this.PagePrevButton).interactable = this.CurrentPage > 1;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PartyScrollRect, (UnityEngine.Object) null))
        return;
      this.PartyScrollRect.normalizedPosition = Vector2.up;
    }

    public void NextPage()
    {
      if (this.CurrentPage >= this.TotalPage)
        return;
      ++this.CurrentPage;
    }

    public void PrevPage()
    {
      if (1 >= this.CurrentPage)
        return;
      --this.CurrentPage;
    }

    public void SetEditParty(long[] units) => units.CopyTo((Array) this.EditPartyIds, 0);

    public bool SetupDefenseParties(JSON_GvGParty[] json, int totalPage, int total_beat_num)
    {
      if (json == null)
        return false;
      this.DefenseParties.Clear();
      for (int index = 0; index < json.Length; ++index)
      {
        if (json[index] != null)
        {
          GvGParty gvGparty = new GvGParty();
          if (gvGparty.Deserialize(json[index]))
            this.DefenseParties.Add(gvGparty);
        }
      }
      this.TotalPage = totalPage;
      this.TotalBeatNum = total_beat_num;
      return true;
    }
  }
}
