/***********************************
 * @Instructor Prof. Dario Guiao   *
 * @Autor: Alqassam Firwana        *
 * @id: ____________               *
 * MidTerm Project                 * 
 * Zombies Vs Humans               *
 * Zombie Human War tools          * 
 ***********************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Windows.Controls;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;

namespace firwanaa_midterm
{
    public abstract class ZHWar
    {
        /**************************************************************
          * ILists of Humans and Zombies
        ***************************************************************/
        static IList<Human> humanArr;
        static IList<Zombie> zombieArr;


        /**************************************************************
          * Save file  Youname_FinalResults.txt
        ***************************************************************/
        public static void save_final_results(String name, int h, int z, int c, int h2, int z2)
        {
            try
            {//saving start_config
                using (StreamWriter wr = new StreamWriter(@"C:\" + name + "_FinalResults" + ".txt"))
                {
                    wr.WriteLine("Name:  " + name);
                    wr.WriteLine("Humans at start:  " + h);
                    wr.WriteLine("Humans at end  : " + h2);
                    wr.WriteLine("Zombies:  " + z);
                    wr.WriteLine("Zombies at end  : " + z2);
                    wr.WriteLine("Human survived: " + ((h2 * 100) / h) + " %");
                    wr.WriteLine("Iterations:  " + c);  
                }
            }
            catch (IOException err)
            {
                Console.WriteLine(err.Message);
            }
        }


        /**************************************************************
          * Save file SomeGuy_StartConfiguration.txt
        ***************************************************************/
        public static void save_config(string name, IList<Human> arr1, IList<Zombie> arr2)
        {
            try
            {//saving start_config

                using (StreamWriter wr = new StreamWriter(@"C:\" + name + "_StartingConfiguration" + ".txt"))
                {
                    int counterHuman = 1;
                    int counterZombie = 1;
                    foreach (Human H in arr1)
                    {
                        foreach (var item in H.getHRecord())
                        {
                            wr.WriteLine("Human " + counterHuman + " starts at " + item.Key + "," + item.Value);
                            counterHuman++;
                        }
                    }
                    wr.WriteLine();
                    foreach (Zombie Z in arr2)
                    {
                        foreach (var item in Z.getZRecord())
                        {
                            wr.WriteLine("Zombie " + counterZombie + " starts at " + item.Key + "," + item.Value);
                            counterZombie++;
                        }
                    }

                }
            }
            catch (IOException err)
            {
                Console.WriteLine(err.Message);
            }
        }

        /*******************************************************************************************
            * Save file SomeGuy_Movements.txt
        ********************************************************************************************/
        public static void save_movement(StringBuilder sb, string name)
        {
            try
            {//saving 
                using (System.IO.StreamWriter file = new System.IO.StreamWriter((@"C:\" + name + "_Movements" + ".txt")))
                {
                    file.WriteLine(sb.ToString()); // "sb" is the StringBuilder
                }
            }
            catch (IOException err)
            {
                Debug.WriteLine(err.Message);
            }
        }


        /************************************************************************
        * function to print DoingTheInfection file  -> SomeGuy_DoingTheInfection 
        *************************************************************************/
        public static void save_doingTheInfection(string name, IList<Zombie> arr2)
        {
            try
            {//saving 
                using (System.IO.StreamWriter file = new System.IO.StreamWriter((@"C:\" + name + "_DoingInfecting" + ".txt")))
                {
                    foreach (Zombie z in arr2)
                    {
                        if (z.get_count() == 0) continue;
                        file.WriteLine(z.getInfectingZ());

                    }
                }
            }
            catch (IOException err)
            {
                Debug.WriteLine(err.Message);
            }
        }


        /**************************************************************
           * Save file SomeGuy_InfectedBy.txt
        ***************************************************************/
        public static void save_infectedBy(string name, IList<Human> arr1)
        {
            try
            {//saving 
                using (System.IO.StreamWriter file = new System.IO.StreamWriter((@"C:\" + name + "_InfectedBy" + ".txt")))
                {
                    foreach (Human h in arr1)
                    {
                        file.WriteLine(h.getInfectedByH());
                    }
                }
            }
            catch (IOException err)
            {
                Debug.WriteLine(err.Message);
            }
        }


        /****************************************************************
            * Check if Human and Zombie in the same location <-- The Fight
        *****************************************************************/
        public static void checkPath(IList<Human> arr1, IList<Zombie> arr2)
        {
            //Debug.WriteLine("Check path arr2 length", arr2.Count().ToString());
            foreach (Zombie z in arr2.Reverse())
            {
                foreach (Human h in arr1)
                {
                    if (z.getCurrentPositoinZ() == h.getCurrentPositoinH() && humanArr.Contains(h)) //<-- If Human and Zombie in the same position
                    {
                        Point temp = h.getCurrentPositoinH();           //<--Save current position
                        int x = Convert.ToInt32(temp.X);                //<--Covert X and Y to give it to the new Zombie
                        int y = Convert.ToInt32(temp.Y);
                        /**************************************************************
                            * Zombies Control over the Ground 
                        ***************************************************************/
                        Zombie newZombie = new Zombie();                //<--Human became Zombie
                        newZombie.setCurrentPositoinZ(x, y);            //<--Give newly born Zombie a position in the BattleField 
                        h.setInfectedByH(z.Zname.ToString(), count);    //<--Collect attacker name 
                        z.setInfecteingZ(h.Hname.ToString(), count);    //<--Collect Prey name
                        humanArr.Remove(h);                             //<--Human Remove from cloned human List
                        arr2.Add(newZombie);                            //<--Zombie Add to Original zombie list

                    }
                }
            }

        }

        /******************************************************************
           * Establishing the BattelField <-- saving start position of all
        *******************************************************************/
        public static void setCurrentPositionZH(string[,] strMtx, IList<Human> arr1, IList<Zombie> arr2)
        {
            for (int i = 0; i < strMtx.GetLength(0); i++)
            {
                for (int j = 0; j < strMtx.GetLength(1); j++)
                {

                    foreach (Human h in arr1)
                    {
                        if (h.Hname == strMtx[i, j])
                        {
                            h.setCurrentPositoinH(i, j);
                        }
                    }
                    foreach (Zombie z in arr2)
                    {
                        if (z.Zname == strMtx[i, j])
                        {
                            z.setCurrentPositoinZ(i, j);
                        }
                    }
                }
            }
        }

        /**************************************************************
            * Clone Human List
        ***************************************************************/
        static public void clone_arrH(IList<Human> arr)
        {
            humanArr = new List<Human>();
            humanArr.Clear();
            foreach (Human h in arr)
            {
                humanArr.Add(h);
            }
        }
        /**************************************************************
            * Clone Zombie List
        ***************************************************************/
        static public void clone_arrZ(IList<Zombie> arr)
        {
            zombieArr = new List<Zombie>();
            zombieArr.Clear();
            foreach (Zombie z in arr)
            {
                zombieArr.Add(z);
            }
        }

        /*****************************************************************************
          * Function to make H & Z Roam within the BattleField.
          * Call function to  check each coordinates at each iteration checkPath(),
          * which will delete human and add zombie and save "infectedBy" and "Infecting".
          * Call all functions that will print information to  files.
          * The while loop is conditioned with and if statement inside it, to control if 
          * it will be till humans = 0 or by number of iteration "iter" given by the user.
          * 
        ******************************************************************************/
        static Random rand = new Random(Guid.NewGuid().GetHashCode());
        static Random rand2 = new Random(Guid.NewGuid().GetHashCode());
        static int count = 0;
        static int tempCount = 0;
        public static void start_war(string[,] arr, int row, int col, string name, IList<Human> arr1, IList<Zombie> arr2, int iter)
        {
            //Establish current configurations
            setCurrentPositionZH(arr, arr1, arr2);
            StringBuilder HStrB = new StringBuilder();
            StringBuilder ZStrB = new StringBuilder();

            //Cloning lists
            clone_arrH(arr1);
            clone_arrZ(arr2);
            count = 0;
            do
            {

                string tempStr = "Iteration " + count;
                HStrB.Append(tempStr).Append('\n');
                /*****************************************************************
                * save to StringBuilder
                ******************************************************************/
                int dirRandOpt1 = rand.Next(0, 8);           //random value for dir1
                int dirRandOpt2 = rand2.Next(0, 8);          //random value for dir2
                int[] dir1 = { -1, -1, -1, 0, 0, 1, 1, 1 };  //x direction option
                int[] dir2 = { -1, 0, 1, -1, 1, -1, 0, 1 };  //y direction option
                Debug.WriteLine("before start");
                /******************************************************************
                    * Roaming Humans
                 ******************************************************************/
                foreach (Human h in humanArr)
                {
                    Point tempPt = h.getCurrentPositoinH();
                    start:
                    dirRandOpt1 = rand.Next(0, 8);           //random value for dir1
                    dirRandOpt2 = rand2.Next(0, 8);          //random value for dir2
                    Debug.WriteLine("after start");

                    int xDir = dir1[dirRandOpt1];            //x new direction value
                    int yDir = dir2[dirRandOpt2];            //y new direction value
                    int i = Convert.ToInt32(tempPt.X);
                    int j = Convert.ToInt32(tempPt.Y);
                    int sumNewXCoordinates = i + xDir;       //move x
                    int sumNewYCoordinates = j + yDir;       //move y

                    /*****************************************************************
                       * Break out if index out of range
                    ******************************************************************/
                    if (sumNewXCoordinates >= row || sumNewXCoordinates < 0 || sumNewYCoordinates >= col || sumNewYCoordinates < 0)
                    {
                        Debug.WriteLine("out of bound condition");
                        goto start;                         //<-- breaking out of "out of bound exception"
                    }
                    else
                    {
                        Debug.WriteLine("after out of bound");
                        h.setCurrentPositoinH(sumNewXCoordinates, sumNewYCoordinates);
                        h.addptH(sumNewXCoordinates, sumNewYCoordinates);
                        HStrB.Append("From ").Append(h.Hname).Append(" ").Append(tempPt.X).Append(",").Append(tempPt.Y).Append(" is now at ").Append(h.getCurrentPositoinH()).Append('\n');
                    }
                }

                /******************************************************************
                    * Roaming Zombies
                 ******************************************************************/
                foreach (Zombie z in arr2)
                {
                    Point tempPt = z.getCurrentPositoinZ();
                    start2:
                    dirRandOpt1 = rand.Next(0, 8);           //random value for dir1
                    dirRandOpt2 = rand2.Next(0, 8);          //random value for dir2
                    int xDir2 = dir1[dirRandOpt1];           //x new direction value
                    int yDir2 = dir2[dirRandOpt2];           //y new direction value
                    int i = Convert.ToInt32(tempPt.X);
                    int j = Convert.ToInt32(tempPt.Y);
                    int sumNewXCoordinates = i + xDir2;      //move x
                    int sumNewYCoordinates = j + yDir2;      //move y
                    if (sumNewXCoordinates >= row || sumNewXCoordinates < 0 || sumNewYCoordinates >= col || sumNewYCoordinates < 0)
                    {
                        //Debug.WriteLine("out of bound condition");
                        goto start2;
                    }
                    else
                    {
                        z.setCurrentPositoinZ(sumNewXCoordinates, sumNewYCoordinates);
                        z.addptZ(sumNewXCoordinates, sumNewYCoordinates);
                        ZStrB.Append("From ").Append(z.Zname).Append(" ").Append(tempPt.X).Append(",").Append(tempPt.Y).Append(" is now at ").Append(z.getCurrentPositoinZ()).Append('\n');
                    }
                    dirRandOpt1 = rand.Next(0, 8);           //random value for dir1
                    dirRandOpt2 = rand2.Next(0, 8);          //random value for dir2
                }
                //Check cross path
                checkPath(arr1, arr2);
                //System.Threading.Thread.Sleep(50);
                //Build String to save each iteration
                HStrB.Append('\n');
                HStrB.Append(ZStrB);
                HStrB.Append("--------------------------------------------\n");
                ZStrB.Clear();
                count++;
                tempCount = humanArr.Count();

                if (iter == -1) continue; else if (count == iter) { tempCount = 0; } //<--Make While loop Selective - By Iteration or Full War

            } while (tempCount != 0);
            save_movement(HStrB, name);         //<--Print movements
            save_infectedBy(name, arr1);        //<--Print victims record
            save_doingTheInfection(name, arr2); //<--Print Attackers record
            //Save Final Results
            save_final_results(name, arr1.Count, zombieArr.Count, count, humanArr.Count, arr2.Count);
        }
    }
}



/**************************************************************************************************************
  * 
  * Failed attempt to reflect the simulation visually my creating new matrix each iteration ""update_matrix()", 
  * and passing the new matrix to the second method "war_simulation()" along with a Grid.
  * And need to modify the parameters of start_war() method to accept myGrid as parameter.
  * The result was the last matrix that contains only zombies, however, all styles and colors removed, so 
  * i stopped here and commented both function.
  * 
  * ***********************************************************************************************************
  * Upgrade matrix  
***************************************************************************************************************/
//public static string[,] update_matrix(string[,] mx, int row, int col, IList<Human> arr1, IList<Zombie> arr2)
//{
//    string[,] updatedMatrix = new string[row, col];
//    StringBuilder tempSB = new StringBuilder();
//    tempSB.Append("");
//    for (int i = 0; i < mx.GetLength(0); i++)
//    {
//        for (int j = 0; j < mx.GetLength(1); j++)
//        {
//            tempSB.Clear();
//            tempSB.Append(" ");
//            Point tempPt = new Point(i,j);
//            foreach (Human h in arr1)
//            {
//                Point tempCurrPtH = new Point(h.getCurrentPositoinH().X, h.getCurrentPositoinH().Y);
//                if (tempPt == tempCurrPtH) { tempSB.Append(h.Hname).Append(' '); }
//            }
//            foreach (Zombie z in arr2)
//            {
//                Point tempCurrPtZ = new Point(z.getCurrentPositoinZ().X, z.getCurrentPositoinZ().Y);
//                if (tempPt == tempCurrPtZ) { tempSB.Append(z.Zname).Append(' '); }
//            }
//            mx[i, j] = tempSB.ToString();
//        }
//    }
//    Debug.WriteLine("Inside updsate matrix");
//    return mx;
//}
/***************************************************************************************************************
  * Upgrade visual Grid <-- I tried to update the Grid at each iteration to create a visual simulation <-- Failed
****************************************************************************************************************/
//public static void war_simulation(string[,] mx, Grid myGrid)
//{
//    for (int i = 0; i < mx.GetLength(0); i++)
//    {
//        for (int j = 0; j < mx.GetLength(1); j++)
//        {
//            //myGrid.Children.Clear();
//            TextBox txtb = new TextBox();
//            txtb.Background = new SolidColorBrush(Colors.AliceBlue);   //Bg Color
//            txtb.Foreground = new SolidColorBrush(Colors.Black);       //Fg Color
//            txtb.Text = " ";
//            txtb.IsEnabled = false;
//            txtb.Height = 40;
//            txtb.Width = 40;
//            //txtb.Style = null;
//            txtb.AppendText(mx[i,j]);
//            //txtb.FontStyle();                                        // <- didn't solve color Bug 
//            Grid.SetColumn(txtb, j);                                   //Set Grid Column Size       
//            Grid.SetRow(txtb, i);                                      //Set Grid Row Size
//            myGrid.Children.Add(txtb);                                 //Add txtbox to the Grid
//            //txtb.Background = new SolidColorBrush(Colors.AliceBlue); //Bg Color
//            //txtb.Foreground = new SolidColorBrush(Colors.Black);     //Fg Color
//            //System.Threading.Thread.Sleep(1000);
//        }
//    }
//    Debug.WriteLine("Inside simulation");
//}

