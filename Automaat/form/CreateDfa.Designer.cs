﻿using System.Windows.Forms;

namespace Automaat.form
{
    partial class CreateDfa
    {
        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }
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
            this.CreateButton = new System.Windows.Forms.Button();
            this.dfaTypeCombobox = new System.Windows.Forms.ComboBox();
            this.dfaTypeLabel = new System.Windows.Forms.Label();
            this.patroonTextBox = new System.Windows.Forms.TextBox();
            this.patroonLabel = new System.Windows.Forms.Label();
            this.alfabetLabel = new System.Windows.Forms.Label();
            this.alfabetTextBox = new System.Windows.Forms.TextBox();
            this.isNietCheckBox = new System.Windows.Forms.CheckBox();
            this.backButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CreateButton
            // 
            this.CreateButton.Location = new System.Drawing.Point(88, 147);
            this.CreateButton.Name = "CreateButton";
            this.CreateButton.Size = new System.Drawing.Size(75, 23);
            this.CreateButton.TabIndex = 0;
            this.CreateButton.Text = "Maak";
            this.CreateButton.UseVisualStyleBackColor = true;
            this.CreateButton.Click += new System.EventHandler(this.CreateButton_Click);
            // 
            // dfaTypeCombobox
            // 
            this.dfaTypeCombobox.FormattingEnabled = true;
            this.dfaTypeCombobox.Items.AddRange(new object[] {
            "Begint met",
            "Bevat",
            "Eindigt op"});
            this.dfaTypeCombobox.Location = new System.Drawing.Point(88, 48);
            this.dfaTypeCombobox.Name = "dfaTypeCombobox";
            this.dfaTypeCombobox.Size = new System.Drawing.Size(121, 21);
            this.dfaTypeCombobox.TabIndex = 1;
            // 
            // dfaTypeLabel
            // 
            this.dfaTypeLabel.AutoSize = true;
            this.dfaTypeLabel.Location = new System.Drawing.Point(12, 51);
            this.dfaTypeLabel.Name = "dfaTypeLabel";
            this.dfaTypeLabel.Size = new System.Drawing.Size(47, 13);
            this.dfaTypeLabel.TabIndex = 2;
            this.dfaTypeLabel.Text = "Dfa type";
            // 
            // patroonTextBox
            // 
            this.patroonTextBox.Location = new System.Drawing.Point(88, 75);
            this.patroonTextBox.Name = "patroonTextBox";
            this.patroonTextBox.Size = new System.Drawing.Size(121, 20);
            this.patroonTextBox.TabIndex = 3;
            // 
            // patroonLabel
            // 
            this.patroonLabel.AutoSize = true;
            this.patroonLabel.Location = new System.Drawing.Point(12, 78);
            this.patroonLabel.Name = "patroonLabel";
            this.patroonLabel.Size = new System.Drawing.Size(44, 13);
            this.patroonLabel.TabIndex = 4;
            this.patroonLabel.Text = "Patroon";
            // 
            // alfabetLabel
            // 
            this.alfabetLabel.AutoSize = true;
            this.alfabetLabel.Location = new System.Drawing.Point(12, 104);
            this.alfabetLabel.Name = "alfabetLabel";
            this.alfabetLabel.Size = new System.Drawing.Size(40, 13);
            this.alfabetLabel.TabIndex = 5;
            this.alfabetLabel.Text = "Alfabet";
            // 
            // alfabetTextBox
            // 
            this.alfabetTextBox.Location = new System.Drawing.Point(88, 101);
            this.alfabetTextBox.Name = "alfabetTextBox";
            this.alfabetTextBox.Size = new System.Drawing.Size(121, 20);
            this.alfabetTextBox.TabIndex = 6;
            this.alfabetTextBox.Text = "ab";
            // 
            // isNietCheckBox
            // 
            this.isNietCheckBox.AutoSize = true;
            this.isNietCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.isNietCheckBox.Location = new System.Drawing.Point(12, 127);
            this.isNietCheckBox.Name = "isNietCheckBox";
            this.isNietCheckBox.Size = new System.Drawing.Size(90, 17);
            this.isNietCheckBox.TabIndex = 7;
            this.isNietCheckBox.Text = "Is ontkenning";
            this.isNietCheckBox.UseVisualStyleBackColor = true;
            // 
            // backButton
            // 
            this.backButton.Location = new System.Drawing.Point(169, 147);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(75, 23);
            this.backButton.TabIndex = 8;
            this.backButton.Text = "Terug";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // CreateDfa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(256, 211);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.isNietCheckBox);
            this.Controls.Add(this.alfabetTextBox);
            this.Controls.Add(this.alfabetLabel);
            this.Controls.Add(this.patroonLabel);
            this.Controls.Add(this.patroonTextBox);
            this.Controls.Add(this.dfaTypeLabel);
            this.Controls.Add(this.dfaTypeCombobox);
            this.Controls.Add(this.CreateButton);
            this.Name = "CreateDfa";
            this.Text = "Create Dfa";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CreateButton;
        private System.Windows.Forms.ComboBox dfaTypeCombobox;
        private System.Windows.Forms.Label dfaTypeLabel;
        private System.Windows.Forms.TextBox patroonTextBox;
        private System.Windows.Forms.Label patroonLabel;
        private System.Windows.Forms.Label alfabetLabel;
        private System.Windows.Forms.TextBox alfabetTextBox;
        private System.Windows.Forms.CheckBox isNietCheckBox;
        private System.Windows.Forms.Button backButton;
    }
}