// Decompiled with JetBrains decompiler
// Type: SRPG.MultiPlayUnitRank
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
      DataSource.Bind<QuestParam>(this.gameObject, instance.FindQuest(GlobalVars.SelectedQuestID));
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
            data.Setup(multiUnitRank[rank].unit, 0, 1, 0, multiUnitRank[rank].job, 1, unitParam.element);
            DataSource.Bind<UnitData>(gameObject, data);
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
      Transform child1 = item.transform.FindChild("body");
      if (!((UnityEngine.Object) child1 != (UnityEngine.Object) null))
        return;
      Transform child2 = child1.FindChild(nameof (rank));
      if ((UnityEngine.Object) child2 != (UnityEngine.Object) null)
      {
        Transform child3 = child2.FindChild("rank_txt");
        if ((UnityEngine.Object) child3 != (UnityEngine.Object) null)
        {
          LText component = child3.GetComponent<LText>();
          if ((UnityEngine.Object) component != (UnityEngine.Object) null)
          {
            string str = string.Format(LocalizedText.Get("sys.MULTI_CLEAR_RANK"), (object) (rank + 1).ToString());
            component.text = str;
          }
        }
      }
      UnitParam unitParam = MonoSingleton<GameManager>.Instance.GetUnitParam(param.unit);
      JobParam jobParam = MonoSingleton<GameManager>.Instance.GetJobParam(param.job);
      Transform child4 = child1.FindChild("name");
      if ((UnityEngine.Object) child4 != (UnityEngine.Object) null)
      {
        LText component = child4.GetComponent<LText>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null)
        {
          string str = string.Format(LocalizedText.Get("sys.MULTI_CLEAR_UNIT_NAME"), (object) unitParam.name, (object) jobParam.name);
          component.text = str;
        }
      }
      this.SetIcon(child1, jobParam);
    }

    public void SetIcon(Transform body, JobParam job)
    {
      Transform child1 = body.FindChild("ui_uniticon");
      if ((UnityEngine.Object) child1 == (UnityEngine.Object) null)
        return;
      Transform child2 = child1.FindChild("unit");
      if ((UnityEngine.Object) child2 == (UnityEngine.Object) null)
        return;
      Transform child3 = child2.FindChild(nameof (job));
      if ((UnityEngine.Object) child3 == (UnityEngine.Object) null)
        return;
      RawImage_Transparent component = child3.GetComponent<RawImage_Transparent>();
      if ((UnityEngine.Object) component == (UnityEngine.Object) null)
        return;
      MonoSingleton<GameManager>.Instance.ApplyTextureAsync((RawImage) component, job == null ? (string) null : AssetPath.JobIconSmall(job));
    }
  }
}
