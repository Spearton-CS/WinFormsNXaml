using System.Reflection;
using System.Text;
using WinFormsNXaml.Xaml;

namespace WinFormsNXaml
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            //XamlElement xaml = XamlReader.Read(File.ReadAllText("xaml.test.txt"));
            //Show(xaml);
            //MessageBox.Show("Test");
            //static void Show(XamlElement xaml)
            //{
            //    MessageBox.Show($"Type: {xaml.Type}; Namespace: {xaml.Namespace}; Properties: {xaml.Properties.Count}; Children: {xaml.Children.Count}", xaml.Name);
            //    if (xaml.Properties.Count > 0)
            //    {
            //        StringBuilder sb = new();
            //        sb.AppendLine("Properties");
            //        foreach (var prop in xaml.Properties)
            //            sb.AppendLine($"{prop.Key}: {prop.Value}");
            //        MessageBox.Show(sb.ToString(), xaml.Name);
            //    }
            //    foreach (XamlElement subXaml in xaml.Children)
            //        Show(subXaml);
            //}
            //Application.Run(new Designer());
        }
    }
}