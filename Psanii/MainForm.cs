using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Psanii
{
    public partial class MainForm : Form
    {
        string strToChange = "scrzydtnaieouSCRZYDTNAIEOU";
        string  strChanged = "ščřžýďťňáíéóůŠČŘŽÝĎŤŇÁÍÉÓŮ";
        string strESpecial = "ďťňĎŤŇ";
        string strEChanged = "dtnDTN";
        bool blPsanii = true;
        System.IO.Stream imgNoAction;
        System.IO.Stream imgOK;
        string strDocument = "Untitled";
        string strDocPath = "";
        bool blDocChanged = false;
        bool blShiftPressed = false;
        const string PRODUCT_NAME = "Psanii Notepad";

        public MainForm()
        {
            InitializeComponent();
            //this.editToolStripMenuItem.DropDown = this.contextEditMenuStrip;
        }

        private string ReadFile(string file)
        {
            StreamReader reader = new StreamReader(file, Encoding.Default);
            string data = reader.ReadToEnd();
            reader.Close();

            return data;
        }

        private void SaveFile(string file, string data)
        {
            StreamWriter writer = new StreamWriter(file);
            writer.Write(data);
            writer.Close();
        }



        private void ResizetxtMain()
        {
            //if (statusStrip1.Visible)
            //{
            txtMain.Height = this.ClientSize.Height - txtMain.Top - (statusStrip1.Visible ? statusStrip1.Height : 0) - ((pnlGoTo.Visible || pnlSearch.Visible) ? pnlGoTo.Height : 0);
            /*}
            else
            {
                txtMain.Height = this.ClientSize.Height - txtMain.Top;
            }*/
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (autoIndentToolStripMenuItem.Checked && e.KeyChar == 13 && (txtMain.Text.Substring(txtMain.GetFirstCharIndexOfCurrentLine(), 1) == " " || txtMain.Text.Substring(txtMain.GetFirstCharIndexOfCurrentLine(), 1) == "\t")) // Enter
                {
                    //txtMain.Text += Convert.ToInt16(e.KeyChar).ToString();
                    int j = txtMain.GetFirstCharIndexOfCurrentLine();
                    int i = 1;
                    try
                    {
                        while (txtMain.Text.Substring(j + i, 1) == " " || txtMain.Text.Substring(j + i, 1) == "\t")
                            i++;
                    }
                    catch
                    {
                        // O.o
                    }

                    txtMain.SelectedText = "\r\n" + txtMain.Text.Substring(j, i);
                    
                    e.Handled = true;
                    return;
                }
            }
            catch
            {
                // Watt
            }


            if (spacesForTabsToolStripMenuItem.Checked && e.KeyChar == 9) // Tab
            {
                //txtMain.Text += Convert.ToInt16(e.KeyChar).ToString();
                if (blShiftPressed)
                {
                    try
                    {
                        if (txtMain.Text.Substring(txtMain.SelectionStart - 2, 2) == "  ")
                        {
                            txtMain.SelectionStart -= 2;
                            txtMain.SelectionLength += 2;
                            txtMain.SelectedText = txtMain.SelectedText.Substring(2);
                        }
                    }
                    catch
                    {
                        // Watt
                    }
                }
                else
                {
                    txtMain.SelectedText = "  ";
                }
                e.Handled = true;
                return;
            }

            if (blPsanii)
            {
                try
                {
                    if (e.KeyChar == 8 && txtMain.SelectionLength == 0)
                    {
                        if (txtMain.Text.Substring(txtMain.SelectionStart - 1, 1).ToLower() == "ě")
                        {
                            int intFound = strToChange.IndexOf(txtMain.Text.Substring(txtMain.SelectionStart - 2, 1));
                            if (intFound > -1)
                            {
                                int intSel = txtMain.SelectionStart;
                                txtMain.Text = txtMain.Text.Remove(txtMain.SelectionStart - 2, 2).Insert(txtMain.SelectionStart - 2, strChanged.Substring(intFound, 1));
                                txtMain.SelectionStart = intSel - 1;
                                e.Handled = true;
                            }
                            else
                            {
                                int intSel = txtMain.SelectionStart;
                                string strTemp = txtMain.Text.Substring(txtMain.SelectionStart - 2, 1);
                                txtMain.Text = txtMain.Text.Remove(txtMain.SelectionStart - 2, 2).Insert(txtMain.SelectionStart - 2, strTemp + strTemp);
                                txtMain.SelectionStart = intSel;
                                e.Handled = true;
                            }
                        }
                        else
                        {
                            int intFound = strChanged.IndexOf(txtMain.Text.Substring(txtMain.SelectionStart - 1, 1));
                            if (intFound > -1)
                            {
                                int intSel = txtMain.SelectionStart;
                                txtMain.Text = txtMain.Text.Remove(txtMain.SelectionStart - 1, 1).Insert(txtMain.SelectionStart - 1, strToChange.Substring(intFound, 1));
                                txtMain.SelectionStart = intSel;
                                e.Handled = true;
                            }
                        }
                    }
                    else if (e.KeyChar.ToString() == txtMain.Text.Substring(txtMain.SelectionStart - 1, 1))
                    {
                        int intFound = strToChange.IndexOf(e.KeyChar);
                        if (intFound > -1)
                        {
                            int intSel = txtMain.SelectionStart;
                            txtMain.Text = txtMain.Text.Remove(txtMain.SelectionStart - 1, 1).Insert(txtMain.SelectionStart - 1, strChanged.Substring(intFound, 1));
                            txtMain.SelectionStart = intSel;
                            e.Handled = true;
                        }
                    }
                    else if (e.KeyChar == 'u' && txtMain.Text.Substring(txtMain.SelectionStart - 1, 1) == "ů")
                    {
                        int intSel = txtMain.SelectionStart;
                        txtMain.Text = txtMain.Text.Remove(txtMain.SelectionStart - 1, 1).Insert(txtMain.SelectionStart - 1, "ú");
                        txtMain.SelectionStart = intSel + 1;
                        e.Handled = true;
                    }
                    else if (e.KeyChar.ToString().ToLower() == "u" && txtMain.Text.Substring(txtMain.SelectionStart - 1, 1) == "Ů")
                    {
                        int intSel = txtMain.SelectionStart;
                        txtMain.Text = txtMain.Text.Remove(txtMain.SelectionStart - 1, 1).Insert(txtMain.SelectionStart - 1, "Ú");
                        txtMain.SelectionStart = intSel + 1;
                        e.Handled = true;
                    }
                    else if (e.KeyChar.ToString().ToLower() == "e")
                    {
                        int intFound = strESpecial.IndexOf(txtMain.Text.Substring(txtMain.SelectionStart - 1, 1));
                        if (intFound > -1)
                        {
                            int intSel = txtMain.SelectionStart;
                            txtMain.Text = txtMain.Text.Remove(txtMain.SelectionStart - 1, 1).Insert(txtMain.SelectionStart - 1, strEChanged.Substring(intFound, 1) + (e.KeyChar == 'e' ? "ě" : "Ě"));
                            txtMain.SelectionStart = intSel + 1;
                            e.Handled = true;
                        }
                        else if (txtMain.Text.Substring(txtMain.SelectionStart - 1, 1) == txtMain.Text.Substring(txtMain.SelectionStart - 2, 1))
                        {
                            int intSel = txtMain.SelectionStart;
                            txtMain.Text = txtMain.Text.Remove(txtMain.SelectionStart - 1, 1).Insert(txtMain.SelectionStart - 1, (e.KeyChar == 'e' ? "ě" : "Ě"));
                            txtMain.SelectionStart = intSel + 1;
                            e.Handled = true;
                        }
                    }
                }
                catch
                {
                    // Watt
                }
            }
        }

        private void sttPsanii_Click(object sender, EventArgs e)
        {
            blPsanii = !blPsanii;
            if (blPsanii)
            {
                sttPsanii.Image = Image.FromStream(imgOK);
                sttPsanii.Text = "ON";
            }
            else
            {
                sttPsanii.Image = Image.FromStream(imgNoAction);
                sttPsanii.Text = "OFF";
            }
            psaniiToolStripMenuItem.Checked = blPsanii;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            imgNoAction = this.GetType().Assembly.GetManifestResourceStream("Psanii.Resources.NoAction.bmp");
            imgOK = this.GetType().Assembly.GetManifestResourceStream("Psanii.Resources.OK.bmp");

            if (Psanii.Properties.Settings.Default.MainForm_Location.X != -1 && Psanii.Properties.Settings.Default.MainForm_Location.Y != -1)
            {
                this.Location = Psanii.Properties.Settings.Default.MainForm_Location;
                this.Size = Psanii.Properties.Settings.Default.MainForm_Size;
            }
            this.WindowState = Psanii.Properties.Settings.Default.MainForm_WindowState;

            if (Psanii.Properties.Settings.Default.MainForm_StatusBar == false)
            {
                statusBarToolStripMenuItem.Checked = Psanii.Properties.Settings.Default.MainForm_StatusBar;
                statusBarToolStripMenuItem_Click(null, null);
            }

            if (Psanii.Properties.Settings.Default.MainForm_Psanii == false)
                sttPsanii_Click(null, null);

            txtMain.Font = Psanii.Properties.Settings.Default.MainForm_Font;
            fontDialog1.Font = Psanii.Properties.Settings.Default.MainForm_Font;
            txtMain.ForeColor = Psanii.Properties.Settings.Default.MainForm_FontColor;
            fontDialog1.Color = Psanii.Properties.Settings.Default.MainForm_FontColor;

            if (Psanii.Properties.Settings.Default.MainForm_SpaceTabs)
                spacesForTabsToolStripMenuItem_Click(null, null);

            psaniierasingToolStripMenuItem.Checked = Psanii.Properties.Settings.Default.MainForm_PsaniiErasing;
            autoIndentToolStripMenuItem.Checked = Psanii.Properties.Settings.Default.MainForm_AutoIndent;

            //textBox1.Text = strToChange.ToUpper() + " " + strChanged.ToUpper();

            
        }

        private void statusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip1.Visible = statusBarToolStripMenuItem.Checked;

            ResizetxtMain();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (blDocChanged == false) {
                blDocChanged = true;
                this.Text = strDocument + "* - " + PRODUCT_NAME;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (CheckForSave() == false)
            {
                e.Cancel = true;
                return;
            }

            Psanii.Properties.Settings.Default.MainForm_WindowState = this.WindowState;
            if (this.WindowState != FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Normal;
            }
            Psanii.Properties.Settings.Default.MainForm_Location = this.Location;
            Psanii.Properties.Settings.Default.MainForm_Size = this.Size;
            Psanii.Properties.Settings.Default.MainForm_Psanii = blPsanii;
            Psanii.Properties.Settings.Default.MainForm_StatusBar = statusStrip1.Visible;
            Psanii.Properties.Settings.Default.MainForm_Font = txtMain.Font;
            Psanii.Properties.Settings.Default.MainForm_FontColor = txtMain.ForeColor;
            Psanii.Properties.Settings.Default.MainForm_SpaceTabs = spacesForTabsToolStripMenuItem.Checked;
            Psanii.Properties.Settings.Default.MainForm_PsaniiErasing = psaniierasingToolStripMenuItem.Checked;
            Psanii.Properties.Settings.Default.MainForm_AutoIndent = autoIndentToolStripMenuItem.Checked;

            Psanii.Properties.Settings.Default.Save();
        }

        private void lineBreakToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtMain.WordWrap = lineBreakToolStripMenuItem.Checked;
            if (lineBreakToolStripMenuItem.Checked)
            {
                txtMain.ScrollBars = ScrollBars.Vertical;
            }
            else
            {
                txtMain.ScrollBars = ScrollBars.Both;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txtGoTo.Focused)
            {
                txtGoTo.SelectionStart = 0;
                txtGoTo.SelectionLength = txtGoTo.Text.Length;
            }
            else if (txtColumn.Focused) {
                txtColumn.SelectionStart = 0;
                txtColumn.SelectionLength = txtColumn.Text.Length;
            }
            else if (txtFind.Focused)
            {
                txtFind.SelectionStart = 0;
                txtFind.SelectionLength = txtFind.Text.Length;
            }
            else if (txtReplace.Focused)
            {
                txtReplace.SelectionStart = 0;
                txtReplace.SelectionLength = txtReplace.Text.Length;
            }
            else
            {
                txtMain.SelectionStart = 0;
                txtMain.SelectionLength = txtMain.Text.Length;
            }
        }

        private void timeAndDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtMain.SelectedText = DateTime.Now.ToString();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtMain.Undo();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtMain.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtMain.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtMain.Paste();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtMain.SelectedText = "";
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            sttLine.Text = "Ln " + (txtMain.GetLineFromCharIndex(txtMain.SelectionStart) + 1) + ", Col " + (txtMain.SelectionStart - txtMain.GetFirstCharIndexOfCurrentLine() + 1);
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey)
            {
                blShiftPressed = false;
                //this.Text = "Unshift";
            }

            textBox1_Click(null, null);
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
                fontDialog1_Apply(null, null);
        }

        private void fontDialog1_Apply(object sender, EventArgs e)
        {
            txtMain.Font = fontDialog1.Font;
            txtMain.ForeColor = fontDialog1.Color;
        }

        private void oDSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sttCoding.Text = oDSToolStripMenuItem.Text;
        }

        private void macToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sttCoding.Text = macToolStripMenuItem.Text;
        }

        private void unixToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sttCoding.Text = unixToolStripMenuItem.Text;
        }

        private void goToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pnlGoTo.Visible && txtMain.Focused == false)
            {
                pnlGoTo.Visible = false;
                txtMain.Focus();
            }
            else
            {
                pnlGoTo.Visible = true;
                txtGoTo.Focus();
                selectAllToolStripMenuItem_Click(null, null);
            }

            pnlSearch.Visible = false;
            ResizetxtMain();
        }

        private void txtGoTo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnGoTo_Click(null, null);
            }
            else if (e.KeyChar == 27)
            {
                goToToolStripMenuItem_Click(null, null);
            }
            else if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != 8)
            {
                //txtMain.Text += Convert.ToInt16(e.KeyChar).ToString();
                e.Handled = true;
                // Beep
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAboutBox frmAbout = new frmAboutBox();
            frmAbout.ShowDialog(this);
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pnlSearch.Visible && txtMain.Focused == false)
            {
                pnlSearch.Visible = false;
                txtMain.Focus();
            }
            else
            {
                pnlSearch.Visible = true;
                txtFind.Focus();
                selectAllToolStripMenuItem_Click(null, null);
            }

            txtReplace.Visible = false;
            label3.Visible = false;
            pnlGoTo.Visible = false;
            ResizetxtMain();
        }

        private void findToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                txtMain.SelectionStart = txtMain.Text.IndexOf(txtFind.Text, txtMain.SelectionStart + txtMain.SelectionLength);
                txtMain.SelectionLength = txtFind.Text.Length;
                txtMain.ScrollToCaret();
                txtMain.Focus();
            }
            catch
            {
                // Beep
            }
        }

        private void findPreviousToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                txtMain.SelectionStart = txtMain.Text.LastIndexOf(txtFind.Text, txtMain.SelectionStart - 1);
                txtMain.SelectionLength = txtFind.Text.Length;
                txtMain.ScrollToCaret();
                txtMain.Focus();
            }
            catch
            {
                // Beep
            }
        }

        private bool CheckForSave()
        {
            if (blDocChanged && !(txtMain.Text == "" && strDocPath == ""))
            {
                switch (MessageBox.Show("File " + strDocument + " not saved.\n\nDo you wish to save?", this.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                    case DialogResult.Yes:
                        saveToolStripMenuItem_Click(null, null);
                        break;
                    case DialogResult.Cancel:
                        return false;
                }
            }
            return true;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CheckForSave())
            {
                strDocument = "Untitled";
                strDocPath = "";
                txtMain.Text = "";
                blDocChanged = false;

                this.Text = strDocument + " - " + PRODUCT_NAME;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (strDocPath != "")
            {
                //strDocPath = saveFileDialog1.FileName;
                //strDocument = System.IO.Path.GetFileName(strDocPath);
                SaveFile(strDocPath, txtMain.Text);

                blDocChanged = false;

                this.Text = strDocument + " - " + PRODUCT_NAME;
            }
            else
            {
                saveAsToolStripMenuItem_Click(null, null);
            }
        }

        private void OpenFile(string file)
        {
            txtMain.Text = ReadFile(file);
            strDocPath = file;
            strDocument = System.IO.Path.GetFileName(strDocPath);
            if (txtMain.Text.Substring(0, 4) == ".LOG")
            {
                txtMain.SelectionStart = txtMain.Text.Length;
                txtMain.SelectedText = "\r\n\r\n" + DateTime.Now.ToString() + "\r\n";
                txtMain.ScrollToCaret();
            }

            blDocChanged = false;

            this.Text = strDocument + " - " + PRODUCT_NAME;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CheckForSave())
            {
                openFileDialog1.FileName = strDocPath;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    OpenFile(openFileDialog1.FileName);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                strDocPath = saveFileDialog1.FileName;
                strDocument = System.IO.Path.GetFileName(strDocPath);
                SaveFile(strDocPath, txtMain.Text);

                blDocChanged = false;

                this.Text = strDocument + " - " + PRODUCT_NAME;
            }
        }

        private void txtMain_DragDrop(object sender, DragEventArgs e)
        {
            if (CheckForSave())
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                OpenFile(files[0]);
            }
        }

        private void txtMain_DragEnter(object sender, DragEventArgs e)
        {
            // make sure they're actually dropping files (not text or anything else)
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
                // allow them to continue
                // (without this, the cursor stays a "NO" symbol
                e.Effect = DragDropEffects.All;

        }

        private void btnGoTo_Click(object sender, EventArgs e)
        {
            try
            {
                txtMain.SelectionStart = txtMain.GetFirstCharIndexFromLine(Convert.ToInt32(txtGoTo.Text) - 1) + Convert.ToInt32(txtColumn.Text) - 1;
                txtMain.SelectionLength = 0;
                txtMain.ScrollToCaret();
                goToToolStripMenuItem_Click(null, null);
            }
            catch
            {
                // Beep
            }
        }

        private void spacesForTabsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            spacesForTabsToolStripMenuItem.Checked = !spacesForTabsToolStripMenuItem.Checked;
            if (spacesForTabsToolStripMenuItem.Checked)
                tabStripStatusLabel1.Text = "SPACE";
            else
                tabStripStatusLabel1.Text = "TAB";
        }

        private void txtMain_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.Shift == true)
            if (e.KeyCode == Keys.ShiftKey)
            {
                blShiftPressed = true;
                //this.Text = "Shift";
            }
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printDialog1.ShowDialog();
        }

        private void pageSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pageSetupDialog1.ShowDialog();
        }
    }
}
