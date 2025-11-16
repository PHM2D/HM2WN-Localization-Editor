namespace hlm2_localization_editor
{
    partial class Form1
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
            // Define Dark Theme Colors
            var darkBg = System.Drawing.ColorTranslator.FromHtml("#242424");
            var lightFg = System.Drawing.Color.Gainsboro;
            var midBg = System.Drawing.ColorTranslator.FromHtml("#333333");
            var headerBg = System.Drawing.ColorTranslator.FromHtml("#1a1a1a");

            // --- YOUR NEW COLOR ---
            var highlightColor = System.Drawing.ColorTranslator.FromHtml("#75ff1f");
            var highlightTextColor = System.Drawing.Color.Black; // Black text on green bg
            // --- END ADD ---

            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            saveAsToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            translationEnglishToolStripMenuItem = new ToolStripMenuItem();
            exportEnglishToTxtToolStripMenuItem = new ToolStripMenuItem();
            importEnglishFromTxtToolStripMenuItem = new ToolStripMenuItem();
            translationRussianToolStripMenuItem = new ToolStripMenuItem();
            exportRussianToTxtToolStripMenuItem = new ToolStripMenuItem();
            importRussianFromTxtToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            exportToTxtToolStripMenuItem = new ToolStripMenuItem();
            importFromTxtToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            dataGridView1 = new DataGridView();
            lang = new DataGridViewTextBoxColumn();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, aboutToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // --- THIS IS THE FIXED LINE (line 74) ---
            menuStrip1.Renderer = new MyToolStripRenderer(highlightColor, highlightTextColor, darkBg, lightFg, headerBg);
            // --- END FIX ---
            // Set MenuStrip Dark Theme
            menuStrip1.BackColor = headerBg;
            menuStrip1.ForeColor = lightFg;
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem, saveToolStripMenuItem, saveAsToolStripMenuItem, toolStripSeparator2, translationEnglishToolStripMenuItem, translationRussianToolStripMenuItem, toolStripSeparator1, exportToTxtToolStripMenuItem, importFromTxtToolStripMenuItem, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            fileToolStripMenuItem.ForeColor = lightFg;
            fileToolStripMenuItem.DropDown.BackColor = darkBg;
            fileToolStripMenuItem.DropDown.ForeColor = lightFg;
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(196, 22);
            openToolStripMenuItem.Text = "Open";
            openToolStripMenuItem.BackColor = darkBg;
            openToolStripMenuItem.ForeColor = lightFg;
            openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(196, 22);
            saveToolStripMenuItem.Text = "Save";
            saveToolStripMenuItem.BackColor = darkBg;
            saveToolStripMenuItem.ForeColor = lightFg;
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            // 
            // saveAsToolStripMenuItem
            // 
            saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            saveAsToolStripMenuItem.Size = new Size(196, 22);
            saveAsToolStripMenuItem.Text = "Save As..";
            saveAsToolStripMenuItem.BackColor = darkBg;
            saveAsToolStripMenuItem.ForeColor = lightFg;
            saveAsToolStripMenuItem.Click += saveAsToolStripMenuItem_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(193, 6);
            // 
            // translationEnglishToolStripMenuItem
            // 
            translationEnglishToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { exportEnglishToTxtToolStripMenuItem, importEnglishFromTxtToolStripMenuItem });
            translationEnglishToolStripMenuItem.Name = "translationEnglishToolStripMenuItem";
            translationEnglishToolStripMenuItem.Size = new Size(196, 22);
            translationEnglishToolStripMenuItem.Text = "Translation (English)";
            translationEnglishToolStripMenuItem.BackColor = darkBg;
            translationEnglishToolStripMenuItem.ForeColor = lightFg;
            translationEnglishToolStripMenuItem.DropDown.BackColor = darkBg;
            translationEnglishToolStripMenuItem.DropDown.ForeColor = lightFg;
            // 
            // exportEnglishToTxtToolStripMenuItem
            // 
            exportEnglishToTxtToolStripMenuItem.Name = "exportEnglishToTxtToolStripMenuItem";
            exportEnglishToTxtToolStripMenuItem.Size = new Size(182, 22);
            exportEnglishToTxtToolStripMenuItem.Text = "Export Pure Text...";
            exportEnglishToTxtToolStripMenuItem.BackColor = darkBg;
            exportEnglishToTxtToolStripMenuItem.ForeColor = lightFg;
            exportEnglishToTxtToolStripMenuItem.Click += exportEnglishToTxtToolStripMenuItem_Click;
            // 
            // importEnglishFromTxtToolStripMenuItem
            // 
            importEnglishFromTxtToolStripMenuItem.Name = "importEnglishFromTxtToolStripMenuItem";
            importEnglishFromTxtToolStripMenuItem.Size = new Size(182, 22);
            importEnglishFromTxtToolStripMenuItem.Text = "Import Pure Text...";
            importEnglishFromTxtToolStripMenuItem.BackColor = darkBg;
            importEnglishFromTxtToolStripMenuItem.ForeColor = lightFg;
            importEnglishFromTxtToolStripMenuItem.Click += importEnglishFromTxtToolStripMenuItem_Click;
            // 
            // translationRussianToolStripMenuItem
            // 
            translationRussianToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { exportRussianToTxtToolStripMenuItem, importRussianFromTxtToolStripMenuItem });
            translationRussianToolStripMenuItem.Name = "translationRussianToolStripMenuItem";
            translationRussianToolStripMenuItem.Size = new Size(196, 22);
            translationRussianToolStripMenuItem.Text = "Translation (Russian)";
            translationRussianToolStripMenuItem.BackColor = darkBg;
            translationRussianToolStripMenuItem.ForeColor = lightFg;
            translationRussianToolStripMenuItem.DropDown.BackColor = darkBg;
            translationRussianToolStripMenuItem.DropDown.ForeColor = lightFg;
            // 
            // exportRussianToTxtToolStripMenuItem
            // 
            exportRussianToTxtToolStripMenuItem.Name = "exportRussianToTxtToolStripMenuItem";
            exportRussianToTxtToolStripMenuItem.Size = new Size(182, 22);
            exportRussianToTxtToolStripMenuItem.Text = "Export Pure Text...";
            exportRussianToTxtToolStripMenuItem.BackColor = darkBg;
            exportRussianToTxtToolStripMenuItem.ForeColor = lightFg;
            exportRussianToTxtToolStripMenuItem.Click += exportRussianToTxtToolStripMenuItem_Click;
            // 
            // importRussianFromTxtToolStripMenuItem
            // 
            importRussianFromTxtToolStripMenuItem.Name = "importRussianFromTxtToolStripMenuItem";
            importRussianFromTxtToolStripMenuItem.Size = new Size(182, 22);
            importRussianFromTxtToolStripMenuItem.Text = "Import Pure Text...";
            importRussianFromTxtToolStripMenuItem.BackColor = darkBg;
            importRussianFromTxtToolStripMenuItem.ForeColor = lightFg;
            importRussianFromTxtToolStripMenuItem.Click += importRussianFromTxtToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(193, 6);
            // 
            // exportToTxtToolStripMenuItem
            // 
            exportToTxtToolStripMenuItem.Name = "exportToTxtToolStripMenuItem";
            exportToTxtToolStripMenuItem.Size = new Size(196, 22);
            exportToTxtToolStripMenuItem.Text = "Export All to TXT...";
            exportToTxtToolStripMenuItem.BackColor = darkBg;
            exportToTxtToolStripMenuItem.ForeColor = lightFg;
            exportToTxtToolStripMenuItem.Click += exportToTxtToolStripMenuItem_Click;
            // 
            // importFromTxtToolStripMenuItem
            // 
            importFromTxtToolStripMenuItem.Name = "importFromTxtToolStripMenuItem";
            importFromTxtToolStripMenuItem.Size = new Size(196, 22);
            importFromTxtToolStripMenuItem.Text = "Import All from TXT...";
            importFromTxtToolStripMenuItem.BackColor = darkBg;
            importFromTxtToolStripMenuItem.ForeColor = lightFg;
            importFromTxtToolStripMenuItem.Click += importFromTxtToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(196, 22);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.BackColor = darkBg;
            exitToolStripMenuItem.ForeColor = lightFg;
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(52, 20);
            aboutToolStripMenuItem.Text = "About";
            aboutToolStripMenuItem.ForeColor = lightFg;
            aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BackgroundColor = darkBg; // Dark background
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { lang });
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(0, 24);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(800, 426);
            dataGridView1.TabIndex = 1;
            // 
            // lang
            // 
            lang.HeaderText = "lang";
            lang.Name = "lang";
            lang.ReadOnly = true;
            lang.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = darkBg; // Main window background
            ClientSize = new Size(800, 450);
            Controls.Add(dataGridView1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "hlm2_localization-editor";
            // Add the icon to the form window
            try
            {
                this.Icon = new System.Drawing.Icon("icon.ico");
            }
            catch (System.IO.FileNotFoundException)
            {
                // Handle case where icon.ico is missing in debug, but will be found on build
                System.Diagnostics.Debug.WriteLine("icon.ico not found. Ensure it is in the project root.");
            }
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem saveAsToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn lang;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem exportToTxtToolStripMenuItem;
        private ToolStripMenuItem importFromTxtToolStripMenuItem;
        private ToolStripMenuItem translationEnglishToolStripMenuItem;
        private ToolStripMenuItem exportEnglishToTxtToolStripMenuItem;
        private ToolStripMenuItem importEnglishFromTxtToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem translationRussianToolStripMenuItem;
        private ToolStripMenuItem exportRussianToTxtToolStripMenuItem;
        private ToolStripMenuItem importRussianFromTxtToolStripMenuItem;
    }
}
