using System.Data;
using System.Data.SqlClient;

namespace BlazorApp1.Data
{
    public class Sql
    {
        string connectionstring = "Data Source=192.168.1.3;Initial Catalog=MemeDB;User ID=sa;Password=Passw0rd;";


        public List<Meme> Read()
        {
            List<Meme> list = new List<Meme>();
            SqlConnection con = new(connectionstring);
            SqlCommand cmd = new SqlCommand("SELECT * FROM MemeTable", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Meme meme = new Meme()
                { Id = (int)dr.GetInt32(0), MemeName = (string)dr.GetString(1), Url = dr.GetString(2)};
                list.Add(meme);
            }
            con.Close();

            return list;
        }

        public void Create(Meme meme)
        {
            using (SqlConnection conn = new(connectionstring))
            {
                var cmd = new SqlCommand($"INSERT INTO MemeTable (MemeName, Url) VALUE(@memeName, @memeUrl)", conn);
                cmd.Parameters.Add("@memeName", SqlDbType.NVarChar).Value = meme.MemeName;
                cmd.Parameters.Add("@memeUrl", SqlDbType.NVarChar).Value = meme.Url;

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
