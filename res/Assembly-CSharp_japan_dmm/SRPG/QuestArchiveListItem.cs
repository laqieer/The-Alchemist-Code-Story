// Decompiled with JetBrains decompiler
// Type: SRPG.QuestArchiveListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class QuestArchiveListItem : MonoBehaviour
  {
    [SerializeField]
    private GameObject BannerObject;
    [SerializeField]
    private Button DetailsButton;
    [SerializeField]
    private GameObject DetailsTemplate;
    [SerializeField]
    private GameObject Lock;
    [SerializeField]
    private UnityEngine.UI.Text QuestName;
    [SerializeField]
    private GameObject TimeRemaining;
    [SerializeField]
    private UnitIcon Unit1Icon;
    [SerializeField]
    private UnitIcon Unit2Icon;
    [SerializeField]
    private ItemIcon ItemIcon;
    [SerializeField]
    private ArtifactIcon ArtifactIcon;
    [SerializeField]
    private ConceptCardIcon ConceptCardIcon;
    [SerializeField]
    public Button OpenButton;
    [SerializeField]
    public Button ChallengeButton;
    private ArchiveParam mArchiveParam;
    private OpenedQuestArchive mOpenedQuestArchive;
    private ChapterParam mChapterParam;
    private UnitParam mUnit1Param;
    private UnitParam mUnit2Param;
    private long mEndTime;
    private float mRefreshInterval = 1f;
    private GameManager gm;

    public void SetupParams(
      ArchiveParam archiveParam,
      OpenedQuestArchive openedQuestArchive,
      ChapterParam chapterParam,
      UnitParam unit1Param,
      UnitParam unit2Param)
    {
      this.mArchiveParam = archiveParam;
      this.mOpenedQuestArchive = openedQuestArchive;
      this.mChapterParam = chapterParam;
      this.mUnit1Param = unit1Param;
      this.mUnit2Param = unit2Param;
      this.gm = MonoSingleton<GameManager>.Instance;
      this.Refresh();
    }

    private void Update()
    {
      if (this.mOpenedQuestArchive == null)
        return;
      this.mRefreshInterval -= Time.unscaledDeltaTime;
      if ((double) this.mRefreshInterval > 0.0)
        return;
      this.RefreshUIState();
      this.mRefreshInterval = 1f;
    }

    private void Refresh()
    {
      if (this.mChapterParam != null)
      {
        DataSource.Bind<ArchiveParam>(((Component) this).gameObject, this.mArchiveParam);
        DataSource.Bind<ChapterParam>(((Component) this).gameObject, this.mChapterParam);
        if (string.IsNullOrEmpty(this.mChapterParam.helpURL))
          ((Component) this.DetailsButton).gameObject.SetActive(false);
      }
      if (!string.IsNullOrEmpty(this.mChapterParam.prefabPath))
      {
        StringBuilder stringBuilder = GameUtility.GetStringBuilder();
        stringBuilder.Append("QuestChapters/");
        stringBuilder.Append(this.mChapterParam.prefabPath);
        ListItemEvents listItemEvents1 = AssetManager.Load<ListItemEvents>(stringBuilder.ToString());
        if (Object.op_Inequality((Object) listItemEvents1, (Object) null))
        {
          ListItemEvents listItemEvents2 = Object.Instantiate<ListItemEvents>(listItemEvents1);
          DataSource.Bind<ChapterParam>(((Component) listItemEvents2).gameObject, this.mChapterParam);
          LayoutElement component1 = ((Component) listItemEvents2).GetComponent<LayoutElement>();
          if (Object.op_Inequality((Object) component1, (Object) null))
            ((Behaviour) component1).enabled = false;
          ((Component) listItemEvents2).transform.SetParent(this.BannerObject.transform, false);
          ((Component) listItemEvents2).gameObject.SetActive(true);
          listItemEvents2.OnSelect = (ListItemEvents.ListItemEvent) null;
          QuestTimeLimit component2 = ((Component) listItemEvents2).GetComponent<QuestTimeLimit>();
          if (Object.op_Inequality((Object) component2, (Object) null))
          {
            if (Object.op_Inequality((Object) component2.Body, (Object) null))
            {
              component2.Body.SetActive(false);
            }
            else
            {
              Transform transform1 = ((Component) listItemEvents2).transform.Find("bg");
              if (Object.op_Inequality((Object) transform1, (Object) null))
              {
                Transform transform2 = transform1.Find("timer_base");
                if (Object.op_Inequality((Object) transform2, (Object) null))
                  ((Component) transform2).gameObject.SetActive(false);
              }
            }
            Object.Destroy((Object) component2);
          }
        }
        else
          DebugUtility.LogError(this.mArchiveParam.iname + "のバナー情報がない");
      }
      else
        DebugUtility.LogError(this.mArchiveParam.iname + "のバナー情報がない");
      if (this.mOpenedQuestArchive != null && TimeManager.ServerTime < this.mOpenedQuestArchive.end_at && Object.op_Inequality((Object) this.TimeRemaining, (Object) null))
      {
        if (Object.op_Inequality((Object) this.QuestName, (Object) null))
          ((Component) ((Component) this.QuestName).transform.parent).gameObject.SetActive(false);
        this.TimeRemaining.SetActive(true);
        DataSource.Bind<OpenedQuestArchive>(this.TimeRemaining.gameObject, this.mOpenedQuestArchive);
        QuestTimeLimit component = this.TimeRemaining.GetComponent<QuestTimeLimit>();
        ((Behaviour) component).enabled = true;
        component.UpdateValue();
      }
      this.RefreshUIState();
      if (Object.op_Inequality((Object) this.Unit1Icon, (Object) null) && this.mUnit1Param != null)
      {
        DataSource.Bind<UnitParam>(((Component) this.Unit1Icon).gameObject, this.mUnit1Param);
        this.Unit1Icon.UpdateValue();
      }
      else if (Object.op_Inequality((Object) this.Unit1Icon, (Object) null) && this.mUnit1Param == null)
        Object.Destroy((Object) ((Component) this.Unit1Icon).gameObject);
      if (Object.op_Inequality((Object) this.Unit2Icon, (Object) null) && this.mUnit2Param != null)
      {
        DataSource.Bind<UnitParam>(((Component) this.Unit2Icon).gameObject, this.mUnit2Param);
        this.Unit2Icon.UpdateValue();
      }
      else if (Object.op_Inequality((Object) this.Unit2Icon, (Object) null) && this.mUnit2Param == null)
        Object.Destroy((Object) ((Component) this.Unit2Icon).gameObject);
      if (this.mArchiveParam == null || this.mArchiveParam.items == null)
        return;
      int num = 0;
      foreach (ArchiveItemsParam archiveItemsParam in this.mArchiveParam.items)
      {
        if (num > 3)
          break;
        ++num;
        switch (archiveItemsParam.type)
        {
          case ArchiveItemTypes.Item:
            ItemParam itemParam = this.gm.MasterParam.GetItemParam(archiveItemsParam.id);
            if (itemParam != null)
            {
              GameObject gameObject = Object.Instantiate<GameObject>(((Component) this.ItemIcon).gameObject, ((Component) this.ItemIcon).transform.parent);
              gameObject.gameObject.SetActive(true);
              DataSource.Bind<ItemParam>(gameObject, itemParam);
              gameObject.GetComponent<ItemIcon>().UpdateValue();
              break;
            }
            break;
          case ArchiveItemTypes.Artifact:
            ArtifactParam artifactParam = this.gm.MasterParam.GetArtifactParam(archiveItemsParam.id);
            if (artifactParam != null)
            {
              GameObject gameObject = Object.Instantiate<GameObject>(((Component) this.ArtifactIcon).gameObject, ((Component) this.ArtifactIcon).transform.parent);
              gameObject.gameObject.SetActive(true);
              DataSource.Bind<ArtifactParam>(gameObject, artifactParam);
              gameObject.GetComponent<ArtifactIcon>().UpdateValue();
              break;
            }
            break;
          case ArchiveItemTypes.ConceptCard:
            ConceptCardParam conceptCardParam = this.gm.MasterParam.GetConceptCardParam(archiveItemsParam.id);
            if (conceptCardParam != null)
            {
              GameObject gameObject = Object.Instantiate<GameObject>(((Component) this.ConceptCardIcon).gameObject, ((Component) this.ConceptCardIcon).transform.parent);
              gameObject.gameObject.SetActive(true);
              DataSource.Bind<ConceptCardParam>(gameObject, conceptCardParam);
              ConceptCardData cardDataForDisplay = ConceptCardData.CreateConceptCardDataForDisplay(conceptCardParam.iname);
              gameObject.GetComponent<ConceptCardIcon>().Setup(cardDataForDisplay);
              break;
            }
            break;
        }
      }
    }

    private void RefreshUIState()
    {
      if (this.mOpenedQuestArchive != null && TimeManager.ServerTime < this.mOpenedQuestArchive.end_at)
      {
        if (Object.op_Inequality((Object) this.Lock, (Object) null))
          this.Lock.SetActive(false);
        if (Object.op_Inequality((Object) this.OpenButton, (Object) null))
          ((Component) this.OpenButton).gameObject.SetActive(false);
        if (Object.op_Inequality((Object) this.ChallengeButton, (Object) null))
          ((Component) this.ChallengeButton).gameObject.SetActive(true);
        if (!Object.op_Inequality((Object) this.TimeRemaining, (Object) null))
          return;
        if (Object.op_Inequality((Object) this.QuestName, (Object) null))
          ((Component) ((Component) this.QuestName).transform.parent).gameObject.SetActive(false);
        this.TimeRemaining.SetActive(true);
      }
      else
      {
        if (Object.op_Inequality((Object) this.Lock, (Object) null))
          this.Lock.SetActive(true);
        if (Object.op_Inequality((Object) this.OpenButton, (Object) null))
          ((Component) this.OpenButton).gameObject.SetActive(true);
        if (Object.op_Inequality((Object) this.ChallengeButton, (Object) null))
          ((Component) this.ChallengeButton).gameObject.SetActive(false);
        if (!Object.op_Inequality((Object) this.QuestName, (Object) null) || this.mChapterParam == null)
          return;
        if (Object.op_Inequality((Object) this.TimeRemaining, (Object) null))
          this.TimeRemaining.SetActive(false);
        ((Component) ((Component) this.QuestName).transform.parent).gameObject.SetActive(true);
        this.QuestName.text = this.mChapterParam.name;
      }
    }
  }
}
