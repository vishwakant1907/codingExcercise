using JSON_CRUD.Controllers;
using JSON_CRUD.Models;
using JSON_CRUD.Services.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace JSON_CRUD.Services.Concrete
{
    public class DataInventory : IDataInventory
    {
        public Inventory AddUpdateInventory(Inventory Model)
        {
            List<Inventory> inventory = new List<Inventory>();
            JSONReadWrite readWrite = new JSONReadWrite();
            inventory = JsonConvert.DeserializeObject<List<Inventory>>(readWrite.Read("Inventory.json", "data"));

            Inventory data = inventory.FirstOrDefault(x => x.Id == Model.Id);
            if (data == null)
            {
                inventory.Add(Model);
            }
            else
            {
                int index = inventory.FindIndex(x => x.Id == Model.Id);
                inventory[index] = Model;
            }
            string jSONString = JsonConvert.SerializeObject(data);
            readWrite.Write("Inventory.json", "data", jSONString);
            return data;
        }

        public void DeleteInventoryById(int Id)
        {
            List<Inventory> inventories = new List<Inventory>();
            JSONReadWrite readWrite = new JSONReadWrite();
            inventories = JsonConvert.DeserializeObject<List<Inventory>>(readWrite.Read("Inventory.json", "data"));

            int index = inventories.FindIndex(x => x.Id == Id);
            inventories.RemoveAt(index);

            string jSONString = JsonConvert.SerializeObject(inventories);
            readWrite.Write("Inventory.json", "data", jSONString);
        }

        public List<Inventory> GetInventory()
        {
            List<Inventory> inventoryList = new List<Inventory>();
            JSONReadWrite readWrite = new JSONReadWrite();
            inventoryList = JsonConvert.DeserializeObject<List<Inventory>>(readWrite.Read("Inventory.json", "data"));
            return inventoryList;
        }
    }
    
    public class JSONReadWrite
        {
            public JSONReadWrite() { }

            public string Read(string fileName, string location)
            {
                string root = "wwwroot";
                var path = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    root,
                    location,
                    fileName);

                string jsonResult;

                using (StreamReader streamReader = new StreamReader(path))
                {
                    jsonResult = streamReader.ReadToEnd();
                }
                return jsonResult;
            }

            public void Write(string fileName, string location, string jSONString)
            {
                string root = "wwwroot";
                var path = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    root,
                    location,
                    fileName);

                using (var streamWriter = File.CreateText(path))
                {
                    streamWriter.Write(jSONString);
                }
            }
        }
}
