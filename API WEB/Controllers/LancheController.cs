using API_WEB.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Data;
using System.IO;

namespace API_WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LancheController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public LancheController(IConfiguration configuration,IWebHostEnvironment web)
        {
            _configuration = configuration;
            _env = web;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                select l.idlanche,l.nomelanche,l.valorlanche,l.descricaolanche,
                       l.imagemlanche,c.idcategoria,c.nomecategoria
                from lanche l join categoria c on c.idcategoria = l.idcategoria
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
        public JsonResult Post(Lanche lanche)
        {
            string query = @"
                insert into lanche(nomelanche,valorlanche,descricaolanche,imagemlanche,idcategoria)
                values(@nomecategoria,@valorlanche,@descricaolanche,@imagemlanche,@idcategoria)
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DataBase");
            NpgsqlDataReader myreader;
            using (NpgsqlConnection mycon = new NpgsqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (NpgsqlCommand mycommand = new NpgsqlCommand(query, mycon))
                {
                    mycommand.Parameters.AddWithValue("@nomecategoria", lanche.nomelanche);
                    mycommand.Parameters.AddWithValue("@valorlanche", lanche.valorlanche);
                    mycommand.Parameters.AddWithValue("@descricaolanche", lanche.descricaolanche);
                    mycommand.Parameters.AddWithValue("@imagemlanche", lanche.imagemlanche);
                    mycommand.Parameters.AddWithValue("@idcategoria", lanche.idcategoria);
                    myreader = mycommand.ExecuteReader();
                    table.Load(myreader);

                    myreader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("Adicionado com sucesso!");
        }
        [HttpPut]
        public JsonResult Put(Lanche lanche)
        {
            string query = @"
                update lanche
                set nomelanche = @nomelanche,
                    valorlanche = @valorlanche,
                    descricaolanche = @descricaolanche,
                    imagemlanche = @imagemlanche,
                    idcategoria = @idcategoria
                where idlanche = @idlanche
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DataBase");
            NpgsqlDataReader myreader;
            using (NpgsqlConnection mycon = new NpgsqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (NpgsqlCommand mycommand = new NpgsqlCommand(query, mycon))
                {
                    mycommand.Parameters.AddWithValue("@idlanche", lanche.idlanche);
                    mycommand.Parameters.AddWithValue("@nomelanche", lanche.nomelanche);
                    mycommand.Parameters.AddWithValue("@valorlanche", lanche.valorlanche);
                    mycommand.Parameters.AddWithValue("@descricaolanche", lanche.descricaolanche);
                    mycommand.Parameters.AddWithValue("@imagemlanche", lanche.imagemlanche);
                    mycommand.Parameters.AddWithValue("@idcategoria", lanche.idcategoria);
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
                delete from lanche
                where idlanche = @idlanche
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DataBase");
            NpgsqlDataReader myreader;
            using (NpgsqlConnection mycon = new NpgsqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (NpgsqlCommand mycommand = new NpgsqlCommand(query, mycon))
                {
                    mycommand.Parameters.AddWithValue("@idlanche", id);
                    myreader = mycommand.ExecuteReader();
                    table.Load(myreader);

                    myreader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("Deletado com sucesso!");
        }

        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/"+ filename;
                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }
                return new JsonResult(filename);
            }
            catch (Exception)
            {
                return new JsonResult("anonimo.png");
            }
        }
    }
}
