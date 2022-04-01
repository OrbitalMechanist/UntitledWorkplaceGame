using System.Collections;
using System.Collections.Generic;

public class PriorityQueue<T>
{
    private struct PQItem<L>
    {
        public L item;
        public float priority;

        public PQItem(L i, float p){
            item = i;
            priority = p;
        }
    }

    private List<PQItem<T>> Contents = new List<PQItem<T>>();

    public void Add(T addition, float priority = 0)
    {
        Contents.Add(new PQItem<T>(addition, priority));
    }

    public T Peek()
    {
        if(Contents.Count == 0)
        {
            return default;
        }
        PQItem<T> top = Contents[0];
        foreach(PQItem<T> i in Contents)
        {
            if(i.priority < top.priority)
            {
                top = i;
            }
        }
        return top.item;
    }

    //CODE DUPLICATION !!!!1111!!!1
    public T Pop()
    {
        if (Contents.Count == 0)
        {
            return default;
        }
        PQItem<T> top = Contents[0];
        foreach (PQItem<T> i in Contents)
        {
            if (i.priority < top.priority)
            {
                top = i;
            }
        }
        Contents.Remove(top);
        return top.item;
    }

    public int Count => Contents.Count;
}
