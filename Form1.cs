using System.Configuration;
using System.Diagnostics;
using System.Drawing; // Added for dark theme colors
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms; // Added for DataGridView styles
// --- ADDED for Renderer ---
using System.Drawing.Drawing2D;
// --- END ADD ---

namespace hlm2_localization_editor
{
    public partial class Form1 : Form
    {
        public struct Language
        {
            public uint ID;
            public uint NameLength;
            public string Name;
            public uint Unk0;
            public uint EntryCount;
            public uint[] Entries;
            public string[] Strings;
        }
        public struct LocDataTable
        {
            public uint LangCount;
            public Language[] Languages;

        }
        LocDataTable LocData = new LocDataTable();
        public string LocFile = string.Empty;

        public Form1()
        {
            InitializeComponent();
            dataGridView1.Columns.Clear();
            ApplyDarkThemeToGrid(); // Apply advanced dark theme styles
        }

        // FUNCTION to apply dark theme to the DataGridView
        private void ApplyDarkThemeToGrid()
        {
            var darkBg = System.Drawing.ColorTranslator.FromHtml("#242424");
            var lightFg = System.Drawing.Color.Gainsboro;
            var midBg = System.Drawing.ColorTranslator.FromHtml("#333333");
            var headerBg = System.Drawing.ColorTranslator.FromHtml("#1a1a1a");
            
            // --- YOUR NEW COLOR ---
            var highlightColor = System.Drawing.ColorTranslator.FromHtml("#75ff1f"); // <-- NEW COLOR
            var highlightTextColor = System.Drawing.Color.Black; // Black text on green bg
            // --- END ---

            // General grid appearance
            dataGridView1.BackgroundColor = darkBg;
            dataGridView1.GridColor = midBg;
            dataGridView1.BorderStyle = BorderStyle.None;

            // Column Headers
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = headerBg;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = lightFg;
            dataGridView1.ColumnHeadersDefaultCellStyle.SelectionBackColor = highlightColor;
            dataGridView1.ColumnHeadersDefaultCellStyle.SelectionForeColor = highlightTextColor;
            dataGridView1.EnableHeadersVisualStyles = false; // Important!

            // Row Headers (the column on the far left)
            dataGridView1.RowHeadersDefaultCellStyle.BackColor = headerBg;
            dataGridView1.RowHeadersDefaultCellStyle.ForeColor = lightFg;
            dataGridView1.RowHeadersDefaultCellStyle.SelectionBackColor = highlightColor;
            dataGridView1.RowHeadersDefaultCellStyle.SelectionForeColor = highlightTextColor;

            // Default Cell Style (for all cells)
            dataGridView1.DefaultCellStyle.BackColor = darkBg;
            dataGridView1.DefaultCellStyle.ForeColor = lightFg;
            dataGridView1.DefaultCellStyle.SelectionBackColor = highlightColor;
            dataGridView1.DefaultCellStyle.SelectionForeColor = highlightTextColor;

            // Alternating Row Style (optional, but good for readability)
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = midBg;
            dataGridView1.AlternatingRowsDefaultCellStyle.ForeColor = lightFg;
            dataGridView1.AlternatingRowsDefaultCellStyle.SelectionBackColor = highlightColor;
            dataGridView1.AlternatingRowsDefaultCellStyle.SelectionForeColor = highlightTextColor;
        }


        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            var fileContent = string.Empty;
            var filePath = string.Empty;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "hlm2_localization.bin (*.bin)|*.bin";
            openFileDialog.FilterIndex = 0;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //Get the path of specified file
                filePath = openFileDialog.FileName;
                LocFile = openFileDialog.FileName;

                //Read the contents of the file into a stream
                var fileStream = openFileDialog.OpenFile();

