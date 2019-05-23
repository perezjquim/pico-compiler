namespace PICOCompiler
{
    partial class ErrorForm
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
            this._list = new System.Windows.Forms.ListView();
            this._col_filename = new System.Windows.Forms.ColumnHeader();
            this._col_error_type = new System.Windows.Forms.ColumnHeader();
            this._col_queue = new System.Windows.Forms.ColumnHeader();
            this._col_column = new System.Windows.Forms.ColumnHeader();
            this._col_description = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            this._list.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this._col_filename,
            this._col_error_type,
            this._col_queue,
            this._col_column,
            this._col_description});
            this._list.Location = new System.Drawing.Point(27, 27);
            this._list.Name = "_list";
            this._list.Size = new System.Drawing.Size(822, 349);
            this._list.TabIndex = 5;
            this._list.UseCompatibleStateImageBehavior = false;
            this._list.View = System.Windows.Forms.View.Details;
            this._col_filename.Text = "File name";
            this._col_filename.Width = 300;
            this._col_error_type.Text = "Error type";
            this._col_error_type.Width = 100;
            this._col_queue.Text = "Line";
            this._col_queue.Width = 100;
            this._col_column.Text = "Column";
            this._col_column.Width = 100;
            this._col_description.Text = "Description";
            this._col_description.Width = 450;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MidnightBlue;
            this.ClientSize = new System.Drawing.Size(883, 414);
            this.Controls.Add(this._list);
            this.Name = "ErrorForm";
            this.Text = "Error list";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ColumnHeader _col_filename;
        private System.Windows.Forms.ColumnHeader _col_error_type;
        private System.Windows.Forms.ColumnHeader _col_queue;
        private System.Windows.Forms.ColumnHeader _col_column;
        private System.Windows.Forms.ColumnHeader _col_description;
        public System.Windows.Forms.ListView _list;
    }
}