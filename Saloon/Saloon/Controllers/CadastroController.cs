using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using Saloon.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Saloon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CadastroController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public CadastroController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                      Select * from dbo.Cadastro";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("SaloonAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;
                    myReader.Close();

                    myCon.Close();

                }
            }
            return new JsonResult(table);
        }
        [HttpPost]
        public JsonResult Post(Cadastro cad)
        {
            string query = @"
                     insert into dbo.Cadastro ( NomeUsuario,Email,SenhaUsuario)
                     values
                        ,NomeUsuario = '" + cad.NomeUsuario + @"'
                        ,Email = '" + cad.Email + @"'
                        ,SenhaUsuario = '" + cad.SenhaUsuario + @"' 
                        ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("SaloonAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;
                    myReader.Close();

                    myCon.Close();

                }
            }
            return new JsonResult("added sucessfully");

        }

        [HttpPut]
        public JsonResult Put(Cadastro cad)
        {
            string query = @"
                     update dbo.Cadastro set
                        ,NomeUsuario = '" + cad.NomeUsuario + @"'
                        ,Email = '" + cad.Email + @"'
                        SenhaUsuario = '" + cad.SenhaUsuario + @"' 
                        ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("SaloonAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;
                    myReader.Close();

                    myCon.Close();

                }
            }
            return new JsonResult("Updated sucessfully");

        }
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                    Delete dbo.Cadastro where UsuarioId = "+ id + @"";
                       
                       

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("SaloonAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;
                    myReader.Close();

                    myCon.Close();

                }
            }
            return new JsonResult("Delete sucessfully");

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
                var physicalPath = _env.ContentRootPath + "/Photos/" + filename;
                
                using(var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }
                return new JsonResult(filename); 
            }
            catch
            {
                return new JsonResult("pessoa.png");
            }
        }
    }
}
