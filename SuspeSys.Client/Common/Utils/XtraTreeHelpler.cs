using DevExpress.XtraTreeList.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuspeSys.Client.Common.Utils
{
    /// <summary>
    /// Dev Tree helper
    /// </summary>
    public class XtraTreeHelpler
    {
        /// <summary>
        /// 设置子节点状态
        /// </summary>
        /// <param name="node"></param>
        /// <param name="check"></param>
        public static void SetCheckedChildNodes(TreeListNode node, CheckState check)
        {
            for (int i = 0; i < node.Nodes.Count; i++)
            {
                node.Nodes[i].CheckState = check;
                SetCheckedChildNodes(node.Nodes[i], check);
            }
        }

        /// <summary>
        /// 设置父节点的状态 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="check"></param>
        static public void SetCheckedParentNodes(TreeListNode node, CheckState check)
        {
            if (node.ParentNode != null)
            {
                bool b = false;
                CheckState state;
                for (int i = 0; i < node.ParentNode.Nodes.Count; i++)
                {
                    state = (CheckState)node.ParentNode.Nodes[i].CheckState;
                    if (!check.Equals(state))
                    {
                        b = !b;
                        break;
                    }
                }
                node.ParentNode.CheckState = b ? CheckState.Indeterminate : check;
                SetCheckedParentNodes(node.ParentNode, check);
            }
        }

        /// <summary>
        /// 递归获取节点状态
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="selectNodes"></param>
        static public List<TreeListNode> GetSelectNode(TreeListNodes nodes)
        {
            List<TreeListNode> selectNodes = new List<TreeListNode>();
            foreach (TreeListNode item in nodes)
            {

                if (item.CheckState != CheckState.Unchecked)
                    selectNodes.Add(item);

                GetSelectNode(item, selectNodes);
            }

            return selectNodes;
        }

        /// <summary>
        /// 递归获取节点状态
        /// </summary>
        /// <param name="node"></param>
        /// <param name="selectNodes"></param>
        /// <param name="checkState"></param>
        static private void GetSelectNode(TreeListNode node, List<TreeListNode> selectNodes)
        {
            if (!node.HasChildren)
            {
                return;
            }
            else
            {
                foreach (TreeListNode item in node.Nodes)
                {
                    if (item.CheckState != CheckState.Unchecked)
                        selectNodes.Add(item);

                    GetSelectNode(item, selectNodes);
                }
            }
        }
    }
}
