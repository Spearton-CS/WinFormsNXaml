namespace WinFormsNXaml.Xaml
{
    /// <summary>A base class for XAML elements. Can be XamlDocument or XamlProperty</summary>
    public class XamlElement
    {
        public virtual string Type { get; }
        public virtual string? Class { get; }
        public virtual string? Name { get; }
        public virtual WeakReference<XamlElement>? RootElement { get; set; }
        public virtual IEnumerable<XamlElement> EnumerateRootElements()
        {
            WeakReference<XamlElement>? elem = RootElement;
            while (elem?.TryGetTarget(out var value) == true)
            {
                yield return value;
                elem = value.RootElement;
            }
        }
        public virtual XamlElement? FirstSubElement { get; set; }
        public virtual IEnumerable<XamlElement> EnumerateSubElements()
        {
            XamlElement? elem = FirstSubElement;
            while (elem is not null)
            {
                yield return elem;
                elem = elem.NextElement;
            }
        }
        public virtual WeakReference<XamlElement>? PreviousElement { get; set; }
        public virtual IEnumerable<XamlElement> EnumeratePreviousElements()
        {
            WeakReference<XamlElement>? elem = PreviousElement;
            while (elem?.TryGetTarget(out var value) == true)
            {
                yield return value;
                elem = value.PreviousElement;
            }
        }
        public virtual XamlElement? NextElement { get; set; }
        public virtual IEnumerable<XamlElement> EnumerateNextElements()
        {
            XamlElement? elem = NextElement;
            while (elem is not null)
            {
                yield return elem;
                elem = elem.NextElement;
            }
        }
    }
}