using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace kpp_rt
{
    class Class1
    {

        public void users_ychet(string deist)
        {
            string dates = DateTime.Now.ToString("dd MMMM yyyy");
            string times = DateTime.Now.ToString("HH:mm:ss");

            SqlConnection conn = new SqlConnection(Form1.connectString);
            SqlCommand cmd = new SqlCommand();
            conn.Open(); //Устанавливаем соединение с базой данных.
            cmd.Connection = conn;
            cmd.CommandText = @"INSERT INTO [Log_users] (Действие, 
		                                Время, Дата, ID_Users)
                                        values (@Действие, 
		                                @Время, @Дата, @ID_Users)";


            cmd.Parameters.Add("@Действие", SqlDbType.NVarChar);
            cmd.Parameters["@Действие"].Value = deist;

            cmd.Parameters.Add("@Время", SqlDbType.NVarChar);
            cmd.Parameters["@Время"].Value = dates;

            cmd.Parameters.Add("@Дата", SqlDbType.NVarChar);
            cmd.Parameters["@Дата"].Value = times;

            cmd.Parameters.Add("@ID_Users", SqlDbType.NVarChar);
            cmd.Parameters["@ID_Users"].Value = Properties.Settings.Default.id;




            cmd.ExecuteNonQuery();
            // Close();
            conn.Close();
        }

    }
}
