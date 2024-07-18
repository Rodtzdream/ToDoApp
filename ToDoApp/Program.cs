using ToDoApp.Data.Context;
using ToDoApp.Data.Models;

var context = new ToDoContext();

//var user = new User
//{
//    Name = "John Doe"
//};

//var board = new Board
//{
//    Name = "My Board"
//};

//var todoItem = new ToDoItem
//{
//    Title = "My First Task",
//    Description = "This is my first task",
//    CreatedAt = DateTime.Now,
//    DueDate = DateTime.Now.AddDays(1),
//    Status = StatusEnum.ToDo,
//    Assignee = user,
//    Board = board
//};

//context.ToDoItems.Add(todoItem);

//await context.SaveChangesAsync();

var item = await context.ToDoItems.FindAsync(1);
item.Status = StatusEnum.InProgress;
;
await context.SaveChangesAsync();
;