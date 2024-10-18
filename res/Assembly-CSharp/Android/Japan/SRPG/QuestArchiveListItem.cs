// Decompiled with JetBrains decompiler
// Type: SRPG.QuestArchiveListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

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
    private GameManager gm;

    public void SetupParams(ArchiveParam archiveParam, OpenedQuestArchive openedQuestArchive, ChapterParam chapterParam, UnitParam unit1Param, UnitParam unit2Param)
    {
      this.mArchiveParam = archiveParam;
      this.mOpenedQuestArchive = openedQuestArchive;
      this.mChapterParam = chapterParam;
      this.mUnit1Param = unit1Param;
      this.mUnit2Param = unit2Param;
      this.gm = MonoSingleton<GameManager>.Instance;
      this.Refresh();
    }

    private void Refresh()
    {
      if (this.mChapterParam != null)
      {
        DataSource.Bind<ArchiveParam>(this.gameObject, this.mArchiveParam, false);
        DataSource.Bind<ChapterParam>(this.gameObject, this.mChapterParam, false);
        if (string.IsNullOrEmpty(this.mChapterParam.helpURL))
          this.DetailsButton.gameObject.SetActive(false);
      }
      if (!string.IsNullOrEmpty(this.mChapterParam.prefabPath))
      {
        StringBuilder stringBuilder = GameUtility.GetStringBuilder();
        stringBuilder.Append("QuestChapters/");
        stringBuilder.Append(this.mChapterParam.prefabPath);
        ListItemEvents original = AssetManager.Load<ListItemEvents>(stringBuilder.ToString());
        if ((UnityEngine.Object) original != (UnityEngine.Object) null)
        {
          ListItemEvents listItemEvents = UnityEngine.Object.Instantiate<ListItemEvents>(original);
          DataSource.Bind<ChapterParam>(listItemEvents.gameObject, this.mChapterParam, false);
          LayoutElement component1 = listItemEvents.GetComponent<LayoutElement>();
          if ((UnityEngine.Object) component1 != (UnityEngine.Object) null)
            component1.enabled = false;
          listItemEvents.transform.SetParent(this.BannerObject.transform, false);
          listItemEvents.gameObject.SetActive(true);
          listItemEvents.OnSelect = (ListItemEvents.ListItemEvent) null;
          QuestTimeLimit component2 = listItemEvents.GetComponent<QuestTimeLimit>();
          if ((UnityEngine.Object) component2 != (UnityEngine.Object) null)
          {
            if ((UnityEngine.Object) component2.Body != (UnityEngine.Object) null)
            {
              component2.Body.SetActive(false);
            }
            else
            {
              Transform transform1 = listItemEvents.transform.Find("bg");
              if ((UnityEngine.Object) transform1 != (UnityEngine.Object) null)
              {
                Transform transform2 = transform1.Find("timer_base");
                if ((UnityEngine.Object) transform2 != (UnityEngine.Object) null)
                  transform2.gameObject.SetActive(false);
              }
            }
            UnityEngine.Object.Destroy((UnityEngine.Object) component2);
          }
        }
        else
          DebugUtility.LogError(this.mArchiveParam.iname + "のバナー情報がない");
      }
      else
        DebugUtility.LogError(this.mArchiveParam.iname + "のバナー情報がない");
      if (this.gm.Player.IsQuestArchiveOpen(this.mArchiveParam.iname))
      {
        if ((UnityEngine.Object) this.Lock != (UnityEngine.Object) null)
          this.Lock.SetActive(false);
        if ((UnityEngine.Object) this.OpenButton != (UnityEngine.Object) null)
          this.OpenButton.gameObject.SetActive(false);
        if ((UnityEngine.Object) this.ChallengeButton != (UnityEngine.Object) null)
          this.ChallengeButton.gameObject.SetActive(true);
      }
      else
      {
        if ((UnityEngine.Object) this.Lock != (UnityEngine.Object) null)
          this.Lock.SetActive(true);
        if ((UnityEngine.Object) this.OpenButton != (UnityEngine.Object) null)
          this.OpenButton.gameObject.SetActive(true);
        if ((UnityEngine.Object) this.ChallengeButton != (UnityEngine.Object) null)
          this.ChallengeButton.gameObject.SetActive(false);
      }
      if (this.gm.Player.IsQuestArchiveOpen(this.mArchiveParam.iname))
      {
        if ((UnityEngine.Object) this.TimeRemaining != (UnityEngine.Object) null && this.mOpenedQuestArchive != null)
        {
          if ((UnityEngine.Object) this.QuestName != (UnityEngine.Object) null)
            this.QuestName.transform.parent.gameObject.SetActive(false);
          this.TimeRemaining.SetActive(true);
          DataSource.Bind<OpenedQuestArchive>(this.TimeRemaining.gameObject, this.mOpenedQuestArchive, false);
          QuestTimeLimit component = this.TimeRemaining.GetComponent<QuestTimeLimit>();
          component.enabled = true;
          component.UpdateValue();
        }
      }
      else if ((UnityEngine.Object) this.QuestName != (UnityEngine.Object) null && this.mChapterParam != null)
      {
        if ((UnityEngine.Object) this.TimeRemaining != (UnityEngine.Object) null)
          this.TimeRemaining.SetActive(false);
        this.QuestName.transform.parent.gameObject.SetActive(true);
        this.QuestName.text = this.mChapterParam.name;
      }
      if ((UnityEngine.Object) this.Unit1Icon != (UnityEngine.Object) null && this.mUnit1Param != null)
      {
        DataSource.Bind<UnitParam>(this.Unit1Icon.gameObject, this.mUnit1Param, false);
        this.Unit1Icon.UpdateValue();
      }
      else if ((UnityEngine.Object) this.Unit1Icon != (UnityEngine.Object) null && this.mUnit1Param == null)
        UnityEngine.Object.Destroy((UnityEngine.Object) this.Unit1Icon.gameObject);
      if ((UnityEngine.Object) this.Unit2Icon != (UnityEngine.Object) null && this.mUnit2Param != null)
      {
        DataSource.Bind<UnitParam>(this.Unit2Icon.gameObject, this.mUnit2Param, false);
        this.Unit2Icon.UpdateValue();
      }
      else if ((UnityEngine.Object) this.Unit2Icon != (UnityEngine.Object) null && this.mUnit2Param == null)
        UnityEngine.Object.Destroy((UnityEngine.Object) this.Unit2Icon.gameObject);
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
              GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ItemIcon.gameObject, this.ItemIcon.transform.parent);
              gameObject.gameObject.SetActive(true);
              DataSource.Bind<ItemParam>(gameObject, itemParam, false);
              gameObject.GetComponent<ItemIcon>().UpdateValue();
              break;
            }
            break;
          case ArchiveItemTypes.Artifact:
            ArtifactParam artifactParam = this.gm.MasterParam.GetArtifactParam(archiveItemsParam.id);
            if (artifactParam != null)
            {
              GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ArtifactIcon.gameObject, this.ArtifactIcon.transform.parent);
              gameObject.gameObject.SetActive(true);
              DataSource.Bind<ArtifactParam>(gameObject, artifactParam, false);
              gameObject.GetComponent<ArtifactIcon>().UpdateValue();
              break;
            }
            break;
          case ArchiveItemTypes.ConceptCard:
            ConceptCardParam conceptCardParam = this.gm.MasterParam.GetConceptCardParam(archiveItemsParam.id);
            if (conceptCardParam != null)
            {
              GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ConceptCardIcon.gameObject, this.ConceptCardIcon.transform.parent);
              gameObject.gameObject.SetActive(true);
              DataSource.Bind<ConceptCardParam>(gameObject, conceptCardParam, false);
              ConceptCardData cardDataForDisplay = ConceptCardData.CreateConceptCardDataForDisplay(conceptCardParam.iname);
              gameObject.GetComponent<ConceptCardIcon>().Setup(cardDataForDisplay);
              break;
            }
            break;
        }
      }
    }
  }
}
