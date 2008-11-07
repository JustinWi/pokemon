using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace PokeMon
{
    [ConfigurationCollection(typeof(WebsitePokerSettings), AddItemName = "poke", 
        CollectionType = ConfigurationElementCollectionType.BasicMap)]
    class WebsitePokerCollection : ConfigurationElementCollection
    {
        public WebsitePokerCollection this[int index]
        {
            get { return base.BaseGet(index) as WebsitePokerCollection; }
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
            return new WebsitePokerSettings();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((WebsitePokerSettings)element).URI;
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        protected override string ElementName
        {
            get { return "poke"; }
        }
    }
}
