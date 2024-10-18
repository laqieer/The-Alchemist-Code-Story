// Decompiled with JetBrains decompiler
// Type: SRPG.QuestListItemExtention
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
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

    public void UpdateValue() => ((Behaviour) this).enabled = true;

    private void Update()
    {
      this.m_LayoutElement.minHeight = 160f;
      bool flag = false;
      bool activeInHierarchy = ((Component) this).gameObject.activeInHierarchy;
      for (int index = 0; index < ((Component) this).transform.childCount; ++index)
        flag |= ((Component) ((Component) this).transform.GetChild(index)).gameObject.activeInHierarchy;
      ((Component) this).gameObject.SetActive(flag);
      ((Behaviour) this).enabled = false;
      if (flag)
      {
        RectTransform component = ((Component) this).GetComponent<RectTransform>();
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
      Debug.Log((object) (activeInHierarchy.ToString() + " => " + (object) flag + " child (" + (object) ((Component) this).transform.childCount + ")"));
    }
  }
}
