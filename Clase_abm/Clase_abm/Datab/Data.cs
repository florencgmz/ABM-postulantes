using Dapper;
using Clase_abm.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;


namespace Clase_abm.Datab
{
    public class Data : IData
    {
        private string connStr = @"Server=(localdb)\MSSQLLocalDB;Database=claseABM; Integrated Security=true";

        //GET
        public Postulant GetPostulant(int id)
        {
            var sql = "select * from Postulant p inner join Countries c on c.IDCountry = p.IDCountry where PostID = @PostID";

            using (var conn = new SqlConnection(connStr))
            {
                conn.Open();
                Postulant postulant = conn.Query<Postulant, Country ,Postulant>(sql, param: new { PostID = id }, map: GetCountry, splitOn: "IDCountry").FirstOrDefault();
                conn.Close();

                return postulant;
            }
        }

        //DELETE
        public void Delete(int id)
        {
            var sql = "delete Postulant where PostID = @PostID";

            using (var conn = new SqlConnection(connStr))
            {
                conn.Open();
                conn.Execute(sql, new { PostID = id });
                conn.Close();
            }

        }

        public IEnumerable<Postulant> GetPostulantAll()
        {
            var sql = "select * from Postulant p inner join Countries c on c.IDCountry = p.IDCountry";

            using (var conn = new SqlConnection(connStr))
            {
                conn.Open();
                IEnumerable<Postulant> postulantList = conn.Query<Postulant,Country,Postulant>(sql, map: GetCountry, splitOn: "IDCountry");
                conn.Close();

                return postulantList;
            }

        }

        //INSERT
        public void Insert(Postulant postulant)
        {

            var sql = "insert into Postulant (PostName, PostAge, IDCountry) values (@PostName, @PostAge, @IDCountry)";

            using (var conn = new SqlConnection(connStr))
            {
                conn.Open();
                conn.Execute(sql, new { @PostName = postulant.PostName, @PostAge = postulant.PostAge, @IDCountry = GetIDCountry(postulant.Country)} );
                conn.Close();
            }
        }

        //UPDATE
        public Postulant Update(Postulant postulant)
        {

            var sql = "update Postulant set PostName = @PostName, PostAge = @PostAge, IDcountry = @IDCountry where PostID = @PostID";

            using (var conn = new SqlConnection(connStr))
            {
                conn.Open();
                conn.Execute(sql, new {@PostID = GetIDPostulant(postulant), @PostName = postulant.PostName, @PostAge = postulant.PostAge, @IDCountry = GetIDCountry(postulant.Country)});
                conn.Close();
            }

            return this.GetPostulant(postulant.PostID);
        }

        private Postulant GetCountry(Postulant postulant, Country country)
        {
            postulant.Country = country;
            return postulant;
        }

        private int GetIDCountry (Country country)
        {
            string countryName = country.CountryName;
            var sql = "select IDCountry from Countries where CountryName = @countryName";
            using (var conn = new SqlConnection(connStr))
            {
                conn.Open();
                Country c = conn.QueryFirst<Country>(sql, country);
                conn.Close();
                return c.IDCountry;
            }
        }

        private int GetIDPostulant(Postulant postulant)
        {
            string postName = postulant.PostName;
            var sql = "select PostID from Postulant where PostName = @postName";
            using (var conn = new SqlConnection(connStr))
            {
                conn.Open();
                Postulant p = conn.QueryFirst<Postulant>(sql, postulant);
                conn.Close();
                return p.PostID;
            }
        }

    }
}
