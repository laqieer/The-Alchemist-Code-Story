// Decompiled with JetBrains decompiler
// Type: SRPG.QuestCampaignList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
    public GameObject TemplateTrustUp;
    public GameObject TemplateTrustIncidence;
    public GameObject TemplateTrustSpecific;
    public GameObject TemplateInspirationSkill;
    [Space(10f)]
    public Text TextConsumeAp;

    public void RefreshIcons()
    {
      this.ResetTemplateActive();
      QuestParam dataOfClass1 = DataSource.FindDataOfClass<QuestParam>(this.gameObject, (QuestParam) null);
      if (dataOfClass1 != null && dataOfClass1.type == QuestTypes.Tower)
        return;
      QuestCampaignData[] dataOfClass2 = DataSource.FindDataOfClass<QuestCampaignData[]>(this.gameObject, (QuestCampaignData[]) null);
      if (dataOfClass2 == null || dataOfClass2.Length == 0)
        return;
      bool flag = false;
      for (int index = 0; index < dataOfClass2.Length && index != 2; ++index)
      {
        GameObject gameObject = (GameObject) null;
        QuestCampaignData data = dataOfClass2[index];
        switch (data.type)
        {
          case QuestCampaignValueTypes.ExpPlayer:
            gameObject = this.TemplateExpPlayer;
            break;
          case QuestCampaignValueTypes.ExpUnit:
            gameObject = !string.IsNullOrEmpty(data.unit) ? this.TemplateExpUnit : this.TemplateExpUnitAll;
            break;
          case QuestCampaignValueTypes.DropRate:
          case QuestCampaignValueTypes.DropNum:
            if (!flag)
            {
              gameObject = this.TemplateDrapItem;
              flag = true;
              break;
            }
            break;
          case QuestCampaignValueTypes.Ap:
            gameObject = this.TemplateAp;
            if ((UnityEngine.Object) this.TextConsumeAp != (UnityEngine.Object) null)
            {
              this.TextConsumeAp.color = this.TextConsumeApColor;
              break;
            }
            break;
          case QuestCampaignValueTypes.TrustUp:
            gameObject = this.TemplateTrustUp;
            break;
          case QuestCampaignValueTypes.TrustIncidence:
            gameObject = this.TemplateTrustIncidence;
            break;
          case QuestCampaignValueTypes.TrustSpecific:
            gameObject = this.TemplateTrustSpecific;
            break;
          case QuestCampaignValueTypes.InspirationSkill:
            gameObject = this.TemplateInspirationSkill;
            break;
        }
        if ((UnityEngine.Object) gameObject != (UnityEngine.Object) null)
        {
          DataSource.Bind<QuestCampaignData>(gameObject, data, false);
          gameObject.SetActive(true);
        }
      }
      if (this.gameObject.activeSelf)
        return;
      this.gameObject.SetActive(true);
    }

    private void Start()
    {
      this.RefreshIcons();
    }

    private void ResetTemplateActive()
    {
      if ((UnityEngine.Object) this.TemplateExpPlayer != (UnityEngine.Object) null)
        this.TemplateExpPlayer.SetActive(false);
      if ((UnityEngine.Object) this.TemplateExpUnit != (UnityEngine.Object) null)
        this.TemplateExpUnit.SetActive(false);
      if ((UnityEngine.Object) this.TemplateExpUnitAll != (UnityEngine.Object) null)
        this.TemplateExpUnitAll.SetActive(false);
      if ((UnityEngine.Object) this.TemplateDrapItem != (UnityEngine.Object) null)
        this.TemplateDrapItem.SetActive(false);
      if ((UnityEngine.Object) this.TemplateAp != (UnityEngine.Object) null)
        this.TemplateAp.SetActive(false);
      if ((UnityEngine.Object) this.TemplateTrustUp != (UnityEngine.Object) null)
        this.TemplateTrustUp.SetActive(false);
      if ((UnityEngine.Object) this.TemplateTrustIncidence != (UnityEngine.Object) null)
        this.TemplateTrustIncidence.SetActive(false);
      if (!((UnityEngine.Object) this.TemplateTrustSpecific != (UnityEngine.Object) null))
        return;
      this.TemplateTrustSpecific.SetActive(false);
    }
  }
}
