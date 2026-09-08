using System.Collections.Generic;
using UnityEngine;

public class WarehouseData : MonoBehaviour, IWarehouseData
{
    private readonly Queue<ForkliftBox> incomingQueue = new Queue<ForkliftBox>();
    private readonly Queue<ForkliftBox> outyoingQueue = new Queue<ForkliftBox>();

    private readonly Dictionary<int, Stack<ForkliftBox>> stacks = new Dictionary<int, Stack<ForkliftBox>>();

    public int IncomingCount => incomingQueue.Count;
    public int OutgoingCount => outyoingQueue.Count;

    public void Clear()
    {
        incomingQueue.Clear();
        outyoingQueue.Clear();
        stacks.Clear();
    }

    public void EnqueueIncoming(ForkliftBox box)
    {
        if (box == null)
        {
            return;
        }

        incomingQueue.Enqueue(box);
    }

    public ForkliftBox PeekIncoming()
    {
        if (incomingQueue.Count == 0)
        {
            return null;
        }

        return incomingQueue.Peek();
    }

    public ForkliftBox DequeueIncoming()
    {
        if (incomingQueue.Count == 0)
        {
            return null;
        }

        return incomingQueue.Dequeue();
    }

    public ForkliftBox[] GetIncomingItems()
    {
        return incomingQueue.ToArray();
    }

    public void EnqueueOutgoing(ForkliftBox box)
    {
        if (box == null)
        {
            return;
        }

        outyoingQueue.Enqueue(box);
    }

    public ForkliftBox PeekOutgoing()
    {
        if (incomingQueue.Count == 0)
        {
            return null;
        }

        return outyoingQueue.Peek();
    }

    public ForkliftBox DequeueOutgoing()
    {
        if (incomingQueue.Count == 0)
        {
            return null;
        }

        return outyoingQueue.Dequeue();
    }

    public ForkliftBox[] GetOutgoingItems()
    {
        return outyoingQueue.ToArray();
    }


    private Stack<ForkliftBox> GetOrCreateStack(int stackIndex)
    {
        if (!stacks.TryGetValue(stackIndex, out Stack<ForkliftBox> stack))
        {
            stack = new Stack<ForkliftBox>();
            stacks.Add(stackIndex, stack);
        }

        return stack;
    }

    public int GetStackCount(int stackIndex)
    {
        return GetOrCreateStack(stackIndex).Count;
    }

    public void PushStack(int stackIndex, ForkliftBox box)
    {
        if (box == null)
        {
            return;
        }

        GetOrCreateStack(stackIndex).Push(box);
    }

    public ForkliftBox PeekStack(int stackIndex)
    {
        Stack<ForkliftBox> stack = GetOrCreateStack(stackIndex);

        if (stack.Count == 0)
        {
            return null;
        }
        return stack.Peek();
    }

    public ForkliftBox PopStack(int stackIndex)
    {
        Stack<ForkliftBox> stack = GetOrCreateStack(stackIndex);

        if (stack.Count == 0)
        {
            return null;
        }

        return stack.Pop();
    }
}
