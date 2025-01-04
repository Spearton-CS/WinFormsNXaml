using System.ComponentModel;
using System.Runtime.Versioning;

namespace WinFormsNXaml.Forms
{
    /// <summary>Base for all Components in WinFormsNXaml</summary>
    public abstract partial class ComponentBase : Component
    {
        protected ComponentBase(bool init = true)
        {
            if (init)
                InitializeComponent();
        }
        protected ComponentBase(IContainer container, bool init = true)
        {
            container.Add(this);
            if (init)
                InitializeComponent();
        }
    }
}