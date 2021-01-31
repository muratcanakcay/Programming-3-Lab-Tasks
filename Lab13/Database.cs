using System;
using System.IO;
using System.Xml.Serialization;

namespace Lab13
{
    public class Database
    {
        private readonly string _baseDir;

        public Database(string baseDir)
        {
            _baseDir = baseDir;

            if (!Directory.Exists(_baseDir))
            {
                Directory.CreateDirectory(_baseDir);
            }
        }

        /// <summary>
        /// Adds new object to the database.
        /// If the object with the given Id and the same type exists, the exception should be thrown.
        /// If Id == 0 the value of Id should be replaced by te max Id value for TEntity object type + 1
        /// </summary>

        // TODO: Implement Add method

        public void Add<TEntity>(TEntity entity) where TEntity : IEntity
        {
            var typeName = typeof(TEntity).Name;            
            var str = Path.Combine(_baseDir, $"{typeName}_{entity.Id}.xml");
            if (File.Exists(str))
            {
                throw new ArgumentException("object exists", nameof(entity));
            }
            if (entity.Id == 0)
            {
                var str2 = $"{_baseDir}\\";
                int max = int.MinValue;
                var filePaths = Directory.GetFiles(str2);
                foreach (string s in filePaths)
                {
                    var idx = s.IndexOf('_');
                    var idx2 = s.IndexOf('.');
                    var t = s.Substring(idx + 1, idx2 - idx - 1);
                    var tmp = int.Parse(t);
                    if (tmp > max)
                    {
                        max = tmp;
                    }
                }
                str = Path.Combine(_baseDir, $"{typeName}_{max + 1}.xml");
                FileStream fs = new FileStream(str, FileMode.Create);
                XmlSerializer xs = new XmlSerializer(typeof(TEntity));
                xs.Serialize(fs, entity);
                fs.Close();
            }
            else
            {
                FileStream fs = new FileStream(str, FileMode.Create);
                XmlSerializer xs = new XmlSerializer(typeof(TEntity));
                xs.Serialize(fs, entity);
                fs.Close();
            }

        }

        /// <summary>
        /// Get retrieves an object from the database. 
        /// If the object with the given Id does not exist, the exception should be thrown.
        /// </summary>

        // TODO: Implement Get method
        public TEntity Get<TEntity>(int id) where TEntity : IEntity
        {
            var typeName = typeof(TEntity).Name;
            var str = Path.Combine(_baseDir, $"{typeName}_{id}.xml");
            if (!File.Exists(str))
            {
                throw new ArgumentException("object does not exist", nameof(id));
            }
            FileStream fs = new FileStream(str, FileMode.Open);
            XmlSerializer xs = new XmlSerializer(typeof(TEntity));
            TEntity ent = (TEntity)xs.Deserialize(fs);
            fs.Close();
            return ent;
        }

        /// <summary>
        /// Updates object in the database.
        /// If the object with the given Id does not exist, the exception should be thrown.
        /// </summary>

        // TODO: Implement Update method
        public void Update<TEntity>(TEntity entity) where TEntity : IEntity
        {
            var typeName = typeof(TEntity).Name;
            var str = Path.Combine(_baseDir, $"{typeName}_{entity.Id}.xml");
            if (!File.Exists(str))
            {
                throw new ArgumentException("object does not exist", nameof(entity));
            }
            FileStream fs = new FileStream(str, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
            XmlSerializer xs = new XmlSerializer(typeof(TEntity));
            xs.Serialize(fs, entity);
            fs.Close();
        }


        /// <summary>
        /// Deletes object from the database.
        /// If the object with the given Id does not exist, the exception should be thrown.
        /// </summary>

        // TODO: Implement Delete method
        public void Delete<TEntity>(int id) where TEntity : IEntity
        {
            var typeName = typeof(TEntity).Name;            
            var str = Path.Combine(_baseDir, $"{typeName}_{id}.xml");
            if (!File.Exists(str))
                throw new ArgumentException("object does not exist", nameof(id));
            File.Delete(str);
        }
    }

    [Serializable]
    public class User : IEntity
    {
        public User() { }
        
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        /// <summary>
        /// Property computed from FirstName and LastName.
        /// It should not be serialized to the file.
        /// </summary>        
        public string FullName => $"{FirstName} {LastName}";

        public override string ToString()
        {
            return $"USER: Id {Id}, FullName: {FullName}";
        }
    }

    [Serializable]    
    public class Product : IEntity
    {
        public Product() { } 
        
        public int Id { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return $"PRODUCT: Id: {Id}, Name {Name}";
        }
    }

    [Serializable]
    public class Order : IEntity
    {
        public Order() {}
        
        public Order(User user, Product product, int amount)
        {
            UserId = user.Id;
            ProductId = product.Id;
            Amount = amount;
        }

        public int Id { get; set; }

        public int UserId { get; set; }

        public int ProductId { get; set; }

        public int Amount { get; set; }

        public override string ToString()
        {
            return $"ORDER: Id {Id}, UserId: {UserId}, ProductId: {ProductId}, Amount: {Amount}";
        }
    }
}