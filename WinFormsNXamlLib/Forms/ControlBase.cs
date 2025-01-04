using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using WinFormsNXaml.Design;

namespace WinFormsNXaml.Forms
{
    /// <summary>Base for all Controls in WinFormsNXaml</summary>
    public abstract partial class ControlBase : UserControl, INotifyPropertyChanged
    {
        protected ControlBase(bool init = true)
        {
            if (init)
                InitializeComponent();
        }
        protected Brush backgroundBrush = new SolidBrush(DefaultBackColor);
        public virtual new Color? BackColor
        {
            get => backgroundBrush is SolidBrush sb ? sb.Color : (backgroundBrush is HatchBrush hb ? hb.BackgroundColor : null);
            set
            {
                ArgumentNullException.ThrowIfNull(value, nameof(value));
                backgroundBrush.Dispose();
                backgroundBrush = new SolidBrush(value.Value);
                base.BackColor = value.Value;
                PropertyChanged?.Invoke(this, new(nameof(BackColor)));
            }
        }
        protected Region backgroundRegion;
        //protected PointF[]? backgroundCurvePoints = null;
        protected CornerRadius cornerRadius;
        public virtual CornerRadius CornerRadius
        {
            get => cornerRadius;
            set
            {
                cornerRadius = value;
                if (value.IsEmpty)
                {
                    Region?.Dispose();
                    Region = null;
                }
                else
                    CreateRoundedRegion();
                //InitBackgroundCurvePoints();
                PropertyChanged?.Invoke(this, new(nameof(CornerRadius)));
            }
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            if (!cornerRadius.IsEmpty)
                CreateRoundedRegion();
            base.OnSizeChanged(e);
            //InitBackgroundCurvePoints();
            PropertyChanged?.Invoke(this, new(nameof(Size)));
        }
        protected virtual void CreateRoundedRegion()
        {
            using GraphicsPath path = new();
            int w = Width, h = Height;
            float perc = w / 10f;
            float LT = cornerRadius.LeftTop * perc, LB = cornerRadius.LeftBottom * perc, RT = cornerRadius.RightTop * perc, RB = cornerRadius.RightBottom * perc;
            float wRT = w - RT, wRB = w - RB, hLB = h - LB, hRB = h - RB;
            path.AddLine(LT, 0, wRT, 0);
            path.AddArc(wRT, 0, RT, RT, 270, 90);
            path.AddLine(w, RT, w, hRB);
            path.AddArc(wRB, hRB, RB, RB, 0, 90);
            path.AddLine(wRB, h, LB, h);
            path.AddArc(0, hLB, LB, LB, 90, 90);
            path.AddLine(0, hLB, 0, LT);
            path.AddArc(0, 0, LT, LT, 180, 90);
            Region?.Dispose();
            Region = new(path);
        }
        //protected virtual void InitBackgroundCurvePoints()
        //{
        //    backgroundCurvePoints ??= new PointF[8];
        //    backgroundCurvePoints[0] = new(0, cornerRadius.LeftTop);
        //    backgroundCurvePoints[1] = new(cornerRadius.LeftTop, 0);
        //    backgroundCurvePoints[2] = new(Width - cornerRadius.RightTop, 0);
        //    backgroundCurvePoints[3] = new(Width, cornerRadius.RightTop);
        //    backgroundCurvePoints[4] = new(Width, Height - cornerRadius.RightBottom);
        //    backgroundCurvePoints[5] = new(Width - cornerRadius.RightBottom, Height);
        //    backgroundCurvePoints[6] = new(cornerRadius.LeftBottom, Height);
        //    backgroundCurvePoints[7] = new(0, Height - cornerRadius.LeftBottom);
        //}
        public event PropertyChangedEventHandler? PropertyChanged;
        //protected override void OnPaintBackground(PaintEventArgs e)
        //{
        //    if (backgroundCurvePoints is null)
        //        InitBackgroundCurvePoints();
        //    e.Graphics.FillClosedCurve(backgroundBrush, backgroundCurvePoints, FillMode.Winding);
        //    e.Graphics.DrawLine(Pens.White, 0, 0, 100, 100);
        //    e.Graphics.Flush();
        //}
        protected override void OnPaint(PaintEventArgs e)
        {
            OnPaintBackground(e);
            base.OnPaint(e);
        }
    }
}