using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Models
{
    public class EFItemRepository : IItemRepository
    {
        ToDoListContext db = new ToDoListContext();
        public EFItemRepository(ToDoListContext connection = null)
        {
            if(connection == null)
            {
                this.db = new ToDoListContext();
            }
            else
            {
                this.db = connection;
            }
        }
        public IQueryable<Item> Items
        {
            get
            {
                return db.Items;
            }
        }

        public Item Edit(Item item)
        {
            db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return item;
        }

        public void Remove(Item item)
        {
            db.Items.Remove(item);
            db.SaveChanges();
        }

        public Item Save(Item item)
        {
            db.Items.Add(item);
            db.SaveChanges();
            return item;
        }

        public void DeleteAll()
        {
            db.Database.ExecuteSqlCommand("delete from Items");
        }
    }
}
