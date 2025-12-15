using AppWeb.Configs;
using AppWeb.Models;
using System;
using System.Data.Common; 

namespace AppWeb.Models
{
    public class ClienteDAO
    {
        private readonly Conexao _conexao;

        public ClienteDAO(Conexao conexao)
        {
            _conexao = conexao;
        }

        public List<Cliente> ListarTodos()
        {
            var lista = new List<Cliente>();

          
            var comando = _conexao.CreateCommand("SELECT * FROM Cliente;");
            var leitor = comando.ExecuteReader();

            while (leitor.Read())
            {
                var cliente = new Cliente();
                cliente.Id = leitor.GetInt32("id_cliente");
                cliente.Nome = DAOHelper.GetString(leitor, "nome");
                cliente.DataNascimento = DAOHelper.GetDateTime(leitor, "data_nascimento"); 
                cliente.Sexo = DAOHelper.GetString(leitor, "sexo");
                cliente.Telefone = DAOHelper.GetString(leitor, "telefone");
                cliente.Email = DAOHelper.GetString(leitor, "email");

                cliente.Endereco = new Endereco
                {
                    Rua = DAOHelper.GetString(leitor, "rua"),
                    Numero = leitor.GetInt32("numero"),
                    Bairro = DAOHelper.GetString(leitor, "bairro"),
                    CEP = DAOHelper.GetString(leitor, "cep"),
                    Cidade = DAOHelper.GetString(leitor, "cidade"),
                    Estado = DAOHelper.GetString(leitor, "estado")
                };

                lista.Add(cliente);
            }

            return lista;
        }

        public void Inserir(Cliente cliente)
        {
            try
            {

                var comando = _conexao.CreateCommand(
                    "INSERT INTO cliente (nome, data_nascimento, sexo, telefone, email, rua, numero, bairro, cep, cidade, estado) " +
                    "VALUES (@nome, @dt_nasc, @sexo, @telefone, @email, @rua, @numero, @bairro, @cep, @cidade, @estado)"
                );


                comando.Parameters.AddWithValue("@nome", cliente.Nome);
                comando.Parameters.AddWithValue("@dt_nasc", cliente.DataNascimento);
                comando.Parameters.AddWithValue("@sexo", cliente.Sexo);
                comando.Parameters.AddWithValue("@telefone", cliente.Telefone);
                comando.Parameters.AddWithValue("@email", cliente.Email);

                comando.Parameters.AddWithValue("@rua", cliente.Endereco.Rua);
                comando.Parameters.AddWithValue("@numero", cliente.Endereco.Numero);
                comando.Parameters.AddWithValue("@bairro", cliente.Endereco.Bairro);
                comando.Parameters.AddWithValue("@cep", cliente.Endereco.CEP);
                comando.Parameters.AddWithValue("@cidade", cliente.Endereco.Cidade);
                comando.Parameters.AddWithValue("@estado", cliente.Endereco.Estado);

                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw new Exception("Erro ao inserir cliente.", ex);
            }
        }

            public Cliente? BuscarPorId(int id)
            {
                var comando = _conexao.CreateCommand(
                    "SELECT * FROM cliente WHERE id_cliente = @id_cliente;");
                comando.Parameters.AddWithValue("@id_cliente", id);

                var leitor = comando.ExecuteReader();

                if (leitor.Read())
                {
                    var cliente = new Cliente();
                    cliente.Id = leitor.GetInt32("id_cliente");
                    cliente.Nome = DAOHelper.GetString(leitor, "nome");
                    cliente.DataNascimento = DAOHelper.GetDateTime(leitor, "data_nascimento");
                    cliente.Sexo = DAOHelper.GetString(leitor, "sexo");
                    cliente.Telefone = DAOHelper.GetString(leitor, "telefone");
                    cliente.Email = DAOHelper.GetString(leitor, "email");
                    cliente.Endereco.Rua = DAOHelper.GetString(leitor, "Rua");
                    cliente.Endereco.Cidade = DAOHelper.GetString(leitor, "Cidade");
                    cliente.Endereco.CEP = DAOHelper.GetString(leitor, "CEP");
                    cliente.Endereco.Bairro = DAOHelper.GetString(leitor, "bairro");
                    cliente.Endereco.Numero = leitor.GetInt32("numero");
                    cliente.Endereco.Estado = DAOHelper.GetString(leitor, "estado");

                return cliente;
                }
                else
                {
                    return null;
                }
            }
        public void Atualizar(Cliente cliente)
        {
            try
            {
                var comando = _conexao.CreateCommand(
                "UPDATE cliente SET nome = @_nome, data_nascimento = @_datanascimento, " +
                "sexo = @_sexo, telefone = @_telefone, email = @_email, EnderecoRua = @_EnderecoRua, EnderecoNumero = @_EnderecoNumero," +
                "EnderecoBairro = @_EnderecoBairro, EnderecoCEP = @_EnderecoCEP, EnderecoCidade = @_EnderecoCidade, EnderecoEstado = @_EnderecoEstado WHERE id_cliente = @_id;");

                comando.Parameters.AddWithValue("@_nome", cliente.Nome);
                comando.Parameters.AddWithValue("@_datanascimento", cliente.DataNascimento);
                comando.Parameters.AddWithValue("@_sexo", cliente.Sexo);
                comando.Parameters.AddWithValue("@_telefone", cliente.Telefone);
                comando.Parameters.AddWithValue("@_email", cliente.Email);
                comando.Parameters.AddWithValue("@_id", cliente.Id);
                comando.Parameters.AddWithValue("@_EnderecoRua", cliente.Endereco.Rua);
                comando.Parameters.AddWithValue("@_EnderecoNumero", cliente.Endereco.Numero);
                comando.Parameters.AddWithValue("@_EnderecoBairro", cliente.Endereco.Bairro);
                comando.Parameters.AddWithValue("@_EnderecoCEP", cliente.Endereco.CEP);
                comando.Parameters.AddWithValue("@_EnderecoCidade", cliente.Endereco.Cidade);
                comando.Parameters.AddWithValue("@_EnderecoEstado", cliente.Endereco.Estado);
                       
        comando.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
        }

        public void Excluir(int id)
        {
            try
            {
                var comando = _conexao.CreateCommand(
                "DELETE FROM cliente WHERE id_cliente = @id_cliente;");

                comando.Parameters.AddWithValue("@id_cliente", id);

                comando.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
        }
    }
   
}

