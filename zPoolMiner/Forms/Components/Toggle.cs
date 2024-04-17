using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace zPoolMiner.Forms.Components
{
    public partial class Toggle : CheckBox
    {
        public Toggle()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            Padding = new Padding(2);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            OnPaintBackground(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (var path = new GraphicsPath())
            {
                var d = Padding.All;
                var r = Height - 2 * d;
                path.AddArc(d, d, r, r, 90, 180);
                path.AddArc(Width - r - d, d, r, r, -90, 180);
                path.CloseFigure();
                e.Graphics.FillPath(Checked ? Brushes.DarkGray : Brushes.LightGray, path);
                r = Height - 1;
                var rect = Checked ? new Rectangle(Width - r - 1 + d / 2, 0 + d / 2, r - d, r - d)
                                   : new Rectangle(0 + d / 2, 0 + d / 2, r - d, r - d);
                e.Graphics.FillEllipse(Checked ? Brushes.LightGreen : Brushes.Salmon, rect);
            }
        }
    }
}