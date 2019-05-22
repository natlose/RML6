using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFramework.BulkInsert.Extensions;

namespace RML_Paging
{
    public class MainWindowVM : ObservableObjectBase
    {
        private int productCount;
        public int ProductCount
        {
            get { return productCount; }
            set
            {
                productCount = value;
                OnPropertyChanged();
            }
        }

        private int stockCount;
        public int StockCount
        {
            get { return stockCount; }
            set
            {
                stockCount = value;
                OnPropertyChanged();
            }
        }

        private int serviceCount;
        public int ServiceCount
        {
            get { return serviceCount; }
            set
            {
                serviceCount = value;
                OnPropertyChanged();
            }
        }

        private void updateCounts()
        {
            using (MyData mydata = new MyData())
            {
                ProductCount = mydata.Products.Count();
                StockCount = mydata.Stocks.Count();
                ServiceCount = mydata.Services.Count();
            }
        }

        public void LoadData()
        {
            updateCounts();
        }

        public async Task Generate(IProgress<int> progress)
        {
            await Task.Run(()=> {
                Random rnd = new Random(DateTime.Now.Millisecond);
                progress.Report(0);
                for (int i = 0; i < 50; i++)
                {
                    using (MyData mydata = new MyData())
                    {
                        for (int j = 0; j < 100; j++)
                        {
                            Product product = new Product()
                            {
                                SKU = Guid.NewGuid().ToString(),
                                Description = Guid.NewGuid().ToString().ToUpper() + " product",
                                Price = 1000m + rnd.Next(100000),
                                Unit = "db"
                            };
                            product.Stocks.Add
                            (
                                new Stock()
                                {
                                    Shelf = Guid.NewGuid().ToString().Substring(0, 3),
                                    Quantity = rnd.Next(100)
                                }
                            );
                            mydata.Products.Add(product);
                            mydata.Services.Add(new Service()
                            {
                                Nr = Guid.NewGuid().ToString().ToUpper().Substring(0, 10),
                                Description = Guid.NewGuid().ToString().ToUpper() + " service",
                                Price = 1000m + rnd.Next(100000),
                                Unit = "h"
                            });
                        }
                        mydata.SaveChanges();
                    }
                    progress.Report(i * 2);
                }
            });
            updateCounts();
        }

        public async Task Bulk(IProgress<int> progress)
        {
            progress.Report(0);
            await Task.Run(()=> {
                List<Product> products = new List<Product>();
                // https://stackoverflow.com/questions/5282999/reading-csv-file-and-storing-values-into-an-array
                using (Microsoft.VisualBasic.FileIO.TextFieldParser csvParser = new Microsoft.VisualBasic.FileIO.TextFieldParser(@"Products.csv"))
                {
                    csvParser.CommentTokens = new string[] { "#" };
                    csvParser.SetDelimiters(new string[] { ";" });
                    csvParser.HasFieldsEnclosedInQuotes = true;
                    //csvParser.ReadLine(); //első sort átléphetjük, ha van fejléc
                    while (!csvParser.EndOfData)
                    {
                        string[] fields = csvParser.ReadFields();
                        products.Add(new Product() {
                            // fields[0] az Id, de azt az SQL Server-re hagyjuk
                            SKU = fields[1],
                            Description = fields[2],
                            Unit = fields[3],
                            Price = Decimal.Parse(fields[4], System.Globalization.CultureInfo.InvariantCulture)
                        });
                    }
                }
                // https://www.nuget.org/packages/EntityFramework6.BulkInsert/
                using (MyData mydata = new MyData())
                {
                    mydata.BulkInsert<Product>(products);
                }
            });
            progress.Report(100);
            updateCounts();
        }
    }
}
