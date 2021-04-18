using HeBianGu.General.DataBase.Identify;
using HeBianGu.General.ModuleService;
using HeBianGu.General.WpfControlLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Module.Identify
{
    /// <summary> 说明</summary>
    internal class RoleViewModel : ModuleCollectionViewModel<IRoleRespository, hi_dd_role>
    {

        #region - 属性 -

        #endregion

        #region - 命令 -

        #endregion


        #region - 方法 -

        protected override async void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "Sumit")
            {


            }
            //  Do：取消
            else if (command == "Loaded")
            {
               
            }
        }

        #endregion
    }
}
