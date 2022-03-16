﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;
namespace VSProject
{
    public partial class Form1 : Form
    {
        static class GlobalVariable
        {
            public static string output = "";
            public static Microsoft.Msagl.Drawing.Graph outputGraph = new Microsoft.Msagl.Drawing.Graph("graph");
            public static Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            public static int counter = 1;
        }
        [STAThread]
        public static void Main(string[] args)
        {
            // Starts the application.
            System.Console.WriteLine("fuck the World!");
            System.Console.WriteLine("this is the program entry point! im not sure this thing will show up");
            Form1 FolderCrawler = new Form1();

            //create a viewer object 
            
            //create a graph object 

            //create the graph content 
            // GlobalVariable.outputGraph.AddEdge("A", "B");
            // GlobalVariable.outputGraph.AddEdge("B", "C");
            //GlobalVariable.outputGraph.AddEdge("A", "C").Attr.Color = Microsoft.Msagl.Drawing.Color.Green;
            GlobalVariable.outputGraph.AddNode(GlobalVariable.counter.ToString());
            GlobalVariable.counter++;
            //GlobalVariable.outputGraph.FindNode("A").Attr.FillColor = Microsoft.Msagl.Drawing.Color.Magenta;
            //GlobalVariable.outputGraph.FindNode("B").Attr.FillColor = Microsoft.Msagl.Drawing.Color.MistyRose;

            //bind the graph to the viewer 
            GlobalVariable.viewer.Graph = GlobalVariable.outputGraph;
            //associate the viewer with the form 
            FolderCrawler.SuspendLayout();
            GlobalVariable.viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            FolderCrawler.Controls.Add(GlobalVariable.viewer);


            Application.Run(FolderCrawler);
        }
        public static void BFS(string path)
        {
            // data yg disimpen itu <parent id, self id, diretory full name>
            int idcounter = 0;
            Queue<Tuple<int,int, string>> myQueue = new Queue<Tuple<int,int, string>>();
            myQueue.Enqueue(new Tuple<int,int, string>(idcounter,idcounter, path));

            while (myQueue.Count != 0)
            {
                Tuple<int, int, string> currentNode = myQueue.Dequeue();
                string currentDirectory = currentNode.Item3;
                DirectoryInfo d = new DirectoryInfo(currentDirectory);
                if (Directory.Exists(currentDirectory))
                {
                    DirectoryInfo[] Directories = d.GetDirectories(); // getting directories
                    FileInfo[] Files = d.GetFiles(); // getting files
                    foreach (DirectoryInfo directory in Directories)
                    {
                        myQueue.Enqueue(new Tuple<int,int, string>(currentNode.Item2,++idcounter,directory.FullName));

                    }

                    foreach (FileInfo file in Files)
                    {
                        myQueue.Enqueue(new Tuple<int,int , string>(currentNode.Item2, ++idcounter, file.FullName));
                    }
                }

                //this is where you code the 'action'
                //GlobalVariable.output = GlobalVariable.output + d.Name + "\n";
                GlobalVariable.outputGraph.AddNode(currentNode.Item2.ToString()).Label.Text = currentNode.Item1 + ", " + currentNode.Item2 + ", "+  d.Name;
                if(currentNode.Item2 != 0)
                {
                    GlobalVariable.outputGraph.AddEdge(currentNode.Item1.ToString(), currentNode.Item2.ToString());
                }


            }
        }

        public static void DFS(string path)
        {
            DirectoryInfo d = new DirectoryInfo(path);

            DirectoryInfo[] Directories = d.GetDirectories(); // getting directories
            FileInfo[] Files = d.GetFiles(); // getting files

            foreach (DirectoryInfo directory in Directories)
            {
                //this is where you code the 'action'
                GlobalVariable.output = GlobalVariable.output + directory.Name + "\n";
                DFS(@directory.FullName);
            }

            foreach (FileInfo file in Files)
            {
                //this is where you code the 'action'
                GlobalVariable.output = GlobalVariable.output + file.Name + "\n";
            }
        }
        public Form1()
        {
            InitializeComponent();
        }
        

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnHelloWorld_Click(object sender, EventArgs e)
        {
            //GlobalVariable.outputGraph.AddNode(GlobalVariable.counter.ToString()).Label.Text = "testing";
            //GlobalVariable.outputGraph.AddEdge("1", "2");


            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            DialogResult result = folderDlg.ShowDialog();
           
            if (result == DialogResult.OK)
            {
                lblDirectory.Text = folderDlg.SelectedPath;
            }
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void btnBFS_Click(object sender, EventArgs e)
        {
            GlobalVariable.counter = 0;
            GlobalVariable.output = "";
            BFS(@lblDirectory.Text);
            lblOutput.Text = GlobalVariable.output;
            // outputing to graph
            GlobalVariable.viewer.Graph = GlobalVariable.outputGraph;

        }

        private void btnDFS_Click(object sender, EventArgs e)
        {
            GlobalVariable.output = "";
            DFS(@lblDirectory.Text);
            lblOutput.Text = GlobalVariable.output;
        }
    }
}
