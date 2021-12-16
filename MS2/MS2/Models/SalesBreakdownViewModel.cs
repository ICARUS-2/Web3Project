using MS2.Data.Entities;

namespace MS2.Models
{
    public class SalesBreakdownViewModel
    {
        public Product Product { get; set; }
        public int SmallUnits { get; set; }
        public int MediumUnits { get; set; }
        public int LargeUnits { get; set; }

        public double SmallTotal
        {
            get { return Product.SmallPrice * SmallUnits; }
        }

        public double MediumTotal
        {
            get { return Product.MediumPrice * MediumUnits; }
        }

        public double LargeTotal
        {
            get { return Product.LargePrice * LargeUnits; }
        }
    }
}
