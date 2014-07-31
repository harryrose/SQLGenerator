using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsertGenerator
{
    public class Singleton<ItemType> where ItemType : class, new()
    {
        public static ItemType Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ItemType();
                }
                return instance;
            }
        }

        private static ItemType instance;
    }
}
