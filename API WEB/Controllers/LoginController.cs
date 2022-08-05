using API_WEB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using System;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody] Login login)
        {
            var user = Authenticate(login);

            if(user != null)
            {
                var token = Generate(user);
                return Ok(token);
            }
            return NotFound("Usuario não encontrado");
        }

        private string Generate(Login login)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, login.email)
            };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
              _configuration["Jwt:Audience"],
              claims,
              expires: DateTime.Now.AddHours(2),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private Login Authenticate(Login login)
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
                from usuario where email = @email and senha = @senha;

            ";
            
            
            string sqlDataSource = _configuration.GetConnectionString("DataBase");
            NpgsqlDataReader myreader;
            using (NpgsqlConnection mycon = new NpgsqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (NpgsqlCommand mycommand = new NpgsqlCommand(query, mycon))
                {
                    mycommand.Parameters.AddWithValue("@email", login.email);
                    mycommand.Parameters.AddWithValue("@senha", login.senha);
                    myreader = mycommand.ExecuteReader();
                    myreader.Read();

                    if (myreader.HasRows)
                    {
                        login.email = myreader.GetString("email");
                        login.senha = myreader.GetString("senha");
                    }
                    else
                    {
                        return null;
                    }                    

                    myreader.Close();
                    mycon.Close();
                }
            }
            
            if (login != null)
            {
                return login;
            }

            return null;
        }
    }
}
