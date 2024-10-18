// Decompiled with JetBrains decompiler
// Type: SRPG.QuestListItemExtention
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class QuestListItemExtention : MonoBehaviour, IGameParameter
  {
    [SerializeField]
    private LayoutElement m_LayoutElement;
    private Vector2 m_InitialLayoutElementMinSize;
    private Vector2 m_InitialLayoutElementPreferredSize;

    private void Start()
    {
      Vector2 vector2_1 = new Vector2();
      Vector2 vector2_2 = new Vector2();
      vector2_1.x = this.m_LayoutElement.minWidth;
      vector2_1.y = this.m_LayoutElement.minHeight;
      vector2_2.x = this.m_LayoutElement.preferredWidth;
      vector2_2.y = this.m_LayoutElement.minHeight;
      this.m_InitialLayoutElementMinSize = vector2_1;
      this.m_InitialLayoutElementPreferredSize = vector2_2;
    }

    public void UpdateValue()
    {
      this.enabled = true;
    }

    private void Update()
    {
      this.m_LayoutElement.minHeight = 160f;
      bool flag = false;
      bool activeInHierarchy = this.gameObject.activeInHierarchy;
      for (int index = 0; index < this.transform.childCount; ++index)
        flag |= this.transform.GetChild(index).gameObject.activeInHierarchy;
      this.gameObject.SetActive(flag);
      this.enabled = false;
      if (flag)
      {
        RectTransform component = this.GetComponent<RectTransform>();
        if ((double) this.m_InitialLayoutElementMinSize.x != 0.0)
          this.m_LayoutElement.minWidth = component.sizeDelta.x;
        else
          this.m_LayoutElement.preferredWidth = component.sizeDelta.x;
        if ((double) this.m_InitialLayoutElementMinSize.y != 0.0)
          this.m_LayoutElement.minHeight = component.sizeDelta.y;
        else
          this.m_LayoutElement.preferredHeight = component.sizeDelta.y;
      }
      else
      {
        this.m_LayoutElement.minWidth = this.m_InitialLayoutElementMinSize.x;
        this.m_LayoutElement.minHeight = this.m_InitialLayoutElementMinSize.y;
        this.m_LayoutElement.preferredWidth = this.m_InitialLayoutElementPreferredSize.x;
        this.m_LayoutElement.preferredHeight = this.m_InitialLayoutElementPreferredSize.y;
      }
      Debug.Log((object) (activeInHierarchy.ToString() + " => " + (object) flag + " child (" + (object) this.transform.childCount + ")"));
    }
  }
}
