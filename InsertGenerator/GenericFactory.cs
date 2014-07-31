using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsertGenerator
{
    public abstract class GenericFactory<FactoryType,KeyType,BaseType> : Singleton<FactoryType> where FactoryType : class, new()
    {
        protected GenericFactory()
        {
            types = new Dictionary<KeyType, Type>();
        }

        public virtual void Add<T>(KeyType name) where T : BaseType, new()
        {
            if (types.ContainsKey(name))
            {
                throw new Exception(string.Format("Duplicate factory key '{0}'", name));
            }

            types.Add(name, typeof(T));
        }

        public virtual BaseType Get(KeyType name)
        {
            if (!types.ContainsKey(name))
            {
                throw new Exception(string.Format("No such key, '{0}'", name));
            }

            return (BaseType)Activator.CreateInstance(types[name]);
        }

        public virtual IEnumerable<KeyType> GetKeys()
        {
            return types.Keys;
        }

        private Dictionary<KeyType, Type> types;
    }
}
