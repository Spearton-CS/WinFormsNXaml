namespace WinFormsNXaml.Forms
{
    public abstract partial class FormBase
    {
        private bool autoDeInit;
        /// <summary>Required VisualStudio designer variable + allows you to not add fields for form's components</summary>
        protected System.ComponentModel.IContainer components = null;
        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            DeInitializeComponent();
            base.Dispose(disposing);
        }
        /// <summary>DeInitializes the contents of the form. Try do not modify that method, if you need to add something override that method and call base.DeInitializeComponent() in end of your code</summary>
        protected virtual void DeInitializeComponent()
        {
            if (components is not null)
                components.Dispose();
            if (autoDeInit)
                FormClosing -= FormBase_FormClosing;
        }
        /// <summary>Initializes the contents of the form. Try do not modify that method, if you need to add something override that method and call base.InitializeComponent() in end of your code</summary>
        protected virtual void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "Form";
        }
    }
}