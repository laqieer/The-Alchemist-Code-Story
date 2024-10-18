// Decompiled with JetBrains decompiler
// Type: InRoomChat
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Photon;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
[RequireComponent(typeof (PhotonView))]
public class InRoomChat : MonoBehaviour
{
  public Rect GuiRect = new Rect(0.0f, 0.0f, 250f, 300f);
  public bool IsVisible = true;
  public bool AlignBottom;
  public List<string> messages = new List<string>();
  private string inputLine = string.Empty;
  private Vector2 scrollPos = Vector2.zero;
  public static readonly string ChatRPC = "Chat";

  public void Start()
  {
    if (!this.AlignBottom)
      return;
    ((Rect) ref this.GuiRect).y = (float) Screen.height - ((Rect) ref this.GuiRect).height;
  }

  public void OnGUI()
  {
    if (!this.IsVisible || !PhotonNetwork.inRoom)
      return;
    if (Event.current.type == 4 && (Event.current.keyCode == 271 || Event.current.keyCode == 13))
    {
      if (!string.IsNullOrEmpty(this.inputLine))
      {
        this.photonView.RPC("Chat", PhotonTargets.All, (object) this.inputLine);
        this.inputLine = string.Empty;
        GUI.FocusControl(string.Empty);
        return;
      }
      GUI.FocusControl("ChatInput");
    }
    GUI.SetNextControlName(string.Empty);
    GUILayout.BeginArea(this.GuiRect);
    this.scrollPos = GUILayout.BeginScrollView(this.scrollPos, new GUILayoutOption[0]);
    GUILayout.FlexibleSpace();
    for (int index = this.messages.Count - 1; index >= 0; --index)
      GUILayout.Label(this.messages[index], new GUILayoutOption[0]);
    GUILayout.EndScrollView();
    GUILayout.BeginHorizontal(new GUILayoutOption[0]);
    GUI.SetNextControlName("ChatInput");
    this.inputLine = GUILayout.TextField(this.inputLine, new GUILayoutOption[0]);
    if (GUILayout.Button("Send", new GUILayoutOption[1]
    {
      GUILayout.ExpandWidth(false)
    }))
    {
      this.photonView.RPC("Chat", PhotonTargets.All, (object) this.inputLine);
      this.inputLine = string.Empty;
      GUI.FocusControl(string.Empty);
    }
    GUILayout.EndHorizontal();
    GUILayout.EndArea();
  }

  [PunRPC]
  public void Chat(string newLine, PhotonMessageInfo mi)
  {
    string str = "anonymous";
    if (mi.sender != null)
      str = string.IsNullOrEmpty(mi.sender.NickName) ? "player " + (object) mi.sender.ID : mi.sender.NickName;
    this.messages.Add(str + ": " + newLine);
  }

  public void AddLine(string newLine) => this.messages.Add(newLine);
}
