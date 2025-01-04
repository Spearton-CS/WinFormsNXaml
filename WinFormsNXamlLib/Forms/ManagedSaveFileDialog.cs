using System.ComponentModel;
using System.Runtime.Versioning;

namespace WinFormsNXaml.Forms
{
    public partial class ManagedSaveFileDialog : ComponentBase
    {
        public ManagedSaveFileDialog()
        {
            InitializeComponent();
        }

        public ManagedSaveFileDialog(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}