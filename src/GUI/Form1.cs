using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;
using System.Data.Common;
using System.Diagnostics;
using System.Security.Policy;
using System.Drawing.Drawing2D;

namespace GUI
{
    public partial class Form1 : Form
    {
        private string fileName;
        private string[] fileContents;
        private Boolean validFile = false;
        public Form1()
        {
            InitializeComponent();

            // To make it unmaximizable
            this.MaximizeBox = false;

            // To make it unresizable
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            // To make it appear at the center of the screen
            this.StartPosition = FormStartPosition.CenterScreen;

            // To change GUI name
            this.Text = "Treasure Hunt Solver";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            if (!validFile)
            {
                MessageBox.Show("File is not valid, please input another file!");
                dataGridView1.Visible = false;
                button3.Enabled = false;
            } 
            else
            {
                MessageBox.Show("The map from your file has been visualized! :)");
                // To make the visualization visible
                dataGridView1.Visible = true;

                // To make the search button enabled
                button3.Enabled = true;
            }  
        }

        

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                // Enable visualize button
                button1.Enabled = true;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                // Enable visualize button
                button1.Enabled = true;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Create a new instance of the OpenFileDialog class
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            // Set the file filter and initial directory for the dialog
            openFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // Show the dialog and check if the user clicked OK
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Update the text box with the selected file path
                fileName = openFileDialog1.FileName;
                textBox1.Text = openFileDialog1.FileName;

                // Read the contents of the selected file into a string variable
                fileContents = File.ReadAllLines(openFileDialog1.FileName);
                dataGridView1_CellContentClick(null, null);

                if (fileContents.Length > 0)
                {
                    radioButton1.Enabled = true;
                    radioButton2.Enabled = true;
                }
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Get the number of rows and columns
            int rows = fileContents.Length;
            int cols = 0;

            foreach (string line in fileContents)
            {
                int nonBlankChars = line.Count(c => !char.IsWhiteSpace(c));
                if (nonBlankChars > cols)
                {
                    cols = nonBlankChars;
                }
            }

            // Set up the DataGridView
            dataGridView1.RowCount = rows;
            dataGridView1.ColumnCount = cols;

            // To make Row Headers and Column Headers not visible
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ColumnHeadersVisible = false;

            // Divide the grids into equal parts
            int cellHeight = dataGridView1.ClientSize.Height / dataGridView1.RowCount;
            int cellWidth = dataGridView1.ClientSize.Width / dataGridView1.ColumnCount;

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Height = cellHeight;
                dataGridView1.Rows[i].Resizable = DataGridViewTriState.False;
            }
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                dataGridView1.Columns[i].Width = cellWidth;
                dataGridView1.Columns[i].Resizable = DataGridViewTriState.False;
            }
            
            // To make the grid view unscrollable
            dataGridView1.ScrollBars = ScrollBars.None;

            // Center the contents
            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();
            cellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.DefaultCellStyle = cellStyle;

            int row = 0;
            int invalidInput = 0;
            int totalK = 0;
            int totalT = 0;
            for (int i = 0; i < fileContents.Length; i++)
            {
                int col = 0;
                for (int j = 0; j < fileContents[i].Length; j++)
                {
                    // Set the value of the cell based on the character in the file
                    switch (fileContents[i][j])
                    {
                        case 'K':
                            dataGridView1[col, row].Value = "Start";
                            totalK++;
                            col++;
                            break;
                        case 'X':
                            dataGridView1[col, row].Style.BackColor = Color.Black;
                            col++;
                            break;
                        case 'T':
                            dataGridView1[col, row].Value = "Treasure";
                            totalT++;
                            col++;
                            break;
                        case 'R':
                            col++;
                            break;
                        case ' ':
                            break;
                        default:
                            invalidInput++;
                            break;
                    }
                }
                if (col > 0)
                {
                    row++;
                }
            }
            // Input Validation
            if (invalidInput == 0 && totalK == 1 && totalT >= 1)
            {
                validFile = true;
            }
            else
            {
                validFile = false;
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            // To make the tresure found message visible
            label9.Visible = true;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
