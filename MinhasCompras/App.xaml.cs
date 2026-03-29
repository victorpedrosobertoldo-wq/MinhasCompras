using Microsoft.Extensions.DependencyInjection;
using MinhasCompras.Helpers;
using MinhasCompras.Models;

namespace MinhasCompras
{
    public partial class App : Application
    {
        public List<Categorias> list_categoria = new List<Categorias>
{
    new Categorias { categoria_selecionada = "Alimentos" },
    new Categorias { categoria_selecionada = "Higiene" },
    new Categorias { categoria_selecionada = "Eletrônicos" },
    new Categorias { categoria_selecionada = "Outros" }
};

        static SQliteDatabaseHelper _db;

        public static SQliteDatabaseHelper Db
        {
            get
            {
                if (_db == null)
                {
                    string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "banco_sqlite_compras.db3");

                    _db = new SQliteDatabaseHelper(path);
                }
                return _db;
            }
        }
        public App()
        {
            InitializeComponent();
            
            MainPage = new NavigationPage(new Views.ListarProduto());
        
        }
    }
}