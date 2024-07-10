using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaximCS
{
    public class QuickSorter : ISorter
    {
        public string Sort(string input)
        {
            char[] arr = input.ToCharArray();
            QuickSort(arr, 0, arr.Length - 1);
            return new string(arr);
        }

        private void QuickSort(char[] arr, int left, int right)
        {
            int i = left, j = right;
            char pivot = arr[(left + right) / 2];

            while (i <= j)
            {
                while (arr[i] < pivot) i++;
                while (arr[j] > pivot) j--;
                if (i <= j)
                {
                    char tmp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = tmp;
                    i++;
                    j--;
                }
            }

            if (left < j) QuickSort(arr, left, j);
            if (i < right) QuickSort(arr, i, right);
        }
    }

}
