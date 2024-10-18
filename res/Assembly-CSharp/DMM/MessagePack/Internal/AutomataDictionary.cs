// Decompiled with JetBrains decompiler
// Type: MessagePack.Internal.AutomataDictionary
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

#nullable disable
namespace MessagePack.Internal
{
  public class AutomataDictionary : IEnumerable<KeyValuePair<string, int>>, IEnumerable
  {
    private readonly AutomataDictionary.AutomataNode root;

    public AutomataDictionary() => this.root = new AutomataDictionary.AutomataNode(0UL);

    public void Add(string str, int value)
    {
      byte[] bytes = Encoding.UTF8.GetBytes(str);
      int offset = 0;
      AutomataDictionary.AutomataNode automataNode = this.root;
      int length = bytes.Length;
      while (length != 0)
      {
        ulong keySafe = AutomataKeyGen.GetKeySafe(bytes, ref offset, ref length);
        automataNode = length != 0 ? automataNode.Add(keySafe) : automataNode.Add(keySafe, value, str);
      }
    }

    public bool TryGetValueSafe(ArraySegment<byte> key, out int value)
    {
      AutomataDictionary.AutomataNode automataNode = this.root;
      byte[] array = key.Array;
      int offset = key.Offset;
      int count = key.Count;
      while (count != 0 && automataNode != null)
        automataNode = automataNode.SearchNextSafe(array, ref offset, ref count);
      if (automataNode == null)
      {
        value = -1;
        return false;
      }
      value = automataNode.Value;
      return true;
    }

    public override string ToString()
    {
      StringBuilder sb = new StringBuilder();
      AutomataDictionary.ToStringCore(this.root.YieldChildren(), sb, 0);
      return sb.ToString();
    }

    private static void ToStringCore(
      IEnumerable<AutomataDictionary.AutomataNode> nexts,
      StringBuilder sb,
      int depth)
    {
      foreach (AutomataDictionary.AutomataNode next in nexts)
      {
        if (depth != 0)
          sb.Append(' ', depth * 2);
        sb.Append("[" + (object) next.Key + "]");
        if (next.Value != -1)
        {
          sb.Append("(" + next.originalKey + ")");
          sb.Append(" = ");
          sb.Append(next.Value);
        }
        sb.AppendLine();
        AutomataDictionary.ToStringCore(next.YieldChildren(), sb, depth + 1);
      }
    }

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

    public IEnumerator<KeyValuePair<string, int>> GetEnumerator()
    {
      return AutomataDictionary.YieldCore(this.root.YieldChildren()).GetEnumerator();
    }

    [DebuggerHidden]
    private static IEnumerable<KeyValuePair<string, int>> YieldCore(
      IEnumerable<AutomataDictionary.AutomataNode> nexts)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      AutomataDictionary.\u003CYieldCore\u003Ec__Iterator0 yieldCoreCIterator0 = new AutomataDictionary.\u003CYieldCore\u003Ec__Iterator0()
      {
        nexts = nexts
      };
      // ISSUE: reference to a compiler-generated field
      yieldCoreCIterator0.\u0024PC = -2;
      return (IEnumerable<KeyValuePair<string, int>>) yieldCoreCIterator0;
    }

    public void EmitMatch(
      ILGenerator il,
      LocalBuilder p,
      LocalBuilder rest,
      LocalBuilder key,
      Action<KeyValuePair<string, int>> onFound,
      Action onNotFound)
    {
      this.root.EmitSearchNext(il, p, rest, key, onFound, onNotFound);
    }

    private class AutomataNode : IComparable<AutomataDictionary.AutomataNode>
    {
      private static readonly AutomataDictionary.AutomataNode[] emptyNodes = new AutomataDictionary.AutomataNode[0];
      private static readonly ulong[] emptyKeys = new ulong[0];
      public ulong Key;
      public int Value;
      public string originalKey;
      private AutomataDictionary.AutomataNode[] nexts;
      private ulong[] nextKeys;
      private int count;

