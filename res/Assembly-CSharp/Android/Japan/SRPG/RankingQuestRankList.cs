// Decompiled with JetBrains decompiler
// Type: SRPG.RankingQuestRankList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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
      this.m_RankingWindow = this.GetComponentInParent<RankingQuestRankWindow>();
    }

    public void SetData(RankingQuestUserData[] data)
    {
      this.m_UserDatas = data;
    }

    public void OnSetUpItems()
    {
      if (this.m_UserDatas == null)
        return;
      ScrollListController component1 = this.GetComponent<ScrollListController>();
      component1.OnItemUpdate.RemoveListener(new UnityAction<int, GameObject>(this.OnUpdateItems));
      component1.OnItemUpdate.AddListener(new UnityAction<int, GameObject>(this.OnUpdateItems));
      this.GetComponentInParent<ScrollRect>().movementType = ScrollRect.MovementType.Clamped;
      RectTransform component2 = this.GetComponent<RectTransform>();
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
        if ((UnityEngine.Object) component1 != (UnityEngine.Object) null)
          component1.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemSelect);
        DataSource.Bind<RankingQuestUserData>(obj, this.m_UserDatas[idx], false);
        DataSource.Bind<ViewGuildData>(obj, this.m_UserDatas[idx].ViewGuild, false);
        SerializeValueBehaviour component2 = obj.GetComponent<SerializeValueBehaviour>();
        if ((UnityEngine.Object) component2 != (UnityEngine.Object) null)
        {
          long num = this.m_UserDatas[idx].ViewGuild == null ? 0L : (long) this.m_UserDatas[idx].ViewGuild.id;
          component2.list.SetField(GuildSVB_Key.GUILD_ID, (int) num);
        }
        DataSource.Bind<UnitData>(obj, this.m_UserDatas[idx].m_UnitData, false);
        RankingQuestInfo component3 = obj.GetComponent<RankingQuestInfo>();
        if (!((UnityEngine.Object) component3 != (UnityEngine.Object) null))
          return;
        component3.UpdateValue();
      }
    }

    private void OnItemSelect(GameObject go)
    {
      this.m_RankingWindow.OnItemSelect(go);
    }
  }
}
