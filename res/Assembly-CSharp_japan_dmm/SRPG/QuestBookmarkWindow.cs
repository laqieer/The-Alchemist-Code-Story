// Decompiled with JetBrains decompiler
// Type: SRPG.QuestBookmarkWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "PartyEditor2から戻ってきた", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "クエスト選択", FlowNode.PinTypes.Output, 100)]
  public class QuestBookmarkWindow : MonoBehaviour, IFlowInterface
  {
    [SerializeField]
    private SRPG_Button ButtonBookmarkBeginEdit;
    [SerializeField]
    private SRPG_Button ButtonBookmarkEndEdit;
    [SerializeField]
    private GameObject ButtonHolder;
    [SerializeField]
    private GameObject BookmarkButtonTemplate;
    [SerializeField]
    private GameObject ButtonTemplate;
    [SerializeField]
    private GameObject ItemTemplate;
    [SerializeField]
    private GameObject ItemContainer;
    [SerializeField]
    private GameObject QuestSelectorTemplate;
    [SerializeField]
    private GameObject BookmarkNotFoundText;
    [SerializeField]
    private ScrollRect ScrollRectObj;
    [SerializeField]
    private Text TitleText;
    [SerializeField]
    private Text DescriptionText;
    private List<SRPG_Button> ButtonSections = new List<SRPG_Button>();
    private readonly string BookmarkTitle = "sys.TITLE_QUESTBOOKMARK";
    private readonly string BookmarkEditTitle = "sys.TITLE_QUESTBOOKMARK_EDITING";
    private readonly string BookmarkDescription = "sys.TXT_QUESTBOOKMARK_DESCRIPTION";
    private readonly string BookmarkEditDescription = "sys.TXT_QUESTBOOKMARK_DESCRIPTION_EDIT";
    private readonly string BookmarkSectionName = string.Empty;
    private readonly int MaxBookmarkCount = 20;
    private string mLastSectionName;
    private int mLastIndex;
    private Dictionary<string, List<QuestBookmarkWindow.ItemAndQuests>> mSectionToPieces = new Dictionary<string, List<QuestBookmarkWindow.ItemAndQuests>>();
    private List<QuestBookmarkWindow.ItemAndQuests> mAllSection = new List<QuestBookmarkWindow.ItemAndQuests>();
    private List<QuestBookmarkWindow.ItemAndQuests> mBookmarkedPieces = new List<QuestBookmarkWindow.ItemAndQuests>();
    private List<QuestBookmarkWindow.ItemAndQuests> mBookmarkedPiecesOrigin = new List<QuestBookmarkWindow.ItemAndQuests>();
    private List<GameObject> mCurrentUnitObjects = new List<GameObject>();
    private bool mIsBookmarkEditing;
    private List<string> mAvailableSections = new List<string>();
    private bool mSelectQuestFlag;

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.RefreshSection(this.mLastIndex);
      this.mSelectQuestFlag = false;
    }

    private void Awake()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BookmarkButtonTemplate, (UnityEngine.Object) null))
        this.BookmarkButtonTemplate.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ButtonTemplate, (UnityEngine.Object) null))
        this.ButtonTemplate.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ItemTemplate, (UnityEngine.Object) null))
        this.ItemTemplate.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ButtonBookmarkBeginEdit, (UnityEngine.Object) null))
      {
        this.ButtonBookmarkBeginEdit.AddListener(new SRPG_Button.ButtonClickEvent(this.OnBookmarkBeginEditButtonClick));
        ((Component) this.ButtonBookmarkBeginEdit).gameObject.SetActive(true);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ButtonBookmarkEndEdit, (UnityEngine.Object) null))
      {
        this.ButtonBookmarkEndEdit.AddListener(new SRPG_Button.ButtonClickEvent(this.OnBookmarkEndEditButtonClick));
        ((Component) this.ButtonBookmarkEndEdit).gameObject.SetActive(false);
      }
      this.ResetScrollPosition();
      this.RequestQuestBookmark();
    }

    private void ResetScrollPosition()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ScrollRectObj, (UnityEngine.Object) null))
        return;
      this.ScrollRectObj.normalizedPosition = new Vector2(1f, 1f);
    }

    private void ToggleSectionButton(int index)
    {
      for (int index1 = 0; index1 < this.ButtonSections.Count; ++index1)
      {
        BookmarkToggleButton component = ((Component) this.ButtonSections[index1]).GetComponent<BookmarkToggleButton>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          component.Activate(index1 == index);
      }
    }

    private void RequestQuestBookmark()
    {
      Network.RequestAPI((WebAPI) new ReqQuestBookmark(new Network.ResponseCallback(this.QuestBookmarkResponseCallback)));
    }

    private void RequestQuestBookmarkUpdate(IEnumerable<string> add, IEnumerable<string> delete)
    {
      Network.RequestAPI((WebAPI) new ReqQuestBookmarkUpdate(add, delete, new Network.ResponseCallback(this.QuestBookmarkUpdateResponseCallback)));
    }

    private void QuestBookmarkResponseCallback(WWWResult www)
    {
      if (FlowNode_Network.HasCommonError(www))
        return;
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.QuestBookmark_RequestMax:
          case Network.EErrCode.QuestBookmark_AlreadyLimited:
            Network.RemoveAPI();
            Network.ResetError();
            UIUtility.SystemMessage((string) null, LocalizedText.Get("sys.TXT_QUESTBOOKMARK_BOOKMARK_NOT_FOUND"), (UIUtility.DialogResultEvent) null, systemModal: true);
            break;
          default:
            FlowNode_Network.Retry();
            break;
        }
      }
      else
      {
        this.Initialize(JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<QuestBookmarkWindow.JSON_Body>>(www.text).body.result);
        Network.RemoveAPI();
      }
    }

    private void QuestBookmarkUpdateResponseCallback(WWWResult www)
    {
      if (FlowNode_Network.HasCommonError(www))
        return;
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.QuestBookmark_RequestMax:
          case Network.EErrCode.QuestBookmark_AlreadyLimited:
            Network.RemoveAPI();
            Network.ResetError();
            UIUtility.SystemMessage((string) null, LocalizedText.Get("sys.TXT_QUESTBOOKMARK_BOOKMARK_NOT_FOUND"), (UIUtility.DialogResultEvent) null, systemModal: true);
            break;
          default:
            FlowNode_Network.Retry();
            break;
        }
      }
      else
      {
        this.mBookmarkedPiecesOrigin = this.mBookmarkedPieces.ToList<QuestBookmarkWindow.ItemAndQuests>();
        if (this.mLastSectionName == this.BookmarkSectionName)
          this.RefreshSection(0);
        this.EndBookmarkEditing();
        Network.RemoveAPI();
      }
    }

    private void Initialize(QuestBookmarkWindow.JSON_Item[] bookmarkItems)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      PlayerData player = instance.Player;
      long serverTime = Network.GetServerTime();
      foreach (QuestParam availableQuest in player.AvailableQuests)
      {
        if (this.IsAvailableQuest(availableQuest, serverTime) && !this.mAvailableSections.Contains(availableQuest.world))
          this.mAvailableSections.Add(availableQuest.world);
      }
      List<SectionParam> list = ((IEnumerable<SectionParam>) instance.Sections).Where<SectionParam>((Func<SectionParam, bool>) (s => !s.hidden)).ToList<SectionParam>();
      Dictionary<string, List<QuestParam>> dictionary = new Dictionary<string, List<QuestParam>>();
      foreach (QuestParam questParam in ((IEnumerable<QuestParam>) MonoSingleton<GameManager>.Instance.Quests).Where<QuestParam>((Func<QuestParam, bool>) (q => q.type == QuestTypes.Free || q.type == QuestTypes.StoryExtra)))
      {
        if (questParam.IsAvailable() || questParam.type != QuestTypes.StoryExtra)
        {
          List<QuestParam> questParamList;
          if (!dictionary.TryGetValue(questParam.world, out questParamList))
          {
            questParamList = new List<QuestParam>();
            dictionary[questParam.world] = questParamList;
          }
          questParamList.Add(questParam);
        }
      }
      List<SectionParam> sectionParamList1 = new List<SectionParam>();
      foreach (string key in dictionary.Keys)
      {
        foreach (SectionParam sectionParam in list)
        {
          if (key == sectionParam.iname)
            sectionParamList1.Add(sectionParam);
        }
      }
      List<SectionParam> sectionParamList2 = sectionParamList1;
      if (bookmarkItems != null && bookmarkItems.Length > 0)
      {
        foreach (QuestBookmarkWindow.JSON_Item bookmarkItem in bookmarkItems)
        {
          ItemParam itemParam = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(bookmarkItem.iname);
          List<QuestParam> itemDropQuestList = QuestDropParam.Instance.GetItemDropQuestList(itemParam, GlobalVars.GetDropTableGeneratedDateTime());
          this.mBookmarkedPiecesOrigin.Add(new QuestBookmarkWindow.ItemAndQuests()
          {
            itemName = itemParam.iname,
            quests = itemDropQuestList
          });
        }
        this.mBookmarkedPieces = this.mBookmarkedPiecesOrigin.ToList<QuestBookmarkWindow.ItemAndQuests>();
      }
      this.mSectionToPieces[this.BookmarkSectionName] = this.mBookmarkedPieces;
      GameObject gameObject1 = UnityEngine.Object.Instantiate<GameObject>(this.BookmarkButtonTemplate);
      gameObject1.SetActive(true);
      gameObject1.transform.SetParent(this.ButtonHolder.transform, false);
      SRPG_Button component1 = gameObject1.GetComponent<SRPG_Button>();
      this.ButtonSections.Add(component1);
      DataSource.Bind<string>(((Component) component1).gameObject, this.BookmarkSectionName);
      gameObject1.GetComponent<BookmarkToggleButton>().Text.text = LocalizedText.Get("sys.BTN_QUESTBOOKMARK_SECTION_BOOKMARK");
      foreach (SectionParam sectionParam in sectionParamList2)
      {
        GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.ButtonTemplate);
        gameObject2.SetActive(true);
        gameObject2.transform.SetParent(this.ButtonHolder.transform, false);
        SRPG_Button component2 = gameObject2.GetComponent<SRPG_Button>();
        BookmarkToggleButton component3 = gameObject2.GetComponent<BookmarkToggleButton>();
        component3.Text.text = sectionParam.name;
        this.ButtonSections.Add(component2);
        if (sectionParam.IsDateUnlock() && this.mAvailableSections.Contains(sectionParam.iname))
        {
          DataSource.Bind<string>(((Component) component2).gameObject, sectionParam.iname);
          component3.EnableShadow(false);
          List<QuestParam> questParamList1 = dictionary[sectionParam.iname];
          OrderedDictionary source = new OrderedDictionary();
          foreach (QuestParam questParam in questParamList1)
          {
            ItemParam hardDropPiece = QuestDropParam.Instance.GetHardDropPiece(questParam.iname, GlobalVars.GetDropTableGeneratedDateTime());
            if (hardDropPiece != null)
            {
              List<QuestParam> questParamList2;
              if (source.Contains((object) hardDropPiece.iname))
              {
                questParamList2 = source[(object) hardDropPiece.iname] as List<QuestParam>;
              }
              else
              {
                questParamList2 = new List<QuestParam>();
                source[(object) hardDropPiece.iname] = (object) questParamList2;
              }
              questParamList2.Add(questParam);
            }
          }
          this.mSectionToPieces[sectionParam.iname] = source.Cast<DictionaryEntry>().Select<DictionaryEntry, QuestBookmarkWindow.ItemAndQuests>((Func<DictionaryEntry, QuestBookmarkWindow.ItemAndQuests>) (kv => new QuestBookmarkWindow.ItemAndQuests()
          {
            itemName = kv.Key as string,
            quests = kv.Value as List<QuestParam>
          })).ToList<QuestBookmarkWindow.ItemAndQuests>();
        }
        else
          component3.EnableShadow(true);
      }
      foreach (SRPG_Button buttonSection in this.ButtonSections)
        buttonSection.AddListener(new SRPG_Button.ButtonClickEvent(this.OnSectionSelect));
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TitleText, (UnityEngine.Object) null))
        this.TitleText.text = LocalizedText.Get(this.BookmarkTitle);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DescriptionText, (UnityEngine.Object) null))
        this.DescriptionText.text = LocalizedText.Get(this.BookmarkDescription);
      foreach (string key in this.mSectionToPieces.Keys)
      {
        if (this.BookmarkSectionName != key)
        {
          for (int index = 0; index < this.mSectionToPieces[key].Count; ++index)
            this.mAllSection.Add(this.mSectionToPieces[key][index]);
        }
      }
      this.RefreshSection(0);
    }

    private void OnBookmarkBeginEditButtonClick(SRPG_Button button)
    {
      if (this.mIsBookmarkEditing || this.mSelectQuestFlag)
        return;
      ((Component) this.ButtonBookmarkBeginEdit).gameObject.SetActive(false);
      ((Component) this.ButtonBookmarkEndEdit).gameObject.SetActive(true);
      if (this.mBookmarkedPieces.Count >= this.MaxBookmarkCount)
        this.SetActivateWithoutBookmarkedUnit(false);
      this.SetDeactivateNotAvailableUnit(true);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TitleText, (UnityEngine.Object) null))
        this.TitleText.text = LocalizedText.Get(this.BookmarkEditTitle);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DescriptionText, (UnityEngine.Object) null))
        this.DescriptionText.text = LocalizedText.Get(this.BookmarkEditDescription);
      this.mIsBookmarkEditing = true;
    }

    private void OnBookmarkEndEditButtonClick(SRPG_Button button)
    {
      if (!this.mIsBookmarkEditing)
        return;
      string[] array1 = this.mBookmarkedPiecesOrigin.Except<QuestBookmarkWindow.ItemAndQuests>((IEnumerable<QuestBookmarkWindow.ItemAndQuests>) this.mBookmarkedPieces).Select<QuestBookmarkWindow.ItemAndQuests, string>((Func<QuestBookmarkWindow.ItemAndQuests, string>) (x => x.itemName)).ToArray<string>();
      string[] array2 = this.mBookmarkedPieces.Except<QuestBookmarkWindow.ItemAndQuests>((IEnumerable<QuestBookmarkWindow.ItemAndQuests>) this.mBookmarkedPiecesOrigin).Select<QuestBookmarkWindow.ItemAndQuests, string>((Func<QuestBookmarkWindow.ItemAndQuests, string>) (x => x.itemName)).ToArray<string>();
      if (array1.Length > 0 || array2.Length > 0)
        this.RequestQuestBookmarkUpdate((IEnumerable<string>) array2, (IEnumerable<string>) array1);
      else
        this.EndBookmarkEditing();
    }

    private void EndBookmarkEditing()
    {
      ((Component) this.ButtonBookmarkBeginEdit).gameObject.SetActive(true);
      ((Component) this.ButtonBookmarkEndEdit).gameObject.SetActive(false);
      this.SetActivateWithoutBookmarkedUnit(true);
      this.SetDeactivateNotAvailableUnit(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TitleText, (UnityEngine.Object) null))
        this.TitleText.text = LocalizedText.Get(this.BookmarkTitle);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DescriptionText, (UnityEngine.Object) null))
        this.DescriptionText.text = LocalizedText.Get(this.BookmarkDescription);
      this.mIsBookmarkEditing = false;
    }

    private void SetDeactivateNotAvailableUnit(bool is_bookmark_editing)
    {
      foreach (GameObject currentUnitObject in this.mCurrentUnitObjects)
      {
        QuestBookmarkWindow.ItemAndQuests itemQuests = DataSource.FindDataOfClass<QuestBookmarkWindow.ItemAndQuests>(currentUnitObject, (QuestBookmarkWindow.ItemAndQuests) null);
        QuestParam[] availableQuests = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
        bool flag = false;
        long currentTime = Network.GetServerTime();
        foreach (QuestParam quest1 in itemQuests.quests)
        {
          QuestParam quest = quest1;
          if (is_bookmark_editing)
          {
            if (this.mBookmarkedPieces.Count >= this.MaxBookmarkCount && this.mBookmarkedPieces.FirstOrDefault<QuestBookmarkWindow.ItemAndQuests>((Func<QuestBookmarkWindow.ItemAndQuests, bool>) (bookmark => bookmark.itemName == itemQuests.itemName)) == null)
              continue;
          }
          else if (!quest.IsOpenUnitHave())
            continue;
          if (((IEnumerable<QuestParam>) availableQuests).Any<QuestParam>((Func<QuestParam, bool>) (q => this.IsAvailableQuest(q, currentTime) && quest.iname == q.iname)))
          {
            flag = true;
            break;
          }
        }
        BookmarkUnit component = currentUnitObject.GetComponent<BookmarkUnit>();
        component.Overlay.SetActive(!flag);
        ((Selectable) component.Button).interactable = flag;
      }
    }

    private bool IsAvailableQuest(QuestParam questParam, long currentTime)
    {
      return !string.IsNullOrEmpty(questParam.ChapterID) && !questParam.IsMulti && questParam.IsDateUnlock(currentTime);
    }

    private void OnSectionSelect(SRPG_Button button)
    {
      if (this.mSelectQuestFlag)
        return;
      string dataOfClass = DataSource.FindDataOfClass<string>(((Component) button).gameObject, (string) null);
      if (dataOfClass == this.mLastSectionName)
        return;
      if (dataOfClass != this.BookmarkSectionName && !this.mAvailableSections.Contains(dataOfClass))
        UIUtility.SystemMessage((string) null, LocalizedText.Get("sys.TXT_QUESTBOOKMARK_BOOKMARK_NOT_AVAIABLE_SECTION", (object) this.ButtonSections.IndexOf(button)), (UIUtility.DialogResultEvent) null, systemModal: true);
      else
        this.RefreshSection(dataOfClass, button);
    }

    private void RefreshSection(int index)
    {
      if (index >= this.ButtonSections.Count)
        return;
      SRPG_Button buttonSection = this.ButtonSections[index];
      this.RefreshSection(DataSource.FindDataOfClass<string>(((Component) buttonSection).gameObject, (string) null), buttonSection);
    }

    private void RefreshSection(string sectionName, SRPG_Button button)
    {
      foreach (UnityEngine.Object currentUnitObject in this.mCurrentUnitObjects)
        UnityEngine.Object.Destroy(currentUnitObject);
      this.mCurrentUnitObjects.Clear();
      this.CreateUnitPanels((IEnumerable<QuestBookmarkWindow.ItemAndQuests>) this.mSectionToPieces[sectionName], sectionName);
      if (this.mIsBookmarkEditing && this.mBookmarkedPieces.Count >= this.MaxBookmarkCount)
        this.SetActivateWithoutBookmarkedUnit(false);
      this.SetDeactivateNotAvailableUnit(this.mIsBookmarkEditing);
      int index = this.ButtonSections.IndexOf(button);
      this.mLastIndex = index;
      this.ToggleSectionButton(index);
      this.ResetScrollPosition();
      if (sectionName == this.BookmarkSectionName)
      {
        bool flag = this.mCurrentUnitObjects.Count <= 0;
        this.BookmarkNotFoundText.SetActive(flag);
        ((Component) this.DescriptionText).gameObject.SetActive(!flag);
      }
      else
      {
        this.BookmarkNotFoundText.SetActive(false);
        ((Component) this.DescriptionText).gameObject.SetActive(true);
      }
      this.mLastSectionName = sectionName;
    }

    private void CreateUnitPanels(
      IEnumerable<QuestBookmarkWindow.ItemAndQuests> targetPieces,
      string sectionName)
    {
      UnitParam[] allUnits = MonoSingleton<GameManager>.Instance.MasterParam.GetAllUnits();
      Dictionary<string, QuestParam> dictionary = ((IEnumerable<QuestParam>) MonoSingleton<GameManager>.Instance.Player.AvailableQuests).ToDictionary<QuestParam, string>((Func<QuestParam, string>) (quest => quest.iname));
      foreach (QuestBookmarkWindow.ItemAndQuests targetPiece in targetPieces)
      {
        QuestBookmarkWindow.ItemAndQuests itemQuests = targetPiece;
        GameObject root = UnityEngine.Object.Instantiate<GameObject>(this.ItemTemplate);
        BookmarkUnit component = root.GetComponent<BookmarkUnit>();
        bool flag1 = this.mBookmarkedPieces.Exists((Predicate<QuestBookmarkWindow.ItemAndQuests>) (p => p.itemName == itemQuests.itemName));
        component.BookmarkIcon.SetActive(flag1);
        long serverTime = Network.GetServerTime();
        bool flag2 = false;
        IEnumerable<QuestParam> questParams = !(sectionName == this.BookmarkSectionName) ? itemQuests.quests.Where<QuestParam>((Func<QuestParam, bool>) (q => q.world == sectionName)) : (IEnumerable<QuestParam>) itemQuests.quests;
        foreach (QuestParam questParam1 in questParams)
        {
          QuestParam questParam2;
          if (dictionary.TryGetValue(questParam1.iname, out questParam2) && questParam2.IsOpenUnitHave() && this.IsAvailableQuest(questParam2, serverTime))
          {
            flag2 = true;
            break;
          }
        }
        component.Overlay.SetActive(!flag2);
        component.Button.AddListener(new SRPG_Button.ButtonClickEvent(this.OnUnitSelect));
        ((Selectable) component.Button).interactable = flag2;
        ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(itemQuests.itemName);
        UnitParam data = ((IEnumerable<UnitParam>) allUnits).FirstOrDefault<UnitParam>((Func<UnitParam, bool>) (unit => unit.piece == itemParam.iname));
        DataSource.Bind<ItemParam>(root, itemParam);
        DataSource.Bind<UnitParam>(root, data);
        DataSource.Bind<QuestBookmarkWindow.ItemAndQuests>(root, itemQuests);
        root.transform.SetParent(this.ItemContainer.transform, false);
        this.mCurrentUnitObjects.Add(root);
        GameParameter.UpdateAll(root);
        int num1 = 0;
        int num2 = 0;
        foreach (QuestParam questParam in questParams)
        {
          num1 += questParam.GetChallangeCount();
          num2 += questParam.GetChallangeLimit();
        }
        component.Challenge.text = num1.ToString();
        component.Limit.text = num2.ToString();
        root.SetActive(true);
      }
    }

    private void OnUnitSelect(SRPG_Button button)
    {
      if (!((Selectable) button).interactable || this.mSelectQuestFlag)
        return;
      QuestBookmarkWindow.ItemAndQuests dataOfClass1 = DataSource.FindDataOfClass<QuestBookmarkWindow.ItemAndQuests>(((Component) button).gameObject, (QuestBookmarkWindow.ItemAndQuests) null);
      long currentTime = Network.GetServerTime();
      QuestParam[] availableQuests = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
      QuestParam[] quests = !(this.mLastSectionName == this.BookmarkSectionName) ? dataOfClass1.quests.Where<QuestParam>((Func<QuestParam, bool>) (q => q.world == this.mLastSectionName)).ToArray<QuestParam>() : QuestDropParam.Instance.GetItemDropQuestList(MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(dataOfClass1.itemName), GlobalVars.GetDropTableGeneratedDateTime()).Where<QuestParam>((Func<QuestParam, bool>) (q => this.mAvailableSections.Contains(q.world))).ToArray<QuestParam>();
      if (quests.Length <= 0)
        return;
      List<QuestParam> questParamList = new List<QuestParam>();
      foreach (QuestParam questParam1 in quests)
      {
        foreach (QuestParam questParam2 in ((IEnumerable<QuestParam>) availableQuests).Where<QuestParam>((Func<QuestParam, bool>) (q => this.IsAvailableQuest(q, currentTime))))
        {
          if (questParam1.iname == questParam2.iname)
            questParamList.Add(questParam1);
        }
      }
      if (questParamList.Count <= 0)
      {
        QuestParam questParam = quests[0];
        UIUtility.SystemMessage((string) null, LocalizedText.Get("sys.TXT_QUESTBOOKMARK_BOOKMARK_NOT_AVAIABLE_QUEST", (object) questParam.title, (object) questParam.name), (UIUtility.DialogResultEvent) null, systemModal: true);
      }
      else if (this.mIsBookmarkEditing)
        this.OnUnitSelectBookmark(dataOfClass1, ((Component) button).GetComponent<BookmarkUnit>());
      else if (quests.Length > 1)
      {
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.QuestSelectorTemplate, (UnityEngine.Object) null))
          return;
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.QuestSelectorTemplate);
        gameObject.transform.SetParent(((Component) this).transform.parent, false);
        QuestBookmarkKakeraWindow component = gameObject.GetComponent<QuestBookmarkKakeraWindow>();
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          return;
        UnitParam dataOfClass2 = DataSource.FindDataOfClass<UnitParam>(((Component) button).gameObject, (UnitParam) null);
        component.Refresh(dataOfClass2, (IEnumerable<QuestParam>) quests);
      }
      else
      {
        this.mSelectQuestFlag = true;
        GlobalVars.SelectedQuestID = quests[0].iname;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      }
    }

    private void SetActivateWithoutBookmarkedUnit(bool doActivate)
    {
      foreach (GameObject currentUnitObject in this.mCurrentUnitObjects)
      {
        BookmarkUnit component = currentUnitObject.GetComponent<BookmarkUnit>();
        ItemParam param = DataSource.FindDataOfClass<ItemParam>(((Component) component).gameObject, (ItemParam) null);
        if (param != null && this.mBookmarkedPieces.FirstOrDefault<QuestBookmarkWindow.ItemAndQuests>((Func<QuestBookmarkWindow.ItemAndQuests, bool>) (b => b.itemName == param.iname)) == null)
        {
          ((Selectable) component.Button).interactable = doActivate;
          component.Overlay.SetActive(!doActivate);
        }
      }
    }

    private bool AddBookmark(QuestBookmarkWindow.ItemAndQuests item)
    {
      if (this.mBookmarkedPieces.Count > this.MaxBookmarkCount)
        return false;
      foreach (QuestBookmarkWindow.ItemAndQuests itemAndQuests in this.mAllSection)
      {
        QuestBookmarkWindow.ItemAndQuests param = itemAndQuests;
        if (param.itemName == item.itemName)
        {
          for (int i = 0; i < param.quests.Count; ++i)
          {
            if (item.quests.FindIndex((Predicate<QuestParam>) (quest => quest == param.quests[i])) < 0)
              item.quests.Add(param.quests[i]);
          }
        }
      }
      this.mBookmarkedPieces.Add(item);
      if (this.mBookmarkedPieces.Count >= this.MaxBookmarkCount)
        this.SetActivateWithoutBookmarkedUnit(false);
      this.SetDeactivateNotAvailableUnit(this.mIsBookmarkEditing);
      return true;
    }

    private void DeleteBookmark(QuestBookmarkWindow.ItemAndQuests item)
    {
      this.mBookmarkedPieces.RemoveAt(this.mBookmarkedPieces.FindIndex((Predicate<QuestBookmarkWindow.ItemAndQuests>) (p => p.itemName == item.itemName)));
      if (this.mBookmarkedPieces.Count < this.MaxBookmarkCount)
        this.SetActivateWithoutBookmarkedUnit(true);
      this.SetDeactivateNotAvailableUnit(this.mIsBookmarkEditing);
    }

    private void OnUnitSelectBookmark(QuestBookmarkWindow.ItemAndQuests target, BookmarkUnit unit)
    {
      if (this.mBookmarkedPieces.Exists((Predicate<QuestBookmarkWindow.ItemAndQuests>) (p => p.itemName == target.itemName)))
      {
        this.DeleteBookmark(target);
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) unit, (UnityEngine.Object) null))
          return;
        unit.BookmarkIcon.SetActive(false);
      }
      else
      {
        bool flag = this.AddBookmark(target);
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) unit, (UnityEngine.Object) null) || !flag)
          return;
        unit.BookmarkIcon.SetActive(true);
      }
    }

    public class ItemAndQuests
    {
      public string itemName;
      public List<QuestParam> quests;
    }

    public class JSON_Body
    {
      public QuestBookmarkWindow.JSON_Item[] result;
    }

    public class JSON_Item
    {
      public string iname;
    }
  }
}
