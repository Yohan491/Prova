using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Saloon.Models;

namespace Saloon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaloonController : ControllerBase
    {

    private readonly IConfiguration _configuration;
    public SaloonController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet]
    public JsonResult Get()
    {
        string query = @"
                      Select * from dbo.Saloon";
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
        public JsonResult Post(Saloona sad)
        {
            string query = @"
                     insert into dbo.Saloon ( NomeCliente,Hora_marcada,hora_da_ligacao,photofilename)
                     values
                        ,NomeCliente = '" + sad.NomeCliente + @"'
                        ,Hora_marcada = '" + sad.Hora_marcada + @"'
                        ,hora_da_ligacao = '" + sad.hora_da_ligacao + @"' 
                        ,hora_da_ligacao = '" + sad.photofilename + @"' 
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
    public JsonResult Put(Saloona sad)
    {
        string query = @"
                     update dbo.Saloon set
                          ,NomeCliente = '" + sad.NomeCliente + @"'
                        ,Hora_marcada = '" + sad.Hora_marcada + @"'
                        ,hora_da_ligacao = '" + sad.hora_da_ligacao + @"' 
                        ,hora_da_ligacao = '" + sad.photofilename + @"' 
                        where SaloonId = '"+ sad.SaloonId+@"'
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
    [HttpDelete("{id}")]
    public JsonResult Delete(int id)
    {
        string query = @"
                    Delete dbo.Saloon where SaloonId = " + id + @"";



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
}
}
