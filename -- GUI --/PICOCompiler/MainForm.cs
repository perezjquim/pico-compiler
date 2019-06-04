using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using com.calitha.goldparser;

namespace PICOCompiler
{
    public partial class MainForm : Form
    {
        private PICOParser _parser;
        private String _file;
        private String _file_name;
        //...

        public MainForm()
        {
            InitializeComponent();
            //...
        }

        private void _onOpenFilePrompt(object sender, EventArgs e)
        {
            this.Refresh();
            // ...
            if (_dialog_file.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamReader reader = new System.IO.StreamReader(_dialog_file.FileName, Encoding.Default);
                _file = reader.ReadToEnd();
                _file_name = _dialog_file.FileName;
                reader.Close();
                _analyzeFile();
            }
        }

        private void _onOpenErrorList(object sender, EventArgs e)
        {
            ErrorForm form = new ErrorForm();           
            _listErrors(form);
            form.Show();
        }

        private void _onOpenParseTree(object sender, EventArgs e)
        {
            if(_parser != null)
            {
                ParserTreeForm form = new ParserTreeForm(_parser.GetTree().Clone() as TreeNode);
                form.Show();
            }            
        }

        private void _listErrors(ErrorForm form)
        {
            if(_parser != null)
            {
                foreach (String[] error in _parser.GetErrorList())
                {
                    System.Windows.Forms.ListViewItem lis = new System.Windows.Forms.ListViewItem(error);
                    form._list.Items.Add(lis);
                }
            }
        }

        private void _analyzeFile()
        {
            _parser = new PICOParser(this, Application.StartupPath + "\\GrammarTable.cgt");
            _parser.Parse(_file);
        }

        public String getFileName()
        {
            return _file_name;
        }
        
        private void setState(PictureBox box, bool success)
        {
            if (success)
            {
                box.Image = global::PICOCompiler.Properties.Resources._checked;
            }
            else
            {
                box.Image = global::PICOCompiler.Properties.Resources.cancel;
            }
        }

        public void setLeituraState(bool success)
        {
            setState(this.pictureBox2, success);           
        }


        public void setSyntaxState(bool success)
        {
            setState(this.pictureBox4, success);
        }

        private void _dialog_file_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void _strip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {

        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void _strip_item_new_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
