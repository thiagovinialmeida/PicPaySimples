using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Data
{
public class PicpaySimplesContext : DbContext
    {
        public PicpaySimplesContext (DbContextOptions<PicpaySimplesContext> options) : base(options) { }

        public DbSet<UserComum> UserCõmum { get; set; } = default!;
        public DbSet<Lojista> Lojistas { get; set; } = default!;
    }
}
