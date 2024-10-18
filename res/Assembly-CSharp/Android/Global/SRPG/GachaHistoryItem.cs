// Decompiled with JetBrains decompiler
// Type: SRPG.GachaHistoryItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
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
        Dictionary<string, List<ArtifactData>> dictionary = new Dictionary<string, List<ArtifactData>>();
        for (int index = dataOfClass.historys.Length - 1; index >= 0; --index)
        {
          GachaHistoryData history = dataOfClass.historys[index];
          if (history != null)
          {
            if (history.type == GachaDropData.Type.Unit)
            {
              GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.UnitIconObj);
              if ((UnityEngine.Object) gameObject != (UnityEngine.Object) null)
              {
                gameObject.transform.SetParent(this.ListParent, false);
                gameObject.SetActive(true);
                DataSource.Bind<UnitData>(gameObject, GachaHistoryWindow.CreateUnitData(history.unit));
                NewBadgeParam data = new NewBadgeParam(true, history.isNew, NewBadgeType.Unit);
                DataSource.Bind<NewBadgeParam>(gameObject, data);
                this.mItems.Add(gameObject);
                gameObject.transform.SetAsFirstSibling();
              }
            }
            else if (history.type == GachaDropData.Type.Item)
            {
              GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ItemIconObj);
              if ((UnityEngine.Object) gameObject != (UnityEngine.Object) null)
              {
                gameObject.transform.SetParent(this.ListParent, false);
                gameObject.SetActive(true);
                DataSource.Bind<ItemData>(gameObject, GachaHistoryWindow.CreateItemData(history.item, history.num));
                NewBadgeParam data = new NewBadgeParam(true, history.isNew, NewBadgeType.Item);
                DataSource.Bind<NewBadgeParam>(gameObject, data);
                this.mItems.Add(gameObject);
                gameObject.transform.SetAsFirstSibling();
              }
            }
            else if (history.type == GachaDropData.Type.Artifact)
            {
              GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ArtifactIconObj);
              if ((UnityEngine.Object) gameObject != (UnityEngine.Object) null)
              {
                gameObject.transform.SetParent(this.ListParent, false);
                gameObject.SetActive(true);
                DataSource.Bind<ArtifactData>(gameObject, GachaHistoryWindow.CreateArtifactData(history.artifact, history.rarity));
                bool isnew = false;
                if (dictionary.ContainsKey(history.artifact.iname))
                {
                  if (dictionary[history.artifact.iname].Count > 0)
                    dictionary[history.artifact.iname].RemoveAt(0);
                  isnew = dictionary[history.artifact.iname].Count <= 0;
                }
                else
                {
                  List<ArtifactData> artifactsByArtifactId = MonoSingleton<GameManager>.Instance.Player.FindArtifactsByArtifactID(history.artifact.iname);
                  if (artifactsByArtifactId != null && artifactsByArtifactId.Count > 0)
                  {
                    artifactsByArtifactId.RemoveAt(0);
                    dictionary.Add(history.artifact.iname, artifactsByArtifactId);
                    isnew = artifactsByArtifactId.Count <= 0;
                  }
                }
                NewBadgeParam data = new NewBadgeParam(true, isnew, NewBadgeType.Artifact);
                DataSource.Bind<NewBadgeParam>(gameObject, data);
                this.mItems.Add(gameObject);
                gameObject.transform.SetAsFirstSibling();
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
