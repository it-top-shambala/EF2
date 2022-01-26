using System.Collections.Generic;

#nullable disable

namespace EF2.Model
{
    public partial class Book
    {
        public Book()
        {
            TabEditions = new HashSet<Edition>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int IdAuthor { get; set; }
        public short? YearOfCreation { get; set; }
        public int IdGenre { get; set; }

        public virtual Author IdAuthorNavigation { get; set; }
        public virtual Genre IdGenreNavigation { get; set; }
        public virtual ICollection<Edition> TabEditions { get; set; }
    }
}
