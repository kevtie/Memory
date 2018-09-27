using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Memory
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        int rowCount, columnCount;


        #region Constructor
        public MainWindow()
        {
            InitializeComponent();

            // if game is not loaded
            NewGame();
        }
        #endregion

        #region Private NewGame
        private void NewGame()
        {
            List<Button> buttons = new List<Button>();

            ClearMemoryContainer();
            SetMemoryContainer(4, 4);
            FillMemoryContainer();
        }
        #endregion

        #region Private ClearMemoryContainer
        private void ClearMemoryContainer()
        {
            MemoryContainer.Children.Clear();
        }
        #endregion

        #region Private FillMemoryContainer
        private void FillMemoryContainer()
        {
            MemoryContainer.Children.Add(new Button());
        }
        #endregion

        #region Private SetMemoryContainer
        private void SetMemoryContainer(int columns, int rows)
        {
            rowCount = rows;
            columnCount = columns;
        }
        #endregion
    }
}
