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
        public ParserTreeForm(TreeNode tree)
        {
            InitializeComponent();
            load(tree);
        }

        private void load(TreeNode tree)
        {
            treeView1.Nodes.Add(tree);      
        }
    }
}
