namespace PriorityQueue
{
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
        

        public PQ()
        {
        }

        // Returns the index of the highest priority item in the queue, whether that be the first item in the queue (default) or an item entered into the queue with a higher priority
        // The runtime of this call is O(n)
        private int priorityItem()
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
            int pIndex = priorityItem();
            item priority = pQueue[pIndex];

            pQueue.RemoveAt(pIndex);
            Count--;

            return priority.data;
        }

        // This function is used to view the highest priority element in the queue without removing it from the queue.
        // The runtime of this method is O(n)
        public T peek()
        {
            return pQueue[priorityItem()].data;
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

        // This operation prints the queue in priority order using merge sort.
        // The runtime of this method is O(nlogn)
        public void printQueueInPriority()
        {
            item[] items = new item[Count];
            pQueue.CopyTo(items, 0);

            MergeSort(items, 0, items.Length - 1);

            Console.WriteLine("In this queue, the smallest value is the highest priority. \nI.E. 1 is of higher priority than 5, with 5 being the default priority value.\nQueue in Priority Order:");
            foreach (item item in items)
            {
                Console.WriteLine($"{item.data} (priority {item.priority})");
            }
            Console.WriteLine("End of Queue");
        }

        // C# Merge Sort from Geeks for Geeks altered for item implementation

        // Merges two subarrays of []arr.
        // First subarray is arr[l..m]
        // Second subarray is arr[m+1..r]
        void merge(item[] arr, int l, int m, int r)
        {
            // Find sizes of two
            // subarrays to be merged
            int n1 = m - l + 1;
            int n2 = r - m;

            // Create temp arrays
            item[] L = new item[n1];
            item[] R = new item[n2];
            int i, j;

            // Copy data to temp arrays
            for (i = 0; i < n1; ++i)
                L[i] = arr[l + i];
            for (j = 0; j < n2; ++j)
                R[j] = arr[m + 1 + j];

            // Merge the temp arrays

            // Initial indexes of first
            // and second subarrays
            i = 0;
            j = 0;

            // Initial index of merged
            // subarray array
            int k = l;
            while (i < n1 && j < n2)
            {
                if (L[i].priority <= R[j].priority)
                {
                    arr[k] = L[i];
                    i++;
                }
                else
                {
                    arr[k] = R[j];
                    j++;
                }
                k++;
            }

            // Copy remaining elements
            // of L[] if any
            while (i < n1)
            {
                arr[k] = L[i];
                i++;
                k++;
            }

            // Copy remaining elements
            // of R[] if any
            while (j < n2)
            {
                arr[k] = R[j];
                j++;
                k++;
            }
        }

        // Main function that
        // sorts arr[l..r] using
        // merge()
        void MergeSort(item[] arr, int l, int r)
        {
            if (l < r)
            {
                // Find the middle
                // point
                int m = l + (r - l) / 2;

                // Sort first and
                // second halves
                MergeSort(arr, l, m);
                MergeSort(arr, m + 1, r);

                // Merge the sorted halves
                merge(arr, l, m, r);
            }
        }
    }

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
}