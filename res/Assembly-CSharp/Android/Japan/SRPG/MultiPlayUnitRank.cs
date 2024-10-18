// Decompiled with JetBrains decompiler
// Type: SRPG.MultiPlayUnitRank
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class MultiPlayUnitRank : MonoBehaviour
  {
    public GameObject ItemTemplate;
    public GameObject Parent;
    public GameObject Root;
    public GameObject NotDataObj;
    public GameObject DataObj;

    private void Start()
    {
      if ((UnityEngine.Object) this.ItemTemplate == (UnityEngine.Object) null)
        return;
      this.RefreshData();
    }

    private void RefreshData()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      List<MultiRanking> multiUnitRank = instance.MultiUnitRank;
      DataSource.Bind<QuestParam>(this.gameObject, instance.FindQuest(GlobalVars.SelectedQuestID), false);
      if (multiUnitRank == null || multiUnitRank.Count == 0)
      {
        if ((UnityEngine.Object) this.NotDataObj != (UnityEngine.Object) null)
          this.NotDataObj.gameObject.SetActive(true);
        if ((UnityEngine.Object) this.DataObj != (UnityEngine.Object) null)
          this.DataObj.gameObject.SetActive(false);
      }
      else
      {
        if ((UnityEngine.Object) this.NotDataObj != (UnityEngine.Object) null)
          this.NotDataObj.gameObject.SetActive(false);
        if ((UnityEngine.Object) this.DataObj != (UnityEngine.Object) null)
          this.DataObj.gameObject.SetActive(true);
        for (int rank = 0; rank < multiUnitRank.Count; ++rank)
        {
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ItemTemplate);
          if ((UnityEngine.Object) gameObject != (UnityEngine.Object) null)
          {
            UnitParam unitParam = instance.GetUnitParam(multiUnitRank[rank].unit);
            UnitData data = new UnitData();
            data.Setup(multiUnitRank[rank].unit, 0, 1, 0, multiUnitRank[rank].job, 1, unitParam.element, 0);
            DataSource.Bind<UnitData>(gameObject, data, false);
            this.SetParam(gameObject, rank, multiUnitRank[rank]);
            if ((UnityEngine.Object) this.Parent != (UnityEngine.Object) null)
              gameObject.transform.SetParent(this.Parent.transform, false);
            gameObject.gameObject.SetActive(true);
          }
        }
      }
      if (!((UnityEngine.Object) this.Root != (UnityEngine.Object) null))
        return;
      GameParameter.UpdateAll(this.Root);
    }

    private void SetParam(GameObject item, int rank, MultiRanking param)
    {
      Transform body = item.transform.Find("body");
      if (!((UnityEngine.Object) body != (UnityEngine.Object) null))
        return;
      Transform transform1 = body.Find(nameof (rank));
      if ((UnityEngine.Object) transform1 != (UnityEngine.Object) null)
      {
        Transform transform2 = transform1.Find("rank_txt");
        if ((UnityEngine.Object) transform2 != (UnityEngine.Object) null)
        {
          LText component = transform2.GetComponent<LText>();
          if ((UnityEngine.Object) component != (UnityEngine.Object) null)
          {
            string str = string.Format(LocalizedText.Get("sys.MULTI_CLEAR_RANK"), (object) (rank + 1).ToString());
            component.text = str;
          }
        }
      }
      UnitParam unitParam = MonoSingleton<GameManager>.Instance.GetUnitParam(param.unit);
      JobParam jobParam = MonoSingleton<GameManager>.Instance.GetJobParam(param.job);
      Transform transform3 = body.Find("name");
      if ((UnityEngine.Object) transform3 != (UnityEngine.Object) null)
      {
        LText component = transform3.GetComponent<LText>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null)
        {
          string str = string.Format(LocalizedText.Get("sys.MULTI_CLEAR_UNIT_NAME"), (object) unitParam.name, (object) jobParam.name);
          component.text = str;
        }
      }
      this.SetIcon(body, jobParam);
    }

    public void SetIcon(Transform body, JobParam job)
    {
      Transform transform1 = body.Find("ui_uniticon");
      if ((UnityEngine.Object) transform1 == (UnityEngine.Object) null)
        return;
      Transform transform2 = transform1.Find("unit");
      if ((UnityEngine.Object) transform2 == (UnityEngine.Object) null)
        return;
      Transform transform3 = transform2.Find(nameof (job));
      if ((UnityEngine.Object) transform3 == (UnityEngine.Object) null)
        return;
      RawImage_Transparent component = transform3.GetComponent<RawImage_Transparent>();
      if ((UnityEngine.Object) component == (UnityEngine.Object) null)
        return;
      MonoSingleton<GameManager>.Instance.ApplyTextureAsync((RawImage) component, job == null ? (string) null : AssetPath.JobIconSmall(job));
    }
  }
}
