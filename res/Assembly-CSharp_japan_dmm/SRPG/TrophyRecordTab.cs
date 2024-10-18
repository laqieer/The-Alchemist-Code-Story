// Decompiled with JetBrains decompiler
// Type: SRPG.TrophyRecordTab
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class TrophyRecordTab : MonoBehaviour
  {
    [SerializeField]
    private GameObject Badge;
    [SerializeField]
    private Image Body;
    [SerializeField]
    private Text Text;
    [SerializeField]
    private GameObject Cursor;
    [SerializeField]
    private Image Banner;
    [HideInInspector]
    public TrophyCategoryData CategoryData;
    [HideInInspector]
    public int Index;

    public int HashCode => this.CategoryData.Param.hash_code;

    private void Awake()
    {
      if (Object.op_Inequality((Object) this.Badge, (Object) null))
        this.Badge.SetActive(false);
      if (!Object.op_Inequality((Object) this.Cursor, (Object) null))
        return;
      this.Cursor.SetActive(false);
    }

    public void Init(string title, Sprite banner = null)
    {
      ((Component) this).gameObject.SetActive(true);
      if (Object.op_Inequality((Object) this.Text, (Object) null))
        this.Text.text = title;
      if (Object.op_Inequality((Object) this.Banner, (Object) null) && Object.op_Inequality((Object) banner, (Object) null))
      {
        this.Banner.sprite = banner;
        ((Component) this.Banner).gameObject.SetActive(true);
        ((Behaviour) this.Body).enabled = false;
      }
      else
      {
        ((Component) this.Banner).gameObject.SetActive(false);
        ((Behaviour) this.Body).enabled = true;
      }
    }

    public void Setup(int _index) => this.Index = _index;

    public void RefreshDisplayParam()
    {
      int num = 0;
      for (int index = 0; index < this.CategoryData.Trophies.Count; ++index)
      {
        if (this.CategoryData.Trophies[index].IsCompleted)
          ++num;
      }
      if (!Object.op_Inequality((Object) this.Badge, (Object) null))
        return;
      this.Badge.SetActive(num > 0);
    }

    public void SetCategoryData(TrophyCategoryData _category_data)
    {
      this.CategoryData = _category_data;
    }

    public void SetCursor(bool isOn)
    {
      if (!Object.op_Inequality((Object) this.Cursor, (Object) null))
        return;
      this.Cursor.SetActive(isOn);
    }
  }
}
