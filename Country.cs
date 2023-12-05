using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDMenu
{
    internal class Country
    {
        private int code;
        private string name;

        public Country(int code, string name)
        {
            this.name = name;
            this.code = code;
        }

        public int Code { get => code; set => code = value; }
        public string Name { get => name; set => name = value; }

        public override string ToString()
        {
            return name;
        }

    }


}
