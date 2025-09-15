using AppExemplo.Configs;

namespace AppExemplo.Models
{
    public class ProdutoDAO
    {
        private readonly Conexao _conexao;

        public ProdutoDAO(Conexao conexao)
        {
            _conexao = conexao;
        }

        public List<Produto> ListarTodos()
        {
            try
            {
                var lista = new List<Produto>();

                var command = _conexao.CreateCommand("SELECT * FROM produto");
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var produto = new Produto
                    {
                        Id = reader.GetInt32("id_pro"),
                        Nome = reader.GetString("nome_pro"),
                        Descricao = reader.IsDBNull(reader.GetOrdinal("descricao_pro")) ? "": reader.GetString("descricao_pro"),
                        Quantidade = reader.GetInt32("quantidade_pro"),
                        Preco = reader.GetInt32("preco_pro")
                    };

                    lista.Add(produto);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public void inserir(Produto produto) 
        {
            try
            {
                var comando = _conexao.CreateCommand("INSERT INTO PRODUTO VALUES (null, null, @_nome, @_descricao, @_qtd, @_preco) ");
                comando.Parameters.AddWithValue("@_nome", produto.Nome);
                comando.Parameters.AddWithValue("@_descricao", produto.Descricao);
                comando.Parameters.AddWithValue("@_qtd", produto.Quantidade);
                comando.Parameters.AddWithValue("@_preco", produto.Preco);

                comando.ExecuteNonQuery();
            }
            catch (Exception ex )
            {
                throw;
            }
        }

    }
}
