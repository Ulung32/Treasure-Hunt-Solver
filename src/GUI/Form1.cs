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
    using Bfs;
    using Dfs;
    using Matrix;
    using Utils;
    using System.Reflection.Emit;
    using System.Linq.Expressions;
    using System.Xml.XPath;
    using System.Runtime.InteropServices;
    using System.Threading;

    public partial class Form1 : Form
    {
        private string fileName;
        private string[] fileContents;
        private Matrix maze;
        private Dfs solve_dfs, solve_dfs_tsp;
        private List<Tuple<int, int>> path = new List<Tuple<int, int>>();
        private List<Tuple<int, int>> searchPath = new List<Tuple<int, int>>();
        private List<Tuple<int, int>> pathTsp = new List<Tuple<int, int>>();
        private List<Tuple<int, int>> searchPathTsp = new List<Tuple<int, int>>();
        private double bfs_exectime, bfs_tsp_exectime;
        private int pause_duration;
        private int calculation = 0;
        private Boolean validFile = false;
        private Size originalSize;
        public Form1()
        {
            InitializeComponent();

            // To make it unmaximizable
            this.MaximizeBox = false;

            // To make it appear at the center of the screen
            this.StartPosition = FormStartPosition.CenterScreen;

            // To change GUI name
            this.Text = "Treasure Hunt Solver";

            // Save original window size
            originalSize = this.Size;
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
                calculation++;
            }
            else
            {
                label4.Text = "Steps: ";
                label5.Text = "Execution Time: ";
                label6.Text = "Route: ";
                label7.Text = "Nodes: ";
                label9.Visible = false;

                int row = 0;
                for (int i = 0; i < fileContents.Length; i++)
                {
                    int col = 0;
                    for (int j = 0; j < fileContents[i].Length; j++)
                    {
                        // Set the value of the cell based on the character in the file
                        switch (fileContents[i][j])
                        {
                            case 'K':
                                dataGridView1[col, row].Style.BackColor = Color.White;
                                col++;
                                break;
                            case 'X':
                                dataGridView1[col, row].Style.BackColor = Color.Black;
                                col++;
                                break;
                            case 'T':
                                dataGridView1[col, row].Style.BackColor = Color.White;
                                col++;
                                break;
                            case 'R':
                                dataGridView1[col, row].Style.BackColor = Color.White;
                                col++;
                                break;
                            case ' ':
                                break;
                            default:
                                break;
                        }
                    }
                    if (col > 0)
                    {
                        row++;
                    }
                }
                MessageBox.Show("The map from your file has been visualized! :)");
                // To make the visualization visible
                dataGridView1.Visible = true;

                // To make the search button enabled
                button3.Enabled = true;

                // To make the trackbar enabled
                trackBar1.Enabled = true;
                trackBar1.Value = 0;

                calculation++;

                // Reset window size
                this.Size = originalSize;

                // To make it appear at the center of the screen
                this.StartPosition = FormStartPosition.CenterScreen;
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

        // Steps
        private void label4_Click(object sender, EventArgs e)
        {

        }

        // Execution Time
        private void label5_Click(object sender, EventArgs e)
        {

        }

        // Route
        private void label6_Click(object sender, EventArgs e)
        {

        }

        // Node
        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.ReadOnly = true;
            textBox1.HideSelection = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (calculation > 0)
            {
                int row = 0;
                for (int i = 0; i < fileContents.Length; i++)
                {
                    int col = 0;
                    for (int j = 0; j < fileContents[i].Length; j++)
                    {
                        // Set the value of the cell based on the character in the file
                        switch (fileContents[i][j])
                        {
                            case 'K':
                                dataGridView1[col, row].Value = "";
                                col++;
                                break;
                            case 'X':
                                dataGridView1[col, row].Value = "";
                                col++;
                                break;
                            case 'T':
                                dataGridView1[col, row].Value = "";
                                col++;
                                break;
                            case 'R':
                                dataGridView1[col, row].Value = "";
                                col++;
                                break;
                            case ' ':
                                break;
                            default:
                                break;
                        }
                    }
                    if (col > 0)
                    {
                        row++;
                    }
                }
                calculation = 0;
            }

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
                    checkBox1.Enabled = true;
                }
            }


            dataGridView1.Visible = false;
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


            // Fill maze
            string[] copyFileContents = fileContents;
            maze = new Matrix(copyFileContents, rows, cols);
            Tuple<int, int> K = new Tuple<int, int>(maze.startRow, maze.startCol);

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
                            K = Tuple.Create(i, j); // Set starting point
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

                // To make the blocks uneditable
                dataGridView1.ReadOnly = true;

                if (col > 0)
                {
                    row++;
                }
            }
            // Input Validation
            if (totalK != 1)
            {
                validFile = false;
            }
            else if (invalidInput == 0 && totalT >= 1)
            {
                validFile = true;
            }
            else
            {
                validFile = false;
            }

            if (validFile)
            {
                // Solve BFS
                // Reset path and searchPath
                path.Clear();
                searchPath.Clear();
                pathTsp.Clear();
                searchPathTsp.Clear();
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                BFSsolver.BFS(maze.container, K, maze.totalTreasure, false, ref path, ref searchPath);
                stopwatch.Stop();
                bfs_exectime = stopwatch.Elapsed.TotalMilliseconds;
                stopwatch.Reset();
                stopwatch.Start();
                BFSsolver.BFS(maze.container, K, maze.totalTreasure, true, ref pathTsp, ref searchPathTsp);
                stopwatch.Stop();
                bfs_tsp_exectime = stopwatch.Elapsed.TotalMilliseconds;

                // Solve DFS
                solve_dfs = new Dfs(maze, false);
                solve_dfs_tsp = new Dfs(maze, true);
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        // Search Button
        private void button3_Click(object sender, EventArgs e)
        {
            // To disable button
            button3.Enabled = false;
            Color brightGreen = Color.FromArgb(0, 255, 0);
            // BFS Button
            if (radioButton1.Checked)
            {
                // TSP
                if (checkBox1.Checked)
                {
                    foreach (Tuple<int, int> block in searchPathTsp)
                    {
                        dataGridView1.Rows[block.Item1].Cells[block.Item2].Style.BackColor = Color.Blue;
                        Application.DoEvents();
                        Thread.Sleep(pause_duration * 100);
                        dataGridView1.Rows[block.Item1].Cells[block.Item2].Style.BackColor = Color.Yellow;
                        Application.DoEvents();
                    }

                    // Answer
                    foreach (Tuple<int, int> block in pathTsp)
                    {
                        dataGridView1.Rows[block.Item1].Cells[block.Item2].Style.BackColor = brightGreen;
                    }

                    label4.Text += (pathTsp.Count - 1).ToString(); // Steps
                    label5.Text += bfs_tsp_exectime.ToString() + " ms"; // Execution Time
                    label6.Text += Utils.convertRoute(pathTsp); // Route
                    label7.Text += searchPathTsp.Count.ToString(); // Nodes
                    label9.Visible = true;

                    // To reset pause duration
                    pause_duration = 0;
                }
                else
                {
                    foreach (Tuple<int, int> block in searchPath)
                    {
                        dataGridView1.Rows[block.Item1].Cells[block.Item2].Style.BackColor = Color.Blue;
                        Application.DoEvents();
                        Thread.Sleep(pause_duration * 100);
                        dataGridView1.Rows[block.Item1].Cells[block.Item2].Style.BackColor = Color.Yellow;
                        Application.DoEvents();
                    }

                    // Answer
                    foreach (Tuple<int, int> block in path)
                    {
                        dataGridView1.Rows[block.Item1].Cells[block.Item2].Style.BackColor = brightGreen;
                    }

                    label4.Text += (path.Count - 1).ToString(); // Steps
                    label5.Text += bfs_exectime.ToString() + " ms"; // Execution Time
                    label6.Text += Utils.convertRoute(path); // Route
                    label7.Text += searchPath.Count.ToString(); // Nodes
                    label9.Visible = true;

                    // To reset pause duration
                    pause_duration = 0;
                }
            }
            // DFS Button
            else if (radioButton2.Checked)
            {
                if (checkBox1.Checked)
                {
                    foreach (Tuple<int, int> block in solve_dfs_tsp.searchPath)
                    {
                        dataGridView1.Rows[block.Item1].Cells[block.Item2].Style.BackColor = Color.Blue;
                        Application.DoEvents();
                        Thread.Sleep(pause_duration * 100);
                        dataGridView1.Rows[block.Item1].Cells[block.Item2].Style.BackColor = Color.Yellow;
                        Application.DoEvents();
                    }

                    // Answer
                    foreach (Tuple<int, int> block in solve_dfs_tsp.pathResult)
                    {
                        dataGridView1.Rows[block.Item1].Cells[block.Item2].Style.BackColor = brightGreen;
                    }

                    label4.Text += solve_dfs_tsp.stepCount.ToString(); // Steps
                    label5.Text += solve_dfs_tsp.execTime.ToString() + " ms"; // Execution Time
                    label6.Text += Utils.convertRoute(solve_dfs_tsp.pathResult); // Route
                    label7.Text += solve_dfs_tsp.visitCount.ToString(); // Nodes
                    label9.Visible = true;
                }
                else
                {
                    foreach (Tuple<int, int> block in solve_dfs.searchPath)
                    {
                        dataGridView1.Rows[block.Item1].Cells[block.Item2].Style.BackColor = Color.Blue;
                        Application.DoEvents();
                        Thread.Sleep(pause_duration * 100);
                        dataGridView1.Rows[block.Item1].Cells[block.Item2].Style.BackColor = Color.Yellow;
                        Application.DoEvents();
                    }

                    // Answer
                    foreach (Tuple<int, int> block in solve_dfs.pathResult)
                    {
                        dataGridView1.Rows[block.Item1].Cells[block.Item2].Style.BackColor = brightGreen;
                    }

                    label4.Text += solve_dfs.stepCount.ToString(); // Steps
                    label5.Text += solve_dfs.execTime.ToString() + " ms"; // Execution Time
                    label6.Text += Utils.convertRoute(solve_dfs.pathResult); // Route
                    label7.Text += solve_dfs.visitCount.ToString(); // Nodes
                    label9.Visible = true;
                }

                // To reset pause duration
                pause_duration = 0;
            }
            // To make the trackbar disabled
            trackBar1.Enabled = false;

            calculation++;

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            pause_duration = trackBar1.Value;
        }
    }
}