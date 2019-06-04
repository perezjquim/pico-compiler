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
            this._col_filename = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._col_error_type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._col_queue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._col_column = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._col_description = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // _list
            // 
            this._list.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this._col_filename,
            this._col_error_type,
            this._col_queue,
            this._col_column,
            this._col_description});
            this._list.Location = new System.Drawing.Point(12, 12);
            this._list.Name = "_list";
            this._list.Size = new System.Drawing.Size(859, 390);
            this._list.TabIndex = 5;
            this._list.UseCompatibleStateImageBehavior = false;
            this._list.View = System.Windows.Forms.View.Details;
            // 
            // _col_filename
            // 
            this._col_filename.Text = "File name";
            this._col_filename.Width = 300;
            // 
            // _col_error_type
            // 
            this._col_error_type.Text = "Error type";
            this._col_error_type.Width = 100;
            // 
            // _col_queue
            // 
            this._col_queue.Text = "Line";
            this._col_queue.Width = 100;
            // 
            // _col_column
            // 
            this._col_column.Text = "Column";
            this._col_column.Width = 100;
            // 
            // _col_description
            // 
            this._col_description.Text = "Description";
            this._col_description.Width = 450;
            // 
            // ErrorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(883, 414);
            this.Controls.Add(this._list);
            this.Name = "ErrorForm";
            this.Text = "PICOCompiler - Error list";
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