                // Use UTF-8 encoding for the reader, but we'll read bytes manually for strings
                using (var reader = new BinaryReader(fileStream, Encoding.UTF8))
                {
                    LocData.LangCount = reader.ReadUInt32();
                    LocData.Languages = new Language[LocData.LangCount];

                    for (int l = 0; l < LocData.LangCount; l++)
                    {
                        var Lang = new Language();

                        Lang.ID = reader.ReadUInt32();
                        Lang.NameLength = reader.ReadUInt32();

                        // Read the name as a byte array and convert from UTF-8
                        byte[] nameBytes = reader.ReadBytes((int)Lang.NameLength);
                        Lang.Name = Encoding.UTF8.GetString(nameBytes);

                        Lang.Unk0 = reader.ReadUInt32();
                        Lang.EntryCount = reader.ReadUInt32();
                        Lang.Entries = new uint[Lang.EntryCount + 1];
                        Lang.Strings = new string[Lang.EntryCount];
                        for (int i = 0; i < Lang.EntryCount + 1; i++)
                        {
                            Lang.Entries[i] = reader.ReadUInt32();
                        }
                        for (int i = 0; i < Lang.EntryCount; i++)
                        {
                            // Use helper function to read strings
                            Lang.Strings[i] = ReadNullTerminatedString(reader);
                        }
                        LocData.Languages[l] = Lang;
                    }

                }

                // Add columns for all languages
                for (int i = 0; i < LocData.LangCount; i++)
                {
                    Language lang = LocData.Languages[i];
                    dataGridView1.Columns.Add(lang.ID.ToString(), lang.Name);
                    dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                
                // Set column 0 (String IDs) to read-only
                if (dataGridView1.Columns.Count > 0)
                {
                    dataGridView1.Columns[0].ReadOnly = true; 
                }

                // Populate rows
                for (int i = 0; i < LocData.Languages[0].Strings.Length; i++)
                {
                    List<string> strs = new List<string>();
                    
                    // Add all localized strings
                    for (int j = 0; j < LocData.LangCount; j++)
                    {
                        strs.Add(LocData.Languages[j].Strings[i]);
                    }
                    
                    int rowIndex = dataGridView1.Rows.Add(strs.ToArray());
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
            Application.Exit();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Columns.Count == 0)
            {
                MessageBox.Show("No file.", "Error", MessageBoxButtons.OK);
                return;
            }
            SaveFile();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Columns.Count == 0)
            {
                MessageBox.Show("No file.", "Error", MessageBoxButtons.OK);
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "bin files (*.bin)|*.bin";
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                SaveFile(saveFileDialog.OpenFile());
            }

        }

        void SaveFile(Stream? stream = null)
        {
            // Applying changes: Update LocData with current DataGridView contents
            // This is the critical sync step before saving.
            for (int i = 0; i < LocData.Languages[0].Strings.Length; i++)
            {
                for (int j = 0; j < LocData.LangCount; j++)
                {
                    // Read the visible text directly from the DataGridView cell
                    LocData.Languages[j].Strings[i] = (dataGridView1.Rows[i].Cells[j].Value ?? string.Empty).ToString()!;
                }
            }


            // Saving

            Stream? saveStream = null;
            if (stream != null)
            {
                saveStream = stream;
            }
            else
            {
                saveStream = File.Open(LocFile, FileMode.Create);
            }

            {
                // Use UTF-8 encoding for the writer
                using (var writer = new BinaryWriter(saveStream, Encoding.UTF8))
                {
                    writer.Write(LocData.LangCount);
                    for (int i = 0; i < LocData.LangCount; i++)
                    {
                        Language Lang = LocData.Languages[i];
                        writer.Write(Lang.ID);

                        // Write the correct NameLength in bytes
                        byte[] nameUtf8Bytes = Encoding.UTF8.GetBytes(Lang.Name);
                        writer.Write((uint)nameUtf8Bytes.Length);

                        // Write the name bytes all at once
                        writer.Write(nameUtf8Bytes);

                        writer.Write(Lang.Unk0);
                        writer.Write(Lang.EntryCount);
                        uint dataPos = 0;
                        writer.Write(dataPos); // Write initial 0 offset

                        // This loop calculates the offsets
                        for (int j = 0; j < Lang.Strings.Length; j++)
                        {
                            byte[] utf8Bytes = Encoding.UTF8.GetBytes(Lang.Strings[j]);

                            // Add string length + 1 (for the null terminator) to the offset
                            dataPos += Convert.ToUInt32(utf8Bytes.Length + 1);
                            writer.Write(dataPos);
                        }

                        // This loop writes the actual string data
                        for (int j = 0; j < Lang.Strings.Length; j++)
                        {
                            byte[] utf8Bytes = Encoding.UTF8.GetBytes(Lang.Strings[j]);

                            // Write the string bytes all at once
                            writer.Write(utf8Bytes);

                            // Write the null terminator (0x00) manually
                            writer.Write((byte)0x00);
                        }
                    }
                }
            }
            saveStream.Close();
            MessageBox.Show("File Saved!", "Success!", MessageBoxButtons.OK);
        }

// ===================== FILE I/O HELPERS & OTHER MENU ACTIONS =====================

