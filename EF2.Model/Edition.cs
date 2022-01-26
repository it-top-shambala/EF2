using System.Collections.Generic;

#nullable disable

namespace EF2.Model
{
    public partial class Edition
    {
        public Edition()
        {
            TabPrices = new HashSet<BookPrice>();
        }

        public int Id { get; set; }
        public int IdBook { get; set; }
        public int IdPublishingHouse { get; set; }
        public int NumberOfPages { get; set; }
        public short YearOfPublishing { get; set; }

        public virtual Book IdBookNavigation { get; set; }
        public virtual PublishingHouse IdPublishingHouseNavigation { get; set; }
        public virtual ICollection<BookPrice> TabPrices { get; set; }
    }
}
