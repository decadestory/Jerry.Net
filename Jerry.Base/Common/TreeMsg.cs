using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jerry.Base.Common
{
    public class TreeMsg
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public IList<TreeMsg> Children { get;set;}
        public int State { get; set; }
        public bool Checked { get; set; }
        public int ParentId { get; set; }
    }
}
