// Decompiled with JetBrains decompiler
// Type: SRPG.TobiraStatusCheckWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
      if ((UnityEngine.Object) this.m_TobiraStatusListItemTemplate == (UnityEngine.Object) null || unit_data == null)
        return;
      this.m_TobiraStatusListItemTemplate.gameObject.SetActive(false);
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
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.m_TobiraStatusListItemTemplate.gameObject);
      TobiraStatusListItem component = gameObject.GetComponent<TobiraStatusListItem>();
      gameObject.transform.SetParent((Transform) this.m_TobiraStatusListItemRoot, false);
      gameObject.SetActive(true);
      return component;
    }
  }
}
