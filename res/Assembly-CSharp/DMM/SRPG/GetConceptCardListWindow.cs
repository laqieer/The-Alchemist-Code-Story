// Decompiled with JetBrains decompiler
// Type: SRPG.GetConceptCardListWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "念装詳細　閉じ", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "念装詳細　開く", FlowNode.PinTypes.Output, 100)]
  public class GetConceptCardListWindow : MonoBehaviour, IFlowInterface
  {
    private const int INPUT_CLOSED_DETAIL = 1;
    private const int OUTPUT_OPEN_DETAIL = 100;
    [SerializeField]
    private GameObject m_ListItemTemplate;
    [SerializeField]
    private RectTransform m_ContentRoot;
    private List<GameObject> m_ListItems = new List<GameObject>();
    private static string s_SelectedConceptCardID = string.Empty;

    private void Awake() => GameUtility.SetGameObjectActive(this.m_ListItemTemplate, false);

    private void ClearListItems()
    {
      if (this.m_ListItems.Count <= 0)
        return;
      for (int index = 0; index < this.m_ListItems.Count; ++index)
      {
        Object.Destroy((Object) this.m_ListItems[index]);
        this.m_ListItems[index] = (GameObject) null;
      }
      this.m_ListItems.Clear();
    }

    private GameObject CreateListItem(ConceptCardData data)
    {
      GameObject listItem = Object.Instantiate<GameObject>(this.m_ListItemTemplate);
      listItem.transform.SetParent((Transform) this.m_ContentRoot, false);
      listItem.SetActive(true);
      ConceptCardIcon componentInChildren1 = listItem.GetComponentInChildren<ConceptCardIcon>();
      if (Object.op_Inequality((Object) componentInChildren1, (Object) null))
        componentInChildren1.Setup(data);
      ListItemEvents componentInChildren2 = listItem.GetComponentInChildren<ListItemEvents>();
      if (Object.op_Inequality((Object) componentInChildren2, (Object) null))
        componentInChildren2.OnSelect += new ListItemEvents.ListItemEvent(this.OnListItemSelect);
      return listItem;
    }

    public void Setup(ConceptCardData[] data)
    {
      if (Object.op_Equality((Object) this.m_ListItemTemplate, (Object) null))
        return;
      this.ClearListItems();
      for (int index = 0; index < data.Length; ++index)
        this.m_ListItems.Add(this.CreateListItem(data[index]));
    }

    public static void SetSelectedConceptCard(ConceptCardData data)
    {
      GetConceptCardListWindow.ClearSelectedConceptCard();
      if (data == null || data.Param == null)
        return;
      GetConceptCardListWindow.s_SelectedConceptCardID = data.Param.iname;
    }

    public static string GetSelectedConceptCard()
    {
      return GetConceptCardListWindow.s_SelectedConceptCardID;
    }

    public static void ClearSelectedConceptCard()
    {
      GetConceptCardListWindow.s_SelectedConceptCardID = string.Empty;
    }

    public void Activated(int pinID)
    {
      if (pinID != 1)
        ;
    }

    private void OnListItemSelect(GameObject go)
    {
      ConceptCardIcon componentInChildren = go.GetComponentInChildren<ConceptCardIcon>();
      if (!Object.op_Inequality((Object) componentInChildren, (Object) null) || componentInChildren.ConceptCard == null)
        return;
      GlobalVars.SelectedConceptCardData.Set(componentInChildren.ConceptCard);
      GetConceptCardListWindow.SetSelectedConceptCard(componentInChildren.ConceptCard);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
    }
  }
}
