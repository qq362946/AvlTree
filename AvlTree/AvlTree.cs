using System;

namespace AvlTree
{
    public class AvlTree<TKey> where TKey : IComparable<TKey>
    {
        public AvlTreeNote<TKey> Root;          // 根节点

        private bool _isBalance;                // 标志是否平衡过二叉树

        /// <summary>
        /// 插入节点
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public AvlTreeNote<TKey> Insert(TKey key) => Root = Insert(key, Root);
        
        /// <summary>
        /// 插入到指定节点下
        /// </summary>
        /// <param name="key"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        private AvlTreeNote<TKey> Insert(TKey key, AvlTreeNote<TKey> node)
        {
            if (node == null)
            {
                node = new AvlTreeNote<TKey>(key, null, null);
            }
            else
            {
                // 如果树里已经存在该节点，直接返回为null
                
                if (key.CompareTo(node.Key) == 0) return null;

                if (key.CompareTo(node.Key) < 0)
                {
                    // 应该在左树进行搜索插入
                    
                    node.LChild = Insert(key, node.LChild);

                    if (node.LChild == null) return node;
                    
                    switch (node.Height)
                    {
                        case 1:
                            return LeftBalance(node);
                        case 0:
                            node.Height = _isBalance ? 0 : 1;
                            break;
                        case -1:
                            node.Height = 0;
                            break;
                    }
                }
                else
                {
                    // 应该在右树进行搜索插入
                    
                    node.RChild = Insert(key, node.RChild);

                    if (node.RChild == null) return node;
                    
                    switch (node.Height)
                    {
                        case 1:
                            node.Height = 0;
                            break;
                        case 0:
                            node.Height = _isBalance ? 0 : -1;
                            break;
                        case -1:
                            return RightBalance(node); 
                    }
                }
            }
            
            _isBalance = false;

            return node;
        }

        /// <summary>
        /// 左树平衡处理
        /// </summary>
        /// <param name="node"></param>
        private AvlTreeNote<TKey> LeftBalance(AvlTreeNote<TKey> node)
        {
            if (_isBalance) return node;
            
            var leftNode = node.LChild;

            switch (leftNode.Height)
            {
                case 1:

                    node.Height = leftNode.Height = 0;

                    node = R_Rotate(node);

                    break;

                case -1:

                    node.Height = leftNode.Height = 0;

                    node.LChild = L_Rotate(leftNode);

                    node = R_Rotate(node);

                    break;
            }

            return node;
        }

        private AvlTreeNote<TKey> RightBalance(AvlTreeNote<TKey> node)
        {
            if (_isBalance) return node;
            
            var rightNode = node.RChild;

            switch (rightNode.Height)
            {
                case -1:
                    
                    node.Height = rightNode.Height = 0;
                
                    node = L_Rotate(node);
                    
                    break;
                
                case 1:

                    node.Height = rightNode.Height = 0;

                    node.RChild = R_Rotate(rightNode);
                    
                    node = L_Rotate(node);
                    
                    break;
            }

            return node;
        }

        /// <summary>
        /// 右旋操作
        /// </summary>
        /// <param name="node"></param>
        private AvlTreeNote<TKey> R_Rotate(AvlTreeNote<TKey> node)
        {
            var temp = node.LChild;
            
            node.LChild = temp.RChild;

            temp.RChild = node;
            
            _isBalance = true;
            
            return temp;
        }

        /// <summary>
        /// 左旋操作
        /// </summary>
        /// <param name="node"></param>
        private AvlTreeNote<TKey> L_Rotate(AvlTreeNote<TKey> node)
        {
            var temp = node.RChild;
            
            node.RChild = temp.LChild;
            
            temp.LChild = node;
            
            _isBalance = true;

            return temp;
        }

        /// <summary>
        /// 查找二叉树
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public AvlTreeNote<TKey> Find(TKey key) => Find(key, Root);

        /// <summary>
        /// 查找二叉树
        /// </summary>
        /// <param name="key"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        public AvlTreeNote<TKey> Find(TKey key,AvlTreeNote<TKey> node)
        {
            if (node == null) return null;
            
            if (key.CompareTo(node.Key) < 0)
            {
                node = Find(key,node.LChild);
            }
            else if(key.CompareTo(node.Key)>0)
            {
                node = Find(key, node.RChild);
            }
            
            return node;
        }

        /// <summary>
        /// 用于删除该节点移动节点
        /// </summary>
        /// <param name="node"></param>
        /// <param name="findNode"></param>
        /// <returns></returns>
        private AvlTreeNote<TKey> Move(AvlTreeNote<TKey> node, AvlTreeNote<TKey> findNode)
        {
            AvlTreeNote<TKey> moveNode;

            if (findNode != null)
            {
                if (findNode.RChild != null)
                {
                    moveNode = findNode.RChild;

                    findNode.RChild = null;
                }
                else
                {
                    findNode.LChild = null;

                    moveNode = findNode;
                }
                
                if (node.LChild != moveNode) moveNode.LChild = node.LChild;

                if (node.RChild != moveNode) moveNode.RChild = node.RChild;
            }
            else
            {
                moveNode = null;
            }

            node.LChild = null;

            node.RChild = null;

            node.Key = default(TKey);

            node.Height = 0;

            return moveNode;
        }

        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="key"></param>
        public void Remove(TKey key) => Root = Remove(key, Root);

        private AvlTreeNote<TKey> Remove(TKey key, AvlTreeNote<TKey> node)
        {
            if (node == null) return null;
            
            if (key.CompareTo(node.Key) < 0)
            {
                if (node.LChild == null) return node;
                
                node.LChild = Remove(key, node.LChild);
                    
                switch (node.Height)
                {
                    case 1:
                        node.Height = 0;
                        break;
                    case 0:
                        node.Height = -1;
                        break;
                    case -1:
                        // 要进行旋转
                        node.Height = 0;
                        return node.LChild == null ? RightBalance(node) : LeftBalance(node);
                }
            }
            else if (key.CompareTo(node.Key) > 0)
            {
                if (node.RChild == null) return node;
                
                node.RChild = Remove(key, node.RChild);

                switch (node.Height)
                {
                    case 1:
                        // 要进行旋转
                        node.Height = 0;
                        return node.RChild == null ? LeftBalance(node) : RightBalance(node);
                        break;
                    case 0:
                        node.Height = 1;
                        break;
                    case -1:
                        node.Height = 0;
                        break;
                }
            }
            else if (key.CompareTo(node.Key) == 0)
            {
                var findNode = Remove(key,node.LChild);

                node = Move(node, findNode);
            }
            
            _isBalance = false;

            return node;
        }
    }
}