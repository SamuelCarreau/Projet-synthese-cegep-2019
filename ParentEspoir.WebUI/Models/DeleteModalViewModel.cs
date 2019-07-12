using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParentEspoir.WebUI.Models
{
    public class DeleteModalViewModel
    {
        public string ModalId { get; set; }
        public int ModelId { get; set; }
        public KeyValuePair<string,int> SecondModelId { get; set; }
        public string ControlerName { get; set; }


    }
}
