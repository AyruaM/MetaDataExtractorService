using Microsoft.EntityFrameworkCore;

namespace MetaDataExtractorService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<PatientDocument> PatientDocuments { get; set; }
    }
}
