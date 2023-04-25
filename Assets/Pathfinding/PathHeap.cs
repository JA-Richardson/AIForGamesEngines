using System;
//Heap data structure
public class PathHeap<T> where T : IHeapItem<T>
{
    readonly T[] items;
    int currentItemCount;
     
    public PathHeap(int maxHeapSize)
    {
        items = new T[maxHeapSize];
    }
    //Add an item to the heap
    public void Add(T item)
    {
        item.HeapIndex = currentItemCount;
        items[currentItemCount] = item;
        SortUp(item);
        currentItemCount++;
    }
    //Remove the first item from the heap
    public T RemoveFirst()
    {
        T firstItem = items[0];
        currentItemCount--;
        items[0] = items[currentItemCount];
        items[0].HeapIndex = 0;
        SortDown(items[0]);
        return firstItem;
    }
    //Update the position of an item in the heap
    public void UpdateItem(T item)
    {
        SortUp(item);
    }
    //Get the number of items in the heap
    public int Count
    {
        get
        {
            return currentItemCount;
        }
    }
    //Check if the heap contains an item
    public bool Contains(T item)
    {
        return Equals(items[item.HeapIndex], item);
    }
    //Sort the heap down
    void SortDown(T item)
    {
        while (true)
        {
            int childIndexLeft = item.HeapIndex * 2 + 1;
            int childIndexRight = item.HeapIndex * 2 + 2;
            //Check if the left child is within the heap
            if (childIndexLeft < currentItemCount)
            {
                //Check if the right child is within the heap
                int swapIndex = childIndexLeft;
                if (childIndexRight < currentItemCount)
                {
                    //Check which child is greater
                    if (items[childIndexLeft].CompareTo(items[childIndexRight]) < 0)
                    {
                        swapIndex = childIndexRight;
                    }
                }
                //Check if the item is less than the child
                if (item.CompareTo(items[swapIndex]) < 0)
                {
                    Swap(item, items[swapIndex]);
                }
                else
                {
                    return;
                }

            }
            else
            {
                return;
            }

        }
    }
    //Sort the heap up 
    void SortUp(T item)
    {
        int parentIndex = (item.HeapIndex - 1) / 2;

        while (true)
        {
            T parentItem = items[parentIndex];
            if (item.CompareTo(parentItem) > 0)
            {
                Swap(item, parentItem);
            }
            else
            {
                break;
            }

            parentIndex = (item.HeapIndex - 1) / 2;
        }
    }
    // Swap two items in the heap
    void Swap(T itemA, T itemB)
    {
        items[itemA.HeapIndex] = itemB;
        items[itemB.HeapIndex] = itemA;
        (itemB.HeapIndex, itemA.HeapIndex) = (itemA.HeapIndex, itemB.HeapIndex);
    }
}
// Interface for the heap
public interface IHeapItem<T> : IComparable<T>
{
    int HeapIndex
    {
        get;
        set;
    }
}