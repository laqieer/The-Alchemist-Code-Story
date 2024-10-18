// Decompiled with JetBrains decompiler
// Type: InRoomChat
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (PhotonView))]
public class InRoomChat : Photon.MonoBehaviour
{
  public static readonly string ChatRPC = "Chat";
  public Rect GuiRect = new Rect(0.0f, 0.0f, 250f, 300f);
  public bool IsVisible = true;
  public List<string> messages = new List<string>();
  private string inputLine = string.Empty;
  private Vector2 scrollPos = Vector2.zero;
  public bool AlignBottom;

  public void Start()
  {
    if (!this.AlignBottom)
      return;
    this.GuiRect.y = (float) Screen.height - this.GuiRect.height;
  }

  public void OnGUI()
  {
    if (!this.IsVisible || !PhotonNetwork.inRoom)
      return;
    if (UnityEngine.Event.current.type == EventType.KeyDown && (UnityEngine.Event.current.keyCode == KeyCode.KeypadEnter || UnityEngine.Event.current.keyCode == KeyCode.Return))
    {
      if (!string.IsNullOrEmpty(this.inputLine))
      {
        this.photonView.RPC("Chat", PhotonTargets.All, new object[1]
        {
          (object) this.inputLine
        });
        this.inputLine = string.Empty;
        GUI.FocusControl(string.Empty);
        return;
      }
      GUI.FocusControl("ChatInput");
    }
    GUI.SetNextControlName(string.Empty);
    GUILayout.BeginArea(this.GuiRect);
    this.scrollPos = GUILayout.BeginScrollView(this.scrollPos);
    GUILayout.FlexibleSpace();
    for (int index = this.messages.Count - 1; index >= 0; --index)
      GUILayout.Label(this.messages[index]);
    GUILayout.EndScrollView();
    GUILayout.BeginHorizontal();
    GUI.SetNextControlName("ChatInput");
    this.inputLine = GUILayout.TextField(this.inputLine);
    if (GUILayout.Button("Send", new GUILayoutOption[1]
    {
      GUILayout.ExpandWidth(false)
    }))
    {
      this.photonView.RPC("Chat", PhotonTargets.All, new object[1]
      {
        (object) this.inputLine
      });
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

  public void AddLine(string newLine)
  {
    this.messages.Add(newLine);
  }
}
