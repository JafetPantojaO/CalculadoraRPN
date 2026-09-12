using System; //Se usa para ocupar de otros codigos
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace StackDev
{
    internal class ArrayStack<T> : IStack<T>
    {
        private const int INITIAL_CAPACITY = 10;
        private T[] data;
        private int index;

        public int Capacity { get ; private set; }
        public int Size => index + 1;
        public bool Empty => index == -1;
        public bool Full => index == data.Length -1;


        public ArrayStack()
        {
            Capacity = INITIAL_CAPACITY;
            data = new T[Capacity];
            index = -1;

        }
        public ArrayStack(int capacity)
        {
            Capacity = capacity < INITIAL_CAPACITY ? INITIAL_CAPACITY : capacity;
            data = new T[Capacity];
            index = -1;

        }

        public T Peek()
        {
            if(Empty)
            {
                throw new IndexOutOfRangeException("Stack is empty");
            }
            return data[index];
        }
        public T Pop()
        {
            if (Empty)
            {
                throw new IndexOutOfRangeException("Stack is empty");
            }

            if (Capacity > INITIAL_CAPACITY && index == Capacity / 5)
            {

                T[] newData = new T[Capacity / 2];
                Array.Copy(data, newData, Capacity / 2);
                
                Capacity /= 2;
                data = newData;

            }


            return data[index--];
        }

        public void Push(T e)
        {
            if (Full)
            {
                T[] newData = new T[Capacity*2];
                Array.Copy(data, newData, Capacity);
                
                Capacity *= 2;
                data = newData;


            }

            data[++index] = e; //Incrementa el índice y agrega el elemento en la posición correspondiente]
        }

    }
}