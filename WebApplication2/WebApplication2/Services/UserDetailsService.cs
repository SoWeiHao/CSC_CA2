using WebApplication2.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication2.Services
{
    public class UserDetailsService
    {
        private readonly IMongoCollection<UserDetails> _userdetails;

        public UserDetailsService(IMongoDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _userdetails = database.GetCollection<UserDetails>(settings.CollectionName);
        }

        public List<UserDetails> Get() =>
            _userdetails.Find(UserDetails => true).ToList();

        public UserDetails Get(string id) =>
            _userdetails.Find<UserDetails>(UserDetails => UserDetails.Email == id).FirstOrDefault();

        public UserDetails Create(UserDetails UserDetails)
        {
            _userdetails.InsertOne(UserDetails);
            return UserDetails;
        }


        public void Remove(UserDetails UserDetailsIn) =>
            _userdetails.DeleteOne(UserDetails => UserDetails.Id == UserDetailsIn.Id);

        public void Remove(string id) =>
            _userdetails.DeleteOne(UserDetails => UserDetails.Id == id);
    }
}