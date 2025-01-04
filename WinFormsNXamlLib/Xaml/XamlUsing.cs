namespace WinFormsNXaml.Xaml
{
    /// <summary>xmlns:prefix='uri_or_namespace' define</summary>
    public class XamlUsing
    {
        public virtual string XMLNS { get; set; }
        public virtual string? Prefix { get; set; }
        public virtual WeakReference<XamlDocument>? RootDocument { get; set; }
        public virtual WeakReference<XamlUsing>? PreviousUsing { get; set; }
        public virtual IEnumerable<XamlUsing> EnumeratePreviousUsings()
        {
            WeakReference<XamlUsing>? elem = PreviousUsing;
            while (elem?.TryGetTarget(out var value) == true)
            {
                yield return value;
                elem = value.PreviousUsing;
            }
        }
        public virtual XamlUsing? NextUsing { get; set; }
        public virtual IEnumerable<XamlUsing> EnumerateNextUsings()
        {
            XamlUsing? use = NextUsing;
            while (use is not null)
            {
                yield return use;
                use = use.NextUsing;
            }
        }
    }
}