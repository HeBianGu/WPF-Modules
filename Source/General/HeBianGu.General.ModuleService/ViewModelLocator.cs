using CommonServiceLocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Unity;

namespace HeBianGu.General.ModuleService
{
   public class ViewModelLocator
    { 
        /// <summary>
        /// 说明
        /// </summary>
        public static readonly DependencyProperty ViewModelTypeProperty = DependencyProperty.RegisterAttached(
            "ViewModelType", typeof(Type), typeof(ViewModelLocator), new FrameworkPropertyMetadata(default(Type), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnViewModelTypeChanged));

        public static Type GetViewModelType(DependencyObject d)
        {
            return (Type)d.GetValue(ViewModelTypeProperty);
        }

        public static void SetViewModelType(DependencyObject obj, Type value)
        {
            obj.SetValue(ViewModelTypeProperty, value);
        }

        static void OnViewModelTypeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            IUnityContainer unity = ServiceLocator.Current.GetInstance<IUnityContainer>();

            if (sender is FrameworkElement element)
            {
                //element.DataContext = unity.Resolve(args.NewValue as Type);
                Type type = args.NewValue as Type;

                if(unity.IsRegistered(type))
                {
                    element.DataContext = unity.Resolve(args.NewValue as Type);
                }
                else
                {

                    unity.RegisterSingleton(type);

                    element.DataContext = unity.Resolve(args.NewValue as Type);
                } 
            }
        }

    }
}
