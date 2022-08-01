using API_WEB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace API_WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public PedidoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                select idpedido as ""idpedido"",
                        pagamento as ""pagamento"",
                        total as ""total"",
                        datapedido as ""datapedido"",
                        idusuario as ""idusuario""
                from pedido
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
        public JsonResult Post(Pedido pedido)
        {
            string query = @"
                insert into pedido(pagamento,total,datapedido,idusuario)
                values(@pagamento,@total,@datapedido,@idusuario)
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DataBase");
            NpgsqlDataReader myreader;
            using (NpgsqlConnection mycon = new NpgsqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (NpgsqlCommand mycommand = new NpgsqlCommand(query, mycon))
                {
                    mycommand.Parameters.AddWithValue("@pagamento", pedido.pagamento);
                    mycommand.Parameters.AddWithValue("@total", pedido.total);
                    mycommand.Parameters.AddWithValue("@datapedido", pedido.datapedido);
                    mycommand.Parameters.AddWithValue("@idusuario", pedido.idusuario);
                    myreader = mycommand.ExecuteReader();
                    table.Load(myreader);

                    myreader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("Adicionado com sucesso!");
        }
        [HttpPut]
        public JsonResult Put(Pedido pedido)
        {
            string query = @"
                update pedido
                set pagamento = @pagamento,
                    total = @total,
                    datapedido = @datapedido,
                    idusuario = @idusuario
                where idpedido = @idpedido
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DataBase");
            NpgsqlDataReader myreader;
            using (NpgsqlConnection mycon = new NpgsqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (NpgsqlCommand mycommand = new NpgsqlCommand(query, mycon))
                {
                    mycommand.Parameters.AddWithValue("@pagamento", pedido.pagamento);
                    mycommand.Parameters.AddWithValue("@total", pedido.total);
                    mycommand.Parameters.AddWithValue("@datapedido", pedido.datapedido);
                    mycommand.Parameters.AddWithValue("@idusuario", pedido.idusuario);
                    mycommand.Parameters.AddWithValue("@idpedido", pedido.idpedido);
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
                delete from pedido
                where idpedido = @idpedido
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DataBase");
            NpgsqlDataReader myreader;
            using (NpgsqlConnection mycon = new NpgsqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (NpgsqlCommand mycommand = new NpgsqlCommand(query, mycon))
                {
                    mycommand.Parameters.AddWithValue("@idpedido", id);
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
