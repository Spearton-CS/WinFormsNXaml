using System.ComponentModel;

namespace WinFormsNXaml.Forms
{
    public partial class ManagedDirectorySelectDialog : ComponentBase
    {
        public ManagedDirectorySelectDialog()
        {
            InitializeComponent();
        }

        public ManagedDirectorySelectDialog(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
