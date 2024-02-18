using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OTTER
{
    
    public partial class IzbornikForma : Form
    {
        public List<Player> rang = new List<Player>();
        public Player player;
        public bool novijeigrac;
        public IzbornikForma()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BGL igra = new BGL();
            novijeigrac = true;
            player = new Player(txtbox_ime.Text, 0);            
            igra.frmIzbornik = this;
            igra.igrac = player;
            rang.Add(player);
            igra.ShowDialog();                      
        }

        private void btn_rang_Click(object sender, EventArgs e)
        {
            Rang_List r = new Rang_List();
            r.ShowDialog();           
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_play_Again_Click(object sender, EventArgs e)
        {
            if (rang.Count == 0)
            {
                MessageBox.Show("Unesite novog igrača i započnite novu igru!");
                return;
            }
            BGL igra = new BGL();
            novijeigrac = false;
            igra.frmIzbornik = this;
            igra.igrac = player;
            igra.ShowDialog();
        }
    }
}
