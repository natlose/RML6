using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RML_Paging
{
    public class Stock : ObservableObjectBase
    {
        public int Id { get; set; }

        public int FiProduct { get; set; }

        private string shelf;

        public string Shelf
        {
            get { return shelf; }
            set {
                shelf = value;
                OnPropertyChanged();
            }
        }

        private decimal quantity;

        public decimal Quantity
        {
            get { return quantity; }
            set {
                quantity = value;
                OnPropertyChanged();
            }
        }

        public Product Product { get; set; }

    }
}
