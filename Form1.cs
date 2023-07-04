using System;
using System.Windows.Forms;

namespace SudokuSolver
{
    public partial class MainForm : Form
    {
        private TextBox[,] sudokuGrid;

        public MainForm()
        {
            InitializeComponent();
            InitializeSudokuGrid();
        }

        private void InitializeComponent()
        {
            throw new NotImplementedException();
        }

        private void InitializeSudokuGrid()
        {
            sudokuGrid = new TextBox[9, 9];

            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    sudokuGrid[row, col] = new TextBox();
                    sudokuGrid[row, col].Location = new System.Drawing.Point(col * 40, row * 40);
                    sudokuGrid[row, col].Size = new System.Drawing.Size(40, 40);
                    sudokuGrid[row, col].MaxLength = 1;
                    sudokuGrid[row, col].TextAlign = HorizontalAlignment.Center;
                    Controls.Add(sudokuGrid[row, col]);
                }
            }
        }

        private void SolveButton_Click(object sender, EventArgs e)
        {
            int[,] board = new int[9, 9];

            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (sudokuGrid[row, col].Text == "")
                    {
                        board[row, col] = 0;
                    }
                    else
                    {
                        board[row, col] = int.Parse(sudokuGrid[row, col].Text);
                    }
                }
            }

            if (SolveSudoku(board))
            {
                for (int row = 0; row < 9; row++)
                {
                    for (int col = 0; col < 9; col++)
                    {
                        sudokuGrid[row, col].Text = board[row, col].ToString();
                    }
                }
            }
            else
            {
                MessageBox.Show("No solution exists!");
            }
        }

        private bool SolveSudoku(int[,] board)
        {
            int row = 0;
            int col = 0;
            bool isEmpty = true;

            for (row = 0; row < 9; row++)
            {
                for (col = 0; col < 9; col++)
                {
                    if (board[row, col] == 0)
                    {
                        isEmpty = false;
                        break;
                    }
                }
                if (!isEmpty)
                {
                    break;
                }
            }

            if (isEmpty)
            {
                return true;
            }

            for (int num = 1; num <= 9; num++)
            {
                if (IsSafe(board, row, col, num))
                {
                    board[row, col] = num;

                    if (SolveSudoku(board))
                    {
                        return true;
                    }

                    board[row, col] = 0;
                }
            }

            return false;
        }

        private bool IsSafe(int[,] board, int row, int col, int num)
        {
            for (int i = 0; i < 9; i++)
            {
                if (board[row, i] == num || board[i, col] == num)
                {
                    return false;
                }
            }

            int startRow = row - row % 3;
            int startCol = col - col % 3;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i + startRow, j + startCol] == num)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}