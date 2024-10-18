// Decompiled with JetBrains decompiler
// Type: SRPG.RankingQuestRankList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class RankingQuestRankList : MonoBehaviour, ScrollListSetUp
  {
    private float Space = 10f;
    private int m_Max;
    private RankingQuestUserData[] m_UserDatas;
    private RankingQuestRankWindow m_RankingWindow;

    private void Start()
    {
      this.m_RankingWindow = ((Component) this).GetComponentInParent<RankingQuestRankWindow>();
    }

    public void SetData(RankingQuestUserData[] data) => this.m_UserDatas = data;

    public void OnSetUpItems()
    {
      if (this.m_UserDatas == null)
        return;
      ScrollListController component1 = ((Component) this).GetComponent<ScrollListController>();
      // ISSUE: method pointer
      component1.OnItemUpdate.RemoveListener(new UnityAction<int, GameObject>((object) this, __methodptr(OnUpdateItems)));
      // ISSUE: method pointer
      component1.OnItemUpdate.AddListener(new UnityAction<int, GameObject>((object) this, __methodptr(OnUpdateItems)));
      ((Component) this).GetComponentInParent<ScrollRect>().movementType = (ScrollRect.MovementType) 2;
      RectTransform component2 = ((Component) this).GetComponent<RectTransform>();
      Vector2 sizeDelta = component2.sizeDelta;
      Vector2 anchoredPosition = component2.anchoredPosition;
      this.m_Max = this.m_UserDatas.Length;
      component1.Space = (component1.ItemScale + this.Space) / component1.ItemScale;
      anchoredPosition.y = 0.0f;
      sizeDelta.y = component1.ItemScale * component1.Space * (float) this.m_Max;
      component2.sizeDelta = sizeDelta;
      component2.anchoredPosition = anchoredPosition;
    }

    public void OnUpdateItems(int idx, GameObject obj)
    {
      if (this.m_UserDatas == null || idx < 0 || idx >= this.m_UserDatas.Length)
      {
        obj.SetActive(false);
      }
      else
      {
        obj.SetActive(true);
        ListItemEvents component1 = obj.GetComponent<ListItemEvents>();
        if (Object.op_Inequality((Object) component1, (Object) null))
          component1.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemSelect);
        DataSource.Bind<RankingQuestUserData>(obj, this.m_UserDatas[idx]);
        DataSource.Bind<ViewGuildData>(obj, this.m_UserDatas[idx].ViewGuild);
        SerializeValueBehaviour component2 = obj.GetComponent<SerializeValueBehaviour>();
        if (Object.op_Inequality((Object) component2, (Object) null))
        {
          long id = this.m_UserDatas[idx].ViewGuild == null ? 0L : (long) this.m_UserDatas[idx].ViewGuild.id;
          component2.list.SetField(GuildSVB_Key.GUILD_ID, (int) id);
        }
        DataSource.Bind<UnitData>(obj, this.m_UserDatas[idx].m_UnitData);
        RankingQuestInfo component3 = obj.GetComponent<RankingQuestInfo>();
        if (!Object.op_Inequality((Object) component3, (Object) null))
          return;
        component3.UpdateValue();
      }
    }

    private void OnItemSelect(GameObject go) => this.m_RankingWindow.OnItemSelect(go);
  }
}
