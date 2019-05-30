using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PICOCompiler
{
    public partial class ParserTreeForm : Form
    {
        public ParserTreeForm()
        {
            InitializeComponent();
            load();
        }

        private void load()
        {

            //First node in the view

            TreeNode treeNode = new TreeNode("Parent");
            treeView1.Nodes.Add(treeNode);

            // Another node following the first node.

            treeNode = new TreeNode("Parent1");
            treeView1.Nodes.Add(treeNode);

            //child nodes in an array
            TreeNode node2 = new TreeNode("child");
            TreeNode node3 = new TreeNode("child1");
            TreeNode[] childs = new TreeNode[] { node2, node3 };

            //Node with array of childrens

            treeNode = new TreeNode("Parent2childs", childs);
            treeView1.Nodes.Add(treeNode);

        }
    }
}
