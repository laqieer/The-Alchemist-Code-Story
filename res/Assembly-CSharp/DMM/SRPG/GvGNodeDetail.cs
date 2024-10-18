// Decompiled with JetBrains decompiler
// Type: SRPG.GvGNodeDetail
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
  public class GvGNodeDetail : MonoBehaviour, IFlowInterface, IPagination
  {
    public const int PIN_INPUT_INIT = 1;
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
    private ImageArray mTitleImageArray;
    [SerializeField]
    private ImageArray mDialogImageArray;
    [SerializeField]
    private Text mDefenseTotalBeat;
    private GvGNodeData CurrentNode;
    private List<GvGParty> DefenseParties = new List<GvGParty>();
    private List<GvGPartyContent> PartyList = new List<GvGPartyContent>();
    private int TotalPage = 1;

    public static GvGNodeDetail Instance { get; private set; }

    public int CurrentPage { get; private set; }

    public int TotalBeatNum { get; private set; }

    private void Awake() => GvGNodeDetail.Instance = this;

    private void OnDestroy() => GvGNodeDetail.Instance = (GvGNodeDetail) null;

    private void Start()
    {
      this.CurrentPage = 1;
      GameUtility.SetGameObjectActive((Component) this.PartyTemplate, false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mDialogImageArray, (UnityEngine.Object) null))
        GameUtility.SetGameObjectActive(((Component) this.mDialogImageArray).gameObject, false);
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
      if (pinID != 1)
        return;
      this.Initialize();
    }

    private void Initialize()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) MonoSingleton<GameManager>.Instance, (UnityEngine.Object) null) && this.CurrentNode == null)
        return;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mDialogImageArray, (UnityEngine.Object) null))
        GameUtility.SetGameObjectActive(((Component) this.mDialogImageArray).gameObject, true);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mTitleImageArray, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mDialogImageArray, (UnityEngine.Object) null))
      {
        this.mTitleImageArray.ImageIndex = Mathf.Clamp((int) GvGManager.Instance.GetNodeImageIndex(this.CurrentNode), 0, this.mTitleImageArray.Images.Length - 1);
        this.mDialogImageArray.ImageIndex = Mathf.Clamp((int) GvGManager.Instance.GetNodeImageIndex(this.CurrentNode), 0, this.mDialogImageArray.Images.Length - 1);
      }
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.NodeNameText, (UnityEngine.Object) null))
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

    public bool SetupDefenseParties(JSON_GvGParty[] json, int totalPage, int totalBeatNum)
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
      this.TotalBeatNum = totalBeatNum;
      return true;
    }
  }
}
