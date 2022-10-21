using System.Drawing.Drawing2D;

namespace MangaBu
{
    public partial class Magnify : Form
    {
        private Bitmap? scrBmp;
        private Graphics? scrGrp;
        private bool mouseDown;
        public int ZoomFactor { get; set; } = 2;
        public bool HideCursor { get; set; } = true;
        public bool AutoClose { get; set; } = true;

        public Magnify()
        {
            SetStyle(
            ControlStyles.OptimizedDoubleBuffer |
            ControlStyles.Opaque |
            ControlStyles.UserPaint |
            ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();

            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            TopMost = true;
            Width = 150;
            Height = 150;

        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            var gp = new GraphicsPath();
            gp.AddEllipse(0, 0, Width, Height);
            Region = new Region(gp);

            CopyScreen();
            SetLocation();

            Capture = true;
            mouseDown = true;
            if (HideCursor) Cursor.Hide();
        }

        private void CopyScreen()
        {
            if (scrBmp == null)
            {

                var sz = Screen.FromControl(this).Bounds.Size;

                scrBmp = new Bitmap(sz.Width, sz.Height);
                scrGrp = Graphics.FromImage(scrBmp);
            }

            scrGrp.CopyFromScreen(Point.Empty, Point.Empty, scrBmp.Size);
        }

        private void SetLocation()
        {
            var p = Cursor.Position;

            Left = p.X - Width / 2;
            Top = p.Y - Height / 2;

        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            Invalidate();
        }



        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.KeyCode == Keys.Escape || e.Control && e.KeyCode == Keys.M)
            {
                mouseDown = false;
                if (HideCursor) Cursor.Show();
                if (AutoClose) Dispose();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {

            if (mouseDown) SetLocation();
            else CopyScreen();

            var pos = Cursor.Position;
            var cr = RectangleToScreen(ClientRectangle);
            var dY = cr.Top - Top;
            var dX = cr.Left - Left;

            e.Graphics.TranslateTransform(Width / 2, Height / 2);
            e.Graphics.ScaleTransform(ZoomFactor, ZoomFactor);
            e.Graphics.TranslateTransform(-pos.X - dX, -pos.Y - dY);
            e.Graphics.Clear(BackColor);
            
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            if (scrBmp != null) e.Graphics.DrawImage(scrBmp, 0, 0);
        }
    }
}
