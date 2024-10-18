// Decompiled with JetBrains decompiler
// Type: SRPG.TobiraStatusCheckWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class TobiraStatusCheckWindow : MonoBehaviour
  {
    [SerializeField]
    private RectTransform m_TobiraStatusListItemRoot;
    [SerializeField]
    private TobiraStatusListItem m_TobiraStatusListItemTemplate;

    private void Start()
    {
      this.Setup(MonoSingleton<GameManager>.Instance.Player.GetUnitData(UnitTobiraInventory.InitTimeUniqueID));
    }

    private void Setup(UnitData unit_data)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_TobiraStatusListItemTemplate, (UnityEngine.Object) null) || unit_data == null)
        return;
      ((Component) this.m_TobiraStatusListItemTemplate).gameObject.SetActive(false);
      TobiraParam[] array = ((IEnumerable<TobiraParam>) MonoSingleton<GameManager>.Instance.MasterParam.GetTobiraListForUnit(unit_data.UnitParam.iname)).Where<TobiraParam>((Func<TobiraParam, bool>) (tobiraParam => tobiraParam.TobiraCategory != TobiraParam.Category.START)).ToArray<TobiraParam>();
      for (int index = 0; index < array.Length; ++index)
      {
        TobiraData tobiraData1 = unit_data.GetTobiraData(array[index].TobiraCategory);
        TobiraStatusListItem listItem = this.CreateListItem();
        if (tobiraData1 != null)
          listItem.SetTobiraLvIsMax(tobiraData1.IsMaxLv);
        else
          listItem.SetTobiraLvIsMax(false);
        if (!array[index].Enable)
        {
          listItem.Setup(array[index]);
        }
        else
        {
          TobiraData tobiraData2 = new TobiraData();
          tobiraData2.Setup(unit_data.UnitParam.iname, array[index].TobiraCategory, (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.TobiraLvCap);
          listItem.Setup(tobiraData2);
        }
      }
    }

    private TobiraStatusListItem CreateListItem()
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(((Component) this.m_TobiraStatusListItemTemplate).gameObject);
      TobiraStatusListItem component = gameObject.GetComponent<TobiraStatusListItem>();
      gameObject.transform.SetParent((Transform) this.m_TobiraStatusListItemRoot, false);
      gameObject.SetActive(true);
      return component;
    }
  }
}
