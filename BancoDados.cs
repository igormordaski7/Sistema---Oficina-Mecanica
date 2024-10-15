using Microsoft.EntityFrameworkCore;

class AppDbContext : DbContext {

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
       string con = "server=localhost;port=3306;" + 
            "database=Mecanica; user=root;password=412410";

        optionsBuilder.UseMySQL(con).LogTo(Console.WriteLine, LogLevel.Information);
    }

    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Veiculo> Veiculos => Set<Veiculo>();
    public DbSet<Servico> Servicos => Set<Servico>();
}