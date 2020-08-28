using System.Collections.Generic;
using task_maneger.Models;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace task_maneger{
    class Data{

        static string cs = @Config.getConfig();

        public static List<Task> getAllTasks(){
            List<Task> alltasks = new List<Task>();

            using var con = new MySqlConnection(cs);
            con.Open();

            string sql = "SELECT * FROM tasks";
            using var cmd = new MySqlCommand(sql, con);

            using MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                alltasks.Add(new Task(rdr.GetInt32(0), rdr.GetString(1), rdr.GetBoolean(2)));
            }
            return alltasks;
        }

        public static Task addTask(string name){

            using var con = new MySqlConnection(cs);
            con.Open();
            
            string sql = "INSERT INTO tasks(name, done) VALUES(@name, @done)";
            using var cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@done", false);
            cmd.Prepare();

            cmd.ExecuteNonQuery();
            int id = (int)cmd.LastInsertedId;

            return new Task(id, name, false);
        }


    }
}