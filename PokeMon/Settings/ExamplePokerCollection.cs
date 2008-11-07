using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace PokeMon
{
    /// <summary>
    /// Represents a collection of example actions and denotes how new ones should be added.
    /// </summary>
    [ConfigurationCollection(typeof(ExamplePokerSettings), AddItemName = "poke", 
        CollectionType = ConfigurationElementCollectionType.BasicMap)]
    class ExamplePokerCollection : ConfigurationElementCollection
    {
        public ExamplePokerCollection this[int index]
        {
            get { return base.BaseGet(index) as ExamplePokerCollection; }
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
            return new ExamplePokerSettings();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ExamplePokerSettings)element).ExampleParameter;
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        // Denotes how to reference a new element in the collection.  If you change this, be sure
        // to also change the AddItemElement in the class's ConfigurationCollection attribute
        protected override string ElementName
        {
            get { return "poke"; }
        }
    }
}
