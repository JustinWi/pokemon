using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace PokeMon
{
    [ConfigurationCollection(typeof(WebsitePokerSettings), AddItemName = "poke", 
        CollectionType = ConfigurationElementCollectionType.BasicMap)]
    class WebservicePokerCollection : WebsitePokerCollection
    {
        public new WebservicePokerCollection this[int index]
        {
            get { return base.BaseGet(index) as WebservicePokerCollection; }
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
            return new WebservicePokerSettings();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((WebservicePokerSettings)element).URI;
        }
    }
}
