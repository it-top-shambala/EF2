using System.Collections.Generic;

#nullable disable

namespace EF2.Model
{
    public partial class Genre
    {
        public Genre()
        {
            TabBooks = new HashSet<Book>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Book> TabBooks { get; set; }
    }
}