        /// <summary>
        /// Reads a UTF-8 encoded, null-terminated string from the binary reader.
        /// </summary>
        private string ReadNullTerminatedString(BinaryReader reader)
        {
            List<byte> byteList = new List<byte>();
            byte currentByte;
            while ((currentByte = reader.ReadByte()) != 0x00) // Read bytes until we hit a null (0)
            {
                byteList.Add(currentByte);
            }
            return Encoding.UTF8.GetString(byteList.ToArray()); // Convert all bytes at once to a UTF-8 string
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // GitHub fork link
            System.Diagnostics.Process.Start(new ProcessStartInfo("https://github.com/PHM2D/HM2WN-Localization-Editor") { UseShellExecute = true });
        }

        // Standard multi-column export ("Export All to TXT...")
        private void exportToTxtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("No data to export.", "Error", MessageBoxButtons.OK);
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.txt)|*.txt";
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName, false, Encoding.UTF8))
                {
                    // Write header (column names)
                    for (int i = 0; i < LocData.LangCount; i++) // Only original languages
                    {
                        writer.Write(dataGridView1.Columns[i].HeaderText);
                        if (i < LocData.LangCount - 1)
                            writer.Write('\t');
                    }
                    writer.WriteLine();

                    // Write each row
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        for (int i = 0; i < LocData.LangCount; i++) // Only original languages
                        {
                            string value = row.Cells[i].Value?.ToString()?.Replace("\0", "") ?? "";
                            value = value.Replace('\t', ' '); // remove tabs inside text
                            value = value.Replace('\r', ' ').Replace('\n', ' '); // remove newlines
                            writer.Write(value);
                            if (i < LocData.LangCount - 1)
                                writer.Write('\t');
                        }
                        writer.WriteLine();
                    }
                }

                MessageBox.Show("Exported successfully!", "Success", MessageBoxButtons.OK);
            }
        }

        // Standard multi-column import ("Import All from TXT...")
        private void importFromTxtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Columns.Count == 0)
            {
                MessageBox.Show("Open a localization file first.", "Error", MessageBoxButtons.OK);
                return;
            }

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt";
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string[] lines = File.ReadAllLines(openFileDialog.FileName, Encoding.UTF8);

                if (lines.Length < 2)
                {
                    MessageBox.Show("Invalid TXT format. Expected header and at least one row.", "Error", MessageBoxButtons.OK);
                    return;
                }

                // Skip header (i=0)
                for (int i = 1; i < lines.Length && i - 1 < dataGridView1.Rows.Count; i++)
                {
                    string[] parts = lines[i].Split('\t');
                    for (int j = 0; j < parts.Length && j < LocData.LangCount; j++)
                    {
                        // Note: This import logic assumes the imported file has the same columns
                        // and order as the currently loaded file.
                        dataGridView1.Rows[i - 1].Cells[j].Value = parts[j];
                    }
                }

                MessageBox.Show("Imported successfully!", "Success", MessageBoxButtons.OK);
            }
        }

