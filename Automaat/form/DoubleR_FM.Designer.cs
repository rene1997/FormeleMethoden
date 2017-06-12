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
            this.combineLabel = new System.Windows.Forms.Label();
            this.combineDfa1Label = new System.Windows.Forms.Label();
            this.combineDfa2Label = new System.Windows.Forms.Label();
            this.combineDfaComboBox = new System.Windows.Forms.ComboBox();
            this.combineDfaSelectButton = new System.Windows.Forms.Button();
            this.combineDfaCreateButton = new System.Windows.Forms.Button();
            this.combineDfaResetButton = new System.Windows.Forms.Button();
            this.MinimizeButtonHopCroft = new System.Windows.Forms.Button();
            this.MinimizeLabel = new System.Windows.Forms.Label();
            this.MinimizeButtonReverse = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listOfDfas
            // 
            this.listOfDfas.FormattingEnabled = true;
            this.listOfDfas.Location = new System.Drawing.Point(311, 9);
            this.listOfDfas.Name = "listOfDfas";
            this.listOfDfas.Size = new System.Drawing.Size(317, 537);
            this.listOfDfas.TabIndex = 0;
            this.listOfDfas.SelectedValueChanged += new System.EventHandler(this.listOfDfas_SelectedValueChanged);
            // 
            // createDfaButton
            // 
            this.createDfaButton.Location = new System.Drawing.Point(215, 523);
            this.createDfaButton.Name = "createDfaButton";
            this.createDfaButton.Size = new System.Drawing.Size(75, 23);
            this.createDfaButton.TabIndex = 1;
            this.createDfaButton.Text = "Maak DFA";
            this.createDfaButton.UseVisualStyleBackColor = true;
            this.createDfaButton.Click += new System.EventHandler(this.createDfaButton_Click);
            // 
            // toonDfaButton
            // 
            this.toonDfaButton.Location = new System.Drawing.Point(215, 38);
            this.toonDfaButton.Name = "toonDfaButton";
            this.toonDfaButton.Size = new System.Drawing.Size(75, 23);
            this.toonDfaButton.TabIndex = 2;
            this.toonDfaButton.Text = "Toon";
            this.toonDfaButton.UseVisualStyleBackColor = true;
            this.toonDfaButton.Click += new System.EventHandler(this.toonDfaButton_Click);
            // 
            // combineLabel
            // 
            this.combineLabel.AutoSize = true;
            this.combineLabel.Location = new System.Drawing.Point(12, 9);
            this.combineLabel.Name = "combineLabel";
            this.combineLabel.Size = new System.Drawing.Size(105, 13);
            this.combineLabel.TabIndex = 3;
            this.combineLabel.Text = "Combineer van Dfa\'s";
            // 
            // combineDfa1Label
            // 
            this.combineDfa1Label.AutoSize = true;
            this.combineDfa1Label.Location = new System.Drawing.Point(9, 32);
            this.combineDfa1Label.Name = "combineDfa1Label";
            this.combineDfa1Label.Size = new System.Drawing.Size(0, 13);
            this.combineDfa1Label.TabIndex = 4;
            // 
            // combineDfa2Label
            // 
            this.combineDfa2Label.AutoSize = true;
            this.combineDfa2Label.Location = new System.Drawing.Point(9, 91);
            this.combineDfa2Label.Name = "combineDfa2Label";
            this.combineDfa2Label.Size = new System.Drawing.Size(0, 13);
            this.combineDfa2Label.TabIndex = 5;
            // 
            // combineDfaComboBox
            // 
            this.combineDfaComboBox.FormattingEnabled = true;
            this.combineDfaComboBox.Location = new System.Drawing.Point(12, 57);
            this.combineDfaComboBox.Name = "combineDfaComboBox";
            this.combineDfaComboBox.Size = new System.Drawing.Size(43, 21);
            this.combineDfaComboBox.TabIndex = 6;
            // 
            // combineDfaSelectButton
            // 
            this.combineDfaSelectButton.Location = new System.Drawing.Point(215, 9);
            this.combineDfaSelectButton.Name = "combineDfaSelectButton";
            this.combineDfaSelectButton.Size = new System.Drawing.Size(75, 23);
            this.combineDfaSelectButton.TabIndex = 7;
            this.combineDfaSelectButton.Text = "Selecteer";
            this.combineDfaSelectButton.UseVisualStyleBackColor = true;
            this.combineDfaSelectButton.Click += new System.EventHandler(this.combineDfaSelectButton_Click);
            // 
            // combineDfaCreateButton
            // 
            this.combineDfaCreateButton.Location = new System.Drawing.Point(93, 121);
            this.combineDfaCreateButton.Name = "combineDfaCreateButton";
            this.combineDfaCreateButton.Size = new System.Drawing.Size(105, 23);
            this.combineDfaCreateButton.TabIndex = 8;
            this.combineDfaCreateButton.Text = "Maak combinatie";
            this.combineDfaCreateButton.UseVisualStyleBackColor = true;
            this.combineDfaCreateButton.Click += new System.EventHandler(this.combineDfaCreateButton_Click);
            // 
            // combineDfaResetButton
            // 
            this.combineDfaResetButton.Location = new System.Drawing.Point(12, 121);
            this.combineDfaResetButton.Name = "combineDfaResetButton";
            this.combineDfaResetButton.Size = new System.Drawing.Size(75, 23);
            this.combineDfaResetButton.TabIndex = 9;
            this.combineDfaResetButton.Text = "Reset";
            this.combineDfaResetButton.UseVisualStyleBackColor = true;
            this.combineDfaResetButton.Click += new System.EventHandler(this.combineDfaResetButton_Click);
            // 
            // MinimizeButtonHopCroft
            // 
            this.MinimizeButtonHopCroft.Location = new System.Drawing.Point(215, 147);
            this.MinimizeButtonHopCroft.Name = "MinimizeButtonHopCroft";
            this.MinimizeButtonHopCroft.Size = new System.Drawing.Size(75, 23);
            this.MinimizeButtonHopCroft.TabIndex = 10;
            this.MinimizeButtonHopCroft.Text = "HopCroft";
            this.MinimizeButtonHopCroft.UseVisualStyleBackColor = true;
            this.MinimizeButtonHopCroft.Click += new System.EventHandler(this.MinimizeButtonHopCroft_Click);
            // 
            // MinimizeLabel
            // 
            this.MinimizeLabel.AutoSize = true;
            this.MinimizeLabel.Location = new System.Drawing.Point(212, 131);
            this.MinimizeLabel.Name = "MinimizeLabel";
            this.MinimizeLabel.Size = new System.Drawing.Size(70, 13);
            this.MinimizeLabel.TabIndex = 11;
            this.MinimizeLabel.Text = "Minimaliseren";
            // 
            // MinimizeButtonReverse
            // 
            this.MinimizeButtonReverse.Location = new System.Drawing.Point(215, 176);
            this.MinimizeButtonReverse.Name = "MinimizeButtonReverse";
            this.MinimizeButtonReverse.Size = new System.Drawing.Size(75, 23);
            this.MinimizeButtonReverse.TabIndex = 12;
            this.MinimizeButtonReverse.Text = "Reverse";
            this.MinimizeButtonReverse.UseVisualStyleBackColor = true;
            this.MinimizeButtonReverse.Click += new System.EventHandler(this.MinimizeButtonReverse_Click);
            // 
            // removeButton
            // 
            this.removeButton.Location = new System.Drawing.Point(215, 65);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(75, 23);
            this.removeButton.TabIndex = 13;
            this.removeButton.Text = "Verwijder";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // DoubleR_FM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 564);
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.MinimizeButtonReverse);
            this.Controls.Add(this.MinimizeLabel);
            this.Controls.Add(this.MinimizeButtonHopCroft);
            this.Controls.Add(this.combineDfaResetButton);
            this.Controls.Add(this.combineDfaCreateButton);
            this.Controls.Add(this.combineDfaSelectButton);
            this.Controls.Add(this.combineDfaComboBox);
            this.Controls.Add(this.combineDfa2Label);
            this.Controls.Add(this.combineDfa1Label);
            this.Controls.Add(this.combineLabel);
            this.Controls.Add(this.toonDfaButton);
            this.Controls.Add(this.createDfaButton);
            this.Controls.Add(this.listOfDfas);
            this.Name = "DoubleR_FM";
            this.Text = "Operaties for Dfa";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listOfDfas;
        private System.Windows.Forms.Button createDfaButton;
        private System.Windows.Forms.Button toonDfaButton;
        private System.Windows.Forms.Label combineLabel;
        private System.Windows.Forms.Label combineDfa1Label;
        private System.Windows.Forms.Label combineDfa2Label;
        private System.Windows.Forms.ComboBox combineDfaComboBox;
        private System.Windows.Forms.Button combineDfaSelectButton;
        private System.Windows.Forms.Button combineDfaCreateButton;
        private System.Windows.Forms.Button combineDfaResetButton;
        private System.Windows.Forms.Button MinimizeButtonHopCroft;
        private System.Windows.Forms.Label MinimizeLabel;
        private System.Windows.Forms.Button MinimizeButtonReverse;
        private System.Windows.Forms.Button removeButton;
    }
}