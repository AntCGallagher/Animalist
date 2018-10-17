using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Animalist.Models;
using LiteDB;

namespace Animalist.Controllers {
    [Route("api/[controller]")]
    public class ListController : Controller {
        public IEnumerable<Animal> animals;
        public ListController() { }

        // Get a list of all the animals to display
        [HttpGet("get")]
        public IEnumerable<Animal> getAnimals() {
            // Access the database file
            using(var db = new LiteDatabase("animals.db"))
            {
                // Retrieve table containing animals
                var col = db.GetCollection<Animal>("animals");

                // Cast table into readable format for the front end
                this.animals = col.FindAll();
                
            }
            return this.animals;
        }

        // When a user wants to add an animal
        [HttpGet("add/{name}")]
        public void addToEnd(string name) {
            // Access the database file
            using(var db = new LiteDatabase("animals.db"))
            {
                // Retrieve table containing animals
                var col = db.GetCollection<Animal>("animals");

                // Item will be put at the end of the table
                var nextId = col.Count() + 1;

                // Insert item into table
                col.Insert(new Animal {Id = nextId, Name = name});
            }
        }

        // When a user wants to delete an animal
        [HttpDelete("delete/{id}")]
        public void deleteAtIndex(int id) {
            // Access the database file
            using(var db = new LiteDatabase("animals.db"))
            {
                // Retrieve table containing animals
                var col = db.GetCollection<Animal>("animals");

                // Cast table into iterable format
                this.animals = col.FindAll();
                Animal previous = null;
                foreach (Animal current in this.animals) {
                    // Move item to be deleted to end of list
                    if (current.Id > id) {
                        previous.Name = current.Name;
                        col.Update(previous);
                    } 
                    previous = current;

                    // If we've reached the end of the list, remove last element
                    if (current.Id == this.animals.Count()) {
                        col.Delete(this.animals.Count());
                    }
                }

            }
        }


    }
}
