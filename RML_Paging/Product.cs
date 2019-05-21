using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RML_Paging
{
    public class Product : ObservableObjectBase
    {
        public int Id { get; set; }

        private string sku;
        public string SKU
        {
            get { return sku; }
            set {
                sku = value;
                OnPropertyChanged();
            }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set {
                description = value;
                OnPropertyChanged();
            }
        }

        private string unit;
        public string Unit
        {
            get { return unit; }
            set {
                unit = value;
                OnPropertyChanged();
            }
        }

        private decimal price;
        public decimal Price
        {
            get { return price; }
            set {
                price = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Stock> Stocks { get; set; }
    }
}
