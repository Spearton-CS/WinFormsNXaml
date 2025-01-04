using System.ComponentModel;
using System.Runtime.Versioning;

namespace WinFormsNXaml.Forms
{
    /// <summary>Base for all Dialogs in WinFormsNXaml</summary>
    public abstract partial class DialogBase : ComponentBase
    {
        protected DialogBase(bool init = true) : base(init)
        {
            
        }
        protected DialogBase(IContainer container, bool init = true) : base(container, init)
        {
            
        }
    }
}