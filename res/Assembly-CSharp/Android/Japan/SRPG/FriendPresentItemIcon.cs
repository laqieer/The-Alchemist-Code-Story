﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FriendPresentItemIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class FriendPresentItemIcon : MonoBehaviour
  {
    [CustomGroup("期間")]
    [CustomField("ルート", CustomFieldAttribute.Type.GameObject)]
    public GameObject m_TimeLimitObject;
    [CustomGroup("期間")]
    [CustomField("時間", CustomFieldAttribute.Type.UIText)]
    public GameObject m_TimeLimitValueObject;
    [CustomGroup("アイテム")]
    [CustomField("名前", CustomFieldAttribute.Type.UIText)]
    public GameObject m_NameObject;
    [CustomGroup("アイテム")]
    [CustomField("フレーム", CustomFieldAttribute.Type.UIImage)]
    public GameObject m_FrameObject;
    [CustomGroup("アイテム")]
    [CustomField("アイテムアイコン", CustomFieldAttribute.Type.UIRawImage)]
    public GameObject m_IconObject;
    [CustomGroup("アイテム")]
    [CustomField("個数", CustomFieldAttribute.Type.UIText)]
    public GameObject m_AmountObject;
    [CustomGroup("アイテム")]
    [CustomField("ゼニーアイコン", CustomFieldAttribute.Type.UIImage)]
    public GameObject m_CoinIconObject;
    [CustomGroup("アイテム")]
    [CustomField("ゼニー", CustomFieldAttribute.Type.UIText)]
    public GameObject m_ZenyObject;
    [CustomGroup("アイテム")]
    [CustomField("設定テキスト", CustomFieldAttribute.Type.GameObject)]
    public GameObject m_SettingTextObject;
    [CustomGroup("所持数")]
    [CustomField("ルート", CustomFieldAttribute.Type.GameObject)]
    public GameObject m_NumObject;
    [CustomGroup("所持数")]
    [CustomField("個数", CustomFieldAttribute.Type.UIText)]
    public GameObject m_NumValueObject;
    private FriendPresentItemParam m_Present;
    private ItemData m_ItemData;

    private void Start()
    {
    }

    public void Clear()
    {
      this.m_Present = (FriendPresentItemParam) null;
      this.m_ItemData = (ItemData) null;
    }

    private void Update()
    {
    }

    public void Refresh()
    {
      if (this.m_Present == null)
      {
        if ((UnityEngine.Object) this.m_TimeLimitObject != (UnityEngine.Object) null)
          this.m_TimeLimitObject.SetActive(false);
        if ((UnityEngine.Object) this.m_NameObject != (UnityEngine.Object) null)
          this.m_NameObject.SetActive(false);
        if ((UnityEngine.Object) this.m_IconObject != (UnityEngine.Object) null)
          this.m_IconObject.SetActive(false);
        if ((UnityEngine.Object) this.m_CoinIconObject != (UnityEngine.Object) null)
          this.m_CoinIconObject.SetActive(false);
        if ((UnityEngine.Object) this.m_NumObject != (UnityEngine.Object) null)
          this.m_NumObject.SetActive(false);
        if ((UnityEngine.Object) this.m_SettingTextObject != (UnityEngine.Object) null)
          this.m_SettingTextObject.SetActive(true);
        if (!((UnityEngine.Object) this.m_FrameObject != (UnityEngine.Object) null))
          return;
        this.SetSprite(this.m_FrameObject, GameSettings.Instance.GetItemFrame(EItemType.Other, 0), Color.gray);
      }
      else
      {
        if ((UnityEngine.Object) this.m_SettingTextObject != (UnityEngine.Object) null)
          this.m_SettingTextObject.SetActive(false);
        if ((UnityEngine.Object) this.m_TimeLimitObject != (UnityEngine.Object) null)
          this.m_TimeLimitObject.SetActive(this.m_Present.HasTimeLimit());
        if ((UnityEngine.Object) this.m_TimeLimitValueObject != (UnityEngine.Object) null)
          this.SetRestTime(this.m_TimeLimitValueObject, this.m_Present.end_at);
        if ((UnityEngine.Object) this.m_NameObject != (UnityEngine.Object) null)
          this.SetText(this.m_NameObject, this.m_Present.name);
        if (this.m_ItemData != null)
        {
          if ((UnityEngine.Object) this.m_FrameObject != (UnityEngine.Object) null)
            this.SetSprite(this.m_FrameObject, GameSettings.Instance.GetItemFrame(this.m_Present.item), Color.white);
          if ((UnityEngine.Object) this.m_IconObject != (UnityEngine.Object) null)
          {
            this.m_IconObject.SetActive(true);
            GameUtility.RequireComponent<IconLoader>(this.m_IconObject).ResourcePath = AssetPath.ItemIcon(this.m_ItemData.Param);
          }
          if ((UnityEngine.Object) this.m_CoinIconObject != (UnityEngine.Object) null)
            this.m_CoinIconObject.SetActive(false);
          if ((UnityEngine.Object) this.m_AmountObject != (UnityEngine.Object) null)
            this.SetText(this.m_AmountObject, this.m_Present.num);
          if ((UnityEngine.Object) this.m_NumObject != (UnityEngine.Object) null)
            this.m_NumObject.SetActive(true);
          if (!((UnityEngine.Object) this.m_NumValueObject != (UnityEngine.Object) null))
            return;
          this.m_NumValueObject.SetActive(true);
          this.SetText(this.m_NumValueObject, this.m_ItemData.Num);
        }
        else
        {
          if ((UnityEngine.Object) this.m_FrameObject != (UnityEngine.Object) null)
            this.SetSprite(this.m_FrameObject, GameSettings.Instance.GetItemFrame(EItemType.Other, 0), Color.white);
          if ((UnityEngine.Object) this.m_IconObject != (UnityEngine.Object) null)
            this.m_IconObject.SetActive(false);
          if ((UnityEngine.Object) this.m_CoinIconObject != (UnityEngine.Object) null)
            this.m_CoinIconObject.SetActive(true);
          if ((UnityEngine.Object) this.m_ZenyObject != (UnityEngine.Object) null)
            this.SetText(this.m_ZenyObject, this.m_Present.zeny);
          if (!((UnityEngine.Object) this.m_NumObject != (UnityEngine.Object) null))
            return;
          this.m_NumObject.SetActive(false);
        }
      }
    }

    public void Bind(FriendPresentItemParam present, bool checkTimeLimit)
    {
      if (present == null)
      {
        present = FriendPresentItemParam.DefaultParam;
        if (present == null)
        {
          Debug.LogError((object) "フレンドプレゼントのデフォルトパラメータが存在しません!");
          return;
        }
      }
      if (checkTimeLimit && !present.IsValid(Network.GetServerTime()))
        present = FriendPresentItemParam.DefaultParam;
      this.m_Present = present;
      if (this.m_Present.item == null)
        return;
      this.m_ItemData = MonoSingleton<GameManager>.Instance.Player.Items.Find((Predicate<ItemData>) (prop => prop.ItemID == present.item.iname));
      if (this.m_ItemData != null)
        return;
      this.m_ItemData = new ItemData();
      this.m_ItemData.Setup(0L, this.m_Present.item, 0);
    }

    public void SetText(GameObject gobj, string text)
    {
      Text component = gobj.GetComponent<Text>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      component.text = text;
    }

    public void SetText(GameObject gobj, int num)
    {
      Text component = gobj.GetComponent<Text>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      component.text = num.ToString();
    }

    public void SetRestTime(GameObject gobj, long endTime)
    {
      Text component = gobj.GetComponent<Text>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      long num = endTime - Network.GetServerTime();
      if (num <= 0L)
        return;
      string str;
      if (num >= 86400L)
        str = LocalizedText.Get("sys.QUEST_TIMELIMIT_D", new object[1]
        {
          (object) (int) (num / 86400L)
        });
      else if (num >= 3600L)
        str = LocalizedText.Get("sys.QUEST_TIMELIMIT_H", new object[1]
        {
          (object) (int) (num / 3600L)
        });
      else
        str = LocalizedText.Get("sys.QUEST_TIMELIMIT_M", new object[1]
        {
          (object) (int) (num / 60L)
        });
      component.text = str;
    }

    public void SetSprite(GameObject gobj, Sprite sprite)
    {
      Image component = gobj.GetComponent<Image>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      component.sprite = sprite;
    }

    public void SetSprite(GameObject gobj, Sprite sprite, Color color)
    {
      Image component = gobj.GetComponent<Image>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      component.sprite = sprite;
      component.color = color;
    }
  }
}
