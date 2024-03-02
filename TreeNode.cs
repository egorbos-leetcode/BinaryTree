namespace BinaryTree;

public class TreeNode<T>(T val = default, TreeNode<T>? left = null, TreeNode<T>? right = null) where T : struct
{
    public T val = val;
    public TreeNode<T>? left = left;
    public TreeNode<T>? right = right;

    public T?[] ToArray()
    {
        var list = new List<T?> { val };
        var queue = new Queue<TreeNode<T>?>([left, right]);

        while (queue.Count > 0)
        {
            var left = queue.Dequeue();
            var right = queue.Dequeue();

            if (left is null && right is null) continue;

            list.Add(left?.val);
            list.Add(right?.val);

            queue.Enqueue(left?.left);
            queue.Enqueue(left?.right);

            queue.Enqueue(right?.left);
            queue.Enqueue(right?.right);
        }

        return [..list];
    }
}

public static class TreeNode
{
    public static TreeNode<T>? BuildTree<T>(T?[] values) where T : struct
    {
        if (values is null || values.Length == 0 || values[0] is null) return null;

        var root = new TreeNode<T>((T)values[0]);
        var queue = new Queue<TreeNode<T>?>([root]);

        var idx = 1;
        
        while (idx < values.Length)
        {
            var current = queue.Dequeue();
            
            if (current is null) continue;

            if (idx < values.Length)
            {
                var value = values[idx++];
                current.left = value.HasValue ? new TreeNode<T>(value.Value) : null;
                queue.Enqueue(current.left);
            }
            if (idx < values.Length)
            {
                var value = values[idx++];
                current.right = value.HasValue ? new TreeNode<T>(value.Value) : null;
                queue.Enqueue(current.right);
            }
        }

        return root;
    }
}