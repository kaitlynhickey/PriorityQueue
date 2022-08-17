namespace PriorityQueue
{
    public class Program
    {
        public static void Main()
        {
            PQ<string> pq = new PQ<string>();
            Console.WriteLine($"Is the queue empty? {pq.isEmpty()}");

            Console.WriteLine("Add item to the queue: rand_item with default priority");
            pq.enqueue("rand_item");

            Console.WriteLine("Add item to the queue: third with priority of 3");
            pq.enqueue("third", 3);

            Console.WriteLine("Add item to the queue: second with priority of 2");
            pq.enqueue("second", 2);

            Console.WriteLine("Add item to the queue: first with priority of 2");
            pq.enqueue("first", 1);

            Console.WriteLine("Add item to the queue: fourth with priority of 4");
            pq.enqueue("fourth", 4);

            Console.WriteLine("Add item to the queue: rand_item with default priority");
            pq.enqueue("rand_item");

            Console.WriteLine("Add item to the queue: low_priority with priority of 6");
            pq.enqueue("low_priority", 6);

            Console.WriteLine("Add item to the queue: rand_item with default priority");
            pq.enqueue("rand_item");

            Console.WriteLine($"Is the queue empty? {pq.isEmpty()}");
            Console.WriteLine();
            pq.printQueue();
            Console.WriteLine();
            Console.WriteLine($"First item in the queue: {pq.peekFront()}");
            Console.WriteLine($"Highest priority item in the queue: {pq.peek()}");
            Console.WriteLine($"Last item in the queue: {pq.peekRear()}");
            Console.WriteLine();

            Console.WriteLine($"Deque and remove highest priority item from queue. {pq.dequeue()} was removed.");
            Console.WriteLine();
            pq.printQueue();
            Console.WriteLine();
            pq.printQueueInPriority();
            Console.WriteLine();
            pq.printQueue();
        }
    }

    // A generic implementation of a priority queue with T being the type of data entered in the Queue
    public class PQ<T>
    {
        // This class creates generic items inheriting property T from PQ with a numeric priority value assigned.
        private class item
        {
            public T data;
            public int priority;
            public item(T data, int priority)
            {
                this.data = data;
                this.priority = priority;
            }
        }

        // Creates a new generic list in which the priority queue will be implemented
        // The runtime of this call is O(1)
        List<item> pQueue = new List<item>();

        // Returns the number of items in the queue
        // The runtime of this call is O(1)
        private int Count { get; set; }

        // Returns the index of the highest priority item in the queue, whether that be the first item in the queue (default) or an item entered into the queue with a higher priority
        // The runtime of this call is O(n)
        private int priorityItem
        {
            get 
            {
                int minIndex = 0;

                for (int i = 1; i < pQueue.Count; i++)
                {
                    if (pQueue[i].priority < pQueue[minIndex].priority)
                    {
                        minIndex = i;
                    }
                }
                return minIndex;
            }
        }

        public PQ()
        {
        }

        // This function is used to insert new data into the queue.
        // The numeric priority value can be provided upon enqueue request, but if it is not, the item is assigned a default priority value of 5.
        // The runtime of this method is O(1) as the item is just added to the back of the queue
        public void enqueue(T data, int priority = 5)
        {
            var item = new item(data, priority);
            pQueue.Add(item);
            Count++;
        }

        // This function removes the element with the highest priority from the queue and returns the data of the removed item
        // The runtime of this method is O(n)
        public T dequeue()
        {
            int pIndex = priorityItem;
            item priority = pQueue[pIndex];

            pQueue.RemoveAt(pIndex);
            Count--;

            return priority.data;
        }

        // This function is used to view the highest priority element in the queue without removing it from the queue.
        // The runtime of this method is O(n)
        public T peek()
        {
            return pQueue[priorityItem].data;
        }

        // This operation returns the element at the front end without removing it.
        // The runtime of this method is O(1)
        public T peekFront()
        {
            return pQueue[0].data;
        }

        // This operation returns the element at the rear end without removing it.
        // The runtime of this method is O(1)
        public T peekRear()
        {
            return pQueue[Count - 1].data;
        }

        // This operation prints the queue in first-in-first-out order.
        // The runtime of this method is O(n)
        public bool isEmpty()
        {
            return Count == 0;
        }

        // This operation prints the queue in first-in-first-out order.
        // The runtime of this method is O(n)
        public void printQueue()
        {
            Console.WriteLine("In this queue, the smallest value is the highest priority. \nI.E. 1 is of higher priority than 5, with 5 being the default priority value.\nQueue in FIFO Order:");
            foreach (item item in pQueue) 
            {
                Console.WriteLine($"{item.data} (priority {item.priority})");
            }
            Console.WriteLine("End of Queue");
        }

        // This operation prints the queue in priority order.
        // The runtime of this method is O(nlogn)
        public void printQueueInPriority()
        {
            item[] items = new item[Count];
            pQueue.CopyTo(items, 0);

            quickSort(items, 0, items.Length - 1);

            Console.WriteLine("In this queue, the smallest value is the highest priority. \nI.E. 1 is of higher priority than 5, with 5 being the default priority value.\nQueue in Priority Order:");
            foreach (item item in items)
            {
                Console.WriteLine($"{item.data} (priority {item.priority})");
            }
            Console.WriteLine("End of Queue");
        }

        // C# Quick Sort Code from Geeks for Geeks
        // A utility function to swap two elements
        static void swap(item[] arr, int i, int j)
        {
            item temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }

        /* This function takes last element as pivot, places
             the pivot element at its correct position in sorted
             array, and places all smaller (smaller than pivot)
             to left of pivot and all greater elements to right
             of pivot */
        static int partition(item[] arr, int low, int high)
        {

            // pivot
            int pivot = arr[high].priority;

            // Index of smaller element and
            // indicates the right position
            // of pivot found so far
            int i = (low - 1);

            for (int j = low; j <= high - 1; j++)
            {

                // If current element is smaller 
                // than the pivot
                if (arr[j].priority < pivot)
                {

                    // Increment index of 
                    // smaller element
                    i++;
                    swap(arr, i, j);
                }
            }
            swap(arr, i + 1, high);
            return (i + 1);
        }

        /* The main function that implements QuickSort
                    arr[] --> Array to be sorted,
                    low --> Starting index,
                    high --> Ending index
           */
        static void quickSort(item[] arr, int low, int high)
        {
            if (low < high)
            {

                // pi is partitioning index, arr[p]
                // is now at right place 
                int pi = partition(arr, low, high);

                // Separately sort elements before
                // partition and after partition
                quickSort(arr, low, pi - 1);
                quickSort(arr, pi + 1, high);
            }
        }
    }
}