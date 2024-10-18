// Decompiled with JetBrains decompiler
// Type: QuestLobbyNewsUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
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
    if (!Object.op_Implicit((Object) this.mText) || !Object.op_Implicit((Object) this.mBadgeRoot))
      return;
    this.mBadgeRoot.SetActive(false);
    QuestLobbyNews questLobbyNews = QuestLobbyNews.FindQuestLobbyNews(this.mCategory);
    if (questLobbyNews == null || !questLobbyNews.isShow())
      return;
    this.mBadgeRoot.SetActive(true);
    this.mText.text = questLobbyNews.GetShowText();
  }
}
