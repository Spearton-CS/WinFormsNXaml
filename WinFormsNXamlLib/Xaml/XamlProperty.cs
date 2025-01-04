namespace WinFormsNXaml.Xaml
{
    public class XamlProperty : XamlElement
    {
        protected object? value = null;
        /// <summary>If value is null, but have sub elements it will return EnumerateSubElements()</summary>
        public virtual object? Value
        {
            get
            {
                if (value is null)
                    if (FirstSubElement is null)
                        return null;
                    else
                        return EnumerateSubElements();
                else
                    return value;
            }
            set => this.value = value;
        }
    }
}