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
            ParserTreeForm form = new ParserTreeForm(_parser.GetTree());
            form.Show();
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
    }
}
