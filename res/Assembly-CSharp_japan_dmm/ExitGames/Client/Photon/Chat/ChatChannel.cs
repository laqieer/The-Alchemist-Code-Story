// Decompiled with JetBrains decompiler
// Type: ExitGames.Client.Photon.Chat.ChatChannel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using System.Text;

#nullable disable
namespace ExitGames.Client.Photon.Chat
{
  public class ChatChannel
  {
    public readonly string Name;
    public readonly List<string> Senders = new List<string>();
    public readonly List<object> Messages = new List<object>();
    public int MessageLimit;
    private Dictionary<object, object> properties;
    public readonly HashSet<string> Subscribers = new HashSet<string>();

    public ChatChannel(string name) => this.Name = name;

    public bool IsPrivate { get; protected internal set; }

    public int MessageCount => this.Messages.Count;

    public int LastMsgId { get; protected set; }

    public bool PublishSubscribers { get; protected set; }

    public int MaxSubscribers { get; protected set; }

    public void Add(string sender, object message, int msgId)
    {
      this.Senders.Add(sender);
      this.Messages.Add(message);
      this.LastMsgId = msgId;
      this.TruncateMessages();
    }

    public void Add(string[] senders, object[] messages, int lastMsgId)
    {
      this.Senders.AddRange((IEnumerable<string>) senders);
      this.Messages.AddRange((IEnumerable<object>) messages);
      this.LastMsgId = lastMsgId;
      this.TruncateMessages();
    }

    public void TruncateMessages()
    {
      if (this.MessageLimit <= 0 || this.Messages.Count <= this.MessageLimit)
        return;
      int count = this.Messages.Count - this.MessageLimit;
      this.Senders.RemoveRange(0, count);
      this.Messages.RemoveRange(0, count);
    }

    public void ClearMessages()
    {
      this.Senders.Clear();
      this.Messages.Clear();
    }

    public string ToStringMessages()
    {
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < this.Messages.Count; ++index)
        stringBuilder.AppendLine(string.Format("{0}: {1}", (object) this.Senders[index], this.Messages[index]));
      return stringBuilder.ToString();
    }

    internal void ReadProperties(Dictionary<object, object> newProperties)
    {
      if (newProperties == null || newProperties.Count <= 0)
        return;
      if (this.properties == null)
        this.properties = new Dictionary<object, object>(newProperties.Count);
      foreach (object key in newProperties.Keys)
      {
        if (newProperties[key] == null)
        {
          if (this.properties.ContainsKey(key))
            this.properties.Remove(key);
        }
        else
          this.properties[key] = newProperties[key];
      }
      object obj;
      if (this.properties.TryGetValue((object) (byte) 254, out obj))
        this.PublishSubscribers = (bool) obj;
      if (!this.properties.TryGetValue((object) byte.MaxValue, out obj))
        return;
      this.MaxSubscribers = (int) obj;
    }

    internal void AddSubscribers(string[] users)
    {
      if (users == null)
        return;
      for (int index = 0; index < users.Length; ++index)
        this.Subscribers.Add(users[index]);
    }

    internal void ClearProperties()
    {
      if (this.properties == null || this.properties.Count <= 0)
        return;
      this.properties.Clear();
    }
  }
}
