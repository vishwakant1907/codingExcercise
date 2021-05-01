using JSON_CRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSON_CRUD.Services.Interface
{
   public interface IDataInventory
    {
        List<Inventory> GetInventory();

        Inventory AddUpdateInventory(Inventory Model);
        void DeleteInventoryById(int Id);
    }
}
