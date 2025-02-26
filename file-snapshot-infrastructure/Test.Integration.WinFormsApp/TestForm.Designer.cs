namespace Test.Integration.WinFormsApp
{
    partial class TestForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblMain = new Label();
            SuspendLayout();
            // 
            // lblMain
            // 
            lblMain.AutoSize = true;
            lblMain.Font = new Font("MS Reference Sans Serif", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblMain.ForeColor = Color.OrangeRed;
            lblMain.Location = new Point(86, 83);
            lblMain.Name = "lblMain";
            lblMain.Size = new Size(144, 24);
            lblMain.TabIndex = 0;
            lblMain.Text = "Incorrect Text";
            // 
            // TestForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(329, 197);
            Controls.Add(lblMain);
            Name = "TestForm";
            Text = "TestForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblMain;
    }
}
