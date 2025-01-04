using System.Runtime.Versioning;

namespace WinFormsNXaml.Forms
{
    /// <summary>Base for all Forms in WinFormsNXaml</summary>
    public abstract partial class FormBase : Form
    {
        protected FormBase(bool init = true, bool autoDeInit = true)
        {
            if (init)
                InitializeComponent();
            if (this.autoDeInit = autoDeInit)
                FormClosing += FormBase_FormClosing;
        }
        private void FormBase_FormClosing(object? sender, FormClosingEventArgs e)
        {
            DeInitializeComponent();
        }
    }
}