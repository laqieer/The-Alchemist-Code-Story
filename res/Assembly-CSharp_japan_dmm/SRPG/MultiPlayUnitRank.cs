// Decompiled with JetBrains decompiler
// Type: SRPG.MultiPlayUnitRank
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
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
      if (Object.op_Equality((Object) this.ItemTemplate, (Object) null))
        return;
      this.RefreshData();
    }

    private void RefreshData()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      List<MultiRanking> multiUnitRank = instance.MultiUnitRank;
      DataSource.Bind<QuestParam>(((Component) this).gameObject, instance.FindQuest(GlobalVars.SelectedQuestID));
      if (multiUnitRank == null || multiUnitRank.Count == 0)
      {
        if (Object.op_Inequality((Object) this.NotDataObj, (Object) null))
          this.NotDataObj.gameObject.SetActive(true);
        if (Object.op_Inequality((Object) this.DataObj, (Object) null))
          this.DataObj.gameObject.SetActive(false);
      }
      else
      {
        if (Object.op_Inequality((Object) this.NotDataObj, (Object) null))
          this.NotDataObj.gameObject.SetActive(false);
        if (Object.op_Inequality((Object) this.DataObj, (Object) null))
          this.DataObj.gameObject.SetActive(true);
        for (int index = 0; index < multiUnitRank.Count; ++index)
        {
          GameObject gameObject = Object.Instantiate<GameObject>(this.ItemTemplate);
          if (Object.op_Inequality((Object) gameObject, (Object) null))
          {
            UnitParam unitParam = instance.GetUnitParam(multiUnitRank[index].unit);
            UnitData data = new UnitData();
            data.Setup(multiUnitRank[index].unit, 0, 1, 0, multiUnitRank[index].job, elem: unitParam.element);
            DataSource.Bind<UnitData>(gameObject, data);
            this.SetParam(gameObject, index, multiUnitRank[index]);
            if (Object.op_Inequality((Object) this.Parent, (Object) null))
              gameObject.transform.SetParent(this.Parent.transform, false);
            gameObject.gameObject.SetActive(true);
          }
        }
      }
      if (!Object.op_Inequality((Object) this.Root, (Object) null))
        return;
      GameParameter.UpdateAll(this.Root);
    }

    private void SetParam(GameObject item, int rank, MultiRanking param)
    {
      Transform body = item.transform.Find("body");
      if (!Object.op_Inequality((Object) body, (Object) null))
        return;
      Transform transform1 = body.Find(nameof (rank));
      if (Object.op_Inequality((Object) transform1, (Object) null))
      {
        Transform transform2 = transform1.Find("rank_txt");
        if (Object.op_Inequality((Object) transform2, (Object) null))
        {
          LText component = ((Component) transform2).GetComponent<LText>();
          if (Object.op_Inequality((Object) component, (Object) null))
          {
            string str = string.Format(LocalizedText.Get("sys.MULTI_CLEAR_RANK"), (object) (rank + 1).ToString());
            component.text = str;
          }
        }
      }
      UnitParam unitParam = MonoSingleton<GameManager>.Instance.GetUnitParam(param.unit);
      JobParam jobParam = MonoSingleton<GameManager>.Instance.GetJobParam(param.job);
      Transform transform3 = body.Find("name");
      if (Object.op_Inequality((Object) transform3, (Object) null))
      {
        LText component = ((Component) transform3).GetComponent<LText>();
        if (Object.op_Inequality((Object) component, (Object) null))
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
      if (Object.op_Equality((Object) transform1, (Object) null))
        return;
      Transform transform2 = transform1.Find("unit");
      if (Object.op_Equality((Object) transform2, (Object) null))
        return;
      Transform transform3 = transform2.Find(nameof (job));
      if (Object.op_Equality((Object) transform3, (Object) null))
        return;
      RawImage_Transparent component = ((Component) transform3).GetComponent<RawImage_Transparent>();
      if (Object.op_Equality((Object) component, (Object) null))
        return;
      MonoSingleton<GameManager>.Instance.ApplyTextureAsync((RawImage) component, job == null ? (string) null : AssetPath.JobIconSmall(job));
    }
  }
}
