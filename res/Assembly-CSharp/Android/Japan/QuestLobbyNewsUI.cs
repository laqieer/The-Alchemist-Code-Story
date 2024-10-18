// Decompiled with JetBrains decompiler
// Type: QuestLobbyNewsUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class QuestLobbyNewsUI : MonoBehaviour
{
  [SerializeField]
  private QuestLobbyNews.QuestLobbyCategory mCategory;
  [SerializeField]
  private GameObject mBadgeRoot;
  [SerializeField]
  private Text mText;

  private void Start()
  {
    if (!(bool) ((Object) this.mText) || !(bool) ((Object) this.mBadgeRoot))
      return;
    this.mBadgeRoot.SetActive(false);
    QuestLobbyNews questLobbyNews = QuestLobbyNews.FindQuestLobbyNews(this.mCategory);
    if (questLobbyNews == null || !questLobbyNews.isShow())
      return;
    this.mBadgeRoot.SetActive(true);
    this.mText.text = questLobbyNews.GetShowText();
  }
}
