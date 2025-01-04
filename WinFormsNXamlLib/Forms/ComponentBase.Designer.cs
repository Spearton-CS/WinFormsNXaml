namespace WinFormsNXaml.Forms
{
    public abstract partial class ComponentBase
    {
        /// <summary>Required VisualStudio designer variable + allows you to not add fields for component's components</summary>
        protected System.ComponentModel.IContainer components = null;
        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            DeInitializeComponent();
            base.Dispose(disposing);
        }
        /// <summary>DeInitializes the contents of the component. Try do not modify that method, if you need to add something override that method and call base.DeInitializeComponent() in end of your code</summary>
        protected virtual void DeInitializeComponent()
        {
            if (components is not null)
            {
                components.Dispose();
                components = null;
            }
        }
        /// <summary>Initializes the contents of the component. Try do not modify that method, if you need to add something override that method and call base.InitializeComponent() in end of your code</summary>
        protected virtual void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }
    }
}
