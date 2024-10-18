// Decompiled with JetBrains decompiler
// Type: SRPG.TabMaker
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class TabMaker : MonoBehaviour
  {
    private List<TabMaker.Info> m_InfoList = new List<TabMaker.Info>();
    public SerializeValueBehaviour m_TabNode;
    public Sprite m_CornerSpriteOff;
    public Sprite m_CornerSpriteOn;
    public Sprite m_SpriteOff;
    public Sprite m_SpriteOn;
    public TabMaker.Element[] m_Elements;

    private void Awake()
    {
      if (!((UnityEngine.Object) this.m_TabNode != (UnityEngine.Object) null))
        return;
      this.m_TabNode.gameObject.SetActive(false);
    }

    private void Start()
    {
    }

    private void OnDestroy()
    {
      this.Destroy();
    }

    private void Update()
    {
    }

    public void Create(string[] keys, Action<GameObject, SerializeValueList> callback)
    {
      if ((UnityEngine.Object) this.m_TabNode == (UnityEngine.Object) null)
      {
        Debug.LogException(new Exception("Failed TabNode Null > " + this.gameObject.name));
      }
      else
      {
        this.Destroy();
        for (int index = 0; index < keys.Length; ++index)
        {
          TabMaker.Element element = this.GetElement(keys[index]);
          if (element == null)
          {
            Debug.LogError((object) ("Tab素材見つかりません > " + (keys[index] == null ? "null" : keys[index])));
          }
          else
          {
            GameObject _node = UnityEngine.Object.Instantiate<GameObject>(this.m_TabNode.gameObject);
            SerializeValueBehaviour component = _node.GetComponent<SerializeValueBehaviour>();
            if ((UnityEngine.Object) component != (UnityEngine.Object) null)
            {
              Image uiImage1 = component.list.GetUIImage("off");
              if ((UnityEngine.Object) uiImage1 != (UnityEngine.Object) null)
              {
                if (index == 0 || index == keys.Length - 1)
                {
                  uiImage1.sprite = !((UnityEngine.Object) this.m_CornerSpriteOff != (UnityEngine.Object) null) ? this.m_SpriteOff : this.m_CornerSpriteOff;
                  if (index == keys.Length - 1)
                  {
                    Vector3 localScale = uiImage1.transform.localScale;
                    localScale.x *= -1f;
                    uiImage1.transform.localScale = localScale;
                  }
                }
                else
                  uiImage1.sprite = this.m_SpriteOff;
              }
              Image uiImage2 = component.list.GetUIImage("on");
              if ((UnityEngine.Object) uiImage2 != (UnityEngine.Object) null)
              {
                if (index == 0 || index == keys.Length - 1)
                {
                  uiImage2.sprite = !((UnityEngine.Object) this.m_CornerSpriteOn != (UnityEngine.Object) null) ? this.m_SpriteOn : this.m_CornerSpriteOn;
                  if (index == keys.Length - 1)
                  {
                    Vector3 localScale = uiImage2.transform.localScale;
                    localScale.x *= -1f;
                    uiImage2.transform.localScale = localScale;
                  }
                }
                else
                  uiImage2.sprite = this.m_SpriteOn;
              }
              Image uiImage3 = component.list.GetUIImage("icon");
              if ((UnityEngine.Object) uiImage3 != (UnityEngine.Object) null)
              {
                if ((UnityEngine.Object) element.icon != (UnityEngine.Object) null)
                {
                  uiImage3.sprite = element.icon;
                  uiImage3.gameObject.SetActive(true);
                }
                else
                  uiImage3.gameObject.SetActive(false);
              }
              Text uiLabel = component.list.GetUILabel("text");
              if ((UnityEngine.Object) uiLabel != (UnityEngine.Object) null)
              {
                if (!string.IsNullOrEmpty(element.text))
                {
                  uiLabel.text = element.text.IndexOf("sys.") != -1 ? LocalizedText.Get(element.text) : element.text;
                  uiLabel.gameObject.SetActive(true);
                }
                else
                  uiLabel.gameObject.SetActive(false);
              }
              component.list.AddObject("element", (object) element);
              if (callback != null)
                callback(_node, component.list);
              _node.name = element.key;
              _node.transform.SetParent(this.gameObject.transform, false);
              _node.SetActive(true);
              this.m_InfoList.Add(new TabMaker.Info(_node, component.list, element));
            }
          }
        }
      }
    }

    public void Destroy()
    {
      for (int index = 0; index < this.m_InfoList.Count; ++index)
      {
        TabMaker.Info info = this.m_InfoList[index];
        if (info != null && (UnityEngine.Object) info.node != (UnityEngine.Object) null)
          UnityEngine.Object.Destroy((UnityEngine.Object) info.node);
      }
      this.m_InfoList.Clear();
    }

    public TabMaker.Element GetElement(string key)
    {
      if (this.m_Elements == null)
        return (TabMaker.Element) null;
      for (int index = 0; index < this.m_Elements.Length; ++index)
      {
        if (this.m_Elements[index] != null && this.m_Elements[index].key == key)
          return this.m_Elements[index];
      }
      return (TabMaker.Element) null;
    }

    public TabMaker.Info[] GetInfos()
    {
      return this.m_InfoList.ToArray();
    }

    public TabMaker.Info GetInfo(string key)
    {
      for (int index = 0; index < this.m_InfoList.Count; ++index)
      {
        TabMaker.Info info = this.m_InfoList[index];
        if (info != null && info.element != null && info.element.key == key)
          return info;
      }
      return (TabMaker.Info) null;
    }

    public TabMaker.Info GetOnIfno()
    {
      for (int index = 0; index < this.m_InfoList.Count; ++index)
      {
        TabMaker.Info info = this.m_InfoList[index];
        if (info != null && info.tgl.isOn)
          return info;
      }
      return (TabMaker.Info) null;
    }

    public void SetOn(string key, bool value)
    {
      TabMaker.Info info = this.GetInfo(key);
      if (info == null)
        return;
      info.isOn = value;
    }

    public void SetOn(Enum key, bool value)
    {
      TabMaker.Info info = this.GetInfo(key.ToString());
      if (info == null)
        return;
      info.isOn = value;
    }

    [Serializable]
    public class Element
    {
      public string key;
      public Sprite icon;
      public string text;
      public int value;
    }

    public class Info
    {
      public GameObject node;
      public SerializeValueList values;
      public TabMaker.Element element;
      public Toggle tgl;
      public ButtonEvent ev;

      public Info(GameObject _node, SerializeValueList _values, TabMaker.Element _element)
      {
        this.node = _node;
        this.values = _values;
        this.element = _element;
        this.tgl = this.node.GetComponent<Toggle>();
        this.ev = this.node.GetComponent<ButtonEvent>();
      }

      public bool interactable
      {
        set
        {
          if (!((UnityEngine.Object) this.tgl != (UnityEngine.Object) null))
            return;
          this.tgl.interactable = value;
        }
        get
        {
          if ((UnityEngine.Object) this.tgl != (UnityEngine.Object) null)
            return this.tgl.interactable;
          return false;
        }
      }

      public bool isOn
      {
        set
        {
          if (!((UnityEngine.Object) this.tgl != (UnityEngine.Object) null))
            return;
          this.tgl.isOn = value;
        }
        get
        {
          if ((UnityEngine.Object) this.tgl != (UnityEngine.Object) null)
            return this.tgl.isOn;
          return false;
        }
      }

      public void SetColor(Color color)
      {
        if (this.values == null)
          return;
        Image uiImage1 = this.values.GetUIImage("off");
        if ((UnityEngine.Object) uiImage1 != (UnityEngine.Object) null)
          uiImage1.color = color;
        Image uiImage2 = this.values.GetUIImage("on");
        if ((UnityEngine.Object) uiImage2 != (UnityEngine.Object) null)
          uiImage2.color = color;
        Image uiImage3 = this.values.GetUIImage("icon");
        if ((UnityEngine.Object) uiImage3 != (UnityEngine.Object) null)
          uiImage3.color = color;
        Text uiLabel = this.values.GetUILabel("text");
        if (!((UnityEngine.Object) uiLabel != (UnityEngine.Object) null))
          return;
        uiLabel.color = color;
      }
    }
  }
}
