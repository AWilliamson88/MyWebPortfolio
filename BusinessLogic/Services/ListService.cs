﻿using DataModels.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class ListService : IListService
    {
        private readonly PortfolioContext context;
        public ListService(PortfolioContext _context)
        {
            context = _context;
        }

    //    using (var context = _contextFactory.CreateDbContext())
    //    {
    //        // ...
    //    }

public void InitialiseDb()
        {
            if (context.ToDoLists.Any())
            {
                return;
            }

            ToDoList list1 = new ToDoList { Name = "Work Items" };
            ToDoList list2 = new ToDoList { Name = "Shopping List" };

            list1.ToDoItems.Add(new ToDoItem { Name = "Daily Planner" });
            list1.ToDoItems.Add(new ToDoItem { Name = "Get Coffee" });
            list1.ToDoItems.Add(new ToDoItem { Name = "Check Emails" });

            list2.ToDoItems.Add(new ToDoItem { Name = "Milk" });
            list2.ToDoItems.Add(new ToDoItem { Name = "Bread" });
            list2.ToDoItems.Add(new ToDoItem { Name = "Dinner" });

            context.Add(list1);
            context.Add(list2);
            context.SaveChanges();

            //ToDoList  list1 = new ToDoList { Name = "Work Items" };
            //ToDoList  list2 = new ToDoList { Name = "Shopping List" };

            //context.Add(list1);
            //context.Add(list2);
            //context.SaveChanges();

            //var lists = context.ToDoLists.ToList();

            //lists.First().ToDoItems.Add(new ToDoItem { Name = "Daily Planner" });
            //lists.First().ToDoItems.Add(new ToDoItem { Name = "Get Coffee" });
            //lists.First().ToDoItems.Add(new ToDoItem { Name = "Check Emails" });

            //lists.Last().ToDoItems.Add(new ToDoItem { Name = "Bread" });
            //lists.Last().ToDoItems.Add(new ToDoItem { Name = "Milk" });
            //lists.Last().ToDoItems.Add(new ToDoItem { Name = "Dinner" });

            //context.SaveChanges();
        }

        public async Task<List<ToDoList>> GetTasks()
        {
            return await context.ToDoLists.Include(l => l.ToDoItems).Distinct().ToListAsync();
            //return await context.ToDoLists.ToListAsync();
            //.Include(l => l.ToDoItems)
        }

        public void AddLists(IEnumerable<ToDoList> lists)
        {
            context.ToDoLists.AddRange(lists);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
