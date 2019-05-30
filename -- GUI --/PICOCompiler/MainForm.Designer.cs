namespace PICOCompiler
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this._strip = new System.Windows.Forms.MenuStrip();
            this._strip_item_new = new System.Windows.Forms.ToolStripMenuItem();
            this._strip_item_open_file = new System.Windows.Forms.ToolStripMenuItem();
            this._strip_item_show_errors = new System.Windows.Forms.ToolStripMenuItem();
            this._strip_item_show_tree = new System.Windows.Forms.ToolStripMenuItem();
            this._dialog_file = new System.Windows.Forms.OpenFileDialog();
            this._strip.SuspendLayout();
            this.SuspendLayout();
            this._strip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._strip_item_new});
            this._strip.Location = new System.Drawing.Point(0, 0);
            this._strip.Name = "_strip";
            this._strip.Size = new System.Drawing.Size(910, 24);
            this._strip.TabIndex = 0;
            this._strip.Text = "_strip";       
            this._strip_item_new.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._strip_item_open_file,
            this._strip_item_show_errors,
            this._strip_item_show_tree});
            this._strip_item_new.Name = "_strip_item_new";
            this._strip_item_new.Size = new System.Drawing.Size(54, 20);
            this._strip_item_new.Text = "File";
            this._strip_item_open_file.Name = "_strip_item_open_file";
            this._strip_item_open_file.Size = new System.Drawing.Size(152, 22);
            this._strip_item_open_file.Text = "Open PICO file";
            this._strip_item_open_file.Click += new System.EventHandler(this._onOpenFilePrompt);
            this._strip_item_show_errors.Name = "_strip_item_show_errors";
            this._strip_item_show_errors.Size = new System.Drawing.Size(152, 22);
            this._strip_item_show_errors.Text = "Show errors";
            this._strip_item_show_errors.Click += new System.EventHandler(this._onOpenErrorList);
            this._strip_item_show_tree.Name = "_strip_item_show_tree";
            this._strip_item_show_tree.Size = new System.Drawing.Size(152, 22);
            this._strip_item_show_tree.Text = "Show parse tree";
            this._strip_item_show_tree.Click += new System.EventHandler(this._onOpenParseTree);
            this._dialog_file.FileName = "_dialog_file";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 442);
            this.Controls.Add(this._strip);
            this.MainMenuStrip = this._strip;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this._strip.ResumeLayout(false);
            this._strip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip _strip;
        private System.Windows.Forms.ToolStripMenuItem _strip_item_new;
        private System.Windows.Forms.ToolStripMenuItem _strip_item_open_file;
        private System.Windows.Forms.ToolStripMenuItem _strip_item_show_errors;
        private System.Windows.Forms.ToolStripMenuItem _strip_item_show_tree;
        private System.Windows.Forms.OpenFileDialog _dialog_file;
    }
}

