using API_WEB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace API_WEB.Controllers
{
    [Route ("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public CategoriaController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                select idcategoria as ""idcategoria"",
                        nomecategoria as ""nomecategoria""
                from categoria
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
        public JsonResult Post(Categoria categoria)
        {
            string query = @"
                insert into categoria(nomecategoria)
                values(@nomecategoria)
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DataBase");
            NpgsqlDataReader myreader;
            using (NpgsqlConnection mycon = new NpgsqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (NpgsqlCommand mycommand = new NpgsqlCommand(query, mycon))
                {
                    mycommand.Parameters.AddWithValue("@nomecategoria", categoria.nomecategoria);
                    myreader = mycommand.ExecuteReader();
                    table.Load(myreader);

                    myreader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("Adicionado com sucesso!");
        }
        [HttpPut]
        public JsonResult Put(Categoria categoria)
        {
            string query = @"
                update categoria
                set nomecategoria = @nomecategoria
                where idcategoria = @idcategoria
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DataBase");
            NpgsqlDataReader myreader;
            using (NpgsqlConnection mycon = new NpgsqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (NpgsqlCommand mycommand = new NpgsqlCommand(query, mycon))
                {
                    mycommand.Parameters.AddWithValue("@idcategoria", categoria.idcategoria);
                    mycommand.Parameters.AddWithValue("@nomecategoria", categoria.nomecategoria);
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
                delete from categoria
                where idcategoria = @idcategoria
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DataBase");
            NpgsqlDataReader myreader;
            using (NpgsqlConnection mycon = new NpgsqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (NpgsqlCommand mycommand = new NpgsqlCommand(query, mycon))
                {
                    mycommand.Parameters.AddWithValue("@idcategoria", id);
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
