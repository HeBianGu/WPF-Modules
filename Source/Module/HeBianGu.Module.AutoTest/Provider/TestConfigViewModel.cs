using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Module.AutoTest
{
    public class TestConfigViewModel : ModelViewModel<TestConfig>
    {

        public TestConfigViewModel(TestConfig model) : base(model)
        {
            foreach (var item in model.TestCategories)
            {
                this.TestCategories.Add(new TestCategoryViewModel(item));
            }
        }

        private ObservableCollection<TestCategoryViewModel> _testCategories = new ObservableCollection<TestCategoryViewModel>();
        /// <summary> 说明  </summary>
        public ObservableCollection<TestCategoryViewModel> TestCategories
        {
            get { return _testCategories; }
            set
            {
                _testCategories = value;
                RaisePropertyChanged("TestCategories");
            }
        }

    }

    public class TestCategoryViewModel : ModelViewModel<TestCategory>
    {

        public TestCategoryViewModel(TestCategory model) : base(model)
        {
            foreach (var item in model.Items)
            {
                this.Items.Add(new ItemViewModel(item));
            }
        }

        private ObservableCollection<ItemViewModel> _items = new ObservableCollection<ItemViewModel>();
        /// <summary> 说明  </summary>
        public ObservableCollection<ItemViewModel> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                RaisePropertyChanged("Items");
            }
        }

    }

    public class ItemViewModel : SelectViewModel<Item>
    {
        public ItemViewModel(Item model) : base(model)
        {
            foreach (var item in model.Parameters)
            {
                this.Parameters.Add(new ParameterViewModel(item));
            }

            foreach (var item in model.Limits)
            {
                this.Limits.Add(new LimitViewModel(item));
            }
        }
        private ObservableCollection<ParameterViewModel> _parameters = new ObservableCollection<ParameterViewModel>();
        /// <summary> 说明  </summary>
        public ObservableCollection<ParameterViewModel> Parameters
        {
            get { return _parameters; }
            set
            {
                _parameters = value;
                RaisePropertyChanged("Parameters");
            }
        }


        private ObservableCollection<LimitViewModel> _limits = new ObservableCollection<LimitViewModel>();
        /// <summary> 说明  </summary>
        public ObservableCollection<LimitViewModel> Limits
        {
            get { return _limits; }
            set
            {
                _limits = value;
                RaisePropertyChanged("Limits");
            }
        }



        private bool _isBuzy;
        /// <summary> 说明  </summary>
        public bool IsBuzy
        {
            get { return _isBuzy; }
            set
            {
                _isBuzy = value;
                RaisePropertyChanged("IsBuzy");
            }
        }



        private bool _isShowChart;
        /// <summary> 说明  </summary>
        public bool IsShowChart
        {
            get { return _isShowChart; }
            set
            {
                _isShowChart = value;
                RaisePropertyChanged("IsShowChart");
            }
        }


        private ObservableCollection<double> _xDatas = new ObservableCollection<double>();
        /// <summary> 说明  </summary>
        public ObservableCollection<double> xDatas
        {
            get { return _xDatas; }
            set
            {
                _xDatas = value;
                RaisePropertyChanged("xDatas");
            }
        }


        private ObservableCollection<double> _yDatas = new ObservableCollection<double>();
        /// <summary> 说明  </summary>
        public ObservableCollection<double> yDatas
        {
            get { return _yDatas; }
            set
            {
                _yDatas = value;
                RaisePropertyChanged("yDatas");
            }
        }


        private bool _drawOnce;
        /// <summary> 说明  </summary>
        public bool DrawOnce
        {
            get { return _drawOnce; }
            set
            {
                _drawOnce = value;
                RaisePropertyChanged("DrawOnce");
            }
        }


        //public TestItemNew Convert()
        //{
        //    TestItemNew test = new TestItemNew();
        //    test.ID = Guid.NewGuid();
        //    test.Name = this.Model.Name;

        //    foreach (var item in this.Parameters)
        //    {
        //        var r = item.Convert();
        //        r.TestItemID = test.ID;
        //        test.TestParameters.Add(r);
        //    }

        //    foreach (var item in this.Limits)
        //    {
        //        var r = item.Convert();
        //        r.TestItemID = test.ID;
        //        test.TestLimits.Add(r);
        //    }
        //    return test;
        //}
    }

    public class ParameterViewModel : ModelViewModel<Parameter>
    {
        public ParameterViewModel(Parameter model) : base(model)
        {

        }

        //public TestParameterNew Convert()
        //{
        //    TestParameterNew test = new TestParameterNew();
        //    test.ID = Guid.NewGuid();
        //    test.Name = this.Model.Name;
        //    //test.Value = this.Model.DefaultValue;
        //    test.StringValue = this.Model.DefaultValue;
        //    return test;
        //}
    }
    public class LimitViewModel : ModelViewModel<Limit>
    {
        public LimitViewModel(Limit model) : base(model)
        {
            foreach (var item in model.CompareConditions)
            {
                this.CompareConditions.Add(new CompareConditionViewModel(item));
            }
        }


        private ObservableCollection<CompareConditionViewModel> _compareConditions = new ObservableCollection<CompareConditionViewModel>();
        /// <summary> 说明  </summary>
        public ObservableCollection<CompareConditionViewModel> CompareConditions
        {
            get { return _compareConditions; }
            set
            {
                _compareConditions = value;
                RaisePropertyChanged("CompareConditions");
            }
        }

        //public TestLimitNew Convert()
        //{
        //    TestLimitNew test = new TestLimitNew();
        //    test.ID = Guid.NewGuid();
        //    test.Name = this.Model.Name;
        //    //test.Value = this.Model.DefaultValue;
        //    test.StringValue = this.Model.Name;
        //    return test;
        //}

    }

    public class CompareConditionViewModel : ModelViewModel<CompareCondition>
    {
        public CompareConditionViewModel(CompareCondition model) : base(model)
        {

        }
    }
}
