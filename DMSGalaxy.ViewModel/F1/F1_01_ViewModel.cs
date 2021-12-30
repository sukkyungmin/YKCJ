using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Windows.Input;

using DMSGalaxy.ViewModel.Common;

namespace DMSGalaxy.ViewModel.F1
{
    public class F1_01_ViewModel : ObservableObject
    {
        public class GroupInfoList<T> : List<object>
        {
            public object Key { get; set; }
            public decimal Amount { get; set; }
            public string Summary { get; set; }
            public string Details1 { get; set; }
            public string Details2 { get; set; }
            public string Details3 { get; set; }
            public string Details4 { get; set; }

            public new IEnumerator<object> GetEnumerator()
            {
                return (IEnumerator<object>)base.GetEnumerator();
            }

            public void ImportList(IEnumerable<object> list)
            {
                foreach (object item in list)
                {
                    this.Add(item);
                }
            }
        }
    }
}
