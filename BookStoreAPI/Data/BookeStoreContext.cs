using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Data
{
    public class BookeStoreContext : DbContext
    {
        public BookeStoreContext(DbContextOptions<BookeStoreContext> options)
            : base(options)
        {
        }

        public DbSet<Books> Books { get; set; } //here books before get set is the table name created in sql table name as Books
       
        // one way to connection string
      /*  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) //type override confi--- fills automatically.......this is for connection to database
        {
            optionsBuilder.UseSqlServer("Server=LAPTOP-5EO3NL9R;Database=BookstoreAPI;Integrated security =True");
            base.OnConfiguring(optionsBuilder);
        }*/
    }
}
