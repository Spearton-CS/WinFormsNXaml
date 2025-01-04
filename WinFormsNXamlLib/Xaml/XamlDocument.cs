namespace WinFormsNXaml.Xaml
{
    /// <summary>A root document. Often used for Forms, Controls and Resources</summary>
    public class XamlDocument : XamlElement
    {
        public virtual XamlUsing? FirstUsing { get; set; }
        public virtual IEnumerable<XamlUsing> EnumerateUsings()
        {
            XamlUsing? use = FirstUsing;
            while (use is not null)
            {
                yield return use;
                use = use.NextUsing;
            }
        }
        public override WeakReference<XamlElement>? RootElement
        {
            get => null;
            set => throw new InvalidOperationException("XamlDocument can't have a root");
        }
        public override IEnumerable<XamlElement> EnumerateRootElements() => [];
        public override WeakReference<XamlElement>? PreviousElement
        {
            get => null;
            set => throw new InvalidOperationException("XamlDocument can't have a previous element");
        }
        public override IEnumerable<XamlElement> EnumeratePreviousElements() => [];
        public override XamlElement? NextElement
        {
            get => null;
            set => throw new InvalidOperationException("XamlDocument can't have a next element");
        }
        public override IEnumerable<XamlElement> EnumerateNextElements() => [];
    }
}