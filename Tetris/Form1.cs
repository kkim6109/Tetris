using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public partial class Form1 : Form
    {
        int sizeMultiplier = 40;
        Graphics graphics;
        SolidBrush square;
        SolidBrush emptySquare;
        Pen box;
        Pen previewP;
        Pen savedP;
        List<Point> block;
        int tetrisBlock; //0 |__ 1 __| 2 [] 3 _|_ 4 s 5 z 6 |
        Random random = new Random();
        int[][] grid;
        bool tetris;
        int savedBlock;
        string path;
        int comingUp;
        List<Point> upcoming;
        int prevUp;
        List<int> lWidth;
        List<int> lHeight;
        int lowestU;
        List<int> lWidth2;
        List<int> lHeight2;
        int lowestU2;
        List<Point> prevBlock;
        Rectangle[] previewRects;
        bool endGame;
        int pSavedB;
        Rectangle[] savedRects;
        List<Point> saving;
        System.IO.Stream music = Tetris.Properties.Resources.Tetris;
        System.Media.SoundPlayer background;
        bool audioPlaying = false;
        int lines;
        int speed;
        bool clockwise = true;
        bool savedABlock;
        int level;
        float factorSize = 1;
        float prevFactorSize = 1;
        bool resize = false;
        Size formSize;
        bool startUp = false;
        bool paused;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "\\My Games\\EFTetris");
                path += "\\";
            }
            catch
            {
                path = "";
            }
            background = new System.Media.SoundPlayer(music);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!startUp)
            {
                startUp = true;
                if (Form1.ActiveForm != null)
                {
                    Rectangle workingArea = Screen.GetWorkingArea(System.Windows.Forms.Cursor.Position);
                    float areaRatio = 1080 / workingArea.Height;
                    Form1.ActiveForm.Size = new Size((int)(918 * areaRatio), (int)(950 * areaRatio));
                }
            }
            if (audioPlaying && endGame)
            {
                background.PlayLooping();
            }
            if (Form1.ActiveForm != null)
            {
                formSize = Form1.ActiveForm.Size;
            }
            else
            {
                formSize = new Size(918, 950);
            }
            string str;
            try
            {
                str = System.IO.File.ReadAllText(path + "Highscore.kev");
            }
            catch
            {
                str = "0";
            }
            highscore.Text = str;
            grid = new int[10][];
            for (int i = 0; i < 10; i++)
            {
                grid[i] = new int[20];
                for (int j = 0; j < 20; j++)
                {
                    grid[i][j] = -1;
                }
            }
            paused = endGame = savedABlock = tetris = timer1.Enabled = false;
            savedBlock = pSavedB = lowestU = lowestU2 = -1;
            speed = timer2.Interval = 500;
            previewRects = new Rectangle[4];
            savedRects = new Rectangle[4];
            block = new List<Point>();
            prevBlock = new List<Point>();
            lHeight = new List<int>();
            lWidth = new List<int>();
            lHeight2 = new List<int>();
            lWidth2 = new List<int>();
            upcoming = new List<Point>();
            saving = new List<Point>();
            score.Text = "0";
            levelDisplay.Text = "Level: 1";
            level = 1;
            lines = 0;
            graphics = this.CreateGraphics();
            Bitmap BackBuffer = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            Graphics tempGraphics = Graphics.FromImage(BackBuffer);
            emptySquare = new SolidBrush(Color.Gray);
            square = new SolidBrush(Color.White);
            box = new Pen(Color.Black);
            previewP = new Pen(Color.Yellow);
            savedP = new Pen(Color.MediumVioletRed);
            tempGraphics.FillRectangle(emptySquare, new Rectangle((int)(100 * factorSize), (int)(50 * factorSize), sizeMultiplier * 10, sizeMultiplier * 20));
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    tempGraphics.DrawRectangle(box, new Rectangle(i * sizeMultiplier + (int)(100 * factorSize), j * sizeMultiplier + (int)(50 * factorSize), sizeMultiplier, sizeMultiplier));
                }
            }
            // Preview Window
            tempGraphics.FillRectangle(emptySquare, new Rectangle(panel2.Location.X, panel2.Location.Y + (int)(120 * factorSize), panel2.Width + 2, panel2.Width + 2));
            tempGraphics.DrawRectangle(previewP, new Rectangle(panel2.Location.X - 1, panel2.Location.Y + (int)(120 * factorSize) - 1, panel2.Width + 2, panel2.Width + 2));

            // Saved Window
            tempGraphics.FillRectangle(emptySquare, new Rectangle(panel2.Location.X, (int)(50 * factorSize) + sizeMultiplier * 20 - panel2.Width, panel2.Width + 2, panel2.Width + 2));
            tempGraphics.DrawRectangle(savedP, new Rectangle(panel2.Location.X - 1, (int)(50 * factorSize) + sizeMultiplier * 20 - panel2.Width + 2, panel2.Width + 2, panel2.Width + 2));

            tempGraphics = previewBlock(tempGraphics);
            graphics.DrawImageUnscaled(BackBuffer, 0, 0);
            tempGraphics.Dispose();
            BackBuffer.Dispose();
            comingUp = random.Next(7);
            newBlock();
            timer2.Enabled = true;
        }

        private void newBlock()
        {
            tetrisBlock = comingUp;
            comingUp = random.Next(7);
            upcoming = blockLocations(comingUp);
            block = blockLocations(tetrisBlock);
        }

        private List<Point> blockLocations(int tempBlock)
        {
            List<Point> nBlock = new List<Point>();
            if (tempBlock == 0 || tempBlock == 1 || tempBlock == 3)
            {
                if (tempBlock == 0)
                {
                    nBlock.Add(new Point(3, -2));
                }
                else if (tempBlock == 1)
                {
                    nBlock.Add(new Point(5, -2));
                }
                else
                {
                    nBlock.Add(new Point(4, -2));
                }
                for (int i = 0; i < 3; i++)
                {
                    nBlock.Add(new Point(i + 3, -1));
                }
            }
            else if (tempBlock == 2 || tempBlock == 5)
            {
                int offset = 0;
                if (tempBlock == 5)
                {
                    offset = -1;
                }
                for (int i = 0; i < 2; i++)
                {
                        nBlock.Add(new Point(4 + offset + i, -1));
                        nBlock.Add(new Point(4 + i, -2));
                }
            }
            else if (tempBlock == 4)
            {
                for (int i = 0; i < 2; i++)
                {
                    nBlock.Add(new Point(4 + i, -1));
                    nBlock.Add(new Point(3 + i, -2));
                }
            }
            else if (tempBlock == 6)
            {
                for (int i = 0; i < 4; i++)
                {
                    nBlock.Add(new Point(3 + i, -1));
                }
            }
            return nBlock;
        }

        private Color blockColor(int tetBlock)
        {
            if (tetBlock == 0)
            {
                return Color.Blue;
            }
            else if (tetBlock == 1)
            {
                return Color.Orange;
            }
            else if (tetBlock == 2)
            {
                return Color.Yellow;
            }
            else if (tetBlock == 3)
            {
                return Color.Purple;
            }
            else if (tetBlock == 4)
            {
                return Color.Lime;
            }
            else if (tetBlock == 5)
            {
                return Color.Red;
            }
            else if (tetBlock == 6)
            {
                return Color.Aqua;
            }
            else
            {
                Console.WriteLine(tetBlock);
                return Color.AntiqueWhite;
            }
        }

        private void updateBoard()
        {
            Bitmap BackBuffer = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            Graphics tempGraphics = Graphics.FromImage(BackBuffer);
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    if (grid[i][j] == -1)
                    {
                        tempGraphics.FillRectangle(emptySquare, new Rectangle(i * sizeMultiplier + panel3.Location.X, j * sizeMultiplier + (int)(50 * factorSize), sizeMultiplier, sizeMultiplier));
                    }
                    else
                    {
                        square = new SolidBrush(blockColor(grid[i][j]));
                        tempGraphics.FillRectangle(square, new Rectangle(i * sizeMultiplier + panel3.Location.X, j * sizeMultiplier + (int)(50 * factorSize), sizeMultiplier, sizeMultiplier));
                    }
                    tempGraphics.DrawRectangle(box, new Rectangle(i * sizeMultiplier + panel3.Location.X, j * sizeMultiplier + (int)(50 * factorSize), sizeMultiplier, sizeMultiplier));
                }
            }
            tempGraphics.FillRectangle(emptySquare, new Rectangle(panel2.Location.X, (int)(50 * factorSize) + sizeMultiplier * 20 - panel2.Width, panel2.Width + 2, panel2.Width + 2));
            tempGraphics.DrawRectangle(savedP, new Rectangle(panel2.Location.X - 1, (int)(50 * factorSize) + sizeMultiplier * 20 - panel2.Width - 1, panel2.Width + 2, panel2.Width + 2));
            if (tetrisBlock != -1)
            {
                tempGraphics = preview(tempGraphics);
            }
            if (savedBlock != -1)
            {
                tempGraphics = savedBlockG(tempGraphics);
            }
            if (comingUp != -1)
            {
                tempGraphics = previewBlock(tempGraphics);
            }
            resize = false;
            graphics.DrawImageUnscaled(BackBuffer, 0, 0);
            BackBuffer.Dispose();
            tempGraphics.Dispose();
        }

        private Graphics savedBlockG(Graphics tempGraphics)
        {
            if (pSavedB != savedBlock || resize)
            {
                lowestU2 = 9999;
                lWidth2.Clear();
                lHeight2.Clear();
                for (int i = 0; i < 4; i++)
                {
                    if (!lWidth2.Contains(saving[i].X))
                    {
                        lWidth2.Add(saving[i].X);
                        if (lowestU2 > saving[i].X)
                        {
                            lowestU2 = saving[i].X;
                        }
                    }
                    if (!lHeight2.Contains(saving[i].Y))
                    {
                        lHeight2.Add(saving[i].Y);
                    }
                }

                int bOffset = 2;

                if (lHeight2.Count == 1)
                {
                    bOffset = 1;
                }

                for (int i = 0; i < 4; i++)
                {
                    savedRects[i] = new Rectangle((saving[i].X - lowestU2) * sizeMultiplier + panel2.Location.X + panel2.Width / 2 - (lWidth2.Count * sizeMultiplier / 2), (saving[i].Y + bOffset) * sizeMultiplier + (int)(50 * factorSize) + sizeMultiplier * 20 - panel2.Width + 1 + panel2.Width / 2 - lHeight2.Count * sizeMultiplier / 2, sizeMultiplier, sizeMultiplier);
                }
                pSavedB = savedBlock;
            }
            
            for (int i = 0; i < 4; i++)
            {
                square = new SolidBrush(blockColor(savedBlock));
                tempGraphics.FillRectangle(square, savedRects[i]);
                tempGraphics.DrawRectangle(box, savedRects[i]);
            }

            return tempGraphics;
        }

        private Graphics previewBlock(Graphics tempGraphics)
        {
            if (prevUp != comingUp || resize)
            {
                lowestU = 9999;
                lWidth.Clear();
                lHeight.Clear();
                for (int i = 0; i < 4; i++)
                {
                    if (!lWidth.Contains(upcoming[i].X))
                    {
                        lWidth.Add(upcoming[i].X);
                        if (lowestU > upcoming[i].X)
                        {
                            lowestU = upcoming[i].X;
                        }
                    }
                    if (!lHeight.Contains(upcoming[i].Y))
                    {
                        lHeight.Add(upcoming[i].Y);
                    }
                }

                int bOffset = 2;

                if (lHeight.Count == 1)
                {
                    bOffset = 1;
                }

                for (int i = 0; i < 4; i++)
                {
                    previewRects[i] = new Rectangle((upcoming[i].X - lowestU) * sizeMultiplier + panel2.Location.X + panel2.Width / 2 - (lWidth.Count * sizeMultiplier / 2), (upcoming[i].Y + bOffset) * sizeMultiplier + panel2.Location.Y + (panel2.Location.Y - panel1.Location.Y) + panel2.Width / 2 - lHeight.Count * sizeMultiplier / 2, sizeMultiplier, sizeMultiplier);
                }
                prevUp = comingUp;
            }

            tempGraphics.FillRectangle(emptySquare, new Rectangle(panel2.Location.X, panel2.Location.Y + (panel2.Location.Y - panel1.Location.Y), panel2.Width + 2, panel2.Width + 2));
            tempGraphics.DrawRectangle(previewP, new Rectangle(panel2.Location.X - 1, panel2.Location.Y + (panel2.Location.Y - panel1.Location.Y) - 1, panel2.Width + 2, panel2.Width + 2));
            for (int i = 0; i < 4; i++)
            {
                square = new SolidBrush(blockColor(comingUp));
                tempGraphics.FillRectangle(square, previewRects[i]);
                tempGraphics.DrawRectangle(box, previewRects[i]);
            }

            return tempGraphics;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (Form1.ActiveForm != null)
            {
                factorSize = Form1.ActiveForm.Size.Height / (float)950;
                if (prevFactorSize != factorSize)
                {
                    controls.Size = new Size((int)(240 * factorSize), (int)(196 * factorSize));
                    controls.Font = new Font(controls.Font.FontFamily, (float)(9.75 * factorSize));
                    controls.Location = new Point((int)((float)582 * factorSize), (int)((float)470 * factorSize));
                    panel1.Size = new Size((int)(160 * factorSize), (int)(70 * factorSize));
                    panel2.Size = panel1.Size;
                    panel3.Size = new Size((int)(400 * factorSize), (int)(30 * factorSize));
                    sizeMultiplier = panel3.Size.Width / 10;
                    //Form1.ActiveForm.Scale(new SizeF(factorSize, factorSize));
                    formSize = new Size((int)(918 * factorSize), Form1.ActiveForm.Size.Height);
                    panel1.Location = new Point((int)(622 * factorSize), (int)(50 * factorSize));
                    panel2.Location = new Point(panel1.Location.X, (int)(panel1.Location.Y + 120 * factorSize));
                    panel3.Location = new Point((int)(100 * factorSize), (int)(10 * factorSize));
                    prevFactorSize = factorSize;
                    score.Font = new Font(score.Font.FontFamily, 30 * factorSize);
                    highscore.Font = score.Font;
                    levelDisplay.Font = new Font(levelDisplay.Font.FontFamily, 12 * factorSize);
                    Form1.ActiveForm.Refresh();
                    resize = true;
                }
                if (Form1.ActiveForm.Size != formSize)
                {
                    Form1.ActiveForm.Size = new Size((int)(918 * factorSize), Form1.ActiveForm.Size.Height);
                }
            }
            if (!paused)
            {
                if (tetrisBlock == -1)
                {
                    savedABlock = false;
                    for (int i = 0; i < 4; i++)
                    {
                        if (block[i].Y < 0)
                        {
                            background.Stop();
                            endGame = true;
                        }
                    }
                    if (!endGame)
                    {
                        newBlock();
                    }
                }
                if (!endGame)
                {
                    prevBlock.Clear();
                    for (int i = 0; i < 4; i++)
                    {
                        Point current = block[i];
                        prevBlock.Add(current);
                        if (current.Y + 1 > -1 && current.Y < 19)
                        {
                            if (grid[current.X][current.Y + 1] != -1 && !block.Contains(new Point(current.X, current.Y + 1)))
                            {
                                tetrisBlock = -1;
                            }
                        }
                        if (block[i].Y == 19)
                        {
                            tetrisBlock = -1;
                        }
                    }
                    for (int i = 0; i < 4; i++)
                    {
                        if (tetrisBlock != -1)
                        {
                            Point current = block[i];
                            block[i] = new Point(current.X, current.Y + 1);
                            if (block[i].X >= 0 && block[i].Y >= 0 && block[i].X < 10 && block[i].Y < 20)
                            {
                                grid[block[i].X][block[i].Y] = tetrisBlock;
                            }
                        }
                        if (!block.Contains(prevBlock[i]))
                        {
                            if (prevBlock[i].Y > -1)
                            {
                                grid[prevBlock[i].X][prevBlock[i].Y] = -1;
                            }
                        }
                    }
                    if (tetrisBlock == -1)
                    {
                        checkClear();
                    }
                }
            }
            updateBoard();
        }

        private Graphics preview(Graphics tempGraphics)
        {
            int lowest = 99;
            for (int i = 0; i < 4; i++)
            {
                int amount = 0;
                bool keepGoing = true;
                while (keepGoing)
                {
                    if (block[i].Y + amount == 19)
                    {
                        keepGoing = false;
                    }
                    if (keepGoing && block[i].Y + amount > -1)
                    {
                        if (grid[block[i].X][block[i].Y + amount + 1] != -1 && !block.Contains(new Point(block[i].X, block[i].Y + amount + 1)))
                        {
                            keepGoing = false;
                        }
                    }
                    if (keepGoing)
                    {
                        amount++;
                    }
                }
                if (lowest > amount)
                {
                    lowest = amount;
                }
            }

            for (int i = 0; i < 4; i++)
            {
                if (block[i].Y + lowest > -1)
                {
                    tempGraphics.DrawRectangle(previewP, new Rectangle(block[i].X * sizeMultiplier + (int)(100 * factorSize), (block[i].Y + lowest) * sizeMultiplier + (int)(50 * factorSize), sizeMultiplier, sizeMultiplier));
                }
            }
            return tempGraphics;
        }

        private void checkClear()
        {
            int linesCleared = 0;
            for (int i = 19; i >= 0; i--)
            {
                bool fullLine = true;
                for (int j = 0; j < 10; j++)
                {
                    if (grid[j][i] == -1)
                    {
                        fullLine = false;
                        j = 10;
                    }
                }
                if (fullLine)
                {
                    removeLine(i);
                    i++;
                    linesCleared++;
                }
            }
            if (linesCleared > 0)
            {
                if (tetris)
                {
                    if (linesCleared == 4)
                    {
                        score.Text = (Int32.Parse(score.Text) + 1200) + "";
                    }
                    else
                    {
                        tetris = false;
                    }
                }
                else
                {
                    score.Text = (Int32.Parse(score.Text) + (linesCleared * 100 + 50 * Math.Pow(2, linesCleared - 1))) + "";
                }
                lines += linesCleared;

                if (lines / 8 >= level)
                {
                    if (speed > 100)
                    {
                        level++;
                        speed -= (int)(lines / 8.0) * 10;
                        if (speed < 100)
                        {
                            speed = 100;
                        }
                        timer2.Interval = speed;
                        levelDisplay.Text = "Level: " + level;
                    }
                    else if (speed > 10)
                    {
                        level++;
                        speed -= 10;
                        timer2.Interval = speed;
                        levelDisplay.Text = "Level: " + level;
                    }
                    else
                    {
                        levelDisplay.Text = "MAX LEVEL!!!";
                    }
                }

                checkHighScore();
            }
        }

        private void removeLine(int line)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = line; j >= 0; j--)
                {
                    if (j != 0)
                    {
                        grid[i][j] = grid[i][j - 1];
                    }
                    else
                    {
                        grid[i][j] = -1;
                    }
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.P)
            {
                paused = !paused;
            }
            else if (e.KeyCode == Keys.R)
            {
                timer1.Enabled = true;
            }
            else if (paused)
            {
                paused = false;
            }
            else if (e.KeyCode == Keys.Z)
            {
                clockwise = !clockwise;
            }
            else if (e.KeyCode == Keys.M)
            {
                audioPlaying = !audioPlaying;
                if (!audioPlaying)
                {
                    background.Stop();
                }
                else
                {
                    background.PlayLooping();
                }
            }
            if (tetrisBlock != -1 && block.Count == 4)
            {
                if (e.KeyCode == Keys.Down || e.KeyCode == Keys.S)
                {
                    timer2.Interval = speed / 10;
                }
                else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
                {
                    shiftDirection(1);
                }
                else if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
                {
                    shiftDirection(-1);
                }
                else if (e.KeyCode == Keys.Space)
                {
                    fallBlock();
                    checkClear();
                }
                else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.W)
                {
                    rotate();
                }
                else if (!savedABlock && (e.KeyCode == Keys.C || e.KeyCode == Keys.Shift))
                {
                    savedABlock = true;
                    for (int i = 0; i < 4; i++)
                    {
                        if (block[i].Y > -1)
                        {
                            grid[block[i].X][block[i].Y] = -1;
                        }
                    }
                    if (savedBlock != -1)
                    {
                        int tempSaved = savedBlock;
                        block = blockLocations(savedBlock);
                        savedBlock = tetrisBlock;
                        tetrisBlock = tempSaved;
                    }
                    else
                    {
                        savedBlock = tetrisBlock;
                        newBlock();
                    }
                    saving = blockLocations(savedBlock);
                    updateBoard();
                }
            }
        }

        private Point originP()
        {
            if (tetrisBlock == 6)
            {
                return block[1];
            }
            else if (tetrisBlock == 4)
            {
                return block[3];
            }
            else if (tetrisBlock == 2)
            {
                return block[0];
            }
            else
            {
                return block[2];
            }
        }

        private void rotate()
        {
            Point[] rotated = new Point[4];
            prevBlock.Clear();

            for (int i = 0; i < 4; i++)
            {
                prevBlock.Add(block[i]);
                Point origin = originP();
                Point translationCoordinate = new Point(block[i].X - origin.X, block[i].Y - origin.Y);
                translationCoordinate.Y *= -1;
                rotated[i] = translationCoordinate;

                // Rotation
                rotated[i].X = -translationCoordinate.Y * 1;
                rotated[i].Y = translationCoordinate.X * 1;

                if (clockwise)
                {
                    rotated[i].X *= -1;
                }
                else
                {
                    rotated[i].Y *= -1;
                }
                rotated[i].X += origin.X;
                rotated[i].Y += origin.Y;
            }

            bool valid = true;
            for (int i = 0; i < 4; i++)
            {
                if (rotated[i].X < 0 || rotated[i].X > 9 || rotated[i].Y > 19)
                {
                    while (rotated[i].X < 0)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            rotated[j].X++;
                        }
                    }
                    while (rotated[i].X > 9)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            rotated[j].X--;
                        }
                    }
                }
            }

            if (valid)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (rotated[i].Y > -1)
                    {
                        if (!block.Contains(rotated[i]) && grid[rotated[i].X][rotated[i].Y] != -1)
                        {
                            valid = false;
                            i = 4;
                        }
                    }
                }
            }

            if (valid)
            {
                for (int i = 0; i < 4; i++)
                {
                    block[i] = rotated[i];
                    if (block[i].Y > -1)
                    {
                        grid[block[i].X][block[i].Y] = tetrisBlock;
                    }
                }
                for (int i = 0; i < 4; i++)
                {
                    if (!block.Contains(prevBlock[i]))
                    {
                        if (prevBlock[i].Y > -1)
                        {
                            grid[prevBlock[i].X][prevBlock[i].Y] = -1;
                        }
                    }
                }
                updateBoard();
            }
        }

        private void checkHighScore()
        {
            if (Int32.Parse(score.Text) > Int32.Parse(highscore.Text))
            {
                highscore.Text = score.Text + "";
                if (!System.IO.Directory.Exists(path))
                {
                    try
                    {
                        System.IO.Directory.CreateDirectory(path);
                    }
                    catch
                    {

                    }
                }
                try
                {
                    System.IO.File.WriteAllText(path + "Highscore.kev", highscore.Text);
                }
                catch
                {

                }
            }
        }

        private void fallBlock()
        {
            prevBlock.Clear();
            int lowest = 99;
            for (int i = 0; i < 4; i++)
            {
                prevBlock.Add(block[i]);
                int amount = 0;
                bool keepGoing = true;
                while (keepGoing)
                {
                    if (block[i].Y + amount == 19)
                    {
                        keepGoing = false;
                    }
                    if (keepGoing && block[i].Y + amount > -1)
                    {
                        if (grid[block[i].X][block[i].Y + amount + 1] != -1 && !block.Contains(new Point(block[i].X, block[i].Y + amount + 1)))
                        {
                            keepGoing = false;
                        }
                    }
                    if (keepGoing)
                    {
                        amount++;
                    }
                }
                if (lowest > amount)
                {
                    lowest = amount;
                }
            }

            for (int i = 0; i < 4; i++)
            {
                block[i] = new Point(block[i].X, block[i].Y + lowest);
                if (block[i].Y > -1)
                {
                    grid[block[i].X][block[i].Y] = tetrisBlock;
                }
            }
            for (int i = 0; i < 4; i++)
            {
                if (!block.Contains(prevBlock[i]))
                {
                    if (prevBlock[i].Y > -1)
                    {
                        grid[prevBlock[i].X][prevBlock[i].Y] = -1;
                    }
                }
            }
            tetrisBlock = -1;
            updateBoard();
        }

        private void shiftDirection(int offset)
        {
            List<Point> prevBlock = new List<Point>();
            bool possible = true;
            for (int i = 0; i < 4; i++)
            {
                Point current = block[i];
                prevBlock.Add(current);
                if (current.Y > -1 && current.Y < 20 && current.X + offset > -1 && current.X + offset < 10)
                {
                    if (grid[current.X + offset][current.Y] != -1 && !block.Contains(new Point(current.X + offset, current.Y)))
                    {
                        possible = false;
                    }
                }
                if (current.X + offset < 0 || current.X + offset > 9)
                {
                    possible = false;
                }
            }
            if (possible)
            {
                for (int i = 0; i < 4; i++)
                {
                    Point current = block[i];
                    block[i] = new Point(current.X + offset, current.Y);
                    if (block[i].Y > -1)
                    {
                        grid[block[i].X][block[i].Y] = tetrisBlock;
                    }
                    if (!block.Contains(prevBlock[i]))
                    {
                        if (prevBlock[i].Y > -1)
                        {
                            grid[prevBlock[i].X][prevBlock[i].Y] = -1;
                        }
                    }
                }
                updateBoard();
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.S)
            {
                timer2.Interval = speed;
            }
        }
    }
}