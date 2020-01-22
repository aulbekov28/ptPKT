import { Component, OnInit } from '@angular/core';

import { Todo, TodoItem } from '../../_models/todo.model';
import { TodoService } from '../../_services/todo.service';

@Component({
  selector: 'app-todo',
  templateUrl: './todo.component.html',
  styleUrls: ['./todo.component.css'],
  providers: [TodoService]
})
export class TodoComponent implements OnInit {

  newTodo: Todo = new Todo();
  todoList: Todo[] = [];

  constructor(private todoData: TodoService) { }

  ngOnInit() {
    this.todoData.getAll().subscribe(result => {
      this.todoList = result
    }, error => {
      console.log(error);
    })
  }

}
