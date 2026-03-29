using MinhasCompras.Models;
using MinhasCompras.Views;
using SQLite;

namespace MinhasCompras.Helpers
{
    public class SQliteDatabaseHelper
    {
        readonly SQLiteAsyncConnection _conn;
        public SQliteDatabaseHelper(string path)
        {

            _conn = new SQLiteAsyncConnection(path);
            _conn.CreateTableAsync<Produto>().Wait();
        }

        public Task<int> insert(Produto p) { 
            
           return _conn.InsertAsync(p);

        }
        
        public Task<List<Produto>> update(Produto p) { 
        
            string sql = "UPDATE Produto SET Descricao = ?, Preco = ?, Quatidade = ?, Categoria = ? WHERE Id = ?";
            return _conn.QueryAsync<Produto>(
                
                sql, p.Descricao, p.Preco, p.Quantidade, p.Categoria, p.Id
                );
        }

        public Task<int> delete(int ID) { 
        
        return _conn.Table<Produto>().DeleteAsync(i => i.Id == ID);
        }

        
        public Task<List<Produto>> GetAll() { 
        
           return _conn.Table<Produto>().ToListAsync();
        }

        public Task<List<Produto>> search(string q) {
            
            string sql = "SELECT * FROM Produto WHERE Descricao LIKE '%" + q + "%' ";
            return _conn.QueryAsync<Produto>(sql);

        }



    }
}
