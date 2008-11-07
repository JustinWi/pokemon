using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace PokeMon
{
    [ConfigurationCollection(typeof(ConfigPokerSettings), AddItemName = "poke", 
        CollectionType = ConfigurationElementCollectionType.BasicMap)]
    class ConfigPokerCollection : ConfigurationElementCollection
    {
        public ConfigPokerCollection this[int index]
        {
            get { return base.BaseGet(index) as ConfigPokerCollection; }
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
            return new ConfigPokerSettings();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ConfigPokerSettings)element).ConfigPath;
        }
    }
}
