using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsNXaml.Forms
{
    public partial class ManagedDirectoryCreateDialog : ComponentBase
    {
        public ManagedDirectoryCreateDialog()
        {
            InitializeComponent();
        }

        public ManagedDirectoryCreateDialog(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
