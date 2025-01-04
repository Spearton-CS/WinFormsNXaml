namespace WinFormsNXaml
{
    public partial class Designer : Form
    {
        public Designer()
        {
            InitializeComponent();
            Controls.Add(new A()
            {
                BackColor = Color.DarkBlue,
                Size = new(200, 200),
                Location = new(200, 200),
                CornerRadius = 5f,
                BorderStyle = BorderStyle.FixedSingle,
                ForeColor = Color.Black
            });
        }
    }
    class A : Forms.ControlBase
    {

    }
}
