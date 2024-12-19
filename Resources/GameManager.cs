using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FluentUIGame.Resources
{
    public class GameManager
    {
        private const int Size = 4;
        private Tile[,] board;

        public GameManager()
        {
            board = new Tile[Size, Size];
            AddRandomTile();
            AddRandomTile();
        }

        public Tile[,] GetBoard() => board;

        public bool MoveLeft()
        {
            bool moved = false;
            for (int y = 0; y < Size; y++)
            {
                var row = board.GetRow(y).Where(t => t != null).ToList();
                for (int x = 0; x < row.Count - 1; x++)
                {
                    if (row[x].Value == row[x + 1].Value)
                    {
                        row[x] = new Tile(row[x].Value * 2, x, y);
                        row.RemoveAt(x + 1);
                        moved = true;
                    }
                }
                for (int x = 0; x < Size; x++)
                {
                    if (x < row.Count)
                    {
                        if (board[x, y] != row[x])
                            moved = true;
                        board[x, y] = row[x];
                    }
                    else
                    {
                        if (board[x, y] != null)
                            moved = true;
                        board[x, y] = null;
                    }
                }
            }
            if (moved)
                AddRandomTile();
            return moved;
        }

        public bool MoveRight()
        {
            bool moved = false;
            for (int y = 0; y < Size; y++)
            {
                var row = board.GetRow(y).Where(t => t != null).ToList();
                for (int x = row.Count - 1; x > 0; x--)
                {
                    if (row[x].Value == row[x - 1].Value)
                    {
                        row[x] = new Tile(row[x].Value * 2, x, y);
                        row.RemoveAt(x - 1);
                        moved = true;
                    }
                }
                for (int x = 0; x < Size; x++)
                {
                    int index = Size - 1 - x;
                    if (x < row.Count)
                    {
                        if (board[index, y] != row[row.Count - 1 - x])
                            moved = true;
                        board[index, y] = row[row.Count - 1 - x];
                    }
                    else
                    {
                        if (board[index, y] != null)
                            moved = true;
                        board[index, y] = null;
                    }
                }
            }
            if (moved)
                AddRandomTile();
            return moved;
        }

        public bool MoveUp()
        {
            bool moved = false;
            for (int x = 0; x < Size; x++)
            {
                var column = board.GetColumn(x).Where(t => t != null).ToList();
                for (int y = 0; y < column.Count - 1; y++)
                {
                    if (column[y].Value == column[y + 1].Value)
                    {
                        column[y] = new Tile(column[y].Value * 2, x, y);
                        column.RemoveAt(y + 1);
                        moved = true;
                    }
                }
                for (int y = 0; y < Size; y++)
                {
                    if (y < column.Count)
                    {
                        if (board[x, y] != column[y])
                            moved = true;
                        board[x, y] = column[y];
                    }
                    else
                    {
                        if (board[x, y] != null)
                            moved = true;
                        board[x, y] = null;
                    }
                }
            }
            if (moved)
                AddRandomTile();
            return moved;
        }

        public bool MoveDown()
        {
            bool moved = false;
            for (int x = 0; x < Size; x++)
            {
                var column = board.GetColumn(x).Where(t => t != null).ToList();
                for (int y = column.Count - 1; y > 0; y--)
                {
                    if (column[y].Value == column[y - 1].Value)
                    {
                        column[y] = new Tile(column[y].Value * 2, x, y);
                        column.RemoveAt(y - 1);
                        moved = true;
                    }
                }
                for (int y = 0; y < Size; y++)
                {
                    int index = Size - 1 - y;
                    if (y < column.Count)
                    {
                        if (board[x, index] != column[column.Count - 1 - y])
                            moved = true;
                        board[x, index] = column[column.Count - 1 - y];
                    }
                    else
                    {
                        if (board[x, index] != null)
                            moved = true;
                        board[x, index] = null;
                    }
                }
            }
            if (moved)
                AddRandomTile();
            return moved;
        }

        private void AddRandomTile()
        {
            var emptyTiles = new List<(int, int)>();
            for (int x = 0; x < Size; x++)
            {
                for (int y = 0; y < Size; y++)
                {
                    if (board[x, y] == null)
                    {
                        emptyTiles.Add((x, y));
                    }
                }
            }

            if (emptyTiles.Count > 0)
            {
                var (x, y) = emptyTiles[new Random().Next(emptyTiles.Count)];
                board[x, y] = new Tile(2, x, y);
            }
        }
    }

    public static class ArrayExtensions
    {
        public static IEnumerable<T> GetRow<T>(this T[,] array, int row)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                yield return array[i, row];
            }
        }

        public static IEnumerable<T> GetColumn<T>(this T[,] array, int column)
        {
            for (int i = 0; i < array.GetLength(1); i++)
            {
                yield return array[column, i];
            }
        }
    }
}
