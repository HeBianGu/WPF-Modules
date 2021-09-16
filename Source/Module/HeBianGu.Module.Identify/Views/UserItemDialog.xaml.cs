using HeBianGu.Base.WpfBase;
using HeBianGu.General.DataBase.Identify;
using HeBianGu.General.WpfControlLib;
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

namespace HeBianGu.Module.Identify.Views
{
    /// <summary>
    /// UserItemDialog.xaml 的交互逻辑
    /// </summary>
    public partial class UserItemDialog : UserControl
    {
        public UserItemDialog()
        {
            InitializeComponent();
        }


        public Action Sumit { get; set; }

        private void ObjectPropertyForm_Sumit(object sender, RoutedEventArgs e)
        {
            Sumit?.Invoke(); 
      
        }
    } 
}