      public AutomataNode(ulong key)
      {
        this.Key = key;
        this.Value = -1;
        this.nexts = AutomataDictionary.AutomataNode.emptyNodes;
        this.nextKeys = AutomataDictionary.AutomataNode.emptyKeys;
        this.count = 0;
        this.originalKey = (string) null;
      }

      public bool HasChildren => this.count != 0;

      public AutomataDictionary.AutomataNode Add(ulong key)
      {
        int index = Array.BinarySearch<ulong>(this.nextKeys, 0, this.count, key);
        if (index >= 0)
          return this.nexts[index];
        if (this.nexts.Length == this.count)
        {
          Array.Resize<AutomataDictionary.AutomataNode>(ref this.nexts, this.count != 0 ? this.count * 2 : 4);
          Array.Resize<ulong>(ref this.nextKeys, this.count != 0 ? this.count * 2 : 4);
        }
        ++this.count;
        AutomataDictionary.AutomataNode automataNode = new AutomataDictionary.AutomataNode(key);
        this.nexts[this.count - 1] = automataNode;
        this.nextKeys[this.count - 1] = key;
        Array.Sort<AutomataDictionary.AutomataNode>(this.nexts, 0, this.count);
        Array.Sort<ulong>(this.nextKeys, 0, this.count);
        return automataNode;
      }

      public AutomataDictionary.AutomataNode Add(ulong key, int value, string originalKey)
      {
        AutomataDictionary.AutomataNode automataNode = this.Add(key);
        automataNode.Value = value;
        automataNode.originalKey = originalKey;
        return automataNode;
      }

      public AutomataDictionary.AutomataNode SearchNextSafe(byte[] p, ref int offset, ref int rest)
      {
        ulong keySafe = AutomataKeyGen.GetKeySafe(p, ref offset, ref rest);
        if (this.count < 4)
        {
          for (int index = 0; index < this.count; ++index)
          {
            if ((long) this.nextKeys[index] == (long) keySafe)
              return this.nexts[index];
          }
        }
        else
        {
          int index = AutomataDictionary.AutomataNode.BinarySearch(this.nextKeys, 0, this.count, keySafe);
          if (index >= 0)
            return this.nexts[index];
        }
        return (AutomataDictionary.AutomataNode) null;
      }

      internal static int BinarySearch(ulong[] array, int index, int length, ulong value)
      {
        int num1 = index;
        int num2 = index + length - 1;
        while (num1 <= num2)
        {
          int index1 = num1 + (num2 - num1 >> 1);
          ulong num3 = array[index1];
          int num4 = num3 >= value ? (num3 <= value ? 0 : 1) : -1;
          if (num4 == 0)
            return index1;
          if (num4 < 0)
            num1 = index1 + 1;
          else
            num2 = index1 - 1;
        }
        return ~num1;
      }

      public int CompareTo(AutomataDictionary.AutomataNode other) => this.Key.CompareTo(other.Key);

      [DebuggerHidden]
      public IEnumerable<AutomataDictionary.AutomataNode> YieldChildren()
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        AutomataDictionary.AutomataNode.\u003CYieldChildren\u003Ec__Iterator0 childrenCIterator0 = new AutomataDictionary.AutomataNode.\u003CYieldChildren\u003Ec__Iterator0()
        {
          \u0024this = this
        };
        // ISSUE: reference to a compiler-generated field
        childrenCIterator0.\u0024PC = -2;
        return (IEnumerable<AutomataDictionary.AutomataNode>) childrenCIterator0;
      }

      public void EmitSearchNext(
        ILGenerator il,
        LocalBuilder p,
        LocalBuilder rest,
        LocalBuilder key,
        Action<KeyValuePair<string, int>> onFound,
        Action onNotFound)
      {
        il.EmitLdloca(p);
        il.EmitLdloca(rest);
        il.EmitCall(AutomataKeyGen.GetGetKeyMethod());
        il.EmitStloc(key);
        AutomataDictionary.AutomataNode.EmitSearchNextCore(il, p, rest, key, onFound, onNotFound, this.nexts, this.count);
      }

