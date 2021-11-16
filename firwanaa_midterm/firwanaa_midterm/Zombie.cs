/***********************************
 * @Instructor Prof. Dario Guiao   *
 * @Autor: Alqassam Firwana        *
 * @id: __________                 *
 * MidTerm Project                 * 
 * Zombies Vs Humans               *
 * Zombie  Object  Class           * 
 ***********************************/


using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows;
using System.Diagnostics;

namespace firwanaa_midterm
{
    public class Zombie : ZHWar
    {
        public static int nameNumZ = 0;                                     //name counter
        List<StringBuilder> infectingList = new List<StringBuilder>();      //collecting preys
        List<Point> pointListZombie = new List<Point>();                    //Saving coordinates
        private Point currentPositionZ;                                     //Current Position
        public string Zname { get; set; }                                   //Zombies names <-- Auto-Properties
        private IDictionary<int, int> Zrecord = new Dictionary<int, int>(); //Dictionary to save start configuration


        /*****************************************************************
            *Returns Preys Count
        ******************************************************************/
        public int get_count()
        {
            return infectingList.Count;
        }

        /*****************************************************************
            *Zombies Constructor  <-- Giving them unique names
        ******************************************************************/
        public Zombie()
        {
            Zname = "Z" + nameNumZ;
            nameNumZ++;
        }

        /*****************************************************************
            *Adding new coordinates to points list
        ******************************************************************/
        public void addptZ(int a, int b)
        {
            Point pt = new Point(a, b);
            pointListZombie.Add(pt);
        }

        /*****************************************************************
            *Adding start coordinates to points list & record
        ******************************************************************/
        public void addZ(int a, int b)
        {
            Point pt = new Point(a, b);
            Zrecord.Add(a, b);
            pointListZombie.Add(pt);
        }

        /*****************************************************************
            *Returns record of start configurations
        ******************************************************************/
        public IDictionary<int, int> getZRecord()
        {
            return Zrecord;
        }


        /*****************************************************************
            *Sets Current Coordinates
        ******************************************************************/
        public void setCurrentPositoinZ(int a, int b)
        {
            currentPositionZ = new Point(a, b);
        }

        /*****************************************************************
            *Returns Current coordinates
        ******************************************************************/
        public Point getCurrentPositoinZ()
        {
            return currentPositionZ;
        }

        /*****************************************************************
            *Setting Record of Preys names and last known coordinates
        ******************************************************************/
        public void setInfecteingZ(string s, int i)
        {
            StringBuilder infecting = new StringBuilder();
            infecting.Append(" Infected Human ").Append(s).Append(" at Iteration ").Append(i);
            infectingList.Add(infecting);

        }

        /*****************************************************************
            *Returns all preys & coordinates for particular Zombie
        ******************************************************************/
        public string getInfectingZ()
        {
            StringBuilder tempSb = new StringBuilder();
            tempSb.Append("Zombie ").Append(Zname).Append(" : ");
            string combindedString = String.Join(",", infectingList);     //<--- got the comma ^_^
            tempSb.Append(combindedString);
            return tempSb.ToString();
        }
    }
}
