using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;


namespace MongoDbTest
{
    class Program
    {
        static void Main(string[] args)
        {

            Letter l = new Letter { LetterID = 1234, FirstName = "Sam", Postcode = 6000 };

            MongoCRUD db = new MongoCRUD("RMMS");

            db.InsertRecord("Letters", l);

            
            Console.WriteLine();
        }
    }

    public class MongoCRUD
    {
        IMongoDatabase db;

        public MongoCRUD(string database)
        {
            var client = new MongoClient();
            db = client.GetDatabase(database);           
        }

        public void InsertRecord<T>(string table, T record)
        {
            var collection = db.GetCollection<T>(table);

            collection.InsertOne(record);

        } 

        public List<T> LoadRecords<T>(string table)
        {
            var collection = db.GetCollection<T>(table);
            return collection.Find(new BsonDocument()).ToList();
        }

        public T LoadRecordById<T>(string table, string id)
        {
            var collection = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("LetterID", id);
            return collection.Find(filter).First();
        }
    }

    public class Letter
    {
        public int LetterID { get; set; }
        public string FirstName { get; set; }
        public int Postcode { get; set; }
    }

}

