using API_WEB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace API_WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 
    public class UsuarioController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public UsuarioController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                select idusuario as ""idusuario"",
                        nome as ""nome"",
                        email as ""email"",
                        senha as ""senha"",
                        cep as ""cep"",
                        cidade as ""cidade"",
                        rua as ""rua"",
                        bairro as ""bairro"",
                        numero as ""numero""
                from usuario
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DataBase");
            NpgsqlDataReader myreader;
            using (NpgsqlConnection mycon = new NpgsqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (NpgsqlCommand mycommand = new NpgsqlCommand(query, mycon))
                {
                    myreader = mycommand.ExecuteReader();
                    table.Load(myreader);

                    myreader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult(table);
        }
        [HttpPost]
        public JsonResult Post(Usuario usuario)
        {
            string query = @"
                insert into usuario(nome,email,senha,cep,cidade,rua,bairro,numero)
                values(@nome,@email,@senha,@cep,@cidade,@rua,@bairro,@numero)
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DataBase");
            NpgsqlDataReader myreader;
            using (NpgsqlConnection mycon = new NpgsqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (NpgsqlCommand mycommand = new NpgsqlCommand(query, mycon))
                {
                    mycommand.Parameters.AddWithValue("@nome", usuario.nome);
                    mycommand.Parameters.AddWithValue("@email", usuario.email);
                    mycommand.Parameters.AddWithValue("@senha", usuario.senha);
                    mycommand.Parameters.AddWithValue("@cep", usuario.cep);
                    mycommand.Parameters.AddWithValue("@cidade", usuario.cidade);
                    mycommand.Parameters.AddWithValue("@rua", usuario.rua);
                    mycommand.Parameters.AddWithValue("@bairro", usuario.bairro);
                    mycommand.Parameters.AddWithValue("@numero", usuario.numero);
                    myreader = mycommand.ExecuteReader();
                    table.Load(myreader);

                    myreader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("Adicionado com sucesso!");
        }
        [HttpPut]
        public JsonResult Put(Usuario usuario)
        {
            string query = @"   
                update usuario
                set nome = @nome,
                    email = @email,
                    senha = @senha,
                    cep = @cep,
                    cidade = @cidade,
                    rua = @rua,
                    bairro = @bairro,
                    numero = @numero
                where idusuario = @idusuario
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DataBase");
            NpgsqlDataReader myreader;
            using (NpgsqlConnection mycon = new NpgsqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (NpgsqlCommand mycommand = new NpgsqlCommand(query, mycon))
                {
                    mycommand.Parameters.AddWithValue("@nome", usuario.nome);
                    mycommand.Parameters.AddWithValue("@email", usuario.email);
                    mycommand.Parameters.AddWithValue("@senha", usuario.senha);
                    mycommand.Parameters.AddWithValue("@cep", usuario.cep);
                    mycommand.Parameters.AddWithValue("@cidade", usuario.cidade);
                    mycommand.Parameters.AddWithValue("@rua", usuario.rua);
                    mycommand.Parameters.AddWithValue("@bairro", usuario.bairro);
                    mycommand.Parameters.AddWithValue("@numero", usuario.numero);
                    myreader = mycommand.ExecuteReader();
                    table.Load(myreader);

                    myreader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("Atualizado com sucesso!");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                delete from usuario
                where idusuario = @idusuario
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DataBase");
            NpgsqlDataReader myreader;
            using (NpgsqlConnection mycon = new NpgsqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (NpgsqlCommand mycommand = new NpgsqlCommand(query, mycon))
                {
                    mycommand.Parameters.AddWithValue("@idusuario", id);
                    myreader = mycommand.ExecuteReader();
                    table.Load(myreader);

                    myreader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("Deletado com sucesso!");
        }
    }
}
