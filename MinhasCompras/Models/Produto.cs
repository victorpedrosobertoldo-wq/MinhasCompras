
using SQLite;

namespace MinhasCompras.Models
{
    public class Produto
    {
        String _descricao;
        Double _Preco;
        Double _Quantidade;

 
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Descricao
        {
            get => _descricao; set
            {
                if (value == null)
                    {
                    throw new Exception("Por favor, preencher Descrição!");
                    }
                    _descricao = value;

            }
        }
        public double Preco {
            get => _Preco; set
            {
                if (value == 0)
                {
                    throw new Exception("Por favor, preencher Preço!");
                    }
                _Preco =value;

            }
        }
        public double Quantidade {
            get => _Quantidade; set
            {
                if (value == 0)
                    {
                    throw new Exception("Por favor, preencher Quantidade!");
                    }
                _Quantidade = value;

            }
        }
        public double Total { get => Quantidade * Preco; }
        
        public string Categoria { get; set; }
    }
}
