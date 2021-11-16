/***********************************
 * @Instructor Prof. Dario Guiao   *
 * @Autor: Alqassam Firwana        *
 * @id:                            *
 * MidTerm Project                 * 
 * Zombies Vs Humans               *
 * Main Windows                    * 
 ***********************************/

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
using System.Collections;
using System.Timers;
using System.Diagnostics;

namespace firwanaa_midterm
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /***********************************************************
            *  variables
        ************************************************************/
        String playerName;                                          //player name
        static IList<Human> humanObj;                               //Static ArrayList of H
        static IList<Zombie> zombieObj;                             //Static ArrayList of Z
        Random rand = new Random(Guid.NewGuid().GetHashCode());     //Random object
        static int col;                                             //column size                                           
        static int row;                                             //Row size
        static int[,] myMatrix;                                     //int matrix
        bool[,] checker;                                            //Boolean matrix
        string[,] strMarix;                                         //string matrix                                    
        bool flag = true;                                           //flag to control buttons flow

        /***********************************************************
            *  Generate Button Action
        ************************************************************/
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            /******************************************************
              * Variables
             ******************************************************/
            flag = true;                                            //set flag to true
            if (pName.Text == "")                                   //If user didn't enter a name
            {
                playerName = "No_Name";
            }
            else playerName = pName.Text;                           //User name for files names
            int humanNumInput = 0;                                  //Human num
            int zombieNumInput = 0;                                 //Zombies num
            Human.nameNumH = 0;
            Zombie.nameNumZ= 0;
            /*****************************************************
                *Check input value is valid & show warnings
            ******************************************************/
            if (!int.TryParse(tbRow.Text, out row))
            {
                MessageBox.Show("Rows - Numeric input only", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbRow.BorderBrush = System.Windows.Media.Brushes.Red;
                return;
            }
            else if (row < 3)
            {

                MessageBox.Show("Rows - must be 3 or more", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbRow.BorderBrush = System.Windows.Media.Brushes.Red;
                return;
            }
            else { tbRow.BorderBrush = System.Windows.Media.Brushes.Transparent; }
            if (!int.TryParse(tbCol.Text, out col))
            {

                MessageBox.Show("Columns - Numeric input only", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbCol.BorderBrush = System.Windows.Media.Brushes.Red;
                return;
            }
            else if (col < 2)
            {

                MessageBox.Show("Columns - must be 2 or more", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbCol.BorderBrush = System.Windows.Media.Brushes.Red;
                return;
            }
            else { tbCol.BorderBrush = System.Windows.Media.Brushes.Transparent; }
            if (!int.TryParse(HNumTF.Text, out humanNumInput))
            {

                MessageBox.Show("Human - Numeric input only", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                HNumTF.BorderBrush = System.Windows.Media.Brushes.Red;
                return;
            }
            else if (humanNumInput < 5)
            {
                MessageBox.Show("Human - must be 5 or more", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                HNumTF.BorderBrush = System.Windows.Media.Brushes.Red;
                return;
            }
            else { HNumTF.BorderBrush = System.Windows.Media.Brushes.Transparent; }

            if (!int.TryParse(ZNumTF.Text, out zombieNumInput))
            {

                MessageBox.Show("Zombies - Numeric input only", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                ZNumTF.BorderBrush = System.Windows.Media.Brushes.Red;
                return;
            }
            else if (zombieNumInput < 1)
            {
                MessageBox.Show("Zombies - must be 1 or more", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                ZNumTF.BorderBrush = System.Windows.Media.Brushes.Red;
                return;
            }
            else { ZNumTF.BorderBrush = System.Windows.Media.Brushes.Transparent; }


            /****************************************
             * Check if H & can fit in space
             ***************************************/
            int colRow = col * row;
            int hz = humanNumInput + zombieNumInput;
            if (colRow == hz || hz > colRow) { MessageBox.Show("No space for roaming", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning); return; }

            int counterH = 0;                                    //Human Counter
            int counterZ = 0;                                    //Zombie Counter
            int totalCounter = 0;                                //Show total for testing
            int totalZH = humanNumInput + zombieNumInput;        //Sum of total H+Z
            humanObj = new List<Human>();                        //ArrayList of Human object
            zombieObj = new List<Zombie>();                      //ArrayList of Zombie object
            humanObj.Clear();                                    //Flush at start
            zombieObj.Clear();                                   //Flush at start
            //Human.nameNumH  = 1;                                
            //Zombie.nameNumZ = 1;
            //Debug.WriteLine(row.ToString());
            //Debug.WriteLine(col.ToString());
            /****************************************
             * Three Matrices: int, string and Boolean
             ***************************************/
            myMatrix = new int[row, col];                        //int values represent H = 1, Z = 0 or Empty =3
            checker = new bool[row, col];                        //Boolean represent occupied position = true
            strMarix = new string[row, col];                     // strMatrix to View as strings

            /****************************************
             * Refresh GUI
             ***************************************/
            myGrid.Children.Clear();                             //Refresh the Grid
            myGrid.RowDefinitions.Clear();                       //Refresh the Grid
            myGrid.ColumnDefinitions.Clear();                    //Refresh the Grid
            myGrid.Style = null;                                 //Reload style <- needs some word


            /****************************************
             * Flush matrices
             ***************************************/
            Array.Clear(myMatrix, 0, myMatrix.Length);           //Flush  ArrayList
            Array.Clear(checker, 0, checker.Length);             //Flush  ArrayList
            Array.Clear(strMarix, 0, strMarix.Length);           //Flush  ArrayList

            /**************************************************************
              * Main do-while Loop <- populate unique position and save it
            ***************************************************************/
            do
            {
                for (int i = 0; i < myMatrix.GetLength(0); i++)
                {
                    for (int j = 0; j < myMatrix.GetLength(1); j++)
                    {
                        ColumnDefinition clm = new ColumnDefinition()
                        {
                            Name = "Col_" + i,
                            Width = new GridLength(32.5),
                        };
                        myGrid.ColumnDefinitions.Add(clm);
                        int ZHPicker = rand.Next(1, 4);
                        RowDefinition row = new RowDefinition();
                        myGrid.RowDefinitions.Add(row);
                        row.Height = new GridLength(40, GridUnitType.Pixel);
                        //clm.Width = new GridLength(40, GridUnitType.Pixel);
                        TextBox txtb = new TextBox();
                        txtb.Text = " ";
                        txtb.IsEnabled = false;
                        txtb.Height =ActualHeight;
                        txtb.Width = ActualWidth;
                        txtb.Style = null;

                        /****************************************
                        * Make sure position Unique at Start
                        * Human  = 1
                        * Zombie = 0
                        * empty  = 3 
                        *****************************************/
                        if (ZHPicker == 1 && counterH < humanNumInput && checker[i, j] != true)
                        {   //Human
                            Human hm = new Human();             //Human obj
                            txtb.Text = " ";                    //empty txtbox
                            txtb.AppendText(hm.Hname);          //Add Human name to txtbox
                            hm.addH(i, j);                      //call addH <- add start config. to record
                            humanObj.Add(hm);                   //add Human obj to ArrayList
                            strMarix[i, j] = hm.Hname;          //save to string matrix
                            counterH = counterH + 1;            //increment H counter
                            checker[i, j] = true;              //make bool cell true <- used to assign unique positions
                            myMatrix[i, j] = 1;                 //Save to numeric matrix 1 = Human
                            //txtb.Background = new SolidColorBrush(Colors.LightGreen); //Bg Color
                            txtb.Foreground = new SolidColorBrush(Colors.Black);
                            txtb.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#5edb80");
                            
                        }
                        if (ZHPicker == 2 && counterZ < zombieNumInput && checker[i, j] != true)
                        {   //Zombie
                            Zombie zm = new Zombie();           //Zobie obj
                            txtb.Text = " ";                    //empty txtbox    
                            txtb.AppendText(zm.Zname);          //add Zombie name to txtbox
                            zm.addZ(i, j);                      //call addZ <- add start config. to record
                            zombieObj.Add(zm);                  //add Z obj to ArrayList
                            strMarix[i, j] = zm.Zname;          //save to string matrix <- testing step
                            counterZ = counterZ + 1;            //increment Z counter
                            checker[i, j] = true;               //make bool cell true <- used to assign unique positions
                            myMatrix[i, j] = 0;                 //Save to numeric matrix 0 = Zombie
                            //txtb.Background = new SolidColorBrush(Colors.Red); //Bg Color #db5e7b
                            txtb.Foreground = new SolidColorBrush(Colors.Black);
                            txtb.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#e82e59");
                            
                        }
                        if (ZHPicker == 3 && counterZ < zombieNumInput && counterH < humanNumInput)
                        {   //Empty
                            txtb.Text = " ";                    //empty txtbox
                            txtb.AppendText(" ");               //add to grid
                            myMatrix[i, j] = 3;                 //save to numeric matrix
                            strMarix[i, j] = "";
                            //txtb.Foreground = new SolidColorBrush(Colors.Black);
                            //txtb.Background = new SolidColorBrush(Colors.AliceBlue); //Bg Color
                            
                        }

                        txtb.FontFamily = new FontFamily("Arial");
                        txtb.FontSize = 12;
                        txtb.Foreground = Brushes.Black;
                        txtb.FontStyle = FontStyles.Normal;
                        txtb.FontWeight = FontWeights.SemiBold;
                        //txtb.FontStyle();                     // <- didn't solve color Bug 
                        Grid.SetColumn(txtb, j);                //Set Grid Column Size       
                        Grid.SetRow(txtb, i);                   //Set Grid Row Size
                        myGrid.Children.Add(txtb);              //Add txtbox to the Grid
                    }

                }
                totalCounter = counterH + counterZ;             //used to confirm all Humans and Zombies took positions
                /****************************************
                * Print Start Configuration to a File
                ***************************************/
                ZHWar.save_config(playerName, humanObj, zombieObj);
            } while (totalZH != totalCounter);
        }


        /************************************************************
            * Start an open war <-- winner takes all
        *************************************************************/
        private void Open_War_Click(object sender, RoutedEventArgs e)
        {
            if (!flag) { MessageBox.Show("Generate new matrix first", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning); return; }
            const int temp = -1; //<- means it's open ware
            if (myMatrix == null) { MessageBox.Show("Generate matrix first", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning); return; }
            ZHWar.start_war(strMarix, col, row, playerName, humanObj, zombieObj, temp);
            flag = false;
           
        }
        /************************************************************
            * Control options by checkbox <- if checked
        *************************************************************/
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (cbIterations.IsChecked == true)
            {
                tbIteration.IsEnabled = true;
                Open_War.IsEnabled = false;
                battles_btn.IsEnabled = true;
            }

        }

        /************************************************************
            * Control options by checkbox <- if unchecked
        *************************************************************/
        private void UnCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (cbIterations.IsChecked == false)
            {
                tbIteration.IsEnabled = false;
                Open_War.IsEnabled = true;
                battles_btn.IsEnabled = false;
            }
        }


        /************************************************************
            * Battles based on iterations numbers
        *************************************************************/
        private void battles_btn_Click(object sender, RoutedEventArgs e)
        {
            if (!flag) { MessageBox.Show("Generate new matrix first", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning); return; }
            if (myMatrix == null) { MessageBox.Show("Generate matrix first", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning); return; }

            int iterationsNum = 0;
            if (!int.TryParse(tbIteration.Text, out iterationsNum))
            {
                MessageBox.Show("Iterations - Numeric input only", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbIteration.BorderBrush = System.Windows.Media.Brushes.Red;
                return;
            }
            else if (iterationsNum < 1)
            {
                MessageBox.Show("Iterations - must be 1 or more", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbIteration.BorderBrush = System.Windows.Media.Brushes.Red;
                return;
            }
            else { Debug.WriteLine("Iter num", iterationsNum.ToString()); iterationsNum = Convert.ToInt32(tbIteration.Text); }
            ZHWar.start_war(strMarix, col, row, playerName, humanObj, zombieObj, iterationsNum);
            flag = false;

        }



        private void tbIterations_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void HNum_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ZNumTF_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }

}
