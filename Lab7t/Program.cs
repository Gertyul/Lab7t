using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Nodes;

public class Node<T>
{
    private T data;
    public Node(T data) => this.data = data;

    public T Data { get => data; set => data = value; }
    public Node<T> Next { get; set; }
    public Node<T> Previous { get; set; }
}

public class MyList<T>
{
    public int Count { get; private set; }

    private Node<T> head;

    public MyList(T data)
    {
        head = new Node<T>(data);
        Count++;
    }

    public Node<T> GetElement(int index)
    {
        var currentElement = head;

        for (var i = 0; i < index; i++)
        {
            if (currentElement == null)
                return null;
            currentElement = currentElement.Next;
        }
        return currentElement;
    }

    public void AddElement(T data)
    {
        Node<T> child = new Node<T>(data);
        var tail = FindTail(head);

        tail.Next = child;
        child.Previous = tail;
        Count++;
    }

    public void RemoveAtIndex(int index)
    {
        var current = GetElement(index);

        if (current.Previous == null)
        {
            head = current.Next;
            head.Previous = null;
            Count--;
            return;
        }

        if (current.Next == null)
        {
            GetElement(index - 1).Next = null;
            Count--;
            return;
        }

        GetElement(index + 1).Previous = current.Previous;
        GetElement(index - 1).Next = current.Next;

        Count--;
    }

    private Node<T> FindTail(Node<T> startPoint)
    {
        if (startPoint.Next != null)
        {
            return FindTail(startPoint.Next);

        }
        return startPoint;
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        for (var i = 0; i < Count; i++)
        {
            sb.Append(GetElement(i).Data + ", ");
        }

        return sb.ToString();
    }

    public string this[int index]
    {
        get
        {
            return GetElement(index).Data.ToString();
        }
    }

}

public static class MyListOperations
{
    public static Node<double> LowerThanAverage(MyList<double> myList)
    {
        if (myList.Count == 0)
        {
            return null;
        }

        double sum = 0;

        Node<double> current = myList.GetElement(0);

        while (current != null)
        {
            sum = sum + current.Data;
            current = current.Next;
        }

        double average = sum / myList.Count;

        current = myList.GetElement(0);

        while (current != null)
        {
            if (current.Data < average)
                return current;
            current = current.Next;
        }

        return null;
    }

    public static double SumAfterLast(MyList<double> myList)
    {
        if (myList.Count == 0)
        {
            return 0;
        }

        double maxValue = myList.GetElement(0).Data;
        double sum = 0;
        int index = 0;
        double current = 0;

        for (int i = 0; i < myList.Count; i++)
        {
            current = myList.GetElement(i).Data;

            if (current > maxValue)
            {
                maxValue = current;
                index = i;
            }
        }

        for (int i = index + 1; i < myList.Count; i++)
        {
            sum += myList.GetElement(i).Data;
        }

        return sum;
    }

    public static MyList<double> NewBiggerList(MyList<double> myList, double max)
    {
        if (myList.Count == 0)
        {
            return null;
        }

        MyList<double> newList = null;

        for (int i = 0; i < myList.Count; i++)
        {
            var currentElement = myList.GetElement(i).Data;
            if (currentElement > max)
            {
                if (newList == null)
                {
                    newList = new MyList<double>(currentElement);
                }
                else
                {
                    newList.AddElement(currentElement);
                }
            }
        }

        return newList;
    }

    public static void RemoveBeforeMax(MyList<double> myList)
    {
        double max = myList.GetElement(0).Data;
        int indexOfMax = 0;

        for (int i = 1; i < myList.Count; i++)
        {
            var current = myList.GetElement(i).Data;

            if (current > max)
            {
                max = current;
                indexOfMax = i;
            }
        }

        for (int i = 0; i < indexOfMax; i++)
        {
            myList.RemoveAtIndex(0);
        }


    }


}

internal class Program
{

    private static void Main(string[] args)
    {

        MyList<double> list = new MyList<double>(1);

        list.AddElement(2);
        list.AddElement(3);
        list.AddElement(4);
        list.AddElement(100);
        list.AddElement(10);
        list.AddElement(12);
        list.AddElement(8);
        list.AddElement(9);
        list.AddElement(20);



        //Console.WriteLine(MyListOperations.LowerThanAverage(list).Data);
        //Console.WriteLine(MyListOperations.SumAfterLast(list));
        //MyList<double> list2 = MyListOperations.NewBiggerList(list, 14);
        //for (int i = 0; i < list2.Count; i++)
        //{
        //    Console.WriteLine(list2.GetElement(i).Data);
        //}


        MyListOperations.RemoveBeforeMax(list);
        for (int i = 0; i < list.Count; i++)
        {
            Console.WriteLine(list[i]);
        }


    }
}