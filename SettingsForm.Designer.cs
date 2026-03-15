using System.ComponentModel;
using System.Windows.Forms;

namespace RunnerTray
{
    partial class SettingsForm
    {
        private IContainer components = null;
        private TextBox txtCommand;
        private Label lblCommand;
        private Button btnOk;
        private Button btnCancel;
        private Button btnBrowse;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtCommand = new TextBox();
            this.lblCommand = new Label();
            this.btnOk = new Button();
            this.btnCancel = new Button();
            this.btnBrowse = new Button();
            this.SuspendLayout();
            // 
            // lblCommand
            // 
            this.lblCommand.AutoSize = true;
            this.lblCommand.Location = new System.Drawing.Point(12, 15);
            this.lblCommand.Name = "lblCommand";
            this.lblCommand.Size = new System.Drawing.Size(57, 13);
            this.lblCommand.TabIndex = 0;
            this.lblCommand.Text = "Command:";
            // 
            // txtCommand
            // 
            this.txtCommand.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.txtCommand.Location = new System.Drawing.Point(75, 12);
            this.txtCommand.Name = "txtCommand";
            this.txtCommand.Size = new System.Drawing.Size(258, 20);
            this.txtCommand.TabIndex = 1;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnBrowse.Location = new System.Drawing.Point(339, 10);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(33, 23);
            this.btnBrowse.TabIndex = 4;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.btnOk.Location = new System.Drawing.Point(216, 47);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(297, 47);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.btnOk;
            this.CancelButton = this.btnCancel;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 82);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtCommand);
            this.Controls.Add(this.lblCommand);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

