using System;
using System.Collections.Generic;

using MySql.Data.MySqlClient;

using BugReportMVC5.Data;

// Add BSON Library file



namespace BugReportMVC5.Models
{
    public class FileUploadContext : Connection
    {


        public FileUploadContext(string connectionString) : base(connectionString)
        {

        }




        public void SaveToDatabase(byte[] file, string FileName, string filetype,int ticketNo)
        {

            //Below part is for MongoDb
            // var url = "mongodb://sidgore:sidgore@sidmongo-shard-00-00-uesva.mongodb.net:27017,sidmongo-shard-00-01-uesva.mongodb.net:27017,sidmongo-shard-00-02-uesva.mongodb.net:27017/Files_Upload?ssl=true&replicaSet=SidMongo-shard-0&authSource=admin";


            //var Client = new MongoClient(url);



            // var client = new MongoClient("mongodb://sidgore:sidgore@sidmongo-shard-00-00-uesva.mongodb.net:27017,sidmongo-shard-00-01-uesva.mongodb.net:27017,sidmongo-shard-00-02-uesva.mongodb.net:27017/Files?ssl=true&replicaSet=SidMongo-shard-0&authSource=admin");

            // var server = client.GetServer();
            //var database = client.GetDatabase("Files");

            // var collection = database.GetCollection<File>("SidFile");

            //File bookStore = new File
            //{
            //    FileContent = file,
            //    Name = FileName,
            //    FileType = filetype

            //};
            //  collection.InsertOne(bookStore);

            // var url = "mongodb://sidgore:sidgore@sidmongo-shard-00-00-uesva.mongodb.net:27017,sidmongo-shard-00-01-uesva.mongodb.net:27017,sidmongo-shard-00-02-uesva.mongodb.net:27017/users?ssl=true&replicaSet=SidMongo-shard-0&authSource=admin";

            // var database = Client.GetDatabase("Files_Upload");

            //  System.Diagnostics.Debug.WriteLine("The Dtatabase is  " + database);

            //Below part to insert into Mysql Database------


            string query = "INSERT INTO `bug_tracking_system`.`Files`(`Name`,`Image`,`Filetype`,`ticketNo`) VALUES(@Name,@Image,@Filetype,@TicketNo)";
            System.Diagnostics.Debug.WriteLine("The query is  " + query);

            MySqlCommand cmd = new MySqlCommand(query, this.MySQLConnection);

            cmd.Parameters.AddWithValue("@Name", FileName);
            cmd.Parameters.AddWithValue("@Image", file);
            cmd.Parameters.AddWithValue("@Filetype", filetype);
            cmd.Parameters.AddWithValue("@TicketNo", ticketNo);


            cmd.ExecuteNonQuery();
            cmd.Dispose();



        }

        public List<File> GetFiles(int ticketNo)
        {


            List<File> list = new List<File>();





            string query = "SELECT * from Files where ticketNo = "+ticketNo;
            MySqlCommand cmd = new MySqlCommand(query, this.MySQLConnection);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(new File()
                    {
                        ID = Convert.ToInt32(reader["ID"]),
                        Filetype = reader["Filetype"].ToString(),
                        Name = reader["Name"].ToString(),
                        Image = ((byte[])reader["Image"])
                    });
                }
            }



            System.Diagnostics.Debug.WriteLine("The query is  " + query);



            cmd.ExecuteNonQuery();
            cmd.Dispose();




            //    var client = new MongoClient("mongodb://sidgore:sidgore@sidmongo-shard-00-00-uesva.mongodb.net:27017,sidmongo-shard-00-01-uesva.mongodb.net:27017,sidmongo-shard-00-02-uesva.mongodb.net:27017/Files?ssl=true&replicaSet=SidMongo-shard-0&authSource=admin");

            //    // var server = client.GetServer();
            //    // var database = client.GetDatabase("Files");

            //    // var collection = database.GetCollection<File>("SidFile");

            //    IMongoDatabase db = client.GetDatabase("Files");
            //    //  var db = client.GetDatabase("Files");

            //    var collection = db.GetCollection<BsonDocument>("SidFile");

            //    var result = collection.Find(FilterDefinition<BsonDocument>.Empty).ToList();
            //    foreach (var item in result)
            //    {

            //        System.Diagnostics.Debug.WriteLine("The Dtatabase is  " + item);

            //    }

            //    // collection.FindAll();

            //    //  var projection = Builders<BsonDocument>.Projection.Include("item").Include("status").Exclude("_id");
            //    //var result = collection.Find<File>({},{FileName:"sss"});


            //    //collection.find({ },{ Name: true,_id: false})

            return list;
            //}



              


        }
    }
}