      private static void EmitSearchNextCore(
        ILGenerator il,
        LocalBuilder p,
        LocalBuilder rest,
        LocalBuilder key,
        Action<KeyValuePair<string, int>> onFound,
        Action onNotFound,
        AutomataDictionary.AutomataNode[] nexts,
        int count)
      {
        if (count < 4)
        {
          AutomataDictionary.AutomataNode[] array1 = ((IEnumerable<AutomataDictionary.AutomataNode>) nexts).Take<AutomataDictionary.AutomataNode>(count).Where<AutomataDictionary.AutomataNode>((Func<AutomataDictionary.AutomataNode, bool>) (x => x.Value != -1)).ToArray<AutomataDictionary.AutomataNode>();
          AutomataDictionary.AutomataNode[] array2 = ((IEnumerable<AutomataDictionary.AutomataNode>) nexts).Take<AutomataDictionary.AutomataNode>(count).Where<AutomataDictionary.AutomataNode>((Func<AutomataDictionary.AutomataNode, bool>) (x => x.HasChildren)).ToArray<AutomataDictionary.AutomataNode>();
          Label label1 = il.DefineLabel();
          Label label2 = il.DefineLabel();
          il.EmitLdloc(rest);
          if (array2.Length != 0 && array1.Length == 0)
            il.Emit(OpCodes.Brfalse, label2);
          else
            il.Emit(OpCodes.Brtrue, label1);
          Label[] array3 = Enumerable.Range(0, Math.Max(array1.Length - 1, 0)).Select<int, Label>((Func<int, Label>) (_ => il.DefineLabel())).ToArray<Label>();
          for (int index = 0; index < array1.Length; ++index)
          {
            Label label3 = il.DefineLabel();
            if (index != 0)
              il.MarkLabel(array3[index - 1]);
            il.EmitLdloc(key);
            il.EmitULong(array1[index].Key);
            il.Emit(OpCodes.Bne_Un, label3);
            onFound(new KeyValuePair<string, int>(array1[index].originalKey, array1[index].Value));
            il.MarkLabel(label3);
            if (index != array1.Length - 1)
              il.Emit(OpCodes.Br, array3[index]);
            else
              onNotFound();
          }
          il.MarkLabel(label1);
          Label[] array4 = Enumerable.Range(0, Math.Max(array2.Length - 1, 0)).Select<int, Label>((Func<int, Label>) (_ => il.DefineLabel())).ToArray<Label>();
          for (int index = 0; index < array2.Length; ++index)
          {
            Label label4 = il.DefineLabel();
            if (index != 0)
              il.MarkLabel(array4[index - 1]);
            il.EmitLdloc(key);
            il.EmitULong(array2[index].Key);
            il.Emit(OpCodes.Bne_Un, label4);
            array2[index].EmitSearchNext(il, p, rest, key, onFound, onNotFound);
            il.MarkLabel(label4);
            if (index != array2.Length - 1)
              il.Emit(OpCodes.Br, array4[index]);
            else
              onNotFound();
          }
          il.MarkLabel(label2);
          onNotFound();
        }
        else
        {
          int count1 = count / 2;
          ulong key1 = nexts[count1].Key;
          AutomataDictionary.AutomataNode[] array5 = ((IEnumerable<AutomataDictionary.AutomataNode>) nexts).Take<AutomataDictionary.AutomataNode>(count).Take<AutomataDictionary.AutomataNode>(count1).ToArray<AutomataDictionary.AutomataNode>();
          AutomataDictionary.AutomataNode[] array6 = ((IEnumerable<AutomataDictionary.AutomataNode>) nexts).Take<AutomataDictionary.AutomataNode>(count).Skip<AutomataDictionary.AutomataNode>(count1).ToArray<AutomataDictionary.AutomataNode>();
          Label label = il.DefineLabel();
          il.EmitLdloc(key);
          il.EmitULong(key1);
          il.Emit(OpCodes.Bge, label);
          AutomataDictionary.AutomataNode.EmitSearchNextCore(il, p, rest, key, onFound, onNotFound, array5, array5.Length);
          il.MarkLabel(label);
          AutomataDictionary.AutomataNode.EmitSearchNextCore(il, p, rest, key, onFound, onNotFound, array6, array6.Length);
        }
      }
    }
  }
}
