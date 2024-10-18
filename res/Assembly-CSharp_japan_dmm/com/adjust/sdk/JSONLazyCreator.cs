// Decompiled with JetBrains decompiler
// Type: com.adjust.sdk.JSONLazyCreator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace com.adjust.sdk
{
  internal class JSONLazyCreator : JSONNode
  {
    private JSONNode m_Node;
    private string m_Key;

    public JSONLazyCreator(JSONNode aNode)
    {
      this.m_Node = aNode;
      this.m_Key = (string) null;
    }

    public JSONLazyCreator(JSONNode aNode, string aKey)
    {
      this.m_Node = aNode;
      this.m_Key = aKey;
    }

    private void Set(JSONNode aVal)
    {
      if (this.m_Key == null)
        this.m_Node.Add(aVal);
      else
        this.m_Node.Add(this.m_Key, aVal);
      this.m_Node = (JSONNode) null;
    }

    public override JSONNode this[int aIndex]
    {
      get => (JSONNode) new JSONLazyCreator((JSONNode) this);
      set
      {
        JSONArray aVal = new JSONArray();
        aVal.Add(value);
        this.Set((JSONNode) aVal);
      }
    }

    public override JSONNode this[string aKey]
    {
      get => (JSONNode) new JSONLazyCreator((JSONNode) this, aKey);
      set
      {
        JSONClass aVal = new JSONClass();
        aVal.Add(aKey, value);
        this.Set((JSONNode) aVal);
      }
    }

    public override void Add(JSONNode aItem)
    {
      JSONArray aVal = new JSONArray();
      aVal.Add(aItem);
      this.Set((JSONNode) aVal);
    }

    public override void Add(string aKey, JSONNode aItem)
    {
      JSONClass aVal = new JSONClass();
      aVal.Add(aKey, aItem);
      this.Set((JSONNode) aVal);
    }

    public static bool operator ==(JSONLazyCreator a, object b)
    {
      return b == null || object.ReferenceEquals((object) a, b);
    }

    public static bool operator !=(JSONLazyCreator a, object b) => !(a == b);

    public override bool Equals(object obj)
    {
      return obj == null || object.ReferenceEquals((object) this, obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override string ToString() => string.Empty;

    public override string ToString(string aPrefix) => string.Empty;

    public override int AsInt
    {
      get
      {
        this.Set((JSONNode) new JSONData(0));
        return 0;
      }
      set => this.Set((JSONNode) new JSONData(value));
    }

    public override float AsFloat
    {
      get
      {
        this.Set((JSONNode) new JSONData(0.0f));
        return 0.0f;
      }
      set => this.Set((JSONNode) new JSONData(value));
    }

    public override double AsDouble
    {
      get
      {
        this.Set((JSONNode) new JSONData(0.0));
        return 0.0;
      }
      set => this.Set((JSONNode) new JSONData(value));
    }

    public override bool AsBool
    {
      get
      {
        this.Set((JSONNode) new JSONData(false));
        return false;
      }
      set => this.Set((JSONNode) new JSONData(value));
    }

    public override JSONArray AsArray
    {
      get
      {
        JSONArray aVal = new JSONArray();
        this.Set((JSONNode) aVal);
        return aVal;
      }
    }

    public override JSONClass AsObject
    {
      get
      {
        JSONClass aVal = new JSONClass();
        this.Set((JSONNode) aVal);
        return aVal;
      }
    }
  }
}
