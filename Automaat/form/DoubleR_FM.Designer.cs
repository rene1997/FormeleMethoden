namespace Automaat.form
{
    partial class DoubleR_FM
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
            this.listOfDfas = new System.Windows.Forms.ListBox();
            this.createDfaButton = new System.Windows.Forms.Button();
            this.toonDfaButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listOfDfas
            // 
            this.listOfDfas.FormattingEnabled = true;
            this.listOfDfas.Location = new System.Drawing.Point(616, 12);
            this.listOfDfas.Name = "listOfDfas";
            this.listOfDfas.Size = new System.Drawing.Size(156, 537);
            this.listOfDfas.TabIndex = 0;
            this.listOfDfas.SelectedValueChanged += new System.EventHandler(this.listOfDfas_SelectedValueChanged);
            // 
            // createDfaButton
            // 
            this.createDfaButton.Location = new System.Drawing.Point(535, 12);
            this.createDfaButton.Name = "createDfaButton";
            this.createDfaButton.Size = new System.Drawing.Size(75, 23);
            this.createDfaButton.TabIndex = 1;
            this.createDfaButton.Text = "Create DFA";
            this.createDfaButton.UseVisualStyleBackColor = true;
            this.createDfaButton.Click += new System.EventHandler(this.createDfaButton_Click);
            // 
            // toonDfaButton
            // 
            this.toonDfaButton.Location = new System.Drawing.Point(535, 72);
            this.toonDfaButton.Name = "toonDfaButton";
            this.toonDfaButton.Size = new System.Drawing.Size(75, 23);
            this.toonDfaButton.TabIndex = 2;
            this.toonDfaButton.Text = "Toon Dfa";
            this.toonDfaButton.UseVisualStyleBackColor = true;
            this.toonDfaButton.Click += new System.EventHandler(this.toonDfaButton_Click);
            // 
            // DoubleR_FM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.toonDfaButton);
            this.Controls.Add(this.createDfaButton);
            this.Controls.Add(this.listOfDfas);
            this.Name = "DoubleR_FM";
            this.Text = "Rene Keijzer & Remco Vorthoren; Formele methode applicatie";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listOfDfas;
        private System.Windows.Forms.Button createDfaButton;
        private System.Windows.Forms.Button toonDfaButton;
    }
}