// Decompiled with JetBrains decompiler
// Type: SRPG.TrophyRecordTab
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

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

    public int HashCode
    {
      get
      {
        return this.CategoryData.Param.hash_code;
      }
    }

    private void Awake()
    {
      if ((UnityEngine.Object) this.Badge != (UnityEngine.Object) null)
        this.Badge.SetActive(false);
      if (!((UnityEngine.Object) this.Cursor != (UnityEngine.Object) null))
        return;
      this.Cursor.SetActive(false);
    }

    public void Init(string title, Sprite banner = null)
    {
      this.gameObject.SetActive(true);
      if ((UnityEngine.Object) this.Text != (UnityEngine.Object) null)
        this.Text.text = title;
      if ((UnityEngine.Object) this.Banner != (UnityEngine.Object) null && (UnityEngine.Object) banner != (UnityEngine.Object) null)
      {
        this.Banner.sprite = banner;
        this.Banner.gameObject.SetActive(true);
        this.Body.enabled = false;
      }
      else
      {
        this.Banner.gameObject.SetActive(false);
        this.Body.enabled = true;
      }
    }

    public void Setup(int _index)
    {
      this.Index = _index;
    }

    public void RefreshDisplayParam()
    {
      int num = 0;
      for (int index = 0; index < this.CategoryData.Trophies.Count; ++index)
      {
        if (this.CategoryData.Trophies[index].IsCompleted)
          ++num;
      }
      if (!((UnityEngine.Object) this.Badge != (UnityEngine.Object) null))
        return;
      this.Badge.SetActive(num > 0);
    }

    public void SetCategoryData(TrophyCategoryData _category_data)
    {
      this.CategoryData = _category_data;
    }

    public void SetCursor(bool isOn)
    {
      if (!((UnityEngine.Object) this.Cursor != (UnityEngine.Object) null))
        return;
      this.Cursor.SetActive(isOn);
    }
  }
}
