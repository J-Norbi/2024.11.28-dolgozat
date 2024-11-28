using System;
using System.Drawing;
using System.Windows.Forms;

namespace _2024._11._28_dolgozat
{
    public partial class Form1 : Form
    {
        Timer electronMoveTimer = new Timer();
        Timer electronAddTimer = new Timer();
        int voltSize = 12;
        int voltSpeed = 0;
        int iranyX = 0;                 // 1 - jobb; 0 - fel vagy le; -1 - bal
        int iranyY = -1;                // 1 - le; 0 - jobb vagy bal; -1 - fel
        int electronEredmeny = 0;
        public Form1()
        {
            InitializeComponent();
            this.Text = "Dolgozat - Electron";
            voltSpeed = voltSize / 3;
            Start();
        }
        void Start()
        {
            electronMoveTimer.Interval = 16;
            electronAddTimer.Interval = 500;
            electronAddTimer.Start();
            electronMoveTimer.Start();

            AddEvents();
            //electronMoveTimer.Tick += ElectronMove;
            //electronAddTimer.Tick += AddElectron;
            AddElectron();
        }
        void AddEvents()
        {
            buttonUp.Click += UpVolatages;
            buttonDown.Click += DownVoltages;
        }
        void AddElectron()  //object s, EventArgs e
        {
            PictureBox electron = new PictureBox();
            this.Controls.Add(electron);
            electron.Location = new Point(638, 308);
            electron.Size = new Size(6, 6);
            electron.BackColor = Color.Red;
            electronMoveTimer.Tick += (ss, ee) =>
            {
                //  felfelé
                if (iranyX == 0 && iranyY == -1)
                {
                    if (electron.Top > felsoVezetek1.Top + 24 / 3)
                    {
                        electron.Top -= 24 / 3;
                    }
                    else
                    {
                        iranyX = -1;
                        iranyY = 0;
                        electron.Top = felsoVezetek1.Top - 2;
                    }
                }
                //  lefelé
                else if (iranyX == 0 && iranyY == 1)
                {
                    if (electron.Top < alsoVezetek.Top - voltSpeed)
                    {
                        electron.Top += voltSpeed;
                    }
                    else
                    {
                        iranyX = 1;
                        iranyY = 0;
                        electron.Top = alsoVezetek.Top - 2;
                    }
                }
                // balra
                else if (iranyX == -1 && iranyY == 0)
                {
                    if (electron.Left > feszszabalyzo.Right - 24/3)
                    {
                        electron.Left -= 24/3;
                    }
                    else if (electron.Left < feszszabalyzo.Right && electron.Left > balVezetek.Left + voltSpeed)
                    {
                        electron.Left -= voltSpeed;
                    }
                    else
                    {
                        iranyX = 0;
                        iranyY = 1;
                        electron.Left = balVezetek.Left - 2;
                    }
                }
                // jobbra
                else if (iranyX == 1 && iranyY == 0)
                {
                    if (electron.Left < alsoVezetek.Right - voltSpeed)
                    {
                        electron.Left += voltSpeed;
                    }
                    else
                    {
                        iranyX = 0;
                        iranyY = -1;
                        electron.Left = jobbVezetek.Left - 2;
                        electron.Top = 308;
                        //electronMoveTimer.Stop();
                        this.Controls.Remove(electron);
                        electronEredmeny++;
                        eredmeny.Text = $"Electrons passed: {electronEredmeny}";
                        AddElectron();
                    }
                }
            };
        }
        void UpVolatages(object s, EventArgs e)
        {
            if (voltSize < 24)
            {
                voltSize += 1;
                feszErtek.Text = $"{voltSize}V";
                voltSpeed = voltSize / 3;
            }
        }
        void DownVoltages(object s, EventArgs e)
        {
            if (voltSize > 1)
            {
                voltSize -= 1;
                feszErtek.Text = $"{voltSize}V";
                voltSpeed = voltSize / 3;
            }
        }
    }
}