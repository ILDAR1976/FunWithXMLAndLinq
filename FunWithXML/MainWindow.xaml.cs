using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace FunWithXML
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const String br = "\r";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            XElement root = XElement.Load(@".\PurchaseOrderInNamespace.xml");
            XNamespace aw = "http://www.adventure-works.com";
            IEnumerable<XElement> address =
                from el in root.Elements(aw + "Address")
                where (string)el.Attribute(aw + "Type") == "Billing"
                select el;
            
            foreach (XElement el in address) { 
                foreach (XElement el2 in el.Nodes()) {
                    if (el2.Name.ToString().Contains("Zip"))
                    info.AppendText(el2.Name.LocalName + " = " + el2.Value + br);
                }
            }
           

            IEnumerable<XNode> zip = from el in (from res in address select res).Nodes()
                                        where el.ToString().Contains("Zip")
                                        select el;
            XElement elem = (XElement)zip.ElementAt(0);

            info.AppendText("Name: " + elem.Name.LocalName + " = " + elem.Value + br); 

        }

     
    }
}