// ===================== TXT EXPORT / IMPORT (CLEAN ENGLISH) =====================

        // This is the function for "Export Pure Text..." (English)
        private void exportEnglishToTxtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // English is in column index 1
            ExportPureLanguage(1, "English");
        }

        // This is the function for "Import Pure Text..." (English)
        private void importEnglishFromTxtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // English is in column index 1
            ImportPureLanguage(1, "English");
        }

// ===================== TXT EXPORT / IMPORT (CLEAN RUSSIAN) =====================

        // NEW FUNCTION for "Export Pure Text..." (Russian)
        private void exportRussianToTxtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Russian is in column index 6
            ExportPureLanguage(6, "Russian");
        }

        // NEW FUNCTION for "Import Pure Text..." (Russian)
        private void importRussianFromTxtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Russian is in column index 6
            ImportPureLanguage(6, "Russian");
        }


// ===================== GENERIC HELPER FUNCTIONS FOR PURE TXT I/O =====================

        // NEW GENERIC FUNCTION to export any language by its column index
        private void ExportPureLanguage(int columnIndex, string languageName)
        {
            // Check if data is loaded and has enough languages
            if (LocData.Languages == null || LocData.Languages.Length <= columnIndex)
            {
                MessageBox.Show($"No localization file loaded, or the file does not contain a {languageName} column (expected at index {columnIndex}).", "Error", MessageBoxButtons.OK);
                return;
            }
            
            // Read from the specified language index
            string[] strings = LocData.Languages[columnIndex].Strings;

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.txt)|*.txt";
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName, false, Encoding.UTF8))
                {
                    // NO HEADER IS WRITTEN
                    for (int i = 0; i < strings.Length; i++)
                    {
                        string value = strings[i];
                        
                        // Clean up the string for export
                        value = value.Replace("\0", "")
                                     .Replace('\t', ' ')
                                     .Replace('\r', ' ')
                                     .Replace('\n', ' ');

                        // Write ONLY the string value, one per line
                        writer.WriteLine(value);
                    }
                }
                MessageBox.Show($"Pure {languageName} strings exported successfully!", "Success", MessageBoxButtons.OK);
            }
        }

        // NEW GENERIC FUNCTION to import any language by its column index
        private void ImportPureLanguage(int columnIndex, string languageName)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Open a localization file first.", "Error", MessageBoxButtons.OK);
                return;
            }

            if (LocData.Languages == null || LocData.Languages.Length <= columnIndex)
            {
                MessageBox.Show($"No localization file loaded, or the file does not contain a {languageName} column (expected at index {columnIndex}).", "Error", MessageBoxButtons.OK);
                return;
            }

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt";
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string[] lines = File.ReadAllLines(openFileDialog.FileName, Encoding.UTF8);

                if (lines.Length == 0)
                {
                    MessageBox.Show("The imported file is empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Check for line count mismatch
                if (lines.Length != dataGridView1.Rows.Count)
                {
                    MessageBox.Show($"Line count mismatch. The file has {lines.Length} lines, but the project requires {dataGridView1.Rows.Count} lines. Please use the correct file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                for (int i = 0; i < lines.Length; i++)
                {
                    int rowIndex = i; 
                    
                    // Update the specified language column with the new line content
                    dataGridView1.Rows[rowIndex].Cells[columnIndex].Value = lines[i];
                }

                MessageBox.Show($"{languageName} strings imported successfully! Don't forget to click Save to write changes to the .bin file.", "Success", MessageBoxButtons.OK);
            }
        }
    } // --- END OF Form1 CLASS ---


    // ===================== CUSTOM TOOLSTRIP RENDERER FOR DARK THEME =====================
    // This class manually draws the menu to override the default blue highlight
    public class MyToolStripRenderer : ToolStripProfessionalRenderer
    {
        private SolidBrush menuHighlightBrush;
        private Color menuHighlightTextColor;
        private Color menuBg;
        private Color menuFg;
        private Color topMenuBg; // Background for "File", "About"

        public MyToolStripRenderer(Color highlightColor, Color highlightTextColor, Color dropdownBg, Color fg, Color topLevelBg)
            : base(new MyColors(highlightColor, dropdownBg))
        {
            menuHighlightBrush = new SolidBrush(highlightColor);
            menuHighlightTextColor = highlightTextColor;
            menuBg = dropdownBg; // #242424
            menuFg = fg; // Gainsboro
            topMenuBg = topLevelBg; // #1a1a1a
        }

        // THIS FUNCTION IS NOW FIXED TO HANDLE DROPDOWNS
        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            Rectangle rc = new Rectangle(Point.Empty, e.Item.Size);

            if (e.Item.OwnerItem == null) // This is a Top-Level item ("File", "About")
            {
                if (e.Item.Selected)
                {
                    // Draw GREEN background for selected top-level
                    e.Graphics.FillRectangle(menuHighlightBrush, rc);
                    e.Item.ForeColor = menuHighlightTextColor;
                }
                else
                {
                    // Draw HEADER background for unselected top-level
                    e.Graphics.FillRectangle(new SolidBrush(topMenuBg), rc);
                    e.Item.ForeColor = menuFg;
                }
            }
            else // This is a Dropdown item ("Open", "Save", etc.)
            {
                if (e.Item.Selected)
                {
                    // Draw GREEN background for selected dropdown items
                    e.Graphics.FillRectangle(menuHighlightBrush, rc);
                    e.Item.ForeColor = menuHighlightTextColor;
                }
                else
                {
                    // Draw DROPDOWN background for unselected dropdown items
                    e.Graphics.FillRectangle(new SolidBrush(menuBg), rc);
                    e.Item.ForeColor = menuFg;
                }
            }
        }
        
        // Background for the dropdown menu itself
        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            if (e.ToolStrip is ToolStripDropDown)
            {
                 if (e.AffectedBounds.Width > 0 && e.AffectedBounds.Height > 0)
                 {
                      e.Graphics.Clear(menuBg);
                 }
                 else
                 {
                     base.OnRenderToolStripBackground(e);
                 }
            }
            else
            {
                base.OnRenderToolStripBackground(e);
            }
        }

        // Ensure text color is correct for dropdown items
        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            if (e.Item.OwnerItem != null) // Dropdown item
            {
                // Text color is set by OnRenderMenuItemBackground, so just call base
                 base.OnRenderItemText(e);
            }
            else
            {
                // Top-level item text color
                e.TextColor = e.Item.Selected ? menuHighlightTextColor : menuFg;
                base.OnRenderItemText(e);
            }
        }
    }

    // This helper class provides the colors to the base renderer
    public class MyColors : ProfessionalColorTable
    {
        private Color highlightColor;
        private Color menuBg;

        public MyColors(Color highlight, Color menuStripBg)
        {
            highlightColor = highlight;
            menuBg = menuStripBg;
        }

        // This is the one that was failing. We now override it with manual drawing.
        public override Color MenuItemSelected
        {
            get { return highlightColor; } 
        }
        
        // This sets the border color
        public override Color MenuItemBorder
        {
            get { return highlightColor; }
        }
        
        // Other overrides for a consistent dark theme
        public override Color ToolStripDropDownBackground
        {
            get { return menuBg; }
        }
        public override Color ImageMarginGradientBegin
        {
             get { return menuBg; }
        }
        public override Color ImageMarginGradientMiddle
        {
             get { return menuBg; }
        }
        public override Color ImageMarginGradientEnd
        {
             get { return menuBg; }
        }
        public override Color MenuBorder
        {
            get { return System.Drawing.ColorTranslator.FromHtml("#333333"); }
        }
        public override Color SeparatorDark
        {
            get { return System.Drawing.ColorTranslator.FromHtml("#333333"); }
        }
        public override Color SeparatorLight
        {
            get { return System.Drawing.ColorTranslator.FromHtml("#333333"); }
        }
    }
}
