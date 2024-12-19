using FluentUIGame.Resources;
using FluentUIGame.ViewModels.Pages;
using System.Security.Cryptography;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Wpf.Ui.Controls;

namespace FluentUIGame.Views.Pages
{
    public partial class GamePage : INavigableView<GameViewModel>
    {
        public GameViewModel ViewModel { get; }
        private GameManager gameManager;
        private Dictionary<Tile, TileControl> tileControls = new Dictionary<Tile, TileControl>();

        public GamePage(GameViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
            gameManager = new GameManager();
            DrawBoard();
            Focus(); // Устанавливаем фокус на страницу для обработки клавиш
        }

        private void DrawBoard()
        {
            var board = gameManager.GetBoard();

            // Удаляем плитки, которые отсутствуют на новом поле
            var tilesToRemove = new List<Tile>();
            foreach (var tile in tileControls.Keys)
            {
                if (!IsTileInBoard(tile, board))
                {
                    var control = tileControls[tile];
                    GameCanvas.Children.Remove(control);
                    tilesToRemove.Add(tile);
                }
            }
            foreach (var tile in tilesToRemove)
            {
                tileControls.Remove(tile);
            }

            // Обновляем существующие плитки и добавляем новые
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    var tile = board[x, y];
                    if (tile != null)
                    {
                        if (tileControls.ContainsKey(tile))
                        {
                            // Обновляем положение существующей плитки
                            var control = tileControls[tile];
                            control.MoveTo(x * 100, y * 100);
                        }
                        else
                        {
                            // Добавляем новую плитку
                            var control = new TileControl(tile.Value);
                            Canvas.SetLeft(control, x * 100);
                            Canvas.SetTop(control, y * 100);
                            GameCanvas.Children.Add(control);
                            tileControls[tile] = control;

                            // Анимация появления
                            var scaleTransform = new ScaleTransform(0.0, 0.0);
                            control.RenderTransformOrigin = new System.Windows.Point(0.5, 0.5);
                            control.RenderTransform = scaleTransform;

                            var scaleAnimation = new DoubleAnimation
                            {
                                To = 1.0,
                                Duration = TimeSpan.FromSeconds(0.3),
                            };
                            scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnimation);
                            scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnimation);
                        }
                    }
                }
            }
        }

        private bool IsTileInBoard(Tile tile, Tile[,] board)
        {
            foreach (var t in board)
            {
                if (t == tile)
                    return true;
            }
            return false;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            bool moved = false;
            switch (e.Key)
            {
                case Key.A:
                    moved = gameManager.MoveLeft();
                    break;
                case Key.D:
                    moved = gameManager.MoveRight();
                    break;
                case Key.W:
                    moved = gameManager.MoveUp();
                    break;
                case Key.S:
                    moved = gameManager.MoveDown();
                    break;
            }
            if (moved)
            {
                DrawBoard();
            }
        }
    }
}