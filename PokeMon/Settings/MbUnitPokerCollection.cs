using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace PokeMon
{
    /// <summary>
    /// Represents a collection of MbUnit actions and denotes how new ones should be added.
    /// </summary>
    [ConfigurationCollection(typeof(MbUnitPokerSettings), AddItemName = "poke", 
        CollectionType = ConfigurationElementCollectionType.BasicMap)]
    class MbUnitPokerCollection : ConfigurationElementCollection
    {
        public MbUnitPokerCollection this[int index]
        {
            get { return base.BaseGet(index) as MbUnitPokerCollection; }
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
            return new MbUnitPokerSettings();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            // More complex than usual to ensure we're always unique
            MbUnitPokerSettings poker = (MbUnitPokerSettings)element;
            string key = poker.TestDll + poker.ReportDirectory + poker.Categories;

            foreach (ParameterSettings parameter in poker.ParameterCollection)
            {
                key += parameter.Value;
            }

            return key;
        }
    }
}
