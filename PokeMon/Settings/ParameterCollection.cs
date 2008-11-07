using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace PokeMon
{
    public class ParameterCollection : ConfigurationElementCollection
    {
        public ParameterCollection this[int index]
        {
            get
            {
                return base.BaseGet(index) as ParameterCollection;
            }
            set
            {
                if (base.BaseGet(index) != null)
                {
                    base.BaseRemoveAt(index);
                }
                this.BaseAdd(index, value);
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ParameterSettings();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ParameterSettings)element).Name;
        }
    } 
}
