namespace real_state_app1.form
{
    partial class OwnerListForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ownerlist = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.ownerlist)).BeginInit();
            this.SuspendLayout();
            // 
            // ownerlist
            // 
            this.ownerlist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ownerlist.Location = new System.Drawing.Point(-36, -5);
            this.ownerlist.Name = "ownerlist";
            this.ownerlist.RowHeadersWidth = 51;
            this.ownerlist.RowTemplate.Height = 24;
            this.ownerlist.Size = new System.Drawing.Size(1012, 350);
            this.ownerlist.TabIndex = 0;
            this.ownerlist.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ownerlist_CellContentClick);
            this.ownerlist.DoubleClick += new System.EventHandler(this.ownerlist_DoubleClick);
            // 
            // OwnerListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(1177, 613);
            this.Controls.Add(this.ownerlist);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "OwnerListForm";
            this.Text = "OwnerListForm";
            this.Load += new System.EventHandler(this.OwnerListForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ownerlist)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.DataGridView ownerlist;
    }
}