using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ArtikliBaza
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			Artikal a = new Artikal("Plazam", 233);
			
			Racun r1 = new Racun();
			Racun r2 = new Racun();

			ArtRac ar = new ArtRac();
			ar.a = a;
			ar.ArtID = a.Sifra;
			ar.r = r2;
			ar.RacID = r2.Rbr;
			ar.Kolicina = 5;

			DB baza = new DB();
			baza.Racuns.Add(r1);
			baza.Racuns.Add(r2);
			baza.SaveChanges();
		}
	}

	public class DB : DbContext
	{
		public DB() : base("Data Source=DESKTOP-75VO5EN\\TESTSERVER;Initial Catalog = Artikli;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
		{ }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Artikal>().HasKey(a => a.Sifra);
			modelBuilder.Entity<Racun>().HasKey(r => r.Rbr);
			modelBuilder.Entity<ArtRac>().HasKey(ar => new { ar.ArtID, ar.RacID });
		}

		public DbSet<Artikal> Artikals { get; set; }
		public DbSet<Racun> Racuns { get; set; }
		public DbSet<ArtRac> AR { get; set; }
	}

	public class ArtRac
	{
		public Artikal a { get; set; }
		public Racun r { get; set; }
		public int ArtID { get; set; }
		public int RacID { get; set; }
		public int Kolicina { get; set; }
	}

	public class Artikal
	{
		public int Sifra { get; set; }
		public string Naziv { get; set; }
		public decimal Cena { get; set; }
		public Artikal(string n, decimal c)
		{
			Naziv = n;
			Cena = c;
		}
		public Artikal() { }
	}

	public class Racun
	{
		public int Rbr { get; set; }
		public DateTime Vreme { get; set; } = DateTime.Now;
	}
}
