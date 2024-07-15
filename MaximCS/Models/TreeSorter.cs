using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaximCS.Models
{
    public class TreeSorter : ISorter
    {
        public string Sort(string input)
        {
            TreeNode root = null;
            foreach (char c in input)
            {
                root = Insert(root, c);
            }

            List<char> sortedList = new List<char>();
            InOrderTraversal(root, sortedList);
            return new string(sortedList.ToArray());
        }

        private TreeNode Insert(TreeNode node, char key)
        {
            if (node == null)
                return new TreeNode(key);

            if (key < node.Key)
                node.Left = Insert(node.Left, key);
            else
                node.Right = Insert(node.Right, key);

            return node;
        }

        private void InOrderTraversal(TreeNode node, List<char> sortedList)
        {
            if (node == null) return;

            InOrderTraversal(node.Left, sortedList);
            sortedList.Add(node.Key);
            InOrderTraversal(node.Right, sortedList);
        }

        private class TreeNode
        {
            public char Key;
            public TreeNode Left, Right;

            public TreeNode(char key)
            {
                Key = key;
                Left = Right = null;
            }
        }
    }

}
