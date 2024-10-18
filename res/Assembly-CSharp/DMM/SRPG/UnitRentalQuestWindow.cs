// Decompiled with JetBrains decompiler
// Type: SRPG.UnitRentalQuestWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(10, "リスト切り替え", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(100, "クエストが選択された", FlowNode.PinTypes.Output, 100)]
  public class UnitRentalQuestWindow : MonoBehaviour, IFlowInterface
  {
    public Transform QuestList;
    public GameObject QuestItemTemplate;
    public GameObject QuestDisableItemTemplate;
    public GameObject QuestDetailTemplate;
    public string DisableFlagName = "is_disable";
    public GameObject CharacterImage;
    private List<QuestParam> mQuestList = new List<QuestParam>();
    private List<GameObject> mQuestListItems = new List<GameObject>();
    private GameObject mQuestDetail;
    private bool mListRefreshing;
    private bool mIsRestore;
    private UnitData CurrentUnit;
    private UnitRentalParam RentalParam;

    public bool IsRestore
    {
      get => this.mIsRestore;
      set => this.mIsRestore = value;
    }

    private void Start()
    {
      this.CurrentUnit = MonoSingleton<GameManager>.Instance.Player.GetRentalUnit();
      if (this.CurrentUnit == null)
        return;
      this.RentalParam = UnitRentalParam.GetParam(this.CurrentUnit.RentalIname);
      if (this.RentalParam == null)
        return;
      this.RefreshQuestList();
      GameParameter.UpdateAll(((Component) this).gameObject);
    }

    private void CreateQuestList()
    {
      this.mQuestList.Clear();
      List<RentalQuestInfo> unitQuestInfo = this.RentalParam.UnitQuestInfo;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      foreach (RentalQuestInfo data in unitQuestInfo)
      {
        QuestParam quest = instance.FindQuest(data.QuestId);
        this.mQuestList.Add(quest);
        GameObject gameObject = (int) data.Point > this.CurrentUnit.RentalFavoritePoint ? Object.Instantiate<GameObject>(this.QuestDisableItemTemplate) : Object.Instantiate<GameObject>(this.QuestItemTemplate);
        if (!Object.op_Equality((Object) gameObject, (Object) null))
        {
          gameObject.SetActive(true);
          gameObject.transform.SetParent(this.QuestList, false);
          DataSource.Bind<QuestParam>(gameObject, quest);
          DataSource.Bind<UnitData>(gameObject, this.CurrentUnit);
          DataSource.Bind<RentalQuestInfo>(gameObject, data);
          DataSource.Bind<UnitRentalParam>(gameObject, this.RentalParam);
          DataSource.Bind<UnitParam>(gameObject, this.CurrentUnit.UnitParam);
          ListItemEvents component = gameObject.GetComponent<ListItemEvents>();
          component.OnSelect = new ListItemEvents.ListItemEvent(this.OnQuestSelect);
          component.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnOpenItemDetail);
          component.OnCloseDetail = new ListItemEvents.ListItemEvent(this.OnCloseItemDetail);
          this.mQuestListItems.Add(gameObject);
        }
      }
    }

    private void RefreshQuestList()
    {
      if (this.mListRefreshing || Object.op_Equality((Object) this.QuestList, (Object) null))
        return;
      this.mListRefreshing = true;
      if (this.mQuestListItems.Count <= 0)
        this.CreateQuestList();
      GameManager instance = MonoSingleton<GameManager>.Instance;
      DataSource.Bind<UnitData>(((Component) this).gameObject, this.CurrentUnit);
      this.mListRefreshing = false;
    }

    private void OnQuestSelect(GameObject button)
    {
      QuestParam dataOfClass1 = DataSource.FindDataOfClass<QuestParam>(button.gameObject, (QuestParam) null);
      RentalQuestInfo dataOfClass2 = DataSource.FindDataOfClass<RentalQuestInfo>(button.gameObject, (RentalQuestInfo) null);
      if (dataOfClass1 == null || this.RentalParam == null)
        return;
      if ((int) dataOfClass2.Point > this.CurrentUnit.RentalFavoritePoint)
      {
        UIUtility.NegativeSystemMessage((string) null, string.Format(LocalizedText.Get("sys.UR_QUEST_LOCKED_MSG"), (object) this.CurrentUnit.UnitParam.name, (object) (int) ((double) ((int) dataOfClass2.Point * 100) / (double) (int) this.RentalParam.PtMax)), (UIUtility.DialogResultEvent) null);
      }
      else
      {
        GlobalVars.SelectedQuestID = dataOfClass1.iname;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      }
    }

    private void OnCloseItemDetail(GameObject go)
    {
      if (!Object.op_Inequality((Object) this.mQuestDetail, (Object) null))
        return;
      Object.DestroyImmediate((Object) this.mQuestDetail.gameObject);
      this.mQuestDetail = (GameObject) null;
    }

    private void OnOpenItemDetail(GameObject go)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      QuestParam dataOfClass = DataSource.FindDataOfClass<QuestParam>(go, (QuestParam) null);
      if (!Object.op_Equality((Object) this.mQuestDetail, (Object) null) || dataOfClass == null)
        return;
      this.mQuestDetail = Object.Instantiate<GameObject>(this.QuestDetailTemplate);
      DataSource.Bind<QuestParam>(this.mQuestDetail, dataOfClass);
      DataSource.Bind<UnitData>(this.mQuestDetail, this.CurrentUnit);
      this.mQuestDetail.SetActive(true);
    }

    public void Activated(int pinID)
    {
      if (pinID != 10)
        return;
      this.RefreshQuestList();
    }
  }
}
