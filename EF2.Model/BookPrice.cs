#nullable disable

namespace EF2.Model
{
    public partial class BookPrice
    {
        public int Id { get; set; }
        public int IdEdition { get; set; }
        public double Cost { get; set; }
        public double Price { get; set; }

        public virtual Edition IdEditionNavigation { get; set; }
    }
}
