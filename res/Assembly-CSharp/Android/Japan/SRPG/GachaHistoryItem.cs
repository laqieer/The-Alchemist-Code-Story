// Decompiled with JetBrains decompiler
// Type: SRPG.GachaHistoryItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class GachaHistoryItem : MonoBehaviour
  {
    private List<GameObject> mItems = new List<GameObject>();
    [SerializeField]
    private GameObject UnitIconObj;
    [SerializeField]
    private GameObject ItemIconObj;
    [SerializeField]
    private GameObject ArtifactIconObj;
    [SerializeField]
    private GameObject ConceptCardIconObj;
    [SerializeField]
    private GameObject TitleText;
    [SerializeField]
    private Transform ListParent;

    private void Start()
    {
      if ((UnityEngine.Object) this.UnitIconObj != (UnityEngine.Object) null)
        this.UnitIconObj.SetActive(false);
      if ((UnityEngine.Object) this.ItemIconObj != (UnityEngine.Object) null)
        this.ItemIconObj.SetActive(false);
      if ((UnityEngine.Object) this.ArtifactIconObj != (UnityEngine.Object) null)
        this.ArtifactIconObj.SetActive(false);
      if ((UnityEngine.Object) this.ConceptCardIconObj != (UnityEngine.Object) null)
        this.ConceptCardIconObj.SetActive(false);
      if ((UnityEngine.Object) this.ListParent == (UnityEngine.Object) null)
        DebugUtility.LogError("ListParentが設定されていません");
      else
        this.Initalize();
    }

    private void Update()
    {
    }

    private void Initalize()
    {
      GachaHistoryItemData dataOfClass = DataSource.FindDataOfClass<GachaHistoryItemData>(this.gameObject, (GachaHistoryItemData) null);
      if (dataOfClass == null)
      {
        DebugUtility.LogError("履歴が存在しません");
      }
      else
      {
        for (int index = dataOfClass.historys.Length - 1; index >= 0; --index)
        {
          GachaHistoryData history = dataOfClass.historys[index];
          if (history != null)
          {
            GameObject gameObject1 = (GameObject) null;
            bool flag = false;
            if (history.type == GachaDropData.Type.Unit)
            {
              gameObject1 = UnityEngine.Object.Instantiate<GameObject>(this.UnitIconObj);
              if ((UnityEngine.Object) gameObject1 != (UnityEngine.Object) null)
              {
                gameObject1.transform.SetParent(this.ListParent, false);
                gameObject1.SetActive(true);
                DataSource.Bind<UnitData>(gameObject1, GachaHistoryWindow.CreateUnitData(history.unit), false);
                flag = history.isNew;
                this.mItems.Add(gameObject1);
                gameObject1.transform.SetAsFirstSibling();
              }
            }
            else if (history.type == GachaDropData.Type.Item)
            {
              gameObject1 = UnityEngine.Object.Instantiate<GameObject>(this.ItemIconObj);
              if ((UnityEngine.Object) gameObject1 != (UnityEngine.Object) null)
              {
                gameObject1.transform.SetParent(this.ListParent, false);
                gameObject1.SetActive(true);
                DataSource.Bind<ItemData>(gameObject1, GachaHistoryWindow.CreateItemData(history.item, history.num), false);
                flag = history.isNew;
                this.mItems.Add(gameObject1);
                gameObject1.transform.SetAsFirstSibling();
              }
            }
            else if (history.type == GachaDropData.Type.Artifact)
            {
              gameObject1 = UnityEngine.Object.Instantiate<GameObject>(this.ArtifactIconObj);
              if ((UnityEngine.Object) gameObject1 != (UnityEngine.Object) null)
              {
                gameObject1.transform.SetParent(this.ListParent, false);
                gameObject1.SetActive(true);
                DataSource.Bind<ArtifactData>(gameObject1, GachaHistoryWindow.CreateArtifactData(history.artifact, history.rarity), false);
                flag = history.isNew;
                this.mItems.Add(gameObject1);
                gameObject1.transform.SetAsFirstSibling();
              }
            }
            else if (history.type == GachaDropData.Type.ConceptCard)
            {
              gameObject1 = UnityEngine.Object.Instantiate<GameObject>(this.ConceptCardIconObj);
              if ((UnityEngine.Object) gameObject1 != (UnityEngine.Object) null)
              {
                gameObject1.transform.SetParent(this.ListParent, false);
                gameObject1.SetActive(true);
                ConceptCardData cardDataForDisplay = ConceptCardData.CreateConceptCardDataForDisplay(history.conceptcard.iname);
                ConceptCardIcon component = gameObject1.GetComponent<ConceptCardIcon>();
                if ((UnityEngine.Object) component != (UnityEngine.Object) null)
                  component.Setup(cardDataForDisplay);
                flag = history.isNew;
                this.mItems.Add(gameObject1);
                gameObject1.transform.SetAsFirstSibling();
              }
            }
            if ((UnityEngine.Object) gameObject1 != (UnityEngine.Object) null)
            {
              SerializeValueBehaviour component = gameObject1.GetComponent<SerializeValueBehaviour>();
              if ((UnityEngine.Object) component != (UnityEngine.Object) null)
              {
                GameObject gameObject2 = component.list.GetGameObject("new");
                if ((UnityEngine.Object) gameObject2 != (UnityEngine.Object) null)
                  gameObject2.SetActive(flag);
              }
            }
          }
        }
        if ((UnityEngine.Object) this.TitleText != (UnityEngine.Object) null)
        {
          Text component = this.TitleText.GetComponent<Text>();
          if ((UnityEngine.Object) component != (UnityEngine.Object) null)
          {
            string str = LocalizedText.Get("sys.TEXT_GACHA_HISTORY_FOOTER", (object) dataOfClass.GetDropAt().ToString("yyyy/MM/dd HH:mm:ss"), (object) dataOfClass.gachaTitle);
            component.text = str;
          }
        }
        GameParameter.UpdateAll(this.gameObject);
      }
    }
  }
}
