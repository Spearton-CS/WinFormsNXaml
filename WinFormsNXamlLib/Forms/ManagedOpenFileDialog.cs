using System.ComponentModel;
using System.Runtime.Versioning;

namespace WinFormsNXaml.Forms
{
    public partial class ManagedOpenFileDialog : ComponentBase
    {
        public ManagedOpenFileDialog()
        {
            InitializeComponent();
        }

        public ManagedOpenFileDialog(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
