/***********************************
 * @Instructor Prof. Dario Guiao   *
 * @Autor: Alqassam Firwana        *
 * @id:                            *
 * MidTerm Project                 * 
 * Zombies Vs Humans               *
 * Human Object Class              * 
 ***********************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Diagnostics;
using System.Windows;

namespace firwanaa_midterm
{
    public class Human : ZHWar
    {
        public static int nameNumH = 0;                                     //name counter
        private StringBuilder infectedBy = new StringBuilder();             //Attackers name
        private Point currentPositionH;                                     //Current Coordinates
        List<Point> pointListHuman = new List<Point>();                     //Human all Coordinates list
        public string Hname { get; set; }                                   //Human name <-- Auto-Properties
        private IDictionary<int, int> Hrecord = new Dictionary<int, int>(); //Human Start Configuration

        /*****************************************************************
            *Humans obj Constructor  <-- Giving them unique names
        ******************************************************************/
        public Human()
        {
            Hname = "H" + nameNumH;
            nameNumH++;
        }

        /*****************************************************************
            *Adding new coordinates to points list
        ******************************************************************/
        public void addptH(int a, int b)
        {
            Point pt = new Point(a, b);
            pointListHuman.Add(pt);
        }

        /*****************************************************************
            *Adding start coordinates to points list & record
        ******************************************************************/
        public void addH(int a, int b)
        {
            //Hrecord.Clear(); <-- Comment for now
            Point pt = new Point(a, b);
            Hrecord.Add(a, b);
            pointListHuman.Add(pt);

        }

        /*****************************************************************
           *Returns record of start configurations
        ******************************************************************/
        public IDictionary<int, int> getHRecord()
        {
            return Hrecord;
        }


        /*****************************************************************
            *Sets Current Coordinates
        ******************************************************************/
        public void setCurrentPositoinH(int a, int b)
        {
            currentPositionH = new Point(a, b);
        }

        /*****************************************************************
            *Returns Current Coordinates
        ******************************************************************/
        public Point getCurrentPositoinH()
        {
            return currentPositionH;
        }

        /*****************************************************************
            *Setting Record of Attacker name and attack location
        ******************************************************************/
        public void setInfectedByH(string s, int i)
        {
            infectedBy.Append("Human ").Append(Hname).Append(" infected by: ").Append(s).Append(" at Iteration ").Append(i);
        }

        /*****************************************************************
            *Returns Attackers details
        ******************************************************************/
        public StringBuilder getInfectedByH()
        {
            return infectedBy;
        }

    }
}
