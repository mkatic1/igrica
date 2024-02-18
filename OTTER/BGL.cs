using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace OTTER
{
    
    //public Dictionary<string, int> rang = new Dictionary<string, int>();

    /// <summary>
    /// -
    /// </summary>
    public partial class BGL : Form
    {
        public IzbornikForma frmIzbornik;
        public Player igrac;

        /* ------------------- */
        #region Environment Variables

        List<Func<int>> GreenFlagScripts = new List<Func<int>>();

        /// <summary>
        /// Uvjet izvršavanja igre. Ako je <c>START == true</c> igra će se izvršavati.
        /// </summary>
        /// <example><c>START</c> se često koristi za beskonačnu petlju. Primjer metode/skripte:
        /// <code>
        /// private int MojaMetoda()
        /// {
        ///     while(START)
        ///     {
        ///       //ovdje ide kod
        ///     }
        ///     return 0;
        /// }</code>
        /// </example>
        public static bool START = true;
        //sprites
        /// <summary>
        /// Broj likova.
        /// </summary>
        public static int spriteCount = 0, soundCount = 0;

        /// <summary>
        /// Lista svih likova.
        /// </summary>
        //public static List<Sprite> allSprites = new List<Sprite>();
        public static SpriteList<Sprite> allSprites = new SpriteList<Sprite>();

        //sensing
        int mouseX, mouseY;
        Sensing sensing = new Sensing();

        //background
        List<string> backgroundImages = new List<string>();
        int backgroundImageIndex = 0;
        string ISPIS = "";

        SoundPlayer[] sounds = new SoundPlayer[1000];
        TextReader[] readFiles = new StreamReader[1000];
        TextWriter[] writeFiles = new StreamWriter[1000];
        bool showSync = false;
        int loopcount;
        DateTime dt = new DateTime();
        String time;
        double lastTime, thisTime, diff;

        #endregion
        /* ------------------- */
        #region Events

        private void Draw(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            try
            {                
                foreach (Sprite sprite in allSprites)
                {                    
                    if (sprite != null)
                        if (sprite.Show == true)
                        {
                            g.DrawImage(sprite.CurrentCostume, new Rectangle(sprite.X, sprite.Y, sprite.Width, sprite.Heigth));
                        }
                    if (allSprites.Change)
                        break;
                }
                if (allSprites.Change)
                    allSprites.Change = false;
            }
            catch
            {
                //ako se doda sprite dok crta onda se mijenja allSprites
                MessageBox.Show("Greška!");
            }
        }

        private void startTimer(object sender, EventArgs e)
        {
            START = true;
            frmIzbornik.Hide();

            timer1.Start();
            timer2.Start();
            Init();
        }

        private void updateFrameRate(object sender, EventArgs e)
        {
            updateSyncRate();
        }

        /// <summary>
        /// Crta tekst po pozornici.
        /// </summary>
        /// <param name="sender">-</param>
        /// <param name="e">-</param>
        public void DrawTextOnScreen(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            var brush = new SolidBrush(Color.WhiteSmoke);
            string text = ISPIS;

            SizeF stringSize = new SizeF();
            Font stringFont = new Font("Arial", 14);
            stringSize = e.Graphics.MeasureString(text, stringFont);

            using (Font font1 = stringFont)
            {
                RectangleF rectF1 = new RectangleF(0, 0, stringSize.Width, stringSize.Height);
                e.Graphics.FillRectangle(brush, Rectangle.Round(rectF1));
                e.Graphics.DrawString(text, font1, Brushes.Black, rectF1);
            }
        }

        private void mouseClicked(object sender, MouseEventArgs e)
        {
            //sensing.MouseDown = true;
            sensing.MouseDown = true;
        }

        private void mouseDown(object sender, MouseEventArgs e)
        {
            //sensing.MouseDown = true;
            sensing.MouseDown = true;            
        }

        private void mouseUp(object sender, MouseEventArgs e)
        {
            //sensing.MouseDown = false;
            sensing.MouseDown = false;
        }

        private void mouseMove(object sender, MouseEventArgs e)
        {
            mouseX = e.X;
            mouseY = e.Y;

            //sensing.MouseX = e.X;
            //sensing.MouseY = e.Y;
            //Sensing.Mouse.x = e.X;
            //Sensing.Mouse.y = e.Y;
            sensing.Mouse.X = e.X;
            sensing.Mouse.Y = e.Y;

        }

        private void keyDown(object sender, KeyEventArgs e)
        {
            sensing.Key = e.KeyCode.ToString();
            sensing.KeyPressedTest = true;
        }

        private void keyUp(object sender, KeyEventArgs e)
        {
            sensing.Key = "";
            sensing.KeyPressedTest = false;
        }

        private void Update(object sender, EventArgs e)
        {
            if (sensing.KeyPressed(Keys.Escape))
            {
                START = false;
            }

            if (START)
            {
                this.Refresh();
            }
        }

        #endregion
        /* ------------------- */
        #region Start of Game Methods

        //my
        #region my

        //private void StartScriptAndWait(Func<int> scriptName)
        //{
        //    Task t = Task.Factory.StartNew(scriptName);
        //    t.Wait();
        //}

        //private void StartScript(Func<int> scriptName)
        //{
        //    Task t;
        //    t = Task.Factory.StartNew(scriptName);
        //}

        private int AnimateBackground(int intervalMS)
        {
            while (START)
            {
                setBackgroundPicture(backgroundImages[backgroundImageIndex]);
                Game.WaitMS(intervalMS);
                backgroundImageIndex++;
                if (backgroundImageIndex == 3)
                    backgroundImageIndex = 0;
            }
            return 0;
        }

        private void KlikNaZastavicu()
        {
            foreach (Func<int> f in GreenFlagScripts)
            {
                Task.Factory.StartNew(f);
            }
        }

        #endregion

        /// <summary>
        /// BGL
        /// </summary>
        public BGL()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Pričekaj (pauza) u sekundama.
        /// </summary>
        /// <example>Pričekaj pola sekunde: <code>Wait(0.5);</code></example>
        /// <param name="sekunde">Realan broj.</param>
        public void Wait(double sekunde)
        {
            int ms = (int)(sekunde * 1000);
            Thread.Sleep(ms);
        }

        //private int SlucajanBroj(int min, int max)
        //{
        //    Random r = new Random();
        //    int br = r.Next(min, max + 1);
        //    return br;
        //}

        /// <summary>
        /// -
        /// </summary>
        public void Init()
        {
            if (dt == null) time = dt.TimeOfDay.ToString();
            loopcount++;
            //Load resources and level here
            this.Paint += new PaintEventHandler(DrawTextOnScreen);
            SetupGame();
        }

        /// <summary>
        /// -
        /// </summary>
        /// <param name="val">-</param>
        public void showSyncRate(bool val)
        {
            showSync = val;
            if (val == true) syncRate.Show();
            if (val == false) syncRate.Hide();
        }

        /// <summary>
        /// -
        /// </summary>
        public void updateSyncRate()
        {
            if (showSync == true)
            {
                thisTime = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
                diff = thisTime - lastTime;
                lastTime = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;

                double fr = (1000 / diff) / 1000;

                int fr2 = Convert.ToInt32(fr);

                syncRate.Text = fr2.ToString();
            }

        }

        //stage
        #region Stage

        /// <summary>
        /// Postavi naslov pozornice.
        /// </summary>
        /// <param name="title">tekst koji će se ispisati na vrhu (naslovnoj traci).</param>
        public void SetStageTitle(string title)
        {
            this.Text = title;
        }

        /// <summary>
        /// Postavi boju pozadine.
        /// </summary>
        /// <param name="r">r</param>
        /// <param name="g">g</param>
        /// <param name="b">b</param>
        public void setBackgroundColor(int r, int g, int b)
        {
            this.BackColor = Color.FromArgb(r, g, b);
        }

        /// <summary>
        /// Postavi boju pozornice. <c>Color</c> je ugrađeni tip.
        /// </summary>
        /// <param name="color"></param>
        public void setBackgroundColor(Color color)
        {
            this.BackColor = color;
        }

        /// <summary>
        /// Postavi sliku pozornice.
        /// </summary>
        /// <param name="backgroundImage">Naziv (putanja) slike.</param>
        public void setBackgroundPicture(string backgroundImage)
        {
            this.BackgroundImage = new Bitmap(backgroundImage);
        }

        /// <summary>
        /// Izgled slike.
        /// </summary>
        /// <param name="layout">none, tile, stretch, center, zoom</param>
        public void setPictureLayout(string layout)
        {
            if (layout.ToLower() == "none") this.BackgroundImageLayout = ImageLayout.None;
            if (layout.ToLower() == "tile") this.BackgroundImageLayout = ImageLayout.Tile;
            if (layout.ToLower() == "stretch") this.BackgroundImageLayout = ImageLayout.Stretch;
            if (layout.ToLower() == "center") this.BackgroundImageLayout = ImageLayout.Center;
            if (layout.ToLower() == "zoom") this.BackgroundImageLayout = ImageLayout.Zoom;
        }

        #endregion

        //sound
        #region sound methods

        /// <summary>
        /// Učitaj zvuk.
        /// </summary>
        /// <param name="soundNum">-</param>
        /// <param name="file">-</param>
        public void loadSound(int soundNum, string file)
        {
            soundCount++;
            sounds[soundNum] = new SoundPlayer(file);
        }

        /// <summary>
        /// Sviraj zvuk.
        /// </summary>
        /// <param name="soundNum">-</param>
        public void playSound(int soundNum)
        {
            sounds[soundNum].Play();
        }

        /// <summary>
        /// loopSound
        /// </summary>
        /// <param name="soundNum">-</param>
        public void loopSound(int soundNum)
        {
            sounds[soundNum].PlayLooping();
        }

        /// <summary>
        /// Zaustavi zvuk.
        /// </summary>
        /// <param name="soundNum">broj</param>
        public void stopSound(int soundNum)
        {
            sounds[soundNum].Stop();
        }

        #endregion

        //file
        #region file methods

        /// <summary>
        /// Otvori datoteku za čitanje.
        /// </summary>
        /// <param name="fileName">naziv datoteke</param>
        /// <param name="fileNum">broj</param>
        public void openFileToRead(string fileName, int fileNum)
        {
            readFiles[fileNum] = new StreamReader(fileName);
        }

        /// <summary>
        /// Zatvori datoteku.
        /// </summary>
        /// <param name="fileNum">broj</param>
        public void closeFileToRead(int fileNum)
        {
            readFiles[fileNum].Close();
        }

        /// <summary>
        /// Otvori datoteku za pisanje.
        /// </summary>
        /// <param name="fileName">naziv datoteke</param>
        /// <param name="fileNum">broj</param>
        public void openFileToWrite(string fileName, int fileNum)
        {
            writeFiles[fileNum] = new StreamWriter(fileName);
        }

        /// <summary>
        /// Zatvori datoteku.
        /// </summary>
        /// <param name="fileNum">broj</param>
        public void closeFileToWrite(int fileNum)
        {
            writeFiles[fileNum].Close();
        }

        /// <summary>
        /// Zapiši liniju u datoteku.
        /// </summary>
        /// <param name="fileNum">broj datoteke</param>
        /// <param name="line">linija</param>
        public void writeLine(int fileNum, string line)
        {
            writeFiles[fileNum].WriteLine(line);
        }

        /// <summary>
        /// Pročitaj liniju iz datoteke.
        /// </summary>
        /// <param name="fileNum">broj datoteke</param>
        /// <returns>vraća pročitanu liniju</returns>
        public string readLine(int fileNum)
        {
            return readFiles[fileNum].ReadLine();
        }

        /// <summary>
        /// Čita sadržaj datoteke.
        /// </summary>
        /// <param name="fileNum">broj datoteke</param>
        /// <returns>vraća sadržaj</returns>
        public string readFile(int fileNum)
        {
            return readFiles[fileNum].ReadToEnd();
        }

        #endregion

        //mouse & keys
        #region mouse methods

        /// <summary>
        /// Sakrij strelicu miša.
        /// </summary>
        public void hideMouse()
        {
            Cursor.Hide();
        }

        /// <summary>
        /// Pokaži strelicu miša.
        /// </summary>
        public void showMouse()
        {
            Cursor.Show();
        }

        /// <summary>
        /// Provjerava je li miš pritisnut.
        /// </summary>
        /// <returns>true/false</returns>
        public bool isMousePressed()
        {
            //return sensing.MouseDown;
            return sensing.MouseDown;
        }

        /// <summary>
        /// Provjerava je li tipka pritisnuta.
        /// </summary>
        /// <param name="key">naziv tipke</param>
        /// <returns></returns>
        public bool isKeyPressed(string key)
        {
            if (sensing.Key == key)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Provjerava je li tipka pritisnuta.
        /// </summary>
        /// <param name="key">tipka</param>
        /// <returns>true/false</returns>
        public bool isKeyPressed(Keys key)
        {
            if (sensing.Key == key.ToString())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #endregion
        /* ------------------- */

        /* ------------ GAME CODE START ------------ */

        /* Game variables */


        /* Initialization */
        bool collectingallcoins;
        int score=0;
        int stoperica = 0;
        Sprite background1;
        Sprite background2;
        Sprite klokan;
        Power power1;
        Kamen_Smedi smedi;
        Kamen_Sivi sivi;
        Grm grm;
        Coin coin1;
        Coin coin2;
        Coin coin3;
        Bomb bomb1;
        Bomb bomb2;
        List<Sprite> prepreke = new List<Sprite>();
        List<Sprite> coins_bombs = new List<Sprite>();



        private void SetupGame()
        {
            //1. setup stage
            //SetStageTitle("Coins");
            SetStageTitle(igrac.Ime);
            //setBackgroundColor(Color.WhiteSmoke);            
            //setBackgroundPicture("backgrounds\\pozadina2.jpg");
            //none, tile, stretch, center, zoom
            setPictureLayout("stretch");

            //2. add sprites
            background1 = new Sprite("backgrounds\\pozadina1.jpg",0,0);
            Game.AddSprite(background1);
            background2 = new Sprite("backgrounds\\pozadina1.jpg", 990, 0);
            Game.AddSprite(background2);

            klokan = new Sprite("sprites\\klokan.png", 500, 280);
            Game.AddSprite(klokan);

            sivi = new Kamen_Sivi("backgrounds\\kamensiv.png", 1000, 340);
            sivi.SetSize(40);
            Game.AddSprite(sivi);

            smedi = new Kamen_Smedi("backgrounds\\kamensmedi.png", 2000, 340);
            smedi.SetSize(50);
            Game.AddSprite(smedi);

            grm = new Grm("backgrounds\\grm.png", 1500, 330);
            grm.SetSize(40);
            Game.AddSprite(grm);

            coin1 = new Coin("backgrounds\\coin.png", 1000, 220);
            coin1.SetSize(40);
            Game.AddSprite(coin1);

            coin2 = new Coin("backgrounds\\coin.png", 1350, 330);
            coin2.SetSize(40);
            Game.AddSprite(coin2);

            coin3 = new Coin("backgrounds\\coin.png", 1700, 100);
            coin3.SetSize(40);
            Game.AddSprite(coin3);

            bomb1 = new Bomb("backgrounds\\bomb.png", 1200, 220);
            bomb1.SetSize(60);
            Game.AddSprite(bomb1);

            bomb2 = new Bomb("backgrounds\\bomb.png", 1400, 340);
            bomb2.SetSize(60);
            Game.AddSprite(bomb2);

            power1 = new Power("backgrounds\\wings.png", 1500, 220);
            power1.SetSize(20);
            Game.AddSprite(power1);

            

            prepreke.Add(smedi);
            prepreke.Add(sivi);
            prepreke.Add(grm);

            coins_bombs.Add(coin1);
            coins_bombs.Add(coin2);
            coins_bombs.Add(coin3);
            coins_bombs.Add(bomb1);
            coins_bombs.Add(bomb2);

           

            
            

            //3. scripts that start
            Game.StartScript(MovingBackground);
            Game.StartScript(Moving_Prepreke);
            Game.StartScript(Moving_Coin_Bomb);
            Game.StartScript(Moving_Power);
            Game.StartScript(Jump);
            Game.StartScript(Collect);
            
            Game.StartScript(Touching_Prepreke);
        }

        /* Scripts */

        private int Metoda()
        {
            while (START) //ili neki drugi uvjet
            {

                Wait(0.1);
            }
            return 0;
        }
        private int MovingBackground()
        {
            while (START) //ili neki drugi uvjet
            {
                
                if (background1.X == -990)
                {
                    background1.X =989;
                }
                else if(background2.X ==-990)
                {
                    background2.X = 989;
                }
                background1.X -= 1;
                background2.X -= 1;
                Wait(0.01);
            }
            return 0;
        }

        private int Moving_Prepreke()
        {
            Random g = new Random();
            
            while (START)
            {
                for (int i = 0; i < prepreke.Count; i++)
                {
                    prepreke[i].X -= 2;
                }
                for (int i = 0; i < prepreke.Count; i++)
                {
                    for (int j = 0; j < prepreke.Count; j++)
                    {
                        if (Math.Abs(prepreke[i].X - prepreke[j].X) < 500 && prepreke[i] != prepreke[j])
                        {
                            if (prepreke[i].X == prepreke[j].X)
                            {
                                prepreke[j].X += 200;
                            }
                            else if(prepreke[i].X < prepreke[j].X)
                            {
                                prepreke[j].X += 200;
                            }
                            else if(prepreke[i].X > prepreke[j].X)
                            {
                                prepreke[i].X += 200;
                            }
                        }
                    }                    
                }
                for (int i = 0; i < prepreke.Count; i++)
                {
                    
                    if (prepreke[i].X < -150)
                    {
                        prepreke[i].X = g.Next(1000,1500);
                    }
                }
                Wait(0.01);
            }
            
            return 0;
        }

        private int Moving_Coin_Bomb()
        {
            Random f = new Random();
            while (START)
            {
                for (int i = 0; i < coins_bombs.Count; i++)
                {
                    coins_bombs[i].X -= 1;
                }
                for (int i = 0; i < coins_bombs.Count; i++)
                {
                    for (int j = 0; j < coins_bombs.Count; j++)
                    {
                        if (Math.Abs(coins_bombs[i].X - coins_bombs[j].X) < 150 && coins_bombs[i] != coins_bombs[j])
                        {
                            if (coins_bombs[i].X == coins_bombs[j].X)
                            {
                                coins_bombs[j].X += 150;
                            }
                            else if (coins_bombs[i].X < coins_bombs[j].X)
                            {
                                coins_bombs[j].X += 150;
                            }
                            else if (coins_bombs[i].X > coins_bombs[j].X)
                            {
                                coins_bombs[i].X += 150;
                            }
                        }
                    }
                    if (Math.Abs(coins_bombs[i].X - power1.X) < 200)
                    {
                        coins_bombs[i].X += 50;

                    }
                }
                for (int i = 0; i < coins_bombs.Count; i++)
                {

                    if (coins_bombs[i].X < -50)
                    {
                        coins_bombs[i].X = f.Next(1000,1500);
                    }
                }
                for (int i = 0; i < coins_bombs.Count; i++)
                {
                    for (int j = 0; j < prepreke.Count; j++)
                    {
                        if (coins_bombs[i].TouchingSprite(prepreke[j]))
                        {
                            coins_bombs[i].X += 200;
                        }
                        else if ((Math.Abs(coins_bombs[3].X - prepreke[j].X)) < 190)
                        {
                            coins_bombs[3].X += 20;
                        }
                        else if ((Math.Abs(coins_bombs[4].X - prepreke[j].X)) < 190)
                        {
                            coins_bombs[4].X += 20;
                        }

                    }
                    
                }
                for (int i = 0; i < coins_bombs.Count; i++)
                {
                    coins_bombs[i].X -= 1;
                }   
              
                Wait(0.01);
            }
            return 0;
        }
        //private int Moving_Bombs()
        //{
        //    Random f = new Random();
        //    while (START)
        //    {
        //        for (int i = 0; i < bombs.Count; i++)
        //        {
        //            bombs[i].X -= 1;
        //        }
        //        for (int i = 0; i < bombs.Count; i++)
        //        {
        //            for (int j = 0; j < bombs.Count; j++)
        //            {
        //                if (Math.Abs(bombs[i].X - bombs[j].X) < 200 && bombs[i] != bombs[j])
        //                {
        //                    if (bombs[i].X == bombs[j].X)
        //                    {
        //                        bombs[j].X += 300;
        //                    }
        //                    else if (bombs[i].X < bombs[j].X)
        //                    {
        //                        bombs[j].X += 300;
        //                    }
        //                    else if (bombs[i].X > bombs[j].X)
        //                    {
        //                        bombs[i].X += 300;
        //                    }
        //                }
        //            }
        //        }
        //        for (int i = 0; i < bombs.Count; i++)
        //        {

        //            if (bombs[i].X < -50)
        //            {
        //                bombs[i].X = f.Next(1000, 1500);
        //            }
        //        }
        //        for (int i = 0; i < bombs.Count; i++)
        //        {
        //            for (int j = 0; j < prepreke.Count; j++)
        //            {
        //                if (bombs[i].TouchingSprite(prepreke[j]))
        //                {
        //                    bombs[i].X += 100;
        //                }

        //            }

        //        }
        //        for (int i = 0; i < bombs.Count; i++)
        //        {
        //            bombs[i].X -= 1;
        //        }

        //        Wait(0.01);
        //    }
        //    return 0;
        //}

        private int Moving_Power()
        {
            Random h = new Random();
            while (START)
            {
                power1.X -= 2;
                if (power1.X < -100)
                {
                    power1.X = h.Next(3000,4000);
                }
                Wait(0.01);
            }
                
                return 0;
        }

        private int Jump()
        {
            while (START)
            {
                if (sensing.KeyPressed(Keys.Space))
                {
                    while (true)
                    {
                        klokan.Y -= 3;
                        Wait(0.01);
                        if (klokan.Y <= 150)
                        {
                            break;
                        }
                    }
                    while (true)
                    {
                        klokan.Y += 3;
                        Wait(0.01);
                        if (klokan.Y == 280)
                        {
                            break;
                        }
                    }

                }
            }
            return 0;
        }

        private int Collect()
        {
            Random k = new Random();
            
            while (START)
            {
                for (int i = 0; i < coins_bombs.Count; i++)
                {
                    if (klokan.TouchingSprite(coins_bombs[i]))
                    {
                        coins_bombs[i].X = 1200;
                        if(coins_bombs[i]==coin1| coins_bombs[i] == coin2| coins_bombs[i] == coin3)
                        {
                            score += 1;
                            PostaviTekstNaLabelu(score.ToString());
                        }
                        else if(coins_bombs[i] == bomb1| coins_bombs[i] == bomb2)
                        {
                            score -= 5;
                            PostaviTekstNaLabelu(score.ToString());
                        }                        
                    }
                    if (score < 0)
                    {
                        START = false;
                        RemoveSprites();
                        this.CloseForm();
                        igrac.Sc = score;
                        MessageBox.Show("GAME OVER" + "\n Score:" + score);
                        
                        
                    }
                }
                if ( klokan.TouchingSprite( power1))
                {
                    power1.X = k.Next(3000, 3500);
                    stoperica += 20;
                    collectingallcoins = true;
                    Game.StartScript(Collectall);
                    Game.StartScript(Stoperica);
                }
            }
            return 0;
        }
        private int Stoperica()
        {

            while (collectingallcoins)
            {
                Wait(1);
                PostaviTekstNaLabeluStoperica(stoperica.ToString());
                stoperica--;
                if (stoperica < 0)
                {
                    collectingallcoins = false;
                    stoperica = 0;
                    break;
                }
            }          
            
            return 0;
        }
        private int Collectall()
        {

            while (collectingallcoins)
            {
                if (klokan.X == coin1.X)
                {
                    coin1.X = 1500;
                    score += 1;
                }
                else if (klokan.X == coin2.X)
                {
                    coin2.X = 1500;
                    score += 1;
                }
                else if (klokan.X == coin3.X)
                {
                    coin3.X = 1500;
                    score += 1;
                }
                
            }
            Wait(stoperica);
            collectingallcoins = false;           
            return 0;
        }

        



        delegate void DelegatTipaVoid(string text);

        

        private void PostaviTekstNaLabelu(string t)
        {  
            if (this.InvokeRequired)
            {
                DelegatTipaVoid d = new DelegatTipaVoid(PostaviTekstNaLabelu);
                this.Invoke(d, new object[] { t });
            }
            else
            {
                this.lbl_score_broj.Text = t;
            }
        }
        private void PostaviTekstNaLabeluStoperica(string v)
        {
            if (this.InvokeRequired)
            {
                DelegatTipaVoid s = new DelegatTipaVoid(PostaviTekstNaLabeluStoperica);
                this.Invoke(s, new object[] { v });
            }
            else
            {
                this.lbl_stoperica.Text = v;
            }
        }

       

        private void BGL_FormClosed(object sender, FormClosedEventArgs e)
        {
            START = false;
            RemoveSprites();
            this.frmIzbornik.Show();
        }

        private int Touching_Prepreke()
        {
            while (START)
            {
                for (int i = 0; i < prepreke.Count; i++)
                {
                    if (((Math.Abs(klokan.X - prepreke[i].X)) <= 60) & (Math.Abs(klokan.Y - prepreke[i].Y)) <= 60)
                    {
                        START = false;
                        RemoveSprites();
                        this.CloseForm();
                        igrac.Sc = score;                        
                        MessageBox.Show("GAME OVER"+"\n Score:"+score);

                        
                    }
                }
            }
            return 0;
        }
        private void RemoveSprites()
        {
            //vrati brojač na 0
            BGL.spriteCount = 0;
            //izbriši sve spriteove
            BGL.allSprites.Clear();
            //počisti memoriju
            GC.Collect();
            
        }
        delegate void DelegatZaClose();
        private void CloseForm()
        {
            if (this.InvokeRequired)
            {
                DelegatZaClose del = new DelegatZaClose(this.Close);
                this.Invoke(del, new object[] { });
            }
            else
            {
                this.Close();
            }
        }
        
        
        /* ------------ GAME CODE END ------------ */


    }
}
