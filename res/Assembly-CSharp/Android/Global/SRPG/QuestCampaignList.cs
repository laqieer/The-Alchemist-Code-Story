// Decompiled with JetBrains decompiler
// Type: SRPG.QuestCampaignList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class QuestCampaignList : MonoBehaviour
  {
    public Color TextConsumeApColor = Color.white;
    public GameObject TemplateExpPlayer;
    public GameObject TemplateExpUnit;
    public GameObject TemplateExpUnitAll;
    public GameObject TemplateDrapItem;
    public GameObject TemplateAp;
    [Space(10f)]
    public Text TextConsumeAp;

    public void RefreshIcons()
    {
      QuestParam dataOfClass1 = DataSource.FindDataOfClass<QuestParam>(this.gameObject, (QuestParam) null);
      if (dataOfClass1 != null && dataOfClass1.type == QuestTypes.Tower)
      {
        this.gameObject.SetActive(false);
      }
      else
      {
        QuestCampaignData[] dataOfClass2 = DataSource.FindDataOfClass<QuestCampaignData[]>(this.gameObject, (QuestCampaignData[]) null);
        if (dataOfClass2 == null || dataOfClass2.Length == 0)
        {
          this.gameObject.SetActive(false);
        }
        else
        {
          List<GameObject> gameObjectList = new List<GameObject>();
          for (int index = 0; index < this.transform.childCount; ++index)
          {
            Transform child = this.transform.GetChild(index);
            if (!((UnityEngine.Object) this.TemplateExpPlayer == (UnityEngine.Object) child.gameObject) && !((UnityEngine.Object) this.TemplateExpUnit == (UnityEngine.Object) child.gameObject) && (!((UnityEngine.Object) this.TemplateExpUnitAll == (UnityEngine.Object) child.gameObject) && !((UnityEngine.Object) this.TemplateDrapItem == (UnityEngine.Object) child.gameObject)) && !((UnityEngine.Object) this.TemplateAp == (UnityEngine.Object) child.gameObject))
              gameObjectList.Add(child.gameObject);
          }
          while (gameObjectList.Count != 0)
          {
            GameObject gameObject = gameObjectList[0];
            gameObjectList.Remove(gameObject);
            UnityEngine.Object.DestroyImmediate((UnityEngine.Object) gameObject);
          }
          bool flag = false;
          for (int index = 0; index < dataOfClass2.Length && index != 2; ++index)
          {
            GameObject original = (GameObject) null;
            QuestCampaignData data = dataOfClass2[index];
            switch (data.type)
            {
              case QuestCampaignValueTypes.ExpPlayer:
                original = this.TemplateExpPlayer;
                break;
              case QuestCampaignValueTypes.ExpUnit:
                original = !string.IsNullOrEmpty(data.unit) ? this.TemplateExpUnit : this.TemplateExpUnitAll;
                break;
              case QuestCampaignValueTypes.DropRate:
              case QuestCampaignValueTypes.DropNum:
                if (!flag)
                {
                  original = this.TemplateDrapItem;
                  flag = true;
                  break;
                }
                break;
              case QuestCampaignValueTypes.Ap:
                original = this.TemplateAp;
                if ((UnityEngine.Object) this.TextConsumeAp != (UnityEngine.Object) null)
                {
                  this.TextConsumeAp.color = this.TextConsumeApColor;
                  break;
                }
                break;
            }
            if ((UnityEngine.Object) original != (UnityEngine.Object) null)
            {
              GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original);
              Vector3 localScale = gameObject.transform.localScale;
              gameObject.transform.SetParent(this.transform);
              gameObject.transform.localScale = localScale;
              DataSource.Bind<QuestCampaignData>(gameObject, data);
              gameObject.SetActive(true);
            }
          }
          if (this.gameObject.activeSelf)
            return;
          this.gameObject.SetActive(true);
        }
      }
    }

    private void Start()
    {
      this.RefreshIcons();
    }
  }
}
