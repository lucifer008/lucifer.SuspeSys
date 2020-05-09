using SuspeSys.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
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

namespace SuspeSys.WpfClient
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //ObservableCollection<Member> memberData = new ObservableCollection<Member>();
            //memberData.Add(new Member()
            //{
            //    Name = "Joe",
            //    Age = "23",
            //    Sex = SexOpt.Male,
            //    Pass = true,
            //    Email = new Uri("mailto:Joe@school.com")
            //});
            //memberData.Add(new Member()
            //{
            //    Name = "Mike",
            //    Age = "20",
            //    Sex = SexOpt.Male,
            //    Pass = false,
            //    Email = new Uri("mailto:Mike@school.com")
            //});
            //memberData.Add(new Member()
            //{
            //    Name = "Lucy",
            //    Age = "25",
            //    Sex = SexOpt.Female,
            //    Pass = true,
            //    Email = new Uri("mailto:Lucy@school.com")
            //});
            //dataGrid.DataContext = memberData;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            TestRemoting();
           
        }
        private static TcpChannel chan=null;
        void TestRemoting() {
            //TcpChannel chan = new TcpChannel();
            if (null == chan) {
                chan = new TcpChannel();
                ChannelServices.RegisterChannel(chan, false);
            }
           
            ICatService calc = (ICatService)Activator.GetObject(
                                     typeof(ICatService),
                             "tcp://localhost:7840/CatService");
            if (calc == null)
                System.Console.WriteLine("Could not locate server");
            else
                Console.WriteLine(calc.TestRemoting());
            MessageBox.Show(calc.TestRemoting());
        }
    }
   

   
}
