using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Conway
{
    public partial class MainWindow : Form
    {
        List<List<bool>> graphicsGrid_ = new List<List<bool>>();
        private int pixelSize_;
        private int gridWidth_;
        private int gridHeight_;
        private int sparseness_;

        private Conway conway;

        List<List<bool>> GraphicsGrid
        {
            get => graphicsGrid_;
            set
            {
                // copy every element
                // possible improvement: check whether the dimensions are equal
                for (int i = 0; i < gridWidth_; i++)
                {
                    for (int j = 0; j < gridHeight_; j++)
                    {
                        graphicsGrid_[i][j] = value[i][j];
                    }
                }
            }
        }

        public MainWindow(int gridWidth, int gridHeight, int pixelSize, int computeInterval, int sparseness)
        {
            // save to member variables
            this.gridWidth_ = gridWidth;
            this.gridHeight_ = gridHeight;
            this.pixelSize_ = pixelSize;
            this.sparseness_ = sparseness;

            // initialize window parameters
            InitializeComponent();
            this.DoubleBuffered = true;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainWindow_KeyDown);

            // Add a transparent panel and auto-size it, so that the window dimensions
            // exactly fit the size of the pixel grid
            TransparentPanel label = new TransparentPanel();
            label.Width = gridWidth * pixelSize - 2;
            label.Height = gridHeight * pixelSize - 2;
            this.Controls.Add(label);
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            // create Conway Game of Life object
            this.conway = new Conway(gridWidth, gridHeight);
            this.conway.randomInitialization(sparseness);

            // zero-initialize the graphics grid
            bool[] sizeRef = new bool[gridHeight];
            for (int j = 0; j < gridWidth; j++)
            {
                List<bool> row = new List<bool>(sizeRef);
                graphicsGrid_.Add(row);
            }
            
            // Start update loop
            Timer tmr = new Timer();
            tmr.Interval = computeInterval;   // milliseconds
            tmr.Tick += UpdateLoop;  // set handler
            tmr.Start();
        }

        private void UpdateLoop(object sender, EventArgs e)  //run this logic each timer tick
        {
            // Update Conway Game of Life
            this.conway.Update();

            // Copy the graphics grid
            GraphicsGrid = this.conway.fullGrid();

            // Update the graphics
            this.Refresh();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {

        }
    }

    public class TransparentPanel : Panel
    {
        protected override void OnPaint(PaintEventArgs e)
        {
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00000020; //WS_EX_TRANSPARENT
                return cp;
            }
        }
    }
}
