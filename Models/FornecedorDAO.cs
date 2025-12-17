using AppWeb.Configs;
using AppWeb.Models;
using System.Data.Common;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;

namespace AppWeb.Models
{
    public class FornecedorDAO
    {
        private readonly Conexao _conexao;

        public FornecedorDAO(Conexao conexao)
        {
            _conexao = conexao;
        }

        public List<Fornecedor> ListarTodos()
        {
            var lista = new List<Fornecedor>();

           
            var comando = _conexao.CreateCommand("SELECT * FROM fornecedor;");
            var leitor = comando.ExecuteReader();

            while (leitor.Read())
            {
                var fornecedor = new Fornecedor();
                fornecedor.Id = leitor.GetInt32("id_for");
                fornecedor.RazaoSocial = DAOHelper.GetString(leitor, "razao_social");
                fornecedor.NomeFantasia = DAOHelper.GetString(leitor, "nome_fantasia");
                fornecedor.CNPJ = DAOHelper.GetString(leitor, "cnpj");
                fornecedor.NomeContato = DAOHelper.GetString(leitor, "nome_contato");
                fornecedor.Telefone = DAOHelper.GetString(leitor, "telefone");
                fornecedor.Email = DAOHelper.GetString(leitor, "email");
                fornecedor.Website = DAOHelper.GetString(leitor, "website");

          
                fornecedor.Endereco = new Endereco
                {
                    Rua = DAOHelper.GetString(leitor, "rua"),
                    Numero = leitor.GetInt32("numero"),
                    Bairro = DAOHelper.GetString(leitor, "bairro"),
                    CEP = DAOHelper.GetString(leitor, "cep"),
                    Cidade = DAOHelper.GetString(leitor, "cidade"),
                    Estado = DAOHelper.GetString(leitor, "estado")
                };

                lista.Add(fornecedor);
            }

            return lista;
        }
        public Fornecedor? BuscarPorId(int id)
        {
            var comando = _conexao.CreateCommand(
                "SELECT * FROM fornecedor WHERE id_for = @id_for;");
            comando.Parameters.AddWithValue("@id_for", id);

            var leitor = comando.ExecuteReader();

            if (leitor.Read())
            {
                var fornecedor = new Fornecedor();
                fornecedor.Id = leitor.GetInt32("id_for");
                fornecedor.NomeFantasia = DAOHelper.GetString(leitor, "nome_fantasia");
                fornecedor.CNPJ = DAOHelper.GetString(leitor, "cnpj");
                fornecedor.RazaoSocial = DAOHelper.GetString(leitor, "razao_social");
                fornecedor.NomeContato = DAOHelper.GetString(leitor, "nome_contato");
                fornecedor.Website = DAOHelper.GetString(leitor, "website");
                fornecedor.Telefone = DAOHelper.GetString(leitor, "telefone");
                fornecedor.Email = DAOHelper.GetString(leitor, "email");
                fornecedor.Endereco.Rua = DAOHelper.GetString(leitor, "Rua");
                fornecedor.Endereco.Cidade = DAOHelper.GetString(leitor, "Cidade");
                fornecedor.Endereco.CEP = DAOHelper.GetString(leitor, "CEP");
                fornecedor.Endereco.Bairro = DAOHelper.GetString(leitor, "bairro");
                fornecedor.Endereco.Numero = leitor.GetInt32("numero");
                fornecedor.Endereco.Estado = DAOHelper.GetString(leitor, "estado");

                return fornecedor;
            }
            else
            {
                return null;
            }
        }
        public void Atualizar(Fornecedor fornecedor)
        {


 
            try
            {
               
                var comando = _conexao.CreateCommand(
                "UPDATE fornecedor SET nome_fantasia = @_nome_fantasia, razao_social = @_razao_social, nome_contato = @_nome_contato " +
                "cnpj = @_cnpj, telefone = @_telefone, email = @_email, website = @_website, rua =  @_rua, numero = @_numero," +
                "bairro = @_bairro, cep = @_cep, cidade = @_cidade, estado = @_estado WHERE id_for = @_id;");

                comando.Parameters.AddWithValue("@_nome_fantasia", fornecedor.NomeFantasia);
                comando.Parameters.AddWithValue("@_razao_social", fornecedor.RazaoSocial);
                comando.Parameters.AddWithValue("@_nome_contato", fornecedor.NomeContato);
                comando.Parameters.AddWithValue("@_cnpj", fornecedor.Telefone);
                comando.Parameters.AddWithValue("@_telefone", fornecedor.Email);
                comando.Parameters.AddWithValue("@_id", fornecedor.Id);
                comando.Parameters.AddWithValue("@_email", fornecedor.Email);
                comando.Parameters.AddWithValue("@_website", fornecedor.Website);
                comando.Parameters.AddWithValue("@_rua", fornecedor.Endereco.Rua);
                comando.Parameters.AddWithValue("@_numero", fornecedor.Endereco.Numero);
                comando.Parameters.AddWithValue("@_bairro", fornecedor.Endereco.Bairro);
                comando.Parameters.AddWithValue("@_cep", fornecedor.Endereco.CEP);
                comando.Parameters.AddWithValue("@_cidade", fornecedor.Endereco.Cidade);
                comando.Parameters.AddWithValue("@_estado", fornecedor.Endereco.Estado);

                comando.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
        }


        public void Inserir(Fornecedor fornecedor)
        {
            try
            {
              
                var comando = _conexao.CreateCommand(
                    "INSERT INTO fornecedor (nome_fantasia, razao_social, nome_contato, cnpj, telefone, email, website,  rua, numero, bairro, cep, cidade, estado) " +
                    "VALUES (@razao, @fantasia, @cnpj, @contato, @telefone, @email, @website, @rua, @numero, @bairro, @cep, @cidade, @estado)"
                );

                comando.Parameters.AddWithValue("@razao", fornecedor.RazaoSocial);
                comando.Parameters.AddWithValue("@fantasia", fornecedor.NomeFantasia);
                comando.Parameters.AddWithValue("@cnpj", fornecedor.CNPJ);
                comando.Parameters.AddWithValue("@contato", fornecedor.NomeContato);
                comando.Parameters.AddWithValue("@telefone", fornecedor.Telefone);
                comando.Parameters.AddWithValue("@email", fornecedor.Email);
                comando.Parameters.AddWithValue("@website", fornecedor.Website);

                comando.Parameters.AddWithValue("@rua", fornecedor.Endereco.Rua);
                comando.Parameters.AddWithValue("@numero", fornecedor.Endereco.Numero);
                comando.Parameters.AddWithValue("@bairro", fornecedor.Endereco.Bairro);
                comando.Parameters.AddWithValue("@cep", fornecedor.Endereco.CEP);
                comando.Parameters.AddWithValue("@cidade", fornecedor.Endereco.Cidade);
                comando.Parameters.AddWithValue("@estado", fornecedor.Endereco.Estado);

                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao inserir fornecedor.", ex);
            }
        }
        public void Excluir(int id)
        {
            try
            {
                var comando = _conexao.CreateCommand(
                "DELETE FROM fornecedor WHERE id_for = @id_for;");

                comando.Parameters.AddWithValue("@id_for", id);

                comando.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
        }
    }
}