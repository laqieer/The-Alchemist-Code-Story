// Decompiled with JetBrains decompiler
// Type: DependencyGraph
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
public class DependencyGraph
{
  public List<DependencyGraph.Node> Nodes = new List<DependencyGraph.Node>();

  public static string SerializeHash(int num) => num.ToString("X8");

  public static int DeserializeHash(string value) => Convert.ToInt32(value, 16);

  public void LoadGraphFromFile(string path)
  {
    using (StreamReader reader = new StreamReader(path))
      this.LoadGraphFromStream((TextReader) reader);
  }

  public void LoadGraphFromMemory(string src)
  {
    this.LoadGraphFromStream((TextReader) new StringReader(src));
  }

  private void LoadGraphFromStream(TextReader reader)
  {
    this.Nodes.Clear();
    reader.ReadLine();
    string str;
    while ((str = reader.ReadLine()) != null)
    {
      string[] strArray1 = str.Split('\t');
      if (strArray1.Length >= 4 && !string.IsNullOrEmpty(strArray1[0]))
      {
        DependencyGraph.NodeFlags nodeFlags = (DependencyGraph.NodeFlags) int.Parse(strArray1[6]);
        DependencyGraph.Node node1 = this.GetNode(strArray1[0]);
        node1.Path = strArray1[2];
        node1.Name = strArray1[1];
        node1.NameHash = node1.Name.GetHashCode();
        node1.Size = int.Parse(strArray1[4]);
        node1.Hash = DependencyGraph.DeserializeHash(strArray1[5]);
        node1.Flags = nodeFlags;
        string[] strArray2 = strArray1[3].Split('/');
        for (int index = 0; index < strArray2.Length; ++index)
        {
          if (!string.IsNullOrEmpty(strArray2[index]))
          {
            DependencyGraph.Node node2 = this.GetNode(strArray2[index]);
            node1.Dependencies.Add(node2);
          }
        }
      }
    }
  }

  public DependencyGraph.Node FindNodeByName(string nodeName)
  {
    int hashCode = nodeName.GetHashCode();
    for (int index = 0; index < this.Nodes.Count; ++index)
    {
      if (this.Nodes[index].NameHash == hashCode && this.Nodes[index].Name == nodeName)
        return this.Nodes[index];
    }
    return (DependencyGraph.Node) null;
  }

  public DependencyGraph.Node FindNode(string nodeID)
  {
    for (int index = 0; index < this.Nodes.Count; ++index)
    {
      if (this.Nodes[index].UniqueID == nodeID)
        return this.Nodes[index];
    }
    return (DependencyGraph.Node) null;
  }

  public DependencyGraph.Node GetNode(string nodeID)
  {
    DependencyGraph.Node node = this.FindNode(nodeID);
    if (node == null)
    {
      node = new DependencyGraph.Node();
      node.UniqueID = nodeID;
      this.Nodes.Add(node);
    }
    return node;
  }

  [Flags]
  public enum NodeFlags
  {
    Required = 1,
    Shared = 2,
    SceneAsset = 4,
    SubAsset = 8,
    Compressed = 16, // 0x00000010
  }

  public class Node
  {
    public string UniqueID;
    public string Path;
    public string Name;
    public int NameHash;
    public int Hash;
    public int Size;
    public bool IsRequired;
    public bool IsShared;
    public bool IsScene;
    public bool IsSubAsset;
    public bool IsCompressed;
    public List<DependencyGraph.Node> Dependencies = new List<DependencyGraph.Node>();

    public DependencyGraph.NodeFlags Flags
    {
      get
      {
        DependencyGraph.NodeFlags flags = (DependencyGraph.NodeFlags) 0;
        if (this.IsRequired)
          flags |= DependencyGraph.NodeFlags.Required;
        if (this.IsShared)
          flags |= DependencyGraph.NodeFlags.Shared;
        if (this.IsScene)
          flags |= DependencyGraph.NodeFlags.SceneAsset;
        if (this.IsSubAsset)
          flags |= DependencyGraph.NodeFlags.SubAsset;
        if (this.IsCompressed)
          flags |= DependencyGraph.NodeFlags.Compressed;
        return flags;
      }
      set
      {
        this.IsRequired = (value & DependencyGraph.NodeFlags.Required) != (DependencyGraph.NodeFlags) 0;
        this.IsShared = (value & DependencyGraph.NodeFlags.Shared) != (DependencyGraph.NodeFlags) 0;
        this.IsScene = (value & DependencyGraph.NodeFlags.SceneAsset) != (DependencyGraph.NodeFlags) 0;
        this.IsSubAsset = (value & DependencyGraph.NodeFlags.SubAsset) != (DependencyGraph.NodeFlags) 0;
        this.IsCompressed = (value & DependencyGraph.NodeFlags.Compressed) != (DependencyGraph.NodeFlags) 0;
      }
    }
  }
}
